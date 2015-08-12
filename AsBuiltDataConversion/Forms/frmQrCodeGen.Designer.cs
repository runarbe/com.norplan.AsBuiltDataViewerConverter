namespace Norplan.Adm.AsBuiltDataConversion.Forms
{
    partial class frmQrCodeGen
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
            this.btnGenerateQRCode = new System.Windows.Forms.Button();
            this.pbQrCode = new System.Windows.Forms.PictureBox();
            this.tbContentForEncoding = new System.Windows.Forms.TextBox();
            this.lblQrStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbQrCode)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGenerateQRCode
            // 
            this.btnGenerateQRCode.Location = new System.Drawing.Point(16, 47);
            this.btnGenerateQRCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGenerateQRCode.Name = "btnGenerateQRCode";
            this.btnGenerateQRCode.Size = new System.Drawing.Size(535, 28);
            this.btnGenerateQRCode.TabIndex = 0;
            this.btnGenerateQRCode.Text = "Generate";
            this.btnGenerateQRCode.UseVisualStyleBackColor = true;
            this.btnGenerateQRCode.Click += new System.EventHandler(this.button1_Click);
            // 
            // pbQrCode
            // 
            this.pbQrCode.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbQrCode.Location = new System.Drawing.Point(17, 94);
            this.pbQrCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbQrCode.Name = "pbQrCode";
            this.pbQrCode.Size = new System.Drawing.Size(533, 492);
            this.pbQrCode.TabIndex = 1;
            this.pbQrCode.TabStop = false;
            // 
            // tbContentForEncoding
            // 
            this.tbContentForEncoding.Location = new System.Drawing.Point(129, 15);
            this.tbContentForEncoding.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbContentForEncoding.Name = "tbContentForEncoding";
            this.tbContentForEncoding.Size = new System.Drawing.Size(420, 22);
            this.tbContentForEncoding.TabIndex = 2;
            // 
            // lblQrStatus
            // 
            this.lblQrStatus.AutoSize = true;
            this.lblQrStatus.Location = new System.Drawing.Point(16, 602);
            this.lblQrStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQrStatus.Name = "lblQrStatus";
            this.lblQrStatus.Size = new System.Drawing.Size(61, 17);
            this.lblQrStatus.TabIndex = 3;
            this.lblQrStatus.Text = "Ready...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Text to encode";
            // 
            // frmQrCodeGen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 633);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblQrStatus);
            this.Controls.Add(this.tbContentForEncoding);
            this.Controls.Add(this.pbQrCode);
            this.Controls.Add(this.btnGenerateQRCode);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmQrCodeGen";
            this.ShowIcon = false;
            this.Text = "Generate QR-code";
            ((System.ComponentModel.ISupportInitialize)(this.pbQrCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerateQRCode;
        private System.Windows.Forms.PictureBox pbQrCode;
        private System.Windows.Forms.TextBox tbContentForEncoding;
        private System.Windows.Forms.Label lblQrStatus;
        private System.Windows.Forms.Label label1;
    }
}