using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CRTERM.Transport;
using CRTERM.Common;

namespace CRTERM.Terminal
{
	/// <summary>
	/// This is the base filter that you will need to inherit in order to program a custom filter.
	/// </summary>
	public class TerminalTTY : ITerminal
	{
		public TerminalTTY()
		{
			ConfigData.Set("AutoLF", false);
		}

		public bool AutoLF
		{
			get
			{
				return ConfigData["AutoLF"].BoolValue;
			}
		}

		private Modem.IModem _modem;
		public Modem.IModem Modem
		{
			get
			{
				return _modem;
			}
			set
			{
				if (_modem != value)
				{
					if (_modem != null)
					{
						_modem.DataReceived -= Modem_DataReceived;
						_modem.TerminalEvent -= Modem_TerminalEvent;
					}
					_modem = value;
					if (_modem != null)
					{
						_modem.DataReceived += new DataReceivedEventHandler(Modem_DataReceived);
						_modem.TerminalEvent += new TerminalEventHandler(Modem_TerminalEvent);
					}
				}
			}
		}

		void Modem_DataReceived(ICommProvider sender, byte[] Data)
		{
			ReceiveData(Data);
		}

		void Modem_TerminalEvent(ICommProvider sender, TerminalEventArgs e)
		{
			onTerminalEvent(e);
		}

		public event TerminalEventHandler TerminalEvent;
		private void onTerminalEvent(TerminalEventArgs e)
		{
			if (TerminalEvent != null)
				TerminalEvent(this, e);
		}

		public event ColorEventHandler ColorEvent;
		private void onColorEvent(ColorEventArgs e)
		{
			if (ColorEvent != null)
				ColorEvent(this, e);
		}

		public event DataReceivedEventHandler DataReceived;
		/// <summary>
		/// Delivers the raw data received with no translation.
		/// </summary>
		/// <param name="Data"></param>
		protected virtual void onDataReceived(byte[] Data)
		{
			if (DataReceived != null)
				DataReceived(this, Data);
		}

		/// <summary>
		/// Process data, convert to Unicode Text, and send it along to the application layer.
		/// </summary>
		/// <param name="Data"></param>
		public void ReceiveData(byte[] Data)
		{
			onDataReceived(Data);
			Print(Data);
		}

		Encoding encoding = System.Text.Encoding.GetEncoding(437);
		/// <summary>
		/// Data was received from transport. Process it and pass it on to the Terminal display.
		/// </summary>
		/// <param name="Data"></param>
		public virtual void Print(byte[] Data)
		{
			string stringData = encoding.GetString(Data);
			Print(stringData);
		}

		/// <summary>
		/// Process string data and pass it on to the terminal display.
		/// </summary>
		/// <param name="Data"></param>
		public virtual void Print(string Data)
		{
			for (int i = 0; i < Data.Length; i++)
			{
				char c = Data[i];
				switch (c)
				{
					case '\x08':
					case '\x80':
						onCursorEvent(CursorCommandCodes.BackSpace);
						break;
					case '\r':
						onCursorEvent(CursorCommandCodes.Return);
						onEnterKeyReceived();
						break;
					case '\n':
						onCursorEvent(CursorCommandCodes.Down);
						break;
					case '\f': // form feed
						onCursorEvent(CursorCommandCodes.ClearScreen);
						break;
					default:
						onTextReceived(c.ToString());
						break;
				}
			}
		}

		public event TextReceivedEventHandler TextReceived;
		private void onTextReceived(string Text)
		{
			if (TextReceived != null)
				TextReceived(this, Text);
		}

		public event SimpleEventHandler EnterKeyReceived;
		void onEnterKeyReceived()
		{
			if (EnterKeyReceived != null)
				EnterKeyReceived(this);
		}

		public event CursorEventHandler CursorEvent;
		private void onCursorEvent(CursorCommandCodes CursorCommand)
		{
			onCursorEvent(CursorCommand, 0, 0);
		}

		private void onCursorEvent(CursorCommandCodes CursorCommand, int Row, int Col)
		{
			if (CursorEvent != null)
			{
				CursorEventArgs e = new CursorEventArgs();
				e.CursorCommand = CursorCommand;
				e.Row = Row;
				e.Col = Col;
				CursorEvent(this, e);
			}
		}

		public class KeyboardEventArgs : EventArgs
		{
			public byte[] KeyData;
		}

		public delegate void KeyPressedEvent(object sender, KeyboardEventArgs e);
		public event KeyPressedEvent KeyPressed;
		protected void onKeyPressed(byte[] KeyData)
		{
			if (KeyPressed != null)
			{
				KeyboardEventArgs e = new KeyboardEventArgs();
				e.KeyData = KeyData;
				KeyPressed(this, e);
			}
		}

		/// <summary>
		/// Handle unprintable keys, such as function keys.
		/// </summary>
		/// <param name="Key"></param>
		public virtual void KeyDown(KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F11:
					Modem.Connect();
					break;
				case Keys.F12:
					Modem.Disconnect();
					break;
			}
		}

		/// <summary>
		/// Handle all printable keys and keys that don't require the keycode to be passed
		/// </summary>
		/// <param name="key"></param>
		public virtual void KeyPress(KeyPressEventArgs e)
		{
			switch (e.KeyChar)
			{
				case '\x08':
					Send(new byte[] { (byte)127 });
					break;
				default:
					SendText(e.KeyChar);
					break;
			}
		}

		/// <summary>
		/// Converts a text string to a byte array and sends it on to the transport layer.
		/// </summary>
		/// <param name="?"></param>
		public virtual void SendText(string Data)
		{
			byte[] byteData = new System.Text.UTF8Encoding(true).GetBytes(Data);
			Send(byteData);
		}

		/// <summary>
		/// Converts a single character to a byte and send it to the transport layer.
		/// Control characters are not translated here; do your own control translation first.
		/// <para>(For example: you need to convert the cursor-up key to ^[[A)</para>
		/// </summary>
		/// <param name="Data"></param>
		public virtual void SendText(char Data)
		{
			string s = Data.ToString();
			byte[] byteData = new System.Text.UTF8Encoding(true).GetBytes(s);
			Send(byteData);
		}

		/// <summary>
		/// Send byte data with no translation.
		/// </summary>
		/// <param name="Data"></param>
		public virtual void Send(byte[] Data)
		{
			onKeyPressed(Data);
			Modem.Send(Data);
		}

		#region Configuration Items

		private ConfigList _configData = new ConfigList();
		public ConfigList ConfigData
		{
			get
			{
				return _configData;
			}
		}

		#endregion

		#region ITerminal Members


		public void Write(string Text)
		{
			throw new NotImplementedException();
		}

		public void WriteLine(string Text)
		{
			throw new NotImplementedException();
		}

		public void SetColor(ConsoleColor Foreground, ConsoleColor Background)
		{
			throw new NotImplementedException();
		}

		public void CursorAction(CursorCommandCodes Command, int Row, int Col)
		{
			throw new NotImplementedException();
		}

		public void SendBreak()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
