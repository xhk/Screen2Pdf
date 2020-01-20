using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sreen2Pdf
{
	public partial class RangeForm : Form
	{
		public Point StartPoint { get; private set; } = new Point();
		public Point EndPoint { get; private set; } = new Point();
		private bool mousedown = false;
		private Graphics bkgGraphics = null;
		private Bitmap srcBmp = null;
		private bool isSelected = false;
		public RangeForm()
		{
			InitializeComponent();
		}

		private Rectangle GetAllScreenRect()
		{
			int left = 0, top = 0, right = 0, bottom = 0;
			foreach(var s in Screen.AllScreens)
			{
				var rect = s.Bounds;
				if (left > rect.Left) { left = rect.Left; }
				if (top > rect.Top) { top = rect.Top; }
				if (right < rect.Right) { right = rect.Right; }
				if (bottom < rect.Bottom) { bottom = rect.Bottom; }
			}

			return new Rectangle(left, top, right - left, bottom - top);
		}

		private void RangeForm_Load(object sender, EventArgs e)
		{
			this.DoubleBuffered = true;
			this.StartPosition = FormStartPosition.Manual;

			var screenCount = Screen.AllScreens.Count();
			var fullRect = GetAllScreenRect();
			Screen childerScreen = Screen.AllScreens[1];
			//Location = new Point(fullRect.X, fullRect.Y);
			Location = childerScreen.WorkingArea.Location;

			Width = fullRect.Width;
			Height = fullRect.Height;

			//var bmp = new Bitmap(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height);
			srcBmp = new Bitmap(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height);
			//Image img = bmp;
			bkgGraphics = Graphics.FromImage(srcBmp);
			bkgGraphics.CopyFromScreen(new Point(0, 0), new Point(0, 0), Screen.AllScreens[0].Bounds.Size);
			//ImgRgbToGray(bmp);
			//bkgGraphics.DrawRectangle(new Pen(Color.Red,2), new Rectangle(0, 0, 500, 500));

			var bmp = (Bitmap)srcBmp.Clone();
			var g = Graphics.FromImage(bmp);
			g.FillRectangle(new SolidBrush(Color.FromArgb(72, 0, 0, 0)), 0, 0, bmp.Width, bmp.Height);
			BackgroundImage = bmp;

			//this.WindowState = FormWindowState.Maximized;

			//// 不在任务栏显示
			//this.ShowInTaskbar = false;
			////this.TopMost = true;

			//// 无边框
			//this.FormBorderStyle = FormBorderStyle.None;
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
			this.DialogResult = DialogResult.OK;
			Close();
		}

		private void RangeForm_MouseDown(object sender, MouseEventArgs e)
		{
			if (isSelected)
			{
				return;
			}
			//记录开始点
			this.mousedown = true;
			this.StartPoint = e.Location;
		}

		private void RangeForm_MouseUp(object sender, MouseEventArgs e)
		{
			if (isSelected)
			{
				return;
			}

			//记录结束点。绘制到窗口上
			if (mousedown)
			{
				this.EndPoint = e.Location;
				this.Refresh();
				//gform.DrawImage(this.bmsave, new Point(0, 0));
				Rectangle rect = new Rectangle();
				this.rect_play(ref rect);

				//this.DrawRectangle(new Pen(Color.Black), rect);
				isSelected = true;
			}
			mousedown = false;

		}

		private void RangeForm_MouseMove(object sender, MouseEventArgs e)
		{
			if(!this.mousedown)
			{
				return;
			}

			//记录结束点。绘制到bitmap上
			this.EndPoint = e.Location;


			//Rectangle rect = new Rectangle();
			//this.rect_play(ref rect);
			//this.Invalidate(rect);
			Invalidate();

			//BackgroundImage = DrawOnPicture();
		}

		private void rect_play(ref Rectangle rect)
		{
			//根据两个点确定矩形的左上角点Location
			if (this.StartPoint.X > this.EndPoint.X && this.StartPoint.Y < this.EndPoint.Y)
			{
				rect.Location = new Point(this.EndPoint.X, this.StartPoint.Y);
			}
			else if (this.StartPoint.X < this.EndPoint.X && this.StartPoint.Y > this.EndPoint.Y)
			{
				rect.Location = new Point(this.StartPoint.X, this.EndPoint.Y);

			}
			else if (this.StartPoint.X > this.EndPoint.X && this.StartPoint.Y > this.EndPoint.Y)
			{
				rect.Location = this.EndPoint;
			}
			else
			{
				rect.Location = this.StartPoint;
			}
			//获取两点的X,Y距离
			rect.Width = Math.Abs(this.StartPoint.X - this.EndPoint.X);
			rect.Height = Math.Abs(this.StartPoint.Y - this.EndPoint.Y);


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
			Bitmap b = bitMap;
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height),
			ImageLockMode.ReadWrite,
			PixelFormat.Format24bppRgb);
			int stride = bmData.Stride; // 扫描的宽度
			unsafe
			{
				byte* p = (byte*)bmData.Scan0.ToPointer(); // 获取图像首地址
				int nOffset = stride - b.Width * 3; // 实际宽度与系统宽度的距离
				byte red, green, blue;
				for (int y = 0; y < b.Height; ++y)
				{
					for (int x = 0; x < b.Width; ++x)
					{
						blue = p[0];
						green = p[1];
						red = p[2];

						p[0] = p[1] = p[2] = (byte)(.299 * red + .587 * green + .114 * blue); // 转换公式

						p += 3; // 跳过3个字节处理下个像素点
					}
					p += nOffset; // 加上间隔
				}
			}
			b.UnlockBits(bmData); // 解锁
			return b;
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

			//return bitMap;
		}

		private void RangeForm_Paint(object sender, PaintEventArgs e)
		{
			Rectangle rect = new Rectangle();
			this.rect_play(ref rect);
			var g = e.Graphics;
			g.DrawImage(srcBmp, rect, rect, GraphicsUnit.Pixel);
			g.DrawRectangle(new Pen(Color.Red, 2), rect);
		}	
	}
}
