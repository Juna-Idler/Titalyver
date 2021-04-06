using System;
using System.Windows.Forms;
using System.Runtime.InteropServices; 


namespace Titalyver
{
	class JunaLyricsMessageListener
	{
		[DllImport( "JunaLyricsMessageListener.dll" )]
		private static extern bool JLM_Initialize( IntPtr hwnd );

		[DllImport( "JunaLyricsMessageListener.dll" )]
		private static extern void JLM_Terminalize();

		[DllImport( "JunaLyricsMessageListener.dll" )]
		private static extern bool JLM_ChangeWindowHandle( IntPtr hwnd );

		[DllImport( "JunaLyricsMessageListener.dll" )]
		private static extern uint JLM_GetMessageValue();

		[DllImport( "JunaLyricsMessageListener.dll" )]
		private static extern bool JLM_GetData(uint wait_ms);


		[DllImport( "JunaLyricsMessageListener.dll" )]
		private static extern uint JLM_GetLength();
		[DllImport( "JunaLyricsMessageListener.dll" )]
		private static extern IntPtr JLM_GetPath();
		[DllImport( "JunaLyricsMessageListener.dll" )]
		private static extern IntPtr JLM_GetTitle();
		[DllImport( "JunaLyricsMessageListener.dll" )]
		private static extern IntPtr JLM_GetArtist();
		[DllImport( "JunaLyricsMessageListener.dll" )]
		private static extern IntPtr JLM_GetAlbum();
		[DllImport( "JunaLyricsMessageListener.dll" )]
		private static extern IntPtr JLM_GetGenre();
		[DllImport( "JunaLyricsMessageListener.dll" )]
		private static extern IntPtr JLM_GetDate();
		[DllImport( "JunaLyricsMessageListener.dll" )]
		private static extern IntPtr JLM_GetComment();


		private uint RegiserMessageValue = JLM_GetMessageValue();


		~JunaLyricsMessageListener() { Terminalize(); }


		public bool Initialize(Form form)
		{
			return JLM_Initialize( form.Handle );
		}
		public void Terminalize()
		{
			JLM_Terminalize();
		}
		public bool ChangeWindowHandle( Form form )
		{
			return JLM_ChangeWindowHandle( form.Handle );
		}

		public enum enumPlaybackEvent {
			PBE_NULL = 0 ,
			PBE_New = 1 ,
			PBE_Stop = 2 ,
			PBE_SeekPlaying = 3 ,
			PBE_SeekPause = 4 ,
			PBE_PauseCancel = 5 ,
			PBE_Pause = 6 ,
		};

		public delegate void PlayBackEvent( enumPlaybackEvent pbevent , uint milisec );

		public bool TestMessage( System.Windows.Forms.Message m ,PlayBackEvent pbevent)
		{
			if ( m.Msg == RegiserMessageValue )
			{
				pbevent( (enumPlaybackEvent)m.WParam , (uint)m.LParam );
				return true;
			}
			return false;
		}

		public bool GetData( out uint total_milisec, out string path ,
							out string title , out string artist , out string album ,
							out string genre , out string date , out string comment ,
							uint wait_ms )
		{
			total_milisec = 0;
			path = title = artist = album = genre = date = comment = "";

			if ( !JLM_GetData( wait_ms ) )
				return false;

			total_milisec = JLM_GetLength();
			path = Marshal.PtrToStringUni( JLM_GetPath() );
			title = Marshal.PtrToStringUni( JLM_GetTitle() );
			artist = Marshal.PtrToStringUni( JLM_GetArtist() );
			album = Marshal.PtrToStringUni( JLM_GetAlbum() );
			genre = Marshal.PtrToStringUni( JLM_GetGenre() );
			date = Marshal.PtrToStringUni( JLM_GetDate() );
			comment = Marshal.PtrToStringUni( JLM_GetComment() );

			return true;
		}



	}
}
