/* Define EVENT_Driven to raise event when data is received.
 * but can use a little more CPU time.
 */
#define EVENT_DRIVEN

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CRTerm.IO
{
  class SerialIO
  {
    private string _PortName = "COM4";
    private System.IO.Ports.SerialPort port;
    //public RingBuffer recData = new RingBuffer();
    volatile private bool ThreadDone = false;
#if EVENT_DRIVEN
#else
    private Thread ReadThread;
#endif

    byte[] recData = new byte[256];
    //int pos;
    int MessageLength;

    /// <summary>
    /// The serial port used to communicate with the PCR
    /// </summary>
    public string PortName
    {
      get { return _PortName; }
      set { _PortName = value; }
    }

    public bool Opened
    {
      get
      {
        if (port != null)
          return port.IsOpen;
        else
          return false;
      }
    }

    public void Open()
    {
      port = new System.IO.Ports.SerialPort(_PortName);
      port.BaudRate = 9600;
      port.Parity = System.IO.Ports.Parity.None;
      port.DataBits = 8;
      port.StopBits = System.IO.Ports.StopBits.One;

      try
      {
        port.Open();
#if EVENT_DRIVEN
        port.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(port_DataReceived);
#else
        ReadThread = new Thread(ReadThreadMain);
        ReadThread.Start();
#endif
      }
      catch (Exception ex)
      {
        // if the port can't be opened, allow the program to run
        // but with no data communication. However, the main form should handle this
        throw new CRTException("COM Port not available: " + PortName + "\n" +
          "Edit the Application config file to set the correct COM port\n" +
          ex.Message);
      }
    }

    internal void Close()
    {
      ThreadDone = true;
#if ! EVENT_DRIVEN
      ReadThread.Join();
#endif
      port.DataReceived -= port_DataReceived;
      port.Close();
    }

    public void ReadThreadMain()
    {
      while (ThreadDone == false)
      {
        while (DataWaiting() && ThreadDone == false)
          port_DataReceived(null, null);
        System.Threading.Thread.Sleep(100);
      }
    }

    void port_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
    {
      //readBuffer();
      //if (ResponseValid())
      //{
      //  PCREventArgs response = new PCREventArgs();
      //  response.Data = recData;
      //  response.responseNumber = (ResponseNumberEnum)response.Data[0];
      //  if (onResponseReceived != null)
      //    onResponseReceived(response);
      //}
    }

    /// <summary>
    /// Get the expected length of the response in the buffer, including the 2 byte header
    /// and the 2 byte footer.
    /// </summary>
    /// <returns>Expected length of the response message.</returns>
    int ResponseLength()
    {
      int length = recData[2] * 256 + recData[3];
      return length + 6;
    }

    /// <summary>
    /// Check to see if the response in the buffer is complete.
    /// This looks at the length of the response and compares it to the length
    /// bytes in the response (offset 2-3, zero base). If the length is correct
    /// then return True. If the length is not, return false
    /// </summary>
    /// <returns>True if the buffer contains a valid command.</returns>
    bool ResponseValid()
    {
      return MessageLength > 0;
    }

    /// <summary>
    /// Check to see whether there's data ready to process.
    /// </summary>
    /// <returns></returns>
    bool DataWaiting()
    {
      if (port != null && (port.BytesToRead > 6))
        return true;
      else
        return false;
    }

    virtual public void SendCommand(byte[] data)
    {
      if (port.IsOpen) {
        byte[] buffer;
        int length = data.Length + 6;
        buffer = new byte[length];
        buffer[0] = 0x5a;
        buffer[1] = 0xa5;
        buffer[2] = (byte)(data.Length / 256);
        buffer[3] = (byte)(data.Length & 0x00ff);
        System.Array.Copy(data, 0, buffer, 4, data.Length);
        buffer[length - 2] = 0xed;
        buffer[length - 1] = 0xed;
        port.Write(buffer, 0, buffer.Length);
      }
    }

    void readBuffer()
    {
      int x;
      MessageLength=0;
      //pos=0;
      if(port.BytesToRead > 6)
      {
        x = 0;
        while(x != 0x5a)
          x=port.ReadByte();
        x = port.ReadByte();
        if (x != 0xa5)
          return;

        MessageLength = port.ReadByte() * 256 + port.ReadByte();
        while (port.BytesToRead < MessageLength + 2)
          System.Threading.Thread.Sleep(1);
        port.Read(recData, 0, MessageLength+2);
      }
    }

  }
}
