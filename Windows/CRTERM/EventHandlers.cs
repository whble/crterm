using System;
using System.Text;
using System.Windows.Forms;

namespace CRTerm
{
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

    public delegate void DataReadyEventHandler(IReceiveChannel receiver);
}