using System;
namespace CRTerm.Ports
{
    public interface IPort
    {
        void Connect();
        event NullPort.DataReceivedEvent DataReceived;
        void Disconnect();
        string Name { get; }
        void OnStatusChanged(CRTerm.ConnectionStatusCodes NewStatus);
        CRTerm.ParameterList Parameters { get; }
        void SendData(byte[] Data);
        void SendByte(byte Data);
        CRTerm.ConnectionStatusCodes Status { get; }
        event NullPort.StatusChangedEvent StatusChanged;
        string Address { get; }
    }
}
