using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRTERM.UI
{
	public partial class FontTestPad : Form
	{
		public FontTestPad()
		{
			InitializeComponent();
		}



		string AsciiText="";
		private void FontTestPad_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			Brush textBrush = new SolidBrush(ForeColor);
			Font textFont=new System.Drawing.Font("Lucida Console",12);

			g.DrawString(AsciiText, textFont, textBrush, new PointF(0, 0));
		}

		private void FontTestPad_Load(object sender, EventArgs e)
		{
			WriteASCII();
		}

		void WriteASCII() {
			textBox1.Text = "Hello";

			byte[] bytes=new byte[256];
			for(int i=0; i<256; i++)
			{
				bytes[i] = (byte)i;
			}
			
		}
	}
}
