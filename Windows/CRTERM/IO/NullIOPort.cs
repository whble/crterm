using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRTerm.Config;
using TerminalUI;

namespace CRTerm.IO
{
    /// <summary>
    /// Ports carry data in and out of CRTERM. All data is encoded as byte arrays
    /// and must be translated to character data by the Terminal Emulator.
    /// </summary>
    public class NullIOPort : ITransport, IConfigurable, IReceiveChannel, ISendChannel, IHasStatus
    {
        [ConfigItem]
        public virtual string Address { get; set; }
        public virtual string Name
        {
            get
            {
                return "Null";
            }
        }
        public bool Paused { get; set; }
        ConnectionStatusCodes status = ConnectionStatusCodes.Disconnected;
        public ConnectionStatusCodes Status
        {
            get { return status; }
            protected set
            {
                status = value;
                UpdateStatus();
            }
        }

        public virtual string StatusDetails
        {
            get
            {
                return "";
            }
        }
        private bool localEcho;
        protected RingBuffer receiveBuffer = new RingBuffer();

        public event StatusChangeEventHandler StatusChangedEvent;
        public event DataReadyEventHandler DataReceived;

        protected bool LocalEcho
        {
            get
            {
                return this.localEcho;
            }

            set
            {
                this.localEcho = value;
                UpdateStatus();
            }
        }

        public virtual void Connect()
        {
            Status = ConnectionStatusCodes.Connected;
        }

        public virtual void Disconnect()
        {
            Status = ConnectionStatusCodes.Disconnected;
        }

        public byte ReadByte()
        {
            return receiveBuffer.Read();
        }

        public bool SendAvailable()
        {
            return true;
        }

        public virtual void Send(byte Data)
        {
            receiveBuffer.Add(Data);
            DataReceived?.Invoke(this);
        }

        public virtual void Send(byte[] Data)
        {
            foreach (byte b in Data)
            {
                Send(b);
            }
        }

        public virtual void ReceiveData(IReceiveChannel dataChannel)
        {
            DataReceived?.Invoke(this);
        }

        public void UpdateStatus()
        {
            StatusEventArgs eventArgs = new StatusEventArgs(this.status, this.StatusDetails);
            StatusChangedEvent?.Invoke(this, eventArgs);
        }

        public int ReadData(byte[] Data, int Count)
        {
            return receiveBuffer.Read(Data, Count);
        }

        public int BytesAvailable
        {
            get
            {
                return receiveBuffer.Count;
            }
        }

        public virtual int BytesWaiting
        {
            get
            {
                return receiveBuffer.Count;
            }
        }

        public virtual bool ClearToSend
        {
            get
            {
                return (receiveBuffer.Count < receiveBuffer.Capacity);
            }
        }

    }
}
