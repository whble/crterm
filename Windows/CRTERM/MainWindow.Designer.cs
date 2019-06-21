namespace CRTerm
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.transferControl1 = new CRTerm.Transfer.TransferControl();
            this.Crt = new TerminalUI.DisplayControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ConnectButton = new System.Windows.Forms.ToolStripButton();
            this.DisconnectButton = new System.Windows.Forms.ToolStripButton();
            this.PortOptionsButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.BaudRateButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.BitsDropdown = new System.Windows.Forms.ToolStripDropDownButton();
            this.dataBits7 = new System.Windows.Forms.ToolStripMenuItem();
            this.dataBits8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.parityNoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paritySpaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parityMarkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parityEvenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parityOddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.stop1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stop2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.bitTrimToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearScreenButton = new System.Windows.Forms.ToolStripButton();
            this.TerminalOptionsButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.BasicModeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.echoOnOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bSDELToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aNSIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pETSCIIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lineWrapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UploadButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aSCIIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xMODEMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownloadButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.xMODEMToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.textCaptureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bufferToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.DisplayOptionsDropdown = new System.Windows.Forms.ToolStripDropDownButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ReceiveTimer = new System.Windows.Forms.Timer(this.components);
            this.StatusBox = new System.Windows.Forms.PictureBox();
            this.CancelTransferButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StatusBox)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.transferControl1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.Crt);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1124, 675);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1124, 700);
            this.toolStripContainer1.TabIndex = 1;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.BackColor = System.Drawing.SystemColors.MenuBar;
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // transferControl1
            // 
            this.transferControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.transferControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.transferControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.transferControl1.BytesSent = ((long)(8192));
            this.transferControl1.BytesToSend = ((long)(16384));
            this.transferControl1.Filename = "[unknown]";
            this.transferControl1.ForeColor = System.Drawing.Color.Silver;
            this.transferControl1.Location = new System.Drawing.Point(882, 20);
            this.transferControl1.Name = "transferControl1";
            this.transferControl1.Operation = "Send / Receive";
            this.transferControl1.Protocol = "[unknown protocol]";
            this.transferControl1.Size = new System.Drawing.Size(220, 461);
            this.transferControl1.TabIndex = 2;
            this.transferControl1.Visible = false;
            // 
            // Crt
            // 
            this.Crt.AddLinefeed = false;
            this.Crt.BackColor = System.Drawing.Color.DimGray;
            this.Crt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Crt.CharUnderCursor = ' ';
            this.Crt.CurrentAttribute = TerminalUI.CharacterCell.AttributeCodes.Normal;
            this.Crt.CurrentBackground = TerminalUI.CharacterCell.ColorCodes.Black;
            this.Crt.CurrentColumn = 0;
            this.Crt.CurrentRow = 0;
            this.Crt.CurrentTextColor = TerminalUI.CharacterCell.ColorCodes.Gray;
            this.Crt.CursorPos = 0;
            this.Crt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Crt.EchoMode = TerminalUI.Terminals.EchoModes.EchoOff;
            this.Crt.Editor = null;
            this.Crt.Font = new System.Drawing.Font("Classic Console", 33.23077F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Crt.InsertMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.Crt.LineWrap = false;
            this.Crt.Location = new System.Drawing.Point(0, 0);
            this.Crt.Margin = new System.Windows.Forms.Padding(440714, 347787, 440714, 347787);
            this.Crt.Name = "Crt";
            this.Crt.Size = new System.Drawing.Size(1124, 675);
            this.Crt.StatusText = null;
            this.Crt.TabIndex = 1;
            this.Crt.Terminal = null;
            this.Crt.TextCursor = TerminalUI.TextCursorStyles.Underline;
            this.Crt.ToggleFullScreenRequest += new System.EventHandler(this.Crt_ToggleFullScreenRequest);
            this.Crt.HotkeyPressed += new System.Windows.Forms.KeyEventHandler(this.Crt_HotkeyPressed);
            this.Crt.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CRT_MouseMove);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConnectButton,
            this.DisconnectButton,
            this.PortOptionsButton,
            this.toolStripSeparator1,
            this.BaudRateButton,
            this.BitsDropdown,
            this.ClearScreenButton,
            this.TerminalOptionsButton,
            this.UploadButton,
            this.toolStripDropDownloadButton,
            this.CancelTransferButton,
            this.toolStripSeparator2,
            this.DisplayOptionsDropdown});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(631, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // ConnectButton
            // 
            this.ConnectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ConnectButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ConnectButton.Image = ((System.Drawing.Image)(resources.GetObject("ConnectButton.Image")));
            this.ConnectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(56, 22);
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DisconnectButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DisconnectButton.Image = ((System.Drawing.Image)(resources.GetObject("DisconnectButton.Image")));
            this.DisconnectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(70, 22);
            this.DisconnectButton.Text = "Disconnect";
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // PortOptionsButton
            // 
            this.PortOptionsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.PortOptionsButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.PortOptionsButton.Image = ((System.Drawing.Image)(resources.GetObject("PortOptionsButton.Image")));
            this.PortOptionsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PortOptionsButton.Name = "PortOptionsButton";
            this.PortOptionsButton.Size = new System.Drawing.Size(42, 22);
            this.PortOptionsButton.Text = "Port";
            this.PortOptionsButton.DropDownOpening += new System.EventHandler(this.PortOptionsButton_DropDownOpening);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.ForeColor = System.Drawing.Color.LightGray;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // BaudRateButton
            // 
            this.BaudRateButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BaudRateButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BaudRateButton.Image = ((System.Drawing.Image)(resources.GetObject("BaudRateButton.Image")));
            this.BaudRateButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BaudRateButton.Name = "BaudRateButton";
            this.BaudRateButton.Size = new System.Drawing.Size(47, 22);
            this.BaudRateButton.Text = "Baud";
            this.BaudRateButton.DropDownOpening += new System.EventHandler(this.BaudRateButton_DropDownOpening);
            // 
            // BitsDropdown
            // 
            this.BitsDropdown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BitsDropdown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataBits7,
            this.dataBits8,
            this.toolStripSeparator3,
            this.parityNoneToolStripMenuItem,
            this.paritySpaceToolStripMenuItem,
            this.parityMarkToolStripMenuItem,
            this.parityEvenToolStripMenuItem,
            this.parityOddToolStripMenuItem,
            this.toolStripSeparator4,
            this.stop1ToolStripMenuItem,
            this.stop2ToolStripMenuItem,
            this.toolStripSeparator5,
            this.bitTrimToolStripMenuItem});
            this.BitsDropdown.Image = ((System.Drawing.Image)(resources.GetObject("BitsDropdown.Image")));
            this.BitsDropdown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BitsDropdown.Name = "BitsDropdown";
            this.BitsDropdown.Size = new System.Drawing.Size(39, 22);
            this.BitsDropdown.Text = "Bits";
            // 
            // dataBits7
            // 
            this.dataBits7.Name = "dataBits7";
            this.dataBits7.Size = new System.Drawing.Size(144, 22);
            this.dataBits7.Text = "7 bits";
            // 
            // dataBits8
            // 
            this.dataBits8.Name = "dataBits8";
            this.dataBits8.Size = new System.Drawing.Size(144, 22);
            this.dataBits8.Text = "8 bits";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(141, 6);
            // 
            // parityNoneToolStripMenuItem
            // 
            this.parityNoneToolStripMenuItem.Name = "parityNoneToolStripMenuItem";
            this.parityNoneToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.parityNoneToolStripMenuItem.Text = "Parity-None";
            // 
            // paritySpaceToolStripMenuItem
            // 
            this.paritySpaceToolStripMenuItem.Name = "paritySpaceToolStripMenuItem";
            this.paritySpaceToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.paritySpaceToolStripMenuItem.Text = "Party-Space";
            // 
            // parityMarkToolStripMenuItem
            // 
            this.parityMarkToolStripMenuItem.Name = "parityMarkToolStripMenuItem";
            this.parityMarkToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.parityMarkToolStripMenuItem.Text = "Parity-Mark";
            // 
            // parityEvenToolStripMenuItem
            // 
            this.parityEvenToolStripMenuItem.Name = "parityEvenToolStripMenuItem";
            this.parityEvenToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.parityEvenToolStripMenuItem.Text = "Parity-Even";
            // 
            // parityOddToolStripMenuItem
            // 
            this.parityOddToolStripMenuItem.Name = "parityOddToolStripMenuItem";
            this.parityOddToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.parityOddToolStripMenuItem.Text = "Parity-Odd";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(141, 6);
            // 
            // stop1ToolStripMenuItem
            // 
            this.stop1ToolStripMenuItem.Name = "stop1ToolStripMenuItem";
            this.stop1ToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.stop1ToolStripMenuItem.Text = "Stop-1";
            // 
            // stop2ToolStripMenuItem
            // 
            this.stop2ToolStripMenuItem.Name = "stop2ToolStripMenuItem";
            this.stop2ToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.stop2ToolStripMenuItem.Text = "Stop-2";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(141, 6);
            // 
            // bitTrimToolStripMenuItem
            // 
            this.bitTrimToolStripMenuItem.Name = "bitTrimToolStripMenuItem";
            this.bitTrimToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.bitTrimToolStripMenuItem.Text = "Trim High Bit";
            // 
            // ClearScreenButton
            // 
            this.ClearScreenButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ClearScreenButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ClearScreenButton.Image = ((System.Drawing.Image)(resources.GetObject("ClearScreenButton.Image")));
            this.ClearScreenButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClearScreenButton.Name = "ClearScreenButton";
            this.ClearScreenButton.Size = new System.Drawing.Size(38, 22);
            this.ClearScreenButton.Text = "Clear";
            this.ClearScreenButton.Click += new System.EventHandler(this.ClearScreenButton_Click);
            // 
            // TerminalOptionsButton
            // 
            this.TerminalOptionsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TerminalOptionsButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BasicModeMenuItem,
            this.echoOnOffToolStripMenuItem,
            this.bSDELToolStripMenuItem,
            this.aNSIToolStripMenuItem,
            this.pETSCIIToolStripMenuItem,
            this.lineWrapToolStripMenuItem});
            this.TerminalOptionsButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TerminalOptionsButton.Image = ((System.Drawing.Image)(resources.GetObject("TerminalOptionsButton.Image")));
            this.TerminalOptionsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TerminalOptionsButton.Name = "TerminalOptionsButton";
            this.TerminalOptionsButton.Size = new System.Drawing.Size(47, 22);
            this.TerminalOptionsButton.Text = "Term";
            // 
            // BasicModeMenuItem
            // 
            this.BasicModeMenuItem.Name = "BasicModeMenuItem";
            this.BasicModeMenuItem.Size = new System.Drawing.Size(163, 22);
            this.BasicModeMenuItem.Text = "BASIC Mode";
            this.BasicModeMenuItem.Click += new System.EventHandler(this.BasicModeMenuItem_Click);
            // 
            // echoOnOffToolStripMenuItem
            // 
            this.echoOnOffToolStripMenuItem.Name = "echoOnOffToolStripMenuItem";
            this.echoOnOffToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.echoOnOffToolStripMenuItem.Text = "Local Echo";
            this.echoOnOffToolStripMenuItem.Click += new System.EventHandler(this.echoOnOffToolStripMenuItem_Click);
            // 
            // bSDELToolStripMenuItem
            // 
            this.bSDELToolStripMenuItem.Name = "bSDELToolStripMenuItem";
            this.bSDELToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)));
            this.bSDELToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.bSDELToolStripMenuItem.Text = "BS/DEL";
            this.bSDELToolStripMenuItem.Click += new System.EventHandler(this.bSDELToolStripMenuItem_Click);
            // 
            // aNSIToolStripMenuItem
            // 
            this.aNSIToolStripMenuItem.Name = "aNSIToolStripMenuItem";
            this.aNSIToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.aNSIToolStripMenuItem.Text = "ANSI";
            // 
            // pETSCIIToolStripMenuItem
            // 
            this.pETSCIIToolStripMenuItem.Name = "pETSCIIToolStripMenuItem";
            this.pETSCIIToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.pETSCIIToolStripMenuItem.Text = "PETSCII";
            // 
            // lineWrapToolStripMenuItem
            // 
            this.lineWrapToolStripMenuItem.Name = "lineWrapToolStripMenuItem";
            this.lineWrapToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.lineWrapToolStripMenuItem.Text = "Line Wrap";
            // 
            // UploadButton
            // 
            this.UploadButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.UploadButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pasteToolStripMenuItem,
            this.aSCIIToolStripMenuItem,
            this.xMODEMToolStripMenuItem});
            this.UploadButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.UploadButton.Image = ((System.Drawing.Image)(resources.GetObject("UploadButton.Image")));
            this.UploadButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UploadButton.Name = "UploadButton";
            this.UploadButton.Size = new System.Drawing.Size(58, 22);
            this.UploadButton.Text = "Upload";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // aSCIIToolStripMenuItem
            // 
            this.aSCIIToolStripMenuItem.Name = "aSCIIToolStripMenuItem";
            this.aSCIIToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.aSCIIToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.aSCIIToolStripMenuItem.Text = "ASCII";
            this.aSCIIToolStripMenuItem.Click += new System.EventHandler(this.aSCIIToolStripMenuItem_Click);
            // 
            // xMODEMToolStripMenuItem
            // 
            this.xMODEMToolStripMenuItem.Name = "xMODEMToolStripMenuItem";
            this.xMODEMToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.U)));
            this.xMODEMToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.xMODEMToolStripMenuItem.Text = "XMODEM";
            this.xMODEMToolStripMenuItem.Click += new System.EventHandler(this.XModem_Send_Click);
            // 
            // toolStripDropDownloadButton
            // 
            this.toolStripDropDownloadButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownloadButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xMODEMToolStripMenuItem1,
            this.textCaptureToolStripMenuItem,
            this.bufferToClipboardToolStripMenuItem});
            this.toolStripDropDownloadButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolStripDropDownloadButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownloadButton.Image")));
            this.toolStripDropDownloadButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownloadButton.Name = "toolStripDropDownloadButton";
            this.toolStripDropDownloadButton.Size = new System.Drawing.Size(74, 22);
            this.toolStripDropDownloadButton.Text = "Download";
            // 
            // xMODEMToolStripMenuItem1
            // 
            this.xMODEMToolStripMenuItem1.Name = "xMODEMToolStripMenuItem1";
            this.xMODEMToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
            this.xMODEMToolStripMenuItem1.Size = new System.Drawing.Size(226, 22);
            this.xMODEMToolStripMenuItem1.Text = "XMODEM";
            this.xMODEMToolStripMenuItem1.Click += new System.EventHandler(this.XModem_Receive_Click);
            // 
            // textCaptureToolStripMenuItem
            // 
            this.textCaptureToolStripMenuItem.Name = "textCaptureToolStripMenuItem";
            this.textCaptureToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.T)));
            this.textCaptureToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.textCaptureToolStripMenuItem.Text = "Text Capture";
            this.textCaptureToolStripMenuItem.Click += new System.EventHandler(this.TextCaptureToolStripMenuItem_Click);
            // 
            // bufferToClipboardToolStripMenuItem
            // 
            this.bufferToClipboardToolStripMenuItem.Name = "bufferToClipboardToolStripMenuItem";
            this.bufferToClipboardToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.bufferToClipboardToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.bufferToClipboardToolStripMenuItem.Text = "Capture  to Clipboard";
            this.bufferToClipboardToolStripMenuItem.Click += new System.EventHandler(this.BufferToClipboardToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // DisplayOptionsDropdown
            // 
            this.DisplayOptionsDropdown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DisplayOptionsDropdown.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DisplayOptionsDropdown.Image = ((System.Drawing.Image)(resources.GetObject("DisplayOptionsDropdown.Image")));
            this.DisplayOptionsDropdown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DisplayOptionsDropdown.Name = "DisplayOptionsDropdown";
            this.DisplayOptionsDropdown.Size = new System.Drawing.Size(58, 22);
            this.DisplayOptionsDropdown.Text = "Display";
            this.DisplayOptionsDropdown.DropDownOpening += new System.EventHandler(this.DisplayOptionsDropdown_DropDownOpening);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ReceiveTimer
            // 
            this.ReceiveTimer.Enabled = true;
            this.ReceiveTimer.Interval = 16;
            this.ReceiveTimer.Tick += new System.EventHandler(this.ReceiveTimer_Tick);
            // 
            // StatusBox
            // 
            this.StatusBox.BackColor = System.Drawing.Color.Black;
            this.StatusBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.StatusBox.Location = new System.Drawing.Point(0, 700);
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.Size = new System.Drawing.Size(1124, 24);
            this.StatusBox.TabIndex = 3;
            this.StatusBox.TabStop = false;
            this.StatusBox.Paint += new System.Windows.Forms.PaintEventHandler(this.StatusBox_Paint);
            // 
            // CancelTransferButton
            // 
            this.CancelTransferButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.CancelTransferButton.Image = ((System.Drawing.Image)(resources.GetObject("CancelTransferButton.Image")));
            this.CancelTransferButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CancelTransferButton.Name = "CancelTransferButton";
            this.CancelTransferButton.Size = new System.Drawing.Size(47, 22);
            this.CancelTransferButton.Text = "Cancel";
            this.CancelTransferButton.Click += new System.EventHandler(this.CancelTransferButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 724);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.StatusBox);
            this.KeyPreview = true;
            this.Name = "MainWindow";
            this.Text = "CR Term";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.SizeChanged += new System.EventHandler(this.MainWindow_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyDown);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StatusBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private TerminalUI.DisplayControl Crt;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton ConnectButton;
        private System.Windows.Forms.ToolStripButton DisconnectButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ClearScreenButton;
        private System.Windows.Forms.ToolStripDropDownButton TerminalOptionsButton;
        private System.Windows.Forms.ToolStripMenuItem bSDELToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton PortOptionsButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripDropDownButton BaudRateButton;
        private System.Windows.Forms.ToolStripMenuItem aNSIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pETSCIIToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton UploadButton;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aSCIIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xMODEMToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem BasicModeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem echoOnOffToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownloadButton;
        private System.Windows.Forms.ToolStripMenuItem xMODEMToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem lineWrapToolStripMenuItem;
        private Transfer.TransferControl transferControl1;
        private System.Windows.Forms.ToolStripDropDownButton DisplayOptionsDropdown;
        private System.Windows.Forms.Timer ReceiveTimer;
        private System.Windows.Forms.ToolStripMenuItem textCaptureToolStripMenuItem;
        private System.Windows.Forms.PictureBox StatusBox;
        private System.Windows.Forms.ToolStripDropDownButton BitsDropdown;
        private System.Windows.Forms.ToolStripMenuItem dataBits7;
        private System.Windows.Forms.ToolStripMenuItem dataBits8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem parityNoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paritySpaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parityMarkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parityEvenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parityOddToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem stop1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stop2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem bitTrimToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bufferToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton CancelTransferButton;
    }
}

