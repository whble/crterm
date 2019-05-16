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

        private Session Session = null;

        private void MainWindow_Load(object sender, EventArgs e)
        {
            Session = new Session();
            Session.Display = this.Crt;
            Session.Init();
            MainWindow_SizeChanged(sender, e);
            timer1.Enabled = true;
        }

        private void LoadPortOptions()
        {
            PortOptionsButton.DropDownItems.Clear();
            string currentPortName = Session.Transport?.Address;


            List<string> portNames = Session.GetPortNames();
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
            try
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

                IO.SerialIOPort port = Session.Transport as IO.SerialIOPort;
                if (port == null)
                {
                    int baudRate = 9600;
                    IO.SerialIOPort p = Session.Transport as IO.SerialIOPort;
                    if (p != null)
                        baudRate = p.BaudRate;
                    port = new IO.SerialIOPort();
                    port.BaudRate = baudRate;
                }

                port.Address = item.Text;
                if (port.Status == ConnectionStatusCodes.Connected)
                    port.Disconnect();
                Session.Transport = port;
                port.Connect();
                UpdateStatus();

            }
            catch (Exception ex)
            {
                Crt.Print(ex.Message);
            }
        }

        private void InitTelnet()
        {
            //throw new NotImplementedException();
        }

        private void InitTestPort()
        {
            Session.Transport = new IO.TestPort();
        }

        void Session_StatusChanged(object Sender, ConnectionStatusCodes NewStatus)
        {
            UpdateStatus();
        }

        public void UpdateStatus()
        {
            if (Session == null)
                return;

            if (Session.Transport == null)
                return;

            PortStatusLabel.Text = Session.Transport.Status.ToString();
            PortNameLabel.Text = Session.Transport.Name;
            PortStatus.Text = Session.Transport.StatusDetails;
            TerminalNameLabel.Text = Session.Terminal_StatusDetails;
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
                Session.Connect();
                UpdateStatus();
            }
            catch (CRTException ex)
            {
                Session.Display.PrintLine("Could not connect");
                Session.Display.PrintLine(ex.Message);
            }
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            Session.Disconnect();
            UpdateStatus();
        }

        private void ClearScreenButton_Click(object sender, EventArgs e)
        {
            Crt.Clear();
        }

        private void bSDELToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicTerminal term = Session.Terminal as BasicTerminal;
            if (term == null)
                return;

            term.BackspaceDeleteMode = !term.BackspaceDeleteMode;
            bSDELToolStripMenuItem.Checked = term.BackspaceDeleteMode;
            UpdateStatus();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Session?.ReceiveTimer_Tick(sender, e);
            UpdateStatus();
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            Transfer.XModem xModem = new Transfer.XModem();
            string fn = Path.ChangeExtension(Path.GetTempFileName(), ".xmodem");
            Crt.PrintLine("Receiving to " + fn);
            xModem.ReceiveFile(Session, fn);
        }

        private void BaudRate_Clicked(object sender, EventArgs e)
        {
            ToolStripMenuItem button = sender as ToolStripMenuItem;
            if (button == null)
                return;
            IO.SerialIOPort port = Session.Transport as IO.SerialIOPort;
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
            config.ConfigurableObjects.Add(Session);
            config.ConfigurableObjects.Add(Session.Transport);
            config.ConfigurableObjects.Add(Session.Terminal);
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
            if (Session.Transport is IO.SerialIOPort p)
                currentBaud = p.BaudRate;

            int[] baudRates = new int[] { 300, 1200, 2400, 9600, 19200, 38400, 57600, 115200 };
            for (int i = 0; i < baudRates.Length; i++)
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

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = "";
            if (Clipboard.ContainsText())
                s = Clipboard.GetText();

            Session.Terminal.SendString(s);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (Session.Transfer != null)
                Session.Transfer.Cancel();
            Session.Transfer = null;
        }

        private void BasicButton_Click(object sender, EventArgs e)
        {
            ToolStripButton b = sender as ToolStripButton;
            if (b == null)
                return;

            b.Checked = !b.Checked;
            if (b.Checked)
                Crt.EchoMode = EchoModes.EditMode;
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
            t.CurrentSession = this.Session;
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

        private void xMODEMToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void xModemPCGETToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void echoOnOffToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aSCIIToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void xMODEMToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();

            f.InitialDirectory = Session.DownloadDirectory;
            if (f.ShowDialog() == DialogResult.OK)
            {
                Session.DownloadDirectory = System.IO.Path.GetDirectoryName(f.FileName);

                Crt.PrintSeparater();
                Crt.PrintLine("\r  Downloading " + f.FileName);

                Transfer.ITransferProtocol t = new Transfer.XModem();
                Session.Transfer = t;
                t.ReceiveFile(Session, f.FileName);
            }
        }

        private void AddNewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void DisplayOptionsDropdown_DropDownOpening(object sender, EventArgs e)
        {
            LoadDisplayModes();
        }

        private void LoadDisplayModes()
        {
            DisplayOptionsDropdown.DropDownItems.Clear();
            string currentOption = Session.Display.Columns.ToString() + "x" + Session.Display.Rows.ToString();

			foreach (string option in new string[] { "80x24", "80x25", "80x36", "120x25", "120x36", "120x50" })
            {
                AddDropdownItem(DisplayOptionsDropdown, option, currentOption, DisplaySize_Clicked);
            }
        }

        void AddDropdownItem(ToolStripDropDownButton Parent, string Name, string CurrentItem, System.EventHandler ClickHandler)
        {
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Text = Name;
            item.DisplayStyle = ToolStripItemDisplayStyle.Text;
            if (Name == CurrentItem)
                item.Checked = true;
            item.Click += ClickHandler;
            Parent.DropDownItems.Add(item);
        }

        void DisplaySize_Clicked(object sender, EventArgs e)
        {
            ToolStripMenuItem c = sender as ToolStripMenuItem;
            if (c == null)
                return;

            string[] parts = c.Text.Split('x');
            if (parts.Length < 2)
                return;

            int rows, cols;
            int.TryParse(parts[0], out cols);
            int.TryParse(parts[1], out rows);

            if (rows == 0 || cols == 0)
            {
                Session.Display.PrintAtStart("Cannot set video mode: " + cols.ToString() + "x" + rows.ToString() + "x");
                return;
            }

            Session.Display.SetTextMode(rows, cols);
			Session.Display.TerminalControl_Resize(this, e);
        }

        private void ReceiveTimer_Tick(object sender, EventArgs e)
        {
            if (Session == null)
                return;
            Session.ReceiveTimer_Tick(sender, e);
        }
    }
}
