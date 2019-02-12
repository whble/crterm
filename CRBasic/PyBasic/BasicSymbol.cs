using System.Text;

namespace CRBasic.PyBasic
{
    public class BasicSymbol
    {
        public DataTypes DataType;
        public object Value = null;

        public override string ToString()
        {
            switch (DataType)
            {
                case DataTypes.EndOfStatement:
                    return ":";
                case DataTypes.String:
                    return Value.ToString();
                case DataTypes.Number:
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
                case DataTypes.Token:
                    return Value.ToString();
                default:
                    return Value.ToString();
            }
        }

        internal void SetText(string Text)
        {
            DataType = DataTypes.Text;
            Value = Text;
        }

        internal void SetToken(string TokenText)
        {
            DataType = DataTypes.Token;
            Value = BasicTokens.Commands[TokenText];
        }

        public string Translate()
        {
            StringBuilder s = new StringBuilder(); 

            switch (this.DataType)
            {
                case DataTypes.EndOfLine:
                    break;
                case DataTypes.EndOfStatement:
                    s.Append(";");
                    break;
                case DataTypes.String:
                case DataTypes.Number:
                    s.Append(this.ToString());
                    break;
                case DataTypes.Text:
                    s.Append(this.ToString());
                    break;
                case DataTypes.Variable:
                    string v = this.ToString();
                    string vs = v.Substring(0, v.Length - 1);
                    string ve = v.Substring(v.Length - 1, 1);

                    switch (ve)
                    {
                        case "$":
                            s.Append(vs);
                            s.Append("S");
                            break;
                        case "%":
                            s.Append(vs);
                            s.Append("I");
                            break;
                        case "!":
                            s.Append(vs);
                            s.Append("R");
                            break;
                        case "#":
                            s.Append(vs);
                            s.Append("D");
                            break;
                        default:
                            s.Append(v);
                            break;
                    }
                    break;
                case DataTypes.Token:
                    BasicToken t = this.Value as BasicToken;
                    s.Append(t.Name);
                    break;
                case DataTypes.Delimiter:
                    s.Append(this.Value.ToString());
                    break;
                default:
                    break;
            }
            return s.ToString();
        }
    }
}
