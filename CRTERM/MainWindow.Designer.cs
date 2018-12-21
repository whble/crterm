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
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ConnectButton = new System.Windows.Forms.ToolStripButton();
            this.DisconnectButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ClearScreenButton = new System.Windows.Forms.ToolStripButton();
            this.PortOptionsButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.BaudRateButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.TerminalOptionsButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.bSDELToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aNSIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pETSCIIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UploadButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aSCIIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xMODEMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DownloadButton = new System.Windows.Forms.ToolStripButton();
            this.CancelTransferButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.BasicButton = new System.Windows.Forms.ToolStripButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.frameBuffer1 = new CRTerm.FrameBuffer();
            this.EntryText = new System.Windows.Forms.TextBox();
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
            this.TerminalNameLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 517);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(965, 22);
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
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.frameBuffer1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.EntryText);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(965, 492);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(965, 517);
            this.toolStripContainer1.TabIndex = 1;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
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
            this.DownloadButton,
            this.CancelTransferButton,
            this.toolStripSeparator2,
            this.BasicButton});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(537, 25);
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
            this.bSDELToolStripMenuItem,
            this.aNSIToolStripMenuItem,
            this.pETSCIIToolStripMenuItem});
            this.TerminalOptionsButton.Image = ((System.Drawing.Image)(resources.GetObject("TerminalOptionsButton.Image")));
            this.TerminalOptionsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TerminalOptionsButton.Name = "TerminalOptionsButton";
            this.TerminalOptionsButton.Size = new System.Drawing.Size(47, 22);
            this.TerminalOptionsButton.Text = "Term";
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
            // UploadButton
            // 
            this.UploadButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.UploadButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pasteToolStripMenuItem,
            this.aSCIIToolStripMenuItem,
            this.xMODEMToolStripMenuItem});
            this.UploadButton.Image = ((System.Drawing.Image)(resources.GetObject("UploadButton.Image")));
            this.UploadButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UploadButton.Name = "UploadButton";
            this.UploadButton.Size = new System.Drawing.Size(58, 22);
            this.UploadButton.Text = "Upload";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // aSCIIToolStripMenuItem
            // 
            this.aSCIIToolStripMenuItem.Name = "aSCIIToolStripMenuItem";
            this.aSCIIToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.aSCIIToolStripMenuItem.Text = "ASCII";
            // 
            // xMODEMToolStripMenuItem
            // 
            this.xMODEMToolStripMenuItem.Name = "xMODEMToolStripMenuItem";
            this.xMODEMToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.xMODEMToolStripMenuItem.Text = "XMODEM";
            // 
            // DownloadButton
            // 
            this.DownloadButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DownloadButton.Image = ((System.Drawing.Image)(resources.GetObject("DownloadButton.Image")));
            this.DownloadButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(65, 22);
            this.DownloadButton.Text = "Download";
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
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
            // BasicButton
            // 
            this.BasicButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BasicButton.Image = ((System.Drawing.Image)(resources.GetObject("BasicButton.Image")));
            this.BasicButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BasicButton.Name = "BasicButton";
            this.BasicButton.Size = new System.Drawing.Size(43, 22);
            this.BasicButton.Text = "BASIC";
            this.BasicButton.Click += new System.EventHandler(this.BasicButton_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frameBuffer1
            // 
            this.frameBuffer1.CurrentAttribute = CRTerm.AttributeCodes.Normal;
            this.frameBuffer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frameBuffer1.Location = new System.Drawing.Point(0, 0);
            this.frameBuffer1.Name = "frameBuffer1";
            this.frameBuffer1.Size = new System.Drawing.Size(965, 479);
            this.frameBuffer1.TabIndex = 1;
            this.frameBuffer1.Terminal = null;
            this.frameBuffer1.X = 0;
            this.frameBuffer1.Y = 24;
            // 
            // EntryText
            // 
            this.EntryText.AcceptsReturn = true;
            this.EntryText.BackColor = System.Drawing.Color.Black;
            this.EntryText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.EntryText.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.EntryText.Location = new System.Drawing.Point(0, 479);
            this.EntryText.Name = "EntryText";
            this.EntryText.Size = new System.Drawing.Size(965, 13);
            this.EntryText.TabIndex = 3;
            this.EntryText.Enter += new System.EventHandler(this.EntryText_Enter);
            this.EntryText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EntryText_KeyDown);
            this.EntryText.Leave += new System.EventHandler(this.EntryText_Leave);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 539);
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
            this.toolStripContainer1.ContentPanel.PerformLayout();
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
        private FrameBuffer frameBuffer1;
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
        private System.Windows.Forms.ToolStripButton DownloadButton;
        private System.Windows.Forms.ToolStripDropDownButton BaudRateButton;
        private System.Windows.Forms.ToolStripMenuItem aNSIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pETSCIIToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton UploadButton;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aSCIIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xMODEMToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton CancelTransferButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton BasicButton;
        private System.Windows.Forms.TextBox EntryText;
    }
}

