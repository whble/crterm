﻿namespace CRTerm.Config
{
    partial class ConfigDialog
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
            this.ConfigDialogLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ConfigDialogLabel
            // 
            this.ConfigDialogLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ConfigDialogLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigDialogLabel.Location = new System.Drawing.Point(0, 0);
            this.ConfigDialogLabel.Name = "ConfigDialogLabel";
            this.ConfigDialogLabel.Size = new System.Drawing.Size(519, 28);
            this.ConfigDialogLabel.TabIndex = 0;
            this.ConfigDialogLabel.Text = "Configuration";
            // 
            // ConfigDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 371);
            this.Controls.Add(this.ConfigDialogLabel);
            this.Name = "ConfigDialog";
            this.Text = "ConfigDialog";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label ConfigDialogLabel;
    }
}