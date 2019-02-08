using System.Text;
using System.Windows.Forms;
using TerminalUI;
using TerminalUI.Terminals;

namespace CRBasic
{
    /// <summary>
    /// BASIC style Full screen editor. This holds one screen of text and the RETURN or ENTER key
    /// submits a line of text to the interpreter. 
    /// </summary>
    public class Editor : TerminalUI.IEditorPlugin
    {
        private const char CR = '\r';
        private const char LF = '\n';

        public DisplayControl Display { get; set; }
        public ITerminal Terminal { get; set; }
        public IInterpreter Interpreter { get; set; }

        private TerminalUI.Terminals.TerminalKeyEventArgs KeyFlags = new TerminalKeyEventArgs();
        private Keys Modifiers = Keys.None;
        private Keys LastKey = Keys.None;

        public void HandleKeyDown(object sender, KeyEventArgs e)
        {
            Modifiers = e.Modifiers;

            switch (e.KeyCode)
            {
                case Keys.Up:
                    Display.MoveUp();
                    break;
                case Keys.Down:
                    Display.MoveDown();
                    break;
                case Keys.Left:
                    Display.MoveLeft();
                    break;
                case Keys.Right:
                    Display.MoveRight();
                    break;
                case Keys.Home:
                    if (Modifiers == Keys.Control)
                    {
                        Display.Clear();
                    }
                    Display.CurrentColumn = 0;
                    break;
                case Keys.End:
                    Display.CurrentColumn = Display.Columns - 1;
                    while (Display.CharUnderCursor <= ' ' && Display.CurrentColumn > 0)
                    {
                        Display.MoveLeft();
                    }
                    if (Display.CharUnderCursor > ' ')
                        Display.MoveRight();
                    break;
                case Keys.Insert:
                     
                default:
                    if(e.KeyCode != LastKey)
                        System.Diagnostics.Debug.WriteLine("Key Down " + e.KeyCode.ToString());
                    LastKey = e.KeyCode;
                    break;
            }
        }

        public void HandleKeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case CR:
                    if (Modifiers.HasFlag(Keys.Shift))
                        Display.PrintLine();
                    else
                        ParseLine();
                    break;
                case LF:
                    Display.PrintLine();
                    break;
                default:
                    Terminal.Print(e.KeyChar.ToString());
                    if (e.KeyChar < ' ' || e.KeyChar > 127)
                    {
                        System.Diagnostics.Debug.WriteLine(((int)(e.KeyChar)).ToString() + " " + Modifiers.ToString());
                    }
                    break;
            }
        }

        public void HandleKeyUp(object sender, KeyEventArgs e)
        {
            Modifiers = e.Modifiers;
        }

        /// <summary>
        /// Parse the line of text under the cursor
        /// </summary>
        public void ParseLine()
        {
            StringBuilder s = new StringBuilder();
            int to = Display.CurrentRowStart + Display.Columns;
            for (int i = Display.CurrentRowStart; i < to; i++)
            {
                s.Append(Display.CharacterData[i].Value);
            }
            string line = s.ToString().Trim();

            //System.Diagnostics.Debug.WriteLine(line);
            Display.PrintLine();
            Interpreter.Execute(line);
        }
    }
}
