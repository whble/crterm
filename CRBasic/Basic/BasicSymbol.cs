namespace CRBasic.Basic
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
    }
}
