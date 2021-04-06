using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Titalyver
{
	public partial class FormTiming : Form
	{
		Form1 This;
		public FormTiming( Form1 form )
		{
			This = form;
			InitializeComponent();
		}

		private void FormTiming_Load( object sender , EventArgs e )
		{
			Rectangle wa = Screen.GetWorkingArea( This );
			Point ThisLeftBottom = This.PointToScreen( new Point( 0 , This.ClientSize.Height ) );

			if ( ThisLeftBottom.Y + this.Height > wa.Bottom )
			{
				this.Top = wa.Bottom - this.Height;
			}
			else
			{
				this.Top = ThisLeftBottom.Y;
			}
			this.Left = ThisLeftBottom.X;
			this.Width = This.ClientSize.Width;


			decimal d = -This.TimingOffset;
			d /= 1000;
			if ( d > 0 )
			{
				numericUpDownMaximum.Value = decimal.Ceiling( d * 2 ) / 2;
			}
			else if ( d < 0 )
			{
				numericUpDownMinimum.Value = decimal.Floor( d * 2 ) / 2;
			}

			trackBar1.Minimum = (int)(numericUpDownMinimum.Value * 100);
			trackBar1.Maximum = (int)(numericUpDownMaximum.Value * 100);

			trackBar1.Value = -This.TimingOffset / 10;
			ChangeTiming();
		}

		private void FormTiming_FormClosed( object sender , FormClosedEventArgs e )
		{
			This.UncheckTSMITiming();
			This.Timing = null;

		}

		private void trackBar1_Scroll( object sender , EventArgs e )
		{
			ChangeTiming();
		}
		private void ChangeTiming()
		{
			decimal t = trackBar1.Value / 100m;

			if ( t < 0 )
			{
				textBoxTiming.Text = string.Format( "{0:F2}" , t );
			}
			else if ( t > 0 )
			{
				textBoxTiming.Text = "+" + string.Format( "{0:F2}" , t );
			}
			else
			{
				textBoxTiming.Text = "0";
			}
			This.TimingOffset = -trackBar1.Value * 10;
		}




		private void numericUpDownMinimum_ValueChanged( object sender , EventArgs e )
		{
			int min = (int)(numericUpDownMinimum.Value * 100);
			if ( trackBar1.Value < min )
			{
				trackBar1.Value = min;
				ChangeTiming();
			}
			trackBar1.Minimum = min;

		}

		private void numericUpDownMaximum_ValueChanged( object sender , EventArgs e )
		{
			int max =  (int)(numericUpDownMaximum.Value * 100);
			if ( trackBar1.Value > max )
			{
				trackBar1.Value = max;
				ChangeTiming();
			}
			trackBar1.Maximum = max;
		}



	}
}
