using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRBasic
{
    public partial class CRBasicMain : Form
    {
        BasicMain BasicMain = new BasicMain();
        public CRBasicMain()
        {
            InitializeComponent();
        }

        private void CRBasicMain_Load(object sender, EventArgs e)
        {
            BasicMain.Display = frameBuffer1;
            BasicMain.Init();
        }
    }
}
