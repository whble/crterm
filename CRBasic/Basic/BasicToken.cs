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
        public delegate void CommandDelegate(BasicProgram Program, ProgramStep Step);
        public event CommandDelegate ExecuteMethod;

        public BasicToken(string Name, UInt16 Code)
        {
        }

        public BasicToken(string Name, UInt16 Code, BasicToken.CommandDelegate Command)
        {
            this.Name = Name;
            this.Code = Code;
            this.ExecuteMethod += Command;
        }

        public void Execute(BasicProgram Program, ProgramStep Step)
        {
            ExecuteMethod?.Invoke(Program, Step);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

}
