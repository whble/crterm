using System;
using System.Collections.Generic;
using CRTERM.Common;

namespace CRTERM.Transport
{
  /// <summary>
  /// Defines a physical transport, such as a serial port, TCP connection, UDP datagrams, or loopback connection.
  /// Could also be a software emulation, such as a PSK modem or Packet modem, or a link to a softwawre Terminal such as
  /// AGWPE.
  /// <para>An Terminal should talk to the modem.</para>
  /// <para>The modem should talk to the transport</para>
  /// <para>The Transport talks to the remote system</para>
  /// </summary>
  public interface ITransport : ICommProvider
  {
    /// <summary>
    /// Underlying connection is opened. For modems or TNCs, this does not actually
    /// mean the call was completed.
    /// </summary>
    event TerminalEventHandler TerminalEvent;
    /// If true, the modem or underlying network connection is still connected.
    /// </summary>
    bool Connected { get; }
    /// <summary>
    /// Connect the underlying transport (the wire or the network connection)
    /// </summary>
    void Open();
    /// <summary>
    /// Disconnect the transport (close the serial port or network connection)
    /// </summary>
    void Close();

    /// <summary>
    /// RS-232 flow control lines are provided for compatibility with serial protocols.
    /// These may be useless for TCP connections, although you could implement them 
    /// for modem-over-tcp connections. Normally, you would not set the modem-side
    /// lines (CTS, DSR, CD, RING), but that ability is provided for virtual serial connections.
    /// For example: when emulating a serial port to connect to a BBS over Telnet.
    /// </summary>
    FlowControl Lines { get; }

    /// <summary>
    /// Returns > 0 if data is waiting to be read.
    /// </summary>
    int BytesWaiting { get; }
    void FlushBuffer();

    void Break();
  }
}
