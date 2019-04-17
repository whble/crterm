using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRBasic.Basic
{
    public class StringBuffer
    {
        #region fields
        string _text = null;
        public int Pos = 0;
        public bool InQuote = false;
        private int ParenLevel = 0;
        #endregion

        #region Constructors
        public StringBuffer(string BufferText)
        {
            Text = BufferText;
            Pos = 0;
        }
        #endregion

        #region public properties
        public char ThisChar
        {
            get
            {
                if (Pos < Text.Length)
                {
                    return Text[Pos];
                }

                return '\0';
            }
        }
        public char NextChar
        {
            get
            {
                if (Pos < Text.Length - 1)
                {
                    return '\0';
                }

                return Text[Pos + 1];
            }
        }

        public char LastChar
        {
            get
            {
                if (Pos <= 0 || Pos >= Text.Length)
                {
                    return '\0';
                }

                return Text[Pos - 1];
            }
        }

        public string Text
        {
            get
            {
                return this._text;
            }

            set
            {
                this._text = value;
                this.Pos = 0;
            }
        }

        #endregion

        #region public methods
        public bool EndOfLine
        {
            get
            {
                return Pos >= Text.Length;
            }
        }

        public bool EndOfStatement
        {
            get
            {
                if (EndOfLine)
                {
                    return true;
                }

                if (!InQuote && ThisChar == ':')
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Returns current character and advances to next position in string
        /// </summary>
        /// <returns></returns>
        public char Next()
        {
            if (Pos >= Text.Length)
            {
                return '\0';
            }

            char c = Text[Pos++];
            if (c == '"')
            {
                InQuote = !InQuote;
            }

            if (!InQuote && c == '(')
            {
                ParenLevel++;
            }

            if (!InQuote && c == ')')
            {
                ParenLevel--;
            }

            return c;
        }

        /// <summary>
        /// Discards all text to the end of the current statement. 
        /// <para>Reading ends at a colon (:) or end of line. </para>
        /// <para>If a colon is found, the colon is discarded and Pos will be on the character following the colon.</para>
        /// </summary>
        /// <returns></returns>
        private void SkipToEnd()
        {
            char c = this.Next();
            while (!this.EndOfStatement)
            {
                c = Next();
            }
        }

        /// <summary>
        /// Read to the end of the current statement. 
        /// </summary>
        /// <returns></returns>
        public string ReadToEOS()
        {
            if (EndOfStatement || EndOfLine)
                return "";

            int start = Pos;
            while (!EndOfStatement)
                Next();

            int len = Pos - start;
            return Text.Substring(start, len);
        }

        #endregion

    }
}
