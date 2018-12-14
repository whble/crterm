using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace CRTerm.IO
{
    public class TCP : ITransport
    {
        protected Socket socket;
        protected RingBuffer ReceiveBuffer = new RingBuffer(16384);
        public event DataReadyEventHandler DataReceived;
        public event StatusChangeEventHandler StatusChangedEvent;
        private string _address;
        private int _port;

        public class StateObject
        {
            public Socket workSocket = null;
            public const int BUFFER_SIZE = 2048;
            public byte[] buffer = new byte[BUFFER_SIZE];
            public TCP Transport;
        }

        public TCP()
        {
            Address = "host.server.com";
            PortNumber = 23;
        }

        public string Name
        {
            get
            {
                return "TCP Port";
            }
        }

        [ConfigItem]
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
            }
        }

        [ConfigItem]
        public int PortNumber
        {
            get { return _port; }
            set
            {
                _port = value;
            }
        }

        public void Connect()
        {
            Connect(Address, PortNumber);
        }

        protected virtual void Connect(string HostName, int PortNumber)
        {
            if (Connected)
                disconnect("Connecting to new host");
            Status = ConnectionStatusCodes.Opening;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            socket.Connect(HostName, PortNumber);
            if (Connected)
            {
                Status = ConnectionStatusCodes.Connected;
            }
        }

        private void BeginReceive()
        {
            try
            {
                StateObject so = new StateObject();
                so.workSocket = socket;
                so.Transport = this;
                socket.BeginReceive(so.buffer, 0, StateObject.BUFFER_SIZE, 0, new AsyncCallback(Read_Callback), so);
            }
            catch (Exception ex)
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(ex.Message);
                ReceiveBuffer.Add(data);
                throw;
            }
        }

        public static void Read_Callback(IAsyncResult ar)
        {
            StateObject so = ar.AsyncState as StateObject;
            Socket socket = so.workSocket;
            int bytesRead = socket.EndReceive(ar);
            for (int i = 0; i < bytesRead; ++i)
            {
                so.Transport.Parse(so.buffer[i]);
            }
            if (ar.IsCompleted)
                so.Transport.ReceiveData(so.Transport);

            if (!socket.Connected)
                so.Transport.disconnect("Remote System Disconnected.");
            else
                socket.BeginReceive(so.buffer, 0, StateObject.BUFFER_SIZE, 0, new AsyncCallback(Read_Callback), so);
        }

        virtual public void Parse(byte b)
        {
            this.ReceiveBuffer.Add(b);
        }

        public void Disconnect()
        {
            disconnect("Session Closed");
        }

        protected virtual void disconnect(string Message)
        {
            if (socket != null && socket.Connected)
            {
                socket.Disconnect(false);
                StatusDetails = Message;
                socket = null;
                UpdateStatus();
            }
        }

        public bool Connected
        {
            get
            {
                if (socket == null)
                    return false;
                return socket.Connected;
            }
        }

        public int BytesWaiting { get; }
        public ConnectionStatusCodes Status { get; set; }
        public string StatusDetails { get; set; }

        private byte[] singleByte = new byte[1];
        public void Send(byte Data)
        {
            singleByte[0] = Data;
            Send(singleByte);
        }

        public void Send(byte[] Data)
        {
            try
            {
                socket.Send(Data);
            }
            catch (Exception ex)
            {
                disconnect(ex.Message);
            }
        }

        public void UpdateStatus()
        {
            StatusEventArgs eventArgs = new StatusEventArgs(this.Status, this.StatusDetails);
            StatusChangedEvent?.Invoke(this, eventArgs);
        }

        public byte ReadByte()
        {
            throw new NotImplementedException();
        }

        public void ReceiveData(IReceiveChannel dataChannel)
        {
            DataReceived?.Invoke(this);
        }
    }
}
