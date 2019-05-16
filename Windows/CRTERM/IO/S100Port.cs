#define EVENT_DRIVEN

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CRTerm.IO
{
    class S100port : NullIOPort
    {
        volatile private bool ThreadDone = false;
#if EVENT_DRIVEN
#else
        private Thread ReadThread;
#endif
        private System.IO.Ports.SerialPort port = null;

        const int REQUEST_BYTE = 0x80;
        const int SEND_ASCII_LOWER = 0x81;
        const int SEND_ASCII_UPPER = 0x82;
        const int PACKET_END = 0xff;

        public int CharacterDelay = 0;
        public int LineDelay = 0;

        RingBuffer SendBuffer = new RingBuffer(2048);
        public bool ClearToSend = false;

        public S100port()
        {
        }

        protected string _address = "COM1";
        /// <summary>
        /// The serial port used to communicate with the PCR
        /// </summary>
        public override string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        private int _baudRate;
        public int BaudRate
        {
            get => _baudRate;
            set
            {
                _baudRate = value;
                if (port != null)
                    port.BaudRate = value;
            }
        }

        public bool Opened
        {
            get
            {
                if (port != null)
                    return port.IsOpen;
                else
                    return false;
            }
        }

        public override void Connect()
        {
            Status = ConnectionStatusCodes.OpeningPort;
            Open();
            if (port.IsOpen)
                Status = ConnectionStatusCodes.Connected;
        }

        public override void Disconnect()
        {
            Status = ConnectionStatusCodes.Disconnecting;
            if (port != null)
                port.Close();
            Status = ConnectionStatusCodes.Disconnected;
        }

        public void Open()
        {
            Disconnect();
            port = new System.IO.Ports.SerialPort(Address);
            port.BaudRate = BaudRate;
            port.Parity = System.IO.Ports.Parity.None;
            port.DataBits = 8;
            port.StopBits = System.IO.Ports.StopBits.One;
            port.DtrEnable = true;
            port.RtsEnable = true;
            port.Handshake = System.IO.Ports.Handshake.None;

            try
            {
                port.Open();
#if EVENT_DRIVEN
                port.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(port_DataReceived);
#else
                ReadThread = new Thread(ReadThreadMain);
                ReadThread.Start();
#endif
            }
            catch (Exception ex)
            {
                // if the port can't be opened, allow the program to run
                // but with no data communication. However, the main form should handle this
                throw new CRTException("COM Port not available: " + Address + "\n" +
                  "Edit the Application config file to set the correct COM port\n" +
                  ex.Message);
            }
        }

        internal void Close()
        {
            ThreadDone = true;
#if ! EVENT_DRIVEN
            ReadThread.Join();
#endif
            port.DataReceived -= port_DataReceived;
            port.Close();
        }

        public void ReadThreadMain()
        {
            while (ThreadDone == false)
            {
                while (DataWaiting() && ThreadDone == false)
                    port_DataReceived(null, null);
                System.Threading.Thread.Sleep(100);
            }
        }

        void port_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            System.IO.Ports.SerialPort p = sender as System.IO.Ports.SerialPort;
            if (p == null || !p.IsOpen)
                return;
            try
            {
                // Receive Packet:
                //   0: REQUEST_BYTE | SEND_ASCII_LOWER | SEND_ASCII_UPPER
                //   1: Port Number (0-255)
                //   2: Data
                //   3: PACKET_END
                int b = 0;
                byte[] RecvPacket = new byte[4];
                while (p.BytesToRead >= 4)
                {
                    b = p.ReadByte();
                    if (b == SEND_ASCII_LOWER || b == SEND_ASCII_UPPER)
                        RecvPacket[0] = (byte)b;
                    else if (b == REQUEST_BYTE)
                    {
                        System.Diagnostics.Debug.WriteLine("REQUEST_BYTE");
                        RecvPacket[0] = (byte)b;
                        ClearToSend = true;
                    }
                    else
                    {
                        if (b == 13)
                            System.Diagnostics.Debug.Write("\\r");
                        else if (b == 10)
                            System.Diagnostics.Debug.WriteLine("\\n");
                        else if (b < 32 || b > 126)
                            System.Diagnostics.Debug.Write(b.ToString("X2"));
                        else
                            System.Diagnostics.Debug.Write((char)b);
                        continue;
                    }

                    p.Read(RecvPacket, 1, 3);
                    if (RecvPacket[3] != 0xff)
                        continue;

                    if (RecvPacket[0] == SEND_ASCII_LOWER && RecvPacket[1] == 0)
                    {
                        Buffer.Add(RecvPacket[2]);
                        ReceiveChannel.ReadData(Buffer);
                    }
                    if (RecvPacket[0] == REQUEST_BYTE && RecvPacket[1] == 0)
                    {
                        SendFromBuffer();
                    }
                }
            }
            catch (Exception ex)
            {
                Buffer.Add(ASCIIEncoding.ASCII.GetBytes(ex.Message));
                ReceiveChannel.ReadData(Buffer);
                Disconnect();
            }
        }

        /// <summary>
        /// Check to see whether there's data ready to process.
        /// </summary>
        /// <returns></returns>
        bool DataWaiting()
        {
            if (port != null && (port.BytesToRead > 6))
                return true;
            else
                return false;
        }

        public override void SendBytes(byte[] Data)
        {
            foreach (byte b in Data)
            {
                SendBuffer.Add(b);
            }
            if (ClearToSend)
                SendFromBuffer();
        }

        public void SendFromBuffer()
        {
            while (SendBuffer.Count > 0 && ClearToSend)
            {
                byte b = SendBuffer.Read();
                SendPacket(0, b);
            }
        }

        public void SendPacket(byte Port, byte Data)
        {
            // Receive Packet:
            //   0: REQUEST_BYTE | SEND_ASCII_LOWER | SEND_ASCII_UPPER
            //   1: Port Number (0-255)
            //   2: Data
            //   3: PACKET_END

            byte[] Packet = new byte[4] {
                SEND_ASCII_LOWER,
                0,
                Data,
                PACKET_END };
            SendRawData(Packet);
            ClearToSend = false;
        }

        public void SendRawData(byte[] Data)
        {
            try
            {
                //CheckPins();
                if (CharacterDelay > 0 || LineDelay > 0)
                {
                    for (int i = 0; i < Data.Length; i++)
                    {
                        if (port == null || !port.IsOpen)
                            break;

                        port.Write(Data, i, 1);

                        DateTime w = DateTime.Now.AddMilliseconds(CharacterDelay);
                        while (DateTime.Now < w)
                            System.Windows.Forms.Application.DoEvents();
                        //Thread.Sleep(CharacterDelay);
                    }
                }
                else
                {
                    if (port == null || !port.IsOpen)
                    {
                        Disconnect();
                    }
                    port.WriteTimeout = 100;
                    port.Write(Data, 0, Data.Length);
                }
            }
            catch (Exception ex)
            {
                Buffer.Add(ASCIIEncoding.ASCII.GetBytes(ex.Message));
                ReceiveChannel.ReadData(Buffer);
                //Disconnect();
            }
        }

        void CheckPins()
        {
            System.Diagnostics.Debug.Write("Pins");
            System.Diagnostics.Debug.Write(" DTR:" + port.DtrEnable.ToString());
            System.Diagnostics.Debug.Write(" DSR:" + port.DsrHolding.ToString());
            System.Diagnostics.Debug.Write(" RTS:" + port.RtsEnable.ToString());
            System.Diagnostics.Debug.Write(" CTS:" + port.CtsHolding.ToString());
            System.Diagnostics.Debug.Write(" CD:" + port.CDHolding.ToString());
            System.Diagnostics.Debug.WriteLine(" Bytes To Write:" + port.BytesToWrite.ToString());
        }
    }
}
