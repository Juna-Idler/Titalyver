using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Titalyver
{
	public partial class FormSetting : Form
	{
		Form1 This;
		bool Initializing = true;
		public FormSetting( Form1 form )
		{
			This = form;
			InitializeComponent();
			groupBoxFrameImage.AllowDrop = true;

			Properties.Settings setting = Properties.Settings.Default;


			//＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝表示＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝

			Font f = setting.LyricsFont;
			textBoxFontText.Text = f.Name + " " + f.Size + (f.Bold ? " Bold" : "") + (f.Italic ? " Italic" : "");

			//			labelCurrentColor.BackColor = setting.CurrentColor;
			//			labelCurrentOutlineColor.BackColor = setting.CurrentOutlineColor;
			//			labelOtherColor.BackColor = setting.OtherColor;
			//			labelOtherOutlineColor.BackColor = setting.OtherOutlineColor;
			//			numericUpDownCurrentBackAlpha.Value = setting.CurrentBackAlpha;
			//			labelCurrentBackColor.BackColor = setting.CurrentBackColor;
			labelBGColor.BackColor = setting.BGColor;

			//			numericUpDownScrollTime.Value = setting.ScrollTime;
			//			numericUpDownFadeMax.Value = setting.FadeTime;

			switch ( setting.HAlignment )
			{
			case StringAlignment.Center: radioButtonHAlignmentCenter.Checked = true; break;
			case StringAlignment.Far: radioButtonHAlignmentRight.Checked = true; break;
			default: radioButtonHAlignmentLeft.Checked = true; break;
			}

			listBoxVAlignment.SelectedIndex = (int)setting.VAlignment;

			comboBoxRubyAlignment.SelectedIndex = setting.RubyAlignment;

			checkBoxContinuousScroll.Checked = setting.ContinuousScroll;
			if ( setting.ContinuousScroll )
				labelScrollTime.Enabled = false;

			numericUpDownYOffset.Value = setting.YOffset;

			//＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝歌詞＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝

			textBoxLyricsSearchPath.Text = This.LyricsSearchPath;
			textBoxNoLyricsFormat.Text = This.NoLyricsFormat;

			if ( setting.KaraokeLastNext )
				radioButtonKaraokeLastNext.Checked = true;
			else
				radioButtonKaraokeLastLast.Checked = true;


			//＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝画像＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝

			textBoxBGImageSearchPath.Text = This.BGImageSearchPath;

			labelBGIFilterColor.BackColor = setting.BGIFilterColor;
			if ( setting.BGIFilterAlpha < 0 )
			{
				checkBoxBGIFilter.Checked = false;
				numericUpDownBGIFilterAlpha.Value = -setting.BGIFilterAlpha;
			}
			else
			{
				checkBoxBGIFilter.Checked = true;
				numericUpDownBGIFilterAlpha.Value = setting.BGIFilterAlpha;
			}

			checkBoxLimitDisplayX.Checked = setting.BGILimitDisplayX;
			checkBoxLimitDisplayY.Checked = setting.BGILimitDisplayY;
			checkBoxLimitSourceX.Checked = setting.BGILimitSourceX;
			checkBoxLimitSourceY.Checked = setting.BGILimitSourceY;
			checkBoxLimitMaxX.Checked = setting.BGILimitMaxSizeX;
			checkBoxLimitMaxY.Checked = setting.BGILimitMaxSizeY;

			numericUpDownBGILimitX.Value = setting.BGILimitSize.Width;
			numericUpDownBGILimitY.Value = setting.BGILimitSize.Height;

			int p = setting.BGIPosition;
			switch ( p % 3 )
			{
			case 1: checkBoxBGILeft.Checked = true; break;
			case 2: break;
			case 0: checkBoxBGIRight.Checked = true; break;
			}
			switch ( (p + 2) / 3 )
			{
			case 3: checkBoxBGITop.Checked = true; break;
			case 2: break;
			case 1: checkBoxBGIBottom.Checked = true; break;
			}

			textBoxFrameImagePath.Text = setting.FrameImagePath;
			if ( setting.FrameImagePath == "" || setting.FrameImagePath[0] == ' ' )
				checkBoxFrameImageShow.Checked = false;
			else
				checkBoxFrameImageShow.Checked = true;


			//＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝その他＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝

			textBoxEditorPath.Text = This.text_editor_path;
			textBoxEditorParam.Text = This.text_editor_param;

			System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration( System.Configuration.ConfigurationUserLevel.PerUserRoamingAndLocal );
			textBoxConfigSavePath.Text = config.FilePath;

			numericUpDownTimerInterval.Value = This.timer1.Interval;


			//＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝レイアウト＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝

			p = setting.LyricsLayoutPosition;
			switch ( p % 3 )
			{
			case 1: checkBoxLayoutLyricsLeft.Checked = true; break;
			case 2: break;
			case 0: checkBoxLayoutLyricsRight.Checked = true; break;
			}
			switch ( (p + 2) / 3 )
			{
			case 3: checkBoxLayoutLyricsTop.Checked = true; break;
			case 2: break;
			case 1: checkBoxLayoutLyricsBottom.Checked = true; break;
			}
			numericUpDownLayoutLyricsLeft.Value = setting.LyricsLayoutLeft;
			numericUpDownLayoutLyricsRight.Value = setting.LyricsLayoutRight;
			numericUpDownLayoutLyricsTop.Value = setting.LyricsLayoutTop;
			numericUpDownLayoutLyricsBottom.Value = setting.LyricsLayoutBottom;

			p = setting.BGILayoutPosition;
			switch ( p % 3 )
			{
			case 1: checkBoxLayoutBGILeft.Checked = true; break;
			case 2: break;
			case 0: checkBoxLayoutBGIRight.Checked = true; break;
			}
			switch ( (p + 2) / 3 )
			{
			case 3: checkBoxLayoutBGITop.Checked = true; break;
			case 2: break;
			case 1: checkBoxLayoutBGIBottom.Checked = true; break;
			}
			numericUpDownLayoutBGILeft.Value = setting.BGILayoutLeft;
			numericUpDownLayoutBGIRight.Value = setting.BGILayoutRight;
			numericUpDownLayoutBGITop.Value = setting.BGILayoutTop;
			numericUpDownLayoutBGIBottom.Value = setting.BGILayoutBottom;


			textBoxLayoutText.Text = setting.InfoText;
			checkBoxLayoutTextAntiAlias.Checked = setting.InfoTextAntiAlias;
			labelLayoutTextColor.BackColor = setting.InfoTextColor;
			checkBoxLayoutTextOutline.Checked = setting.InfoTextOutline;
			labelLayoutTextOutlineColor.BackColor = setting.InfoTextOutlineColor;
			listBoxLayoutTextHAlignment.SelectedIndex = (int)setting.InfoTextHAlignment;
			listBoxLayoutTextVAlignment.SelectedIndex = (int)setting.InfoTextVAlignment;

			f = setting.InfoTextFont;
			textBoxLayoutTextFontText.Text = f.Name + " " + f.Size + (f.Bold ? " Bold" : "") + (f.Italic ? " Italic" : "");
			numericUpDownLayoutTextOutlineSlip.Value = setting.InfoTextOutlineSlip;

			p = setting.InfoTextLayoutPosition;
			switch ( p % 3 )
			{
			case 1: checkBoxLayoutTextLeft.Checked = true; break;
			case 2: break;
			case 0: checkBoxLayoutTextRight.Checked = true; break;
			}
			switch ( (p + 2) / 3 )
			{
			case 3: checkBoxLayoutTextTop.Checked = true; break;
			case 2: break;
			case 1: checkBoxLayoutTextBottom.Checked = true; break;
			}
			numericUpDownLayoutTextLeft.Value = setting.InfoTextLayoutLeft;
			numericUpDownLayoutTextRight.Value = setting.InfoTextLayoutRight;
			numericUpDownLayoutTextTop.Value = setting.InfoTextLayoutTop;
			numericUpDownLayoutTextBottom.Value = setting.InfoTextLayoutBottom;

			//＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝拡張＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝

			labelStandbyColor.BackColor = setting.StandbyColor;
			labelStandbyOutlineColor.BackColor = setting.StandbyOutlineColor;
			labelNTTTColor.BackColor = setting.NTTTColor;
			labelNTTTOutlineColor.BackColor = setting.NTTTOutlineColor;

			listBoxNTTTVAlignment.SelectedIndex = (setting.NTTTVAlignment == StringAlignment.Near) ? 0 : 1;
			numericUpDownNTTTYOffset.Value = setting.NTTTYOffset;


			switch ( This.KaraokeMode )
			{
			case Form1.enumKaraokeMode.SelectExt: radioButtonKaraokeSelectExt.Checked = true; break;
			case Form1.enumKaraokeMode.Ignore: radioButtonKaraokeIgnore.Checked = true; break;
			case Form1.enumKaraokeMode.Force: radioButtonKaraokeForce.Checked = true; break;
			}

			checkBoxKaraokeDisplay.Checked = Properties.Settings.Default.KaraokeDisplay;
			numericUpDownKaraokeBottomMargin.Value = Properties.Settings.Default.KaraokeDisplayBottomMargin;

			numericUpDownBGColorAlpha.Value = setting.BGColorAlpha > 0 ? setting.BGColorAlpha : 1;
			numericUpDownWindowAlpha.Value = setting.LayeredWindowAlpha > 128 ? setting.LayeredWindowAlpha : 128;
			numericUpDownBGImageAlpha.Value = setting.BGImageAlpha > 0 ? setting.BGImageAlpha : 1;

			Initializing = false;
		}

		private void FormSetting_FormClosed( object sender , FormClosedEventArgs e )
		{
			This.UncheckTSMIOpenSetting();
			This.Setting = null;
			This.Activate();
		}


		/*=================================================================================================
		 *
		 *										表示 
		 * 
		=================================================================================================*/

		private void SetFont(Font font)
		{
			Properties.Settings setting = Properties.Settings.Default;
			Juna.RubyLyricsViewer.SetFont( font , checkBoxAntiAlias.Checked , checkBoxOutline.Checked , (int)numericUpDownSlip.Value );

			This.DrawLyrics();
		}
		private void buttonFont_Click( object sender , EventArgs e )
		{
			FontDialog dialog = new FontDialog();
			dialog.Font = Properties.Settings.Default.LyricsFont;
			if ( dialog.ShowDialog() == DialogResult.OK )
			{
				Properties.Settings.Default.LyricsFont = dialog.Font;
				SetFont( dialog.Font );
				Font f = dialog.Font;
				textBoxFontText.Text = f.Name + " " + f.Size + (f.Bold ? " Bold" : "") + (f.Italic ? " Italic" : "");
			}
		}

		private void checkBoxAntiAlias_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Properties.Settings.Default.AntiAlias = checkBoxAntiAlias.Checked;
			SetFont( Properties.Settings.Default.LyricsFont );
		}

		private void checkBoxOutline_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Properties.Settings.Default.FontOutline = checkBoxOutline.Checked;
			SetFont( Properties.Settings.Default.LyricsFont );
		}

		private void numericUpDownSlip_ValueChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Properties.Settings.Default.FontOutlineSlip = (int)numericUpDownSlip.Value;
			SetFont( Properties.Settings.Default.LyricsFont );
		}


		private static bool LabelColorChagneDialog( Label label )
		{
			ColorDialog cd = new ColorDialog();
			cd.Color = label.BackColor;
			if ( cd.ShowDialog() == DialogResult.OK )
			{
				label.BackColor = cd.Color;
				return true;
			}
			return false;
		}

		private void labelCurrentColor_Click( object sender , EventArgs e )
		{
			Label this_ = (Label)sender;
			if ( LabelColorChagneDialog( this_ ) )
			{
				Properties.Settings setting = Properties.Settings.Default;

				//				setting.CurrentColor = labelCurrentColor.BackColor;
				//				setting.CurrentOutlineColor = labelCurrentOutlineColor.BackColor;
				Juna.RubyLyricsViewer.SetCurrentColor( labelCurrentColor.BackColor , labelCurrentOutlineColor.BackColor );

				This.DrawLyrics();
			}
		}

		private void labelOtherColor_Click( object sender , EventArgs e )
		{
			Label this_ = (Label)sender;
			if ( LabelColorChagneDialog( this_ ) )
			{
				Properties.Settings setting = Properties.Settings.Default;

				//				setting.OtherColor = labelOtherColor.BackColor;
				//				setting.OtherOutlineColor = labelOtherOutlineColor.BackColor;
				Juna.RubyLyricsViewer.SetOtherColor( labelOtherColor.BackColor , labelOtherOutlineColor.BackColor );

				This.DrawLyrics();
			}

		}

		private void labelBGColor_Click( object sender , EventArgs e )
		{
			Label this_ = (Label)sender;
			if ( LabelColorChagneDialog( this_ ) )
			{
				Properties.Settings setting = Properties.Settings.Default;

				setting.BGColor = labelBGColor.BackColor;
				Juna.RubyLyricsViewer.SetBGColor( setting.BGColorAlpha , labelBGColor.BackColor );

				This.DrawLyrics();
			}
		}



		private void labelCurrentBackColor_Click( object sender , EventArgs e )
		{
			Label this_ = (Label)sender;
			if ( LabelColorChagneDialog( this_ ) )
			{
				Properties.Settings setting = Properties.Settings.Default;

				//				setting.CurrentBackColor = labelCurrentBackColor.BackColor;
				Juna.RubyLyricsViewer.SetLineBackColor( (byte)numericUpDownCurrentBackAlpha.Value , labelCurrentBackColor.BackColor );

				This.DrawLyrics();
			}
		}

		private void numericUpDownCurrentBackAlpha_ValueChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Properties.Settings setting = Properties.Settings.Default;

			//			setting.CurrentBackAlpha = numericUpDownCurrentBackAlpha.Value;
			Juna.RubyLyricsViewer.SetLineBackColor( (byte)numericUpDownCurrentBackAlpha.Value , labelCurrentBackColor.BackColor );

			This.DrawLyrics();
		}

		private void numericUpDownCurrentBackPlusUp_ValueChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Juna.RubyLyricsViewer.LyricsView_SetLineBackPlusUp( (int)numericUpDownCurrentBackPlusUp.Value );
			This.DrawLyrics();
		}

		private void numericUpDownCurrentBackPlusDown_ValueChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Juna.RubyLyricsViewer.LyricsView_SetLineBackPlusDown( (int)numericUpDownCurrentBackPlusDown.Value );
			This.DrawLyrics();
		}



		private void SetAlignment()
		{
			This.SetAlignment();
			This.DrawLyrics();
		}

		private void radioButtonHAlignmentLeft_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;
			Properties.Settings.Default.HAlignment = StringAlignment.Near;
			SetAlignment();
		}

		private void radioButtonHAlignmentCenter_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;
			Properties.Settings.Default.HAlignment = StringAlignment.Center;
			SetAlignment();
		}

		private void radioButtonHAlignmentRight_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;
			Properties.Settings.Default.HAlignment = StringAlignment.Far;
			SetAlignment();
		}


		private void listBoxVAlignment_SelectedIndexChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			StringAlignment sa = StringAlignment.Center;
			switch (listBoxVAlignment.SelectedIndex)
			{
			case 0: sa = StringAlignment.Near; break;
			case 2: sa = StringAlignment.Far; break;
			}
			Properties.Settings setting = Properties.Settings.Default;
			setting.VAlignment = sa;
			SetAlignment();
		}
		private void numericUpDownYOffset_ValueChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Properties.Settings setting = Properties.Settings.Default;
			setting.YOffset = (int)numericUpDownYOffset.Value;
			SetAlignment();
		}


		private void comboBoxRubyAlignment_SelectedIndexChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Properties.Settings setting = Properties.Settings.Default;
			setting.RubyAlignment = comboBoxRubyAlignment.SelectedIndex;
			Juna.RubyLyricsViewer.LyricsView_SetRubyAlignment( comboBoxRubyAlignment.SelectedIndex );
			This.DrawLyrics();
		}

		private void checkBoxContinuousScroll_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			bool check = checkBoxContinuousScroll.Checked;
			labelScrollTime.Enabled = !check;
			Properties.Settings.Default.ContinuousScroll = check;
		}


		private void numericUpDownScrollTime_ValueChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Properties.Settings setting = Properties.Settings.Default;
			/*
						setting.ScrollTime = numericUpDownScrollTime.Value;
						setting.FadeTime = numericUpDownFadeMax.Value;
			*/
			Juna.RubyLyricsViewer.LyricsView_SetScrollTime( (int)numericUpDownScrollTime.Value , (int)numericUpDownFadeTime.Value );

			This.DrawLyrics();
		}

		private void numericUpDownSpaces_ValueChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Properties.Settings setting = Properties.Settings.Default;
			/*
						setting.LineSpace = numericUpDownLineSpace.Value;
						setting.CharSpace = numericUpDownCharSpace.Value;
						setting.NoRubyPlusSpace = numericUpDownNoRubyLineSpace.Value;
						setting.RubyBottomSpace = numericUpDownRubyBottomSpace.Value;
			*/
			Juna.RubyLyricsViewer.LyricsView_SetSpace( (int)numericUpDownLineSpace.Value , (int)numericUpDownCharSpace.Value , (int)numericUpDownNoRubyLineSpace.Value , (int)numericUpDownRubyBottomSpace.Value );

			This.DrawLyrics();
		}

		private void numericUpDownMargins_ValueChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Juna.RubyLyricsViewer.LyricsView_SetMargin( (int)numericUpDownLeftMargin.Value , 0 , (int)numericUpDownRightMargin.Value , 0 );
			This.DrawLyrics();
		}



		/*=================================================================================================
		 *
		 *										歌詞 
		 * 
		=================================================================================================*/



		private void textBoxLyricsSearchPath_TextChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			This.SetLyricsSearchPath( textBoxLyricsSearchPath.Text );
			Properties.Settings.Default.LyricsSearchPath = textBoxLyricsSearchPath.Text;
		}

		private void textBoxNoLyricsFormat_TextChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			This.NoLyricsFormat = textBoxNoLyricsFormat.Text;
			Properties.Settings.Default.NoLyricsFormat = This.NoLyricsFormat;

			if ( This.NoLyrics )
			{
				This.SetNoLyricsString();
				This.DrawLyrics();
			}
		}

		private void buttonAliasCopy_Click( object sender , EventArgs e )
		{

			if ( listBoxAlias.SelectedIndex >= 0 )
			{
				string alias = listBoxAlias.SelectedItem.ToString();
				if ( alias != "" )
					Clipboard.SetText( alias );
			}

		}

		private void radioButtonKaraokeMode_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Initializing = true;

			if ( radioButtonKaraokeSelectExt.Checked )
			{
				This.KaraokeMode = Form1.enumKaraokeMode.SelectExt;
			}
			else if ( radioButtonKaraokeIgnore.Checked )
			{
				This.KaraokeMode = Form1.enumKaraokeMode.Ignore;
			}
			else if ( radioButtonKaraokeForce.Checked )
			{
				This.KaraokeMode = Form1.enumKaraokeMode.Force;
			}

			Properties.Settings.Default.KaraokeMode = (int)This.KaraokeMode;

			Initializing = false;
		}


		private void radioButtonKaraokeLast_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Initializing = true;

			if ( radioButtonKaraokeLastNext.Checked )
			{
				Properties.Settings.Default.KaraokeLastNext = true;
			}
			else if ( radioButtonKaraokeLastLast.Checked )
			{
				Properties.Settings.Default.KaraokeLastNext = false;
			}

			Initializing = false;
		}



		/*=================================================================================================
		 *
		 *										画像
		 * 
		=================================================================================================*/


		private void buttonReloadBGI_Click( object sender , EventArgs e )
		{
			This.LoadBGImage();
			This.DrawLyrics();
		}

		private void textBoxBGImageSearchPath_TextChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			This.SetBGImageSearchPath( textBoxBGImageSearchPath.Text );
			Properties.Settings.Default.BGImageSearchPath = textBoxBGImageSearchPath.Text;
		}


		private void LimitFlagChanged()
		{
			Juna.RubyLyricsViewer.LyricsView_SetBGImageLimitFlag(
				checkBoxLimitDisplayX.Checked , checkBoxLimitDisplayY.Checked ,
				checkBoxLimitSourceX.Checked , checkBoxLimitSourceY.Checked ,
				checkBoxLimitMaxX.Checked , checkBoxLimitMaxY.Checked );

			This.DrawLyrics();
		}

		private void checkBoxLimitDisplayX_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Properties.Settings.Default.BGILimitDisplayX = checkBoxLimitDisplayX.Checked;
			LimitFlagChanged();
		}

		private void checkBoxLimitDisplayY_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Properties.Settings.Default.BGILimitDisplayY = checkBoxLimitDisplayY.Checked;
			LimitFlagChanged();
		}

		private void checkBoxLimitSourceX_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Properties.Settings.Default.BGILimitSourceX = checkBoxLimitSourceX.Checked;
			LimitFlagChanged();
		}

		private void checkBoxLimitSourceY_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Properties.Settings.Default.BGILimitSourceY = checkBoxLimitSourceY.Checked;
			LimitFlagChanged();
		}

		private void checkBoxLimitMaxX_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Properties.Settings.Default.BGILimitMaxSizeX = checkBoxLimitMaxX.Checked;
			LimitFlagChanged();
		}

		private void checkBoxLimitMaxY_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Properties.Settings.Default.BGILimitMaxSizeY = checkBoxLimitMaxY.Checked;
			LimitFlagChanged();
		}

		private void numericUpDownBGILimitX_ValueChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			int width = (int)numericUpDownBGILimitX.Value;
			Juna.RubyLyricsViewer.LyricsView_SetBGImageMaxSize( width , (int)numericUpDownBGILimitY.Value );
			Properties.Settings.Default.BGILimitSize = new Size( width , (int)numericUpDownBGILimitY.Value );
			This.DrawLyrics();
		}

		private void numericUpDownBGILimitY_ValueChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			int height = (int)numericUpDownBGILimitY.Value;
			Juna.RubyLyricsViewer.LyricsView_SetBGImageMaxSize( (int)numericUpDownBGILimitX.Value , (int)numericUpDownBGILimitY.Value );
			Properties.Settings.Default.BGILimitSize = new Size( (int)numericUpDownBGILimitX.Value , height );
			This.DrawLyrics();
		}



		private void checkBoxFrameImageShow_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			string path;
			if ( checkBoxFrameImageShow.Checked )
				path = textBoxFrameImagePath.Text.Trim();
			else
				path = ' ' + textBoxFrameImagePath.Text;
			textBoxFrameImagePath.Text = path;
			This.LoadFrameImage( path );
		}


		private void buttonFrameImageOpen_Click( object sender , EventArgs e )
		{
			string open_extension = "*.bmp;*.jpg;*.png";
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "picture files (" + open_extension + ")|" + open_extension + "|All files (*.*)|*.*";
			if ( dialog.ShowDialog() == DialogResult.OK )
			{
				string path = dialog.FileName;
				if ( !checkBoxFrameImageShow.Checked )
					path = ' ' + path;
				textBoxFrameImagePath.Text = path;
				This.LoadFrameImage( path );
			}
		}
		private void groupBoxFrameImage_DragEnter( object sender , DragEventArgs e )
		{
			e.Effect = DragDropEffects.None;
			if ( e.Data.GetDataPresent( DataFormats.FileDrop ) )
			{
				string[] filename = (string[])e.Data.GetData( DataFormats.FileDrop , false );
				if ( !System.IO.File.Exists( filename[0] ) )
					return;
				string ext = System.IO.Path.GetExtension( filename[0] ).ToLower();
				if ( ext != ".png" && ext != ".jpg" && ext != ".bmp" )
					return;

				e.Effect = DragDropEffects.All;
			}
		}

		private void groupBoxFrameImage_DragDrop( object sender , DragEventArgs e )
		{
			string[] filename = (string[])e.Data.GetData( DataFormats.FileDrop , false );
			if ( !System.IO.File.Exists( filename[0] ) )
			{
				return;
			}
			string path = filename[0];
			if ( !checkBoxFrameImageShow.Checked )
				path = ' ' + path;
			textBoxFrameImagePath.Text = path;
			This.LoadFrameImage( path );
		}





		private void checkBoxBGIFilter_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Properties.Settings setting = Properties.Settings.Default;
			if ( checkBoxBGIFilter.Checked )
			{
				setting.BGIFilterAlpha = (int)numericUpDownBGIFilterAlpha.Value;
				Juna.RubyLyricsViewer.SetBGImageFilter( (byte)numericUpDownBGIFilterAlpha.Value , labelBGIFilterColor.BackColor , setting.BGImageAlpha );
			}
			else
			{
				setting.BGIFilterAlpha = -(int)numericUpDownBGIFilterAlpha.Value;
				Juna.RubyLyricsViewer.SetBGImageFilter( 0 , labelBGIFilterColor.BackColor , setting.BGImageAlpha );
			}
			This.DrawLyrics();

		}

		private void numericUpDownBGIFilterAlpha_ValueChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Properties.Settings setting = Properties.Settings.Default;
			if ( checkBoxBGIFilter.Checked )
			{
				setting.BGIFilterAlpha = (int)numericUpDownBGIFilterAlpha.Value;
				Juna.RubyLyricsViewer.SetBGImageFilter( (byte)numericUpDownBGIFilterAlpha.Value , labelBGIFilterColor.BackColor , setting.BGImageAlpha );
			}
			else
			{
				setting.BGIFilterAlpha = -(int)numericUpDownBGIFilterAlpha.Value;
				Juna.RubyLyricsViewer.SetBGImageFilter( 0 , labelBGIFilterColor.BackColor , setting.BGImageAlpha );
			}
			This.DrawLyrics();
		}

		private void labelBGIFilterColor_Click( object sender , EventArgs e )
		{
			Label this_ = (Label)sender;
			if ( LabelColorChagneDialog( this_ ) )
			{
				Properties.Settings setting = Properties.Settings.Default;
				setting.BGIFilterColor = this_.BackColor;
				if ( checkBoxBGIFilter.Checked )
					Juna.RubyLyricsViewer.SetBGImageFilter( (byte)numericUpDownBGIFilterAlpha.Value , this_.BackColor , setting.BGImageAlpha );
				else
					Juna.RubyLyricsViewer.SetBGImageFilter( 0 , this_.BackColor , setting.BGImageAlpha );
				This.DrawLyrics();
			}
		}

		private void BGIPositionChanged()
		{
			int p;
			if ( checkBoxBGILeft.Checked )
				p = 1;
			else if ( checkBoxBGIRight.Checked )
				p = 3;
			else
				p = 2;
			if ( checkBoxBGITop.Checked )
				p += 6;
			else if ( checkBoxBGIBottom.Checked )
				p += 0;
			else
				p += 3;

			Properties.Settings setting = Properties.Settings.Default;
			setting.BGIPosition = p;

			Juna.RubyLyricsViewer.LyricsView_SetBGImagePosition( p );
			This.DrawLyrics();
		}


		private void checkBoxBGILeft_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Initializing = true;
			if ( checkBoxBGILeft.Checked )
				checkBoxBGIRight.Checked = false;
			Initializing = false;
			BGIPositionChanged();
		}

		private void checkBoxBGIRight_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Initializing = true;
			if ( checkBoxBGIRight.Checked )
				checkBoxBGILeft.Checked = false;
			Initializing = false;
			BGIPositionChanged();
		}

		private void checkBoxBGITop_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Initializing = true;
			if ( checkBoxBGITop.Checked )
				checkBoxBGIBottom.Checked = false;
			Initializing = false;
			BGIPositionChanged();
		}

		private void checkBoxBGIBottom_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Initializing = true;
			if ( checkBoxBGIBottom.Checked )
				checkBoxBGITop.Checked = false;
			Initializing = false;
			BGIPositionChanged();
		}




		/*=================================================================================================
		 *
		 *										その他 
		 * 
		=================================================================================================*/

		private void textBoxEditorPath_TextChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			This.text_editor_path = textBoxEditorPath.Text;
			Properties.Settings.Default.EditorPath = textBoxEditorPath.Text;
		}
		private void textBoxEditorParam_TextChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			This.text_editor_param = textBoxEditorParam.Text;
			Properties.Settings.Default.EditorParam = textBoxEditorParam.Text;
		}

		private void buttonSelectEditor_Click( object sender , EventArgs e )
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.FileName = "";
			ofd.InitialDirectory = "";
			ofd.Filter = "実行可能ファイル(*.exe)|*.exe|すべてのファイル(*.*)|*.*";
			ofd.RestoreDirectory = true;

			if ( ofd.ShowDialog( this ) == System.Windows.Forms.DialogResult.OK )
			{
				textBoxEditorPath.Text = ofd.FileName;
			}
		}


		private void buttonOpenConfigPath_Click( object sender , EventArgs e )
		{
			Properties.Settings.Default.Save();
			System.Diagnostics.Process.Start( System.IO.Path.GetDirectoryName( textBoxConfigSavePath.Text ) );
		}
		private void buttonResetSetting_Click( object sender , EventArgs e )
		{
			Properties.Settings.Default.Reset();
			this.Close();
		}



		/*=================================================================================================
		 *
		 *										レイアウト 
		 * 
		=================================================================================================*/

		private int GetLayoutPosition( CheckBox cleft , CheckBox ctop , CheckBox cright , CheckBox cbottom )
		{
			int p;
			if ( cleft.Checked )
				p = 1;
			else if ( cright.Checked )
				p = 3;
			else
				p = 2;
			if ( ctop.Checked )
				p += 6;
			else if ( cbottom.Checked )
				p += 0;
			else
				p += 3;
			return p;
		}


		private void checkBoxLayoutLyricsLeft_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Initializing = true;
			if ( checkBoxLayoutLyricsLeft.Checked )
			{
				if ( checkBoxLayoutLyricsRight.Checked )
				{
					numericUpDownLayoutLyricsLeft.Value = This.ClientSize.Width - numericUpDownLayoutLyricsLeft.Value;
					checkBoxLayoutLyricsRight.Checked = false;
				}
			}
			numericUpDownLayoutLyricsRight.Value = This.ClientSize.Width - numericUpDownLayoutLyricsRight.Value;

			Initializing = false;
			LayoutLyricsChanged();
		}

		private void checkBoxLayoutLyricsRight_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Initializing = true;
			if ( checkBoxLayoutLyricsRight.Checked )
			{
				if ( checkBoxLayoutLyricsLeft.Checked )
				{
					checkBoxLayoutLyricsLeft.Checked = false;
					numericUpDownLayoutLyricsRight.Value = This.ClientSize.Width - numericUpDownLayoutLyricsRight.Value;
				}
			}
			numericUpDownLayoutLyricsLeft.Value = This.ClientSize.Width - numericUpDownLayoutLyricsLeft.Value;
			Initializing = false;
			LayoutLyricsChanged();

		}

		private void checkBoxLayoutLyricsTop_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Initializing = true;
			if ( checkBoxLayoutLyricsTop.Checked )
			{
				if ( checkBoxLayoutLyricsBottom.Checked )
				{
					numericUpDownLayoutLyricsTop.Value = This.ClientSize.Height - numericUpDownLayoutLyricsTop.Value;
					checkBoxLayoutLyricsBottom.Checked = false;
				}
			}
			numericUpDownLayoutLyricsBottom.Value = This.ClientSize.Height - numericUpDownLayoutLyricsBottom.Value;
			Initializing = false;
			LayoutLyricsChanged();
		}

		private void checkBoxLayoutLyricsBottom_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Initializing = true;
			if ( checkBoxLayoutLyricsBottom.Checked )
			{
				if ( checkBoxLayoutLyricsTop.Checked )
				{
					numericUpDownLayoutLyricsBottom.Value = This.ClientSize.Height - numericUpDownLayoutLyricsBottom.Value;
					checkBoxLayoutLyricsTop.Checked = false;
				}
			}
			numericUpDownLayoutLyricsTop.Value = This.ClientSize.Height - numericUpDownLayoutLyricsTop.Value;
			Initializing = false;
			LayoutLyricsChanged();
		}

		private void LayoutLyricsChanged()
		{
			if ( Initializing )
				return;

			int left = (int)numericUpDownLayoutLyricsLeft.Value;
			int top = (int)numericUpDownLayoutLyricsTop.Value;
			int right = (int)numericUpDownLayoutLyricsRight.Value;
			int bottom = (int)numericUpDownLayoutLyricsBottom.Value;

			int p = GetLayoutPosition( checkBoxLayoutLyricsLeft , checkBoxLayoutLyricsTop , checkBoxLayoutLyricsRight , checkBoxLayoutLyricsBottom );

			Properties.Settings setting = Properties.Settings.Default;
			setting.LyricsLayoutPosition = p;
			setting.LyricsLayoutLeft = left;
			setting.LyricsLayoutRight = right;
			setting.LyricsLayoutTop = top;
			setting.LyricsLayoutBottom = bottom;

			This.SetLyricsLayout( p , left , top , right , bottom );
			This.DrawLyrics();
		}


		private void numericUpDownLayoutLyricsLeft_ValueChanged( object sender , EventArgs e )
		{
			int v = (int)numericUpDownLayoutLyricsLeft.Value;
			v = Math.Min( v , This.ClientSize.Width );
			numericUpDownLayoutLyricsLeft.Value = v;

			LayoutLyricsChanged();
		}

		private void numericUpDownLayoutLyricsRight_ValueChanged( object sender , EventArgs e )
		{
			int v = (int)numericUpDownLayoutLyricsRight.Value;
			v = Math.Min( v , This.ClientSize.Width );
			numericUpDownLayoutLyricsRight.Value = v;

			LayoutLyricsChanged();
		}

		private void numericUpDownLayoutLyricsTop_ValueChanged( object sender , EventArgs e )
		{
			int v = (int)numericUpDownLayoutLyricsTop.Value;
			v = Math.Min( v , This.ClientSize.Height );
			numericUpDownLayoutLyricsTop.Value = v;

			LayoutLyricsChanged();
		}

		private void numericUpDownLayoutLyricsBottom_ValueChanged( object sender , EventArgs e )
		{
			int v = (int)numericUpDownLayoutLyricsBottom.Value;
			v = Math.Min( v , This.ClientSize.Height );
			numericUpDownLayoutLyricsBottom.Value = v;

			LayoutLyricsChanged();
		}


		private void checkBoxLayoutBGILeft_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Initializing = true;
			if ( checkBoxLayoutBGILeft.Checked )
			{
				if ( checkBoxLayoutBGIRight.Checked )
				{
					checkBoxLayoutBGIRight.Checked = false;
					numericUpDownLayoutBGILeft.Value = This.ClientSize.Width - numericUpDownLayoutBGILeft.Value;
				}
			}
			numericUpDownLayoutBGIRight.Value = This.ClientSize.Width - numericUpDownLayoutBGIRight.Value;
			Initializing = false;
			LayoutBGIChanged();

		}

		private void checkBoxLayoutBGIRight_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Initializing = true;
			if ( checkBoxLayoutBGIRight.Checked )
			{
				if ( checkBoxLayoutBGILeft.Checked )
				{
					checkBoxLayoutBGILeft.Checked = false;
					numericUpDownLayoutBGIRight.Value = This.ClientSize.Width - numericUpDownLayoutBGIRight.Value;
				}
			}
			numericUpDownLayoutBGILeft.Value = This.ClientSize.Width - numericUpDownLayoutBGILeft.Value;
			Initializing = false;
			LayoutBGIChanged();
		}

		private void checkBoxLayoutBGITop_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Initializing = true;
			if ( checkBoxLayoutBGITop.Checked )
			{
				if ( checkBoxLayoutBGIBottom.Checked )
				{
					checkBoxLayoutBGIBottom.Checked = false;
					numericUpDownLayoutBGITop.Value = This.ClientSize.Height - numericUpDownLayoutBGITop.Value;
				}
			}
			numericUpDownLayoutBGIBottom.Value = This.ClientSize.Height - numericUpDownLayoutBGIBottom.Value;
			Initializing = false;
			LayoutBGIChanged();
		}

		private void checkBoxLayoutBGIBottom_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Initializing = true;
			if ( checkBoxLayoutBGIBottom.Checked )
			{
				if ( checkBoxLayoutBGITop.Checked )
				{
					checkBoxLayoutBGITop.Checked = false;
					numericUpDownLayoutBGIBottom.Value = This.ClientSize.Height - numericUpDownLayoutBGIBottom.Value;
				}
			}
			numericUpDownLayoutBGITop.Value = This.ClientSize.Height - numericUpDownLayoutBGITop.Value;
			Initializing = false;
			LayoutBGIChanged();
		}

		private void LayoutBGIChanged()
		{
			if ( Initializing )
				return;

			int left = (int)numericUpDownLayoutBGILeft.Value;
			int top = (int)numericUpDownLayoutBGITop.Value;
			int right = (int)numericUpDownLayoutBGIRight.Value;
			int bottom = (int)numericUpDownLayoutBGIBottom.Value;

			int p = GetLayoutPosition( checkBoxLayoutBGILeft , checkBoxLayoutBGITop , checkBoxLayoutBGIRight , checkBoxLayoutBGIBottom );

			Properties.Settings setting = Properties.Settings.Default;
			setting.BGILayoutPosition = p;
			setting.BGILayoutLeft = left;
			setting.BGILayoutRight = right;
			setting.BGILayoutTop = top;
			setting.BGILayoutBottom = bottom;

			This.SetBGILayout( p , left , top , right , bottom );
			This.DrawLyrics();
		}

		private void numericUpDownLayoutBGILeft_ValueChanged( object sender , EventArgs e )
		{
			int v = (int)numericUpDownLayoutBGILeft.Value;
			v = Math.Min( v , This.ClientSize.Width );
			numericUpDownLayoutBGILeft.Value = v;

			LayoutBGIChanged();
		}

		private void numericUpDownLayoutBGIRight_ValueChanged( object sender , EventArgs e )
		{
			int v = (int)numericUpDownLayoutBGIRight.Value;
			v = Math.Min( v , This.ClientSize.Width );
			numericUpDownLayoutBGIRight.Value = v;

			LayoutBGIChanged();
		}

		private void numericUpDownLayoutBGITop_ValueChanged( object sender , EventArgs e )
		{
			int v = (int)numericUpDownLayoutBGITop.Value;
			v = Math.Min( v , This.ClientSize.Height );
			numericUpDownLayoutBGITop.Value = v;

			LayoutBGIChanged();
		}

		private void numericUpDownLayoutBGIBottom_ValueChanged( object sender , EventArgs e )
		{
			int v = (int)numericUpDownLayoutBGIBottom.Value;
			v = Math.Min( v , This.ClientSize.Height );
			numericUpDownLayoutBGIBottom.Value = v;

			LayoutBGIChanged();
		}


		private void LayoutTextChanged()
		{
			int left = (int)numericUpDownLayoutTextLeft.Value;
			int top = (int)numericUpDownLayoutTextTop.Value;
			int right = (int)numericUpDownLayoutTextRight.Value;
			int bottom = (int)numericUpDownLayoutTextBottom.Value;

			int p = GetLayoutPosition( checkBoxLayoutTextLeft , checkBoxLayoutTextTop , checkBoxLayoutTextRight , checkBoxLayoutTextBottom );

			Properties.Settings setting = Properties.Settings.Default;
			setting.InfoTextLayoutPosition = p;
			setting.InfoTextLayoutLeft = left;
			setting.InfoTextLayoutRight = right;
			setting.InfoTextLayoutTop = top;
			setting.InfoTextLayoutBottom = bottom;

			This.SetTextLayout(p , left , top , right , bottom );
			This.DrawLyrics();
		}


		private void textBoxLayoutText_TextChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Properties.Settings.Default.InfoText = textBoxLayoutText.Text;
			This.SetDisplayText( textBoxLayoutText.Text );
			This.DrawLyrics();
		}


		private void labelLayoutTextColor_Click( object sender , EventArgs e )
		{
			if ( LabelColorChagneDialog( labelLayoutTextColor ) )
			{
				Properties.Settings.Default.InfoTextColor = labelLayoutTextColor.BackColor;
				Juna.RubyLyricsViewer.SetInfoTextColor( labelLayoutTextColor.BackColor , labelLayoutTextOutlineColor.BackColor );
				This.DrawLyrics();
			}
		}

		private void checkBoxLayoutTextOutline_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;
			Properties.Settings setting = Properties.Settings.Default;
			setting.InfoTextOutline = checkBoxLayoutTextOutline.Checked;
			Juna.RubyLyricsViewer.SetInfoTextFont( setting.InfoTextFont , checkBoxLayoutTextAntiAlias.Checked , checkBoxLayoutTextOutline.Checked , (int)numericUpDownLayoutTextOutlineSlip.Value );
			This.DrawLyrics();
		}

		private void labelLayoutTextOutlineColor_Click( object sender , EventArgs e )
		{
			if ( LabelColorChagneDialog( labelLayoutTextOutlineColor ) )
			{
				Properties.Settings.Default.InfoTextOutlineColor = labelLayoutTextOutlineColor.BackColor;
				Juna.RubyLyricsViewer.SetInfoTextColor( labelLayoutTextColor.BackColor , labelLayoutTextOutlineColor.BackColor );
				This.DrawLyrics();
			}
		}


		private void listBoxLayoutTextHAlignment_SelectedIndexChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;
			Properties.Settings setting = Properties.Settings.Default;
			StringAlignment sa = StringAlignment.Near;
			switch ( listBoxLayoutTextHAlignment.SelectedIndex )
			{
			case 1: sa = StringAlignment.Center; break;
			case 2: sa = StringAlignment.Far; break;
			}
			setting.InfoTextHAlignment = sa;
			Juna.RubyLyricsViewer.SetInfoTextAlignment( setting.InfoTextVAlignment , sa );
			This.DrawLyrics();
		}

		private void listBoxLayoutTextVAlignment_SelectedIndexChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;
			Properties.Settings setting = Properties.Settings.Default;
			StringAlignment sa = StringAlignment.Near;
			switch ( listBoxLayoutTextVAlignment.SelectedIndex )
			{
			case 1: sa = StringAlignment.Center; break;
			case 2: sa = StringAlignment.Far; break;
			}
			setting.InfoTextVAlignment = sa;
			Juna.RubyLyricsViewer.SetInfoTextAlignment( sa , setting.InfoTextHAlignment );
			This.DrawLyrics();
		}



		private void checkBoxLayoutTextLeft_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Initializing = true;

			if ( checkBoxLayoutTextLeft.Checked )
			{
				if ( checkBoxLayoutTextRight.Checked )
				{
					checkBoxLayoutTextRight.Checked = false;
					numericUpDownLayoutTextLeft.Value = This.ClientSize.Width - numericUpDownLayoutTextLeft.Value;
				}
			}
			numericUpDownLayoutTextRight.Value = This.ClientSize.Width - numericUpDownLayoutTextRight.Value;
			Initializing = false;
			LayoutTextChanged();
		}

		private void checkBoxLayoutTextRight_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Initializing = true;
			if ( checkBoxLayoutTextRight.Checked )
			{
				if ( checkBoxLayoutTextLeft.Checked )
				{
					checkBoxLayoutTextLeft.Checked = false;
					numericUpDownLayoutTextRight.Value = This.ClientSize.Width - numericUpDownLayoutTextRight.Value;
				}
			}
			numericUpDownLayoutTextLeft.Value = This.ClientSize.Width - numericUpDownLayoutTextLeft.Value;
			Initializing = false;
			LayoutTextChanged();
		}

		private void checkBoxLayoutTextTop_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Initializing = true;
			if ( checkBoxLayoutTextTop.Checked )
			{
				if ( checkBoxLayoutTextBottom.Checked )
				{
					checkBoxLayoutTextBottom.Checked = false;
					numericUpDownLayoutTextTop.Value = This.ClientSize.Height - numericUpDownLayoutTextTop.Value;
				}
			}
			numericUpDownLayoutTextBottom.Value = This.ClientSize.Height - numericUpDownLayoutTextBottom.Value;
			Initializing = false;
			LayoutTextChanged();
		}

		private void checkBoxLayoutTextBottom_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Initializing = true;
			if ( checkBoxLayoutTextBottom.Checked )
			{
				if ( checkBoxLayoutTextTop.Checked )
				{
					checkBoxLayoutTextTop.Checked = false;
					numericUpDownLayoutTextBottom.Value = This.ClientSize.Height - numericUpDownLayoutTextBottom.Value;
				}
			}
			numericUpDownLayoutTextTop.Value = This.ClientSize.Height - numericUpDownLayoutTextTop.Value;
			Initializing = false;
			LayoutTextChanged();
		}

		private void numericUpDownLayoutTextLeft_ValueChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;
			int v = (int)numericUpDownLayoutTextLeft.Value;
			v = Math.Min( v , This.ClientSize.Width );
			numericUpDownLayoutTextLeft.Value = v;

			LayoutTextChanged();
		}

		private void numericUpDownLayoutTextRight_ValueChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;
			int v = (int)numericUpDownLayoutTextRight.Value;
			v = Math.Min( v , This.ClientSize.Width );
			numericUpDownLayoutTextRight.Value = v;

			LayoutTextChanged();
		}

		private void numericUpDownLayoutTextTop_ValueChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;
			int v = (int)numericUpDownLayoutTextTop.Value;
			v = Math.Min( v , This.ClientSize.Height );
			numericUpDownLayoutTextTop.Value = v;

			LayoutTextChanged();
		}

		private void numericUpDownLayoutTextBottom_ValueChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;
			int v = (int)numericUpDownLayoutTextBottom.Value;
			v = Math.Min( v , This.ClientSize.Height );
			numericUpDownLayoutTextBottom.Value = v;

			LayoutTextChanged();

		}

		private void buttonLayoutTextFont_Click( object sender , EventArgs e )
		{
			Properties.Settings setting = Properties.Settings.Default;
			FontDialog dialog = new FontDialog();
			dialog.Font = Properties.Settings.Default.InfoTextFont;
			if ( dialog.ShowDialog() == DialogResult.OK )
			{
				setting.InfoTextFont = dialog.Font;
				Juna.RubyLyricsViewer.SetInfoTextFont( dialog.Font , checkBoxLayoutTextAntiAlias.Checked , checkBoxLayoutTextOutline.Checked , (int)numericUpDownLayoutTextOutlineSlip.Value );
				Font f = dialog.Font;
				textBoxLayoutTextFontText.Text = f.Name + " " + f.Size + (f.Bold ? " Bold" : "") + (f.Italic ? " Italic" : "");
				This.DrawLyrics();
			}
		}

		private void numericUpDownLayoutTextOutlineSlip_ValueChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;
			Properties.Settings setting = Properties.Settings.Default;
			setting.InfoTextOutlineSlip = (int)numericUpDownLayoutTextOutlineSlip.Value;
			Juna.RubyLyricsViewer.SetInfoTextFont( setting.InfoTextFont , checkBoxLayoutTextAntiAlias.Checked , checkBoxLayoutTextOutline.Checked , (int)numericUpDownLayoutTextOutlineSlip.Value );
			This.DrawLyrics();
		}

		private void checkBoxLayoutTextAntiAlias_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;
			Properties.Settings setting = Properties.Settings.Default;
			setting.InfoTextAntiAlias = checkBoxLayoutTextAntiAlias.Checked;
			Juna.RubyLyricsViewer.SetInfoTextFont( setting.InfoTextFont , checkBoxLayoutTextAntiAlias.Checked , checkBoxLayoutTextOutline.Checked , (int)numericUpDownLayoutTextOutlineSlip.Value );
			This.DrawLyrics();
		}


		/*=================================================================================================
		 *
		 *										拡張 
		 * 
		=================================================================================================*/



		private void labelStandbyColor_Click( object sender , EventArgs e )
		{
			Label this_ = (Label)sender;
			if ( LabelColorChagneDialog( this_ ) )
			{
				Properties.Settings setting = Properties.Settings.Default;

				setting.StandbyColor = this_.BackColor;
				Juna.RubyLyricsViewer.SetStandbyColor( labelStandbyColor.BackColor , labelStandbyOutlineColor.BackColor );

				This.DrawLyrics();
			}

		}

		private void labelStandbyOutlineColor_Click( object sender , EventArgs e )
		{
			Label this_ = (Label)sender;
			if ( LabelColorChagneDialog( this_ ) )
			{
				Properties.Settings setting = Properties.Settings.Default;

				setting.StandbyOutlineColor = this_.BackColor;
				Juna.RubyLyricsViewer.SetStandbyColor( labelStandbyColor.BackColor , labelStandbyOutlineColor.BackColor );

				This.DrawLyrics();
			}

		}


		private void labelNTTTColor_Click( object sender , EventArgs e )
		{
			Label this_ = (Label)sender;
			if ( LabelColorChagneDialog( this_ ) )
			{
				Properties.Settings setting = Properties.Settings.Default;

				setting.NTTTColor = this_.BackColor;
				Juna.RubyLyricsViewer.SetNTTTextColor( labelNTTTColor.BackColor , labelNTTTOutlineColor.BackColor );

				This.DrawLyrics();
			}

		}

		private void labelNTTTOutlineColor_Click( object sender , EventArgs e )
		{
			Label this_ = (Label)sender;
			if ( LabelColorChagneDialog( this_ ) )
			{
				Properties.Settings setting = Properties.Settings.Default;

				setting.NTTTOutlineColor = this_.BackColor;
				Juna.RubyLyricsViewer.SetNTTTextColor( labelNTTTColor.BackColor , labelNTTTOutlineColor.BackColor );

				This.DrawLyrics();
			}
		}

		private void listBoxNTTTVAlignment_SelectedIndexChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			StringAlignment sa = ( listBoxNTTTVAlignment.SelectedIndex == 0 ) ? StringAlignment.Near : StringAlignment.Center;

			Properties.Settings setting = Properties.Settings.Default;
			setting.NTTTVAlignment = sa;
			SetAlignment();
		}
		private void numericUpDownNTTTYOffset_ValueChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			Properties.Settings setting = Properties.Settings.Default;
			setting.NTTTYOffset = (int)numericUpDownNTTTYOffset.Value;
			SetAlignment();
		}


		private void checkBoxKaraokeDisplay_CheckedChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;
			Properties.Settings.Default.KaraokeDisplay = checkBoxKaraokeDisplay.Checked;
			This.DrawLyrics();
		}

		private void numericUpDownKaraokeBottomMargin_ValueChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;
			Properties.Settings.Default.KaraokeDisplayBottomMargin = (int)numericUpDownKaraokeBottomMargin.Value;
			This.DrawLyrics();
		}

		private void buttonReloadLyrics_Click( object sender , EventArgs e )
		{
			This.LoadLyrics();
			This.DrawLyrics();
		}

		private void numericUpDownBGColorAlpha_ValueChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			byte a = (byte)numericUpDownBGColorAlpha.Value;
			Juna.RubyLyricsViewer.SetBGColor( a , labelBGColor.BackColor );
			Properties.Settings.Default.BGColorAlpha = a;
			This.DrawLyrics();
		}

		private void numericUpDownWindowAlpha_ValueChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			byte a = (byte)numericUpDownWindowAlpha.Value;
			Properties.Settings.Default.LayeredWindowAlpha = a;
			This.DrawLyrics();
		}

		private void numericUpDownBGImageAlpha_ValueChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			byte a = (byte)numericUpDownBGImageAlpha.Value;
			Properties.Settings.Default.BGImageAlpha = a;
			if ( checkBoxBGIFilter.Checked )
			{
				Juna.RubyLyricsViewer.SetBGImageFilter( (byte)numericUpDownBGIFilterAlpha.Value , labelBGIFilterColor.BackColor , a );
			}
			else
			{
				Juna.RubyLyricsViewer.SetBGImageFilter( 0 , labelBGIFilterColor.BackColor , a );
			} 
			This.DrawLyrics();
		}

		private void numericUpDownInterval_ValueChanged( object sender , EventArgs e )
		{
			if ( Initializing )
				return;

			int i = (int)numericUpDownTimerInterval.Value;
			This.timer1.Interval = i;
			Properties.Settings.Default.TimerInterval = i;
		}






	}
}
