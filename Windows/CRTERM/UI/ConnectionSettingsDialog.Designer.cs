namespace CRTERM.UI
{
	partial class ConnectionSettingsDialog
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
      this.panel3 = new System.Windows.Forms.Panel();
      this.Cancel_Button = new System.Windows.Forms.Button();
      this.OkButton = new System.Windows.Forms.Button();
      this.ConnectionNamePanel = new System.Windows.Forms.Panel();
      this.NameText = new System.Windows.Forms.TextBox();
      this.NameLabel = new System.Windows.Forms.Label();
      this.TransportGroupBox = new System.Windows.Forms.GroupBox();
      this.TransportSettings = new System.Windows.Forms.Panel();
      this.TransportProperties = new CRTERM.UI.PropertyPanel();
      this.TransportButtons = new CRTERM.UI.ButtonPanel();
      this.ModemGroupBox = new System.Windows.Forms.GroupBox();
      this.ModemProperties = new CRTERM.UI.PropertyPanel();
      this.ModemButtons = new CRTERM.UI.ButtonPanel();
      this.panel2 = new System.Windows.Forms.Panel();
      this.EmulatorGroupBox = new System.Windows.Forms.GroupBox();
      this.EmulatorProperties = new CRTERM.UI.PropertyPanel();
      this.EmulatorButtons = new CRTERM.UI.ButtonPanel();
      this.splitter2 = new System.Windows.Forms.Splitter();
      this.splitter1 = new System.Windows.Forms.Splitter();
      this.panel3.SuspendLayout();
      this.ConnectionNamePanel.SuspendLayout();
      this.TransportGroupBox.SuspendLayout();
      this.TransportSettings.SuspendLayout();
      this.ModemGroupBox.SuspendLayout();
      this.panel2.SuspendLayout();
      this.EmulatorGroupBox.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.Cancel_Button);
      this.panel3.Controls.Add(this.OkButton);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel3.Location = new System.Drawing.Point(0, 444);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(584, 42);
      this.panel3.TabIndex = 0;
      // 
      // Cancel_Button
      // 
      this.Cancel_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.Cancel_Button.Location = new System.Drawing.Point(497, 9);
      this.Cancel_Button.Name = "Cancel_Button";
      this.Cancel_Button.Size = new System.Drawing.Size(75, 23);
      this.Cancel_Button.TabIndex = 1;
      this.Cancel_Button.Text = "Cancel";
      this.Cancel_Button.UseVisualStyleBackColor = true;
      this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);
      // 
      // OkButton
      // 
      this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.OkButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.OkButton.Location = new System.Drawing.Point(416, 9);
      this.OkButton.Name = "OkButton";
      this.OkButton.Size = new System.Drawing.Size(75, 23);
      this.OkButton.TabIndex = 0;
      this.OkButton.Text = "Ok";
      this.OkButton.UseVisualStyleBackColor = true;
      this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
      // 
      // ConnectionNamePanel
      // 
      this.ConnectionNamePanel.Controls.Add(this.NameText);
      this.ConnectionNamePanel.Controls.Add(this.NameLabel);
      this.ConnectionNamePanel.Dock = System.Windows.Forms.DockStyle.Top;
      this.ConnectionNamePanel.Location = new System.Drawing.Point(0, 0);
      this.ConnectionNamePanel.Name = "ConnectionNamePanel";
      this.ConnectionNamePanel.Padding = new System.Windows.Forms.Padding(5, 10, 5, 0);
      this.ConnectionNamePanel.Size = new System.Drawing.Size(584, 29);
      this.ConnectionNamePanel.TabIndex = 1;
      // 
      // NameText
      // 
      this.NameText.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.NameText.Dock = System.Windows.Forms.DockStyle.Fill;
      this.NameText.Location = new System.Drawing.Point(102, 10);
      this.NameText.Name = "NameText";
      this.NameText.Size = new System.Drawing.Size(477, 13);
      this.NameText.TabIndex = 1;
      // 
      // NameLabel
      // 
      this.NameLabel.Dock = System.Windows.Forms.DockStyle.Left;
      this.NameLabel.Location = new System.Drawing.Point(5, 10);
      this.NameLabel.Name = "NameLabel";
      this.NameLabel.Size = new System.Drawing.Size(97, 19);
      this.NameLabel.TabIndex = 0;
      this.NameLabel.Text = "Connection Name";
      // 
      // TransportGroupBox
      // 
      this.TransportGroupBox.Controls.Add(this.TransportSettings);
      this.TransportGroupBox.Controls.Add(this.TransportButtons);
      this.TransportGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
      this.TransportGroupBox.Location = new System.Drawing.Point(0, 0);
      this.TransportGroupBox.Name = "TransportGroupBox";
      this.TransportGroupBox.Size = new System.Drawing.Size(567, 220);
      this.TransportGroupBox.TabIndex = 2;
      this.TransportGroupBox.TabStop = false;
      this.TransportGroupBox.Text = "Port";
      // 
      // TransportSettings
      // 
      this.TransportSettings.AutoScroll = true;
      this.TransportSettings.Controls.Add(this.TransportProperties);
      this.TransportSettings.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TransportSettings.Location = new System.Drawing.Point(3, 35);
      this.TransportSettings.Name = "TransportSettings";
      this.TransportSettings.Size = new System.Drawing.Size(561, 182);
      this.TransportSettings.TabIndex = 1;
      // 
      // TransportProperties
      // 
      this.TransportProperties.AutoScroll = true;
      this.TransportProperties.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.TransportProperties.ConfigData = null;
      this.TransportProperties.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TransportProperties.Location = new System.Drawing.Point(0, 0);
      this.TransportProperties.Name = "TransportProperties";
      this.TransportProperties.Padding = new System.Windows.Forms.Padding(5, 10, 5, 0);
      this.TransportProperties.Size = new System.Drawing.Size(561, 182);
      this.TransportProperties.TabIndex = 0;
      // 
      // TransportButtons
      // 
      this.TransportButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.TransportButtons.Dock = System.Windows.Forms.DockStyle.Top;
      this.TransportButtons.Location = new System.Drawing.Point(3, 16);
      this.TransportButtons.Name = "TransportButtons";
      this.TransportButtons.Size = new System.Drawing.Size(561, 19);
      this.TransportButtons.TabIndex = 2;
      this.TransportButtons.TextChanged += new System.EventHandler(this.TransportButtons_TextChanged);
      // 
      // ModemGroupBox
      // 
      this.ModemGroupBox.Controls.Add(this.ModemProperties);
      this.ModemGroupBox.Controls.Add(this.ModemButtons);
      this.ModemGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
      this.ModemGroupBox.Location = new System.Drawing.Point(0, 223);
      this.ModemGroupBox.Name = "ModemGroupBox";
      this.ModemGroupBox.Size = new System.Drawing.Size(567, 220);
      this.ModemGroupBox.TabIndex = 3;
      this.ModemGroupBox.TabStop = false;
      this.ModemGroupBox.Text = "Modem";
      // 
      // ModemProperties
      // 
      this.ModemProperties.AutoScroll = true;
      this.ModemProperties.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.ModemProperties.ConfigData = null;
      this.ModemProperties.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ModemProperties.Location = new System.Drawing.Point(3, 37);
      this.ModemProperties.Name = "ModemProperties";
      this.ModemProperties.Padding = new System.Windows.Forms.Padding(5, 10, 5, 0);
      this.ModemProperties.Size = new System.Drawing.Size(561, 180);
      this.ModemProperties.TabIndex = 1;
      // 
      // ModemButtons
      // 
      this.ModemButtons.Dock = System.Windows.Forms.DockStyle.Top;
      this.ModemButtons.Location = new System.Drawing.Point(3, 16);
      this.ModemButtons.Name = "ModemButtons";
      this.ModemButtons.Size = new System.Drawing.Size(561, 21);
      this.ModemButtons.TabIndex = 0;
      this.ModemButtons.TextChanged += new System.EventHandler(this.ModemButtons_TextChanged);
      // 
      // panel2
      // 
      this.panel2.AutoScroll = true;
      this.panel2.AutoScrollMargin = new System.Drawing.Size(0, 5);
      this.panel2.Controls.Add(this.EmulatorGroupBox);
      this.panel2.Controls.Add(this.splitter2);
      this.panel2.Controls.Add(this.ModemGroupBox);
      this.panel2.Controls.Add(this.splitter1);
      this.panel2.Controls.Add(this.TransportGroupBox);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(0, 29);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(584, 415);
      this.panel2.TabIndex = 4;
      // 
      // EmulatorGroupBox
      // 
      this.EmulatorGroupBox.Controls.Add(this.EmulatorProperties);
      this.EmulatorGroupBox.Controls.Add(this.EmulatorButtons);
      this.EmulatorGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
      this.EmulatorGroupBox.Location = new System.Drawing.Point(0, 446);
      this.EmulatorGroupBox.Name = "EmulatorGroupBox";
      this.EmulatorGroupBox.Size = new System.Drawing.Size(567, 220);
      this.EmulatorGroupBox.TabIndex = 6;
      this.EmulatorGroupBox.TabStop = false;
      this.EmulatorGroupBox.Text = "Terminal";
      // 
      // EmulatorProperties
      // 
      this.EmulatorProperties.AutoScroll = true;
      this.EmulatorProperties.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.EmulatorProperties.ConfigData = null;
      this.EmulatorProperties.Dock = System.Windows.Forms.DockStyle.Fill;
      this.EmulatorProperties.Location = new System.Drawing.Point(3, 37);
      this.EmulatorProperties.Name = "EmulatorProperties";
      this.EmulatorProperties.Padding = new System.Windows.Forms.Padding(5, 10, 5, 0);
      this.EmulatorProperties.Size = new System.Drawing.Size(561, 180);
      this.EmulatorProperties.TabIndex = 1;
      // 
      // EmulatorButtons
      // 
      this.EmulatorButtons.Dock = System.Windows.Forms.DockStyle.Top;
      this.EmulatorButtons.Location = new System.Drawing.Point(3, 16);
      this.EmulatorButtons.Name = "EmulatorButtons";
      this.EmulatorButtons.Size = new System.Drawing.Size(561, 21);
      this.EmulatorButtons.TabIndex = 0;
      this.EmulatorButtons.TextChanged += new System.EventHandler(this.EmulatorButtons_TextChanged);
      // 
      // splitter2
      // 
      this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
      this.splitter2.Location = new System.Drawing.Point(0, 443);
      this.splitter2.Name = "splitter2";
      this.splitter2.Size = new System.Drawing.Size(567, 3);
      this.splitter2.TabIndex = 5;
      this.splitter2.TabStop = false;
      // 
      // splitter1
      // 
      this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
      this.splitter1.Location = new System.Drawing.Point(0, 220);
      this.splitter1.Name = "splitter1";
      this.splitter1.Size = new System.Drawing.Size(567, 3);
      this.splitter1.TabIndex = 4;
      this.splitter1.TabStop = false;
      // 
      // ConnectionSettingsDialog
      // 
      this.AcceptButton = this.OkButton;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoScrollMargin = new System.Drawing.Size(5, 0);
      this.CancelButton = this.Cancel_Button;
      this.ClientSize = new System.Drawing.Size(584, 486);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.ConnectionNamePanel);
      this.Controls.Add(this.panel3);
      this.Name = "ConnectionSettingsDialog";
      this.Text = "Connection Options";
      this.Load += new System.EventHandler(this.ConnectionSettingsDialog_Load);
      this.panel3.ResumeLayout(false);
      this.ConnectionNamePanel.ResumeLayout(false);
      this.ConnectionNamePanel.PerformLayout();
      this.TransportGroupBox.ResumeLayout(false);
      this.TransportSettings.ResumeLayout(false);
      this.ModemGroupBox.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.EmulatorGroupBox.ResumeLayout(false);
      this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Button Cancel_Button;
		private System.Windows.Forms.Button OkButton;
		private System.Windows.Forms.Panel ConnectionNamePanel;
		private System.Windows.Forms.TextBox NameText;
		private System.Windows.Forms.Label NameLabel;
		private System.Windows.Forms.GroupBox TransportGroupBox;
		private System.Windows.Forms.Panel TransportSettings;
		private PropertyPanel TransportProperties;
		private System.Windows.Forms.GroupBox ModemGroupBox;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Splitter splitter1;
		private ButtonPanel TransportButtons;
		private PropertyPanel ModemProperties;
		private ButtonPanel ModemButtons;
		private System.Windows.Forms.GroupBox EmulatorGroupBox;
		private PropertyPanel EmulatorProperties;
		private ButtonPanel EmulatorButtons;
		private System.Windows.Forms.Splitter splitter2;
	}
}