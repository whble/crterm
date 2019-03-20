using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRBasic.Basic
{
    public class BasicOperator : BasicToken
    {
        //public string Name;
        public int Precedence;
        //public delegate BasicValue EvaluateDelegate(BasicValue LValue, BasicValue RValue);
        //public event EvaluateDelegate EvaluateMethod;

        //public BasicOperator(string Name, int Precedence, EvaluateDelegate EvaluateMethod)
        //{
        //    this.Name = Name;
        //    this.Precedence = Precedence;
        //    this.EvaluateMethod += EvaluateMethod;
        //}

        //public BasicValue Evaluate(BasicValue LValue, BasicValue RValue)
        //{
        //    if (EvaluateMethod == null)
        //        return null;

        //    return EvaluateMethod(LValue, RValue);
        //}

        public BasicOperator(string Name, int Precedence, ushort Code, CommandDelegate EvaluateMethod) : base(Name, Code, EvaluateMethod)
        {
            this.Precedence = Precedence;
        }
    }
}


