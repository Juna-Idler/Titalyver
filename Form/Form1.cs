using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using System.Runtime.InteropServices;
using System.IO;
using System.Threading;

using LyricsTimeTag;

namespace Titalyver
{

	public partial class Form1 : Form
	{
		public string ExePath = "";
		public string ApplicationFolder = "";

		private Juna.WindowMoveSize WindowMoveSize;
		private Juna.LayeredWindow LayeredWindow;

		[DllImport( "winmm.dll" )]
		private static extern uint timeGetTime();


		private JunaLyricsMessageListener MessageListener = new JunaLyricsMessageListener();

		private uint TotalMilisec = 0;
		private string Path = "";
		private string Title = "";
		private string Artist = "";
		private string Album = "";
		private string Genre = "";
		private string Date = "";
		private string Comment = "";

		private string SoundFolder = "";
		private string SoundFileName = "";
		private string SoundFileExtension = "";

		private uint StartTime;

		private int ViewMilisecond = 0;
		public int TimingOffset = 0;

		public int WheelYOffset = 0;

		public FormSetting Setting = null;
		public FormTiming Timing = null;


		public string LyricsSearchPath = "";
		private List<string> LyricsSearchPathList = new List<string>();

		public bool NoLyrics = true;
		public string NoLyricsFormat = "";

		private Juna.LyricsTextFile LyricsFile = new Juna.LyricsTextFile();
		public string text_editor_path = "";
		public string text_editor_param = "";

		public string BGImageSearchPath = "";
		private List<string> BGImageSearchPathList = new List<string>();
		private string BGImageFileName = "";


		public enum enumKaraokeMode { SelectExt = 0 , Ignore = 1 , Force = 2 ,KaraokeModeMax}
		public enumKaraokeMode KaraokeMode = enumKaraokeMode.SelectExt;

