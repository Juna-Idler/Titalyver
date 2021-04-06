using System;
using System.Collections.Generic;
using System.Text;

namespace Juna.SoundTag.ID3v2.Frames
{

	/*
	  <Header for 'Attached picture', ID: "APIC">
	   Text encoding$xx
	   MIME type<text string> $00
	   Picture type$xx
	   Description<text string according to encoding> $00 (00)
	   Picture data<binary data>
	*/
	public enum PictureType : byte
	{
		Other = 0x00 ,
		FileIcon = 0x01 ,
		OtherFileIcon = 0x02 ,
		FrontCover = 0x03 ,
		BackCover = 0x04 ,
		LeafletPage = 0x05 ,
		Media = 0x06 ,
		LeadArtist = 0x07 ,
		Artist = 0x08 ,
		Conductor = 0x09 ,
		Band = 0x0A ,
		Composer = 0x0B ,
		Lyricist = 0x0C ,
		RecordingLocation = 0x0D ,
		DuringRecording = 0x0E ,
		DuringPerformance = 0x0F ,
		MovieScreenCapture = 0x10 ,
		ColoredFish = 0x11 ,
		Illustration = 0x12 ,
		BandLogo = 0x13 ,
		PublisherLogo = 0x14
	}

	class AttachedPicture
	{


		public TextEncoding TextEncoding {get {return text_encoding;}}
		public string MINE_Type {get {return mine_type;}}
		public PictureType PictureType {get {return picture_type;}}
		public string Description {get {return description;}}
		public byte[] PictureData {get {return picture_data;}}



		private TextEncoding text_encoding;
		private string mine_type;
		private PictureType picture_type;
		private string description;
		private byte[] picture_data;

		public AttachedPicture( Frame frame )
		{
			if ( frame.FrameID != "APIC" )
			{
				throw new ArgumentException( "Frame ID is not APIC" );
			}
            text_encoding = (TextEncoding)frame.Data[0];

			int text_end = Frame.GetTextEndPosition(frame.Data,1,TextEncoding.UTF_8);
			if (text_end < 0)
				throw new ArgumentException( "MINE type string error" );
			mine_type = Frame.GetText( frame.Data , 1 , text_end , TextEncoding.UTF_8 );

			int offset = text_end + 1;

			picture_type = (PictureType)frame.Data[offset];
			offset++;

			text_end = Frame.GetTextEndPosition( frame.Data , offset , text_encoding );
			if ( text_end < 0 )
				throw new ArgumentException( "Description string error" );
			description = Frame.GetText( frame.Data , offset , text_end , text_encoding );

			offset = text_end + Frame.TextTerminalSize( text_encoding );

			picture_data = new byte[frame.Data.Length - offset];
			Array.Copy( frame.Data , offset , picture_data , 0 , picture_data.Length );

		}

	}
}
