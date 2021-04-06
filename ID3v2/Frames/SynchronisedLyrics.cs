using System;
using System.Collections.Generic;
using System.Text;

namespace Juna.SoundTag.ID3v2.Frames
{
	/*
		 <Header for 'Synchronised lyrics/text', ID: "SYLT">
		 Text encoding        $xx
		 Language             $xx xx xx
		 Time stamp format    $xx
		 Content type         $xx
		 Content descriptor   <text string according to encoding> $00 (00)
	 * 
		 Terminated text to be synced (typically a syllable)
		 Sync identifier (terminator to above string)   $00 (00)
		 Time stamp                                     $xx (xx ...)
	 */

	public enum SYLTContentType : byte
	{
		Other             = 0x00,
		Lyrics            = 0x01,
		TextTranscription = 0x02,
		Movement          = 0x03,
		Events            = 0x04,
		Chord             = 0x05,
		Trivia            = 0x06,
		URLsToWebpages    = 0x07,
		URLsToImages      = 0x08
	}

	public enum TimeStampFormat
	{
		AbsoluteTime32_MPEGFrames   = 0x01,
		AbsoluteTime32_MilliSeconds = 0x02
	}

	class SynchronisedLyrics
	{
		public struct SyncBlock
		{
			public string text;
			public uint time_stamp;
		}

		public string FrameID { get { return frame_id; } }
		public TextEncoding TextEncoding { get { return text_encoding; } }
		public string Language { get { return language; } }
		public TimeStampFormat TimeStampFormat { get { return time_stamp_format; } }
		public SYLTContentType ConentType { get { return content_type; } }
		public string Descriptor { get { return descriptor; } }
		public SyncBlock[] Lyrics { get { return lyrics; } }


		private string frame_id;
		private TextEncoding text_encoding;
		private string language;
		private TimeStampFormat time_stamp_format;
		private SYLTContentType content_type;
		private string descriptor;
		private SyncBlock[] lyrics;


		public SynchronisedLyrics( Frame frame )
		{
			if ( frame.FrameID != "SYLT" )
				throw new ArgumentException( "Frame ID is not 'SYLT'" );

			frame_id = frame.FrameID;
			text_encoding = (TextEncoding)frame.Data[0];

			language = "" + (char)frame.Data[1] + (char)frame.Data[2] + (char)frame.Data[3];

			time_stamp_format = (TimeStampFormat)frame.Data[4];
			content_type = (SYLTContentType)frame.Data[5];

			int start = 6;
			int end = Frame.GetTextEndPosition( frame.Data , start , text_encoding );
			descriptor = Frame.GetText( frame.Data , start , end , text_encoding );

			int text_terminal_size = Frame.TextTerminalSize( text_encoding );
			start = end + text_terminal_size;
			List<SyncBlock> list = new List<SyncBlock> ();

			while ( start < frame.Data.Length )
			{
				end = Frame.GetTextEndPosition( frame.Data , start , text_encoding );
				
				if (end < 0)
					throw new ArgumentException ("Synchronised Lyrics error");
				
				string text = Frame.GetText( frame.Data , start , end , text_encoding );
				start = end + text_terminal_size;
				uint time = (uint)((frame.Data[start] << 24) | (frame.Data[start + 1] << 16) | (frame.Data[start + 2] << 8) | frame.Data[start + 3]);
				start += 4;

				SyncBlock sb = new SyncBlock();
				sb.text = text;
				sb.time_stamp = time;

				list.Add(sb);
			}
			lyrics = list.ToArray();
		}
	}
}
