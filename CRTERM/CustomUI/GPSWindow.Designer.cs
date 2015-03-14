namespace CRTERM.CustomUI
{
	partial class GPSWindow
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
			this.FixString = new System.Windows.Forms.TextBox();
			this.FixStringGroup = new System.Windows.Forms.GroupBox();
			this.FixStringGroup.SuspendLayout();
			this.SuspendLayout();
			// 
			// FixString
			// 
			this.FixString.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FixString.Location = new System.Drawing.Point(3, 16);
			this.FixString.Name = "FixString";
			this.FixString.Size = new System.Drawing.Size(540, 20);
			this.FixString.TabIndex = 0;
			// 
			// FixStringGroup
			// 
			this.FixStringGroup.Controls.Add(this.FixString);
			this.FixStringGroup.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.FixStringGroup.Location = new System.Drawing.Point(0, 146);
			this.FixStringGroup.Name = "FixStringGroup";
			this.FixStringGroup.Size = new System.Drawing.Size(546, 41);
			this.FixStringGroup.TabIndex = 1;
			this.FixStringGroup.TabStop = false;
			this.FixStringGroup.Text = "Last Fix String";
			// 
			// GPSWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(546, 187);
			this.Controls.Add(this.FixStringGroup);
			this.Name = "GPSWindow";
			this.Text = "GPSWindow";
			this.FixStringGroup.ResumeLayout(false);
			this.FixStringGroup.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox FixString;
		private System.Windows.Forms.GroupBox FixStringGroup;
	}
}