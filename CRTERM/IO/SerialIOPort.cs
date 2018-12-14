#define EVENT_DRIVEN

using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace CRTerm.IO
{
    class SerialIOPort : ITransport
    {
        #region Fields
        volatile private bool ThreadDone = false;
#if EVENT_DRIVEN
#else
        private Thread ReadThread;
#endif
        protected System.IO.Ports.SerialPort port = null;
        public int BytesReceived = 0;
        public int CharacterDelay = 0;
        public int LineDelay = 0;
        private ConnectionStatusCodes status = ConnectionStatusCodes.Disconnected;
        protected string _address = "COM1";
        private int _baudRate;
        #endregion

        #region Events
        public event StatusChangeEventHandler StatusChangedEvent;
        public event DataReadyEventHandler DataReceived;
        #endregion

        #region Properties
        public string Name { get; }
        public bool ClearToSend
        {
            get
            {
                return (port != null && port.IsOpen && port.CtsHolding);
            }
        }
        public int BytesAvailable
        {
            get
            {
                return port.BytesToRead;
            }
        }

        [ConfigItem]
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        [ConfigItem]
        public int BaudRate
        {
            get => _baudRate;
            set
            {
                _baudRate = value;
                if (Port != null)
                    Port.BaudRate = value;
            }
        }

        public bool Opened
        {
            get
            {
                if (Port != null)
                    return Port.IsOpen;
                else
                    return false;
            }
        }

        public SerialPort Port
        {
            get
            {
                return this.port;
            }

            protected set
            {
                this.port = value;
            }
        }

        bool DataWaiting
        {
            get
            {
                if (Port != null && (Port.BytesToRead > 6))
                    return true;
                else
                    return false;
            }
        }

        public virtual ConnectionStatusCodes Status
        {
            get
            {
                return this.status;
            }

            protected set
            {
                this.status = value;
                UpdateStatus();
                System.Windows.Forms.Application.DoEvents();
            }
        }

        #endregion

        #region Constructors
        public SerialIOPort()
        {
        }
        #endregion

        #region private and protected methods
        #endregion

        #region public methods
        public static string[] ListPortNames()
        {
            return SerialPort.GetPortNames();
        }

        public void Connect()
        {
            Status = ConnectionStatusCodes.Opening;
            Open();
            if (Port.IsOpen)
                Status = ConnectionStatusCodes.Connected;
        }

        public void Disconnect()
        {
            Status = ConnectionStatusCodes.Disconnecting;
            Port.Close();
            Status = ConnectionStatusCodes.Disconnected;
        }

        public void Open()
        {
            if (Port == null)
                Port = new System.IO.Ports.SerialPort();
            else
                Port.Close();

            Port.PortName = Address;
            Port.BaudRate = BaudRate;
            Port.Parity = System.IO.Ports.Parity.None;
            Port.DataBits = 8;
            Port.StopBits = System.IO.Ports.StopBits.One;
            Port.DtrEnable = true;
            Port.RtsEnable = true;
            //Port.DiscardNull = false;

            try
            {
                Port.Open();
#if EVENT_DRIVEN
                Port.DataReceived += Port_DataReceived;
#else
                ReadThread = new Thread(ReadThreadMain);
                ReadThread.Start();
#endif
                Status = ConnectionStatusCodes.Connected;
            }
            catch (Exception ex)
            {
                Status = ConnectionStatusCodes.Disconnected;

                // if the port can't be opened, allow the program to run
                // but with no data communication. However, the main form should handle this
                throw new CRTException("COM Port not available: " + Address + "\n" +
                  "Edit the Application config file to set the correct COM port\n" +
                  ex.Message);
            }
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            DataReceived?.Invoke(this);
        }

        internal void Close()
        {
            ThreadDone = true;
#if ! EVENT_DRIVEN
            ReadThread.Join();
#endif
            Port.DataReceived -= Port_DataReceived;
            Port.Close();
        }

        public void ReadThreadMain()
        {
            while (ThreadDone == false)
            {
                while (DataWaiting && ThreadDone == false)
                    Port_DataReceived(null, null);
                System.Threading.Thread.Sleep(100);
            }
        }

        public virtual void Send(byte Data)
        {
            Send(new byte[] { Data });
        }

        public virtual void Send(byte[] Data)
        {
            if (Port != null && Port.IsOpen)
            {
                Port.Write(Data, 0, Data.Length);
            }
        }

        public virtual void ReceiveData(IReceiveChannel dataChannel)
        {
            DataReceived?.Invoke(this);
        }

        public int BytesWaiting
        {
            get
            {
                return port.BytesToRead;
            }
        }

        public byte ReadByte()
        {
            return (byte)port.ReadByte();
        }

        public void UpdateStatus()
        {
            //StatusEventArgs eventArgs = new StatusEventArgs(this.status, this.StatusDetails);
            //StatusChangedEvent?.Invoke(this, eventArgs);
        }

        public int ReadData(byte[] Data, int Count)
        {
            throw new NotImplementedException();
        }

        public string StatusDetails
        {
            get
            {
                StringBuilder s = new StringBuilder();
                s.Append(Address + " " + BaudRate.ToString());
                if (port != null)
                {
                    s.Append(" " + port.DataBits.ToString());
                    s.Append(port.Parity.ToString()[0]);
                    switch (port.StopBits)
                    {
                        case StopBits.None:
                            s.Append("0");
                            break;
                        case StopBits.One:
                            s.Append("1");
                            break;
                        case StopBits.OnePointFive:
                            s.Append("1.5");
                            break;
                        case StopBits.Two:
                            s.Append("2");
                            break;
                        default:
                            break;
                    }
                }
                return s.ToString();
            }
        }

        #endregion
    }

}