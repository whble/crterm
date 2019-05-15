using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRTerm.Transfer
{
    public interface ITransferProtocol 
    {
        Session CurrentSession { get; set; }
        void SendFile(Session CurrentSession, string Filename);
        void ReceiveFile(Session CurrentSession, string Filename);
        void Cancel();
        void ReceiveData(IBuffered receiver);
    }
}
