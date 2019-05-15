using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TerminalUI
{
    public interface IEditorPlugin
    {
        TerminalUI.DisplayControl Display { get; set; }
        TerminalUI.Terminals.ITerminal Terminal { get; set; }

        void HandleKeyDown(object sender, KeyEventArgs e);
        void HandleKeyPress(object sender, KeyPressEventArgs e);
        void HandleKeyUp(object sender, KeyEventArgs e);
    }
}