		public Form1()
		{
			ExePath = System.Windows.Forms.Application.ExecutablePath;
			ApplicationFolder = System.IO.Path.GetDirectoryName( ExePath );
			if ( ApplicationFolder != System.IO.Path.GetPathRoot( ExePath ) )
				ApplicationFolder += '\\';

			WindowMoveSize = new Juna.WindowMoveSize( this , 8 , 16 );
			InitializeComponent();
			this.MouseWheel += new System.Windows.Forms.MouseEventHandler( this.Form1_MouseWheel );
			this.SetStyle( ControlStyles.Opaque , true );

			LayeredWindow = new Juna.LayeredWindow( this );
			LayeredWindow.SetWindowStyle( true );

			Properties.Settings setting = Properties.Settings.Default;

			Juna.RubyLyricsViewer.SetFont( setting.LyricsFont , setting.AntiAlias , setting.FontOutline , (int)setting.FontOutlineSlip );

			Juna.RubyLyricsViewer.SetBGColor( setting.BGColorAlpha , setting.BGColor );
			Juna.RubyLyricsViewer.SetCurrentColor( setting.CurrentColor , setting.CurrentOutlineColor );
			Juna.RubyLyricsViewer.SetOtherColor( setting.OtherColor , setting.OtherOutlineColor );
			Juna.RubyLyricsViewer.SetStandbyColor( setting.StandbyColor , setting.StandbyOutlineColor );
			Juna.RubyLyricsViewer.SetNTTTextColor( setting.NTTTColor , setting.NTTTOutlineColor );
	
			Juna.RubyLyricsViewer.SetLineBackColor( (byte)setting.CurrentBackAlpha , setting.CurrentBackColor );
			Juna.RubyLyricsViewer.LyricsView_SetLineBackPlusUp( (int)setting.CurrentBackPlusUp );
			Juna.RubyLyricsViewer.LyricsView_SetLineBackPlusDown( (int)setting.CurrentBackPlusDown );

			Juna.RubyLyricsViewer.SetAlignment( setting.VAlignment , setting.HAlignment , setting.YOffset );

			Juna.RubyLyricsViewer.LyricsView_SetMargin( (int)setting.LeftMargin , (int)setting.TopMargin , (int)setting.RightMargin , (int)setting.BottomMargin );
			Juna.RubyLyricsViewer.LyricsView_SetSpace( (int)setting.LineSpace , (int)setting.CharSpace , (int)setting.NoRubyPlusSpace , (int)setting.RubyBottomSpace );
			Juna.RubyLyricsViewer.LyricsView_SetRubyAlignment( setting.RubyAlignment );

			Juna.RubyLyricsViewer.LyricsView_SetScrollTime( (int)setting.ScrollTime , (int)setting.FadeTime );


			Juna.RubyLyricsViewer.LyricsView_SetBGImagePosition(setting.BGIPosition);

			Juna.RubyLyricsViewer.SetBGImageFilter( (byte)Math.Max( setting.BGIFilterAlpha , 0 ) , setting.BGIFilterColor , setting.BGImageAlpha );

			Juna.RubyLyricsViewer.LyricsView_SetBGImageLimitFlag( setting.BGILimitDisplayX , setting.BGILimitDisplayY , setting.BGILimitSourceX , setting.BGILimitSourceY , setting.BGILimitMaxSizeX , setting.BGILimitMaxSizeY );

			Juna.RubyLyricsViewer.LyricsView_SetBGImageMaxSize( setting.BGILimitSize.Width , setting.BGILimitSize.Height );


			SetLyricsLayout( setting.LyricsLayoutPosition , setting.LyricsLayoutLeft , setting.LyricsLayoutTop , setting.LyricsLayoutRight , setting.LyricsLayoutBottom );
			SetBGILayout( setting.BGILayoutPosition , setting.BGILayoutLeft , setting.BGILayoutTop , setting.BGILayoutRight , setting.BGILayoutBottom );

			SetTextLayout( setting.InfoTextLayoutPosition , setting.InfoTextLayoutLeft , setting.InfoTextLayoutTop , setting.InfoTextLayoutRight , setting.InfoTextLayoutBottom );
			Juna.RubyLyricsViewer.SetInfoTextFont( setting.InfoTextFont , setting.InfoTextAntiAlias , setting.InfoTextOutline , setting.InfoTextOutlineSlip );
			Juna.RubyLyricsViewer.SetInfoTextColor( setting.InfoTextColor , setting.InfoTextOutlineColor );
			Juna.RubyLyricsViewer.SetInfoTextAlignment( setting.InfoTextVAlignment , setting.InfoTextHAlignment );
			SetDisplayText( setting.InfoText );


			this.ShowInTaskbar = setting.ShowInTaskbar;
			toolStripMenuItemShowTaskBar.Checked = setting.ShowInTaskbar;
			notifyIcon1.Visible = setting.TaskTray;
			toolStripMenuItemTaskTray.Checked = setting.TaskTray;

			toolStripMenuItemMinimize.Enabled = setting.ShowInTaskbar || setting.TaskTray;

			SetLyricsSearchPath(setting.LyricsSearchPath);
			SetBGImageSearchPath(setting.BGImageSearchPath);
			NoLyricsFormat = setting.NoLyricsFormat;
			text_editor_path = setting.EditorPath;
			text_editor_param = setting.EditorParam;

			{
				int k = setting.KaraokeMode;
				if ( k < 0 ) k = 0;
				if ( k >= (int)enumKaraokeMode.KaraokeModeMax ) k = (int)enumKaraokeMode.KaraokeModeMax - 1;
				setting.KaraokeMode = k;
				KaraokeMode = (enumKaraokeMode)k;
			}

			timer1.Interval = setting.TimerInterval;

			LoadFrameImage( setting.FrameImagePath );
		}
		private void Form1_Load( object sender , EventArgs e )
		{
			Properties.Settings setting = Properties.Settings.Default;
			if ( this.Size == setting.WindowSize )
				Form1_SizeChanged( null , null );
			else
				this.Size = setting.WindowSize;

			this.Location = setting.WindowPosition;

			Rectangle cr = RectangleToScreen( this.ClientRectangle );
			Rectangle wa = Screen.GetWorkingArea( this );
			if ( cr.Right < wa.Left )
				this.Left = wa.Left;
			if ( cr.Bottom < wa.Top )
				this.Top = wa.Top;
			if ( cr.Left >= wa.Right )
				this.Left = wa.Right - this.Width;
			if ( cr.Top >= wa.Bottom )
				this.Top = wa.Bottom - this.Height;


			while ( !MessageListener.Initialize( this ) )
			{
				DialogResult dr = MessageBox.Show( "Mutexが作れなかったので起動できませんでした" , "エラー" , MessageBoxButtons.RetryCancel , MessageBoxIcon.Exclamation );
				if ( dr == System.Windows.Forms.DialogResult.Cancel )
				{
					this.Close();
					return;
				}
			}
			if ( !SetNewData() )
			{
				Juna.RubyLyricsViewer.LyricsView_SetText( "No Data\n" );
			}
			LayeredWindow.SetWindowStyle( true );
			DrawLyrics();
		}
		private void Form1_FormClosed( object sender , FormClosedEventArgs e )
		{
			MessageListener.Terminalize();

			Properties.Settings setting = Properties.Settings.Default;

			setting.WindowPosition = this.Location;
			setting.WindowSize = this.Size;

			setting.Save();

			Properties.Settings set = new Properties.Settings();
		}


