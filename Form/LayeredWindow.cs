using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace Juna
{
	class LayeredWindow
	{
		private Form This = null;

		public LayeredWindow( Form form )
		{
			This = form;
//			SetWindowStyle( true );
		}

		public void SetWindowStyle( bool enable )
		{
			if ( enable )
			{
				SetWindowLong( This.Handle , GWL_EXSTYLE , GetWindowLong( This.Handle , GWL_EXSTYLE ) | WS_EX_LAYERED );
			}
			else
			{
				SetWindowLong( This.Handle , GWL_EXSTYLE , GetWindowLong( This.Handle , GWL_EXSTYLE ) & ~WS_EX_LAYERED );
			}
		}



		public void UpdateLayerImage( IntPtr hbitmap , int width , int height , byte opacity )
		{

			IntPtr screenDc = GetDC( IntPtr.Zero );
			IntPtr memDc = CreateCompatibleDC( screenDc );
			IntPtr oldBitmap = IntPtr.Zero;

			try
			{
				oldBitmap = SelectObject( memDc , hbitmap );

				Size size = new Size( width , height );
				Point pointSource = new Point( 0 , 0 );
				Point topPos = new Point( This.Left , This.Top );
				BLENDFUNCTION blend = new BLENDFUNCTION();
				blend.BlendOp = AC_SRC_OVER;
				blend.BlendFlags = 0;
				blend.SourceConstantAlpha = opacity;
				blend.AlphaFormat = AC_SRC_ALPHA;

				UpdateLayeredWindow( This.Handle , screenDc , ref topPos , ref size , memDc , ref pointSource , 0 , ref blend , ULW_ALPHA );
			}
			finally
			{
				ReleaseDC( IntPtr.Zero , screenDc );
				SelectObject( memDc , oldBitmap );
				DeleteDC( memDc );
			}
		}

		public void UpdateLayerImage( Bitmap bitmap , byte opacity )
		{
			IntPtr hbitmap = bitmap.GetHbitmap( Color.FromArgb( 0 ) );
			try
			{
				UpdateLayerImage( hbitmap , bitmap.Width , bitmap.Height , opacity );
			}
			finally
			{
				DeleteObject( hbitmap );
			}
		}


		private const int GWL_EXSTYLE = -20;
		private const int WS_EX_LAYERED = 0x00080000;

		[DllImport( "user32.dll" , CharSet = CharSet.Auto , SetLastError = true )]
		private extern static int GetWindowLong( IntPtr hwnd , int nIndex );
		[DllImport( "user32.dll" , CharSet = CharSet.Auto , SetLastError = true )]
		private extern static int SetWindowLong( IntPtr hwnd , int nIndex , int dwNewLong );


		[DllImport( "user32.dll" , ExactSpelling = true , SetLastError = true )]
		private static extern IntPtr GetDC( IntPtr hWnd );
		[DllImport( "user32.dll" , ExactSpelling = true )]
		private static extern int ReleaseDC( IntPtr hWnd , IntPtr hDC );

		[DllImport( "gdi32.dll" , ExactSpelling = true , SetLastError = true )]
		private static extern IntPtr CreateCompatibleDC( IntPtr hDC );
		[DllImport( "gdi32.dll" , ExactSpelling = true , SetLastError = true )]
		private static extern bool DeleteDC( IntPtr hdc );

		[DllImport( "gdi32.dll" , ExactSpelling = true )]
		private static extern IntPtr SelectObject( IntPtr hDC , IntPtr hObject );

		[DllImport( "gdi32.dll" , ExactSpelling = true , SetLastError = true )]
		public static extern bool DeleteObject( IntPtr hObject );


		private const byte AC_SRC_OVER = 0;
		private const byte AC_SRC_ALPHA = 1;
		private const int ULW_ALPHA = 2;

		[StructLayout( LayoutKind.Sequential , Pack = 1 )]
		private struct BLENDFUNCTION
		{
			public byte BlendOp;
			public byte BlendFlags;
			public byte SourceConstantAlpha;
			public byte AlphaFormat;
		}

		[DllImport( "user32.dll" , CharSet = CharSet.Auto , SetLastError = true )]
		private static extern int UpdateLayeredWindow(
			IntPtr hwnd ,
			IntPtr hdcDst ,
			[System.Runtime.InteropServices.In()]
            ref Point pptDst ,
			[System.Runtime.InteropServices.In()]
            ref Size psize ,
			IntPtr hdcSrc ,
			[System.Runtime.InteropServices.In()]
            ref Point pptSrc ,
			int crKey ,
			[System.Runtime.InteropServices.In()]
            ref BLENDFUNCTION pblend ,
			int dwFlags );

	}
}
