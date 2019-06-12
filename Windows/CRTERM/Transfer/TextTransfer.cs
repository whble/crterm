using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace CRTerm.Transfer
{
    /// <summary>
    /// Text Uploader. Uplaoder sends one byte, waits for an echo, then sends the next byte. 
    /// Control characters (except CR) will not be sent. When CR is sent, uploader will wait
    /// for a Line Feed to send the next byte. If no data is received after Timeout milliseconds,
    /// the next byte will be sent. 
    /// </summary>
    class TextTransfer : ITransferProtocol
    {
        /// <summary>
        /// Position of next character in transmit buffer
        /// </summary>
        private int pos = 0;
        /// <summary>
        /// delay timer for sending text. 
        /// </summary>
        Timer sendTimer = new Timer();
        char waitFor = '\0';
        char lastReceived = '\0';
        private Session _currentSession;
        bool ClearToSend = false;
        private TransferControl _tc = null;

        public string Text = "";
        public string Filename = "";
        public int Timeout = 1000;
        private bool Finished = false;

        public Session CurrentSession
        {
            get
            {
                return this._currentSession;
            }

            set
            {
                this._currentSession = value;
                if (value != null)
                    _currentSession.Transfer = this;
            }
        }

        public TransferControl TransferControl
        {
            get
            {
                return this._tc;
            }

            set
            {
                this._tc = value;
            }
        }

        public void Cancel()
        {
            Finished = true;
            Detach();
        }

        public void Receive()
        {
            throw new NotImplementedException();
        }

        public void SendFile()
        {

        }

        public void Send()
        {
            sendTimer.Elapsed += Sending_SendDelay_Elapsed;
            sendTimer.Interval = 1;
            sendTimer.AutoReset = false;

            try
            {
                if (Filename != "")
                    Text = System.IO.File.ReadAllText(Filename);
                pos = 0;
                SendNextChar();
            }
            catch (Exception ex)
            {
                CurrentSession.Display.PrintSeparater();
                CurrentSession.Display.PrintLine();
                CurrentSession.Display.PrintLine("Could not send file \"" + Filename + "\"");
                CurrentSession.Display.PrintLine(ex.Message);
                CurrentSession.Display.PrintLine("Transfer Terminated");
                Detach();
            }

        }

        private void Sending_SendDelay_Elapsed(object sender, ElapsedEventArgs e)
        {
            Timer t = sender as Timer;
            if (t == null)
                return;

            t.Stop();
            SendNextChar();
        }

        private void SendNextChar()
        {
            sendTimer.Stop();

            char c = Text[pos++];
            if (c >= ' ' || c == '\r')
            {
                waitFor = c;
                if (c == '\r')
                    waitFor = '\n';
                CurrentSession.Transport.Send((byte)c);
                sendTimer.Interval = 1000;
            }
            else
            {
                sendTimer.Interval = 1;
            }

            if (pos < Text.Length)
                sendTimer.Start();
            else
                Detach();
        }

        public void Detach()
        {
            sendTimer.Elapsed -= Sending_SendDelay_Elapsed;
            CurrentSession.Transfer = null;
            sendTimer.Enabled = false;
        }

        public void ReceiveData(IReceiveChannel receiver)
        {
            while (receiver.BytesWaiting > 0)
            {

                lastReceived = (char)receiver.Read();

                if (waitFor == '\0' || lastReceived == waitFor)
                    sendTimer.Interval = 1;

                CurrentSession.Terminal.ProcessReceivedCharacter(lastReceived);
            }
        }

        public void Send(string Filename)
        {
            throw new NotImplementedException();
        }

        public void Receive(string Filename)
        {
            throw new NotImplementedException();
        }

        public void SendFile(Session CurrentSession, string Filename)
        {
            throw new NotImplementedException();
        }

        public void ReceiveFile(Session CurrentSession, string Filename)
        {
            throw new NotImplementedException();
        }
    }
}
