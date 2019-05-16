using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRTERM.Common;

namespace CRTERM.Common
{
	public enum EventTypeCodes
	{
		None = 0,
		Connected,
		Disconnected,
		BreakReceived,
	}

	public enum CursorCommandCodes
	{
		None = 0,
		Goto,
		Up,
		Down,
		Left,
		Right,
		PageUp,
		PageDown,
		LineStart,
		LineEnd,
		Insert,
		Delete,
		BackSpace,
		ScreenHome,
		ClearScreen,
		Return
	}

	public class TerminalEventArgs : EventArgs
	{
		public EventTypeCodes EventType;
		public string Message;
	}

	public class ColorEventArgs : EventArgs
	{
		public ConsoleColor ForeColor;
		public ConsoleColor BackColor;
	}

	public class CursorEventArgs : EventArgs
	{
		public CursorCommandCodes CursorCommand;
		public int Row;
		public int Col;
	}

	public delegate void DataReceivedEventHandler(ICommProvider sender, byte[] Data);
	public delegate void TextReceivedEventHandler(ICommProvider sender, string Text);
	public delegate void SimpleEventHandler(ICommProvider sender);
	public delegate void TerminalEventHandler(ICommProvider sender, TerminalEventArgs e);
	public delegate void CursorEventHandler(ICommProvider sender, CursorEventArgs e);
	public delegate void ColorEventHandler(ICommProvider sender, ColorEventArgs e);

}
