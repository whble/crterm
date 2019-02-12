using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRBasic.PyBasic
{
    public class BasicLine
    {
        public static int IndentLevel = 0;
        public const int LN_IMMEDIATE = -1;
        public int LineNumber = LN_IMMEDIATE;
        public List<BasicSymbol> Symbols = new List<BasicSymbol>();

        public BasicLine()
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
            sym.DataType = Type;
            sym.Value = Value;
            Symbols.Add(sym);
        }

        public string Translate()
        {
            StringBuilder s = new StringBuilder();
            string lastText = "";

            for (int i = 0; i < Symbols.Count; i++)
            {
                BasicSymbol sym = Symbols[i];
                DataTypes dt = sym.DataType;

                if (i > 0)
                    s.Append(" ");

                switch (dt)
                {
                    case DataTypes.Token:
                        BasicToken t = sym.Value as BasicToken;
                        t.TranslateCommand(s, t, this, ref i);
                        break;
                    case DataTypes.Text:
                        s.Append(sym.ToString());
                        break;
                    case DataTypes.EndOfStatement:
                        s.Append(";");
                        break;
                }
            }
            return s.ToString();
        }

        internal void AppendPrintParams(StringBuilder pythonString, BasicToken token, BasicLine arguments, ref int startIndex)
        {
            StringBuilder s = pythonString;
            string delim = "";

            for(int i=startIndex; i<arguments.Symbols.Count; i++)
            {
                BasicSymbol sym = arguments.Symbols[i];
                if (sym.DataType == DataTypes.EndOfLine
                    || sym.DataType == DataTypes.EndOfStatement)
                    break;

                string st = arguments.Symbols[i].ToString();
                switch(st)
                {
                    case ";":
                        delim = ";";
                        break;
                    case ",":
                        s.Append("+\"\t\"");
                        delim = ",";
                        break;
                    default:
                        if (i > startIndex)
                            s.Append("+");
                        s.Append(st);
                        delim = "";
                        break;
                }
            }

            if (delim == ";" || delim == ",")
                s.Append(",");
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();

            s.Append(LineNumber.ToString().PadLeft(6));
            for (int i = 0; i < Symbols.Count; i++)
            {
                BasicSymbol b = Symbols[i];
                s.Append(" ");
                s.Append(b.ToString());
            }
            return s.ToString();
        }
    }
}
