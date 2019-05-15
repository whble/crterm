using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRTerm
{
    /*
    public delegate void DataChannelEvent(IDataChannel dataChannel);
    public interface IDataChannel
    {
        /// <summary>
        /// Object is connected to the remote host and is clear to send data.
        /// </summary>
        /// <returns></returns>
        bool ClearToSend { get; }
        // Send a byte to the remote system.
        void SendByte(byte Data);
        /// <summary>
        /// Notify an upstream object that data is waiting to be read. Recipient can bubble 
        /// this up or read the data directly.
        /// </summary>
        event DataReceivedEventHandler DataReceivedEvent;
        void DataReceived(IDataChannel dataChannel);
        /// <summary>
        /// number of bytes waiting to be read
        /// </summary>
        int BytesWaiting { get; }
        /// <summary>
        /// retrieve a byte from the receive buffer
        /// </summary>
        /// <returns></returns>
        byte ReadByte();
        /// <summary>
        /// The status of this device has changed. (Connected, Disconnected, etc.)
        /// </summary>
        event DataChannelEvent StatusChangedEvent;
        /// <summary>
        /// The basic status (Connected, Disconnected, etc.)
        /// </summary>
        ConnectionStatusCodes Status { get; }
        /// <summary>
        /// Detailed status, including connection state, port speed, terminal type, or whatever
        /// the user needs to know about this thing.
        /// </summary>
        string StatusDetails { get; }
    }
    */
}
