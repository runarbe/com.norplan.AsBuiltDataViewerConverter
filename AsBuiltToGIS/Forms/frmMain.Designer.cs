namespace AsBuiltToGIS
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.dlgSelectDb = new System.Windows.Forms.OpenFileDialog();
            this.theMapToolbar = new System.Windows.Forms.ToolStrip();
            this.btnIdentify = new System.Windows.Forms.ToolStripButton();
            this.toolPan = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolZoomIn = new System.Windows.Forms.ToolStripButton();
            this.toolZoomOut = new System.Windows.Forms.ToolStripButton();
            this.btnZoomFullExtent = new System.Windows.Forms.ToolStripButton();
            this.btnOpenFile = new System.Windows.Forms.ToolStripButton();
            this.btnPrintLayout = new System.Windows.Forms.ToolStripButton();
            this.btnToggleSatelliteImagery = new System.Windows.Forms.ToolStripButton();
            this.theMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.getRandomSelectionOfSignsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateQRcodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importRoadsAndRoadCenterLinesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPlotIDsToFGDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cleanFileGDBStreetAndDistrictNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportFileGDBToMyabudhabinetSQLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importAddressingDistrictsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.identifyRoadDefinitionSuspectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testQRCodesOfSelectedLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addGoogleSatelliteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeBINGSatelliteImageryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportSelectedPointLayerToSpatialiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportSelectedAddressUnitLayerToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportSelectedPointLayerAsGPXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exportSelectedLayerAsFileGDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAddressUnitsToGeopaparazziProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAddressUnitsToSQLITEDbToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportForMyabudhabinetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportSelectedDatabasesToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sQLforMyabudhabinetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileGDBforADMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandWordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.theStatusStrip = new System.Windows.Forms.StatusStrip();
            this.lblProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.pgBar = new System.Windows.Forms.ToolStripProgressBar();
            this.lblInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.theDataGridView = new System.Windows.Forms.DataGridView();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.dlgOpenShapefile = new System.Windows.Forms.OpenFileDialog();
            this.MainContainer = new System.Windows.Forms.ToolStripContainer();
            this.MapTableSplitter = new System.Windows.Forms.SplitContainer();
            this.LegendMapLogSplitter = new System.Windows.Forms.SplitContainer();
            this.theLegend = new DotSpatial.Controls.Legend();
            this.MapLogSubSplitter = new System.Windows.Forms.SplitContainer();
            this.theMap = new DotSpatial.Controls.Map();
            this.TableToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.TableToolStrip = new System.Windows.Forms.ToolStrip();
            this.lblSortField1 = new System.Windows.Forms.ToolStripLabel();
            this.cbSortField1 = new System.Windows.Forms.ToolStripComboBox();
            this.lblSortField2 = new System.Windows.Forms.ToolStripLabel();
            this.cbSortField2 = new System.Windows.Forms.ToolStripComboBox();
            this.TheDatabaseToolbar = new System.Windows.Forms.ToolStrip();
            this.lblDb = new System.Windows.Forms.ToolStripLabel();
            this.tbSelDbFn = new System.Windows.Forms.ToolStripTextBox();
            this.btnSelectDb = new System.Windows.Forms.ToolStripButton();
            this.theAppManager = new DotSpatial.Controls.AppManager();
            this.dlgOpenMdbFile = new System.Windows.Forms.OpenFileDialog();
            this.dlgSelectDBs = new System.Windows.Forms.OpenFileDialog();
            this.dlgSelectFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.theMapToolbar.SuspendLayout();
            this.theMenuStrip.SuspendLayout();
            this.theStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.theDataGridView)).BeginInit();
            this.MainContainer.BottomToolStripPanel.SuspendLayout();
            this.MainContainer.ContentPanel.SuspendLayout();
            this.MainContainer.TopToolStripPanel.SuspendLayout();
            this.MainContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MapTableSplitter)).BeginInit();
            this.MapTableSplitter.Panel1.SuspendLayout();
            this.MapTableSplitter.Panel2.SuspendLayout();
            this.MapTableSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LegendMapLogSplitter)).BeginInit();
            this.LegendMapLogSplitter.Panel1.SuspendLayout();
            this.LegendMapLogSplitter.Panel2.SuspendLayout();
            this.LegendMapLogSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MapLogSubSplitter)).BeginInit();
            this.MapLogSubSplitter.Panel1.SuspendLayout();
            this.MapLogSubSplitter.Panel2.SuspendLayout();
            this.MapLogSubSplitter.SuspendLayout();
            this.TableToolStripContainer.ContentPanel.SuspendLayout();
            this.TableToolStripContainer.TopToolStripPanel.SuspendLayout();
            this.TableToolStripContainer.SuspendLayout();
            this.TableToolStrip.SuspendLayout();
            this.TheDatabaseToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // dlgSelectDb
            // 
            this.dlgSelectDb.FileName = "openFileDialog1";
            this.dlgSelectDb.Filter = "Access database|*.accdb";
            // 
            // theMapToolbar
            // 
            this.theMapToolbar.Dock = System.Windows.Forms.DockStyle.None;
            this.theMapToolbar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.theMapToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnIdentify,
            this.toolPan,
            this.toolStripButton1,
            this.toolZoomIn,
            this.toolZoomOut,
            this.btnZoomFullExtent,
            this.btnOpenFile,
            this.btnPrintLayout,
            this.btnToggleSatelliteImagery});
            this.theMapToolbar.Location = new System.Drawing.Point(3, 24);
            this.theMapToolbar.Margin = new System.Windows.Forms.Padding(3);
            this.theMapToolbar.Name = "theMapToolbar";
            this.theMapToolbar.Padding = new System.Windows.Forms.Padding(2);
            this.theMapToolbar.Size = new System.Drawing.Size(267, 35);
            this.theMapToolbar.TabIndex = 9;
            this.theMapToolbar.Text = "toolBar";
            this.theMapToolbar.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolBar_ItemClicked);
            // 
            // btnIdentify
            // 
            this.btnIdentify.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnIdentify.Image = ((System.Drawing.Image)(resources.GetObject("btnIdentify.Image")));
            this.btnIdentify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIdentify.Name = "btnIdentify";
            this.btnIdentify.Size = new System.Drawing.Size(28, 28);
            this.btnIdentify.Text = "Information";
            this.btnIdentify.Click += new System.EventHandler(this.toolBtnIdentify_Click);
            // 
            // toolPan
            // 
            this.toolPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolPan.Image = ((System.Drawing.Image)(resources.GetObject("toolPan.Image")));
            this.toolPan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolPan.Name = "toolPan";
            this.toolPan.Size = new System.Drawing.Size(28, 28);
            this.toolPan.Text = "Pan";
            this.toolPan.Click += new System.EventHandler(this.toolBtnPan_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(28, 28);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolZoomIn
            // 
            this.toolZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("toolZoomIn.Image")));
            this.toolZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolZoomIn.Name = "toolZoomIn";
            this.toolZoomIn.Size = new System.Drawing.Size(28, 28);
            this.toolZoomIn.Text = "Zoom in";
            this.toolZoomIn.Click += new System.EventHandler(this.toolBtnZoomIn_Click);
            // 
            // toolZoomOut
            // 
            this.toolZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("toolZoomOut.Image")));
            this.toolZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolZoomOut.Name = "toolZoomOut";
            this.toolZoomOut.Size = new System.Drawing.Size(28, 28);
            this.toolZoomOut.Text = "Zoom out";
            this.toolZoomOut.Click += new System.EventHandler(this.toolBtnZoomOut_Click);
            // 
            // btnZoomFullExtent
            // 
            this.btnZoomFullExtent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomFullExtent.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomFullExtent.Image")));
            this.btnZoomFullExtent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomFullExtent.Name = "btnZoomFullExtent";
            this.btnZoomFullExtent.Size = new System.Drawing.Size(28, 28);
            this.btnZoomFullExtent.Text = "Zoom to full extent";
            this.btnZoomFullExtent.Click += new System.EventHandler(this.toolBtnZoomToMaxExtent_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenFile.Image")));
            this.btnOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(28, 28);
            this.btnOpenFile.Text = "Add shapefile to map";
            this.btnOpenFile.Click += new System.EventHandler(this.toolBtnAddLayer_Click);
            // 
            // btnPrintLayout
            // 
            this.btnPrintLayout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrintLayout.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintLayout.Image")));
            this.btnPrintLayout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrintLayout.Name = "btnPrintLayout";
            this.btnPrintLayout.Size = new System.Drawing.Size(28, 28);
            this.btnPrintLayout.Text = "Open layout for printing";
            this.btnPrintLayout.Click += new System.EventHandler(this.btnPrintLayout_Click);
            // 
            // btnToggleSatelliteImagery
            // 
            this.btnToggleSatelliteImagery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnToggleSatelliteImagery.Image = ((System.Drawing.Image)(resources.GetObject("btnToggleSatelliteImagery.Image")));
            this.btnToggleSatelliteImagery.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnToggleSatelliteImagery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnToggleSatelliteImagery.Name = "btnToggleSatelliteImagery";
            this.btnToggleSatelliteImagery.Size = new System.Drawing.Size(28, 28);
            this.btnToggleSatelliteImagery.Text = "Satellite Imagery";
            this.btnToggleSatelliteImagery.Click += new System.EventHandler(this.btnToggleSatelliteImagery_Click);
            // 
            // theMenuStrip
            // 
            this.theMenuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.theMenuStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.theMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.toolsToolStripMenuItem,
            this.mapToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.theMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.theMenuStrip.Name = "theMenuStrip";
            this.theMenuStrip.Size = new System.Drawing.Size(704, 24);
            this.theMenuStrip.TabIndex = 10;
            this.theMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem1.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.getRandomSelectionOfSignsToolStripMenuItem,
            this.generateQRcodesToolStripMenuItem,
            this.importRoadsAndRoadCenterLinesToolStripMenuItem,
            this.addPlotIDsToFGDBToolStripMenuItem,
            this.cleanFileGDBStreetAndDistrictNamesToolStripMenuItem,
            this.exportFileGDBToMyabudhabinetSQLToolStripMenuItem,
            this.importAddressingDistrictsToolStripMenuItem,
            this.identifyRoadDefinitionSuspectsToolStripMenuItem,
            this.testQRCodesOfSelectedLayerToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(307, 6);
            // 
            // getRandomSelectionOfSignsToolStripMenuItem
            // 
            this.getRandomSelectionOfSignsToolStripMenuItem.Name = "getRandomSelectionOfSignsToolStripMenuItem";
            this.getRandomSelectionOfSignsToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.getRandomSelectionOfSignsToolStripMenuItem.Text = "Generate random sample from selected layer";
            this.getRandomSelectionOfSignsToolStripMenuItem.Click += new System.EventHandler(this.getRandomSelectionOfSignsToolStripMenuItem_Click);
            // 
            // generateQRcodesToolStripMenuItem
            // 
            this.generateQRcodesToolStripMenuItem.Name = "generateQRcodesToolStripMenuItem";
            this.generateQRcodesToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.generateQRcodesToolStripMenuItem.Text = "Generate QR-codes";
            this.generateQRcodesToolStripMenuItem.Click += new System.EventHandler(this.generateQRcodesToolStripMenuItem_Click);
            // 
            // importRoadsAndRoadCenterLinesToolStripMenuItem
            // 
            this.importRoadsAndRoadCenterLinesToolStripMenuItem.Name = "importRoadsAndRoadCenterLinesToolStripMenuItem";
            this.importRoadsAndRoadCenterLinesToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.importRoadsAndRoadCenterLinesToolStripMenuItem.Text = "Import roads and road center lines";
            this.importRoadsAndRoadCenterLinesToolStripMenuItem.Click += new System.EventHandler(this.importRoadsAndRoadCenterLinesToolStripMenuItem_Click);
            // 
            // addPlotIDsToFGDBToolStripMenuItem
            // 
            this.addPlotIDsToFGDBToolStripMenuItem.Name = "addPlotIDsToFGDBToolStripMenuItem";
            this.addPlotIDsToFGDBToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.addPlotIDsToFGDBToolStripMenuItem.Text = "Add zone, sector, plot to FileGDB";
            this.addPlotIDsToFGDBToolStripMenuItem.Click += new System.EventHandler(this.addPlotIDsToFGDBToolStripMenuItem_Click);
            // 
            // cleanFileGDBStreetAndDistrictNamesToolStripMenuItem
            // 
            this.cleanFileGDBStreetAndDistrictNamesToolStripMenuItem.Name = "cleanFileGDBStreetAndDistrictNamesToolStripMenuItem";
            this.cleanFileGDBStreetAndDistrictNamesToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.cleanFileGDBStreetAndDistrictNamesToolStripMenuItem.Text = "Clean FileGDB street and district names ";
            this.cleanFileGDBStreetAndDistrictNamesToolStripMenuItem.Click += new System.EventHandler(this.cleanFileGDBStreetAndDistrictNamesToolStripMenuItem_Click);
            // 
            // exportFileGDBToMyabudhabinetSQLToolStripMenuItem
            // 
            this.exportFileGDBToMyabudhabinetSQLToolStripMenuItem.Name = "exportFileGDBToMyabudhabinetSQLToolStripMenuItem";
            this.exportFileGDBToMyabudhabinetSQLToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.exportFileGDBToMyabudhabinetSQLToolStripMenuItem.Text = "Export FileGDB to myabudhabi.net SQL";
            this.exportFileGDBToMyabudhabinetSQLToolStripMenuItem.Click += new System.EventHandler(this.exportFileGDBToMyabudhabinetSQLToolStripMenuItem_Click);
            // 
            // importAddressingDistrictsToolStripMenuItem
            // 
            this.importAddressingDistrictsToolStripMenuItem.Name = "importAddressingDistrictsToolStripMenuItem";
            this.importAddressingDistrictsToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.importAddressingDistrictsToolStripMenuItem.Text = "Import addressing districts";
            this.importAddressingDistrictsToolStripMenuItem.Click += new System.EventHandler(this.importAddressingDistrictsToolStripMenuItem_Click);
            // 
            // identifyRoadDefinitionSuspectsToolStripMenuItem
            // 
            this.identifyRoadDefinitionSuspectsToolStripMenuItem.Name = "identifyRoadDefinitionSuspectsToolStripMenuItem";
            this.identifyRoadDefinitionSuspectsToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.identifyRoadDefinitionSuspectsToolStripMenuItem.Text = "Identify road definition suspects";
            this.identifyRoadDefinitionSuspectsToolStripMenuItem.Click += new System.EventHandler(this.identifyRoadDefinitionSuspectsToolStripMenuItem_Click);
            // 
            // testQRCodesOfSelectedLayerToolStripMenuItem
            // 
            this.testQRCodesOfSelectedLayerToolStripMenuItem.Name = "testQRCodesOfSelectedLayerToolStripMenuItem";
            this.testQRCodesOfSelectedLayerToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
            this.testQRCodesOfSelectedLayerToolStripMenuItem.Text = "Test QR codes of selected layer";
            this.testQRCodesOfSelectedLayerToolStripMenuItem.Click += new System.EventHandler(this.testQRCodesOfSelectedLayerToolStripMenuItem_Click);
            // 
            // mapToolStripMenuItem
            // 
            this.mapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addGoogleSatelliteToolStripMenuItem,
            this.removeBINGSatelliteImageryToolStripMenuItem});
            this.mapToolStripMenuItem.Name = "mapToolStripMenuItem";
            this.mapToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.mapToolStripMenuItem.Text = "Map";
            // 
            // addGoogleSatelliteToolStripMenuItem
            // 
            this.addGoogleSatelliteToolStripMenuItem.Name = "addGoogleSatelliteToolStripMenuItem";
            this.addGoogleSatelliteToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.addGoogleSatelliteToolStripMenuItem.Text = "Add BING satellite imagery";
            this.addGoogleSatelliteToolStripMenuItem.Click += new System.EventHandler(this.addGoogleSatelliteToolStripMenuItem_Click);
            // 
            // removeBINGSatelliteImageryToolStripMenuItem
            // 
            this.removeBINGSatelliteImageryToolStripMenuItem.Name = "removeBINGSatelliteImageryToolStripMenuItem";
            this.removeBINGSatelliteImageryToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.removeBINGSatelliteImageryToolStripMenuItem.Text = "Remove BING satellite imagery";
            this.removeBINGSatelliteImageryToolStripMenuItem.Click += new System.EventHandler(this.removeBINGSatelliteImageryToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportSelectedPointLayerToSpatialiteToolStripMenuItem,
            this.exportSelectedAddressUnitLayerToToolStripMenuItem,
            this.exportSelectedDatabasesToToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // exportSelectedPointLayerToSpatialiteToolStripMenuItem
            // 
            this.exportSelectedPointLayerToSpatialiteToolStripMenuItem.Name = "exportSelectedPointLayerToSpatialiteToolStripMenuItem";
            this.exportSelectedPointLayerToSpatialiteToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.exportSelectedPointLayerToSpatialiteToolStripMenuItem.Text = "Export selected layer to Spatialite";
            this.exportSelectedPointLayerToSpatialiteToolStripMenuItem.Click += new System.EventHandler(this.exportSelectedFeatureLayerToSpatialiteToolStripMenuItem_Click);
            // 
            // exportSelectedAddressUnitLayerToToolStripMenuItem
            // 
            this.exportSelectedAddressUnitLayerToToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportSelectedPointLayerAsGPXToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exportSelectedLayerAsFileGDBToolStripMenuItem,
            this.exportAddressUnitsToGeopaparazziProjectToolStripMenuItem,
            this.exportAddressUnitsToSQLITEDbToolStripMenuItem,
            this.exportForMyabudhabinetToolStripMenuItem});
            this.exportSelectedAddressUnitLayerToToolStripMenuItem.Name = "exportSelectedAddressUnitLayerToToolStripMenuItem";
            this.exportSelectedAddressUnitLayerToToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.exportSelectedAddressUnitLayerToToolStripMenuItem.Text = "Export selected address unit layer to...";
            // 
            // exportSelectedPointLayerAsGPXToolStripMenuItem
            // 
            this.exportSelectedPointLayerAsGPXToolStripMenuItem.Name = "exportSelectedPointLayerAsGPXToolStripMenuItem";
            this.exportSelectedPointLayerAsGPXToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.exportSelectedPointLayerAsGPXToolStripMenuItem.Text = "...GPX";
            this.exportSelectedPointLayerAsGPXToolStripMenuItem.Click += new System.EventHandler(this.exportSelectedPointLayerAsGPXToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(220, 22);
            this.toolStripMenuItem1.Text = "...KML (for Google Earth)";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.exportSelectedLayerAsKMLToolStripMenuItem_Click);
            // 
            // exportSelectedLayerAsFileGDBToolStripMenuItem
            // 
            this.exportSelectedLayerAsFileGDBToolStripMenuItem.Name = "exportSelectedLayerAsFileGDBToolStripMenuItem";
            this.exportSelectedLayerAsFileGDBToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.exportSelectedLayerAsFileGDBToolStripMenuItem.Text = "...FileGDB (for ADM)";
            this.exportSelectedLayerAsFileGDBToolStripMenuItem.Click += new System.EventHandler(this.exportSelectedLayerAsFileGDBToolStripMenuItem_Click);
            // 
            // exportAddressUnitsToGeopaparazziProjectToolStripMenuItem
            // 
            this.exportAddressUnitsToGeopaparazziProjectToolStripMenuItem.Name = "exportAddressUnitsToGeopaparazziProjectToolStripMenuItem";
            this.exportAddressUnitsToGeopaparazziProjectToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.exportAddressUnitsToGeopaparazziProjectToolStripMenuItem.Text = "...Geopaparazzi project";
            this.exportAddressUnitsToGeopaparazziProjectToolStripMenuItem.Click += new System.EventHandler(this.exportAddressUnitsToGeopaparazziProjectToolStripMenuItem_Click);
            // 
            // exportAddressUnitsToSQLITEDbToolStripMenuItem
            // 
            this.exportAddressUnitsToSQLITEDbToolStripMenuItem.Name = "exportAddressUnitsToSQLITEDbToolStripMenuItem";
            this.exportAddressUnitsToSQLITEDbToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.exportAddressUnitsToSQLITEDbToolStripMenuItem.Text = "...SQLITE db";
            this.exportAddressUnitsToSQLITEDbToolStripMenuItem.Click += new System.EventHandler(this.exportAddressUnitsToSQLITEDbToolStripMenuItem_Click);
            // 
            // exportForMyabudhabinetToolStripMenuItem
            // 
            this.exportForMyabudhabinetToolStripMenuItem.Name = "exportForMyabudhabinetToolStripMenuItem";
            this.exportForMyabudhabinetToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.exportForMyabudhabinetToolStripMenuItem.Text = "...SQL (for myabudhabi.net)";
            this.exportForMyabudhabinetToolStripMenuItem.Click += new System.EventHandler(this.exportForMyabudhabinetToolStripMenuItem_Click);
            // 
            // exportSelectedDatabasesToToolStripMenuItem
            // 
            this.exportSelectedDatabasesToToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sQLforMyabudhabinetToolStripMenuItem,
            this.fileGDBforADMToolStripMenuItem});
            this.exportSelectedDatabasesToToolStripMenuItem.Name = "exportSelectedDatabasesToToolStripMenuItem";
            this.exportSelectedDatabasesToToolStripMenuItem.Size = new System.Drawing.Size(271, 22);
            this.exportSelectedDatabasesToToolStripMenuItem.Text = "Export selected databases to...";
            // 
            // sQLforMyabudhabinetToolStripMenuItem
            // 
            this.sQLforMyabudhabinetToolStripMenuItem.Name = "sQLforMyabudhabinetToolStripMenuItem";
            this.sQLforMyabudhabinetToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.sQLforMyabudhabinetToolStripMenuItem.Text = "...SQL (for myabudhabi.net)";
            this.sQLforMyabudhabinetToolStripMenuItem.Click += new System.EventHandler(this.sQLforMyabudhabinetToolStripMenuItem_Click);
            // 
            // fileGDBforADMToolStripMenuItem
            // 
            this.fileGDBforADMToolStripMenuItem.Name = "fileGDBforADMToolStripMenuItem";
            this.fileGDBforADMToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.fileGDBforADMToolStripMenuItem.Text = "...FileGDB (for ADM)";
            this.fileGDBforADMToolStripMenuItem.Click += new System.EventHandler(this.fileGDBforADMToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator5,
            this.aboutToolStripMenuItem,
            this.expandWordToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(139, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // expandWordToolStripMenuItem
            // 
            this.expandWordToolStripMenuItem.Name = "expandWordToolStripMenuItem";
            this.expandWordToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.expandWordToolStripMenuItem.Text = "Expand word";
            // 
            // theStatusStrip
            // 
            this.theStatusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.theStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblProgress,
            this.pgBar,
            this.lblInfo,
            this.lblStatus});
            this.theStatusStrip.Location = new System.Drawing.Point(0, 0);
            this.theStatusStrip.Name = "theStatusStrip";
            this.theStatusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.theStatusStrip.Size = new System.Drawing.Size(704, 22);
            this.theStatusStrip.TabIndex = 11;
            this.theStatusStrip.Text = "statusStrip1";
            // 
            // lblProgress
            // 
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(52, 17);
            this.lblProgress.Text = "Progress";
            // 
            // pgBar
            // 
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(200, 16);
            // 
            // lblInfo
            // 
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(31, 17);
            this.lblInfo.Text = "Info:";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(39, 17);
            this.lblStatus.Text = "Ready";
            // 
            // theDataGridView
            // 
            this.theDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.theDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.theDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.theDataGridView.Location = new System.Drawing.Point(0, 0);
            this.theDataGridView.Name = "theDataGridView";
            this.theDataGridView.Size = new System.Drawing.Size(704, 88);
            this.theDataGridView.TabIndex = 1;
            // 
            // tbLog
            // 
            this.tbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLog.Location = new System.Drawing.Point(0, 0);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLog.Size = new System.Drawing.Size(154, 256);
            this.tbLog.TabIndex = 0;
            // 
            // dlgOpenShapefile
            // 
            this.dlgOpenShapefile.FileName = "dlgOpenShapefile";
            // 
            // MainContainer
            // 
            // 
            // MainContainer.BottomToolStripPanel
            // 
            this.MainContainer.BottomToolStripPanel.Controls.Add(this.theStatusStrip);
            // 
            // MainContainer.ContentPanel
            // 
            this.MainContainer.ContentPanel.Controls.Add(this.MapTableSplitter);
            this.MainContainer.ContentPanel.Size = new System.Drawing.Size(704, 373);
            this.MainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainContainer.LeftToolStripPanelVisible = false;
            this.MainContainer.Location = new System.Drawing.Point(0, 0);
            this.MainContainer.Name = "MainContainer";
            this.MainContainer.RightToolStripPanelVisible = false;
            this.MainContainer.Size = new System.Drawing.Size(704, 479);
            this.MainContainer.TabIndex = 16;
            this.MainContainer.Text = "toolStripContainer1";
            // 
            // MainContainer.TopToolStripPanel
            // 
            this.MainContainer.TopToolStripPanel.Controls.Add(this.theMenuStrip);
            this.MainContainer.TopToolStripPanel.Controls.Add(this.theMapToolbar);
            this.MainContainer.TopToolStripPanel.Controls.Add(this.TheDatabaseToolbar);
            // 
            // MapTableSplitter
            // 
            this.MapTableSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MapTableSplitter.Location = new System.Drawing.Point(0, 0);
            this.MapTableSplitter.Name = "MapTableSplitter";
            this.MapTableSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // MapTableSplitter.Panel1
            // 
            this.MapTableSplitter.Panel1.Controls.Add(this.LegendMapLogSplitter);
            // 
            // MapTableSplitter.Panel2
            // 
            this.MapTableSplitter.Panel2.Controls.Add(this.TableToolStripContainer);
            this.MapTableSplitter.Size = new System.Drawing.Size(704, 373);
            this.MapTableSplitter.SplitterDistance = 256;
            this.MapTableSplitter.TabIndex = 17;
            // 
            // LegendMapLogSplitter
            // 
            this.LegendMapLogSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LegendMapLogSplitter.Location = new System.Drawing.Point(0, 0);
            this.LegendMapLogSplitter.Name = "LegendMapLogSplitter";
            // 
            // LegendMapLogSplitter.Panel1
            // 
            this.LegendMapLogSplitter.Panel1.Controls.Add(this.theLegend);
            // 
            // LegendMapLogSplitter.Panel2
            // 
            this.LegendMapLogSplitter.Panel2.Controls.Add(this.MapLogSubSplitter);
            this.LegendMapLogSplitter.Size = new System.Drawing.Size(704, 256);
            this.LegendMapLogSplitter.SplitterDistance = 173;
            this.LegendMapLogSplitter.SplitterWidth = 5;
            this.LegendMapLogSplitter.TabIndex = 16;
            // 
            // theLegend
            // 
            this.theLegend.BackColor = System.Drawing.Color.White;
            this.theLegend.ControlRectangle = new System.Drawing.Rectangle(0, 0, 173, 256);
            this.theLegend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.theLegend.DocumentRectangle = new System.Drawing.Rectangle(0, 0, 187, 428);
            this.theLegend.HorizontalScrollEnabled = true;
            this.theLegend.Indentation = 30;
            this.theLegend.IsInitialized = false;
            this.theLegend.Location = new System.Drawing.Point(0, 0);
            this.theLegend.MinimumSize = new System.Drawing.Size(5, 5);
            this.theLegend.Name = "theLegend";
            this.theLegend.ProgressHandler = null;
            this.theLegend.ResetOnResize = true;
            this.theLegend.SelectionFontColor = System.Drawing.Color.Black;
            this.theLegend.SelectionHighlight = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(238)))), ((int)(((byte)(252)))));
            this.theLegend.Size = new System.Drawing.Size(173, 256);
            this.theLegend.TabIndex = 14;
            this.theLegend.Text = "Layers";
            this.theLegend.VerticalScrollEnabled = true;
            // 
            // MapLogSubSplitter
            // 
            this.MapLogSubSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MapLogSubSplitter.Location = new System.Drawing.Point(0, 0);
            this.MapLogSubSplitter.Name = "MapLogSubSplitter";
            // 
            // MapLogSubSplitter.Panel1
            // 
            this.MapLogSubSplitter.Panel1.Controls.Add(this.theMap);
            // 
            // MapLogSubSplitter.Panel2
            // 
            this.MapLogSubSplitter.Panel2.Controls.Add(this.tbLog);
            this.MapLogSubSplitter.Panel2MinSize = 100;
            this.MapLogSubSplitter.Size = new System.Drawing.Size(526, 256);
            this.MapLogSubSplitter.SplitterDistance = 367;
            this.MapLogSubSplitter.SplitterWidth = 5;
            this.MapLogSubSplitter.TabIndex = 3;
            // 
            // theMap
            // 
            this.theMap.AllowDrop = true;
            this.theMap.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.theMap.BackColor = System.Drawing.Color.White;
            this.theMap.CollectAfterDraw = false;
            this.theMap.CollisionDetection = false;
            this.theMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.theMap.ExtendBuffer = false;
            this.theMap.FunctionMode = DotSpatial.Controls.FunctionMode.Pan;
            this.theMap.IsBusy = false;
            this.theMap.IsZoomedToMaxExtent = true;
            this.theMap.Legend = this.theLegend;
            this.theMap.Location = new System.Drawing.Point(0, 0);
            this.theMap.Name = "theMap";
            this.theMap.ProgressHandler = null;
            this.theMap.ProjectionModeDefine = DotSpatial.Controls.ActionMode.Never;
            this.theMap.ProjectionModeReproject = DotSpatial.Controls.ActionMode.Never;
            this.theMap.RedrawLayersWhileResizing = false;
            this.theMap.SelectionEnabled = true;
            this.theMap.Size = new System.Drawing.Size(367, 256);
            this.theMap.TabIndex = 2;
            // 
            // TableToolStripContainer
            // 
            this.TableToolStripContainer.BottomToolStripPanelVisible = false;
            // 
            // TableToolStripContainer.ContentPanel
            // 
            this.TableToolStripContainer.ContentPanel.Controls.Add(this.theDataGridView);
            this.TableToolStripContainer.ContentPanel.Size = new System.Drawing.Size(704, 88);
            this.TableToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableToolStripContainer.LeftToolStripPanelVisible = false;
            this.TableToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.TableToolStripContainer.Name = "TableToolStripContainer";
            this.TableToolStripContainer.RightToolStripPanelVisible = false;
            this.TableToolStripContainer.Size = new System.Drawing.Size(704, 113);
            this.TableToolStripContainer.TabIndex = 2;
            this.TableToolStripContainer.Text = "toolStripContainer1";
            // 
            // TableToolStripContainer.TopToolStripPanel
            // 
            this.TableToolStripContainer.TopToolStripPanel.Controls.Add(this.TableToolStrip);
            // 
            // TableToolStrip
            // 
            this.TableToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.TableToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblSortField1,
            this.cbSortField1,
            this.lblSortField2,
            this.cbSortField2});
            this.TableToolStrip.Location = new System.Drawing.Point(3, 0);
            this.TableToolStrip.Name = "TableToolStrip";
            this.TableToolStrip.Size = new System.Drawing.Size(384, 25);
            this.TableToolStrip.TabIndex = 2;
            this.TableToolStrip.Text = "toolStrip1";
            // 
            // lblSortField1
            // 
            this.lblSortField1.Name = "lblSortField1";
            this.lblSortField1.Size = new System.Drawing.Size(63, 22);
            this.lblSortField1.Text = "Sort field 1";
            // 
            // cbSortField1
            // 
            this.cbSortField1.Name = "cbSortField1";
            this.cbSortField1.Size = new System.Drawing.Size(121, 25);
            this.cbSortField1.SelectedIndexChanged += new System.EventHandler(this.onUpdateSort1);
            // 
            // lblSortField2
            // 
            this.lblSortField2.Name = "lblSortField2";
            this.lblSortField2.Size = new System.Drawing.Size(63, 22);
            this.lblSortField2.Text = "Sort field 2";
            // 
            // cbSortField2
            // 
            this.cbSortField2.Name = "cbSortField2";
            this.cbSortField2.Size = new System.Drawing.Size(121, 25);
            this.cbSortField2.SelectedIndexChanged += new System.EventHandler(this.onUpdateSort2);
            // 
            // TheDatabaseToolbar
            // 
            this.TheDatabaseToolbar.Dock = System.Windows.Forms.DockStyle.None;
            this.TheDatabaseToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblDb,
            this.tbSelDbFn,
            this.btnSelectDb});
            this.TheDatabaseToolbar.Location = new System.Drawing.Point(3, 59);
            this.TheDatabaseToolbar.Name = "TheDatabaseToolbar";
            this.TheDatabaseToolbar.Size = new System.Drawing.Size(374, 25);
            this.TheDatabaseToolbar.TabIndex = 11;
            // 
            // lblDb
            // 
            this.lblDb.Name = "lblDb";
            this.lblDb.Size = new System.Drawing.Size(102, 22);
            this.lblDb.Text = "Selected Database";
            // 
            // tbSelDbFn
            // 
            this.tbSelDbFn.Enabled = false;
            this.tbSelDbFn.Name = "tbSelDbFn";
            this.tbSelDbFn.Size = new System.Drawing.Size(200, 25);
            // 
            // btnSelectDb
            // 
            this.btnSelectDb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSelectDb.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectDb.Image")));
            this.btnSelectDb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelectDb.Name = "btnSelectDb";
            this.btnSelectDb.Size = new System.Drawing.Size(58, 22);
            this.btnSelectDb.Text = "Browse...";
            this.btnSelectDb.Click += new System.EventHandler(this.btnSelectDb_Click);
            // 
            // theAppManager
            // 
            this.theAppManager.Directories = ((System.Collections.Generic.List<string>)(resources.GetObject("theAppManager.Directories")));
            this.theAppManager.DockManager = null;
            this.theAppManager.HeaderControl = null;
            this.theAppManager.Legend = this.theLegend;
            this.theAppManager.Map = this.theMap;
            this.theAppManager.ProgressHandler = null;
            this.theAppManager.ShowExtensionsDialogMode = DotSpatial.Controls.ShowExtensionsDialogMode.Default;
            // 
            // dlgOpenMdbFile
            // 
            this.dlgOpenMdbFile.FileName = "openFileDialog1";
            // 
            // dlgSelectDBs
            // 
            this.dlgSelectDBs.FileName = "openFileDialog1";
            this.dlgSelectDBs.Filter = "Access databases|*.accdb";
            this.dlgSelectDBs.Multiselect = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(704, 479);
            this.Controls.Add(this.MainContainer);
            this.MainMenuStrip = this.theMenuStrip;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Convert As-Built Data to GIS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.theMapToolbar.ResumeLayout(false);
            this.theMapToolbar.PerformLayout();
            this.theMenuStrip.ResumeLayout(false);
            this.theMenuStrip.PerformLayout();
            this.theStatusStrip.ResumeLayout(false);
            this.theStatusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.theDataGridView)).EndInit();
            this.MainContainer.BottomToolStripPanel.ResumeLayout(false);
            this.MainContainer.BottomToolStripPanel.PerformLayout();
            this.MainContainer.ContentPanel.ResumeLayout(false);
            this.MainContainer.TopToolStripPanel.ResumeLayout(false);
            this.MainContainer.TopToolStripPanel.PerformLayout();
            this.MainContainer.ResumeLayout(false);
            this.MainContainer.PerformLayout();
            this.MapTableSplitter.Panel1.ResumeLayout(false);
            this.MapTableSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MapTableSplitter)).EndInit();
            this.MapTableSplitter.ResumeLayout(false);
            this.LegendMapLogSplitter.Panel1.ResumeLayout(false);
            this.LegendMapLogSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LegendMapLogSplitter)).EndInit();
            this.LegendMapLogSplitter.ResumeLayout(false);
            this.MapLogSubSplitter.Panel1.ResumeLayout(false);
            this.MapLogSubSplitter.Panel2.ResumeLayout(false);
            this.MapLogSubSplitter.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MapLogSubSplitter)).EndInit();
            this.MapLogSubSplitter.ResumeLayout(false);
            this.TableToolStripContainer.ContentPanel.ResumeLayout(false);
            this.TableToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.TableToolStripContainer.TopToolStripPanel.PerformLayout();
            this.TableToolStripContainer.ResumeLayout(false);
            this.TableToolStripContainer.PerformLayout();
            this.TableToolStrip.ResumeLayout(false);
            this.TableToolStrip.PerformLayout();
            this.TheDatabaseToolbar.ResumeLayout(false);
            this.TheDatabaseToolbar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog dlgSelectDb;
        private System.Windows.Forms.ToolStrip theMapToolbar;
        private System.Windows.Forms.ToolStripButton toolPan;
        private System.Windows.Forms.ToolStripButton toolZoomIn;
        private System.Windows.Forms.ToolStripButton toolZoomOut;
        private System.Windows.Forms.MenuStrip theMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.StatusStrip theStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripProgressBar pgBar;
        private System.Windows.Forms.ToolStripButton btnZoomFullExtent;
        private System.Windows.Forms.DataGridView theDataGridView;
        private System.Windows.Forms.ToolStripButton btnOpenFile;
        private DotSpatial.Controls.Legend theLegend;
        private System.Windows.Forms.ToolStripButton btnIdentify;
        private System.Windows.Forms.SaveFileDialog dlgSaveFile;
        private System.Windows.Forms.ToolStripMenuItem getRandomSelectionOfSignsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnPrintLayout;
        private System.Windows.Forms.OpenFileDialog dlgOpenShapefile;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        public System.Windows.Forms.TextBox tbLog;
        private DotSpatial.Controls.AppManager theAppManager;
        private System.Windows.Forms.ToolStripContainer MainContainer;
        private System.Windows.Forms.SplitContainer LegendMapLogSplitter;
        private System.Windows.Forms.SplitContainer MapLogSubSplitter;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.SplitContainer MapTableSplitter;
        private System.Windows.Forms.ToolStrip TheDatabaseToolbar;
        private System.Windows.Forms.ToolStripTextBox tbSelDbFn;
        private System.Windows.Forms.ToolStripButton btnSelectDb;
        private System.Windows.Forms.ToolStripLabel lblDb;
        private System.Windows.Forms.ToolStripContainer TableToolStripContainer;
        private System.Windows.Forms.ToolStrip TableToolStrip;
        private System.Windows.Forms.ToolStripLabel lblSortField1;
        private System.Windows.Forms.ToolStripComboBox cbSortField1;
        private System.Windows.Forms.ToolStripLabel lblSortField2;
        private System.Windows.Forms.ToolStripComboBox cbSortField2;
        private System.Windows.Forms.ToolStripStatusLabel lblProgress;
        private System.Windows.Forms.ToolStripStatusLabel lblInfo;
        private System.Windows.Forms.ToolStripMenuItem exportSelectedPointLayerToSpatialiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addGoogleSatelliteToolStripMenuItem;
        public DotSpatial.Controls.Map theMap;
        private System.Windows.Forms.ToolStripMenuItem expandWordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateQRcodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importRoadsAndRoadCenterLinesToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnToggleSatelliteImagery;
        private System.Windows.Forms.OpenFileDialog dlgOpenMdbFile;
        private System.Windows.Forms.ToolStripMenuItem exportSelectedAddressUnitLayerToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportSelectedPointLayerAsGPXToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exportSelectedLayerAsFileGDBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportAddressUnitsToGeopaparazziProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportAddressUnitsToSQLITEDbToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportForMyabudhabinetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeBINGSatelliteImageryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportSelectedDatabasesToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sQLforMyabudhabinetToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog dlgSelectDBs;
        private System.Windows.Forms.ToolStripMenuItem fileGDBforADMToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog dlgSelectFolder;
        private System.Windows.Forms.ToolStripMenuItem addPlotIDsToFGDBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cleanFileGDBStreetAndDistrictNamesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportFileGDBToMyabudhabinetSQLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importAddressingDistrictsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem identifyRoadDefinitionSuspectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testQRCodesOfSelectedLayerToolStripMenuItem;
    }
}

