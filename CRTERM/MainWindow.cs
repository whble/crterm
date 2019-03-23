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
using TerminalUI;
using TerminalUI.Terminals;


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
            CurrentSession = new Session();
            CurrentSession.Display = this.Crt;
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
            TerminalNameLabel.Text = CurrentSession.Terminal_StatusDetails;
            EchoStatus.Text = CamelToSpace(Crt.EchoMode.ToString());
        }

        private string CamelToSpace(string Name)
        {
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < Name.Length; i++)
            {
                char c = Name[i];
                if (i > 0 && c >= 'A' && c <= 'Z')
                    s.Append(' ');
                s.Append(c);
            }

            return s.ToString();
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
                CurrentSession.Display.PrintLine("Could not connect");
                CurrentSession.Display.PrintLine(ex.Message);
            }
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            CurrentSession.Disconnect();
            UpdateStatus();
        }

        private void ClearScreenButton_Click(object sender, EventArgs e)
        {
            Crt.Clear();
        }

        private void bSDELToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicTerminal term = CurrentSession.Terminal as BasicTerminal;
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
            string fn = Path.ChangeExtension(Path.GetTempFileName(), ".xmodem");
            Crt.PrintLine("Receiving to " + fn);
            xModem.ReceiveFile(CurrentSession, fn);
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

            int[] baudRates = new int[] { 300, 1200, 2400, 9600, 19200, 38400, 57600, 115200 };
            for (int i = 0; i<baudRates.Length; i++)
            {
                AddBaudRate(baudRates[i], currentBaud);
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
            if (b.Checked)
                Crt.EchoMode = EchoModes.FullScreen;
            else
                Crt.EchoMode = EchoModes.EchoOff;

            UpdateStatus();
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
            Crt.CursorEnabled = false;
        }

        private void EntryText_Leave(object sender, EventArgs e)
        {
            Crt.CursorEnabled = true;
        }
    }
}
