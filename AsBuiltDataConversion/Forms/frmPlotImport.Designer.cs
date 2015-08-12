namespace Norplan.Adm.AsBuiltDataConversion.Forms
{
    partial class frmPlotImport
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
            this.dlgOpenShapefile = new System.Windows.Forms.OpenFileDialog();
            this.tlp = new System.Windows.Forms.TableLayoutPanel();
            this.lblSelectSourceFile = new System.Windows.Forms.Label();
            this.lblSectorField = new System.Windows.Forms.Label();
            this.lblPlotField = new System.Windows.Forms.Label();
            this.tbSectorPlotFile = new System.Windows.Forms.TextBox();
            this.cbSectorField = new System.Windows.Forms.ComboBox();
            this.cbPlotField = new System.Windows.Forms.ComboBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.lblSelectZoneFile = new System.Windows.Forms.Label();
            this.lblSelectZoneField = new System.Windows.Forms.Label();
            this.tbZoneFile = new System.Windows.Forms.TextBox();
            this.cbZoneField = new System.Windows.Forms.ComboBox();
            this.btnSelectZoneFile = new System.Windows.Forms.Button();
            this.btnSelectSectorPlotFile = new System.Windows.Forms.Button();
            this.lblCurrentStatus = new System.Windows.Forms.Label();
            this.lblStatusMessage = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tlp.SuspendLayout();
            this.SuspendLayout();
            // 
            // dlgOpenShapefile
            // 
            this.dlgOpenShapefile.FileName = "*.shp";
            this.dlgOpenShapefile.Filter = "ESRI Shapefile|*.shp";
            this.dlgOpenShapefile.Title = "Select ESRI Shapefile";
            // 
            // tlp
            // 
            this.tlp.ColumnCount = 4;
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp.Controls.Add(this.lblSelectSourceFile, 0, 3);
            this.tlp.Controls.Add(this.lblSectorField, 0, 4);
            this.tlp.Controls.Add(this.lblPlotField, 0, 5);
            this.tlp.Controls.Add(this.tbSectorPlotFile, 1, 3);
            this.tlp.Controls.Add(this.cbSectorField, 1, 4);
            this.tlp.Controls.Add(this.cbPlotField, 1, 5);
            this.tlp.Controls.Add(this.btnImport, 2, 6);
            this.tlp.Controls.Add(this.lblSelectZoneFile, 0, 1);
            this.tlp.Controls.Add(this.lblSelectZoneField, 0, 2);
            this.tlp.Controls.Add(this.tbZoneFile, 1, 1);
            this.tlp.Controls.Add(this.cbZoneField, 1, 2);
            this.tlp.Controls.Add(this.btnSelectZoneFile, 3, 1);
            this.tlp.Controls.Add(this.btnSelectSectorPlotFile, 3, 3);
            this.tlp.Controls.Add(this.lblCurrentStatus, 0, 0);
            this.tlp.Controls.Add(this.lblStatusMessage, 1, 0);
            this.tlp.Controls.Add(this.btnCancel, 0, 6);
            this.tlp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp.Location = new System.Drawing.Point(0, 0);
            this.tlp.Name = "tlp";
            this.tlp.RowCount = 7;
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp.Size = new System.Drawing.Size(633, 237);
            this.tlp.TabIndex = 2;
            this.tlp.Paint += new System.Windows.Forms.PaintEventHandler(this.tlp_Paint);
            // 
            // lblSelectSourceFile
            // 
            this.lblSelectSourceFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelectSourceFile.AutoSize = true;
            this.lblSelectSourceFile.Location = new System.Drawing.Point(3, 91);
            this.lblSelectSourceFile.Name = "lblSelectSourceFile";
            this.lblSelectSourceFile.Size = new System.Drawing.Size(152, 41);
            this.lblSelectSourceFile.TabIndex = 3;
            this.lblSelectSourceFile.Text = "Select source file";
            this.lblSelectSourceFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSectorField
            // 
            this.lblSectorField.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSectorField.AutoSize = true;
            this.lblSectorField.Location = new System.Drawing.Point(3, 132);
            this.lblSectorField.Name = "lblSectorField";
            this.lblSectorField.Size = new System.Drawing.Size(152, 30);
            this.lblSectorField.TabIndex = 4;
            this.lblSectorField.Text = "Sector name field";
            this.lblSectorField.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPlotField
            // 
            this.lblPlotField.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPlotField.AutoSize = true;
            this.lblPlotField.Location = new System.Drawing.Point(3, 162);
            this.lblPlotField.Name = "lblPlotField";
            this.lblPlotField.Size = new System.Drawing.Size(152, 30);
            this.lblPlotField.TabIndex = 5;
            this.lblPlotField.Text = "Plot number field";
            this.lblPlotField.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbSectorPlotFile
            // 
            this.tbSectorPlotFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSectorPlotFile.BackColor = System.Drawing.SystemColors.Control;
            this.tlp.SetColumnSpan(this.tbSectorPlotFile, 2);
            this.tbSectorPlotFile.Enabled = false;
            this.tbSectorPlotFile.Location = new System.Drawing.Point(161, 94);
            this.tbSectorPlotFile.Name = "tbSectorPlotFile";
            this.tbSectorPlotFile.ReadOnly = true;
            this.tbSectorPlotFile.Size = new System.Drawing.Size(310, 22);
            this.tbSectorPlotFile.TabIndex = 6;
            // 
            // cbSectorField
            // 
            this.cbSectorField.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlp.SetColumnSpan(this.cbSectorField, 3);
            this.cbSectorField.Enabled = false;
            this.cbSectorField.FormattingEnabled = true;
            this.cbSectorField.Location = new System.Drawing.Point(161, 135);
            this.cbSectorField.Name = "cbSectorField";
            this.cbSectorField.Size = new System.Drawing.Size(469, 24);
            this.cbSectorField.TabIndex = 7;
            // 
            // cbPlotField
            // 
            this.cbPlotField.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlp.SetColumnSpan(this.cbPlotField, 3);
            this.cbPlotField.Enabled = false;
            this.cbPlotField.FormattingEnabled = true;
            this.cbPlotField.Location = new System.Drawing.Point(161, 165);
            this.cbPlotField.Name = "cbPlotField";
            this.cbPlotField.Size = new System.Drawing.Size(469, 24);
            this.cbPlotField.TabIndex = 8;
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlp.SetColumnSpan(this.btnImport, 2);
            this.btnImport.Location = new System.Drawing.Point(319, 195);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(311, 39);
            this.btnImport.TabIndex = 10;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // lblSelectZoneFile
            // 
            this.lblSelectZoneFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelectZoneFile.AutoSize = true;
            this.lblSelectZoneFile.Location = new System.Drawing.Point(3, 20);
            this.lblSelectZoneFile.Name = "lblSelectZoneFile";
            this.lblSelectZoneFile.Size = new System.Drawing.Size(152, 41);
            this.lblSelectZoneFile.TabIndex = 12;
            this.lblSelectZoneFile.Text = "Select zone file";
            this.lblSelectZoneFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSelectZoneField
            // 
            this.lblSelectZoneField.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelectZoneField.AutoSize = true;
            this.lblSelectZoneField.Location = new System.Drawing.Point(3, 61);
            this.lblSelectZoneField.Name = "lblSelectZoneField";
            this.lblSelectZoneField.Size = new System.Drawing.Size(152, 30);
            this.lblSelectZoneField.TabIndex = 13;
            this.lblSelectZoneField.Text = "Select zone field";
            this.lblSelectZoneField.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbZoneFile
            // 
            this.tbZoneFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbZoneFile.BackColor = System.Drawing.SystemColors.Control;
            this.tlp.SetColumnSpan(this.tbZoneFile, 2);
            this.tbZoneFile.Enabled = false;
            this.tbZoneFile.Location = new System.Drawing.Point(161, 23);
            this.tbZoneFile.Name = "tbZoneFile";
            this.tbZoneFile.ReadOnly = true;
            this.tbZoneFile.Size = new System.Drawing.Size(310, 22);
            this.tbZoneFile.TabIndex = 14;
            // 
            // cbZoneField
            // 
            this.cbZoneField.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlp.SetColumnSpan(this.cbZoneField, 3);
            this.cbZoneField.Enabled = false;
            this.cbZoneField.FormattingEnabled = true;
            this.cbZoneField.Location = new System.Drawing.Point(161, 64);
            this.cbZoneField.Name = "cbZoneField";
            this.cbZoneField.Size = new System.Drawing.Size(469, 24);
            this.cbZoneField.TabIndex = 15;
            // 
            // btnSelectZoneFile
            // 
            this.btnSelectZoneFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectZoneFile.Location = new System.Drawing.Point(477, 23);
            this.btnSelectZoneFile.Name = "btnSelectZoneFile";
            this.btnSelectZoneFile.Size = new System.Drawing.Size(153, 35);
            this.btnSelectZoneFile.TabIndex = 16;
            this.btnSelectZoneFile.Text = "Browse...";
            this.btnSelectZoneFile.UseVisualStyleBackColor = true;
            this.btnSelectZoneFile.Click += new System.EventHandler(this.btnSelectZoneFile_Click);
            // 
            // btnSelectSectorPlotFile
            // 
            this.btnSelectSectorPlotFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectSectorPlotFile.Location = new System.Drawing.Point(477, 94);
            this.btnSelectSectorPlotFile.Name = "btnSelectSectorPlotFile";
            this.btnSelectSectorPlotFile.Size = new System.Drawing.Size(153, 35);
            this.btnSelectSectorPlotFile.TabIndex = 17;
            this.btnSelectSectorPlotFile.Text = "Browse...";
            this.btnSelectSectorPlotFile.UseVisualStyleBackColor = true;
            this.btnSelectSectorPlotFile.Click += new System.EventHandler(this.btnSelectSectorPlotFile_Click);
            // 
            // lblCurrentStatus
            // 
            this.lblCurrentStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrentStatus.AutoSize = true;
            this.lblCurrentStatus.Location = new System.Drawing.Point(3, 0);
            this.lblCurrentStatus.Name = "lblCurrentStatus";
            this.lblCurrentStatus.Size = new System.Drawing.Size(152, 20);
            this.lblCurrentStatus.TabIndex = 18;
            this.lblCurrentStatus.Text = "Current status";
            this.lblCurrentStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatusMessage
            // 
            this.lblStatusMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatusMessage.AutoSize = true;
            this.tlp.SetColumnSpan(this.lblStatusMessage, 3);
            this.lblStatusMessage.Location = new System.Drawing.Point(161, 0);
            this.lblStatusMessage.Name = "lblStatusMessage";
            this.lblStatusMessage.Size = new System.Drawing.Size(469, 20);
            this.lblStatusMessage.TabIndex = 19;
            this.lblStatusMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlp.SetColumnSpan(this.btnCancel, 2);
            this.btnCancel.Location = new System.Drawing.Point(3, 195);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(310, 39);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmPlotImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 237);
            this.Controls.Add(this.tlp);
            this.Name = "frmPlotImport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import zones, sectors, plots";
            this.Load += new System.EventHandler(this.frmImportZoneSectorPlot_Load);
            this.tlp.ResumeLayout(false);
            this.tlp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog dlgOpenShapefile;
        private System.Windows.Forms.TableLayoutPanel tlp;
        private System.Windows.Forms.Label lblSelectSourceFile;
        private System.Windows.Forms.Label lblSectorField;
        private System.Windows.Forms.Label lblPlotField;
        private System.Windows.Forms.TextBox tbSectorPlotFile;
        private System.Windows.Forms.ComboBox cbSectorField;
        private System.Windows.Forms.ComboBox cbPlotField;
        private System.Windows.Forms.Label lblSelectZoneFile;
        private System.Windows.Forms.Label lblSelectZoneField;
        private System.Windows.Forms.TextBox tbZoneFile;
        private System.Windows.Forms.ComboBox cbZoneField;
        private System.Windows.Forms.Button btnSelectZoneFile;
        private System.Windows.Forms.Button btnSelectSectorPlotFile;
        private System.Windows.Forms.Label lblCurrentStatus;
        private System.Windows.Forms.Label lblStatusMessage;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnCancel;
    }
}