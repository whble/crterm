using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TerminalUI.Terminals
{
    public class TerminalKeyEventArgs : System.Windows.Forms.KeyPressEventArgs
    {
        const char CHAR_NONE = '\0';

        public Keys KeyCode = Keys.None;
        public Keys Modifier = Keys.None;

        public TerminalKeyEventArgs() : base(CHAR_NONE)
        {
        }

        public TerminalKeyEventArgs(KeyEventArgs e) : base(CHAR_NONE)
        {
            Modifier = e.Modifiers;
            KeyCode = e.KeyCode;
            this.KeyChar = CHAR_NONE;

            System.Diagnostics.Debug.WriteLine("Modifiers: " + Modifier.ToString());
        }

        public TerminalKeyEventArgs(Keys KeyCode, Keys Modifiers = Keys.None) : base(CHAR_NONE)
        {
            this.Modifier = Modifiers;
            this.KeyCode = KeyCode;
        }

        public TerminalKeyEventArgs(char KeyChar, Keys Modifiers = Keys.None) : base(KeyChar)
        {
            this.Modifier = Modifiers;
            this.KeyCode = Keys.None;
        }

        public override string ToString()
        {

            StringBuilder s = new StringBuilder();
            if (Modifier.HasFlag(ConsoleModifiers.Control))
                s.Append("Control+");
            if (Modifier.HasFlag(ConsoleModifiers.Alt))
                s.Append("Alt+");
            if (Modifier.HasFlag(ConsoleModifiers.Shift))
                s.Append("Shift+");
            if (KeyCode != Keys.None)
                s.Append(KeyCode.ToString());
            else if (KeyChar != CHAR_NONE)
                s.Append(KeyChar);
            return s.ToString();
        }

        public override bool Equals(object obj)
        {
            TerminalKeyEventArgs k = obj as TerminalKeyEventArgs;
            if (k == null)
                return false;

            if (this.KeyCode == Keys.None)
                return (this.KeyCode == k.KeyCode && this.Modifier == k.Modifier);
            else
                return this.KeyChar == k.KeyChar;
        }


        public override int GetHashCode()
        {
            if (this.KeyCode != Keys.None)
            {
                return 0 - ((int)this.Modifier | (int)this.KeyCode);
            }
            else
                return this.KeyChar;
        }

    }
    public delegate void TerminalKeyHandler(DisplayControl frameBuffer, TerminalKeyEventArgs e);

    /// <summary>
    /// Special keys for CRTerm: mode switching, menu, etc. Terminal specific keys (Up/Down/Left/Right, etc) will be
    /// processed directly in the relevant terminal. 
    /// </summary>
    public class TerminalKeyMap
    {
        // Keys specific to CRTerm
        public TerminalKeyEventArgs BASIC_ModeToggle = new TerminalKeyEventArgs() { KeyCode = System.Windows.Forms.Keys.F12 };
    }
}
