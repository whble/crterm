using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRTerm.Transfer
{
    public interface ITransferProtocol 
    {
        Session CurrentSession { get; set; }
        void Send();
        void Receive();
        void Cancel();
        void ReceiveData(IBuffered receiver);
    }
}
