namespace AsBuiltToGIS.Forms
{
    partial class frmLayout
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
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.theLayoutControl = new DotSpatial.Controls.LayoutControl();
            this.layoutDocToolStrip1 = new DotSpatial.Controls.LayoutDocToolStrip();
            this.layoutInsertToolStrip1 = new DotSpatial.Controls.LayoutInsertToolStrip();
            this.layoutMapToolStrip1 = new DotSpatial.Controls.LayoutMapToolStrip();
            this.layoutMenuStrip1 = new DotSpatial.Controls.LayoutMenuStrip();
            this.layoutPropertyGrid1 = new DotSpatial.Controls.LayoutPropertyGrid();
            this.layoutZoomToolStrip1 = new DotSpatial.Controls.LayoutZoomToolStrip();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.layoutListBox1 = new DotSpatial.Controls.LayoutListBox();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(726, 503);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(726, 577);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.layoutMenuStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.layoutDocToolStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.layoutZoomToolStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.layoutMapToolStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.layoutInsertToolStrip1);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.theLayoutControl);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(726, 503);
            this.splitContainer1.SplitterDistance = 506;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // theLayoutControl
            // 
            this.theLayoutControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.theLayoutControl.Cursor = System.Windows.Forms.Cursors.Default;
            this.theLayoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.theLayoutControl.DrawingQuality = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.theLayoutControl.Filename = "";
            this.theLayoutControl.LayoutDocToolStrip = this.layoutDocToolStrip1;
            this.theLayoutControl.LayoutInsertToolStrip = this.layoutInsertToolStrip1;
            this.theLayoutControl.LayoutListBox = this.layoutListBox1;
            this.theLayoutControl.LayoutMapToolStrip = this.layoutMapToolStrip1;
            this.theLayoutControl.LayoutMenuStrip = this.layoutMenuStrip1;
            this.theLayoutControl.LayoutPropertyGrip = this.layoutPropertyGrid1;
            this.theLayoutControl.LayoutZoomToolStrip = this.layoutZoomToolStrip1;
            this.theLayoutControl.Location = new System.Drawing.Point(0, 0);
            this.theLayoutControl.MapControl = null;
            this.theLayoutControl.MapPanMode = false;
            this.theLayoutControl.Name = "theLayoutControl";
            this.theLayoutControl.ShowMargin = false;
            this.theLayoutControl.Size = new System.Drawing.Size(506, 503);
            this.theLayoutControl.TabIndex = 0;
            this.theLayoutControl.Zoom = 0.320787F;
            // 
            // layoutDocToolStrip1
            // 
            this.layoutDocToolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.layoutDocToolStrip1.LayoutControl = this.theLayoutControl;
            this.layoutDocToolStrip1.Location = new System.Drawing.Point(3, 24);
            this.layoutDocToolStrip1.Name = "layoutDocToolStrip1";
            this.layoutDocToolStrip1.Size = new System.Drawing.Size(131, 25);
            this.layoutDocToolStrip1.TabIndex = 1;
            // 
            // layoutInsertToolStrip1
            // 
            this.layoutInsertToolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.layoutInsertToolStrip1.LayoutControl = this.theLayoutControl;
            this.layoutInsertToolStrip1.Location = new System.Drawing.Point(128, 49);
            this.layoutInsertToolStrip1.Name = "layoutInsertToolStrip1";
            this.layoutInsertToolStrip1.Size = new System.Drawing.Size(171, 25);
            this.layoutInsertToolStrip1.TabIndex = 2;
            // 
            // layoutMapToolStrip1
            // 
            this.layoutMapToolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.layoutMapToolStrip1.Enabled = false;
            this.layoutMapToolStrip1.LayoutControl = this.theLayoutControl;
            this.layoutMapToolStrip1.Location = new System.Drawing.Point(3, 49);
            this.layoutMapToolStrip1.Name = "layoutMapToolStrip1";
            this.layoutMapToolStrip1.Size = new System.Drawing.Size(125, 25);
            this.layoutMapToolStrip1.TabIndex = 3;
            // 
            // layoutMenuStrip1
            // 
            this.layoutMenuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.layoutMenuStrip1.LayoutControl = this.theLayoutControl;
            this.layoutMenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.layoutMenuStrip1.Name = "layoutMenuStrip1";
            this.layoutMenuStrip1.Size = new System.Drawing.Size(726, 24);
            this.layoutMenuStrip1.TabIndex = 4;
            this.layoutMenuStrip1.Text = "layoutMenuStrip1";
            this.layoutMenuStrip1.CloseClicked += new System.EventHandler(this.layoutMenuStrip1_CloseClicked);
            // 
            // layoutPropertyGrid1
            // 
            this.layoutPropertyGrid1.AutoSize = true;
            this.layoutPropertyGrid1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.layoutPropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPropertyGrid1.LayoutControl = this.theLayoutControl;
            this.layoutPropertyGrid1.Location = new System.Drawing.Point(10, 10);
            this.layoutPropertyGrid1.Margin = new System.Windows.Forms.Padding(0);
            this.layoutPropertyGrid1.Name = "layoutPropertyGrid1";
            this.layoutPropertyGrid1.Size = new System.Drawing.Size(195, 238);
            this.layoutPropertyGrid1.TabIndex = 0;
            // 
            // layoutZoomToolStrip1
            // 
            this.layoutZoomToolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.layoutZoomToolStrip1.LayoutControl = this.theLayoutControl;
            this.layoutZoomToolStrip1.Location = new System.Drawing.Point(134, 24);
            this.layoutZoomToolStrip1.Name = "layoutZoomToolStrip1";
            this.layoutZoomToolStrip1.Size = new System.Drawing.Size(156, 25);
            this.layoutZoomToolStrip1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.layoutListBox1);
            this.splitContainer2.Panel1.Padding = new System.Windows.Forms.Padding(10);
            this.splitContainer2.Panel1MinSize = 100;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.layoutPropertyGrid1);
            this.splitContainer2.Panel2.Padding = new System.Windows.Forms.Padding(10);
            this.splitContainer2.Panel2MinSize = 100;
            this.splitContainer2.Size = new System.Drawing.Size(215, 503);
            this.splitContainer2.SplitterDistance = 240;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 1;
            // 
            // layoutListBox1
            // 
            this.layoutListBox1.AutoSize = true;
            this.layoutListBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.layoutListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutListBox1.LayoutControl = this.theLayoutControl;
            this.layoutListBox1.Location = new System.Drawing.Point(10, 10);
            this.layoutListBox1.Name = "layoutListBox1";
            this.layoutListBox1.Size = new System.Drawing.Size(195, 220);
            this.layoutListBox1.TabIndex = 0;
            // 
            // frmLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 577);
            this.Controls.Add(this.toolStripContainer1);
            this.MainMenuStrip = this.layoutMenuStrip1;
            this.Name = "frmLayout";
            this.Text = "Print and Export As-Built Data";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmLayout_Load);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private DotSpatial.Controls.LayoutZoomToolStrip layoutZoomToolStrip1;
        private DotSpatial.Controls.LayoutDocToolStrip layoutDocToolStrip1;
        private DotSpatial.Controls.LayoutInsertToolStrip layoutInsertToolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DotSpatial.Controls.LayoutPropertyGrid layoutPropertyGrid1;
        private DotSpatial.Controls.LayoutMenuStrip layoutMenuStrip1;
        private DotSpatial.Controls.LayoutMapToolStrip layoutMapToolStrip1;
        public DotSpatial.Controls.LayoutControl theLayoutControl;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private DotSpatial.Controls.LayoutListBox layoutListBox1;
    }
}