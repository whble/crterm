using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRBasic.Basic
{
    public class BasicOperator : BasicToken
    {
        public int Precedence;

        public BasicOperator(string Name, int Precedence, ushort Code, CommandDelegate TranslateMethod) : base(Name, Code, TranslateMethod)
        {
            this.Precedence = Precedence;
        }
    }
}


