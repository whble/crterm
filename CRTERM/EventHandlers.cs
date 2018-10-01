using System;
using System.Windows.Forms;

namespace CRTerm
{
    public class TerminalKeyEventArgs : System.Windows.Forms.KeyPressEventArgs
    {
        public Keys Modifiers;
        public TerminalKeyEventArgs(char KeyChar, Keys Modifiers = Keys.None) : base(KeyChar)
        {
            this.Modifiers = Modifiers;
        }
    }
    public delegate void KeyPressEventHandler(IFrameBuffer frameBuffer, TerminalKeyEventArgs e);

    public class StatusEventArgs : EventArgs
    {
        public StatusEventArgs(ConnectionStatusCodes newStatus)
        {
            this.Status = newStatus;
            StatusDetails = newStatus.ToString();
        }
        public StatusEventArgs(ConnectionStatusCodes newStatus, string Details)
        {
            this.Status = newStatus;
            StatusDetails = Details;
        }
        public ConnectionStatusCodes Status;
        public string StatusDetails;
    }
    public delegate void StatusChangeEventHandler(IHasStatus dataChannel, StatusEventArgs e);

    public delegate void DataReadyEventHandler(IBuffered receiver);
}