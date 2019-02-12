using System;
using System.Text;

namespace CRBasic.PyBasic
{
    /// <summary>
    /// Read a line of text and tokenize the objects. 
    /// </summary>
    public class BasicParser
    {
        private enum SymbolModes
        {
            None,
            Word,
            Number,
            LineNumber,
            Operator,
            Whitespace,
            Other,
            String,
            EndOfStatement
        }

        public int Pos = 0;
        public string Text = "";
        private BasicLine Line = new BasicLine();

        /// <summary>
        /// true when first character in symbol is a letter. 
        /// </summary>
        private bool InQuote = false;
        private int ParenLevel = 0;

        public BasicLine Parse(string CommandText)
        {
            Pos = 0;
            InQuote = false;
            ParenLevel = 0;
            Line = new BasicLine();
            Text = CommandText;

            if (IsDigit(ThisChar))
            {
                ParseLineNumber();
            }

            while (!EndOfLine)
            {
                ParseStatement();
            }
            return Line;
        }

        private void ParseLineNumber()
        {
            try
            {
                StringBuilder s = new StringBuilder();
                while (IsDigit(ThisChar))
                    s.Append(Read());
                Line.LineNumber = int.Parse(s.ToString());
            }
            catch (Exception ex)
            {
                throw new BasicException(ex, "Parsing line number");
            }

        }

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

                if (ParenLevel > 0 && ThisChar == ':')
                {
                    throw new BasicException("Syntax error", Line.LineNumber, Pos, ": is inside parentheses");
                }

                if (!InQuote && ThisChar == ':')
                {
                    return true;
                }

                return false;
            }
        }

        private char Read()
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

        private char ThisChar
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

        private char NextChar
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

        private char LastChar
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

        /// <summary>
        /// Tests whether the current letter is part of an identifier. 
        /// An identifier is a Letter followed by letters, digits, or Underscore. 
        /// Set First to true when testing the first letter, which will do strict checking (only allows A-Z and a-z). 
        /// Set First to false for subsequent letters, which will allow sigils, decimal, and underscore.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="First">Test the first character of a word; only returns true on letters. When False, returns true on letters, numbers, decimal, </param>
        /// <returns></returns>
        private bool IsIdentifier(char c, bool First)
        {
            if ((c >= 'A' && c <= 'Z')
                || (c >= 'a' && c <= 'z'))
            {
                return true;
            }

            if (First)
            {
                return false;
            }

            if ((c >= '0' && c <= '9')
                || (c == '%')
                || (c == '$')
                || (c == '!')
                || (c == '#')
                || (c == '_')
                || (c == '.'))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Tests if the character is an operator (+,-,*,/,^)
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private bool IsOperator(char c)
        {
            if (BasicTokens.Operators.Contains(c.ToString()))
            {
                return true;
            }

            return false;
        }

        private bool IsDigit(char c)
        {
            if (c >= '0' && c <= '9')
            {
                return true;
            }

            if (c == '.')
            {
                return true;
            }

            return false;
        }

        private bool IsWhitespace(char c)
        {
            if (c <= ' ')
            {
                return true;
            }

            return false;
        }

        private void DiscardWhiteSpace()
        {
            while (!EndOfStatement && IsWhitespace(ThisChar))
            {
                Pos++;
            }
        }

        /// <summary>
        /// Parse a statement. A statement is a word followed by 0 or more arguments and is 
        /// terminated by end of line or a colon (:). 
        /// <para>Parsed statemetns will be placed in the current ProgramLine</para>
        /// <para>If none of the above, a syntax error is generated.</para>
        /// </summary>
        private void ParseStatement()
        {
            char c;
            SymbolModes lastMode = SymbolModes.None;
            SymbolModes mode = SymbolModes.None;
            StringBuilder s = new StringBuilder();

            lastMode = mode;
            c = ThisChar;
            while (!EndOfStatement)
            {
                mode = CheckMode(c);
                lastMode = mode;
                while (mode == lastMode)
                {
                    s.Append(c);
                    Read();
                    c = ThisChar;
                    mode = CheckMode(c);
                }
                AppendSymbol(s.ToString(), lastMode);
                s.Clear();
            }
        }

        private SymbolModes CheckMode(char c)
        {
            if (InQuote)
                return SymbolModes.String;
            else if (c == '"')
                return SymbolModes.String;
            else if (IsDigit(c))
                return SymbolModes.Number;
            else if (IsIdentifier(c, true))
                return SymbolModes.Word;
            else if (IsOperator(c))
                return SymbolModes.Operator;
            else if (IsWhitespace(c))
                return SymbolModes.Whitespace;
            else
                return SymbolModes.Other;
        }

        private void AppendSymbol(string s, SymbolModes mode)
        {
            BasicSymbol ns = new BasicSymbol();
            string su = s.ToUpper();

            switch (mode)
            {
                case SymbolModes.None:
                    ns.SetText(s);
                    break;
                case SymbolModes.String:
                    ns.DataType = DataTypes.String;
                    ns.Value = s;
                    break;
                case SymbolModes.Operator:
                case SymbolModes.Word:
                    if (BasicTokens.Commands.ContainsKey(su))
                        ns.SetToken(su);
                    else
                        ns.SetText(s);
                    break;
                case SymbolModes.Number:
                    ns.Value = s;
                    ns.DataType = DataTypes.Number;
                    break;
                case SymbolModes.LineNumber:
                    Line.LineNumber = int.Parse(s);
                    break;
                case SymbolModes.Whitespace:
                default:
                    break;
            }

            if (ns.Value != null)
                Line.Symbols.Add(ns);
            mode = SymbolModes.None;
        }

        private string ParseExpressoin()
        {
            DiscardWhiteSpace();
            StringBuilder s = new StringBuilder();
            while (!EndOfStatement)
            {
                char c = Read();

            }
            return "";
        }

        /// <summary>
        /// Reads to the end of the statement. No arguments are allowed, so any values in this space
        /// should trigger a syntax error.
        /// </summary>
        private void ReadNoArgs()
        {
            while (!EndOfLine)
            {
                char c = Read();
                if (c == ':')
                {
                    return;
                }

                if (c > ' ')
                {
                    throw new BasicException("Syntax Error", Line.LineNumber, Pos, "No arguments allowed");
                }
            }
        }

        /// <summary>
        /// Discards all text to the end of the current statement. 
        /// <para>Reading ends at a colon (:) or end of line. </para>
        /// <para>If a colon is found, the colon is discarded and Pos will be on the character following the colon.</para>
        /// </summary>
        /// <returns></returns>
        private void SkipToEnd()
        {
            char c = Read();
            while (!EndOfStatement)
            {
                c = Read();
            }
        }
    }
}
