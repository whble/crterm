using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRTERM.Transport
{
	public class FlowControl
	{
		public delegate void FlowControlEventHandler(FlowControl Lines);
		public event FlowControlEventHandler StateChanged;
		void onLineStateChanged()
		{
			if (StateChanged != null)
				StateChanged(this);
		}

		private bool _requestToSend;      // RTS: Computer sets high when it wants to send data
		public bool RequestToSend
		{
			get { return _requestToSend; }
			set
			{
				_requestToSend = value;
				onLineStateChanged();
			}
		}

		private bool _clearToSend;        // CTS: Modem sets high when it can accept data
		public bool ClearToSend
		{
			get { return _clearToSend; }
			set
			{
				_clearToSend = value;
				onLineStateChanged();
			}
		}

		private bool _dataTerminalReady;  // DTR: Computer is on. Set Low to hang up a modem		
		public bool DataTerminalReady
		{
			get { return _dataTerminalReady; }
			set
			{
				_dataTerminalReady = value;
				onLineStateChanged();
			}
		}

		private bool _dataSetReady;       // DSR: Modem is physically connected and ready to accept commands.
		public bool DataSetReady
		{
			get { return _dataSetReady; }
			set
			{
				_dataSetReady = value;
				onLineStateChanged();
			}
		}

		private bool _carrierDetect;      // CD: Modem is connected to another modem.
		public bool CarrierDetect
		{
			get { return _carrierDetect; }
			set
			{
				_carrierDetect = value;
				onLineStateChanged();
			}
		}

		private bool _ring;               // RING: Phone is ringing. 
		public bool Ring
		{
			get { return _ring; }
			set
			{
				_ring = value;
				onLineStateChanged();
			}
		}
	}
}
