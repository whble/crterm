using System;

namespace CRBasic.Basic
{
    public class BasicSymbol : BasicValue
    {
        public enum Padding
        {
            /// <summary>
            /// A space is not added before or after this symbol
            /// </summary>
            None,
            /// <summary>
            /// Always put a space before and after this symbol
            /// </summary>
            Required,
            /// <summary>
            /// A space is preferred but not necessary. Spaces will be printed
            /// if this symbol and the one next to it both "Allow".
            /// </summary>
            Allowed,
        }
        public Padding PadSpace = Padding.Allowed;
        public bool SpaceAfter = false;

        public string ListText()
        {
            if (DataType == DataTypes.String)
                return "\"" + Value.ToString() + "\"";
            return this.ToString();
        }

        public void SetText(string Text)
        {
            DataType = DataTypes.Text;
            Value = Text;
        }

        public void SetToken(string TokenText)
        {
            DataType = DataTypes.Command;
            Value = BasicTokens.Commands[TokenText];
        }

        public void SetOperator(string su)
        {
            DataType = DataTypes.Operator;
            Value = BasicTokens.Operators[su];
        }
    }
}
