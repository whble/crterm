using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TerminalUI;
using TerminalUI.Terminals;

namespace CRTerm.IO
{
    /// <summary>
    /// Test Port used to test terminal and framebuffer functions. 
    /// Depending on the command sent, this will send back various test patterns and text strings.
    /// </summary>
    public class TestPort : IO.NullIOPort
    {
        public override string Name
        {
            get
            {
                return "Test";
            }
        }

        public override string StatusDetails
        {
            get
            {
                return "Loopback 115200";
            }
        }

        public override void Connect()
        {
            this.Status = ConnectionStatusCodes.Opening;
            System.Windows.Forms.Application.DoEvents();
            ReceiveText("AT S0=0 Q0 E1 X4\r\n");
            ReceiveText("OK\r\n");
            ReceiveText("ATDT 555-1212\r\n");
            this.Status = ConnectionStatusCodes.Connected;
            ReceiveText("CONNECTED 19200\r\n");
            ReceiveText("Welcome to the Null Zone BBS\r\n");
            ReceiveText(": ");
        }

        public override void Disconnect()
        {

            ReceiveText("+++\r\n");
            ReceiveText("ATH\r\n");
            ReceiveText("LOST CARRIER\r\n");
            this.Status = ConnectionStatusCodes.Disconnected;
        }

        protected void ReceiveText(string Text)
        {
            byte[] data = BasicTerminal.GetBytes(Text);
            receiveBuffer.Add(data);
            ReceiveData(this);
        }

        public override void Send(byte Data)
        {
            base.Send(Data);
            if (Data == '?')
                ShowMenu();
        }

        private void ShowMenu()
        {
            OperatingSystem os = Environment.OSVersion;
            Version ver = os.Version;

            ReceiveText("\xC");
            ReceiveText("THE NULL ZONE\r\n");
            ReceiveText("\r\n");
            ReceiveText("OSVersion=" + os.ToString() + "\r\n");
            ReceiveText("Running on " + ver.ToString() + "\r\n");
            ReceiveText("\r\n");

            ReceiveText("[M]essages\r\n");
            ReceiveText("[F]iles\r\n");
            ReceiveText("[D]oor games\r\n");
            ReceiveText("[E]mail\r\n");
            ReceiveText("[G]oodbye\r\n");
            ReceiveText(": ");
        }
    }
}
