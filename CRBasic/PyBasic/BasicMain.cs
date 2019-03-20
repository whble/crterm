using System;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalUI;
using System.IO;

namespace CRBasic.PyBasic
{
    public class BasicMain : IInterpreter
    {
        public DisplayControl Display { get; set; }
        ScriptEngine engine = null;
        BasicProgram Program = new BasicProgram();
        BasicParser Parser = new BasicParser();
        string PythonText = null;

        public enum RunStates
        {
            Stopped = 0,
            Running,
        }

        public int Pos = 0;

        public BasicLine CurrentLine = null;
        public BasicLine NextLine = null;
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
            Print(GetUnits(FreeBytes()) + "bytes", true);
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
            string pyText = Program.Translate();
            ExecPython(pyText);
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

        private void PrintErrorState(BasicException basicException)
        {
            ErrorState = basicException;
            PrintErrorState();
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
            string s = ProgramText.Trim();
            if (s.Length <= 0)
                return;
            if (s[0] < '0' || s[0] > '9')
                throw new BasicException("Immediate statement encountered in AddLine", ProgramText);

            Execute(ProgramText);
        }

        public void Execute(string Line)
        {
            //Print("Executing \"" + Line + "\"");
            try
            {
                ErrorState = null;
                BasicLine pl = Parser.Parse(Line);
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

        private void Execute(BasicLine pl)
        {
            try
            {
                string s = pl.Translate().Trim();
                if (s.ToUpper().StartsWith("$PLIST"))
                    Program.PList();
                else if (s.ToUpper().StartsWith("$LIST"))
                    Program.List();
                if (s.ToUpper().StartsWith("$RUN"))
                    Run();
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

        public void ExecPython(string PythonText)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);

            if (engine == null)
                engine = Python.CreateEngine();
            engine.Runtime.IO.SetOutput(stream, writer);
            try
            {
                engine.Execute(PythonText);
            }
            catch (Exception ex)
            {
                Display.PrintLine(ex.Message);
            }

            stream.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(stream);
            while (!reader.EndOfStream)
            {
                string s = reader.ReadLine();
                Display.Terminal.PrintLine(s);
            }
        }

    }
}

