using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRBasic.Basic
{
    public class BasicToken
    {
        public string Name;
        public UInt16 Code;
        public delegate void CommandDelegate(BasicProgram Program, ProgramLine SourceLine);
        public event CommandDelegate Translate;

        public BasicToken(string Name, UInt16 Code)
        {
        }

        public BasicToken(string Name, UInt16 Code, BasicToken.CommandDelegate Command)
        {
            this.Name = Name;
            this.Code = Code;
            this.Translate += Command;
        }

        public void Execute(BasicProgram Program, ProgramLine Step)
        {
            Translate?.Invoke(Program, Step);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

}
