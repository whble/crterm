namespace CRTerm
{
    public interface IHasStatus
    {
        /// <summary>
        /// The status of this device has changed. (Connected, Disconnected, etc.)
        /// </summary>
        event StatusChangeEventHandler StatusChangedEvent;
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
}