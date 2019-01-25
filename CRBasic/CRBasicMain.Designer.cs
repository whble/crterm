namespace CRBasic
{
    partial class CRBasicMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CRBasicMain));
            this.frameBuffer1 = new TerminalControl.FrameBuffer();
            this.SuspendLayout();
            // 
            // frameBuffer1
            // 
            this.frameBuffer1.AddLinefeed = false;
            this.frameBuffer1.BackColor = System.Drawing.Color.DimGray;
            this.frameBuffer1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("frameBuffer1.BackgroundImage")));
            this.frameBuffer1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.frameBuffer1.BackspaceDelete = false;
            this.frameBuffer1.BackspaceOverwrite = false;
            this.frameBuffer1.BackspacePull = false;
            this.frameBuffer1.CurrentAttribute = TerminalControl.AttributeCodes.Normal;
            this.frameBuffer1.CursorStyle = TerminalControl.CursorStyles.Underline;
            this.frameBuffer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frameBuffer1.EditMode = TerminalControl.Terminals.EditModes.None;
            this.frameBuffer1.Font = new System.Drawing.Font("Classic Console", 40.8064F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.frameBuffer1.LineWrap = false;
            this.frameBuffer1.Location = new System.Drawing.Point(0, 0);
            this.frameBuffer1.Margin = new System.Windows.Forms.Padding(774801, 465530, 774801, 465530);
            this.frameBuffer1.Name = "frameBuffer1";
            this.frameBuffer1.Size = new System.Drawing.Size(1264, 797);
            this.frameBuffer1.TabIndex = 0;
            this.frameBuffer1.Terminal = null;
            this.frameBuffer1.X = 0;
            this.frameBuffer1.Y = 24;
            // 
            // CRBasicMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 797);
            this.Controls.Add(this.frameBuffer1);
            this.Name = "CRBasicMain";
            this.Text = "CR BASIC";
            this.Load += new System.EventHandler(this.CRBasicMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private TerminalControl.FrameBuffer frameBuffer1;
    }
}

