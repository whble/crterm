using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRBasic.Basic
{
    /// <summary>
    /// Read a line of text and tokenize the objects. 
    /// </summary>
    public class BasicParser
    {
        public int Pos = 0;
        public string Text = "";
        ProgramLine Line = new ProgramLine();
        /// <summary>
        /// true when first character in symbol is a letter. 
        /// </summary>
        bool InQuote = false;
        int ParenLevel = 0;

        public ProgramLine Parse(string CommandText)
        {
            Pos = 0;
            this.InQuote = false;
            this.ParenLevel = 0;
            this.Line = new ProgramLine();
            this.Text = CommandText;

            if (IsDigit(ThisChar))
                ReadLineNumber();
            while (!EndOfLine)
            {
                ReadStatement();
            }
            return Line;
        }

        public void Parse(List<string> Lines)
        {
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
                    return true;
                if (ParenLevel > 0 && ThisChar == ':')
                    throw new BasicException("Syntax error", Line.LineNumber, Pos, ": is inside parentheses");
                if (!InQuote && ThisChar == ':')
                    return true;
                return false;
            }
        }

        char Read()
        {
            if (Pos >= Text.Length)
                return '\0';
            char c = Text[Pos++];
            if (c == '"')
                InQuote = !InQuote;
            if (!InQuote && c == '(')
                ParenLevel++;
            if (!InQuote && c == ')')
                ParenLevel--;
            return c;
        }

        char ThisChar
        {
            get
            {
                return Text[Pos];
            }
        }

        char NextChar
        {
            get
            {
                if (EndOfLine)
                    return '\0';
                return Text[Pos + 1];
            }
        }

        /// <summary>
        /// Tests whether the current letter is part of an identifier. 
        /// An identifier is a Letter followed by letters, digits, or Underscore. 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        bool IsWord(char c)
        {
            if ((c >= 'A' && c <= 'Z')
                || (c >= '0' && c <= '9')
                || (c >= 'a' && c <= 'z')
                || (c == '%')
                || (c == '$')
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
        bool IsOperator(char c)
        {
            if (BasicTokens.Operators.Contains(c.ToString()))
                return true;

            return false;
        }

        bool IsDigit(char c)
        {
            if (c >= '0' && c <= '9')
                return true;

            if (c == '.')
                return true;

            return false;
        }

        bool IsWhitespace(char c)
        {
            if (c <= ' ')
                return true;
            return false;
        }

        /// <summary>
        /// Reads the next word which may be an identifier, a number, or a string.
        /// Pos will be at the space immediately following the word.
        /// </summary>
        /// <returns></returns>
        string ReadWord()
        {
            StringBuilder s = new StringBuilder();

            DiscardWhiteSpace();
            if (EndOfStatement)
                return "";

            while (!EndOfStatement && IsWord(ThisChar))
            {
                s.Append(ThisChar);
                Read();
            }
            return s.ToString();
        }

        private string ReadNonWord()
        {
            StringBuilder s = new StringBuilder();

            DiscardWhiteSpace();
            if (EndOfStatement)
                return "";

            while (!EndOfStatement 
                && !IsWord(ThisChar)
                && !IsWhitespace(ThisChar))
            {
                s.Append(ThisChar);
                Read();
            }
            return s.ToString();
        }

        void ReadLineNumber()
        {
            Line.LineNumber = ProgramLine.LN_IMMEDIATE;
            int ln = ReadInt();
            if (ln >= 0)
                Line.LineNumber = ln;
        }

        private void DiscardWhiteSpace()
        {
            while (Pos < Text.Length && IsWhitespace(Text[Pos]))
                Pos++;
        }

        /// <summary>
        /// Reads a string at the current input position. 
        /// Pos must be set to the first character inside the string,
        /// after the opening quote.
        /// </summary>
        /// <returns></returns>
        private string ReadString()
        {
            StringBuilder s = new StringBuilder();
            while (Pos < Text.Length)
            {
                char c = Read();
                if (c == '"')
                {
                    // two quotes ("") are inserted into the string as a one quote (")
                    if (NextChar == '"')
                        s.Append('"');
                    else
                        break;
                }
                s.Append(c);
            }
            return s.ToString();
        }

        /// <summary>
        /// Reads an integer from the current line. 
        /// </summary>
        /// <returns></returns>
        private int ReadInt()
        {
            StringBuilder s = new StringBuilder();

            DiscardWhiteSpace();
            if (EndOfLine)
                return int.MinValue;

            while (!EndOfLine && IsDigit(ThisChar))
            {
                s.Append(Read());
            }

            return int.Parse(s.ToString());
        }

        /// <summary>
        /// Read a statement. A statement is a word followed by 0 or more arguments and is 
        /// terminated by end of line or a colon (:). 
        /// Pos will be placed at the end of the line or after the colon, if one is present.
        /// <para>The first word is compared to the command list in TokenDef. if it is a
        /// command, it will be tokenized.</para>
        /// <para>If the first word is followed by an =, this is a variable assignment, and
        /// a variable will be created if necessary.</para>
        /// <para>If the word is not a tokenized command and not a variable, it will be 
        /// checked against user-created subroutines.</para>
        /// <para>If none of the above, a syntax error is generated.</para>
        /// </summary>
        private void ReadStatement()
        {
            // get past the : if we ended up there, 
            // skip any whitespace before the command word
            while (EndOfStatement && !EndOfLine)
                Read();
            if (EndOfLine)
                return;

            string word = ReadWord().ToUpper();
            if (BasicTokens.Commands.ContainsKey(word))
            {
                BasicToken cmd = BasicTokens.Commands[word];
                Line.Add(cmd, DataTypes.Token);
            }
            else
            {
                DiscardWhiteSpace();
                char c = Read();
                if (c == '=')
                {
                    BasicToken cmd = BasicTokens.Commands["LET"];
                    Line.Add(cmd, DataTypes.Token);
                    Line.Add(word, DataTypes.Variable);
                }
                else if (c == ':')
                {
                    BasicToken cmd = BasicTokens.Commands["LET"];
                    Line.Add(cmd, DataTypes.Token);
                    Line.Add(word, DataTypes.Variable);
                    Line.Add(Line.LineNumber, DataTypes.Integer);
                }
                else
                {
                    throw new BasicException("Syntax Error",Line.LineNumber, Pos,"\"" + word + "\" is not a statement or assignment");
                }
            }
        }

        private void ReadArguments()
        {
            while (!EndOfStatement)
            {
                string s = ReadExpression();
                BasicSymbol b = new BasicSymbol();
                ConvertSymbol(b, s);
            }
        }

        private void ConvertSymbol(BasicSymbol b, string s)
        {
            throw new NotImplementedException();
        }

        private string ReadExpression()
        {

            DiscardWhiteSpace();
            while (!EndOfStatement)
            {
                if (IsWord(ThisChar))
                    return ReadWord();
                return ReadNonWord();
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
                    return;
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
