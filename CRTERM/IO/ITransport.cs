using System;
namespace CRTerm.IO
{
    public interface ITransport : IConfigurable, IReceiveChannel, IHasStatus
    {
        void Connect();
        void Disconnect();
        string Address { get; set; }
        void SendByte(byte Data);
        void SendBytes(byte[] Data);
        void UpdateStatus();
    }
}
