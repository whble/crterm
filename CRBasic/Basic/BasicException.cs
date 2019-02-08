using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRBasic.Basic
{
    public class BasicException : Exception
    {
        public BasicException(string Message)
            : base(Message)
        { }
        public BasicException(string Message, int LineNumber)
            : base(Message + " in " + LineNumber)
        { }
        public BasicException(string Message, int LineNumber, int ErrorPos)
            : base(Message + " in " + LineNumber + "/" + ErrorPos)
        {
        }
        public BasicException(string Message, int LineNumber, int ErrorPos, string Details)
            : base(Message + " in " + LineNumber + "/" + ErrorPos + "\r\n" + Details)
        {
        }
    }
}
