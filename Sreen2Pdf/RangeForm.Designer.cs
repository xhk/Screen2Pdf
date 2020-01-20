namespace Sreen2Pdf
{
	partial class RangeForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// RangeForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "RangeForm";
			this.Text = "RangeForm";
			this.TopMost = true;
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.RangeForm_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.RangeForm_Paint);
			this.DoubleClick += new System.EventHandler(this.RangeForm_DoubleClick);
			this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.RangeForm_MouseDoubleClick);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RangeForm_MouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.RangeForm_MouseMove);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RangeForm_MouseUp);
			this.ResumeLayout(false);

		}

		#endregion
	}
}