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
            ActiveControl = terminalControl1;
        }

        private void PrintTestText()
        {
            terminalControl1.Clear();
            terminalControl1.Locate(0, 0);
            for (int i = 1; i <= terminalControl1.Rows; i++)
            {
                if (i > 1)
                    terminalControl1.PrintNewLine();
                string s = i.ToString("00 ");
                terminalControl1.Print(s);
            }

            terminalControl1.Locate(0, 0);
            for (int i = 1; i <= terminalControl1.Columns / 10; i += 1)
            {
                terminalControl1.Print("         ");
                string s = i.ToString("0");
                terminalControl1.Print(s[0]);
            }

            terminalControl1.Locate(1, 0);
            for (int i = 1; i <= terminalControl1.Columns; i++)
            {
                string s = i.ToString("00");
                terminalControl1.Print(s[1]);
            }

            terminalControl1.Locate(2, 3);
        }

    }
}
