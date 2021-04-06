using System;
using System.Collections.Generic;
using System.Text;

namespace Juna.SoundTag.ID3v2.Frames
{
/*
	 <Header for 'Comment', ID: "COMM">
	 Text encoding          $xx
	 Language               $xx xx xx
	 Short content descrip. <text string according to encoding> $00 (00)
	 The actual text        <full text string according to encoding>
*/

	class Comments
	{
		public string FrameID { get { return frame_id; } }
		public TextEncoding TextEncoding { get { return text_encoding; } }
		public string Language { get { return language; } }
		public string Descriptor { get { return descriptor; } }
		public string Comment { get { return comment; } }


		private string frame_id;
		private TextEncoding text_encoding;
		private string language;
		private string descriptor;
		private string comment;


		public Comments( Frame frame )
		{
			if ( frame.FrameID != "COMM" )
				throw new ArgumentException( "Frame ID is not 'COMM'" );

			frame_id = frame.FrameID;
			text_encoding = (TextEncoding)frame.Data[0];

			language = "" + (char)frame.Data[1] + (char)frame.Data[2] + (char)frame.Data[3];

			int start = 4;
			int end = Frame.GetTextEndPosition( frame.Data , start , text_encoding );
			descriptor = Frame.GetText( frame.Data , start , end , text_encoding );
			start = end + Frame.TextTerminalSize( text_encoding );
			comment = Frame.GetText( frame.Data , start , frame.Data.Length , text_encoding );
		}
	}
}
