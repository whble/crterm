using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRBasic.Basic
{
    public class ProgramStep : EventArgs
    {
        /// <summary>
        /// The current executing line
        /// </summary>
        public List<BasicSymbol> Symbols = new List<BasicSymbol>();
        public int LineNumber = -1;

        /// <summary>
        /// The current command or argument being processed. 
        /// After completing the command, Pos will be set to the position following the statement -
        /// either after the end of the line or on the colon.
        /// </summary>
        public int Pos;
        public BasicSymbol LValue = null;
        public BasicSymbol RValue = null;
        public BasicOperator Operation = null;
        public BasicSymbol Result;

        public ProgramStep(ProgramLine CurrentLine)
        {
            this.Symbols = new List<BasicSymbol>();
            foreach (var item in CurrentLine.Symbols)
            {
                this.Symbols.Add(item);
            }
            this.LineNumber = CurrentLine.LineNumber;
            this.Pos = 0;
        }

        public void Next()
        {
            Pos++;
        }

        public BasicSymbol CurrentSymbol
        {
            get
            {
                if (Pos < 0 || Pos >= Symbols.Count)
                    return null;
                return Symbols[Pos];
            }
            set
            {
                if (Pos < 0 || Pos >= Symbols.Count)
                    throw new BasicException("Invalid position in expression", "Pos=" + Pos.ToString() + ", Count=" + Symbols.Count.ToString() + ", Value=" + value.ToString());
                Symbols[Pos] = value;
            }
        }

        public bool EndOfStatement
        {
            get
            {
                if (CurrentSymbol == null)
                    return true;
                if (CurrentSymbol.DataType == DataTypes.EndOfStatement)
                    return true;
                if (CurrentSymbol.Value.ToString() == ":")
                    return true;
                return false;
            }
        }

        public bool EndOfLine
        {
            get {
                if (Pos >= Symbols.Count)
                    return true;
                return false;
            }
        }

        internal void Add(BasicSymbol currentSymbol)
        {
            Symbols.Add(currentSymbol);
        }

    }
}
