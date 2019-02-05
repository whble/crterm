using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalUI;

namespace CRBasic
{
    public class BasicMain : IInterpreter
    {
        List<string> _programText = new List<string>();
        public DisplayControl Display { get; set; }

        public List<string> ProgramText
        {
            get
            {
                return _programText;
            }
        }

        public void Init()
        {
            Cls();
            Print("CR BASIC by Compiled Reality");
            Print("(C)2019 Tom P. Wilson");
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

        public void Execute(string Line)
        {
            throw new NotImplementedException();
        }

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}
