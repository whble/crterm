using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRTERM.Terminal;
using CRTERM.Common;

namespace CRTERM.UI
{
	/// <summary>
	/// The display buffer holds the displayed text data for your terminal. 
	/// All of the filters should invoke methods in FrameBufferin order show text
	/// on the screen or invoke cursor commands.
	/// </summary>
	public class FrameBuffer
	{
		public class TextChangedEventArgs : EventArgs
		{
			public bool WholeScreen;
			public FrameBuffer FrameBuffer;
		}

		private ITerminal _terminal = null;
		public ITerminal Terminal
		{
			get { return _terminal; }
			set
			{
				if (value != _terminal)
				{
					if (_terminal != null)
					{
						_terminal.TextReceived -= Terminal_TextReceived;
						_terminal.CursorEvent -= Terminal_CursorEvent;
						_terminal.ColorEvent -= Terminal_ColorEvent;
					}
					_terminal = value;
					if (value != null)
					{
						_terminal.TextReceived += new TextReceivedEventHandler(Terminal_TextReceived);
						_terminal.CursorEvent += new CursorEventHandler(Terminal_CursorEvent);
						_terminal.ColorEvent += new ColorEventHandler(Terminal_ColorEvent);
					}
				}
			}
		}

		void Terminal_TextReceived(ICommProvider sender, string Text)
		{
			PrintString(Text);
		}

		void Terminal_ColorEvent(ICommProvider sender, Common.ColorEventArgs e)
		{
		}

		void Terminal_CursorEvent(ICommProvider sender, Common.CursorEventArgs e)
		{
			switch (e.CursorCommand)
			{
				case CursorCommandCodes.None:
					break;
				case CursorCommandCodes.Goto:
					CursorRow = e.Row;
					CursorCol = e.Col;
					break;
				case CursorCommandCodes.Up:
					CursorRow--;
					break;
				case CursorCommandCodes.Down:
					CursorRow++;
					break;
				case CursorCommandCodes.Left:
					CursorCol--;
					break;
				case CursorCommandCodes.Right:
					CursorCol++;
					break;
				case CursorCommandCodes.PageUp:
					break;
				case CursorCommandCodes.PageDown:
					break;
				case CursorCommandCodes.LineStart:
					break;
				case CursorCommandCodes.LineEnd:
					break;
				case CursorCommandCodes.Insert:
					break;
				case CursorCommandCodes.Delete:
					break;
				case CursorCommandCodes.BackSpace:
          if (CursorCol == 0)
          {
            CursorRow--;
            CursorCol = Columns - 1;
            SetChar(' ');
          }
          else
            CursorCol--;
					break;
				case CursorCommandCodes.ScreenHome:
					CursorCol = 0;
					CursorRow = 0;
					break;
				case CursorCommandCodes.ClearScreen:
					Clear();
					break;
				case CursorCommandCodes.Return:
					CursorCol = 0;
					break;
				default:
					break;
			}
		}

		void Terminal_DataReceived(ICommProvider sender, byte[] Data)
		{
		}

		public delegate void TextChangedEvent(object sender, TextChangedEventArgs e);
		public event TextChangedEvent TextChanged;
		protected void onTextChanged(bool RefreshWholeScreen)
		{
			if (TextChanged != null)
			{
				TextChangedEventArgs e = new TextChangedEventArgs();
				e.WholeScreen = RefreshWholeScreen;
				e.FrameBuffer = this;
				TextChanged(this, e);
			}
		}

		int _columns = 80;
		public int Columns
		{
			get { return _columns; }
			set
			{
				_columns = value;
				Clear();
			}
		}

		int _rows = 25;
		public int Rows
		{
			get { return _rows; }
			set
			{
				_rows = value;
				Clear();
			}
		}

		int ScrollbackSize = 10000; // buffer size in rows

		int _cursorRow = 0; // the top of the screen is 0
		public int CursorRow
		{
			get { return _cursorRow; }
			set
			{
				if (value != CursorRow)
				{
					if (value >= Rows && AutoScroll) Scroll();
					_cursorRow = InBounds(value, Rows);
					onTextChanged(true);
				}
			}
		}

