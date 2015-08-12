namespace Norplan.Adm.AsBuiltDataConversion.Forms
{
    partial class frmMySQLConnection
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbMySQLServer = new System.Windows.Forms.TextBox();
            this.tbMySQLPort = new System.Windows.Forms.TextBox();
            this.tbMySQLUser = new System.Windows.Forms.TextBox();
            this.tbMySQLPass = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMySQLSchema = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbMySQLDriver = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkBoxTruncate = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 58);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server";
            // 
            // tbMySQLServer
            // 
            this.tbMySQLServer.Location = new System.Drawing.Point(109, 54);
            this.tbMySQLServer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbMySQLServer.Name = "tbMySQLServer";
            this.tbMySQLServer.Size = new System.Drawing.Size(253, 22);
            this.tbMySQLServer.TabIndex = 1;
            this.tbMySQLServer.Text = "localhost";
            // 
            // tbMySQLPort
            // 
            this.tbMySQLPort.Location = new System.Drawing.Point(109, 87);
            this.tbMySQLPort.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbMySQLPort.Name = "tbMySQLPort";
            this.tbMySQLPort.Size = new System.Drawing.Size(253, 22);
            this.tbMySQLPort.TabIndex = 2;
            this.tbMySQLPort.Text = "3306";
            // 
            // tbMySQLUser
            // 
            this.tbMySQLUser.Location = new System.Drawing.Point(109, 151);
            this.tbMySQLUser.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbMySQLUser.Name = "tbMySQLUser";
            this.tbMySQLUser.Size = new System.Drawing.Size(253, 22);
            this.tbMySQLUser.TabIndex = 3;
            this.tbMySQLUser.Text = "root";
            // 
            // tbMySQLPass
            // 
            this.tbMySQLPass.Location = new System.Drawing.Point(109, 185);
            this.tbMySQLPass.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbMySQLPass.Name = "tbMySQLPass";
            this.tbMySQLPass.Size = new System.Drawing.Size(253, 22);
            this.tbMySQLPass.TabIndex = 4;
            this.tbMySQLPass.Text = "root";
            this.tbMySQLPass.UseSystemPasswordChar = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(156, 261);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(264, 261);
            this.btnExport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(100, 28);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 91);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 155);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Username";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 188);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Password";
            // 
            // tbMySQLSchema
            // 
            this.tbMySQLSchema.Location = new System.Drawing.Point(109, 119);
            this.tbMySQLSchema.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbMySQLSchema.Name = "tbMySQLSchema";
            this.tbMySQLSchema.Size = new System.Drawing.Size(253, 22);
            this.tbMySQLSchema.TabIndex = 7;
            this.tbMySQLSchema.Text = "admadr";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 123);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Database";
            // 
            // cbMySQLDriver
            // 
            this.cbMySQLDriver.FormattingEnabled = true;
            this.cbMySQLDriver.Location = new System.Drawing.Point(109, 21);
            this.cbMySQLDriver.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbMySQLDriver.Name = "cbMySQLDriver";
            this.cbMySQLDriver.Size = new System.Drawing.Size(253, 24);
            this.cbMySQLDriver.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 25);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "ODBC driver";
            // 
            // chkBoxTruncate
            // 
            this.chkBoxTruncate.AutoSize = true;
            this.chkBoxTruncate.Location = new System.Drawing.Point(109, 217);
            this.chkBoxTruncate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkBoxTruncate.Name = "chkBoxTruncate";
            this.chkBoxTruncate.Size = new System.Drawing.Size(122, 21);
            this.chkBoxTruncate.TabIndex = 10;
            this.chkBoxTruncate.Text = "Truncate table";
            this.chkBoxTruncate.UseVisualStyleBackColor = true;
            // 
            // frmMySQLConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 304);
            this.Controls.Add(this.chkBoxTruncate);
            this.Controls.Add(this.cbMySQLDriver);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbMySQLSchema);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tbMySQLPass);
            this.Controls.Add(this.tbMySQLUser);
            this.Controls.Add(this.tbMySQLPort);
            this.Controls.Add(this.tbMySQLServer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmMySQLConnection";
            this.ShowIcon = false;
            this.Text = "Export to MySQL";
            this.Load += new System.EventHandler(this.frmMySQLConnection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbMySQLServer;
        private System.Windows.Forms.TextBox tbMySQLPort;
        private System.Windows.Forms.TextBox tbMySQLUser;
        private System.Windows.Forms.TextBox tbMySQLPass;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbMySQLSchema;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.ComboBox cbMySQLDriver;
        private System.Windows.Forms.CheckBox chkBoxTruncate;
    }
}