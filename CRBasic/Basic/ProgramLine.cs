using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRBasic.Basic
{
    public class ProgramLine 
    {
        public const int IMMEDIATE = -1;
        public int LineNumber = IMMEDIATE;
        public List<BasicSymbol> Symbols = new List<BasicSymbol>();

        public ProgramLine()
        {
        }

        public bool IsImmediate
        {
            get
            {
                return LineNumber < 0;
            }
            set
            {
                LineNumber = IMMEDIATE;
            }
        }

        public void Add(object Value, DataTypes Type)
        {
            BasicSymbol sym = new BasicSymbol();
            sym.DataType = Type;
            sym.Value = Value;
            Symbols.Add(sym);
        }

        public void Add(BasicSymbol newSymbol)
        {
            Symbols.Add(newSymbol);
        }
    }
}
