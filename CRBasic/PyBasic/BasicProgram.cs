using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRBasic.PyBasic
{
    public class BasicProgram
    {
        public TerminalUI.DisplayControl Display = null;
        public BasicTokens Commands = new BasicTokens();
        public SortedList<int, BasicLine> Lines = new SortedList<int, BasicLine>();
        public SortedList<string, BasicVariable> Variables = new SortedList<string, BasicVariable>();
        public Stack<BasicVariable> Stack = new Stack<BasicVariable>();

        public void Add(BasicLine pl)
        {
            Lines.Add(pl.LineNumber, pl);
        }

        public void List(int Start = 0, int End = int.MaxValue)
        {
            foreach (int ln in Lines.Keys)
            {
                BasicLine line = Lines[ln];
                Display.PrintLine(line.ToString());
            }
        }

        public string Translate()
        {
            StringBuilder s = new StringBuilder();
            foreach (int ln in Lines.Keys)
            {
                BasicLine line = Lines[ln];
                s.AppendLine(line.Translate());
            }
            return s.ToString();
        }

        internal void PList()
        {
            foreach (int ln in Lines.Keys)
            {
                BasicLine line = Lines[ln];
                Display.Print("'");
                Display.Print(ln.ToString().PadLeft(5));
                Display.Print(" ");
                Display.PrintLine(line.Translate());
            }
        }
    }
}
