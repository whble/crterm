using System;
using System.Collections.Generic;
using TerminalUI;

namespace CRTerm.IO
{
    public interface ITransport : IConfigurable, IReceiveChannel, IHasStatus
    {
        /// <summary>
        /// Connect to the remote endpoint. Dial the modem, etc, if appropriate.
        /// </summary>
        void Connect();
        /// <summary>
        /// Disconnect from the remote endpoint. Hang up modem or close network connection.
        /// </summary>
        void Disconnect();
        /// <summary>
        /// Address or phone number to connect to. 
        /// </summary>
        string Address { get; set; }
        /// <summary>
        /// Send one byte on this connection.
        /// </summary>
        /// <param name="Data"></param>
        void Send(byte Data);
        /// <summary>
        /// Send a series of bytes on this connection. 
        /// </summary>
        /// <param name="Data"></param>
        void Send(byte[] Data);
        /// <summary>
        /// Update the internal status variables and fire callbacks.
        /// </summary>
        void UpdateStatus();
    }
}
