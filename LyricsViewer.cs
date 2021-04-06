using System;
using System.Drawing;
using System.Runtime.InteropServices;



namespace Juna
{
	class RubyLyricsViewer
	{
		[DllImport( "GDI32.dll" )]
		private static extern bool DeleteObject( IntPtr objectHandle ); 
		[DllImport( "GDI32.dll" )]
		private static extern bool BitBlt( IntPtr hdc , int x , int y , int cx , int cy , IntPtr hdcSrc , int x1 , int y1 , uint rop );
		private const uint SRCCOPY = 0x00CC0020;


		[DllImport( "RKLyricsView.dll" )]
		public static extern void LyricsView_Terminalize();

		[DllImport( "RKLyricsView.dll" )]
		public static extern bool LyricsView_IsValid();

		public enum enumLyricsMode { LM_Null = 0 , LM_Lyrics = 1 , LM_Karaoke = 2 , LM_Text = 3 };
		[DllImport( "RKLyricsView.dll" )]
		private static extern int LyricsView_GetLyricsMode();
		public static enumLyricsMode GetLyricsMode() { return (enumLyricsMode)LyricsView_GetLyricsMode(); }


		[DllImport( "RKLyricsView.dll" )]
		public static extern void LyricsView_SetLyrics( [MarshalAs( UnmanagedType.LPWStr )] string lyrics );

		[DllImport( "RKLyricsView.dll" )]
		public static extern void LyricsView_SetKaraoke( [MarshalAs( UnmanagedType.LPWStr )] string lyrics , bool keephead );

		[DllImport( "RKLyricsView.dll" )]
		public static extern void LyricsView_SetText( [MarshalAs( UnmanagedType.LPWStr )] string lyrics );

		[DllImport( "RKLyricsView.dll" )]
		public static extern void LyricsView_SetRubyKaraoke( [MarshalAs( UnmanagedType.LPWStr )] string lyrics , bool keephead );

		[DllImport( "RKLyricsView.dll" )]
		private static extern bool LyricsView_SetFont( uint fontquality , IntPtr hfont , bool outline , int slip );

		public static void SetFont( Font font , bool antialias , bool outline , int slip )
		{
			IntPtr hfont = font.ToHfont();
			uint aa = 0;
			if ( antialias )
				aa = 3;
			LyricsView_SetFont( aa , hfont , outline , slip );
			DeleteObject( hfont );
		}


		[DllImport( "RKLyricsView.dll" )]
		public static extern void LyricsView_Resize( int width , int height );


		[DllImport( "RKLyricsView.dll" )]
		public static extern bool LyricsView_Update( int playtime_ms );


		[DllImport( "RKLyricsView.dll" )]
		public static extern bool LyricsView_UpdateCS( int playtime_ms );

		[DllImport( "RKLyricsView.dll" )]
		public static extern bool LyricsView_KaraokeUpdate( int playtime_ms , int bottom_margin );


		//return IntPtr is HDC
		[DllImport( "RKLyricsView.dll" )]
		public static extern IntPtr LyricsView_GetCanvasDC( out int width , out int height );
		//return IntPtr is HBITMAP
		[DllImport( "RKLyricsView.dll" )]
		public static extern IntPtr LyricsView_GetCanvasBitmap( out int width , out int height );

		public static void Draw( IntPtr hdc , int x , int y ,int srcx,int srcy, int width , int height )
		{
			int w,h;
			IntPtr image = LyricsView_GetCanvasDC( out w , out h );
			if (image == IntPtr.Zero)
				return;
			if (srcx + width > w)
				width -= srcx + width - w;
			if (srcy + height < h)
				height -= srcy + height - h;
			BitBlt( hdc , x , y , width , height , image , srcx , srcy , SRCCOPY );
		}

		[DllImport( "RKLyricsView.dll" )]
		private static extern void LyricsView_SetCurrentColor( byte r , byte g , byte b , byte or , byte og , byte ob );
		public static void SetCurrentColor( Color color , Color outlinecolor )
		{
			LyricsView_SetCurrentColor( color.R , color.G , color.B , outlinecolor.R , outlinecolor.G , outlinecolor.B );
		}


