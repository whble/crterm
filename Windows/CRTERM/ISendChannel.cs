namespace CRTerm
{
    public interface ISendChannel
    {
        /// <summary>
        /// Object can send data: transport is connected, buffer is not full, flow control is open.
        /// </summary>
        /// <returns></returns>
        bool ClearToSend { get; }
        // Send a byte to the remote system.
        void Send(byte Data);
    }
}