		int _cursorCol = 0; // the left-most column is 0
		public int CursorCol
		{
			get { return _cursorCol; }
			set
			{
				if (value >= Columns && AutoWrap)
					NewLine();
				else
					_cursorCol = InBounds(value, Columns);
				onTextChanged(false);
			}
		}

		/// <summary>
		/// Moves cursor to start of next line when current line is full
		/// </summary>
		bool AutoWrap = true;
		/// <summary>
		/// Scrolls screen when a line feed is sent on the last line.
		/// </summary>
		bool AutoScroll = true;
		/// <summary>
		/// append a line feed when CR is received
		/// </summary>
		bool AutoLF = false;

		/// <summary>
		/// The scroll buffer lets you read old text on the screen. Item 0 is the oldest
		/// line of text. 
		/// </summary>
		List<string> ScrollBuffer = new List<string>();
		char[,] charData;

		public FrameBuffer()
		{
			Clear();
		}

		int InBounds(int value, int Count)
		{
			return InBounds(value, 0, Count - 1);
		}

		int InBounds(int value, int MinValue, int MaxValue)
		{
			if (value > MaxValue) return MaxValue;
			if (value < MinValue) return MinValue;
			return value;
		}

		public void SetPos(int Row, int Col)
		{
			CursorCol = Col;
			CursorRow = Row;
		}

		/// <summary>
		/// Print the character to the screen and move the cursor to the next column
		/// </summary>
		/// <param name="Text"></param>
		public void PrintChar(char Char)
		{
			SetChar(Char);
			++CursorCol;
		}

		/// <summary>
		/// Set the character at the cursor without advancing the cursor
		/// </summary>
		/// <param name="Char"></param>
		public void SetChar(char Char)
		{
			charData[CursorRow, CursorCol] = Char;
			onTextChanged(false);
		}

		/// <summary>
		/// Move the cursor to the beginning of the line
		/// </summary>
		public void CR()
		{
			CursorCol = 0;
			if (AutoLF)
				LF();
		}

		/// <summary>
		/// Move the cursor to the next line
		/// </summary>
		public void LF()
		{
			++CursorRow;
		}

		/// <summary>
		/// Move the cursor to the beginning of the next line.
		/// </summary>
		public void NewLine()
		{
			CR();
			LF();
		}

		/// <summary>
		/// Scroll the screen by one line. All text on the screen moves up one line,
		/// and the bottom line will be empty.
		/// </summary>
		public void Scroll()
		{
			ScrollBuffer.Add(GetLine(1));
			while (ScrollBuffer.Count > ScrollbackSize)
				ScrollBuffer.RemoveAt(0);
			for (int row = 0; row < charData.GetLength(0) - 1; row++)
				Array.Copy(charData, (row + 1) * Columns, charData, row * Columns, Columns);
			FillRow(Rows - 1, 0, ' ');
		}

		private string GetLine(int LineNumber)
		{
			StringBuilder s = new StringBuilder();
			for (int col = 0; col < charData.GetLength(1); col++)
				s.Append(charData[LineNumber, col]);
			return s.ToString();
		}

		public char GetChar(int Row, int Col)
		{
			return charData[Row, Col];
		}

		public void Clear()
		{
			SetPos(0, 0);
			charData = new char[Rows, Columns];
			FillScreen(' ');
			onTextChanged(true);
		}

		private void FillScreen(char FillChar)
		{
			for (int row = 0; row < charData.GetLength(0); row++)
			{
				FillRow(row, 0, FillChar);
			}
		}

		private void FillRow(int Row, int StartCol, char FillChar)
		{
			for (int col = StartCol; col < charData.GetLength(1); col++)
			{
				charData[Row, col] = FillChar;
			}
		}

		public char GetCharAtCursor()
		{
			return GetChar(this.CursorRow, this.CursorCol);
		}

		public void PrintString(string p)
		{
			if (p == null)
				return;
			for (int i = 0; i < p.Length; i++)
			{
				PrintChar(p[i]);
			}
		}

		public void PrintControlChar(char c)
		{
			if (c >= ' ') PrintChar(c);
			int x = (int)c;
			PrintChar('^');
			PrintChar((char)(x + 64));
		}

		internal void Backspace()
		{
			--CursorCol;
			if (CursorCol < 0)
			{
				if (CursorRow > 0) --CursorRow;
				CursorCol = 0;
			}
		}
	}
}
