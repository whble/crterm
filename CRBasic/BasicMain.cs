using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalUI;

namespace CRBasic
{
    public class BasicMain : IInterpreter
    {
        List<string> _programText = new List<string>();
        public DisplayControl Display { get; set; }

        public List<string> ProgramText
        {
            get
            {
                return _programText;
            }
        }

        public void Init()
        {
            Cls();
            Print("CR BASIC " + System.Windows.Forms.Application.ProductVersion.ToString());
            Print("(C)2019 Tom P. Wilson");
            Print(GetUnits(TotalBytes()) + "byte system");
            Print(GetUnits(FreeBytes()) + "bytes free");
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
            string[] units = { "", "bytes", "kilo", "giga", "tera"};
            ulong v = Value;
            string u = "";

            for (int i = 1; i < magnitude.Length; i++)
            {
                if (Value < magnitude[i])
                {
                    v = ((ulong)(Value / magnitude[i-1]));
                    u = units[i-1];
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
                Display.PrintNewLine();
        }

        public void Cls()
        {
            if (Display == null)
                return;

            Display.Clear();
        }

        public void Execute(string Line)
        {
            throw new NotImplementedException();
        }

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}
