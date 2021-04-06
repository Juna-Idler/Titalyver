namespace Titalyver
{
	partial class Form1
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose( bool disposing )
		{
			if ( disposing && (components != null) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemOpenSetting = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemTiming = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemShowTaskBar = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemTaskTray = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemTopMost = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemFullScreen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemOpenLyrics = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemMinimize = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemOpenSetting,
            this.toolStripMenuItemTiming,
            this.toolStripSeparator2,
            this.toolStripMenuItemShowTaskBar,
            this.toolStripMenuItemTaskTray,
            this.toolStripMenuItemTopMost,
            this.toolStripMenuItemFullScreen,
            this.toolStripSeparator1,
            this.toolStripMenuItemOpenLyrics,
            this.toolStripMenuItemMinimize,
            this.toolStripSeparator3,
            this.toolStripMenuItemExit});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(156, 220);
			this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
			// 
			// toolStripMenuItemOpenSetting
			// 
			this.toolStripMenuItemOpenSetting.Name = "toolStripMenuItemOpenSetting";
			this.toolStripMenuItemOpenSetting.Size = new System.Drawing.Size(155, 22);
			this.toolStripMenuItemOpenSetting.Text = "設定";
			this.toolStripMenuItemOpenSetting.Click += new System.EventHandler(this.toolStripMenuItemOpenSetting_Click);
			// 
			// toolStripMenuItemTiming
			// 
			this.toolStripMenuItemTiming.Name = "toolStripMenuItemTiming";
			this.toolStripMenuItemTiming.Size = new System.Drawing.Size(155, 22);
			this.toolStripMenuItemTiming.Text = "タイミング調整";
			this.toolStripMenuItemTiming.Click += new System.EventHandler(this.toolStripMenuItemTiming_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(152, 6);
			// 
			// toolStripMenuItemShowTaskBar
			// 
			this.toolStripMenuItemShowTaskBar.Checked = true;
			this.toolStripMenuItemShowTaskBar.CheckOnClick = true;
			this.toolStripMenuItemShowTaskBar.CheckState = System.Windows.Forms.CheckState.Checked;
			this.toolStripMenuItemShowTaskBar.Name = "toolStripMenuItemShowTaskBar";
			this.toolStripMenuItemShowTaskBar.Size = new System.Drawing.Size(155, 22);
			this.toolStripMenuItemShowTaskBar.Text = "タスクバー表示";
			this.toolStripMenuItemShowTaskBar.Click += new System.EventHandler(this.toolStripMenuItemShowTaskBar_Click);
			// 
			// toolStripMenuItemTaskTray
			// 
			this.toolStripMenuItemTaskTray.CheckOnClick = true;
			this.toolStripMenuItemTaskTray.Name = "toolStripMenuItemTaskTray";
			this.toolStripMenuItemTaskTray.Size = new System.Drawing.Size(155, 22);
			this.toolStripMenuItemTaskTray.Text = "タスクトレイ";
			this.toolStripMenuItemTaskTray.Click += new System.EventHandler(this.toolStripMenuItemTaskTray_Click);
			// 
			// toolStripMenuItemTopMost
			// 
			this.toolStripMenuItemTopMost.CheckOnClick = true;
			this.toolStripMenuItemTopMost.Name = "toolStripMenuItemTopMost";
			this.toolStripMenuItemTopMost.Size = new System.Drawing.Size(155, 22);
			this.toolStripMenuItemTopMost.Text = "最前面";
			this.toolStripMenuItemTopMost.Click += new System.EventHandler(this.toolStripMenuItemTopMost_Click);
			// 
			// toolStripMenuItemFullScreen
			// 
			this.toolStripMenuItemFullScreen.CheckOnClick = true;
			this.toolStripMenuItemFullScreen.Name = "toolStripMenuItemFullScreen";
			this.toolStripMenuItemFullScreen.Size = new System.Drawing.Size(155, 22);
			this.toolStripMenuItemFullScreen.Text = "最大化";
			this.toolStripMenuItemFullScreen.Click += new System.EventHandler(this.toolStripMenuItemFullScreen_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(152, 6);
			// 
			// toolStripMenuItemOpenLyrics
			// 
			this.toolStripMenuItemOpenLyrics.Name = "toolStripMenuItemOpenLyrics";
			this.toolStripMenuItemOpenLyrics.Size = new System.Drawing.Size(155, 22);
			this.toolStripMenuItemOpenLyrics.Text = "歌詞ファイルを開く";
			this.toolStripMenuItemOpenLyrics.Click += new System.EventHandler(this.toolStripMenuItemOpenLyrics_Click);
			// 
			// toolStripMenuItemMinimize
			// 
			this.toolStripMenuItemMinimize.Name = "toolStripMenuItemMinimize";
			this.toolStripMenuItemMinimize.Size = new System.Drawing.Size(155, 22);
			this.toolStripMenuItemMinimize.Text = "最小化";
			this.toolStripMenuItemMinimize.Click += new System.EventHandler(this.toolStripMenuItemMinimize_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(152, 6);
			// 
			// toolStripMenuItemExit
			// 
			this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
			this.toolStripMenuItemExit.Size = new System.Drawing.Size(155, 22);
			this.toolStripMenuItemExit.Text = "終了";
			this.toolStripMenuItemExit.Click += new System.EventHandler(this.toolStripMenuItemExit_Click);
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "Titalyver";
			this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
			this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
			// 
			// timer1
			// 
			this.timer1.Interval = 16;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(194, 76);
			this.ContextMenuStrip = this.contextMenuStrip1;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(200, 100);
			this.Name = "Form1";
			this.Text = "Titalyver";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
			this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpenSetting;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExit;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemShowTaskBar;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTopMost;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMinimize;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpenLyrics;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTaskTray;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFullScreen;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTiming;
		public System.Windows.Forms.Timer timer1;
	}
}

