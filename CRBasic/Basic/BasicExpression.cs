namespace CRBasic.Basic
{
    public class BasicExpression
    {
        public enum ExpressionTypes
        {
            // 1, 2
            IntLiteral,
            // 3.14, 0.5
            FloatLiteral,
            // "Hello"
            StringLiteral,
            // X, A$, B(3), C%
            Variable, 
            // SIN(32)
            Function,
            // TO, STEP, THEN
            CommandPart
        }
        /// <summary>
        /// The expression as written in the code line
        /// </summary>
        public string Text;

        /// <summary>
        /// Tokenized value of the expression
        /// </summary>
        public object Value;

        public BasicExpression()
        {
        }

        public BasicExpression(string Text)
        {
            this.Text = Text;
        }
    }
}