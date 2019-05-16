namespace TerminalUITest
{
    partial class TestWindow
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
            TerminalUI.Terminals.ANSITerminal ansiTerminal1 = new TerminalUI.Terminals.ANSITerminal();
            this.terminalControl1 = new TerminalUI.DisplayControl();
            this.SuspendLayout();
            // 
            // terminalControl1
            // 
            this.terminalControl1.AddLinefeed = false;
            this.terminalControl1.BackColor = System.Drawing.Color.DimGray;
            this.terminalControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.terminalControl1.CurrentAttribute = TerminalUI.CharacterCell.Attributes.Normal;
            this.terminalControl1.CurrentBackground = TerminalUI.CharacterCell.ColorCodes.Black;
            this.terminalControl1.CurrentColumn = 0;
            this.terminalControl1.CurrentRow = 0;
            this.terminalControl1.CurrentTextColor = TerminalUI.CharacterCell.ColorCodes.Gray;
            this.terminalControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.terminalControl1.EchoMode = TerminalUI.Terminals.EchoModes.EchoOff;
            this.terminalControl1.Font = new System.Drawing.Font("Consolas", 15.227F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.terminalControl1.LineWrap = false;
            this.terminalControl1.Location = new System.Drawing.Point(0, 0);
            this.terminalControl1.Name = "terminalControl1";
            this.terminalControl1.Size = new System.Drawing.Size(1386, 731);
            this.terminalControl1.TabIndex = 0;
            ansiTerminal1.BackspaceDeleteMode = false;
            ansiTerminal1.Display = null;
            ansiTerminal1.EchoMode = TerminalUI.Terminals.EchoModes.EchoOff;
            this.terminalControl1.Terminal = ansiTerminal1;
            this.terminalControl1.TextCursor = TerminalUI.TextCursorStyles.Underline;
            // 
            // TestWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1386, 731);
            this.Controls.Add(this.terminalControl1);
            this.Name = "TestWindow";
            this.Text = "Terminal Test";
            this.Load += new System.EventHandler(this.TestWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private TerminalUI.DisplayControl terminalControl1;
    }
}

