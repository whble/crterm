using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRBasic.Basic
{
    public class ProgramLine
    {
        public const int LINENUMBER_IMMEDIATE = -1;
        public BasicLabel Label = null;
        /// <summary>
        /// When true, a closing paren should be added before the end of statement symbol.
        /// </summary>
        public bool NeedsClosingParen;

        public int LineNumber = LINENUMBER_IMMEDIATE;
        public string Command = "";
        public string Arguments = "";
        public string LValue = "";

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
                LineNumber = LINENUMBER_IMMEDIATE;
            }
        }

        /// <summary>
        /// Get the BASIC text as entered.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder s = new StringBuilder();

            if (!IsImmediate)
            {
                s.Append(LineNumber.ToString());
                s.Append(" ");
            }

            if (Command == "=")
            {
                s.Append(LValue);
                s.Append(" =");
            }
            else
                s.Append(Command);

            if (Arguments != "")
            {
                s.Append(" ");
                s.Append(Arguments);
            }
            return s.ToString();
        }
    }
}
