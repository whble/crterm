using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRTerm.Terminals
{
    /// <summary>
    /// The terminal acts as the translation layer between the data port and the display.
    /// Terminal receives byte data to UTF text and sends text to the frame buffer.
    /// </summary>
    public class BasicTerminal : ITerminal
    {
        public event DataReadyEventHandler DataSent;
        public event StatusChangeEventHandler StatusChangedEvent;

        /// <summary>
        /// Send DELETE when user presses Backspace? 
        /// </summary>
        private bool backspaceDeleteMode;
        private RingBuffer SendBuffer = new RingBuffer();

        private ConnectionStatusCodes status;
        private string statusDetails;

        private bool _basicMode = false;
        public bool BasicMode
        {
            get
            {
                return _basicMode;
            }
            set
            {
                _basicMode = value;
                if (value)
                    FrameBuffer.CursorType = CursorTypes.Block;
            }
        }

        private IFrameBuffer _frameBuffer = null;
        public IFrameBuffer FrameBuffer
        {
            get { return _frameBuffer; }
            set
            {
                _frameBuffer = value;
                //_frameBuffer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(_frameBuffer_KeyPress);
            }
        }

        void _frameBuffer_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            SendChar(e.KeyChar);
        }


        public virtual string Name
        {
            get { return "TTY"; }
        }

        public virtual string StatusText
        {
            get
            {
                StringBuilder s = new StringBuilder();
                s.Append(this.Name);
                if (BackspaceDeleteMode)
                {
                    s.Append(" DEL");
                }

                return s.ToString();
            }
        }

        [ConfigItem]
        public bool BackspaceDeleteMode
        {
            get
            {
                return this.backspaceDeleteMode;
            }

            set
            {
                this.backspaceDeleteMode = value;
                UpdateStatus();
            }
        }

        public string StatusDetails { get; }
        public ConnectionStatusCodes Status { get; }

        public delegate void TextReceivedEvent(string Text);
        public event TextReceivedEvent TextReceived;
        protected virtual void OnTextReceived(string Text)
        {
            if (TextReceived == null)
                return;

            TextReceived(Text);
        }

        public virtual void SendChar(char c)
        {
            //System.Diagnostics.Debug.WriteLine("SendChar: " + (int) c);
            byte data = (byte)c;
            if (data == 8 && BackspaceDeleteMode)
                data = 127;
            SendByte(data);
        }

        public static byte[] GetBytes(string Text)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(Text);
            return bytes;
        }

        /// <summary>
        /// Convert a UTF string to ASCII data and send it to the output port.
        /// </summary>
        /// <param name="Text">Text to transmit</param>
        public virtual void SendString(string Text)
        {
            byte[] data = GetBytes(Text);
            SendBytes(data);
        }

        /// <summary>
        /// Call this function to send a non-printable key, such as arrow keys,
        /// function keys, etc. This should not be used to send printable keys,
        /// except when the intent is to substitute it for a different key.
        /// </summary>
        /// <param name="KeyCode"></param>
        public virtual void SendKey(TerminalKeyEventArgs terminalKey)
        {
            //System.Diagnostics.Debug.WriteLine("Keypress: " + KeyCode.ToString());
            SendChar(terminalKey.KeyChar);
        }

        /// <summary>
        /// Handles a single character at a time. If you override this function,
        /// call base at the END.
        /// </summary>
        /// <param name="c"></param>
        public virtual void ProcessReceivedCharacter(Char c)
        {
            switch (c)
            {
                case '\r':
                    FrameBuffer.PrintReturn();
                    break;
                case '\n':
                    FrameBuffer.PrintNewLine();
                    break;
                case '\x8':
                    FrameBuffer.X -= 1;
                    break;
                default:
                    if (c >= ' ')
                        FrameBuffer.PrintChar(c);
                    //else
                    //    System.Diagnostics.Debug.WriteLine("ProcessCharacter: " + (int)c);
                    break;
            }
        }

        public virtual void SendByte(byte Data, bool RaiseEvent=true)
        {
            SendBuffer.Add(Data);
            if (RaiseEvent)
                DataSent?.Invoke(this);
        }

        public virtual void SendBytes(byte[] Data)
        {
            foreach (byte b in Data)
            {
                SendByte(b, false);
            }
            DataSent?.Invoke(this);

        }

        public virtual int BytesWaiting
        {
            get
            {
                return SendBuffer.Count;
            }
        }

        public virtual byte ReadByte()
        {
            return SendBuffer.Read();
        }

        public virtual void ReceiveData(IBuffered channel)
        {
            while (channel.BytesWaiting > 0)
                ProcessReceivedCharacter((char)channel.ReadByte());
        }

        public void ReceiveBytes(byte[] Data)
        {
            string Text = Encoding.ASCII.GetString(Data);
            for (int i = 0; i < Text.Length; i++)
            {
                char c = Text[i];
                ProcessReceivedCharacter(c);
            }
        }

        public virtual bool ClearToSend
        {
            get
            {
                return true;
            }
        }

        public ConnectionStatusCodes Status1
        {
            get
            {
                return this.status;
            }

            set
            {
                this.status = value;
            }
        }

        public string StatusDetails1
        {
            get
            {
                return this.statusDetails;
            }

            set
            {
                this.statusDetails = value;
            }
        }


        public virtual void Print(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                ProcessReceivedCharacter(c);
            }
        }

        public void UpdateStatus()
        {
            StatusEventArgs eventArgs = new StatusEventArgs(this.Status1, this.StatusDetails);
            StatusChangedEvent?.Invoke(this, eventArgs);
        }

        public int ReadData(byte[] Data, int Count)
        {
            return SendBuffer.Read(Data, Count);
        }
    }
}
