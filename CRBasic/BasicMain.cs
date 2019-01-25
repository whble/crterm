using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRBasic
{
    public class BasicMain
    {
        public TerminalControl.IFrameBuffer Display;

        public void Init()
        {
            Cls();
            Print("CR BASIC by Compiled Reality");
            Print("(C)2019 Thomas Wilson");
            Print(Free().ToString(), true);
            Print(" bytes free");
            Ok();
        }

        public void Ok()
        {
            Print("Ok");
        }

        public int Free()
        {
            return 65536;
        }

        public void Print(string s, bool SuppressNewline = false)
        {
            if (Display == null)
                return;

            Display.Print(s);
            if (!SuppressNewline)
                Display.PrintNewLine();
        }

        public void Cls()
        {
            if (Display == null)
                return;

            Display.Clear();
        }
    }
}
