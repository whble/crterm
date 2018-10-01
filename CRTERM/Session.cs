using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRTerm.Terminals;
using CRTerm.IO;
using CRTerm.Transfer;

namespace CRTerm
{
    [Serializable]
    public class Session : IConfigurable
    {
        private string name = "Session";
        private ITransport _transport = null;
        private ITerminal _terminal = null;
        private Transfer.ITransferProtocol _transfer;
        private IFrameBuffer _frameBuffer = null;

        [ConfigItem]
        public ITransport Transport
        {
            get { return _transport; }
            set
            {
                if(_transport != null)
                    _transport.DataReceived -= _transport_DataReceived;

                _transport = value;

                if (_transport != null)
                    _transport.DataReceived += _transport_DataReceived;
            }
        }

        private void _transport_DataReceived(IBuffered receiver)
        {
            Terminal.ReceiveData(receiver);
        }

        [ConfigItem]
        public ITerminal Terminal
        {
            get { return _terminal; }
            set
            {
                if (_terminal != null)
                    _terminal.DataSent -= _terminal_DataSent;
                _terminal = value;
                if(_terminal != null)
                    _terminal.DataSent += _terminal_DataSent;
            }
        }

        private void _terminal_DataSent(IBuffered terminal)
        {
            while (terminal.BytesWaiting > 0)
                Transport?.SendByte(terminal.ReadByte());
        }

        private void FrameBuffer_KeyPressed(IFrameBuffer frameBuffer, TerminalKeyEventArgs e)
        {
            Terminal.SendKey(e);
        }

        public IFrameBuffer FrameBuffer
        {
            get { return _frameBuffer; }
            set
            {
                _frameBuffer = value;
                this.FrameBuffer.KeyPressed += FrameBuffer_KeyPressed;
            }
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

        public TextConsole Display { get; internal set; }

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
            this.Terminal = new Terminals.BasicTerminal();
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
            Terminal.FrameBuffer = this.FrameBuffer;

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
            Transport?.SendByte(Data);
        }

        public void SendBytes(byte[] Data)
        {
            Transport?.SendBytes(Data);
        }

        public void DataReceived(IBuffered channel)
        {
            if (Transfer != null)
                Transfer.ReceiveData(channel);
            else
                Terminal.ReceiveData(channel);
        }
    }
}
