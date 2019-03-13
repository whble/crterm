using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRBasic
{
    public class BasicException : Exception
    {
        string Details = "";
        int LineNumber = -1;
        int ErrorPos = -1;

        public BasicException(string Message, int LineNumber, int ErrorPos, string Details)
            : base(Message)
        {
            this.LineNumber = LineNumber;
            this.ErrorPos = ErrorPos;
            this.Details = Details;
        }

        public BasicException(string Message, string Details = "") : base(Message)
        {
            this.Details = "";
        }

        public BasicException(Exception ex, string Message)
            : base(ex.Message, ex)
        {
            this.Details = Message;
        }

        public BasicException(string Message, int LineNumber)
            : base(Message)
        {
            this.LineNumber = LineNumber;
        }

        public BasicException(string Message, int LineNumber, int ErrorPos)
            : base(Message)
        {
            this.LineNumber = LineNumber;
            this.ErrorPos = ErrorPos;
        }

    }
}
