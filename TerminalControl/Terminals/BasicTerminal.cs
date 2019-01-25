using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerminalControl.Terminals
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

        private EditModes _editMode = EditModes.None;
        [ConfigItem]
        public EditModes EditMode
        {
            get
            {
                return _editMode;
            }
            set
            {
                _editMode = value;
                switch (value)
                {
                    case EditModes.LineEdit:
                        FrameBuffer.CursorStyle = CursorStyles.Block;
                        break;
                    case EditModes.FullScreen:
                        FrameBuffer.CursorStyle = CursorStyles.Block;
                        break;
                    case EditModes.None:
                    case EditModes.LocalEcho:
                    default:
                        FrameBuffer.CursorStyle = CursorStyles.Underline;
                        break;
                }
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

        private RingBuffer<char> _sendBuffer = new RingBuffer<char>();
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

        public virtual void Print(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                ProcessReceivedCharacter(c);
            }
        }

        public virtual void InitKeymap()
        {
        }
    }
}
