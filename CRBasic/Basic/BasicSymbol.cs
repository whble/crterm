using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRBasic.Basic
{
    public class BasicSymbol
    {
        public DataTypes Type;
        public object Value = "";

        public override string ToString()
        {
            switch (Type)
            {
                case DataTypes.EndOfStatement:
                    return ":";
                case DataTypes.String:
                    return "\"" + Value.ToString() + "\"";
                case DataTypes.Integer:
                case DataTypes.Single:
                case DataTypes.Double:
                    return Value.ToString();
                case DataTypes.Text:
                    return Value.ToString();
                case DataTypes.Variable:
                    BasicVariable v = Value as BasicVariable;
                    if (v == null)
                        return " " + Value.ToString();
                    return v.Name;
                case DataTypes.Token:
                    return Value.ToString();
                default:
                    return Value.ToString();
            }
        }
    }
}
