using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRTERM.Common;

namespace CRTERM.Modem
{
	/// <summary>
	/// Passes data straight from the Terminal to the transport. Does not do any processing and provides
	/// no call handling. If you call the Call function, this will simply immediately respond with 
	/// the connected event.
	/// </summary>
	class NoModem : IModem
	{
		protected Transport.ITransport _transport;
		public Transport.ITransport Transport
		{
			get
			{
				return _transport;
			}
			set
			{
				if (value != _transport)
				{
					if (_transport != null)
					{
						_transport.DataReceived -= _transport_DataReceived;
						_transport.TerminalEvent -= _transport_TerminalEvent;
					}
					_transport = value;
					if (value != null)
					{
						_transport.DataReceived += new DataReceivedEventHandler(_transport_DataReceived);
						_transport.TerminalEvent += new TerminalEventHandler(_transport_TerminalEvent);
					}
				}
			}
		}

		void _transport_TerminalEvent(ICommProvider sender, TerminalEventArgs e)
		{
			onTerminalEvent(e.EventType, e.Message);
		}

		void _transport_DataReceived(ICommProvider sender, byte[] Data)
		{
			ReceiveData(Data);
		}

		public bool Connected
		{
      get { return Transport.Connected; }
		}

		public event TerminalEventHandler TerminalEvent;
		protected void onTerminalEvent(EventTypeCodes EventType, string Message)
		{
			if (TerminalEvent != null)
			{
				TerminalEventArgs e = new TerminalEventArgs();
				e.EventType = EventType;
				e.Message = Message;
				TerminalEvent(this, e);
			}
		}

		public void Connect()
		{
			if (Transport != null)
				Transport.Open();
		}

		public void Disconnect()
		{
			if (Transport != null)
				Transport.Close();
		}

		public event DataReceivedEventHandler DataReceived;
		public void ReceiveData(byte[] data)
		{
			if (DataReceived != null)
			{
				DataReceived(this, data);
			}
		}

		public void Send(byte[] data)
		{
			if (Transport != null)
				Transport.Send(data);
		}


		#region ICommProvider Members


		private ConfigList _configData = new ConfigList();
		public ConfigList ConfigData
		{
			get
			{
				return _configData;
			}
		}

		#endregion

	}
}
