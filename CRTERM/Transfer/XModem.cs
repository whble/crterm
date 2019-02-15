using System;
using System.Threading;
using System.IO;
using System.IO.Ports;
using CRTerm.IO;
using TerminalUI.Terminals;

namespace CRTerm.Transfer
{
    public class XModem : ITransferProtocol
    {
        #region Fields
        // an XModem block is 128 bytes + control characters
        const int DATA_LENGTH = 128;
        // 4 control characters makes up 132
        const int BLOCK_LENGTH = 132;
        // NAK asks sender to resend packet
        const byte NAK = 21;
        // ACK means a good packet was received 
        const byte ACK = 6;
        // SOH indicates beginning of a block
        const byte SOH = 1;
        // EOT after the last block means "DONE".
        const byte EOT = 4;
        // ^C during the waiting phase cancels the transfer
        const byte ETX = 3;

        private ITransferDialog _dialog = null;
        private ITransport _transport = null;
        private ConnectionStatusCodes _status = ConnectionStatusCodes.Disconnected;
        RingBuffer dataBuffer = new RingBuffer();
        RingBuffer keyboardBuffer = new RingBuffer();
        RingBuffer terminalBuffer = new RingBuffer();
        Timer timer;
        DateTime nextCheck = DateTime.MinValue;
        /// <summary>
        /// timeout will cause the transfer to fail (if data is not received within 90 seconds)
        /// or a NAK to be resent if data is not received for 7 seconds. 
        /// </summary>
        DateTime nakTimer = DateTime.MaxValue;
        DateTime failTimer = DateTime.MaxValue;
        string filename = @"C:\temp\receive.dat";

        public int FileLength = 0;
        public int FilePosition = 0;
        public int CurrentBlock = 1;
        public int ErrorCount = 0;
        public string LastMessage = "";
        public FileStream stream;

        enum TransferStages
        {
            Waiting,
            Header,
            Data,
            Acknowledgement,
            Complete,
            Fail
        };
        TransferStages Stage = TransferStages.Waiting;

        enum TransferModes
        {
            None,
            Sending,
            Receiving
        }
        TransferModes Mode = TransferModes.None;

        #endregion

        #region Events
        public event StatusChangeEventHandler StatusChangedEvent;
        public event DataReadyEventHandler DataReceived;
        #endregion

        #region Properties
        public ITransport Transport
        {
            get { return _transport; }
            set
            {
                if (value == _transport)
                    return;
                if (_transport != null)
                    _transport.DataReceived -= this.ReceiveData;
                _transport = value;
                if (_transport != null)
                    _transport.DataReceived += this.ReceiveData;
            }
        }

        public ConnectionStatusCodes Status
        {
            get
            {
                return this._status;
            }

            protected set
            {
                this._status = value;
                UpdateStatus();
                System.Windows.Forms.Application.DoEvents();
            }
        }

        public string StatusDetails
        {
            get
            {
                return Mode.ToString() + " Block: " + CurrentBlock;
            }
        }

        public int BytesAvailable
        {
            get
            {
                return terminalBuffer.Count;
            }
        }

        #endregion
        #region Constructors
        #endregion

        #region Private and protected methods
        #endregion

        #region Public methods
        /// <summary>
        /// Receives commands from the terminal. Data is not passed through
        /// to the serial port. 
        /// </summary>
        /// <param name="Data"></param>
        public void SendByte(byte Data)
        {
            if (Data == 3)
            {
                Print("Canceled by user.");
                CancelTransfer();
            }
        }

        protected virtual void SendNAK()
        {
            Transport.Send(NAK);
        }

        protected virtual void SendACK()
        {
            Transport.Send(ACK);
        }

        protected virtual void Print(string s)
        {
            throw new NotImplementedException();
        }

        public void Open(ITransport Port)
        {
            this.Transport = Port;
        }

        public void Close()
        {
            Transport = null;
        }

        public void ReceiveFile(Session CurrentSession, string Filename)
        {
            Open(CurrentSession.Transport);
            Print("Receiving file: " + filename + "\r\n");

            string fn = filename;
            int fc = 0;
            while (System.IO.File.Exists(fn))
            {
                fc += 1;
                fn = filename + "." + fc.ToString("d3");
                if (fc >= 1000)
                    throw new Exception("Could not save file \"" + filename + "\".");
            }
            stream = new FileStream(fn, FileMode.Create);

            CurrentBlock = 1;
            FilePosition = 0;
            Open(CurrentSession.Transport);
            Mode = TransferModes.Receiving;
            Stage = TransferStages.Waiting;
            ResetNAKTimer();
            SetTimer();
            SendNAK();
        }

        private void ResetNAKTimer()
        {
            nakTimer = DateTime.Now.AddSeconds(7);
        }

        private void ResetFailTimer()
        {
            failTimer = DateTime.Now.AddSeconds(30);
        }

        private void SetTimer()
        {
            if (Mode == TransferModes.Receiving)
            {
                if (Stage == TransferStages.Waiting)
                {
                    timer = new Timer(ReceiveTimeoutCheck, this, 1000, 1000);
                }
            }

        }

        private void StopTimer()
        {
            timer?.Dispose();
        }

