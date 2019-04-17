using System;
using System.Collections.Generic;
using System.Text;

namespace CRBasic.Basic
{
    /// <summary>
    /// Read a line of text and tokenize the objects. 
    /// </summary>
    public class BasicParser
    {
        private enum SymbolTypes
        {
            None,
            Identifier,
            Number,
            LineNumber,
            Operator,
            Whitespace,
            Other,
            String,
            EndOfStatement,
            Separator
        }

        public SortedList<string, BasicLabel> Labels = new SortedList<string, BasicLabel>();
        public ProgramLine OutputLine = new ProgramLine();
        public StringBuffer InputLine = null;

        /// <summary>
        /// Reads a line of program text and returns a ProgramLine object
        /// </summary>
        /// <param name="BasicText"></param>
        /// <param name="LineNumber">Line number of the source file. The first line is 1. (This is not the BASIC line number, ie: 10 GOTO 10.) 
        /// Enter ProgramLine.LINENUMBER_IMMEDIATE for immediate statements</param>
        /// <returns></returns>
        public ProgramLine ParseLine(string BasicText)
        {
            OutputLine = new ProgramLine();
            InputLine = new StringBuffer(BasicText);
            if (IsDigit(InputLine.ThisChar))
            {
                ParseLineNumber(true);
            }

            while (!InputLine.EndOfLine)
            {
                ParseStatement();
            }
            return OutputLine;
        }

        private void ParseLineNumber(bool Declaration)
        {
            StringBuilder s = new StringBuilder();
            try
            {
                while (IsDigit(InputLine.ThisChar))
                    s.Append(InputLine.Next());
            }
            catch (Exception ex)
            {
                throw new BasicException(ex, "Parsing line number");
            }

            string ln = s.ToString().Trim();
            BasicLabel label;
            if (Labels.ContainsKey(ln))
                label = Labels[ln];
            else
            {
                label = new BasicLabel();
                label.Name = ln;
                if (!int.TryParse(ln, out label.LineNumber))
                    label.LineNumber = -1;
                Labels.Add(ln, label);
                OutputLine.LineNumber = label.LineNumber;
            }

            if (Declaration)
                label.FoundDeclaration = true;
            else
                label.FoundReference = true;
        }

        internal void Clear()
        {
            throw new NotImplementedException();
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
        private bool IsOperatorChar(char c)
        {
            return BasicTokens.OperatorChars.Contains(c);
        }

        /// <summary>
        /// The character is a comma or semicolon.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private bool IsSeparator(char c)
        {
            string s = c.ToString();
            if (BasicTokens.ListSeperators.Contains(s) || BasicTokens.PrintSeperators.Contains(s))
                return true;
            return false;
        }

        /// <summary>
        /// Tests if the character is an operator (+,-,*,/,^)
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private bool IsSigil(char c)
        {
            if (BasicTokens.Sigils.Contains(c.ToString()))
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
            while (!InputLine.EndOfStatement && IsWhitespace(InputLine.ThisChar))
            {
                InputLine.Next();
            }
        }

        /// <summary>
        /// Parse a statement. A statement is a word followed by 0 or more arguments and is 
        /// terminated by end of line or a colon (:). 
        /// An assignment is a variable, followed by =, followed by the value.
        /// <para>Examples</para>
        /// <para>PRINT "HELLO"</para>
        /// <para>A = 23</para>
        /// </summary>
        private void ParseStatement()
        {
            SkipSpace();
            OutputLine.Command = ReadWord();
            SkipSpace();
            if (InputLine.ThisChar == '=')
            {
                OutputLine.LValue = OutputLine.Command;
                OutputLine.Command = "=";
                InputLine.Next();
            }
            SkipSpace();
            OutputLine.Arguments = InputLine.ReadToEOS();
        }

        string ReadWord()
        {
            StringBuilder s = new StringBuilder();
            SymbolTypes mode = SymbolType(InputLine.ThisChar, SymbolTypes.None);
            SymbolTypes lastMode = mode;
            while (mode == lastMode)
            {
                s.Append(InputLine.ThisChar);
                InputLine.Next();
                lastMode = mode;
                mode = SymbolType(InputLine.ThisChar, lastMode);
            }
            return s.ToString();
        }

        private void SkipSpace()
        {
            while ((SymbolType(InputLine.ThisChar, SymbolTypes.None) == SymbolTypes.Whitespace) && !InputLine.EndOfStatement)
            {
                InputLine.Next();
            }
        }

        private SymbolTypes SymbolType(char c, SymbolTypes lastMode)
        {
            if (InputLine.InQuote)
                return SymbolTypes.String;
            else if (c == '"')
                return SymbolTypes.String;
            else if (lastMode == SymbolTypes.Identifier && IsSigil(c))
                return SymbolTypes.Identifier;
            else if (IsIdentifier(c, lastMode != SymbolTypes.Identifier))
                return SymbolTypes.Identifier;
            else if (IsDigit(c))
                return SymbolTypes.Number;
            else if (IsOperatorChar(c))
                return SymbolTypes.Operator;
            else if (IsWhitespace(c))
                return SymbolTypes.Whitespace;
            else if (IsSeparator(c))
                return SymbolTypes.Separator;
            else
                return SymbolTypes.Other;
        }

        private void AppendSymbol(StringBuilder Line, string Symbol, SymbolTypes Mode)
        {
            String ns = null;
            string su = Symbol.ToUpper();

            switch (Mode)
            {
                case SymbolTypes.None:
                    ns = " " + Symbol;
                    break;
                case SymbolTypes.String:
                    if (Symbol.Length > 2 && Symbol.StartsWith("\"") && Symbol.EndsWith("\""))
                        ns = Symbol;
                    else
                        ns = "\"" + Symbol + "\"";
                    break;
                case SymbolTypes.Separator:
                    ns = Symbol;
                    break;
                case SymbolTypes.Operator:
                    ns = su;
                    break;
                case SymbolTypes.Other:
                case SymbolTypes.Identifier:
                    ns = su;
                    break;
                case SymbolTypes.Number:
                    ns = su;
                    break;
                case SymbolTypes.Whitespace:
                default:
                    break;
            }

            if (!string.IsNullOrEmpty(ns))
                Line.Append(ns);
        }

        private string ParseExpressoin()
        {
            DiscardWhiteSpace();
            StringBuilder s = new StringBuilder();
            while (!InputLine.EndOfStatement)
            {
                char c = InputLine.Next();

            }
            return "";
        }

    }
}
