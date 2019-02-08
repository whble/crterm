using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRBasic.Basic
{
    public class ProgramLine 
    {
        public const int LN_IMMEDIATE = -1;
        public int LineNumber = LN_IMMEDIATE;
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
                LineNumber = LN_IMMEDIATE;
            }
        }

        public void Add(object Value, DataTypes Type)
        {
            BasicSymbol sym = new BasicSymbol();
            sym.Type = Type;
            sym.Value = Value;
            Symbols.Add(sym);
        }
    }
}
