using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRTERM.Common
{
  public class TerminalException : Exception
  {
    public TerminalException(string Message) : base(Message) { }
    public TerminalException(string Message, Exception InnerException) : base(Message, InnerException) { }
  }
}
