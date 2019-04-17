using System;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalUI;

namespace CRBasic.Basic
{
    public class BasicInterpreter : IInterpreter
    {
        BasicProgram Program = new BasicProgram();
        public DisplayControl Display { get; set; }
        BasicParser parser = new BasicParser();
        string ConvertedText = "";

        public void Init()
        {
        }

        public void AddLine(string Text)
        {
            ProgramLine pl = parser.ParseLine(Text);
            Program.Lines.Add(pl);
        }

        public void ExecuteStatement(string Line)
        {
            ProgramLine pl = parser.ParseLine(Line);
            if (!pl.IsImmediate)
                Program.Add(pl);
            else
            {
                switch (pl.Command.ToUpper())
                {
                    case "LIST":
                        List();
                        Ready();
                        break;
                    case "CONVERT":
                        Convert();
                        ListConverted();
                        Ready();
                        break;
                    case "RUN":
                        Run();
                        break;
                }
            }
        }

        public void Run(string StartLabel = "")
        {
        }

        public void List()
        {
            foreach (ProgramLine line in Program.Lines)
            {
                Display.PrintLine(line.ToString());
            }
        }

        public void Edit()
        {
        }

        public void Load(string Filespec)
        {
            try
            {
                if (!System.IO.File.Exists(Filespec))
                    throw new BasicException("File not found: " + Filespec);

                Program = new BasicProgram();
                Display.PrintLine("Loading " + System.IO.Path.GetFileName(Filespec));
                FileStream f = File.OpenRead(Filespec);
                StreamReader reader = new StreamReader(f);
                string line = reader.ReadLine();
                while (line != null)
                {
                    this.Program.Add(line);
                    line = reader.ReadLine();
                }
            }
            catch (BasicException ex)
            {
                PrintError(ex);
            }
        }

        private void PrintError(BasicException ex)
        {
            Display.PrintAtStart(ex.Message);
        }

        private void Ready()
        {
            Display.PrintLine("READY.");
        }

        /// <summary>
        /// Converts the BASIC source code to the target langauge.
        /// </summary>
        public void Convert()
        {
            int indent = 0;
            StringBuilder s = new StringBuilder();
            s.AppendLine("namespace CRBasic");
            AppendIndent(s, indent);
            s.AppendLine("{");
            AppendIndent(s, ++indent);
            s.AppendLine("public class UserCode");
            AppendIndent(s, indent);
            s.AppendLine("{");
            AppendIndent(s, ++indent);
            s.AppendLine("public void Main() {");
            ++indent;
            foreach (ProgramLine line in Program.Lines)
            {
                AppendIndent(s, indent);
                if (line.Command == "=")
                {
                    s.Append(line.LValue);
                    s.Append(" = ");
                    s.Append(line.Arguments);
                    s.Append(";");
                }
                else
                {
                    s.Append(line.Command);
                    s.Append("(");
                    s.Append(line.Arguments);
                    s.Append(");");
                }
                s.AppendLine("  // " + line.LineNumber.ToString());
            }
            AppendIndent(s, --indent);
            s.AppendLine("}");
            AppendIndent(s, --indent);
            s.AppendLine("}");
            AppendIndent(s, --indent);
            s.AppendLine("}");
            this.ConvertedText = s.ToString();
        }

        private void AppendIndent(StringBuilder S, int Level)
        {
            S.Append(new string(' ', Level * 4));
        }

        private void ListConverted()
        {
            Display.PrintLine(ConvertedText);
        }
    }
}

