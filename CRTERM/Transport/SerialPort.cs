using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRTERM.Common;
using System.IO.Ports;

namespace CRTERM.Transport
{
  public class SerialPort : ITransport
  {
    public SerialPort()
    {
      string[] portList = System.IO.Ports.SerialPort.GetPortNames();
      string defaultPort = "";
      if (portList.Length > 0)
        defaultPort = portList[0];

      ConfigData.Set("Port", defaultPort, portList);
      ConfigData.Set("Speed", "9600", new string[] { "300", "1200", "2400", "4800", "9600", "19200", "38400", "57600", "115200" });
      ConfigData.Set("Parity", "None", Enum.GetNames(typeof(Parity)));
      ConfigData.Set("Data Bits", "8", new string[] { "7", "8" });
      ConfigData.Set("Stop Bits", "One", Enum.GetNames(typeof(StopBits)));
      ConfigData.Set("Flow Control", "Off", new string[] { "Off", "RTS/CTS", "XON/XOFF" });
    }

    System.IO.Ports.SerialPort port = new System.IO.Ports.SerialPort();

    public bool Connected
    {
      get
      {
        if (port == null)
          return false;
        return port.IsOpen;
      }
    }

    public void Open()
    {
      if (Connected)
        Close();
      try
      {
        Parity parity = (Parity)Enum.Parse(typeof(Parity), ConfigData["Parity"].Value);
        StopBits stopBits = (StopBits)Enum.Parse(typeof(StopBits), ConfigData["Stop Bits"].Value);
        port.PortName = ConfigData["Port"].Value;
        port.BaudRate = ConfigData["Speed"].IntValue;
        port.DataBits = ConfigData["Data Bits"].IntValue;
        port.StopBits = stopBits;
        port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
        port.Open();
        port.DtrEnable = true;
        port.RtsEnable = true;
      }
      catch (Exception ex)
      {
        System.Windows.Forms.MessageBox.Show(ex.Message);
      }
      onConnected();
    }

    void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
      if (e.EventType == SerialData.Chars)
      {
        FlushBuffer();
      }
    }

    public void ReceiveText(string text)
    {
      UTF8Encoding enc = new UTF8Encoding(false);
      byte[] data = enc.GetBytes(text);
      ReceiveData(data);
    }

    public void ReceiveData(byte[] data)
    {
      onDataReceived(data);
    }

    public void Close()
    {
      if (port == null)
        return;

      port.Close();
      CheckPortState();
    }

    bool wasConnected = false;
    private void CheckPortState()
    {
      if (!wasConnected && Connected)
        onConnected();
      if (wasConnected && !Connected)
        onDisconnected("Closed");
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
				TerminalEvent(this, e);
			}
		}

    /// <summary>
    /// Send data to the remote host
    /// </summary>
    /// <param name="Data"></param>
    public void Send(byte[] Data)
    {
      if (Connected)
        port.Write(Data, 0, Data.Length);
    }

    public void Write(string text)
    {
      UTF8Encoding enc = new UTF8Encoding(false);
      byte[] data = enc.GetBytes(text);
      Send(data);
    }

    private ConfigList _configData = new ConfigList();
    public ConfigList ConfigData
    {
      get
      {
        return _configData;
      }
    }

		private FlowControl _lines = new FlowControl();
    public FlowControl Lines
    {
      get { return _lines; }
    }

    public int BytesWaiting
    {
      get
      {
        if (!Connected)
          return 0;
        return (port.BytesToRead);
      }
    }

    public void FlushBuffer()
    {
      if (BytesWaiting > 0)
      {
        int bytes = port.BytesToRead;
        byte[] buffer = new byte[bytes];
        port.Read(buffer, 0, bytes);
        ReceiveData(buffer);
      }
    }

    public void Break()
    {
      if (Connected)
      {
        port.BreakState = true;
        System.Threading.Thread.Sleep(200);
        port.BreakState = false;
      }
    }

	}
}
