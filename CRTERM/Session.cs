using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TerminalUI.Terminals;
using CRTerm.IO;
using CRTerm.Transfer;
using CRTerm.Config;
using TerminalUI;

namespace CRTerm
{
    [Serializable]
    public class Session : IConfigurable
    {
        private string name = "Session";
        private ITransport _transport = null;
        private ITerminal _terminal = null;
        private Transfer.ITransferProtocol _transfer;
        private DisplayControl _frameBuffer = null;

        [ConfigItem]
        public ITransport Transport
        {
            get { return _transport; }
            set
            {
                if (_transport != null)
                    _transport.DataReceived -= _transport_DataReceived;

                _transport = value;

                if (_transport != null)
                    _transport.DataReceived += _transport_DataReceived;
            }
        }

        private void _transport_DataReceived(IBuffered receiver)
        {
            if (Transfer != null)
                Transfer.ReceiveData(receiver);
            else
                Terminal_ReceiveData(receiver);
        }

        private void Terminal_ReceiveData(IBuffered receiver)
        {
            while (receiver.BytesWaiting > 0)
                Terminal.ProcessReceivedCharacter((char)receiver.ReadByte());
        }

        [ConfigItem]
        public ITerminal Terminal
        {
            get { return _terminal; }
            set
            {
                _terminal = value;
            }
        }

        private void _terminal_DataSent(IBuffered terminal)
        {
            while (terminal.BytesWaiting > 0)
                Transport?.Send(terminal.ReadByte());
        }

        public DisplayControl FrameBuffer
        {
            get { return _frameBuffer; }
            set
            {
                _frameBuffer = value;
            }
        }

        private void FrameBuffer_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            TerminalKeyEventArgs ea = new TerminalKeyEventArgs(e);
            Terminal.SendKey(ea);
            DumpBuffer(Terminal);
        }

        private void DumpBuffer(ITerminal Terminal)
        {
            while (!Terminal.SendBuffer.IsEmpty())
            {
                char c = Terminal.SendBuffer.Read();
                SendChar(c);
                DateTime t = DateTime.Now.AddMilliseconds(20);
                while (DateTime.Now < t)
                    System.Windows.Forms.Application.DoEvents();
            }
        }

        public virtual void SendChar(char c)
        {
            //System.Diagnostics.Debug.WriteLine("SendChar: " + (int) c);
            byte data = (byte)c;
            if (data == 8 && FrameBuffer.BackspaceDelete)
                data = 127;
            SendByte(data);
        }

        [ConfigItem]
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
            }
        }

        public ITransferProtocol Transfer
        {
            get
            {
                return this._transfer;
            }

            set
            {
                this._transfer = value;
            }
        }

        public string Terminal_StatusDetails { get; internal set; }

        //public TextConsole Display { get; internal set; }

        public List<string> GetPortNames()
        {
            List<string> ret = new List<string>();

            ret.AddRange(IO.SerialIOPort.ListPortNames());
            ret.Sort();
            ret.Insert(0, "Test");
            ret.Insert(1, "Telnet");
            //Array.Sort(portNames);

            return ret;
        }

        public Session()
        {
        }

        public void Load(string ConnectionDetails)
        {
            this.Transport = new IO.TestPort();
            this.Terminal = new ANSITerminal();
        }

        public void Connect()
        {
            if (Transport == null)
                return;

            Transport.Connect();
        }

        public void Disconnect()
        {
            if (Transport == null)
                return;

            Transport.Disconnect();
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            if (Transport != null)
                s.AppendLine("Port:" + Transport.ToString());
            if (Terminal != null)
                s.AppendLine("Terminal:" + Terminal.ToString());
            return s.ToString();
        }

        public void Init()
        {
            Configuration config = new Configuration();
            config.LoadConfiguration(this);

            Terminal.Display = this.FrameBuffer;
            Terminal.ReadyToSend += Terminal_DataToSend;

            FrameBuffer.Terminal = Terminal;

            //FrameBuffer.KeyPress -= FrameBuffer_KeyPress;
            //FrameBuffer.KeyPress += FrameBuffer_KeyPress;

            this.FrameBuffer.PrintLine("Port:" + Transport.ToString());
            this.FrameBuffer.PrintLine("Terminal:" + Terminal.ToString());

            try
            {
                this.Connect();
            }
            catch (CRTException ex)
            {
                this.FrameBuffer.PrintLine(ex.Message);
            }

            Transport?.UpdateStatus();
        }

        private void Terminal_DataToSend(object sender, EventArgs e)
        {
            ITerminal t = sender as ITerminal;
            if (t == null)
                return;

            DumpBuffer(t);
        }

        public void SaveConfiguration()
        {
            Configuration config = new Configuration();
            config.ConfigurableObjects.Add(this);
            config.ConfigurableObjects.Add(this.Transport);
            config.ConfigurableObjects.Add(this.Terminal);
            config.SaveConfiguration();
        }


        public void SendByte(byte Data)
        {
            Transport?.Send(Data);
        }

        public void SendBytes(byte[] Data)
        {
            Transport?.Send(Data);
        }

        //public void DataReceived(IBuffered channel)
        //{
        //    if (Transfer != null)
        //        Transfer.ReceiveData(channel);
        //    else
        //        Terminal.ReceiveData(channel);
        //}
    }
}