		protected override void WndProc( ref Message m )
		{
			MessageListener.TestMessage( m , PlaybackEvent );


			switch ( m.Msg )
			{
			case Juna.WindowMoveSize.WM_NCHITTEST:
				WindowMoveSize.WndProc_WM_NCHITTEST( ref m );
				return;
			}
			                    
			base.WndProc( ref m );
		}

		public void SetLyricsSearchPath( string path )
		{
			LyricsSearchPath = path;
			StringReader sr = new StringReader( path );
			LyricsSearchPathList.Clear();
			string line;
			while ( (line = sr.ReadLine()) != null )
			{
				LyricsSearchPathList.Add( line );
			}
		}

		public void SetBGImageSearchPath( string path )
		{
			BGImageSearchPath = path;
			StringReader sr = new StringReader( path );
			BGImageSearchPathList.Clear();
			string line;
			while ( (line = sr.ReadLine()) != null )
			{
				BGImageSearchPathList.Add( line );
			}
		}


		private string ReplaceAliasText( string aliastext )
		{
			StringBuilder sb = new StringBuilder( aliastext );
			sb.Replace( "%FileFolder%" , SoundFolder );
			sb.Replace( "%FileName%" , SoundFileName );
			sb.Replace( "%FileExt%" , SoundFileExtension );
			sb.Replace( "%Title%" , Title );
			sb.Replace( "%Artist%" , Artist );
			sb.Replace( "%Album%" , Album );
			sb.Replace( "%Genre%" , Genre );
			sb.Replace( "%Date%" , Date );
			sb.Replace( "%Comment%" , Comment );
			sb.Replace( "%ExeFolder%" , ApplicationFolder );

			sb.Replace( "%Minute%" , (TotalMilisec / 1000 / 60).ToString() );
			sb.Replace( "%Second%" , (TotalMilisec / 1000 % 60).ToString() );
			sb.Replace( "%Second2%" , string.Format( "{0:D2}" , TotalMilisec / 1000 % 60 ) );
			sb.Replace( "%MiliSec%" , string.Format( "{0:D3}" , TotalMilisec % 1000 ) );

			return sb.ToString();
		}

