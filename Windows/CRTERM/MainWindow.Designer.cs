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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.PortStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.PortNameLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.PortStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.TerminalNameLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.EchoStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.Crt = new TerminalUI.DisplayControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ConnectButton = new System.Windows.Forms.ToolStripButton();
            this.DisconnectButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ClearScreenButton = new System.Windows.Forms.ToolStripButton();
            this.PortOptionsButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.BaudRateButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.TerminalOptionsButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.bASICModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.echoOnOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bSDELToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aNSIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pETSCIIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lineWrapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UploadButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aSCIIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xMODEMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xModemPCGETToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownloadButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.xMODEMToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.xmodemPCPUTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CancelTransferButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.DisplayOptionsDropdown = new System.Windows.Forms.ToolStripDropDownButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.transferControl1 = new CRTerm.Transfer.TransferControl();
            this.ReceiveTimer = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PortStatusLabel,
            this.PortNameLabel,
            this.PortStatus,
            this.TerminalNameLabel,
            this.EchoStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 702);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1124, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // PortStatusLabel
            // 
            this.PortStatusLabel.Name = "PortStatusLabel";
            this.PortStatusLabel.Size = new System.Drawing.Size(79, 17);
            this.PortStatusLabel.Text = "Disconnected";
            // 
            // PortNameLabel
            // 
            this.PortNameLabel.Name = "PortNameLabel";
            this.PortNameLabel.Size = new System.Drawing.Size(48, 17);
            this.PortNameLabel.Text = "No Port";
            // 
            // PortStatus
            // 
            this.PortStatus.Name = "PortStatus";
            this.PortStatus.Size = new System.Drawing.Size(68, 17);
            this.PortStatus.Text = "No Address";
            // 
            // TerminalNameLabel
            // 
            this.TerminalNameLabel.Name = "TerminalNameLabel";
            this.TerminalNameLabel.Size = new System.Drawing.Size(72, 17);
            this.TerminalNameLabel.Text = "No Terminal";
            // 
            // EchoStatus
            // 
            this.EchoStatus.Name = "EchoStatus";
            this.EchoStatus.Size = new System.Drawing.Size(53, 17);
            this.EchoStatus.Text = "Echo Off";
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.Crt);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.transferControl1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1124, 677);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1124, 702);
            this.toolStripContainer1.TabIndex = 1;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // Crt
            // 
            this.Crt.AddLinefeed = false;
            this.Crt.BackColor = System.Drawing.Color.DimGray;
            this.Crt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Crt.CharUnderCursor = ' ';
            this.Crt.CurrentAttribute = TerminalUI.CharacterCell.Attributes.Normal;
            this.Crt.CurrentBackground = TerminalUI.CharacterCell.ColorCodes.Black;
            this.Crt.CurrentColumn = 0;
            this.Crt.CurrentRow = 0;
            this.Crt.CurrentTextColor = TerminalUI.CharacterCell.ColorCodes.Gray;
            this.Crt.CursorPos = 0;
            this.Crt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Crt.EchoMode = TerminalUI.Terminals.EchoModes.EchoOff;
            this.Crt.Editor = null;
            this.Crt.Font = new System.Drawing.Font("Classic Console", 33.32923F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Crt.InsertMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.Crt.LineWrap = false;
            this.Crt.Location = new System.Drawing.Point(0, 0);
            this.Crt.Margin = new System.Windows.Forms.Padding(440714, 347787, 440714, 347787);
            this.Crt.Name = "Crt";
            this.Crt.Size = new System.Drawing.Size(904, 677);
            this.Crt.StatusText = null;
            this.Crt.TabIndex = 1;
            this.Crt.Terminal = null;
            this.Crt.TextCursor = TerminalUI.TextCursorStyles.Underline;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConnectButton,
            this.DisconnectButton,
            this.toolStripSeparator1,
            this.ClearScreenButton,
            this.PortOptionsButton,
            this.BaudRateButton,
            this.TerminalOptionsButton,
            this.UploadButton,
            this.toolStripDropDownloadButton,
            this.CancelTransferButton,
            this.toolStripSeparator2,
            this.DisplayOptionsDropdown});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(561, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // ConnectButton
            // 
            this.ConnectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
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
            this.DisconnectButton.Image = ((System.Drawing.Image)(resources.GetObject("DisconnectButton.Image")));
            this.DisconnectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(70, 22);
            this.DisconnectButton.Text = "Disconnect";
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ClearScreenButton
            // 
            this.ClearScreenButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ClearScreenButton.Image = ((System.Drawing.Image)(resources.GetObject("ClearScreenButton.Image")));
            this.ClearScreenButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClearScreenButton.Name = "ClearScreenButton";
            this.ClearScreenButton.Size = new System.Drawing.Size(38, 22);
            this.ClearScreenButton.Text = "Clear";
            this.ClearScreenButton.Click += new System.EventHandler(this.ClearScreenButton_Click);
            // 
            // PortOptionsButton
            // 
            this.PortOptionsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.PortOptionsButton.Image = ((System.Drawing.Image)(resources.GetObject("PortOptionsButton.Image")));
            this.PortOptionsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PortOptionsButton.Name = "PortOptionsButton";
            this.PortOptionsButton.Size = new System.Drawing.Size(42, 22);
            this.PortOptionsButton.Text = "Port";
            this.PortOptionsButton.DropDownOpening += new System.EventHandler(this.PortOptionsButton_DropDownOpening);
            // 
            // BaudRateButton
            // 
            this.BaudRateButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BaudRateButton.Image = ((System.Drawing.Image)(resources.GetObject("BaudRateButton.Image")));
            this.BaudRateButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BaudRateButton.Name = "BaudRateButton";
            this.BaudRateButton.Size = new System.Drawing.Size(47, 22);
            this.BaudRateButton.Text = "Baud";
            this.BaudRateButton.DropDownOpening += new System.EventHandler(this.BaudRateButton_DropDownOpening);
            // 
            // TerminalOptionsButton
            // 
            this.TerminalOptionsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TerminalOptionsButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bASICModeToolStripMenuItem,
            this.echoOnOffToolStripMenuItem,
            this.bSDELToolStripMenuItem,
            this.aNSIToolStripMenuItem,
            this.pETSCIIToolStripMenuItem,
            this.lineWrapToolStripMenuItem});
            this.TerminalOptionsButton.Image = ((System.Drawing.Image)(resources.GetObject("TerminalOptionsButton.Image")));
            this.TerminalOptionsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TerminalOptionsButton.Name = "TerminalOptionsButton";
            this.TerminalOptionsButton.Size = new System.Drawing.Size(47, 22);
            this.TerminalOptionsButton.Text = "Term";
            // 
            // bASICModeToolStripMenuItem
            // 
            this.bASICModeToolStripMenuItem.Name = "bASICModeToolStripMenuItem";
            this.bASICModeToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.bASICModeToolStripMenuItem.Text = "BASIC Mode";
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
            this.xMODEMToolStripMenuItem,
            this.xModemPCGETToolStripMenuItem});
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
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // aSCIIToolStripMenuItem
            // 
            this.aSCIIToolStripMenuItem.Name = "aSCIIToolStripMenuItem";
            this.aSCIIToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.aSCIIToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.aSCIIToolStripMenuItem.Text = "ASCII";
            this.aSCIIToolStripMenuItem.Click += new System.EventHandler(this.aSCIIToolStripMenuItem_Click);
            // 
            // xMODEMToolStripMenuItem
            // 
            this.xMODEMToolStripMenuItem.Name = "xMODEMToolStripMenuItem";
            this.xMODEMToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.U)));
            this.xMODEMToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.xMODEMToolStripMenuItem.Text = "XMODEM";
            this.xMODEMToolStripMenuItem.Click += new System.EventHandler(this.xMODEMToolStripMenuItem_Click);
            // 
            // xModemPCGETToolStripMenuItem
            // 
            this.xModemPCGETToolStripMenuItem.Name = "xModemPCGETToolStripMenuItem";
            this.xModemPCGETToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.G)));
            this.xModemPCGETToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.xModemPCGETToolStripMenuItem.Text = "XModem-PCGET";
            this.xModemPCGETToolStripMenuItem.Click += new System.EventHandler(this.xModemPCGETToolStripMenuItem_Click);
            // 
            // toolStripDropDownloadButton
            // 
            this.toolStripDropDownloadButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownloadButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xMODEMToolStripMenuItem1,
            this.xmodemPCPUTToolStripMenuItem});
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
            this.xMODEMToolStripMenuItem1.Size = new System.Drawing.Size(202, 22);
            this.xMODEMToolStripMenuItem1.Text = "XMODEM";
            this.xMODEMToolStripMenuItem1.Click += new System.EventHandler(this.xMODEMToolStripMenuItem1_Click);
            // 
            // xmodemPCPUTToolStripMenuItem
            // 
            this.xmodemPCPUTToolStripMenuItem.Name = "xmodemPCPUTToolStripMenuItem";
            this.xmodemPCPUTToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.P)));
            this.xmodemPCPUTToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.xmodemPCPUTToolStripMenuItem.Text = "Xmodem-PCPUT";
            // 
            // CancelTransferButton
            // 
            this.CancelTransferButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.CancelTransferButton.Image = ((System.Drawing.Image)(resources.GetObject("CancelTransferButton.Image")));
            this.CancelTransferButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CancelTransferButton.Name = "CancelTransferButton";
            this.CancelTransferButton.Size = new System.Drawing.Size(47, 22);
            this.CancelTransferButton.Text = "Cancel";
            this.CancelTransferButton.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // DisplayOptionsDropdown
            // 
            this.DisplayOptionsDropdown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
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
            // transferControl1
            // 
            this.transferControl1.BytesSent = 0;
            this.transferControl1.BytesToSend = 0;
            this.transferControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.transferControl1.Filename = "[unknown]";
            this.transferControl1.Location = new System.Drawing.Point(904, 0);
            this.transferControl1.Name = "transferControl1";
            this.transferControl1.Operation = "Send / Receive";
            this.transferControl1.Protocol = "[unknown protocol]";
            this.transferControl1.Size = new System.Drawing.Size(220, 677);
            this.transferControl1.TabIndex = 2;
            this.transferControl1.Visible = false;
            // 
            // ReceiveTimer
            // 
            this.ReceiveTimer.Enabled = true;
            this.ReceiveTimer.Interval = 16;
            this.ReceiveTimer.Tick += new System.EventHandler(this.ReceiveTimer_Tick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 724);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "MainWindow";
            this.Text = "CR Term";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.SizeChanged += new System.EventHandler(this.MainWindow_SizeChanged);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private TerminalUI.DisplayControl Crt;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton ConnectButton;
        private System.Windows.Forms.ToolStripButton DisconnectButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ClearScreenButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel PortStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel PortNameLabel;
        private System.Windows.Forms.ToolStripStatusLabel PortStatus;
        private System.Windows.Forms.ToolStripStatusLabel TerminalNameLabel;
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
        private System.Windows.Forms.ToolStripButton CancelTransferButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripStatusLabel EchoStatus;
        private System.Windows.Forms.ToolStripMenuItem xModemPCGETToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bASICModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem echoOnOffToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownloadButton;
        private System.Windows.Forms.ToolStripMenuItem xMODEMToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem xmodemPCPUTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lineWrapToolStripMenuItem;
        private Transfer.TransferControl transferControl1;
        private System.Windows.Forms.ToolStripDropDownButton DisplayOptionsDropdown;
        private System.Windows.Forms.Timer ReceiveTimer;
    }
}

