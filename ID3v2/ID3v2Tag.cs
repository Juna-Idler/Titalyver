using System;
using System.Collections.Generic;
using System.Text;

namespace Juna.SoundTag.ID3v2
{
	class ID3v2Tag
	{
		[Flags]
		public enum HeaderFlags : byte
		{
			Unsynchronisation = 0x80 ,
			ExtendedHeader = 0x40 ,
			ExperimentalIndicator = 0x20 ,
			FooterPresent = 0x10
		}

		public byte MajorVersion { get { return major_version; } }
		public byte RevisionNumber { get { return revision_number; } }
		public HeaderFlags Flags { get { return flags; } }
		public int TagSize { get { return tag_size; } }

		public int ExtendedHeaderSize { get { return extended_header_size; } }
		public byte[] ExtendedHeaderData { get { return extended_header_data; } }

		public List<Frame> Frames { get { return frames; } }


		private byte major_version;
		private byte revision_number;
		private HeaderFlags flags;
		private int tag_size;

		private int extended_header_size = 0;
		private byte[] extended_header_data = null;

		private List<Frame> frames;

		public ID3v2Tag( System.IO.Stream stream )
		{

			if ( !stream.CanRead )
				throw new ArgumentNullException( "data" );

			byte[] head = new byte[10];
			int offset = stream.Read( head , 0 , 10 );
			if ( offset < 10 )
				throw new ArgumentException("data is too small" );

			if ( !(head[0] == 'I' &&  head[1] == 'D' &&  head[2] == '3'))
				throw new ArgumentException("data is not ID3v2" );

			major_version = head[3];
			revision_number = head[4];
			flags = (HeaderFlags)head[5];

			if ( major_version < 2 || major_version > 4 )
				throw new ArgumentException("not support ID3v2 major version " + major_version );

			tag_size = ((head[6] << 21) | (head[7] << 14) | (head[8] << 7) | head[9]);

			byte[] data = new byte[tag_size + 10];
			Array.Copy(head,data,10);
			stream.Read(data,10,tag_size);

			if ((flags & HeaderFlags.Unsynchronisation) != 0)	//unsync解除
			{
				int j = 10;
				for (int i = 10;i < 10 + tag_size;i++ )
				{
					data[i] = data[j];
					j += ((data[j] == 0xFF && data[j + 1] == 0) ? 2 : 1);
				}
				tag_size = j - 10;
			}
			ParseTag( data );
		}

		public ID3v2Tag(byte[] data)
		{
			if ( data == null )
				throw new ArgumentNullException( "data" );

			if ( data.Length < 10 )
				throw new ArgumentException("data is too small" );

			if ( !(data[0] == 'I' &&  data[1] == 'D' &&  data[2] == '3'))
				throw new ArgumentException("data is not ID3v2" );

			major_version = data[3];
			revision_number = data[4];
			flags = (HeaderFlags)data[5];

			if ( major_version < 2 || major_version > 4 )
				throw new ArgumentException("not support ID3v2 major version " + major_version );

			tag_size = ((data[6] << 21) | (data[7] << 14) | (data[8] << 7) | data[9]);

			if ((flags & HeaderFlags.Unsynchronisation) != 0)	//unsync解除
			{
				byte[] syncdata = new byte[tag_size + 10];
				Array.Copy( data , syncdata , 10 );
				int j = 10;
				for (int i = 10;i < 10 + tag_size;i++ )
				{
					syncdata[i] = data[j];
					j += ((data[j] == 0xFF && data[j + 1] == 0) ? 2 : 1);
				}
				data = syncdata;
				tag_size = j - 10;
			}

			ParseTag( data );
		}

		private void ParseTag(byte[] tag)
		{
			int frame_position = 10;
			if ( (flags & HeaderFlags.ExtendedHeader) != 0 )	//exheadは無視
			{
				extended_header_size = ((tag[10] << 21) | (tag[11] << 14) | (tag[12] << 7) | tag[13]);
				if ( major_version == 3 )
					extended_header_size += 4;
				extended_header_data = new byte[extended_header_size - 4];
				Array.Copy( tag , 14 , extended_header_data , 0 , extended_header_data.Length );
				frame_position += extended_header_size;
			}

			frames = new List<Frame>();

			int end_point = tag_size + 10;
			switch ( major_version )
			{
			case 2:
				break;
			case 3:
				while ( frame_position < end_point && tag[frame_position] != 0x00 )
				{
					Frame f = Frame.ReadV23( tag , frame_position );
					frame_position += f.Size + 10;
					frames.Add( f );
				}
				break;
			case 4:
				while ( frame_position < end_point && tag[frame_position] != 0x00 )
				{
					Frame f = Frame.ReadV24( tag , frame_position );
					frame_position += f.Size + 10;
					frames.Add( f );
				}
				break;
			}
		}


	}
}
