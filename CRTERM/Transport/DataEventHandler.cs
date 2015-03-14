using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRTERM.Transport
{
  public class DataEventArgs : EventArgs
  {
    public byte[] Data;
    public string StatusMessage;
  }
  public delegate void DataEventHandler(TransportBase sender, DataEventArgs e);
}
