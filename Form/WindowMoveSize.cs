using System;

using System.Drawing;
using System.Windows.Forms;


namespace Juna
{
	class WindowMoveSize
	{
        Form This;
 
		private bool isDragLeft = false;
		public bool IsDragLeft { get { return isDragLeft; } }

		private Point DragPoint;

		private int sizingBarSize = 8;
		private int cornerSize = 16;

		public int SizingBarSize
		{
			get { return sizingBarSize; }
			set
			{
				if ( value < 0 )
					sizingBarSize = 0;
				else if ( value > This.Width / 4 || value > This.Height / 4 )
				{
					sizingBarSize = Math.Min( This.Width , This.Height ) / 4;
				}
				else
					sizingBarSize = value;
			}
		}
		public int CornerSize
		{
			get { return cornerSize; }
			set
			{
				if ( value < 0 )
					cornerSize = 0;
				else if ( value > This.Width / 2 || value > This.Height / 2 )
				{
					cornerSize = Math.Min( This.Width , This.Height ) / 2;
				}
				else
					cornerSize = value;
			}
		}

		private Cursor DefaultCursor;


		enum enumDragType {
			bit_left = 1,
			bit_right = 2,
			bit_top = 4,
			bit_bottom = 8,

			TopLeft = bit_top + bit_left,
			TopMiddle = bit_top,
			TopRight = bit_top + bit_right,
			MiddleLeft = bit_left,
			MiddleMiddle = 0,
			MiddleRight = bit_right,
			BottomLeft = bit_bottom + bit_left,
			BottomMiddle = bit_bottom,
			BottomRight = bit_bottom + bit_right,
		}

		private enumDragType DragType;


        public WindowMoveSize(Form form, int sizing_bar_size,int corner_size)
        {
			This = form;
			sizingBarSize = sizing_bar_size;
			cornerSize = corner_size;
			DefaultCursor = form.Cursor;

            This.MouseDown += new MouseEventHandler(MouseDown);
            This.MouseMove += new MouseEventHandler(MouseMove);
            This.MouseUp += new MouseEventHandler(MouseUp);
        }


		public const int WM_NCHITTEST = 0x0084;
		public const int HTCLIENT = 1;
		public void WndProc_WM_NCHITTEST(ref Message m)
		{
			m.Result = (IntPtr)HTCLIENT;
		}

        private void MouseDown(object sender, MouseEventArgs e)
        {
			if ( (e.Button & MouseButtons.Left) == MouseButtons.Left )
			{
				DragPoint = e.Location;

				isDragLeft = true;
				This.Capture = true;
			}
        }

