using System;
using System.Windows.Forms;

namespace CRBasic
{
    public partial class CRBasicMain : Form
    {
        // private IInterpreter engine = new PyMain();
        // private IInterpreter engine = new BasicMain();
        private IInterpreter engine = new CRBasic.PyBasic.BasicMain();

        public CRBasicMain()
        {
            InitializeComponent();
        }

        private void CRBasicMain_Load(object sender, EventArgs e)
        {
            TerminalUI.Terminals.ANSITerminal ansi = new TerminalUI.Terminals.ANSITerminal();
            Display.Terminal = ansi;
            engine.Display = Display;
            engine.Display.EchoMode = TerminalUI.Terminals.EchoModes.Plugin;

            var editor = new Editor();
            Display.Editor = editor;
            editor.Interpreter = engine;

            engine.Init();
        }

        private void CRBasicMain_KeyDown(object sender, KeyEventArgs e)
        {
            Display.HandleKeyDown(sender, e);
        }

        private void CRBasicMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            Display.HandleKeyPress(sender, e);
        }

        private void CRBasicMain_KeyUp(object sender, KeyEventArgs e)
        {
            Display.HandleKeyUp(sender, e);
        }
    }
}

