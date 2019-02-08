using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalUI;

namespace CRBasic.Basic
{
    public class BasicMain : IInterpreter
    {
        public DisplayControl Display { get; set; }

        BasicProgram Program = new BasicProgram();
        BasicParser Parser = new BasicParser();

        public enum RunStates
        {
            Stopped = 0,
            Running,
        }

        public int Pos = 0;

        public ProgramLine CurrentLine = null;
        public ProgramLine NextLine = null;
        public RunStates RunState = RunStates.Stopped;
        private BasicException _errorState = null;

        public void Init()
        {
            Program.Display = this.Display;

            Cls();
            Print("CR BASIC " + System.Windows.Forms.Application.ProductVersion.ToString());
            Print("(C)2019 Tom P. Wilson");
            Print(GetUnits(TotalBytes()) + "byte system");
            Display.CurrentTextColor = CharacterCell.ColorCodes.White;
            Print(GetUnits(FreeBytes()) + "bytes",true);
            Display.CurrentTextColor = CharacterCell.ColorCodes.Gray;
            Print(" free");
            Ok();

            //debug 
            AddLine("10 print \"Hello world\"");
            AddLine("20 end");
        }

        public void Ok()
        {
            Print("Ok");
        }

        public string GetUnits(ulong Value)
        {
            if (Value < 1)
                return ((int)Value).ToString();

            ulong[] magnitude = {
                1,
                (ulong) Math.Pow(2, 10),
                (ulong) Math.Pow(2, 20),
                (ulong) Math.Pow(2, 30),
                (ulong) Math.Pow(2, 40),
                (ulong) Math.Pow(2, 50),
            };
            string[] units = { "", "bytes", "kilo", "giga", "tera" };
            ulong v = Value;
            string u = "";

            for (int i = 1; i < magnitude.Length; i++)
            {
                if (Value < magnitude[i])
                {
                    v = ((ulong)(Value / magnitude[i - 1]));
                    u = units[i - 1];
                    break;
                }
            }

            return v.ToString() + " " + u;
        }

        public ulong FreeBytes()
        {
            ulong f = new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory;
            return f;
        }

        public ulong TotalBytes()
        {
            ulong f = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory;
            return f;
        }

        public void Print(string s, bool SuppressNewline = false)
        {
            if (Display == null)
                return;

            Display.Print(s);
            if (!SuppressNewline)
                Display.PrintLine();
        }

        public void Cls()
        {
            if (Display == null)
                return;

            Display.Clear();
        }

        public void Execute(string Line)
        {
            //Print("Executing \"" + Line + "\"");
            try
            {
                ErrorState = null;
                ProgramLine pl = Parser.Parse(Line);
                if (pl.IsImmediate)
                {
                    CurrentLine = null;
                    NextLine = null;
                    Execute(pl);
                    if (ErrorState != null)
                    {
                        PrintErrorState();
                    }
                }
                else if (Program.Lines.ContainsKey(pl.LineNumber) && pl.Symbols.Count > 0)
                {
                    Program.Lines[pl.LineNumber] = pl;
                }
                else if (Program.Lines.ContainsKey(pl.LineNumber) && pl.Symbols.Count == 0)
                {
                    Program.Lines.Remove(pl.LineNumber);
                }
                else
                    Program.Add(pl);
            }
            catch (Exception ex)
            {
                Display.PrintLine(ex.Message);
            }
        }

        private void PrintErrorState()
        {
            if (ErrorState == null)
                return;

            if (Display.CurrentColumn > 0)
                Display.PrintLine();

            Display.Print(ErrorState.Message);
            Display.PrintLine();
        }

        public void Run()
        {
            ErrorState = null;
            RunState = RunStates.Running;
            int lineNo = Program.Lines.Keys[0];
            Continue();
        }

        void Continue()
        {
            while (RunState == RunStates.Running && CurrentLine != null)
            {
                // get next line number
                int nl = Program.Lines.Keys.IndexOf(CurrentLine.LineNumber) + 1;
                NextLine = null;
                if (nl < Program.Lines.Count)
                    NextLine = Program.Lines[Program.Lines.Keys[nl]];
                Execute(CurrentLine);
                CurrentLine = NextLine;
            }
        }

        private void Stop()
        {
            this.RunState = RunStates.Stopped;
        }

        public BasicException ErrorState
        {
            get
            {
                return _errorState;
            }

            set
            {
                _errorState = value;
                if (value != null)
                {
                    Stop();
                }
            }
        }


        public void AddLine(string ProgramText)
        {
            Execute(ProgramText);
        }

        private void Execute(ProgramLine pl)
        {
            try
            {
                foreach (BasicSymbol b in pl.Symbols)
                {
                    switch (b.DataType)
                    {
                        case DataTypes.EndOfLine:
                            break;
                        case DataTypes.EndOfStatement:
                            break;
                        case DataTypes.String:
                            break;
                        case DataTypes.Integer:
                            break;
                        case DataTypes.Single:
                            break;
                        case DataTypes.Double:
                            break;
                        case DataTypes.Text:
                            break;
                        case DataTypes.Variable:
                            break;
                        case DataTypes.Token:
                            BasicToken t = b.Value as BasicToken;
                            BasicVariable result = new BasicVariable();
                            t.ExecuteCommand(Program, result, pl);
                            break;
                        case DataTypes.Delimiter:
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorState = ex as BasicException;
                Print(ex.Message, (CurrentLine != null));
                if (CurrentLine != null)
                {
                    Print("in line " + CurrentLine);
                }
                Stop();
            }
        }
    }
}

