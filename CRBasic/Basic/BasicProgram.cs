using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRBasic.Basic
{
    public class BasicProgram
    {
        public TerminalUI.DisplayControl Display = null;
        public BasicTokens Commands = new BasicTokens();
        public SortedList<int, ProgramLine> Lines = new SortedList<int, ProgramLine>();
        public SortedList<string, BasicVariable> Variables = new SortedList<string, BasicVariable>();
        public Stack<BasicVariable> Stack = new Stack<BasicVariable>();

        public void Add(ProgramLine pl)
        {
            Lines.Add(pl.LineNumber, pl);
        }

        public void List(ProgramStep step)
        {
            step.Next();

            foreach (int ln in Lines.Keys)
            {
                // always print a space after the line number
                BasicSymbol.Padding lastPad = BasicSymbol.Padding.Required;
                ProgramLine line = Lines[ln];
                Display.Print(line.LineNumber.ToString().PadLeft(5));
                for (int i = 0; i < line.Symbols.Count; i++)
                {
                    BasicSymbol b = line.Symbols[i];

                    if (lastPad == BasicSymbol.Padding.Required || b.PadSpace == BasicSymbol.Padding.Required)
                        Display.Print(" ");
                    else if (lastPad == BasicSymbol.Padding.Allowed && b.PadSpace == BasicSymbol.Padding.Allowed)
                        Display.Print(" ");
                    else
                        Display.Print(" ");
                    lastPad = b.PadSpace;

                    Display.Print(b.ListText());
                    //Display.Print("{" + b.DataType + ":" + b.ToString() + "}");
                }
                Display.PrintLine();
            }
        }

        public void Run(ProgramLine pl = null)
        {
            Clear();
            if (Lines.Count == 0)
                return;

            ProgramLine line = Lines.Values[0];
            while (line != null)
            {
                ExecuteLine(line);
                line = GetNextLine(line);
            }
        }

        /// <summary>
        /// Returns the line number of the next line in the program.
        /// Returns null when the program has reached END or there are no more lines. 
        /// </summary>
        /// <param name="thisLine"></param>
        /// <returns></returns>
        private ProgramLine GetNextLine(ProgramLine line)
        {
            int i = Lines.Keys.IndexOf(line.LineNumber) + 1;
            if (i < Lines.Keys.Count)
                return Lines[Lines.Keys[i]];
            return null;
        }

        /// <summary>
        /// Delete all variables and reset the stack
        /// </summary>
        private void Clear()
        {
            Variables.Clear();
            Stack.Clear();
        }

        /// <summary>
        /// Executes a line of BASIC code
        /// </summary>
        /// <param name="Line"></param>
        public void ExecuteLine(ProgramLine Line)
        {
            // the first symbol must be a variable or token
            // tokens are executed with the provided parameters. Variables are assigned
            // based on the expression after the =
            ProgramStep step = new ProgramStep(Line);
            while (step.Pos < step.Symbols.Count)
            {
                BasicSymbol s = step.CurrentSymbol;
                switch (s.DataType)
                {
                    // execute a token
                    case DataTypes.Command:
                        ExecuteToken(step);
                        break;
                    // assign a varaible
                    case DataTypes.Text:
                        SetVariable(step);
                        break;
                    default:
                        break;
                }
            }
        }

        private void SetVariable(ProgramStep step)
        {
            BasicVariable var = new BasicVariable
            {
                Name = step.CurrentSymbol.ToString()
            };

            step.Next();
            if (step.CurrentSymbol.ToString() != "=")
                throw new BasicException("Syntax Error", step.LineNumber, step.Pos, "Expected =");

            step.Next();
            var.Value = Evaluate(step);
        }

        /// <summary>
        /// Evaluate a BASIC expression and return the result; 
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        private BasicSymbol Evaluate(ProgramStep step, int startPos = -1)
        {
            if (startPos >= 0)
                step.Pos = startPos;
            else
                startPos = step.Pos;

            step.Operation = null;
            step.LValue = null;
            step.RValue = null;

            bool done = false;
            while (! step.EndOfStatement)
            {
                if (step.CurrentSymbol.DataType == DataTypes.Delimiter)
                {
                    return step.Symbols[startPos];
                }
                else if (step.CurrentSymbol.DataType == DataTypes.Operator || step.EndOfStatement)
                {
                    if (step.Operation == null)
                        step.Operation = step.CurrentSymbol.Value as BasicOperator;
                    else
                    {
                        BasicOperator n = step.CurrentSymbol.Value as BasicOperator;
                        if (n != null && n.Precedence > step.Operation.Precedence)
                        {
                            step.LValue = step.RValue;
                            step.RValue = null;
                            step.Operation = step.CurrentSymbol.Value as BasicOperator;
                        }
                        else
                        {
                            EvaluateOperator(step);
                            step.Pos = startPos;
                            continue;
                        }
                    }
                }
                else
                {
                    if (step.LValue == null)
                    {
                        step.LValue = step.CurrentSymbol;
                    }
                    else if (step.RValue == null)
                        step.RValue = step.CurrentSymbol;
                    else
                        throw new BasicException("Syntax Error", step.LineNumber, step.Pos, "Operator expected");
                }

                step.Next();
                if (step.EndOfStatement && step.Operation != null)
                {
                    EvaluateOperator(step);
                    step.Pos = startPos;
                    continue;
                }
            }

            return step.LValue;
        }


        public void EvaluateOperator(ProgramStep step)
        {
            if (step.LValue == null || step.RValue == null || step.Operation == null)
                throw new BasicException("Syntax error", step.LineNumber, step.Pos, "Missing operator or operand");

            step.Operation.Execute(this, step);
            step.Pos -= 3;
            step.Symbols.RemoveRange(step.Pos, 2);
            step.CurrentSymbol = step.Result;
            step.LValue = null;
            step.RValue = null;
            step.Operation = null;
        }

        /// <summary>
        /// Executes the BASIC token. step.Pos should be the first argument of the command.
        /// </summary>
        /// <param name="step"></param>
        private void ExecuteToken(ProgramStep step)
        {
            if (step.CurrentSymbol == null)
                throw new BasicException("ExecuteToken() Invalid position", step.LineNumber, step.Pos, "Count=" + step.Symbols.Count);

            BasicToken t = step.CurrentSymbol.Value as BasicToken;
            if (t == null)
                throw new BasicException("ExecuteToken() symbol not token: " + step.CurrentSymbol.ToString(), step.LineNumber, step.Pos, "Type=" + step.CurrentSymbol.GetType().ToString());

            t.Execute(this, step);
        }

        internal void Print(ProgramStep step)
        {
            bool suppressNewline = false;
            step.Next();
            while (!step.EndOfStatement)
            {
                suppressNewline = false;
                switch (step.CurrentSymbol.DataType)
                {
                    case DataTypes.Delimiter:
                        if (step.CurrentSymbol.ToString() == ";")
                            suppressNewline = true;
                        else if (step.CurrentSymbol.ToString() == ",")
                            Display.CurrentColumn = (Display.CurrentColumn + 10) % 10;
                        step.Next();
                        break;
                    default:
                        BasicValue val = Evaluate(step);
                        Display.Print(val.ToString());
                        break;
                }
            }
            if (!suppressNewline)
                Display.PrintLine();
        }
    }
}
