namespace Sreen2Pdf
{
	partial class MainForm
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.buttonSelectRange = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxRange = new System.Windows.Forms.TextBox();
            this.textBoxClickPos = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonSelectRange
            // 
            this.buttonSelectRange.Location = new System.Drawing.Point(75, 12);
            this.buttonSelectRange.Name = "buttonSelectRange";
            this.buttonSelectRange.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectRange.TabIndex = 0;
            this.buttonSelectRange.Text = "选择区域";
            this.buttonSelectRange.UseVisualStyleBackColor = true;
            this.buttonSelectRange.Click += new System.EventHandler(this.buttonSelectRange_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "屏幕区域";
            // 
            // textBoxRange
            // 
            this.textBoxRange.Location = new System.Drawing.Point(109, 62);
            this.textBoxRange.Name = "textBoxRange";
            this.textBoxRange.Size = new System.Drawing.Size(119, 21);
            this.textBoxRange.TabIndex = 2;
            this.textBoxRange.Text = "630,98,1290,1025";
            // 
            // textBoxClickPos
            // 
            this.textBoxClickPos.Location = new System.Drawing.Point(109, 99);
            this.textBoxClickPos.Name = "textBoxClickPos";
            this.textBoxClickPos.Size = new System.Drawing.Size(100, 21);
            this.textBoxClickPos.TabIndex = 4;
            this.textBoxClickPos.Text = "1338,812";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "单击位置";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(56, 149);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 5;
            this.buttonStart.Text = "开始";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 450);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.textBoxClickPos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxRange);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSelectRange);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonSelectRange;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxRange;
        private System.Windows.Forms.TextBox textBoxClickPos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonStart;
    }
}

