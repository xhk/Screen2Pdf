using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sreen2Pdf
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void buttonSelectRange_Click(object sender, EventArgs e)
		{
			var body = new RangeForm();
			body.Show();
		}

        private void buttonStart_Click(object sender, EventArgs e)
        {
            this.Hide();
            var rngArr = textBoxRange.Text.Split(',');
            var left = int.Parse(rngArr[0]);
            var top = int.Parse(rngArr[1]);
            var right = int.Parse(rngArr[2]);
            var bottom = int.Parse(rngArr[3]);

            var posArr = textBoxClickPos.Text.Split(',');
            var x = int.Parse(posArr[0]);
            var y = int.Parse(posArr[1]);

            SetCursorPos(x, y);

            // 先Focus
            mouse_event((int)(MouseEventFlags.LeftUp | MouseEventFlags.Absolute), x, y, 0, IntPtr.Zero);
            mouse_event((int)(MouseEventFlags.LeftDown | MouseEventFlags.Absolute), x, y, 0, IntPtr.Zero);

            for(int i = 0; i < 1040; ++i)
            {
                var bmp = new Bitmap(right - left, bottom - top);
                var g = Graphics.FromImage(bmp);
                g.CopyFromScreen(left, top, 0, 0, new Size(right - left, bottom - top));
                bmp.Save($"{i+1:D4}.bmp");


                Thread.Sleep(5000);
                mouse_event((int)(MouseEventFlags.LeftUp | MouseEventFlags.Absolute), x, y, 0, IntPtr.Zero);
                mouse_event((int)(MouseEventFlags.LeftDown | MouseEventFlags.Absolute), x, y, 0, IntPtr.Zero);
            }
        }

        [DllImport("User32")]
        public extern static void mouse_event(int dwFlags, int dx, int dy, int dwData, IntPtr dwExtraInfo);
        [DllImport("User32")]
        public extern static void SetCursorPos(int x, int y);
        [DllImport("User32")]
        public extern static bool GetCursorPos(out POINT p);
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }
        public enum MouseEventFlags
        {
            Move = 0x0001, //移动鼠标
            LeftDown = 0x0002,//模拟鼠标左键按下
            LeftUp = 0x0004,//模拟鼠标左键抬起
            RightDown = 0x0008,//鼠标右键按下
            RightUp = 0x0010,//鼠标右键抬起
            MiddleDown = 0x0020,//鼠标中键按下 
            MiddleUp = 0x0040,//中键抬起
            Wheel = 0x0800,
            Absolute = 0x8000//标示是否采用绝对坐标
        }

		private void button2Pdf_Click(object sender, EventArgs e)
		{
			DirectoryInfo root = new DirectoryInfo("Img");
			var files = root.GetFiles();
			iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 25, 25, 25, 25);
			iTextSharp.text.pdf.PdfWriter.GetInstance(document, new FileStream(@"1.pdf", FileMode.Create, FileAccess.ReadWrite));
			document.Open();
			iTextSharp.text.Image image;
			for (int i = 0; i < files.Length; i++)
			{
				if (String.IsNullOrEmpty(files[i].FullName)) break;


				image = iTextSharp.text.Image.GetInstance(files[i].FullName);


				if (image.Height > iTextSharp.text.PageSize.A4.Height - 25)
				{
					image.ScaleToFit(iTextSharp.text.PageSize.A4.Width - 25, iTextSharp.text.PageSize.A4.Height - 25);
				}
				else if (image.Width > iTextSharp.text.PageSize.A4.Width - 25)
				{
					image.ScaleToFit(iTextSharp.text.PageSize.A4.Width - 25, iTextSharp.text.PageSize.A4.Height - 25);
				}
				image.Alignment = iTextSharp.text.Image.ALIGN_MIDDLE;
				document.NewPage();
				document.Add(image);
				//iTextSharp.text.Chunk c1 = new iTextSharp.text.Chunk("Hello World");
				//iTextSharp.text.Phrase p1 = new iTextSharp.text.Phrase();
				//p1.Leading = 150;      //行间距
				//document.Add(p1);
			}

			document.Close();
		}
	}

   
}
