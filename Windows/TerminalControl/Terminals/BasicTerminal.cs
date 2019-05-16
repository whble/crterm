using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerminalUI.Terminals
{
    /// <summary>
    /// The terminal acts as the translation layer between the data port and the display.
    /// Terminal receives byte data to UTF text and sends text to the frame buffer.
    /// </summary>
    public class BasicTerminal : ITerminal
    {
        /// <summary>
        /// Send DELETE when user presses Backspace? 
        /// </summary>
        private bool backspaceDeleteMode;

        private EchoModes _editMode = EchoModes.EchoOff;
        [ConfigItem]
        public EchoModes EchoMode
        {
            get
            {
                return _editMode;
            }
            set
            {
                _editMode = value;
            }
        }

        private TerminalKeyMap _keyMap = new TerminalKeyMap();
        public TerminalKeyMap KeyMap
        {
            get
            {
                return this._keyMap;
            }
        }

        private DisplayControl _frameBuffer = null;
        public DisplayControl Display
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

        private RingBuffer<char> _sendBuffer = new RingBuffer<char>();

        public event EventHandler ReadyToSend;

        public RingBuffer<char> SendBuffer
        {
            get
            {
                return this._sendBuffer;
            }

            set
            {
                this._sendBuffer = value;
            }
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
            }
        }

        public virtual void SendChar(char c)
        {
            //System.Diagnostics.Debug.WriteLine("SendChar: " + (int) c);
            if (c == (char)8 && BackspaceDeleteMode)
                c = (char)127;

            SendBuffer.Add(c);
            ReadyToSend?.Invoke(this, new EventArgs());
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
            for (int i = 0; i < Text.Length; i++)
            {
                SendBuffer.Add(Text[i]);
                ReadyToSend?.Invoke(this, new EventArgs());
                while (SendBuffer.Count > 255)
                    System.Windows.Forms.Application.DoEvents();
            }
        }

        /// <summary>
        /// Call this function to send a non-printable key, such as arrow keys,
        /// function keys, etc. This should not be used to send printable keys,
        /// except when the intent is to substitute it for a different key.
        /// </summary>
        /// <param name="KeyCode"></param>
        public virtual void SendKey(TerminalKeyEventArgs terminalKey)
        {
            switch (terminalKey.KeyCode)
            {
                case System.Windows.Forms.Keys.Home:
                    if (terminalKey.Modifier.HasFlag(System.Windows.Forms.Keys.Control))
                        Display.Clear();    
                    break;
                case System.Windows.Forms.Keys.Tab:
                    SendChar('\t');
                    break;
                default:
                    break;
            }
            //System.Diagnostics.Debug.WriteLine("Keypress: " + KeyCode.ToString());
            //SendChar(terminalKey.KeyChar);
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
                    Display.PrintReturn();
                    break;
                case '\n':
                    Display.PrintLineFeed();
                    break;
                case '\x8':
                    Display.CurrentColumn -= 1;
                    break;
                default:
                    if (c >= ' ')
                        Display.Print(c);
                    //else
                    //    System.Diagnostics.Debug.WriteLine("ProcessCharacter: " + (int)c);
                    break;
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

        public virtual void PrintLine()
        {
            Display.PrintLine();
        }

        public virtual void PrintLine(string s)
        {
            Print(s);
            Display.PrintLine();
        }

        public virtual void InitKeymap()
        {
        }


    }
}
