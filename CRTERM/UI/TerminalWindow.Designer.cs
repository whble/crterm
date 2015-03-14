namespace CRTERM.UI
{
	partial class TerminalWindow
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
			this.MainMenu = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openConnectionProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveConnectionAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.connectionPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.TermDisplay = new CRTERM.UI.TerminalDisplay();
			this.transferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.captureASCIIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sendASCIIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.uIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.gPSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.radioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MainMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainMenu
			// 
			this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.connectToolStripMenuItem,
            this.disconnectToolStripMenuItem,
            this.transferToolStripMenuItem,
            this.uIToolStripMenuItem});
			this.MainMenu.Location = new System.Drawing.Point(0, 0);
			this.MainMenu.Name = "MainMenu";
			this.MainMenu.Size = new System.Drawing.Size(700, 24);
			this.MainMenu.TabIndex = 0;
			this.MainMenu.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newConnectionToolStripMenuItem,
            this.openConnectionProfileToolStripMenuItem,
            this.saveConnectionToolStripMenuItem,
            this.saveConnectionAsToolStripMenuItem,
            this.connectionPropertiesToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// newConnectionToolStripMenuItem
			// 
			this.newConnectionToolStripMenuItem.Name = "newConnectionToolStripMenuItem";
			this.newConnectionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.N)));
			this.newConnectionToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
			this.newConnectionToolStripMenuItem.Text = "&New Connection...";
			this.newConnectionToolStripMenuItem.Click += new System.EventHandler(this.newConnectionToolStripMenuItem_Click);
			// 
			// openConnectionProfileToolStripMenuItem
			// 
			this.openConnectionProfileToolStripMenuItem.Name = "openConnectionProfileToolStripMenuItem";
			this.openConnectionProfileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.O)));
			this.openConnectionProfileToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
			this.openConnectionProfileToolStripMenuItem.Text = "&Open Connection Profile...";
			this.openConnectionProfileToolStripMenuItem.Click += new System.EventHandler(this.openConnectionProfileToolStripMenuItem_Click);
			// 
			// saveConnectionToolStripMenuItem
			// 
			this.saveConnectionToolStripMenuItem.Name = "saveConnectionToolStripMenuItem";
			this.saveConnectionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
			this.saveConnectionToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
			this.saveConnectionToolStripMenuItem.Text = "&Save Connection";
			this.saveConnectionToolStripMenuItem.Click += new System.EventHandler(this.saveConnectionToolStripMenuItem_Click);
			// 
			// saveConnectionAsToolStripMenuItem
			// 
			this.saveConnectionAsToolStripMenuItem.Name = "saveConnectionAsToolStripMenuItem";
			this.saveConnectionAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
			this.saveConnectionAsToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
			this.saveConnectionAsToolStripMenuItem.Text = "Save Connection &As...";
			this.saveConnectionAsToolStripMenuItem.Click += new System.EventHandler(this.saveConnectionAsToolStripMenuItem_Click);
			// 
			// connectionPropertiesToolStripMenuItem
			// 
			this.connectionPropertiesToolStripMenuItem.Name = "connectionPropertiesToolStripMenuItem";
			this.connectionPropertiesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.P)));
			this.connectionPropertiesToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
			this.connectionPropertiesToolStripMenuItem.Text = "Connection &Properties...";
			this.connectionPropertiesToolStripMenuItem.Click += new System.EventHandler(this.connectionPropertiesToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(250, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
			this.exitToolStripMenuItem.Text = "&Close Window";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "&Edit";
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
			this.copyToolStripMenuItem.Text = "&Copy";
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
			this.pasteToolStripMenuItem.Text = "&Paste";
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
			this.optionsToolStripMenuItem.Text = "&Options";
			// 
			// connectToolStripMenuItem
			// 
			this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
			this.connectToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
			this.connectToolStripMenuItem.Text = "&Connect";
			this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click_1);
			// 
			// disconnectToolStripMenuItem
			// 
			this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
			this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
			this.disconnectToolStripMenuItem.Text = "&Disconnect";
			this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.disconnectToolStripMenuItem_Click_1);
			// 
			// TermDisplay
			// 
			this.TermDisplay.BackColor = System.Drawing.Color.Black;
			this.TermDisplay.CurrentConnection = null;
			this.TermDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TermDisplay.ForeColor = System.Drawing.Color.LightGreen;
			this.TermDisplay.Location = new System.Drawing.Point(0, 24);
			this.TermDisplay.Name = "TermDisplay";
			this.TermDisplay.Size = new System.Drawing.Size(700, 536);
			this.TermDisplay.TabIndex = 0;
			// 
			// transferToolStripMenuItem
			// 
			this.transferToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.captureASCIIToolStripMenuItem,
            this.sendASCIIToolStripMenuItem});
			this.transferToolStripMenuItem.Name = "transferToolStripMenuItem";
			this.transferToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
			this.transferToolStripMenuItem.Text = "Transfer";
			// 
			// captureASCIIToolStripMenuItem
			// 
			this.captureASCIIToolStripMenuItem.Name = "captureASCIIToolStripMenuItem";
			this.captureASCIIToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.captureASCIIToolStripMenuItem.Text = "Capture ASCII";
			// 
			// sendASCIIToolStripMenuItem
			// 
			this.sendASCIIToolStripMenuItem.Name = "sendASCIIToolStripMenuItem";
			this.sendASCIIToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.sendASCIIToolStripMenuItem.Text = "Send ASCII";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point(0, 560);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(700, 22);
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// uIToolStripMenuItem
			// 
			this.uIToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gPSToolStripMenuItem,
            this.radioToolStripMenuItem});
			this.uIToolStripMenuItem.Name = "uIToolStripMenuItem";
			this.uIToolStripMenuItem.Size = new System.Drawing.Size(30, 20);
			this.uIToolStripMenuItem.Text = "UI";
			// 
			// gPSToolStripMenuItem
			// 
			this.gPSToolStripMenuItem.Name = "gPSToolStripMenuItem";
			this.gPSToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.gPSToolStripMenuItem.Text = "GPS";
			this.gPSToolStripMenuItem.Click += new System.EventHandler(this.gPSToolStripMenuItem_Click);
			// 
			// radioToolStripMenuItem
			// 
			this.radioToolStripMenuItem.Name = "radioToolStripMenuItem";
			this.radioToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.radioToolStripMenuItem.Text = "Radio";
			// 
			// TerminalWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(700, 582);
			this.Controls.Add(this.TermDisplay);
			this.Controls.Add(this.MainMenu);
			this.Controls.Add(this.statusStrip1);
			this.MainMenuStrip = this.MainMenu;
			this.Name = "TerminalWindow";
			this.Text = "CRTERM";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TerminalWindow_FormClosed);
			this.Load += new System.EventHandler(this.TerminalWindow_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TerminalWindow_KeyDown);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TerminalWindow_KeyPress);
			this.MainMenu.ResumeLayout(false);
			this.MainMenu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private TerminalDisplay TermDisplay;
		private System.Windows.Forms.MenuStrip MainMenu;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newConnectionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openConnectionProfileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveConnectionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem connectionPropertiesToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem saveConnectionAsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem transferToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem captureASCIIToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sendASCIIToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem uIToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem gPSToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem radioToolStripMenuItem;
		private System.Windows.Forms.StatusStrip statusStrip1;
	}
}