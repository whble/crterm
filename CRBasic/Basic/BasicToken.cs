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
        public delegate void CommandDelegate(BasicProgram Program, BasicVariable Result, ArgumentList Arguments);
        public event CommandDelegate RunDelegate;

        public BasicToken(string Name, UInt16 Code)
        {
        }

        public BasicToken(string Name, UInt16 Code, BasicToken.CommandDelegate Command)
        {
            this.Name = Name;
            this.Code = Code;
            this.RunDelegate += Command;
        }

        public void ExecuteCommand(BasicProgram ProgramState, BasicVariable Result, ArgumentList Arguments)
        {
            RunDelegate?.Invoke(ProgramState, Result, Arguments);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

}