        public void ReceiveTimeoutCheck(object stateInfo)
        {
            if (!(stateInfo is XModem x))
                return;
            if (DateTime.Now < nextCheck)
                return;

            // if no data is received for 90 seconds, hard fail
            if (FailTimerExpired())
            {
                Print("Canceled due to timeout.");
                CancelTransfer();
                return;
            }

            switch (Stage)
            {
                // NAK is required to get this party started
                case TransferStages.Waiting:
                case TransferStages.Header:
                case TransferStages.Data:
                case TransferStages.Acknowledgement:
                    if (NAKTimerExpired())
                    {
                        SendNAK();
                        ResetNAKTimer();
                    }
                    if (NAKTimerExpired())
                    {
                        SendNAK();
                        ResetNAKTimer();
                    }
                    break;
                case TransferStages.Complete:
                case TransferStages.Fail:
                    StopTimer();
                    break;
                default:
                    break;
            }
        }

        private bool NAKTimerExpired()
        {
            return DateTime.Now > nakTimer;
        }

        private bool FailTimerExpired()
        {
            return DateTime.Now > failTimer;
        }

        public void CancelTransfer()
        {
            Stage = TransferStages.Fail;
            Close();
        }

        public virtual int BytesWaiting
        {
            get
            {
                return terminalBuffer.Count;
            }
        }

        public ITransferDialog Dialog
        {
            get
            {
                return this._dialog;
            }

            set
            {
                this._dialog = value;
            }
        }

        public bool ClearToSend { get; }
        public Session CurrentSession { get; set; }

        public virtual byte ReadByte()
        {
            if (terminalBuffer.Count > 0)
                return terminalBuffer.Read();
            return 0;
        }

        protected void ReadAllData()
        {
            while (Transport.BytesWaiting > 0)
            {
                dataBuffer.Add(Transport.ReadByte());
            }
        }

        public virtual void ReceiveData(IBuffered receiver)
        {
            ResetNAKTimer();
            ResetFailTimer();
            ReadAllData();

            switch (Mode)
            {
                case TransferModes.None:
                    break;
                case TransferModes.Sending:
                    break;
                case TransferModes.Receiving:
                    AppendData();
                    break;
                default:
                    break;
            }
        }

        protected virtual void AppendData()
        {
            byte b = dataBuffer.Peek();
            if (dataBuffer.Count >= BLOCK_LENGTH)
            {
                // SOH begins an XMODEM block
                // read until we get a valid SOH and then 
                // enter the block.
                if (b == SOH)
                {
                    Stage = TransferStages.Data;
                    ProcessBuffer();
                }
                else
                    dataBuffer.Read();
            }
            if (b == ETX) // cancel
            {
                Print("Canceled by remote host.");
                CancelTransfer();
            }
            else if (b == EOT)
            {
                SendACK();
                CompleteReceive();
            }
        }

        private void CompleteReceive()
        {
            int fc = 0;
            string fn = filename;
            FilePosition += DATA_LENGTH;
            Stage = TransferStages.Complete;
            stream.Close();
            Close();
        }

        // xmodem format is 
        // <SOH>
        // Block
        // 255-Block
        // 128 bytes of data
        // Check Digit (sum of data bytes & 0xff)
        // If block is valid, reply with <ACK>
        // otherwise reply with <NAK>
        // finally, if the block is <next block number> append data. 
        // If block is <last block number>, re-write previous block
        private void ProcessBuffer()
        {
            Print("Receiving block " + CurrentBlock + " \r");

            byte b = dataBuffer.Read();
            byte checksum = 0;
            int blockNo;
            int blockCheck;
            if (b != SOH)
                return;

            // Check the block number and the check-block
            // if they don't match, dump and NAK
            blockNo = dataBuffer.Read();
            blockCheck = dataBuffer.Read();
            if (blockNo != (255 - blockCheck))
            {
                Print("\nInvalid block number: " + blockNo.ToString() + " " + blockCheck.ToString() + "\r\n");
                dataBuffer.Clear();
                SendNAK();
                return;
            }

            // if the received block number is the NEXT block
            // advance the write pointer. Otherwise, we're getting
            // the previous block again, so re-write the last block
            if (blockNo == ((CurrentBlock + 1) & 0xff))
            {
                CurrentBlock += 1;
                FilePosition += DATA_LENGTH;
            }
            stream.Seek(FilePosition, SeekOrigin.Begin);

            // get the block data 
            for (int i = 0; i < DATA_LENGTH; i++)
            {
                b = dataBuffer.Read();
                stream.WriteByte(b);
                checksum += b;
            }

            // get the checksum and send either an <ACK> or <NAK> 
            // depending on whether it matches
            b = dataBuffer.Read();
            if (b == checksum)
                SendACK();
            else
            {
                Print("\nInvalid Checksum. Expected " + checksum + " got " + b.ToString() + "\r\n");
                dataBuffer.Clear();
                SendByte(NAK);
            }
        }

        public void Attach()
        {
            
        }

        public void Detach()
        {
            throw new NotImplementedException();
        }

        public void SendFile(string Filename)
        {
            throw new NotImplementedException();
        }

        public void ReceiveFile(string Filename)
        {
            throw new NotImplementedException();
        }

        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public void UpdateStatus()
        {
            //StatusEventArgs eventArgs = new StatusEventArgs(this.Status1, this.StatusDetails);
            //StatusChangedEvent?.Invoke(this, eventArgs);
        }

        public int ReadData(byte[] Data, int Count)
        {
            throw new NotImplementedException();
        }

        public void Send()
        {
            throw new NotImplementedException();
        }

        public void Receive()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