		[DllImport( "RKLyricsView.dll" )]
		private static extern void LyricsView_SetOtherColor( byte r , byte g , byte b , byte or , byte og , byte ob );
		public static void SetOtherColor( Color color , Color outlinecolor )
		{
			LyricsView_SetOtherColor( color.R , color.G , color.B , outlinecolor.R , outlinecolor.G , outlinecolor.B );
		}

		[DllImport( "RKLyricsView.dll" )]
		private static extern void LyricsView_SetStandbyColor( byte r , byte g , byte b , byte or , byte og , byte ob );
		public static void SetStandbyColor( Color color , Color outlinecolor )
		{
			LyricsView_SetStandbyColor( color.R , color.G , color.B , outlinecolor.R , outlinecolor.G , outlinecolor.B );
		}

		[DllImport( "RKLyricsView.dll" )]
		private static extern void LyricsView_SetNTTTextColor( byte r , byte g , byte b , byte or , byte og , byte ob );
		public static void SetNTTTextColor( Color color , Color outlinecolor )
		{
			LyricsView_SetNTTTextColor( color.R , color.G , color.B , outlinecolor.R , outlinecolor.G , outlinecolor.B );
		}


		[DllImport( "RKLyricsView.dll" )]
		public static extern void LyricsView_SetLyricsLayout( int left , int top , int right , int bottom );

		[DllImport( "RKLyricsView.dll" )]
		public static extern void LyricsView_SetBGImageLayout( int left , int top , int right , int bottom );



		[DllImport( "RKLyricsView.dll" )]
		private static extern void LyricsView_SetBGColor( byte a , byte r , byte g , byte b );
//		public static void SetBGColor( Color color )
//		{
//			LyricsView_SetBGColor( 255 , color.R , color.G , color.B );
//		}
		public static void SetBGColor( byte alpha , Color color )
		{
			LyricsView_SetBGColor( alpha , color.R , color.G , color.B );
		}

		[DllImport( "RKLyricsView.dll" )]
		private static extern void LyricsView_SetBGImage( IntPtr hbitmap );

		public static void SetBGImage( Bitmap bitmap )
		{
			if ( bitmap == null )
			{
				LyricsView_SetBGImage( IntPtr.Zero );
			}
			else
			{
				IntPtr hbitmap = bitmap.GetHbitmap( Color.FromArgb( 0 ) );
				LyricsView_SetBGImage( hbitmap );
				DeleteObject( hbitmap );
			}
		}

		[DllImport( "RKLyricsView.dll" )]
		public static extern void LyricsView_SetBGImagePosition( int position );

		[DllImport( "RKLyricsView.dll" )]
		private static extern void LyricsView_SetBGImageFilter( byte a , byte r , byte g , byte b , int translucent_alpha );

		public static void SetBGImageFilter( byte alpha , Color color , byte translucent_alpha )
		{
			LyricsView_SetBGImageFilter( alpha , color.R , color.G , color.B , translucent_alpha + 1 );
		}

		[DllImport( "RKLyricsView.dll" )]
		public static extern void LyricsView_SetBGImageLimitFlag( bool display_x , bool display_y ,
																  bool source_x , bool source_y ,
																  bool maxsize_x , bool maxsize_y );


		[DllImport( "RKLyricsView.dll" )]
		public static extern void LyricsView_SetBGImageMaxSize( int width , int height );


		[DllImport( "RKLyricsView.dll" )]
		private static extern void LyricsView_SetLineBackColor( byte a , byte r , byte g , byte b );
		public static void SetLineBackColor(byte alpha, Color color )
		{
			LyricsView_SetLineBackColor( alpha , color.R , color.G , color.B );
		}

		[DllImport( "RKLyricsView.dll" )]
		public static extern void LyricsView_SetLineBackPlusUp( int pluswidth );
		[DllImport( "RKLyricsView.dll" )]
		public static extern void LyricsView_SetLineBackPlusDown( int pluswidth );


		[DllImport( "RKLyricsView.dll" )]
		public static extern void LyricsView_SetScrollTime( int scroll_time , int fade_time );


		[DllImport( "RKLyricsView.dll" )]
		private static extern void LyricsView_SetVAlignment( int alignment , int y_offset );
		[DllImport( "RKLyricsView.dll" )]
		private static extern void LyricsView_SetHAlignment( int alignment );

