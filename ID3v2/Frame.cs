using System;
using System.Collections.Generic;
using System.Text;

namespace Juna.SoundTag.ID3v2
{
/*
     $00   ISO-8859-1 [ISO-8859-1]. Terminated with $00.
     $01   UTF-16 [UTF-16] encoded Unicode [UNICODE] with BOM. All
           strings in the same frame SHALL have the same byteorder.
           Terminated with $00 00.
     $02   UTF-16BE [UTF-16] encoded Unicode [UNICODE] without BOM.
           Terminated with $00 00.
     $03   UTF-8 [UTF-8] encoded Unicode [UNICODE]. Terminated with $00.
*/
	public enum TextEncoding : byte
	{
		ISO_8859_1 = 0 ,
		UTF_16 = 1 ,
		UTF_16BE = 2 ,
		UTF_8 = 3 ,
	}

	class Frame
	{
		[Flags]
		public enum FrameFlags : ushort
		{
			TagAlterPreservation = 0x4000 ,
			FileAlterPreservation = 0x2000 ,
			ReadOnly = 0x1000 ,
			GroupingIdentity = 0x0040 ,
			Compression = 0x0008 ,
			Encryption = 0x0004 ,
			Unsynchronisation = 0x0002 ,
			DataLengthIndicator = 0x0001
		}

		public byte FrameID0 { get { return frame_id0; } }
		public byte FrameID1 { get { return frame_id1; } }
		public byte FrameID2 { get { return frame_id2; } }
		public byte FrameID3 { get { return frame_id3; } }


		public string FrameID { get { return frame_id; } }
		public int Size { get { return size; } }
		public FrameFlags Flags { get { return flags; } }
		public byte[] Data { get { return data; } }


		private byte frame_id0;
		private byte frame_id1;
		private byte frame_id2;
		private byte frame_id3;
		private string frame_id;

		private int size;
		private FrameFlags flags;

		private byte[] data;


		private Frame()
		{
		}

		public static Frame ReadV23(byte[] data,int offset)
		{
			Frame new_ = new Frame();
			new_.frame_id0 = data[offset];
			new_.frame_id1 = data[offset + 1];
			new_.frame_id2 = data[offset + 2];
			new_.frame_id3 = data[offset + 3];
			new_.frame_id = "" + (char)new_.frame_id0 + (char)new_.frame_id1 + (char)new_.frame_id2 + (char)new_.frame_id3;

			new_.size = ((data[offset + 4] << 24) | (data[offset + 5] << 16) | (data[offset + 6] << 8) | data[offset + 7]);
			new_.flags = (FrameFlags)(
				((data[offset + 8] << 7) & 0x7000) |
				((data[offset + 9] >> 4) & 0x000C) |
				((data[offset + 9] << 1) & 0x0040));

			new_.data = new byte[new_.size];
			Array.Copy( data , offset + 10 , new_.data , 0 , new_.size );

			return new_;
		}

		public static Frame ReadV24( byte[] data , int offset )
		{
			Frame new_ = new Frame();
			new_.frame_id0 = data[offset];
			new_.frame_id1 = data[offset + 1];
			new_.frame_id2 = data[offset + 2];
			new_.frame_id3 = data[offset + 3];
			new_.frame_id = "" + (char)new_.frame_id0 + (char)new_.frame_id1 + (char)new_.frame_id2 + (char)new_.frame_id3;

			new_.size = ((data[offset + 4] << 21) | (data[offset + 5] << 14) | (data[offset + 6] << 7) | data[offset + 7]);
			new_.flags = (FrameFlags)((data[offset + 8] << 8) | data[offset + 9]);

			new_.data = new byte[new_.size];
			Array.Copy( data , offset + 10 , new_.data , 0 , new_.size );

			return new_;
		}

		public static System.Text.Encoding GetTextEncoding( TextEncoding text_encoding ,byte[] bom = null )
		{
			switch ( text_encoding )
			{
			case TextEncoding.UTF_16:
				if (bom == null || bom.Length < 2 )
					return System.Text.Encoding.Unicode;
				if ( bom[0] == 0xFE && bom[1] == 0xFF )
					return System.Text.Encoding.BigEndianUnicode;
				else
					return System.Text.Encoding.Unicode;
			case TextEncoding.UTF_16BE:
				return System.Text.Encoding.BigEndianUnicode;
			case TextEncoding.UTF_8:
				return System.Text.Encoding.UTF8;
			case TextEncoding.ISO_8859_1:
				return System.Text.Encoding.Default;
			default:
				return System.Text.Encoding.Default;
			}
		}

		public static int GetTextEndPosition( byte[] data , int start , TextEncoding text_encoding )
		{
			switch ( text_encoding )
			{
			case TextEncoding.UTF_16:
			case TextEncoding.UTF_16BE:
				for ( int i = start ; i < data.Length - 1 ; i += 2 )
					if ( data[i] == 0 && data[i + 1] == 0 )
						return i;
				break;
			case TextEncoding.ISO_8859_1:
			case TextEncoding.UTF_8:
				for ( int i = start ; i < data.Length ; i++ )
					if ( data[i] == 0 )
						return i;
				break;
			}
			return -1;
		}

		public static string GetText( byte[] data , int start ,int end, TextEncoding text_encoding )
		{
			int bom = 0;
			System.Text.Encoding encode;
			switch ( text_encoding )
			{
			case TextEncoding.UTF_16:
				if ( data[start] == 0xFE && data[start + 1] == 0xFF )
					encode = System.Text.Encoding.BigEndianUnicode;
				else
					encode = System.Text.Encoding.Unicode;
				bom = 2;
				break;
			case TextEncoding.UTF_16BE:
				encode = System.Text.Encoding.BigEndianUnicode;
				break;
			case TextEncoding.UTF_8:
				encode = System.Text.Encoding.UTF8;
				if ( data.Length >= 3 && data[start] == 0xEF && data[start + 1] == 0xBB && data[start + 2] == 0xBF )
					bom = 3;
				break;
			case TextEncoding.ISO_8859_1:
				encode = System.Text.Encoding.Default;
				break;
			default:
				return "";
			}
			start += bom;
			return encode.GetString( data , start , end - start );
		}
/*
		public static int FindPattern( byte[] data , int start , byte[] pattern )
		{
			for ( int i = start ; i < data.Length ; i++ )
			{
				int d = i , p = 0;
				while ( data[d++] == pattern[p++] )
				{
					if ( p == pattern.Length )
						return i;
					if ( d == data.Length )
						return -1;
				}
			}
			return -1;
		}
*/
		public static int TextTerminalSize( TextEncoding text_encoding )
		{
			switch ( text_encoding )
			{
			case TextEncoding.UTF_16:
			case TextEncoding.UTF_16BE:
				return 2;
			case TextEncoding.ISO_8859_1:
			case TextEncoding.UTF_8:
				return 1;
			default:
				return -1;
			}
		}

	}
}