		public void LoadBGImage()
		{
			foreach ( string line in BGImageSearchPathList )
			{
				if ( line == "" )
					break;
				string path = ReplaceAliasText( line );

				if ( path.Contains( "*" ) )
				{
					string directory = System.IO.Path.GetDirectoryName( path );
					string name = System.IO.Path.GetFileName( path );
					try
					{
						string[] files = System.IO.Directory.GetFiles( directory , name );
						foreach ( string file in files )
						{
							if ( file == BGImageFileName )
								return;
							try
							{
								using ( Bitmap bitmap = new Bitmap( file ) )
								{
									BGImageFileName = file;
									Juna.RubyLyricsViewer.SetBGImage( bitmap );
									return;
								}
							}
							catch ( Exception e )
							{
							}
						}
					}
					catch ( Exception e )
					{
					}
				}
				else
				{
					if ( path == BGImageFileName )
						return;
					try
					{
						using ( Bitmap bitmap = new Bitmap( path ) )
						{
							BGImageFileName = path;
							Juna.RubyLyricsViewer.SetBGImage( bitmap );
							return;
						}
					}
					catch ( Exception e )
					{
					}
					try
					{
						using ( FileStream fs = new FileStream( path , FileMode.Open , FileAccess.Read , FileShare.Read ) )
						{
							Juna.SoundTag.ID3v2.ID3v2Tag v2 = new Juna.SoundTag.ID3v2.ID3v2Tag( fs );
							Juna.SoundTag.ID3v2.Frame f = v2.Frames.Find( delegate( Juna.SoundTag.ID3v2.Frame t ) { return t.FrameID == "APIC"; } );
							if ( f != null )
							{
								Juna.SoundTag.ID3v2.Frames.AttachedPicture apic = new Juna.SoundTag.ID3v2.Frames.AttachedPicture( f );

								using ( System.IO.MemoryStream ms = new MemoryStream( apic.PictureData ) )
								{
									using ( Bitmap bitmap = new Bitmap(ms) )
									{
										BGImageFileName = path;
										Juna.RubyLyricsViewer.SetBGImage( bitmap );
										return;
									}
								}
								 
							}
						}
					}
					catch ( Exception e )
					{
					}
				}
			}
			Juna.RubyLyricsViewer.SetBGImage( null );
			BGImageFileName = "";
		}

		public void LoadLyrics()
		{
			LyricsFile.Close();
			foreach ( string line in LyricsSearchPathList )
			{
				string path = ReplaceAliasText( line );
				if ( LyricsFile.Open( path ) )
					if ( LyricsFile.Text != "" )
						break;
			}
			if ( LyricsFile.Text == "" )
			{
				NoLyrics = true;
				SetNoLyricsString();
				return;
			}
			switch ( KaraokeMode )
			{
			case enumKaraokeMode.Ignore:
				Juna.RubyLyricsViewer.LyricsView_SetLyrics( LyricsFile.Text );
				break;
			case enumKaraokeMode.Force:
				{
					string ext = System.IO.Path.GetExtension( LyricsFile.FilePath );
					if ( ext.ToLower() == ".krr" )
						Juna.RubyLyricsViewer.LyricsView_SetRubyKaraoke( LyricsFile.Text , !Properties.Settings.Default.KaraokeLastNext );
					else
						Juna.RubyLyricsViewer.LyricsView_SetKaraoke( LyricsFile.Text , !Properties.Settings.Default.KaraokeLastNext );
				}
				break;
			default:
				{
					string ext = System.IO.Path.GetExtension( LyricsFile.FilePath );
					if (ext.ToLower() == ".krr")
						Juna.RubyLyricsViewer.LyricsView_SetRubyKaraoke( LyricsFile.Text , !Properties.Settings.Default.KaraokeLastNext );
					else if ( ext.ToLower() == ".kra" )
						Juna.RubyLyricsViewer.LyricsView_SetKaraoke( LyricsFile.Text , !Properties.Settings.Default.KaraokeLastNext );
					else
						Juna.RubyLyricsViewer.LyricsView_SetLyrics( LyricsFile.Text );
				}
				break;
			}
			if ( !Juna.RubyLyricsViewer.LyricsView_IsValid() )
			{
				Juna.RubyLyricsViewer.LyricsView_SetText( LyricsFile.Text );
			}

			NoLyrics = false;
			return;
		}
		public void SetNoLyricsString()
		{
			string no_lyrics_string = ReplaceAliasText( NoLyricsFormat );
			Juna.RubyLyricsViewer.LyricsView_SetLyrics( no_lyrics_string );
		}


