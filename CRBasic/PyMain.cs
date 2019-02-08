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
            try
            {
                engine.Execute(Line);
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

        public void Init()
        {
            var engine = Python.CreateEngine();

            Cls();
            //Print(BasicMain.StartupMessage);
            Execute(@"print('CR Python with IronPython')");
            Execute("print('(C) 2019 Tom P. Wilson and Compiled Reality')");
            Print(GetUnits(TotalMemory()) + "byte system");
            Print(GetUnits(FreeMemory()) + "bytes free");
            Ok();
        }

        public ulong FreeMemory()
        {
            ulong f = new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory;
            return f;
        }

        public ulong TotalMemory()
        {
            ulong f = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory;
            return f;
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
                Display.PrintLine();
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

        public void AddLine(string ProgramLine)
        {
            throw new NotImplementedException();
        }
    }
}