		private void MouseMove(object sender, MouseEventArgs e)
        {

			if ( This.WindowState == FormWindowState.Maximized )
			{
				isDragLeft = false;
				This.Cursor = DefaultCursor;
				return;
			}

			if ( e.Button == MouseButtons.None )
			{
				Point diff = This.PointToClient( This.Location );
				Point p = new Point( e.X - diff.X , e.Y - diff.Y );
				if ( p.X < sizingBarSize )
				{
					if ( p.Y < cornerSize )
					{
						DragType =  enumDragType.TopLeft;
						This.Cursor = Cursors.SizeNWSE;
						return;
					}
					if ( p.Y >= This.Size.Height - cornerSize )
					{
						DragType = enumDragType.BottomLeft;
						This.Cursor = Cursors.SizeNESW;
						return;
					}
					DragType = enumDragType.MiddleLeft;
					This.Cursor = Cursors.SizeWE;
					return;
				}
				if ( p.X >= This.Size.Width - sizingBarSize )
				{
					if ( p.Y < cornerSize )
					{
						DragType = enumDragType.TopRight;
						This.Cursor = Cursors.SizeNESW;
						return;
					}
					if ( p.Y >= This.Size.Height - cornerSize )
					{
						DragType = enumDragType.BottomRight;
						This.Cursor = Cursors.SizeNWSE;
						return;
					}
					DragType = enumDragType.MiddleRight;
					This.Cursor = Cursors.SizeWE;
					return;
				}
				if ( p.Y < sizingBarSize )
				{
					if ( p.X < cornerSize )
					{
						DragType = enumDragType.TopLeft;
						This.Cursor = Cursors.SizeNWSE;
						return;
					}
					if ( p.X >= This.Size.Width - cornerSize )
					{
						DragType = enumDragType.TopRight;
						This.Cursor = Cursors.SizeNESW;
						return;
					}
					DragType = enumDragType.TopMiddle;
					This.Cursor = Cursors.SizeNS;
					return;
				}
				if ( p.Y >= This.Size.Height - sizingBarSize )
				{
					if ( p.X < cornerSize )
					{
						DragType = enumDragType.BottomLeft;
						This.Cursor = Cursors.SizeNESW;
						return;
					}
					if ( p.X >= This.Size.Width - cornerSize )
					{
						DragType = enumDragType.BottomRight;
						This.Cursor = Cursors.SizeNWSE;
						return;
					}
					DragType = enumDragType.BottomMiddle;
					This.Cursor = Cursors.SizeNS;
					return;
				}
				DragType = enumDragType.MiddleMiddle;
				This.Cursor = DefaultCursor;
				return;
			}


			if ( (e.Button & MouseButtons.Left) == MouseButtons.Left )
			{
				int move_x = e.X - DragPoint.X;
				int move_y = e.Y - DragPoint.Y;
				Rectangle wa = Screen.GetWorkingArea( This );

				if ( DragType == enumDragType.MiddleMiddle )
				{
					Point oldp = This.Location;
					Point newp = new Point( oldp.X + move_x , oldp.Y + move_y );
					if ( (e.Button & MouseButtons.Right) != MouseButtons.Right )
					{
						if ( oldp.X >= wa.Left && newp.X < wa.Left )
						{
							newp.X = wa.Left;
						}
						if ( oldp.Y >= wa.Top && newp.Y < wa.Top )
						{
							newp.Y = wa.Top;
						}
						if ( oldp.X + This.Size.Width <= wa.Right && newp.X + This.Size.Width > wa.Right )
						{
							newp.X = wa.Right - This.Size.Width;
						}
						if ( oldp.Y + This.Size.Height <= wa.Bottom && newp.Y + This.Size.Height > wa.Bottom )
						{
							newp.Y = wa.Bottom - This.Size.Height;
						}
					}

					This.Location = newp;
				}
				else
				{
					Point location = This.Location;
					Size size = This.Size;

					if ( (DragType & enumDragType.bit_left) != 0)
					{
						location.X += move_x;
						size.Width -= move_x;


						if ( location.X < wa.Left )
						{
							size.Width -= wa.Left - location.X;
							location.X = wa.Left;
						}

						if ( size.Width < This.MinimumSize.Width )
						{
							location.X -= This.MinimumSize.Width - size.Width;
							size.Width = This.MinimumSize.Width;
						}
					}
					else if ((DragType & enumDragType.bit_right) != 0)
					{
						DragPoint.X = e.X;
						int right = location.X + This.Size.Width + move_x;
						size.Width += move_x;
						if ( right > wa.Right )
						{
							size.Width -= right - wa.Right;
						}
					}

					if ((DragType & enumDragType.bit_top) != 0)
					{
						location.Y += move_y;
						size.Height -= move_y;

						if ( location.Y < wa.Top )
						{
							size.Height -= wa.Top - location.Y;
							location.Y = wa.Top;
						}

						if ( size.Height < This.MinimumSize.Height )
						{
							location.Y -= This.MinimumSize.Height - size.Height;
							size.Height = This.MinimumSize.Height;
						}
					}
					else if ((DragType & enumDragType.bit_bottom) != 0 )
					{
						DragPoint.Y = e.Y;
						int bottom = location.Y + This.Size.Height + move_y;
						size.Height += move_y;
						if ( bottom > wa.Bottom )
						{
							size.Height -= bottom - wa.Bottom;
						}
					}

					This.Size = size;
					This.Location = location;
				}

			}

        }
 
        private void MouseUp(object sender, MouseEventArgs e)
        {
			if ( (e.Button & MouseButtons.Left) == MouseButtons.Left )
			{
				isDragLeft = false;
				This.Capture = false;
			}
        }
    }
}
