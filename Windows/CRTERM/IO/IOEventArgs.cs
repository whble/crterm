using System;
using System.Collections.Generic;
using System.Text;

namespace CRTerm.IO
{
  public class PCREventArgs : EventArgs {
    public byte[] Data;
    public ConnectionStatusCodes Status;
  }
}
