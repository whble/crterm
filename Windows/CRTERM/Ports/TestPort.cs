using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRTerm.Ports
{
    /// <summary>
    /// Test Port used to test terminal and framebuffer functions. 
    /// Depending on the command sent, this will send back various test patterns and text strings.
    /// </summary>
    public class TestPort : Ports.NullPort
    {
        public override void Connect()
        {
            this.Status = ConnectionStatusCodes.OpeningPort;
            System.Windows.Forms.Application.DoEvents();
            this.Status = ConnectionStatusCodes.Connected;
            ReceiveText("CONNECTED 19200\r\n");
        }

        public override void Disconnect()
        {
            this.Status = ConnectionStatusCodes.Disconnected;

            ReceiveText("LOST CARRIER\r\n");
        }

        public override void SendData(byte[] Data)
        {
            OnDataReceived(Data);
            System.Windows.Forms.Application.DoEvents();
        }

        protected void ReceiveText(string Text)
        {
            byte[] data = Terminals.BasicTerminal.GetBytes(Text);
            OnDataReceived(data);
            System.Windows.Forms.Application.DoEvents();
        }
    }
}
