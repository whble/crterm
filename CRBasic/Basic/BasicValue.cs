using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRBasic.Basic
{
    public class BasicValue
    {
        public DataTypes DataType;
        public object Value;

        public bool IsNumeric
        {
            get
            {
                switch (DataType)
                {
                    case DataTypes.Integer:
                    case DataTypes.Single:
                    case DataTypes.Double:
                        return true;
                    case DataTypes.Variable:
                        return false;
                }
                return false;
            }
        }

        public override string ToString()
        {
            switch (DataType)
            {
                case DataTypes.EndOfStatement:
                    return ":";
                case DataTypes.String:
                case DataTypes.Integer:
                case DataTypes.Single:
                case DataTypes.Double:
                    return Value.ToString();
                case DataTypes.Text:
                    return Value.ToString();
                case DataTypes.Variable:
                    BasicVariable v = Value as BasicVariable;
                    if (v == null)
                    {
                        return Value.ToString();
                    }
                    return v.Name;
                case DataTypes.Command:
                    return Value.ToString();
                default:
                    return Value.ToString();
            }
        }

        public int IntVal
        {
            get
            {
                if (IsNumeric)
                    return (int)Value;
                return 0;
            }
        }

    }
}
