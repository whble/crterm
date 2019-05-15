using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRTERM.Common;

namespace CRTERM
{
	public interface ICommProvider
	{
    /// <summary>
    /// Configuration information for this provider. You should use this information
    /// to initialize your provider and ensure that data is saved to this list before
    /// shutdown.
    /// </summary>
		ConfigList ConfigData
		{
			get;
		}
		/// <summary>
		/// Data was received from the remote system. 
		/// </summary>
		event DataReceivedEventHandler DataReceived;
		/// <summary>
		/// Push data in to the receive buffer and raise the DataReceived event
		/// </summary>
		/// <param name="data">Byte data to add to the buffer</param>
		void ReceiveData(byte[] data);
		/// <summary>
		/// Writes data to the remote system
		/// </summary>
		/// <param name="data"></param>
		void Send(byte[] data);
	}
}
