using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRTerm.Ports
{
    /// <summary>
    /// Ports carry data in and out of CRTERM. All data is encoded as byte arrays
    /// and must be translated to character data by the Terminal Emulator.
    /// </summary>
    public class NullPort : CRTerm.Ports.IPort
    {
        private ConnectionStatusCodes _status = ConnectionStatusCodes.Disconnected;
        public ConnectionStatusCodes Status
        {
            get { return _status; }
            protected set { 
                _status = value;
                OnStatusChanged(value);
            }
        }

        private ParameterList _parameters = null;
        public ParameterList Parameters
        {
            get { return _parameters; }
        }

        public delegate void DataReceivedEvent(byte[] Data);
        public event DataReceivedEvent DataReceived;
        protected virtual void OnDataReceived(byte[] Data)
        {
            if(DataReceived == null)
                return;

            DataReceived(Data);
        }

        private string addressParameter = "Address";
        public virtual string Address
        {
            get { return Parameters.GetValue(addressParameter); }
        }

        public delegate void StatusChangedEvent(object Sender, ConnectionStatusCodes NewStatus);
        public event StatusChangedEvent StatusChanged;
        public void OnStatusChanged(ConnectionStatusCodes NewStatus)
        {
            if (StatusChanged == null)
                return;

            StatusChanged(this, NewStatus);
        }

        public string Name
        {
            get
            {
                return Parameters.GetValue("Name");
            }
            protected set
            {
                Parameters.SetValue("Name", value);
            }
        }

        public NullPort()
        {
            _parameters = new ParameterList(this);
            Parameters.SetValue("Address", "Test Address");
        }

        public virtual void Connect()
        {
        }

        public virtual void Disconnect()
        {
        }

        public virtual void SendData(byte[] Data)
        {
        }

        public virtual void SendByte(byte Data)
        {
            SendData(new byte[] { Data });
        }
    }
}
