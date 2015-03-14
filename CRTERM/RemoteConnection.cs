using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRTERM
{
	/// <summary>
	/// Connection is responsible for saving the information necessary to
	/// connect to a specific remote system. This will save the configuration
	/// data for your transport and emulators.
	/// </summary>
	public class RemoteConnection
	{
		private Transport.ITransport _transport=null;
		public Transport.ITransport Transport
		{
			get { return _transport; }
			set { _transport = value; }
		}

		private Modem.IModem _modem = null;
		internal Modem.IModem Modem
		{
			get { return _modem; }
			set { _modem = value; }
		}

		private Emulator.IEmulator _emulator = null;
		internal Emulator.IEmulator Emulator
		{
			get { return _emulator; }
			set { _emulator = value; }
		}
	}
}