		public bool SetNewData()
		{
			if (this.WheelYOffset != 0)
			{
				this.WheelYOffset = 0;
//				SetAlignment();
			}
			if ( !MessageListener.GetData( out TotalMilisec , out Path , out Title , out Artist , out Album , out Genre , out Date , out Comment , 100 ) )
				return false;

			if ( Path.Length == 0 )
				return false;

			string filepath = Path.Replace( "file://" , "" );
			SoundFolder = System.IO.Path.GetDirectoryName( filepath );
			if ( SoundFolder != System.IO.Path.GetPathRoot( filepath ) )
				SoundFolder += '\\';
			SoundFileName = System.IO.Path.GetFileNameWithoutExtension( filepath );
			if ( Title == "" )
				Title = SoundFileName;
			SoundFileExtension = System.IO.Path.GetExtension( filepath );

			LoadBGImage();

			SetDisplayText(Properties.Settings.Default.InfoText);

			LoadLyrics();
			SetAlignment();

			return true;
		}

		public void SetAlignment()
		{
			Properties.Settings setting = Properties.Settings.Default;
			if ( Juna.RubyLyricsViewer.GetLyricsMode() == Juna.RubyLyricsViewer.enumLyricsMode.LM_Text )
			{
				Juna.RubyLyricsViewer.SetAlignment( setting.NTTTVAlignment, setting.HAlignment , setting.NTTTYOffset + WheelYOffset );
			}
			else
			{
				Juna.RubyLyricsViewer.SetAlignment( setting.VAlignment , setting.HAlignment , setting.YOffset + WheelYOffset );
			}
		}

		class Rect
		{
			public Rect() { left = 0; top = 0; right = 0; bottom = 0; }
			public Rect( int l , int t , int r , int b ) { left = r; top = t; right = r; bottom = b; }
			public int left , top , right , bottom;
		}
		private Rect LayoutRectangle( int position , int left , int top , int right , int bottom )
		{
			Rect r = new Rect();
			switch ( position % 3 )
			{
			case 1:
				r.left = left;
				if ( right <= left )
					r.right = Size.Width;
				else
					r.right = right;
				break;
			case 2:
				r.left = left;
				r.right = Size.Width - right;
				break;
			case 0:
				if ( left <= right )
					r.left = 0;
				else
					r.left = Size.Width - left;
				r.right = Size.Width - right;
				break;
			}
			switch ( (position + 2) / 3 )
			{
			case 3:
				r.top = top;
				if ( bottom <= top )
					r.bottom = Size.Height;
				else
					r.bottom = bottom;
				break;
			case 2:
				r.top = top;
				r.bottom = Size.Height - bottom;
				break;
			case 1:
				if ( top <= bottom )
					r.top = 0;
				else
					r.top = Size.Height - top;
				r.bottom = Size.Height - bottom;
				break;
			}
			return r;
		}

		public void SetLyricsLayout( int position , int left , int top , int right , int bottom )
		{
			Rect r = LayoutRectangle( position , left , top , right , bottom );
			Juna.RubyLyricsViewer.LyricsView_SetLyricsLayout( r.left , r.top , r.right , r.bottom );
		}
		public void SetBGILayout( int position , int left , int top , int right , int bottom )
		{
			Rect r = LayoutRectangle( position , left , top , right , bottom );
			Juna.RubyLyricsViewer.LyricsView_SetBGImageLayout( r.left , r.top , r.right , r.bottom );
		}
		public void SetTextLayout( int position , int left , int top , int right , int bottom )
		{
			Rect r = LayoutRectangle( position , left , top , right , bottom );
			Juna.RubyLyricsViewer.LyricsView_SetInfoTextLayout( r.left , r.top , r.right , r.bottom );
		}

