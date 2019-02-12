using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRBasic.PyBasic
{
    public class BasicToken
    {
        public string Name;
        public UInt16 Code;
        public delegate void TranslateDelegate(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex);
        public event TranslateDelegate Translate;

        public int Index;

        public BasicToken(string Name, UInt16 Code)
        {
        }

        public BasicToken(string Name, UInt16 Code, BasicToken.TranslateDelegate Command)
        {
            this.Name = Name;
            this.Code = Code;
            this.Translate += Command;
        }

        public void TranslateCommand(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex)
        {
            Translate?.Invoke(PythonString, Token, Arguments, ref StartIndex);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

}
