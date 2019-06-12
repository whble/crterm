namespace CRTerm.Transfer
{
    partial class TransferControl
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
            this.remainingLabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.CancelTransfer = new System.Windows.Forms.Button();
            this.protocolLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.estimatedTimeLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.elapsedTimeLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.filenameLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.bytesSentLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.BytesToSendLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.operationLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // remainingLabel
            // 
            this.remainingLabel.AutoSize = true;
            this.remainingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remainingLabel.Location = new System.Drawing.Point(3, 345);
            this.remainingLabel.Name = "remainingLabel";
            this.remainingLabel.Size = new System.Drawing.Size(39, 13);
            this.remainingLabel.TabIndex = 35;
            this.remainingLabel.Text = "00:00";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 322);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 13);
            this.label10.TabIndex = 34;
            this.label10.Text = "Remaining";
            // 
            // CancelTransfer
            // 
            this.CancelTransfer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.CancelTransfer.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelTransfer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelTransfer.ForeColor = System.Drawing.Color.Silver;
            this.CancelTransfer.Location = new System.Drawing.Point(47, 403);
            this.CancelTransfer.Name = "CancelTransfer";
            this.CancelTransfer.Size = new System.Drawing.Size(120, 23);
            this.CancelTransfer.TabIndex = 33;
            this.CancelTransfer.Text = "Cancel";
            this.CancelTransfer.UseVisualStyleBackColor = false;
            this.CancelTransfer.Click += new System.EventHandler(this.CancelTransfer_Click);
            // 
            // protocolLabel
            // 
            this.protocolLabel.AutoSize = true;
            this.protocolLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.protocolLabel.Location = new System.Drawing.Point(3, 23);
            this.protocolLabel.Name = "protocolLabel";
            this.protocolLabel.Size = new System.Drawing.Size(116, 13);
            this.protocolLabel.TabIndex = 32;
            this.protocolLabel.Text = "[unknown protocol]";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "Protocol";
            // 
            // estimatedTimeLabel
            // 
            this.estimatedTimeLabel.AutoSize = true;
            this.estimatedTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.estimatedTimeLabel.Location = new System.Drawing.Point(3, 299);
            this.estimatedTimeLabel.Name = "estimatedTimeLabel";
            this.estimatedTimeLabel.Size = new System.Drawing.Size(39, 13);
            this.estimatedTimeLabel.TabIndex = 30;
            this.estimatedTimeLabel.Text = "00:00";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 276);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Estimated Time";
            // 
            // elapsedTimeLabel
            // 
            this.elapsedTimeLabel.AutoSize = true;
            this.elapsedTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.elapsedTimeLabel.Location = new System.Drawing.Point(3, 253);
            this.elapsedTimeLabel.Name = "elapsedTimeLabel";
            this.elapsedTimeLabel.Size = new System.Drawing.Size(39, 13);
            this.elapsedTimeLabel.TabIndex = 28;
            this.elapsedTimeLabel.Text = "00:00";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 230);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "Elapsed Time";
            // 
            // filenameLabel
            // 
            this.filenameLabel.AutoSize = true;
            this.filenameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filenameLabel.Location = new System.Drawing.Point(3, 69);
            this.filenameLabel.Name = "filenameLabel";
            this.filenameLabel.Size = new System.Drawing.Size(66, 13);
            this.filenameLabel.TabIndex = 26;
            this.filenameLabel.Text = "[unknown]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Filename";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 374);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(200, 23);
            this.progressBar1.TabIndex = 24;
            // 
            // bytesSentLabel
            // 
            this.bytesSentLabel.AutoSize = true;
            this.bytesSentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bytesSentLabel.Location = new System.Drawing.Point(3, 207);
            this.bytesSentLabel.Name = "bytesSentLabel";
            this.bytesSentLabel.Size = new System.Drawing.Size(14, 13);
            this.bytesSentLabel.TabIndex = 23;
            this.bytesSentLabel.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Bytes Sent";
            // 
            // BytesToSendLabel
            // 
            this.BytesToSendLabel.AutoSize = true;
            this.BytesToSendLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BytesToSendLabel.Location = new System.Drawing.Point(3, 161);
            this.BytesToSendLabel.Name = "BytesToSendLabel";
            this.BytesToSendLabel.Size = new System.Drawing.Size(14, 13);
            this.BytesToSendLabel.TabIndex = 21;
            this.BytesToSendLabel.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Bytes To Send";
            // 
            // operationLabel
            // 
            this.operationLabel.AutoSize = true;
            this.operationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.operationLabel.Location = new System.Drawing.Point(3, 115);
            this.operationLabel.Name = "operationLabel";
            this.operationLabel.Size = new System.Drawing.Size(97, 13);
            this.operationLabel.TabIndex = 19;
            this.operationLabel.Text = "Send / Receive";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Operation";
            // 
            // TransferControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.remainingLabel);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.CancelTransfer);
            this.Controls.Add(this.protocolLabel);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.estimatedTimeLabel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.elapsedTimeLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.filenameLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.bytesSentLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.BytesToSendLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.operationLabel);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.Silver;
            this.Name = "TransferControl";
            this.Size = new System.Drawing.Size(210, 438);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label remainingLabel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button CancelTransfer;
        private System.Windows.Forms.Label protocolLabel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label estimatedTimeLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label elapsedTimeLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label filenameLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label bytesSentLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label BytesToSendLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label operationLabel;
        private System.Windows.Forms.Label label1;
    }
}