		public void SetDisplayText(string text)
		{
			Juna.RubyLyricsViewer.LyricsView_SetInfoTextString( ReplaceAliasText( text ) );
		}


		private void PlaybackEvent( JunaLyricsMessageListener.enumPlaybackEvent pbevent , uint milisec )
		{
			switch ( pbevent )
			{
			case JunaLyricsMessageListener.enumPlaybackEvent.PBE_NULL:
				break;
			case JunaLyricsMessageListener.enumPlaybackEvent.PBE_New:
				{
					if ( SetNewData() )
					{
						StartTime = milisec;
						timer1.Enabled = true;
						this.Invalidate();
					}
					else
					{
						MessageBox.Show( "GetFileMappingData Error" );
					}
				}
				break;
			case JunaLyricsMessageListener.enumPlaybackEvent.PBE_Stop:
				timer1.Enabled = false;
				break;
			case JunaLyricsMessageListener.enumPlaybackEvent.PBE_SeekPlaying:
				{
					uint time = timeGetTime();
					StartTime = time - milisec;
					ViewMilisecond = (int)milisec;
					timer1.Enabled = true;
					DrawLyrics();
				}
				break;
			case JunaLyricsMessageListener.enumPlaybackEvent.PBE_SeekPause:
				{
					uint time = timeGetTime();
					StartTime = time - milisec;
					ViewMilisecond = (int)milisec;
					timer1.Enabled = false;
					DrawLyrics();
				}
				break;
			case JunaLyricsMessageListener.enumPlaybackEvent.PBE_PauseCancel:
				{
					uint time = timeGetTime();
					StartTime = time - milisec;
					timer1.Enabled = true;
				}
				break;
			case JunaLyricsMessageListener.enumPlaybackEvent.PBE_Pause:
				timer1.Enabled = false;
				break;
			}
		}


		private void timer1_Tick( object sender , EventArgs e )
		{
			ViewMilisecond = (int)(timeGetTime() - StartTime);
			DrawLyrics();
		}

		public void DrawLyrics()
		{
			if ( !Juna.RubyLyricsViewer.LyricsView_IsValid() )
				return;

			if ( this.WindowState == FormWindowState.Minimized )
				return;

			if ( Properties.Settings.Default.KaraokeDisplay && Juna.RubyLyricsViewer.GetLyricsMode() == Juna.RubyLyricsViewer.enumLyricsMode.LM_Karaoke )
				Juna.RubyLyricsViewer.LyricsView_KaraokeUpdate( ViewMilisecond + TimingOffset , Properties.Settings.Default.KaraokeDisplayBottomMargin );
			else if ( Properties.Settings.Default.ContinuousScroll )
				Juna.RubyLyricsViewer.LyricsView_UpdateCS( ViewMilisecond + TimingOffset );
			else
				Juna.RubyLyricsViewer.LyricsView_Update( ViewMilisecond + TimingOffset );

			int w,h;
			IntPtr hbitmap = Juna.RubyLyricsViewer.LyricsView_GetCanvasBitmap( out w , out h );
			LayeredWindow.UpdateLayerImage( hbitmap , w , h , Properties.Settings.Default.LayeredWindowAlpha );
//			this.Invalidate();
		}


		private void Form1_ResizeEnd( object sender , EventArgs e )
		{
		}

		private void Form1_SizeChanged( object sender , EventArgs e )
		{
			Juna.RubyLyricsViewer.LyricsView_Resize( Width , Height );

			Properties.Settings setting = Properties.Settings.Default;
			SetLyricsLayout( setting.LyricsLayoutPosition , setting.LyricsLayoutLeft , setting.LyricsLayoutTop , setting.LyricsLayoutRight , setting.LyricsLayoutBottom );
			SetBGILayout( setting.BGILayoutPosition , setting.BGILayoutLeft , setting.BGILayoutTop , setting.BGILayoutRight , setting.BGILayoutBottom );
			SetTextLayout( setting.InfoTextLayoutPosition , setting.InfoTextLayoutLeft , setting.InfoTextLayoutTop , setting.InfoTextLayoutRight , setting.InfoTextLayoutBottom );

			DrawLyrics();
		}

