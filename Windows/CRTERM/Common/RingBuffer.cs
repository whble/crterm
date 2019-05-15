using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRTERM.Common
{
	public class RingBuffer
	{
		byte[] buffer;
		int readPos = 0;
		int writePos = 0;

		public RingBuffer(int Size)
		{
			buffer = new byte[Size];
		}

		public bool isEmpty()
		{
			if (readPos == writePos)
				return true;
			else
				return false;
		}

		public int BytesWaiting
		{
			get
			{
				int len = writePos - readPos;
				if (len < 0)
					len += buffer.Length;
				return len;
			}
		}

		public void Clear()
		{
			writePos = 0;
			readPos = 0;
		}

		public byte this[int index]
		{
			get
			{
				return buffer[(readPos + index) % buffer.Length];
			}
			set
			{
				buffer[(writePos + index) % buffer.Length] = value;
			}
		}

		public void Write(byte data)
		{
			buffer[writePos] = data;
			writePos = (writePos + 1) % buffer.Length;
			if (readPos == writePos)
				throw new Exception("Buffer full");
		}

		public void Write(byte[] data)
		{
			foreach (byte b in data)
			{
				Write(b);
			}
		}

		public byte Peek()
		{
			return buffer[readPos];
		}

		public byte Read()
		{
			int pos = readPos;
			readPos = (readPos + 1) % buffer.Length;
			return buffer[pos];
		}

		public byte[] ReadAll()
		{
			byte[] b = new byte[BytesWaiting];
			for (int i = 0; BytesWaiting > 0; ++i)
				b[i] = Read();
			return b;
		}

		public void Discard(int bytes)
		{
			if (BytesWaiting > bytes)
				bytes = BytesWaiting;

			readPos = (readPos + bytes) % buffer.Length;
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

		public void displayBuffer(int Length)
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

