using System;
using System.Collections.Generic;
using System.Text;

namespace CRTerm
{
    public class RingBuffer
    {
        readonly int size = 16384;
        public byte[] data;
        int readPos = 0;
        int writePos = 0;

        public RingBuffer(int Capacity = 4096)
        {
            this.size = Capacity;
            data = new byte[size];
        }

        public bool IsEmpty()
        {
            return ReadPos == WritePos;
        }

        public int Count
        {
            get
            {
                int len = WritePos - ReadPos;
                if (len < 0)
                    len += size;
                return len;
            }
        }

        public int Capacity
        {
            get
            {
                return size;
            }
        }

        public int ReadPos
        {
            get
            {
                return this.readPos;
            }

            protected set
            {
                this.readPos = value;
            }
        }

        public int WritePos
        {
            get
            {
                return this.writePos;
            }

            protected set
            {
                this.writePos = value;
            }
        }

        public int CountToEnd
        {
            get
            {
                if (readPos > writePos)
                    return size - readPos;
                else
                    return writePos - readPos;
            }
        }

        public void Clear()
        {
            WritePos = 0;
            ReadPos = 0;
        }

        public byte this[int index]
        {
            get
            {
                return data[(ReadPos + index) % size];
            }
            set
            {
                data[(WritePos + index) % size] = value;
            }
        }

        public void Add(byte[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                Add(data[i]);
            }
        }

        public void Add(byte data)
        {
            this.data[this.WritePos] = data;
            WritePos = (WritePos + 1) % size;
            if (ReadPos == WritePos)
                throw new Exception("Buffer full");
        }

        public byte Peek()
        {
            return data[ReadPos];
        }

        public byte Read()
        {
            int pos = ReadPos;
            ReadPos = (ReadPos + 1) % size;
            return data[pos];
        }

        public int Read(byte[] Data, int Max)
        {
            int max = Max;
            if (this.Count < max)
                max = this.Count;
            if (Data.Length < max)
                max = Data.Length;

            for(int i=0; i<max; i++)
                Data[i] = Read();

            return max;
        }

        public void Read(byte[] buffer, int offset, int len)
        {
            for (int i = 0; i < len; ++i)
                buffer[i] = Read();
        }

        public void Discard(int bytes)
        {
            if (Count < bytes)
                bytes = Count;

            ReadPos = (ReadPos + bytes) % size;
        }

        public string getString(int pos, int length)
        {
            string s = "";
            for (int i = pos; i < pos + length; ++i)
            {
                if (this[i] >= 32 && this[i] <= 127)
                    s += (char)this[i];
            }
            return s.Trim();
        }


        public void Debug_WriteBuffer(int Length)
        {
            int i = 0;
            while (i < Length)
            {
                System.Diagnostics.Debug.Write(i.ToString("X2") + " : ");
                for (int j = i; j < i + 16; ++j)
                {
                    if (j < Length)
                        System.Diagnostics.Debug.Write(this[j].ToString("X2"));
                    else
                        System.Diagnostics.Debug.Write("  ");
                    System.Diagnostics.Debug.Write(' ');
                }
                System.Diagnostics.Debug.Write(": ");
                for (int j = i; j < i + 16; ++j)
                {
                    char c;
                    if (j < Length)
                        c = (char)this[j];
                    else
                        c = ' ';
                    if (c < ' ' || c > '~')
                        c = '.';
                    System.Diagnostics.Debug.Write(c);
                }
                i += 16;
                System.Diagnostics.Debug.WriteLine(" :");
            }
            System.Diagnostics.Debug.WriteLine("");
        }
    }
}
