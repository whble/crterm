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
            Print("(C)2019 Tom Wilson aka \"tomxp41\"");
            Print(GetUnits(TotalBytes()) + "byte system");
            Print("Screen Mode: " + Display.ModeText);
            Display.CurrentTextColor = CharacterCell.ColorCodes.White;
            Print(GetUnits(FreeBytes()) + "bytes",true);
            Display.CurrentTextColor = CharacterCell.ColorCodes.Gray;
            Print(" free");
            Ok();
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

        private void Print()
        {
            Display.PrintLine();
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

        private void Execute(ProgramLine pl)
        {
            try
            {
                Program.ExecuteLine(pl);
            }
            catch (BasicException ex)
            {
                PrintErrorState(ex);
            }
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
                PrintErrorState(ex);
                Display.PrintLine(ex.Message);
            }
        }

        private void PrintErrorState(Exception ex)
        {

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


        public void Run(string Filename)
        {
            Load(Filename);
            if (Program.Lines.Count > 0)
                Run();
        }

        public void Load(string Filename)
        {
            try
            {
                if (!System.IO.File.Exists(Filename))
                {
                    PrintErrorState(new BasicException("File Not Found", Filename));
                    return;
                }

                var r = System.IO.File.OpenText(Filename);
                while (!r.EndOfStream)
                {
                    string line = r.ReadLine();
                    Execute(line);
                }
                r.Close();
            }
            catch (Exception ex)
            {
                PrintErrorState(new BasicException(ex, "Error loading program"));
            }
        }

        private void PrintErrorState(BasicException ex)
        {
            ErrorState = ex as  BasicException;
            Print(ex.Message, true);
            if (CurrentLine != null)
            {
                Print("in line " + CurrentLine, true);
            }
            Print();
            Stop();
        }

    }
}

