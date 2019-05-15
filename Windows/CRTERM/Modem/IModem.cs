using System;
using System.Collections.Generic;
using CRTERM.Common;

namespace CRTERM.Modem
{
	/// <summary>
	/// Provides the logical modem layer commands to connect to a remote system.
	/// <para>An Terminal should talk to the modem.</para>
	/// <para>The modem should talk to the transport</para>
	/// <para>The Transport talks to the remote system</para>
	/// </summary>
	public interface IModem : ICommProvider
	{
		Transport.ITransport Transport { get; set; }
		/// <summary>
		/// True if the modem is connected to the remote host. This should always
		/// be false if the physical connection is disconnected. This will be true
		/// after the modem has dialed and gotten an answer.
		/// </summary>
		bool Connected { get; }
		event TerminalEventHandler TerminalEvent;
		void Connect();
		void Disconnect();
	}
}
