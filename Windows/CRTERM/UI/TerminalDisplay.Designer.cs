namespace CRTERM.UI
{
	partial class TerminalDisplay
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.cursorTimer = new System.Windows.Forms.Timer(this.components);
			this.redrawTimer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// cursorTimer
			// 
			this.cursorTimer.Interval = 500;
			this.cursorTimer.Tick += new System.EventHandler(this.CursorTimer_Tick);
			// 
			// redrawTimer
			// 
			this.redrawTimer.Interval = 50;
			this.redrawTimer.Tick += new System.EventHandler(this.redrawTimer_Tick);
			// 
			// TerminalDisplay
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(0)))));
			this.DoubleBuffered = true;
			this.ForeColor = System.Drawing.Color.LightGreen;
			this.Name = "TerminalDisplay";
			this.Size = new System.Drawing.Size(640, 480);
			this.Load += new System.EventHandler(this.TerminalDisplay_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.TerminalDisplay_Paint);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HandleKeyDown);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HandleKeyPress);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer cursorTimer;
		private System.Windows.Forms.Timer redrawTimer;
	}
}
