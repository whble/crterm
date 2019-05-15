namespace CRTerm
{
    public interface IReceiveChannel : IBuffered
    {
        /// <summary>
        /// Notify an upstream object that data is waiting to be read. Recipient can bubble 
        /// this up or read the data directly.
        /// </summary>
        event DataReadyEventHandler DataReceived;
        /// <summary>
        /// Tell a receiving class to go get data from this object
        /// </summary>
        /// <param name="dataChannel"></param>
        void ReceiveData(IReceiveChannel dataChannel);
    }
}