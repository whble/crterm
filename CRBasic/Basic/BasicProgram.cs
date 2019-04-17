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
        public BasicTokens Tokens = new BasicTokens();
        public List<ProgramLine> Lines = new List<ProgramLine>();
        public BasicVariables Variables = new BasicVariables();
        public BasicParser Parser = new BasicParser();

        public void Add(ProgramLine Line)
        {
            if (Line.LineNumber == ProgramLine.LINENUMBER_IMMEDIATE)
                Lines.Add(Line);
            else
            {
                int i;
                for (i = 0; i < Lines.Count; i++)
                {
                    if (Lines[i].LineNumber > Line.LineNumber)
                        break;
                }
                if (i <= Lines.Count)
                    Lines.Insert(i, Line);
                else
                    Lines.Add(Line);
            }
        }

        internal void Add(string Text)
        {
            ProgramLine line = Parser.ParseLine(Text);
            Add(line);
        }
    }
}
