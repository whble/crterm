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
        int MouseY = 0;
        private static string MEASURE_STRING = new string('W', 80);
        private EchoModes lastEchoMode = EchoModes.EchoOff;

        string[] fonts = new[] {
            "Classic Console",
            "Consolas",
            "Lucida Console",
            "Monospace"
        };

        string StatusText = "CRTerm (c) 2019 Tom Wilson";

        public MainWindow()
        {
            InitializeComponent();
        }

        private Session Session = null;
        private Font StatusFont;

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

            if (FormBorderStyle == FormBorderStyle.None && toolStrip1.Visible && MouseY > toolStrip1.Height)
                SetMenuVisible(false);
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
                Crt.EchoMode = EchoModes.FullScreenEdit;
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

        private void xModemPCGETToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void echoOnOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Session == null || Session.Terminal == null)
                return;

            if (Session.Terminal.EchoMode == EchoModes.LocalEcho)
            {
                Session.Terminal.EchoMode = EchoModes.EchoOff;
            }
            else
            {
                Session.Terminal.EchoMode = EchoModes.LocalEcho;
            }
            lastEchoMode = Session.Terminal.EchoMode;
        }

        private void aSCIIToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void XModem_Send_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();

            f.InitialDirectory = Session.DownloadDirectory;
            if (f.ShowDialog() == DialogResult.OK)
            {
                Session.DownloadDirectory = System.IO.Path.GetDirectoryName(f.FileName);

                Transfer.ITransferProtocol t = new Transfer.XModem();
                t.TransferControl = transferControl1;
                Session.Transfer = t;
                t.SendFile(Session, f.FileName);
            }
        }

        private void XModem_Receive_Click(object sender, EventArgs e)
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

        public void ToggleFullScreen()
        {
            Form f = this;
            if (f == null)
                return;

            // already full screen. Restore to previous state
            if (f.FormBorderStyle == FormBorderStyle.None)
            {
                f.FormBorderStyle = FormBorderStyle.Sizable;
                f.WindowState = FormWindowState.Normal;
                f.TopMost = false;
                SetMenuVisible(true);
            }
            else
            {
                f.TopMost = true;
                f.FormBorderStyle = FormBorderStyle.None;
                f.WindowState = FormWindowState.Maximized;
                SetMenuVisible(false);
            }
        }

        private void SetMenuVisible(bool Visible)
        {
            if (FormBorderStyle != FormBorderStyle.None)
                Visible = true;

            toolStrip1.Visible = Visible;
        }

        private void Crt_ToggleFullScreenRequest(object sender, EventArgs e)
        {
            ToggleFullScreen();
        }

        private void CRT_MouseMove(object sender, MouseEventArgs e)
        {
            MouseY = e.Y;
            if (e.Y < toolStrip1.Height)
                SetMenuVisible(true);
        }

        private void TextCaptureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Session.captureBuffer.Status == CaptureBuffer.CaptureStatusCodes.Capturing)
            {
                Session.captureBuffer.StopCapture();
                SaveFileDialog d = new SaveFileDialog();
                DialogResult r = d.ShowDialog();
                if (r == DialogResult.OK)
                {
                    System.IO.File.WriteAllText(d.FileName, Session.captureBuffer.ToString());
                    Session.captureBuffer.Status = CaptureBuffer.CaptureStatusCodes.Saved;
                }
            }
            else
                Session.captureBuffer.StartCapture();


            UpdateStatus();
        }

        private SizeF MeasureFont(Font font, Graphics g)
        {
            SizeF size = g.MeasureString(MEASURE_STRING, font, int.MaxValue, StringFormat.GenericTypographic);
            size.Width = size.Width / MEASURE_STRING.Length;
            return size;
        }

        private Font GetBestFont(int Rows)
        {
            float RowHeight = 16;
            float ColWidth = 8;
            int testSize = 12;

            Font useFont = this.Font;
            float ch = (float)ClientRectangle.Height / (float)(Rows + 1);
            if (ch < 8)
            {
                ch = 8;
            }

            foreach (string f in fonts)
            {
                using (Font testFont = new Font(
                                           f,
                                           testSize,
                                           FontStyle.Regular,
                                           GraphicsUnit.Pixel))
                {
                    if (testFont.Name == f)
                    {
                        useFont = new Font(f, ch, FontStyle.Regular, GraphicsUnit.Pixel);
                        break;
                    }
                    else
                    {
                    }
                }
            }
            return useFont;
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
        }

        public void UpdateStatus()
        {
            if (Session == null)
                return;

            if (Session.Transport == null)
                return;

            StringBuilder s = new StringBuilder();
            s.Append(CamelToSpace(Session.Transport.Status.ToString()));
            s.Append(" │ ");
            s.Append(Session.Transport.Name);
            s.Append(" │ ");
            s.Append(Session.Transport.StatusDetails);
            s.Append(" │ ");
            s.Append(Session.Terminal.Name);
            s.Append(" │ ");
            s.Append(CamelToSpace(Crt.EchoMode.ToString()));
            s.Append(" │ ");
            s.Append(Session.captureBuffer.StatusText);
            StatusText = s.ToString();

            StatusBox.Refresh();

            UpdatePortMenu();
            UpdateTerminalMode();
        }

        private void StatusBox_Paint(object sender, PaintEventArgs e)
        {
            if (this.StatusFont == null)
                UpdateStatusSize();

            Graphics g = e.Graphics;
            g.Clear(Color.Black);
            g.DrawLine(Pens.Gray, 0, 0, StatusBox.ClientRectangle.Width, 0);
            g.DrawString(StatusText, this.StatusFont, Brushes.Gray, 4, 0);

            //string PressAlt = "Press ALT-M for menu";
            //SizeF sf = g.MeasureString(PressAlt, StatusFont);
            //g.DrawString(PressAlt, this.StatusFont, Brushes.Gray, StatusBox.ClientRectangle.Width - sf.Width, 0);
        }

        private void UpdateStatusSize()
        {
            int rows = 25;
            if (Crt != null)
                rows = Crt.Rows + 1;
            this.StatusFont = GetBestFont(rows);
            StatusBox.Height = this.Font.Height + 8;
        }

        private void Crt_HotkeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Menu)
            {
                switch (e.KeyCode)
                {
                    case Keys.H:
                        ShowHelp();
                        break;
                }
            }
        }

        private void ShowHelp()
        {
            throw new NotImplementedException();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(e.Alt.ToString() + " " + e.KeyCode.ToString());
            //if(e.Alt && e.KeyCode == Keys.M)
            //{
            //    ShowMainMenu();
            //    e.Handled = true;
            //}
        }

        private void ShowMainMenu()
        {
            TextDialog d = new TextDialog();
            Crt.CurrentDialog = d;
            d.Display = Crt;

            d.Add("Connect", "Alt-C");
            d.Add("Disconnect", "Alt-D");
            d.Add("Port settings", "Alt-P");
            d.Add("Terminal Settings", "Alt-T");
            d.Add("Screen Size", "Alt-V");
            d.Add("Clear Screen", "Control-Home");
            d.Add("Upload (Send)", "Alt-U");
            d.Add("Download (Receive)", "Alt-R");

            d.Left = 15;
            d.Top = 5;
            d.Width = 44;
            d.Height = 14;

            d.Show();

            //debug 
            //Crt.PrintAtStart("Main Menu");
        }

        public void UpdatePortMenu()
        {
            if (Session.Transport == null)
                return;

            IO.SerialIOPort sp = Session.Transport as IO.SerialIOPort;
            if (sp == null)
            {
                BitsDropdown.Visible = false;
            }
            else
            {
                BitsDropdown.Visible = true;

                if (sp.Port != null)
                {
                    dataBits7.Checked = sp.Port.DataBits == 7;
                    dataBits8.Checked = sp.Port.DataBits == 8;

                    parityNoneToolStripMenuItem.Checked = sp.Port.Parity == System.IO.Ports.Parity.None;
                    paritySpaceToolStripMenuItem.Checked = sp.Port.Parity == System.IO.Ports.Parity.Space;
                    parityMarkToolStripMenuItem.Checked = sp.Port.Parity == System.IO.Ports.Parity.Mark;
                    parityEvenToolStripMenuItem.Checked = sp.Port.Parity == System.IO.Ports.Parity.Even;
                    parityOddToolStripMenuItem.Checked = sp.Port.Parity == System.IO.Ports.Parity.Odd;

                    stop1ToolStripMenuItem.Checked = sp.Port.StopBits == System.IO.Ports.StopBits.One;
                    stop2ToolStripMenuItem.Checked = sp.Port.StopBits == System.IO.Ports.StopBits.Two;
                }
            }

            if (Session.Terminal != null)
            {
                bitTrimToolStripMenuItem.Checked = Session.Terminal.TrimHighBit;
            }

        }

        public void UpdateTerminalMode()
        {
            if (Session == null || Session.Terminal == null)
                return;

            BasicModeMenuItem.Checked = Session.Terminal.EchoMode == EchoModes.FullScreenEdit;
            echoOnOffToolStripMenuItem.Checked = Session.Terminal.EchoMode == EchoModes.LocalEcho;
        }

        private void BasicModeMenuItem_Click(object sender, EventArgs e)
        {
            if (Session == null || Session.Terminal == null)
                return;

            if (Session.Terminal.EchoMode == EchoModes.FullScreenEdit)
            {
                Session.Terminal.EchoMode = lastEchoMode;
            }
            else
            {
                lastEchoMode = Session.Terminal.EchoMode;
                Session.Terminal.EchoMode = EchoModes.FullScreenEdit;
            }
        }

        private void BufferToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Session.captureBuffer.Status == CaptureBuffer.CaptureStatusCodes.Capturing)
            {
                Session.captureBuffer.StopCapture();
                if (Session.captureBuffer.Buffer.Length > 0)
                    Clipboard.SetText(Session.captureBuffer.ToString());
            }
            else
                Session.captureBuffer.StartCapture();


            UpdateStatus();
        }

        private void CancelTransferButton_Click(object sender, EventArgs e)
        {
            if (Session.Transfer != null)
                Session.Transfer.Cancel();
        }
    }

}