		public static void SetAlignment( StringAlignment v_alignment,StringAlignment h_alignment , int y_offset )
		{
			int a = 0;
			switch ( v_alignment )
			{
			case StringAlignment.Near: a = 1; break;
//			case StringAlignment.Center: a = 0; break;
			case StringAlignment.Far: a = -1; break;
			}
			LyricsView_SetVAlignment( a , y_offset );
			a = 0;
			switch ( h_alignment )
			{
			case StringAlignment.Near: a = 1; break;
//			case StringAlignment.Center: a = 0; break;
			case StringAlignment.Far: a = -1; break;
			}
			LyricsView_SetHAlignment( a );
		}


		[DllImport( "RKLyricsView.dll" )]
		public static extern void LyricsView_SetSpace( int linespace , int charspace , int norubylinetopspace , int rubybottomspace );

		[DllImport( "RKLyricsView.dll" )]
		public static extern void LyricsView_SetRubyAlignment( int ruby_alignment );

		[DllImport( "RKLyricsView.dll" )]
		public static extern void LyricsView_SetMargin( int left , int top , int right , int bottom );


		[DllImport( "RKLyricsView.dll" )]
		private static extern void LyricsView_SetRubyLineMode( int mode );

		public enum enumRubyLineMode {RLM_Default = 0,RLM_Force = 1,RLM_Ignore = 2}
		public static void SetRubyLineMode( enumRubyLineMode mode )
		{
			LyricsView_SetRubyLineMode( (int)mode );
		}




		[DllImport( "RKLyricsView.dll" )]
		private static extern bool LyricsView_SetInfoTextFont( uint fontquality , IntPtr hfont , bool outline , int slip );

		public static void SetInfoTextFont( Font font , bool antialias , bool outline , int slip )
		{
			IntPtr hfont = font.ToHfont();
			uint aa = 0;
			if ( antialias )
				aa = 3;
			LyricsView_SetInfoTextFont( aa , hfont , outline , slip );
			DeleteObject( hfont );
		}


		[DllImport( "RKLyricsView.dll" )]
		private static extern void LyricsView_SetInfoTextColor( byte r , byte g , byte b , byte or , byte og , byte ob );
		public static void SetInfoTextColor( Color color , Color outlinecolor )
		{
			LyricsView_SetInfoTextColor( color.R , color.G , color.B , outlinecolor.R , outlinecolor.G , outlinecolor.B );
		}
		
		
		[DllImport( "RKLyricsView.dll" )]
		private static extern void LyricsView_SetInfoTextAlignment( int valignment , int halignment );
		public static void SetInfoTextAlignment( StringAlignment v_alignment , StringAlignment h_alignment )
		{
			int va = 0;
			switch ( v_alignment )
			{
			case StringAlignment.Near: va = 1; break;
			//			case StringAlignment.Center: a = 0; break;
			case StringAlignment.Far: va = -1; break;
			}
			int ha = 0;
			switch ( h_alignment )
			{
			case StringAlignment.Near: ha = 1; break;
			//			case StringAlignment.Center: a = 0; break;
			case StringAlignment.Far: ha = -1; break;
			}
			LyricsView_SetInfoTextAlignment( va , ha );
		}
		[DllImport( "RKLyricsView.dll" )]
		public static extern void LyricsView_SetInfoTextLayout( int left , int top , int right , int bottom );

		[DllImport( "RKLyricsView.dll" )]
		public static extern void LyricsView_SetInfoTextString( [MarshalAs( UnmanagedType.LPWStr )] string text );


		[DllImport( "RKLyricsView.dll" )]
		private static extern void LyricsView_SetFrameImage( IntPtr hbitmap );

		public static void SetFrameImage( Bitmap bitmap )
		{
			if ( bitmap == null )
			{
				LyricsView_SetFrameImage( IntPtr.Zero );
			}
			else
			{
				IntPtr hbitmap = bitmap.GetHbitmap( Color.FromArgb( 0 ) );
				LyricsView_SetFrameImage( hbitmap );
				DeleteObject( hbitmap );
			}
		}

	}
}
