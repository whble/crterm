using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRBasic.Basic
{
    public class BasicProgram
    {
        public TerminalUI.DisplayControl Display = null;
        public BasicTokens Commands = new BasicTokens();
        public SortedList<int, ProgramLine> Lines = new SortedList<int, ProgramLine>();
        public SortedList<string, BasicVariable> Variables = new SortedList<string, BasicVariable>();
        public Stack<BasicVariable> Stack = new Stack<BasicVariable>();

        public void Add(ProgramLine pl)
        {
            Lines.Add(pl.LineNumber, pl);
        }

        public void List(int Start = 0, int End = int.MaxValue)
        {
            foreach (int ln in Lines.Keys)
            {
                ProgramLine line = Lines[ln];
                Display.Print(line.LineNumber.ToString().PadLeft(5));
                for (int i = 0; i < line.Symbols.Count; i++)
                {
                    BasicSymbol b = line.Symbols[i];
                    Display.Print(" ");
                    Display.Print(b.ToString());
                }
                Display.PrintLine();
            }
        }
    }
}
