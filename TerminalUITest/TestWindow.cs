using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TerminalUITest
{
    public partial class TestWindow : Form
    {
        public TestWindow()
        {
            InitializeComponent();
        }

        private void TestWindow_Load(object sender, EventArgs e)
        {
            PrintTestText();
        }

        private void PrintTestText()
        {
            frameBuffer1.Clear();
            frameBuffer1.Locate(0, 0);
            for (int i = 1; i <= 8; i += 1)
            {
                frameBuffer1.Print("         ");
                string s = i.ToString("0");
                frameBuffer1.PrintChar(s[0]);
            }

            frameBuffer1.Locate(1, 0);
            for (int i=1; i<=80; i++)
            {
                string s = i.ToString("00");
                frameBuffer1.PrintChar(s[1]);
            }

            frameBuffer1.Locate(0, 0);
            for (int i=1; i<=25; i++)
            {
                string s = i.ToString("00 ");
                frameBuffer1.Print(s);
                if (i < 25)
                    frameBuffer1.PrintNewLine();
            }

            frameBuffer1.Locate(2, 3);
        }
    }
}
