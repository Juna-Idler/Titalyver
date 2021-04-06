using System;
using System.Collections.Generic;
using System.Text;

namespace Juna.SoundTag.ID3v2.Frames
{
/*
     <Header for 'Text information frame', ID: "T000" - "TZZZ",
     excluding "TXXX" described in 4.2.6.>
     Text encoding                $xx
     Information                  <text string(s) according to encoding>
*/

	//とりあえずstring(s)には未対応で
	class TextInformation
	{
		public string FrameID { get { return frame_id; } }
		public TextEncoding TextEncoding { get { return text_encoding; } }
		public string Information { get { return information; } }


		private string frame_id;
		private TextEncoding text_encoding;
		private string information;


		public TextInformation( Frame frame )
		{
			if ( frame.FrameID0 != 'T' )
				throw new ArgumentException( "Frame ID is not begin with 'T'" );
			if ( frame.FrameID == "TXXX")
				throw new ArgumentException( "Frame ID \"TXXX\" is not Text informaition frame" );

			frame_id = frame.FrameID;
			text_encoding = (TextEncoding)frame.Data[0];

			information = Frame.GetText( frame.Data , 1 , frame.Size , text_encoding );
			int end = information.IndexOf( '\0' );
			if ( end >= 0 )
			{
				information = information.Substring( 0 , end );
			}
		}
	}
}
