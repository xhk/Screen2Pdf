using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sreen2Pdf
{
	public partial class RangeForm : Form
	{
		private bool mousedown = false;
		private Point startpoint = new Point();
		private Point endpoint = new Point();
		private Graphics bkgGraphics = null;
		private Bitmap srcBmp = null;
		public RangeForm()
		{
			InitializeComponent();
		}

		private void RangeForm_Load(object sender, EventArgs e)
		{
			var bmp = new Bitmap(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height);
			//Image img = bmp;
			bkgGraphics = Graphics.FromImage(bmp);
			bkgGraphics.CopyFromScreen(new Point(0, 0), new Point(0, 0), Screen.AllScreens[0].Bounds.Size);
			ImgRgbToGray(bmp);
			srcBmp = bmp;
			bkgGraphics.DrawRectangle(new Pen(Color.Red,2), new Rectangle(0, 0, 500, 500));
			
			BackgroundImage = bmp;

			this.WindowState = FormWindowState.Maximized;

			
		}

		private void RangeForm_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (((MouseEventArgs)e).Button == MouseButtons.Left)
			{
				//保存图片
				Image memory = new Bitmap(this.Width, this.Height);
				Graphics g = Graphics.FromImage(memory);
				g.CopyFromScreen(this.Location.X + 1, this.Location.Y + 1, 0, 0, this.Size);

				Clipboard.SetImage(memory);//把图片放到剪切板中
				this.Close();
			}
		}

		private void RangeForm_DoubleClick(object sender, EventArgs e)
		{

		}

		private void RangeForm_MouseDown(object sender, MouseEventArgs e)
		{
			//记录开始点
			this.mousedown = true;
			this.startpoint = e.Location;
		}

		private void RangeForm_MouseUp(object sender, MouseEventArgs e)
		{
			//记录结束点。绘制到窗口上
			if (mousedown)
			{
				this.endpoint = e.Location;
				this.Refresh();
				//gform.DrawImage(this.bmsave, new Point(0, 0));
				Rectangle rect = new Rectangle();
				this.rect_play(ref rect);

				//this.DrawRectangle(new Pen(Color.Black), rect);
			}
		}

		private void RangeForm_MouseMove(object sender, MouseEventArgs e)
		{
			//记录结束点。绘制到bitmap上
			this.endpoint = e.Location;
			this.mousedown = false;
			Rectangle rect = new Rectangle();
			this.rect_play(ref rect);
			//DrawRectangle(new Pen(Color.Black), rect);
			//gform.DrawImage(this.bmsave, new Point(0, 0));

			var bmp = (Bitmap)srcBmp.Clone();
			var g = Graphics.FromImage(bmp);
			g.DrawRectangle(new Pen(Color.Red, 2), new Rectangle(0, 0, 500, 500));
			BackgroundImage = bmp;
		}

		private void rect_play(ref Rectangle rect)
		{
			//根据两个点确定矩形的左上角点Location
			if (this.startpoint.X > this.endpoint.X && this.startpoint.Y < this.endpoint.Y)
			{
				rect.Location = new Point(this.endpoint.X, this.startpoint.Y);
			}
			else if (this.startpoint.X < this.endpoint.X && this.startpoint.Y > this.endpoint.Y)
			{
				rect.Location = new Point(this.startpoint.X, this.endpoint.Y);

			}
			else if (this.startpoint.X > this.endpoint.X && this.startpoint.Y > this.endpoint.Y)
			{
				rect.Location = this.endpoint;
			}
			else
			{
				rect.Location = this.startpoint;
			}
			//获取两点的X,Y距离
			rect.Width = Math.Abs(this.startpoint.X - this.endpoint.X);
			rect.Height = Math.Abs(this.startpoint.Y - this.endpoint.Y);


			if (rect.Width == 0 && rect.Height == 0)
			{
				//防止误点的时候进行绘制
			}
			else if (rect.Width == 0)
			{
				rect.Width = 1;

			}
			else if (rect.Height == 0)
			{
				rect.Height = 1;
			}
		}

		private Bitmap ImgRgbToGray(Bitmap bitMap)
		{
			// 速度太慢，待优化
			//for (int i = 0; i < bitMap.Width; i++)
			//{
			//	for (int j = 0; j < bitMap.Height; j++)
			//	{
			//		Color origalColor = bitMap.GetPixel(i, j);
			//		int grayScale = (int)(origalColor.R * .3 + origalColor.G * .59 + origalColor.B * .11);
			//		Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);
			//		bitMap.SetPixel(i, j, newColor);
			//	}
			//}

			return bitMap;
		}
	}
}
