namespace CRBasic
{
    partial class CRBasicMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private TerminalUI.DisplayControl Display = null;

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
            this.Display = new TerminalUI.DisplayControl();
            this.SuspendLayout();
            // 
            // Display
            // 
            this.Display.AddLinefeed = false;
            this.Display.BackColor = System.Drawing.Color.DimGray;
            this.Display.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Display.BackspaceDelete = false;
            this.Display.BackspaceOverwrite = false;
            this.Display.BackspacePull = false;
            this.Display.CharUnderCursor = ' ';
            this.Display.CurrentAttribute = TerminalUI.CharacterCell.Attributes.Normal;
            this.Display.CurrentBackground = TerminalUI.CharacterCell.ColorCodes.Black;
            this.Display.CurrentColumn = 0;
            this.Display.CurrentRow = 0;
            this.Display.CurrentTextColor = TerminalUI.CharacterCell.ColorCodes.Gray;
            this.Display.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Display.EchoMode = TerminalUI.Terminals.EchoModes.None;
            this.Display.Editor = null;
            this.Display.Font = new System.Drawing.Font("Classic Console", 34.90461F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Display.InsertMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.Display.LineWrap = false;
            this.Display.Location = new System.Drawing.Point(0, 0);
            this.Display.Margin = new System.Windows.Forms.Padding(774801, 465530, 774801, 465530);
            this.Display.Name = "Display";
            this.Display.Size = new System.Drawing.Size(1411, 709);
            this.Display.TabIndex = 0;
            this.Display.Terminal = null;
            this.Display.TextCursor = TerminalUI.TextCursorStyles.Underline;
            // 
            // CRBasicMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1411, 709);
            this.Controls.Add(this.Display);
            this.Name = "CRBasicMain";
            this.Text = "CR BASIC";
            this.Load += new System.EventHandler(this.CRBasicMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CRBasicMain_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CRBasicMain_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CRBasicMain_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

