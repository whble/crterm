using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRTerm.Terminals;

namespace CRTerm
{
    public class TextConsole : IFrameBuffer
    {
        bool Done = false;
        private Session CurrentSession = null;
        private Terminals.ITerminal _terminal;
        int _cols = 80;
        int _rows = 25;

        public event KeyPressEventHandler KeyPressed;

        public int X
        {
            get
            {
                return Console.CursorLeft;
            }
            set
            {
                Console.CursorLeft = value;
            }
        }

        public int Y
        {
            get
            {
                return Console.CursorTop;
            }
            set
            {
                Console.CursorTop = value;
            }
        }

        public ITerminal Terminal
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int Cols
        {
            get
            {
                return _cols;
            }
            set
            {
                _cols = value;
                Console.SetWindowSize(_cols, _rows);
            }
        }

        public ColorCodes CurrentBackground
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ColorCodes CurrentForeground
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int Rows
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool ClearToSend { get; }
        public int BytesWaiting { get; }
        public ConnectionStatusCodes Status { get; }
        public string StatusDetails { get; }

        public void Setup()
        {
            Console.TreatControlCAsInput = true;
            CurrentSession = new Session();
            CurrentSession.FrameBuffer = this;
            CurrentSession.Display = this;
            CurrentSession.Init();
        }

        public void Loop()
        {
            while (!Done)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.C && key.Modifiers == ConsoleModifiers.Control)
                    Done = true;
            }
        }

        public void UpdateStatus()
        {
            //throw new NotImplementedException();
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void Fill(char c)
        {
            throw new NotImplementedException();
        }

        public void Locate(int Row, int Col)
        {
            throw new NotImplementedException();
        }

        public void PrintChar(char c)
        {
            throw new NotImplementedException();
        }

        public void PrintChars(char[] Chars)
        {
            throw new NotImplementedException();
        }

        public void PrintLine(string s)
        {
            throw new NotImplementedException();
        }

        public void PrintLineFeed()
        {
            throw new NotImplementedException();
        }

        public void PrintNewLine()
        {
            throw new NotImplementedException();
        }

        public void PrintReturn()
        {
            throw new NotImplementedException();
        }

        public void PrintString(string s)
        {
            throw new NotImplementedException();
        }

        public void SendByte(byte Data)
        {
            throw new NotImplementedException();
        }

        public byte ReadByte()
        {
            throw new NotImplementedException();
        }
    }
}