		public bool LoadFrameImage( string filename )
		{
			if ( filename == null || filename.Length == 0)
			{
				Properties.Settings.Default.FrameImagePath = " ";
				Juna.RubyLyricsViewer.SetFrameImage( null );
				DrawLyrics();
				return true;
			}
			if ( filename[0] == ' ' )
			{
				Properties.Settings.Default.FrameImagePath = filename;
				Juna.RubyLyricsViewer.SetFrameImage( null );
				DrawLyrics();
				return true;
			}
			try
			{
				using (Bitmap bitmap = new Bitmap( filename ))
				{
					Juna.RubyLyricsViewer.SetFrameImage( bitmap );
					WindowMoveSize.SizingBarSize = Math.Max( bitmap.Width , bitmap.Height ) / 2;
					WindowMoveSize.CornerSize = WindowMoveSize.SizingBarSize * 2;
					Properties.Settings.Default.FrameImagePath = filename;
					DrawLyrics();
					return true;
				}
			}
			catch ( Exception e )
			{
			}
			return false;
		}


		private bool IsDragRight = false;
		private Point DragRight;

		private void Form1_MouseDown( object sender , MouseEventArgs e )
		{
			if ( (e.Button & MouseButtons.Right) == MouseButtons.Right )
			{
				DragRight = e.Location;
			}
			if ( (e.Button & MouseButtons.Middle) == MouseButtons.Middle )
			{
				this.WheelYOffset = 0;
				SetAlignment();
				DrawLyrics();
			}

		}


		private void Form1_MouseMove( object sender , MouseEventArgs e )
		{
			if ( (e.Button & MouseButtons.Right) == MouseButtons.Right )
			{
				if ( Math.Abs( e.X - DragRight.X ) + Math.Abs( e.Y - DragRight.Y ) > 10 )
				{
					IsDragRight = true;
				}
			}
		}

		private void Form1_MouseUp( object sender , MouseEventArgs e )
		{

			if ( (e.Button & MouseButtons.Right) == MouseButtons.Right )
			{
				IsDragRight = false;
			}
		}

		private void Form1_MouseWheel( object sender , System.Windows.Forms.MouseEventArgs e )
		{
			Properties.Settings setting = Properties.Settings.Default;
			int height = setting.LyricsFont.Height;
			int d = e.Delta / 120;

			this.WheelYOffset += d * height;

			SetAlignment();
			DrawLyrics();
		}






		private void contextMenuStrip1_Opening( object sender , CancelEventArgs e )
		{
			if (WindowMoveSize.IsDragLeft || IsDragRight)
				e.Cancel = true;


			if (WindowState == FormWindowState.Maximized)
				toolStripMenuItemFullScreen.Checked = true;
			else
				toolStripMenuItemFullScreen.Checked = false;

		}

		public void UncheckTSMIOpenSetting()
		{
			toolStripMenuItemOpenSetting.Checked = false;
		}
		private void toolStripMenuItemOpenSetting_Click( object sender , EventArgs e )
		{
			if ( Setting == null )
			{
				Setting = new FormSetting( this );
				Rectangle wa = Screen.GetWorkingArea( this );
				Point p = MousePosition;
				if (p.X + Setting.Width > wa.Right)
					p.X = wa.Right - Setting.Width;
				if (p.Y + Setting.Height > wa.Bottom)
					p.Y = wa.Bottom - Setting.Height;
				Setting.Location =  p;
				Setting.Show( this );
				toolStripMenuItemOpenSetting.Checked = true;
			}
			else
			{
				Setting.Close();
			}
		}


