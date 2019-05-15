using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRTERM.Common;

namespace CRTERM.Transport
{
	/// <summary>
	/// This is a basic transport that doesn't actually go anywhere. If you use this transport
	/// directly, it will simply loop sent data back to the terminal.
	/// There are no interited properties (such as baud rate), since your transport handles all that internally
	/// using the config dialog and the LoadConfigData and SaveConfigData classes.
	/// <para>To see an example of a wire protocol, see the SerialTransport class.</para>
	/// <para>To see an example of a network protocol, see the TCPTransport class.</para>
	/// </summary>
	public class Loopback : ITransport
	{
    public Loopback()
    {
			ConfigData.Clear();
			ConfigData.Set("Connect Message", "Hello, World.");
			ConfigData.Set("Disconnect Message", "Disconnected.", new string[] { "Test", "Kilroy was here", "The future is now", "Compile Your Reality" });
      ConfigData.Set("Checkbox", true);
      ConfigData.Set("Number", 123);
    }

		bool _Connected;
		virtual public bool Connected
		{
			protected set { _Connected = value; }
			get { return _Connected; }
		}

		virtual public void Open()
		{
			Connected = true;
			onConnected();

			// throw some test data through the connection
			Write("Looback Transport\r\n"
				+ "ABCDEFGHIJKLMNOPQRSTUVWXYZ\r\n"
				+ "1234567890\r\n"
				+ "!@#$%^&*()\r\n"
        + DateTime.Now.ToString() + "\r\n"
				+ ConfigData["Connect Message"].Value + "\r\n");
		}

		virtual public void Write(string text)
		{
			UTF8Encoding enc = new UTF8Encoding(false);
			byte[] data = enc.GetBytes(text);
			Send(data);
		}

		virtual public void ReceiveText(string text)
		{
			UTF8Encoding enc = new UTF8Encoding(false);
			byte[] data = enc.GetBytes(text);
			ReceiveData(data);
		}

		virtual public void ReceiveData(byte[] data)
		{
			onDataReceived(data);
		}

		virtual public void Close()
		{
      ReceiveText(ConfigData["Disconnect Message"].Value + "\r\n");
			ReceiveText("Connection Closed at " + DateTime.Now.ToString() + "\r\n");
			Connected = false;
			onDisconnected("Disconnected");
		}

		/// <summary>
		/// Data received from the remote host.
		/// </summary>
		public event DataReceivedEventHandler DataReceived;
		protected void onDataReceived(byte[] Data)
		{
			if (DataReceived != null)
			{
				DataReceived(this, Data);
			}
		}

		public event TerminalEventHandler TerminalEvent;
		protected void onConnected()
		{
			if (TerminalEvent != null)
			{
				TerminalEventArgs e = new TerminalEventArgs();
				e.EventType = EventTypeCodes.Connected;
				TerminalEvent(this, e);
			}
		}

		protected void onDisconnected(string DisconnectMessage)
		{
			if (TerminalEvent != null)
			{
				TerminalEventArgs e = new TerminalEventArgs();
				e.EventType = EventTypeCodes.Disconnected;
				e.Message = DisconnectMessage;
				TerminalEvent(this, e);
			}
		}

		/// <summary>
		/// Send data to the remote host
		/// </summary>
		/// <param name="Data"></param>
		virtual public void Send(byte[] Data)
		{
			// this just loops the data back to the receiver. 
			// This tests to make sure the emulation and display layers work.
			if (Connected)
				onDataReceived(Data);
		}

		private ConfigList _configData=new ConfigList();
    public ConfigList ConfigData {
      get {
        return _configData;
      }
    }

		private FlowControl _lines = new FlowControl();
		public FlowControl Lines
		{
			get { return _lines; }
		}

    virtual public int BytesWaiting
    {
      get { return 0; }
    }

    virtual public void FlushBuffer()
    {
    }

    virtual public void Break()
    {
      Write("\r\n*** BREAK ***\r\n");
    }
	}

}
