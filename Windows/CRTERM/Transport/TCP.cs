using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using CRTERM.Common;

namespace CRTERM.Transport
{
	public class TCP : Loopback
	{
		protected Socket socket;
		protected RingBuffer ReceiveBuffer = new RingBuffer(16384);

		public class StateObject
		{
			public Socket workSocket = null;
			public const int BUFFER_SIZE = 2048;
			public byte[] buffer = new byte[BUFFER_SIZE];
			public TCP Transport;
		}

		public TCP()
		{
			ConfigData.Clear();
			ConfigData.Set("Remote Host", "host.server.com");
			ConfigData.Set("Port", 23);
		}

		public string Host
		{
			get { return ConfigData["Remote Host"].Value; }
			set
			{
				ConfigData["Remote Host"].Value = value;
			}
		}

		public int Port
		{
			get { return ConfigData["Port"].IntValue; }
			set
			{
				ConfigData["Port"].IntValue = value;
			}
		}

		public override void Open()
		{
			Connect(Host, Port);
		}

		protected virtual void Connect(string HostName, int PortNumber)
		{
			if (Connected)
				disconnect("Connecting to new host");
			socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
			try
			{
				socket.Connect(HostName, PortNumber);
			}
			catch (SocketException ex)
			{
				onDisconnected(ex.Message);
			}
			if (Connected)
			{
				onConnected();
				BeginReceive();
			}
		}

		private void BeginReceive()
		{
			StateObject so = new StateObject();
			so.workSocket = socket;
			so.Transport = this;
			socket.BeginReceive(so.buffer, 0, StateObject.BUFFER_SIZE, 0, new AsyncCallback(Read_Callback), so);
		}

		public static void Read_Callback(IAsyncResult ar)
		{
			StateObject so = ar.AsyncState as StateObject;
			Socket socket = so.workSocket;
			int bytesRead = socket.EndReceive(ar);
			for (int i = 0; i < bytesRead; ++i)
			{
				so.Transport.Parse(so.buffer[i]);
			}
			if (ar.IsCompleted)
				so.Transport.ReceiveData(so.Transport.ReceiveBuffer.ReadAll());

			if (!socket.Connected)
				so.Transport.disconnect("Remote System Disconnected.");
			else
				socket.BeginReceive(so.buffer, 0, StateObject.BUFFER_SIZE, 0, new AsyncCallback(Read_Callback), so);
		}

		virtual public void Parse(byte b)
		{
			this.ReceiveBuffer.Write(b);
		}

		public override void Close()
		{
			disconnect("Session Closed");
		}

		protected virtual void disconnect(string Message)
		{
			if (socket != null)
			{
				socket.Disconnect(false);
				socket = null;
				onDisconnected(Message);
			}
		}

		public override bool Connected
		{
			get
			{
				if (socket == null)
					return false;
				return socket.Connected;
			}
			protected set
			{
				base.Connected = value;
			}
		}

		public override void Send(byte[] Data)
		{
			try
			{
				socket.Send(Data);
			}
			catch (Exception ex)
			{
				disconnect(ex.Message);
			}
		}
	}
}
