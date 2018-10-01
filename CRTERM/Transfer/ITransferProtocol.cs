using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRTerm.Transfer
{
    public interface ITransferProtocol 
    {
        IO.ITransport Transport { get; set; }
        ITransferDialog Dialog { get; set; }
        void SendFile(string Filename);
        void ReceiveFile(string Filename);
        void Cancel();
        void ReceiveData(IBuffered transport);
    }
}
