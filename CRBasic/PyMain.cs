using System;
using System.Collections.Generic;
using TerminalUI;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.IO;

namespace CRBasic
{
    internal class PyMain : IInterpreter
    {
        ScriptEngine engine = null;
        TerminalUI.DisplayControl display = null;

        public List<string> ProgramText
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public DisplayControl Display
        {
            get
            {
                return this.display;
            }

            set
            {
                this.display = value;
            }
        }

        public void Execute(string Line)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);

            if (engine == null)
                engine = Python.CreateEngine();
            engine.Runtime.IO.SetOutput(stream, writer);
            engine.Execute(Line);

            stream.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(stream);
            while (!reader.EndOfStream)
            {
                string s = reader.ReadLine();
                Display.Terminal.PrintLine(s);
            }
        }

        public void Init()
        {
            var engine = Python.CreateEngine();

            Cls();
            //Print(BasicMain.StartupMessage);
            Execute(@"print('CR Python with IronPython')");
            Execute("print('(C) 2019 Tom P. Wilson and Compiled Reality')");
            Print(Free().ToString(), true);
            Print("MB free");
            Ok();
        }

        public void Run()
        {
            throw new NotImplementedException();
        }

        public void Ok()
        {
            Print("Python Ready");
        }

        public int Free()
        {
            System.Diagnostics.PerformanceCounter performance = new System.Diagnostics.PerformanceCounter("Memory", "Available MBytes");
            float memory = performance.NextValue();
            return (int)memory;
        }

        public void Print(string s, bool SuppressNewline = false)
        {
            if (Display == null || Display.Terminal == null)
            {
                return;
            }

            Display.Terminal.Print(s);
            if (!SuppressNewline)
            {
                Display.PrintNewLine();
            }
        }

        public void Cls()
        {
            if (Display == null)
            {
                return;
            }

            Display.Clear();
        }

    }
}