		public void UncheckTSMITiming()
		{
			toolStripMenuItemTiming.Checked = false;
		}
		private void toolStripMenuItemTiming_Click( object sender , EventArgs e )
		{
			if ( Timing == null )
			{
				Timing = new FormTiming( this );
				Timing.Show( this );
				toolStripMenuItemTiming.Checked = true;
			}
			else
			{
				Timing.Close();
			}

		}

		private void toolStripMenuItemExit_Click( object sender , EventArgs e )
		{
			this.Close();
		}



		private void toolStripMenuItemTaskTray_Click( object sender , EventArgs e )
		{
			if ( toolStripMenuItemTaskTray.Checked )
			{
				notifyIcon1.Visible = true;
				toolStripMenuItemMinimize.Enabled = true;
				Properties.Settings.Default.TaskTray = true;
			}
			else
			{
				notifyIcon1.Visible = false;
				toolStripMenuItemMinimize.Enabled = toolStripMenuItemShowTaskBar.Checked;
				Properties.Settings.Default.TaskTray = false;
			}
		}

		private void toolStripMenuItemShowTaskBar_Click( object sender , EventArgs e )
		{
			if ( toolStripMenuItemShowTaskBar.Checked )
			{
				this.ShowInTaskbar = true;
				toolStripMenuItemMinimize.Enabled = true;
				Properties.Settings.Default.ShowInTaskbar = true;
			}
			else
			{
				this.ShowInTaskbar = false;
				toolStripMenuItemMinimize.Enabled = toolStripMenuItemTaskTray.Checked;
				Properties.Settings.Default.ShowInTaskbar = false;
			}
			//ウインドウハンドルが変わるので
			MessageListener.ChangeWindowHandle( this );
			//レイヤードフラグがリセットされるようなので再設定再描画
			LayeredWindow.SetWindowStyle( true );
			DrawLyrics();
		}

		private void toolStripMenuItemTopMost_Click( object sender , EventArgs e )
		{
			if ( toolStripMenuItemTopMost.Checked )
			{
				this.TopMost = true;
			}
			else
			{
				this.TopMost = false;
			}
		}


		private void toolStripMenuItemFullScreen_Click( object sender , EventArgs e )
		{
			if ( toolStripMenuItemFullScreen.Checked )
			{
				WindowState = FormWindowState.Maximized;
			}
			else
			{
				WindowState = FormWindowState.Normal;
			}
		}

		private void toolStripMenuItemMinimize_Click( object sender , EventArgs e )
		{
			WindowState = FormWindowState.Minimized;
			if ( !this.ShowInTaskbar )
			{
				this.Visible = false;
			}
		}



		private void toolStripMenuItemOpenLyrics_Click( object sender , EventArgs e )
		{
			if ( LyricsFile.FilePath != "" )
			{
				if ( LyricsFile.FType == Juna.LyricsTextFile.FileType.Text )
				{
					string param = text_editor_param.Replace( "%1" , LyricsFile.FilePath );

					try
					{
						System.Diagnostics.Process.Start( text_editor_path , param );
					}
					catch ( Exception )
					{
						MessageBox.Show( "例外エラーが発生しました" );
					}

				}
				else
				{
					MessageBox.Show( "埋め込み歌詞は開けません" );
				}
			}
		}




		private void notifyIcon1_MouseClick( object sender , MouseEventArgs e )
		{
			if ( e.Button == System.Windows.Forms.MouseButtons.Left )
			{
				Visible = true;
				if ( WindowState == FormWindowState.Minimized )
				{
					WindowState = FormWindowState.Normal;
				}
				Activate();
			}
			else if ( e.Button == System.Windows.Forms.MouseButtons.Right )
			{
			}
		}

		private void notifyIcon1_MouseDoubleClick( object sender , MouseEventArgs e )
		{
			if ( e.Button == System.Windows.Forms.MouseButtons.Left )
			{
			}
			else if ( e.Button == System.Windows.Forms.MouseButtons.Right )
			{
			}
		}



	}
}
