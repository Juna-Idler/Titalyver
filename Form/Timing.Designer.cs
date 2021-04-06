namespace Titalyver
{
	partial class FormTiming
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
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.numericUpDownMinimum = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownMaximum = new System.Windows.Forms.NumericUpDown();
			this.textBoxTiming = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinimum)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaximum)).BeginInit();
			this.SuspendLayout();
			// 
			// trackBar1
			// 
			this.trackBar1.AutoSize = false;
			this.trackBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.trackBar1.Location = new System.Drawing.Point(0, 0);
			this.trackBar1.Maximum = 100;
			this.trackBar1.Minimum = -100;
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(392, 32);
			this.trackBar1.TabIndex = 0;
			this.trackBar1.TickFrequency = 10;
			this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(67, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(23, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "早く";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(302, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(23, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "遅く";
			// 
			// numericUpDownMinimum
			// 
			this.numericUpDownMinimum.DecimalPlaces = 2;
			this.numericUpDownMinimum.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.numericUpDownMinimum.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
			this.numericUpDownMinimum.Location = new System.Drawing.Point(1, 35);
			this.numericUpDownMinimum.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.numericUpDownMinimum.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
			this.numericUpDownMinimum.Name = "numericUpDownMinimum";
			this.numericUpDownMinimum.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.numericUpDownMinimum.Size = new System.Drawing.Size(60, 22);
			this.numericUpDownMinimum.TabIndex = 3;
			this.numericUpDownMinimum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numericUpDownMinimum.Value = new decimal(new int[] {
            5,
            0,
            0,
            -2147418112});
			this.numericUpDownMinimum.ValueChanged += new System.EventHandler(this.numericUpDownMinimum_ValueChanged);
			// 
			// numericUpDownMaximum
			// 
			this.numericUpDownMaximum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDownMaximum.DecimalPlaces = 2;
			this.numericUpDownMaximum.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.numericUpDownMaximum.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
			this.numericUpDownMaximum.Location = new System.Drawing.Point(331, 35);
			this.numericUpDownMaximum.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numericUpDownMaximum.Name = "numericUpDownMaximum";
			this.numericUpDownMaximum.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.numericUpDownMaximum.Size = new System.Drawing.Size(60, 22);
			this.numericUpDownMaximum.TabIndex = 3;
			this.numericUpDownMaximum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numericUpDownMaximum.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
			this.numericUpDownMaximum.ValueChanged += new System.EventHandler(this.numericUpDownMaximum_ValueChanged);
			// 
			// textBoxTiming
			// 
			this.textBoxTiming.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.textBoxTiming.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.textBoxTiming.Location = new System.Drawing.Point(167, 38);
			this.textBoxTiming.Name = "textBoxTiming";
			this.textBoxTiming.ReadOnly = true;
			this.textBoxTiming.Size = new System.Drawing.Size(60, 23);
			this.textBoxTiming.TabIndex = 4;
			this.textBoxTiming.Text = "0";
			this.textBoxTiming.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// FormTiming
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(392, 64);
			this.Controls.Add(this.textBoxTiming);
			this.Controls.Add(this.numericUpDownMaximum);
			this.Controls.Add(this.numericUpDownMinimum);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.trackBar1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(800, 90);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(200, 90);
			this.Name = "FormTiming";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "タイミング調整";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormTiming_FormClosed);
			this.Load += new System.EventHandler(this.FormTiming_Load);
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinimum)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaximum)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TrackBar trackBar1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericUpDownMinimum;
		private System.Windows.Forms.NumericUpDown numericUpDownMaximum;
		private System.Windows.Forms.TextBox textBoxTiming;
	}
}