﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRTerm.Transfer
{
    public partial class TransferControl : UserControl
    {
        public TransferControl()
        {
            InitializeComponent();
        }

        DateTime startTime = DateTime.Now;
        TimeSpan elapsedTime = new TimeSpan();
        TimeSpan estimatedTime = new TimeSpan();

        public event EventHandler CancelClicked;

        public string Protocol
        {
            get { return protocolLabel.Text; }
            set { protocolLabel.Text = value; }
        }

        public string Filename
        {
            get { return filenameLabel.Text; }
            set { filenameLabel.Text = value; }
        }

        public string Operation
        {
            get { return operationLabel.Text; }
            set { operationLabel.Text = value; }
        }

        private long _bytesToSend;
        public long BytesToSend
        {
            get { return _bytesToSend; }
            set
            {
                BytesToSendLabel.Text = value.ToString();
                _bytesToSend = value;
                if (progressBar1.Value > (int) value)
                    progressBar1.Value = (int) value;
                progressBar1.Maximum = (int) value;
            }
        }

        private long _bytesSent;
        public long BytesSent
        {
            get { return _bytesSent; }
            set
            {
                if (_bytesSent == 0 && value > 0)
                    ClearTimer();

                bytesSentLabel.Text = value.ToString();
                _bytesSent = value;
                progressBar1.Value = Math.Min((int)value, progressBar1.Maximum);
                UpdateTimer();
            }
        }

        public void ClearTimer()
        {
            startTime = DateTime.Now;
            elapsedTime = new TimeSpan(0);
            estimatedTime = new TimeSpan(0);

            UpdateTimer();
        }

        public void UpdateTimer()
        {
            float bps = 0;
            elapsedTime = DateTime.Now - startTime;
            if (BytesToSend > 0 && BytesSent > 0)
            {
                bps = BytesSent / BytesToSend;
                estimatedTime = new TimeSpan(0, 0, (int)(BytesToSend / bps));
            }

            elapsedTimeLabel.Text = elapsedTime.ToString();
            estimatedTimeLabel.Text = estimatedTime.ToString();
            remainingLabel.Text = (estimatedTime - elapsedTime).ToString();
        }

        private void CancelTransfer_Click(object sender, EventArgs e)
        {
            CancelClicked?.Invoke(sender, e);
        }
    }

}
