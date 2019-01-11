using System;
using CRTerm.Config;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRTerm
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Session CurrentSession = null;

        private void MainWindow_Load(object sender, EventArgs e)
        {
            CurrentSession = new Session
            {
                FrameBuffer = this.frameBuffer1
            };
            CurrentSession.Init();
            MainWindow_SizeChanged(sender, e);
            timer1.Enabled = true;
        }

        private void LoadPortOptions()
        {
            PortOptionsButton.DropDownItems.Clear();
            string currentPortName = CurrentSession.Transport?.Address;


            List<string> portNames = CurrentSession.GetPortNames();
            foreach (var portName in portNames)
            {
                AddPortItem(portName, currentPortName);
            }
        }

        void AddPortItem(string Name, string CurrentItem = "")
        {
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Text = Name;
            item.DisplayStyle = ToolStripItemDisplayStyle.Text;
            if (Name == CurrentItem)
                item.Checked = true;
            item.Click += PortName_Clicked;
            PortOptionsButton.DropDownItems.Add(item);
        }

        private void PortName_Clicked(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item == null)
                return;

            if (item.Text == "Test")
            {
                InitTestPort();
                return;
            }

            if (item.Text == "Telnet")
            {
                InitTelnet();
                return;
            }

            IO.SerialIOPort port = CurrentSession.Transport as IO.SerialIOPort;
            if (port == null)
            {
                int baudRate = 9600;
                IO.SerialIOPort p = CurrentSession.Transport as IO.SerialIOPort;
                if (p != null)
                    baudRate = p.BaudRate;
                port = new IO.SerialIOPort();
                port.BaudRate = baudRate;
            }

            port.Address = item.Text;
            if (port.Status == ConnectionStatusCodes.Connected)
                port.Disconnect();
            CurrentSession.Transport = port;
            port.Connect();
            UpdateStatus();
        }

        private void InitTelnet()
        {
            //throw new NotImplementedException();
        }

        private void InitTestPort()
        {
            CurrentSession.Transport = new IO.TestPort();
        }

        void Session_StatusChanged(object Sender, ConnectionStatusCodes NewStatus)
        {
            UpdateStatus();
        }

        public void UpdateStatus()
        {
            if (CurrentSession == null)
                return;

            if (CurrentSession.Transport == null)
                return;

            PortStatusLabel.Text = CurrentSession.Transport.Status.ToString();
            PortNameLabel.Text = CurrentSession.Transport.Name;
            PortStatus.Text = CurrentSession.Transport.StatusDetails;
            TerminalNameLabel.Text = CurrentSession.Terminal.StatusDetails;
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                CurrentSession.Connect();
                UpdateStatus();
            }
            catch (CRTException ex)
            {
                CurrentSession.FrameBuffer.PrintLine("Could not connect");
                CurrentSession.FrameBuffer.PrintLine(ex.Message);
            }
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            CurrentSession.Disconnect();
            UpdateStatus();
        }

        private void ClearScreenButton_Click(object sender, EventArgs e)
        {
            frameBuffer1.Clear();
        }

        private void bSDELToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Terminals.BasicTerminal term = CurrentSession.Terminal as Terminals.BasicTerminal;
            if (term == null)
                return;

            term.BackspaceDeleteMode = !term.BackspaceDeleteMode;
            bSDELToolStripMenuItem.Checked = term.BackspaceDeleteMode;
            UpdateStatus();
        }

        private void portToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            IO.SerialIOPort p = CurrentSession.Transport as IO.SerialIOPort;
            if (p == null)
                return;
            UpdateStatus();
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            string s = "";
            //string test = System.IO.File.ReadAllText("bastest.txt");
            if (Clipboard.ContainsText())
                s = Clipboard.GetText();
            CurrentSession.Terminal.SendString(s);
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            Transfer.XModem xModem = new Transfer.XModem();
            xModem.ReceiveFile(CurrentSession, @"C:\temp\receive.txt");
        }

        private void BaudRate_Clicked(object sender, EventArgs e)
        {
            ToolStripMenuItem button = sender as ToolStripMenuItem;
            if (button == null)
                return;
            IO.SerialIOPort port = CurrentSession.Transport as IO.SerialIOPort;
            if (port == null)
                return;

            int baud = int.Parse(button.Text);
            //port.Disconnect();
            port.BaudRate = baud;
            //port.Connect();
            UpdateStatus();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Configuration config = new Configuration();
            config.ConfigurableObjects.Add(CurrentSession);
            config.ConfigurableObjects.Add(CurrentSession.Transport);
            config.ConfigurableObjects.Add(CurrentSession.Terminal);
            config.SaveConfiguration();
        }

        private void PortOptionsButton_DropDownOpening(object sender, EventArgs e)
        {
            LoadPortOptions();
        }

        private void BaudRateButton_DropDownOpening(object sender, EventArgs e)
        {
            LoadBaudRates();
        }

        private void LoadBaudRates()
        {
            BaudRateButton.DropDownItems.Clear();

            int currentBaud = 0;
            if (CurrentSession.Transport is IO.SerialIOPort p)
                currentBaud = p.BaudRate;

            for (int i = 300; i <= 115200; i *= 2)
            {
                AddBaudRate(i, currentBaud);
            }
        }

        void AddBaudRate(int Baud, int CurrentBaud)
        {
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Text = Baud.ToString();
            item.DisplayStyle = ToolStripItemDisplayStyle.Text;
            if (Baud == CurrentBaud)
                item.Checked = true;
            item.Click += BaudRate_Clicked;
            BaudRateButton.DropDownItems.Add(item);
        }

        private void Upload_Paste_Click(object sender, EventArgs e)
        {
            Transfer.ITransferProtocol t = new Transfer.TextTransfer();
            t.CurrentSession = this.CurrentSession;
            t.Send();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Transfer.TextTransfer t = new Transfer.TextTransfer();
            t.CurrentSession = this.CurrentSession;
            if (Clipboard.ContainsText())
            {
                t.Text = Clipboard.GetText();
                t.Send();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (CurrentSession.Transfer != null)
                CurrentSession.Transfer.Cancel();
        }

        private void BasicButton_Click(object sender, EventArgs e)
        {
            ToolStripButton b = sender as ToolStripButton;
            if (b == null)
                return;

            b.Checked = !b.Checked;

            CurrentSession.Terminal.BasicMode = b.Checked;
        }

        private void MainWindow_SizeChanged(object sender, EventArgs e)
        {
        }

        private void SendText(string Data)
        {
            if (string.IsNullOrEmpty(Data))
                return;

            Transfer.TextTransfer t = new Transfer.TextTransfer();
            t.CurrentSession = this.CurrentSession;
            if (Clipboard.ContainsText())
            {
                t.Text = Data;
                t.Send();
            }
        }

        private void EntryText_Enter(object sender, EventArgs e)
        {
            frameBuffer1.CursorEnabled = false;
        }

        private void EntryText_Leave(object sender, EventArgs e)
        {
            frameBuffer1.CursorEnabled = true;
        }
    }
}
