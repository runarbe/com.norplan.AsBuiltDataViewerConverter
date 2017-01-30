using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Norplan.Adm.AsBuiltDataConversion.DataTypes;
using Norplan.Adm.AsBuiltDataConversion.FeatureTypes;
using Norplan.Adm.AsBuiltDataConversion.Forms;
using Norplan.Adm.AsBuiltDataConversion.Functions;
using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Projections;
using DotSpatial.Symbology;
using norplan.adm.qrlib;
using DotSpatial.Topology;
using CsvHelper.Excel;
using Norplan.Adm.AsBuiltDataConversion.MyAbuDhabi;
using System.Diagnostics;
using Norplan.Adm.AsBuiltDataConversion.CSharpFeatureTypes;
using System.Reflection;
using OSGeo.OGR;
using OSGeo.GDAL;

namespace Norplan.Adm.AsBuiltDataConversion
{
    public partial class frmMain : Form
    {

        #region declarations


        /// <summary>
        /// Layer object to hold bing satellite object
        /// </summary>
        public static BruTileLayer lyrBingSatellite = null;

        /// <summary>
        /// The main database connection for the application
        /// </summary>
        public static Database dbx = null;

        /// <summary>
        /// A re-usable datatable object
        /// </summary>
        public DataTable table = null;

        /// <summary>
        /// A re-usable dataview object
        /// </summary>
        public DataView dataview = null;

        /// <summary>
        /// handle for the satellite imagery 
        /// </summary>
        public BruTileLayer satellite = null;

        /// <summary>
        /// handle for the vector basemap
        /// </summary>
        public MapGroup basemap = null;

        /// <summary>
        /// A map function
        /// </summary>
        private MapFunction mClickZoomFunction = null;

        /// <summary>
        /// The EPSG code of the map projection
        /// </summary>
        public static int EPSGCode = 3857;

        /// <summary>
        /// The default projection for the project, by default Web Mercator (Bing, Google etc)
        /// </summary>
        public static ProjectionInfo MapProjection = KnownCoordinateSystems.Projected.World.WebMercator;

        /// <summary>
        /// Enable DotSpatial extensions
        /// </summary>
        [Export("Shell", typeof(ContainerControl))]
        private static ContainerControl Shell;

        #endregion

        #region Constructor, initializer
        /// <summary>
        /// Main constructor
        /// </summary>
        public frmMain()
        {
            // Export extension point
            Shell = this;

            // INitialize layout components
            InitializeComponent();

            // Load extensions, if any
            //theAppManager.LoadExtensions();

            // Add map function to map
            mClickZoomFunction = new DotSpatial.Controls.MapFunctionClickZoom(theMap);
            theMap.MapFunctions.Add(mClickZoomFunction);

            // Add additional click-handlers to elements
            theMap.Layers.LayerSelected += new EventHandler<LayerSelectedEventArgs>(onLayerSelectionChanged);

            // Set selection properties on datagridview
            theDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            theDataGridView.SelectionChanged += new EventHandler(onGridViewSelectionChanged);

            // Default map projection
            frmMain.MapProjection = KnownCoordinateSystems.Projected.World.WebMercator;

            // Set map projection
            theMap.Projection = frmMain.MapProjection;

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Text = String.Format("As-Built Data Viewer and Converter (build: {0} on the {1})",
                Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                Properties.Resources.BuildDate);
            // Add base layers
            theMap.BackColor = Color.LightBlue;
            ExtFunctions.AddBaseLayers(theMap);
            CheckIfPlotFileImported();
            Log("Application ready...");
        }

        private void CheckIfPlotFileImported()
        {
            if (File.Exists(PlotImport.GetShapefileName()))
            {
                Properties.Settings.Default.PlotImportFilePresent = true;
                Properties.Settings.Default.PlotImportFileDate = new FileInfo(PlotImport.GetShapefileName()).LastWriteTime;
            }
        }

        #endregion;

        #region Click, event handlers

        private void onGridViewSelectionChanged(object sender, EventArgs e)
        {
            Utilities.LogDebug("Selection changed");
        }

        private void onLayerSelectionChanged(Object o, LayerSelectedEventArgs e)
        {
            if (e.IsSelected == false)
            {
                if (e.Layer is IFeatureLayer)
                {
                    //ExtFunctions.UpdateDataTable(
                    this.UpdateDataTable((IFeatureSet)e.Layer.DataSet);
                    tcTableLog.SelectedTab = TableTab;
                }
            }

        }

        private void toolBtnPan_Click(object sender, EventArgs e)
        {
            theMap.FunctionMode = DotSpatial.Controls.FunctionMode.Pan;
        }

        private void toolBtnZoomIn_Click(object sender, EventArgs e)
        {
            theMap.ZoomIn();
        }

        private void toolBtnZoomOut_Click(object sender, EventArgs e)
        {
            theMap.ZoomOut();
        }

        public void Log(Object pMsg, bool pDoEvents = false)
        {
            if (this.tbLog.Lines.Length > 256)
            {
                this.tbLog.Lines = this.tbLog.Lines.GetLastN(50);
            }
            this.tbLog.AppendText(pMsg.ToString() + Environment.NewLine);
            tcTableLog.SelectedTab = LogTab;
            if (pDoEvents)
            {
                Application.DoEvents();
            }
            return;
        }

        private void parseToESRIShapefileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProxyParseSelectedDatabase();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolBtnZoomToMaxExtent_Click(object sender, EventArgs e)
        {
            theMap.ZoomToMaxExtent();
        }

        private void toolBar_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void addLayerToMapToolStripMenuItem_Click(object sender, EventArgs e)
        {

            theMap.AddLayer();
        }

        private void toolBtnAddLayer_Click(object sender, EventArgs e)
        {
            theMap.AddLayer();
        }

        private void btnSelectParseDatabase(object sender, EventArgs e)
        {
            if (SelectDatabase())
            {
                ProxyParseSelectedDatabase();
            };
        }

        private void toolBtnIdentify_Click(object sender, EventArgs e)
        {
            ExtFunctions.ToggleSatelliteLayer(this, true);
            theMap.FunctionMode = DotSpatial.Controls.FunctionMode.Info;
        }

        private void btnZoomBox_Click(object sender, EventArgs e)
        {

        }

        private void getRandomSelectionOfSignsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProxyCreateRandom();
        }

        private void addLayerToMapToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            theMap.AddLayers();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            theMap.FunctionMode = FunctionMode.None;
            mClickZoomFunction.Activate();
        }

        private void btnPrintLayout_Click(object sender, EventArgs e)
        {
            var lFrm = new frmLayout(theMap);
            LayoutFunctions.LayoutPageSize(theMap, lFrm.theLayoutControl, "Testmap");
            lFrm.ShowDialog(this);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mFrmAboutBox = new frmAboutBox();
            mFrmAboutBox.ShowDialog();
            mFrmAboutBox = null;
        }

        private void parseToGMLToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (frmMain.dbx != null)
            {
                // Get outputShapefileName
                dlgSaveFile.Title = "Please select where to save the GML file";
                dlgSaveFile.FileName = frmMain.dbx.DbBaseName + ".gml";
                dlgSaveFile.Filter = "GML file|*.gml";
                if (dlgSaveFile.ShowDialog() != DialogResult.OK)
                {
                    Log("Operation cancelled, no output file specified...");
                    return;
                }
                var m = ExtFunctions.ExportSelectedDatabaseToGML(Log, dlgSaveFile.FileName);
                Log("Operation completed");
            }

        }

        #endregion

        #region Proxy functions for underlying libraries

        private bool SelectDatabase()
        {
            dlgSelectDb.Title = "Please select a database";
            dlgSelectDb.Filter = "Microsoft Access >= 2010 database|*.accdb";
            dlgSelectDb.FileName = "*.accdb";
            var dlgResult = dlgSelectDb.ShowDialog();
            if (dlgResult == DialogResult.OK)
            {
                frmMain.dbx = new Database(dlgSelectDb.FileName);
                this.tbSelDbFn.Text = new FileInfo(dlgSelectDb.FileName).Name;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ProxyParseSelectedDatabase()
        {
            try
            {
                ExtFunctions.ParseSelectedDatabase(theMap, ProjectionInfo.FromEpsgCode(32640));
                Log("Operation completed");

            }
            catch (Exception ex)
            {
                Log("Operation aborted: " + ex.Message);
            }
        }

        private void ProxyCreateRandom()
        {
            // Default sample size
            int pSample = 25;

            var mSelectedLayer = ExtFunctions.GetSelectedLayer(theMap);

            IFeatureLayer mLayer = null;

            if (mSelectedLayer == null || !(mSelectedLayer is IFeatureLayer))
            {
                this.Log("No feature layer selected, please select a layer and try again.");
                return;
            }
            mLayer = (IFeatureLayer)mSelectedLayer;

            var mFrmInputBox = new frmInputBox("Specify size of sample", "Please specify number of items to be included in the output file", pSample);

            if (DialogResult.OK == mFrmInputBox.ShowDialog())
            {
                if (mFrmInputBox.GetAsInteger() != null)
                {
                    pSample = (int)mFrmInputBox.GetAsInteger();
                    Utilities.LogDebug("Using specified sample size: " + pSample);
                }
                else if (mFrmInputBox.GetAsPercent() != null)
                {
                    int mPercentage = (int)mFrmInputBox.GetAsPercent();
                    IFeatureSet mFeatureSet = mLayer.DataSet;
                    pSample = (int)Math.Floor(((double)mPercentage * (double)mFeatureSet.NumRows()) / 100);
                    Utilities.LogDebug("Using " + mPercentage + "% : " + pSample);
                }
                else
                {
                    Utilities.LogDebug("Using default sample size: " + pSample);
                }

                this.Log(pSample.ToString());
                if (pSample > 0)
                {
                    var mRndFeatureSet = ExtFunctions.CreateRandomSelection(mLayer, pSample);

                    if (mRndFeatureSet == null)
                    {
                        Utilities.LogDebug("Creation of random selection layer failed");
                        return;
                    }

                    var mLayer2 = (IFeatureLayer)ExtFunctions.GetFeatureLayer(theMap.Layers, mRndFeatureSet, mLayer.LegendText + " : random", ExtFunctions.CopyLayerSymbolizer(mLayer), mLayer.Projection);
                    ExtFunctions.AddLabelsForFeatureLayer(mLayer2, "Address unit numbers", "#[ADDRESSUNITNR], [ROADNAME_EN]", GoogleMapsColors.BoundaryMajor);

                }
            }

        }
        private void exportSelectedLayerAsFileGDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFeatureLayer mLayer;

            if (null != (mLayer = ExtFunctions.GetSelectedAddressUnitLayer(theMap)))
            {
                string mOutputFilename;

                if (null != (mOutputFilename = SelectOutputFilename(null, mLayer.LegendText, "File GDB|*.gdb")))
                {
                    var mReturnValue = ExtFunctions.ExportFeatureLayerToOGR(
                        pDrvNm: "FileGDB",
                        pFLyr: (IFeatureLayer)theMap.Layers.SelectedLayer,
                        pOPFn: dlgSaveFile.FileName,
                        pSrcProj: theMap.Projection,
                        pTgtProj: ExtFunctions.GetProjByEPSG(32640),
                        pLCOpts: new List<string>() { "FEATURE_DATASET=Simplified" });

                    Log(mReturnValue.GetMessages());
                    Log("Operation completed");
                }
                else
                {
                    Log("Operation cancelled: No output FileGDB name specified");
                }
            }
            else
            {
                Log("Operation cancelled: No layer selected");
            }
        }


        private void btnSelectDb_Click(object sender, EventArgs e)
        {
            if (this.SelectDatabase())
            {
                ProxyParseSelectedDatabase();
            }

        }
        private void onUpdateSort1(object sender, EventArgs e)
        {
            UpdateDataGridViewSortOrder();
        }

        private void onUpdateSort2(object sender, EventArgs e)
        {
            UpdateDataGridViewSortOrder();
        }

        private void exportSelectedFeatureLayerToSpatialiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFeatureLayer mLyr = ExtFunctions.GetSelectedFeatureLayer(theMap);
            if (mLyr == null) return;

            string mOPFn = SelectOutputFilename(null, mLyr.LegendText + ".sqlite", "Spatialite|*.sqlite");
            if (mOPFn == null) return;

            var mDSCOpts = new List<string>();
            mDSCOpts.Add("SPATIALITE=YES");

            var mLCOpts = new List<string>();
            mLCOpts.Add("SPATIAL_INDEX=YES");

            var mReturnValue = ExtFunctions.ExportFeatureLayerToOGR(
                "SQLite",
                mLyr,
                mOPFn,
                theMap.Projection,
                ExtFunctions.GetProjByEPSG(4326),
                false,
                null,
                null,
                mLCOpts,
                mDSCOpts);

            SetFunctionExecutionStatus(mReturnValue);
            return;

        }

        private void SetFunctionExecutionStatus(ReturnValue pReturnValue)
        {
            lblStatus.Text = (pReturnValue.Success) ? "Operation completed successfully" : "Operation failed";
            foreach (string pMsg in pReturnValue.Messages)
            {
                this.Log(pMsg);
            }

        }


        private void exportSelectedPointLayerAsGPXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IPointLayer mLyr = ExtFunctions.GetSelectedAddressUnitLayer(theMap);
            if (mLyr == null)
            {
                Utilities.LogDebug("No point layer was selected");
                return;
            }

            var mOPFn = SelectOutputFilename(null, mLyr.LegendText + ".gpx", "GPS Exchange Format|*.gpx");
            if (mOPFn == null)
            {
                Utilities.LogDebug("No filename specified");
                return;
            }

            var mFieldMap = new Dictionary<string, string>();
            mFieldMap.Add(ExtFunctions.TitleFieldName, "name");
            mFieldMap.Add("ROADNAME_EN", "desc");

            var success = ExtFunctions.ExportFeatureLayerToOGR(
                pDrvNm: "GPX",
                pFLyr: mLyr,
                pOPFn: mOPFn,
                pSrcProj: theMap.Projection,
                pTgtProj: ExtFunctions.GetProjByEPSG(4326),
                pHasTitle: true,
                pTitleFieldNames: "ADDRESSUNITNR,ROADID",
                pTitleFormat: "wpt{0}-{1}",
                pDSCOpts: new List<string>() { "GPX_USE_EXTENSIONS=NO" },
                pFieldMap: mFieldMap,
                pOnlyInFieldMap: true
                );
        }

        #endregion


        private void exportSelectedLayerAsKMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFeatureLayer mLayer;

            if (null != (mLayer = ExtFunctions.GetSelectedAddressUnitLayer(theMap)))
            {
                dlgSaveFile.Title = "Choose where to save the exported KML file";
                dlgSaveFile.Filter = "Keyhole Markup File (KML)|*.kml";
                dlgSaveFile.FileName = theMap.Layers.SelectedLayer.LegendText + ".kml";
                if (dlgSaveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var mReturnValue = ExtFunctions.ExportFeatureLayerToOGR(
                        pDrvNm: "KML",
                        pFLyr: (IFeatureLayer)theMap.Layers.SelectedLayer,
                        pOPFn: dlgSaveFile.FileName,
                        pHasTitle: true,
                        pSrcProj: theMap.Projection,
                        pTgtProj: ExtFunctions.GetProjByEPSG(4326),
                        pTitleFieldNames: "ADDRESSUNITNR",
                        pTitleFormat: "#{0}");

                    SetFunctionExecutionStatus(mReturnValue);

                }
            }
            else
            {
                Utilities.LogDebug("No layer selected");
            }
        }

        /// <summary>
        /// Returns the selected output outputShapefileName or null if none specified
        /// </summary>
        /// <param name="pTitle">Title of dialog box</param>
        /// <param name="pFilename">Suggested outputShapefileName</param>
        /// <param name="pFilter">Filter</param>
        /// <returns></returns>
        private string SelectOutputFilename(string pTitle = "Choose location and filename", string pFilename = "", string pFilter = "All file types|*.*", bool pConfirmOverwrite = true)
        {
            dlgSaveFile.OverwritePrompt = pConfirmOverwrite;
            dlgSaveFile.Title = pTitle;
            dlgSaveFile.Filter = pFilter;
            dlgSaveFile.FileName = pFilename;
            return (dlgSaveFile.ShowDialog() == DialogResult.OK) ? dlgSaveFile.FileName : null;
        }

        public void UpdateDataTable(IFeatureSet pFeatureSet)
        {

            if (dataview != null)
            {
                dataview.Sort = "";
            }
            cbSortField1.ComboBox.DataSource = null;
            cbSortField2.ComboBox.DataSource = null;

            dataview = new DataView(pFeatureSet.DataTable);
            dataview.Sort = "";
            theDataGridView.DataSource = dataview;

            // Get all columns
            var mColumns = new List<string>();
            foreach (DataGridViewColumn mCol in theDataGridView.Columns)
            {
                mColumns.Add(mCol.Name);
            }

            // Clear and prepare sort fields
            cbSortField1.ComboBox.DataSource = mColumns;
            cbSortField2.ComboBox.DataSource = new List<string>(mColumns);

            theDataGridView.Refresh();

            return;
        }

        private void UpdateDataGridViewSortOrder()
        {
            string mSortString = "";
            string mSortField1 = (string)cbSortField1.ComboBox.SelectedValue;
            string mSortField2 = (string)cbSortField2.ComboBox.SelectedValue;

            if (!string.IsNullOrEmpty(mSortField1))
            {
                mSortString = mSortField1 + " ASC";
                if (!string.IsNullOrEmpty(mSortField2))
                {
                    mSortString += ", " + mSortField2 + " ASC";
                }
                dataview.Sort = mSortString;
                theDataGridView.DataSource = dataview;
                theDataGridView.Refresh();
            }
        }

        private void exportAddressUnitsToGeopaparazziProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mLyr = ExtFunctions.GetSelectedAddressUnitLayer(theMap);
            if (mLyr == null) return;

            var mOPFn = SelectOutputFilename(null, mLyr.LegendText + ".zip", "Compressed archive|*.zip");
            if (mOPFn == null) return;

            ExtFunctions.ExportToGeopaparazzi(mLyr, mOPFn, theMap.Projection, KnownCoordinateSystems.Geographic.World.WGS1984);
        }

        private void exportSelectedAddressUnitSignLayerToToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addGoogleSatelliteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtFunctions.ToggleSatelliteLayer(this);
        }

        private void exportAddressUnitsToSQLITEDbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mLyr = ExtFunctions.GetSelectedAddressUnitLayer(theMap);
            if (mLyr == null) return;

            var mOpFn = SelectOutputFilename(null, mLyr.LegendText + ".sqlite", "SQLite database|*.sqlite", false);
            if (mOpFn == null) return;

            ExtFunctions.ExportToSQLiteDatabase(
                mLyr,
                mOpFn,
                theMap.Projection,
                ExtFunctions.GetProjByEPSG(4326));

        }

        private void exportAddressUnitsToMySQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mLyr = ExtFunctions.GetSelectedAddressUnitLayer(theMap);
            if (mLyr == null) return;

            var frm = new frmMySQLConnection(this, mLyr);
            if (frm.ShowDialog() != DialogResult.OK)
            {
                return;
            }
        }


        private void generateQRcodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mQrCodeGen = new frmQrCodeGen();
            mQrCodeGen.ShowDialog();
            mQrCodeGen = null;
        }

        private void importRoadsAndRoadCenterLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Add select dialog here...
            dlgOpenMdbFile.Filter = "Addressing Database|*.mdb";
            dlgOpenMdbFile.FileName = "*.mdb";
            if (dlgOpenMdbFile.ShowDialog() == DialogResult.OK)
            {
                var mRoadsFeatureSet = ExtFunctions.GetRoadFeatureSetFromAdmAdrMdb(ref this.pgBar, Log, dlgOpenMdbFile.FileName, 1);
                var mRoadsLayer = ExtFunctions.GetFeatureLayer(theMap.Layers, mRoadsFeatureSet, "SimplifiedRoads", MapSymbols.LineSymbol(SignColors.AddressUnitSign, 2), KnownCoordinateSystems.Projected.UtmWgs1984.WGS1984UTMZone40N);
                dlgSaveFile.Filter = "FileGeodatabases|*.gdb";
                dlgSaveFile.Title = "Save imported roads to ESRI FileGDB";
                if (dlgSaveFile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ExtFunctions.ExportFeatureLayerToOGR("FileGDB", mRoadsLayer, dlgSaveFile.FileName, KnownCoordinateSystems.Projected.UtmWgs1984.WGS1984UTMZone40N, KnownCoordinateSystems.Projected.UtmWgs1984.WGS1984UTMZone40N);
                    }
                    catch (Exception ex)
                    {
                        Log("Operation cancelled");
                        Log(ex.Message);
                    }
                }
                else
                {
                    Log("Export to FileGDB cancelled");
                }
                if (MessageBox.Show("Would you like to add the imported roads to the map?", "Import roads", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    mRoadsLayer.Reproject(theMap.Projection);
                    theMap.Refresh();
                }

            }
            else
            {
                Log("Operation cancelled, please select an addressing database file");
            }
        }

        private void btnToggleSatelliteImagery_Click(object sender, EventArgs e)
        {
            ExtFunctions.ToggleSatelliteLayer(this);
        }

        private void exportForMyabudhabinetToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var mOPFn = SelectOutputFilename(null, "myabudhabi_net_ddl.sql", "SQL|*.sql");
            if (mOPFn == null)
            {
                Utilities.LogDebug("No filename specified");
                return;
            }

            var mLyr = ExtFunctions.GetSelectedAddressUnitLayer(theMap);
            if (mLyr == null)
            {
                Utilities.LogDebug("No address unit layer selected");
                return;
            }

            ExtFunctions.ExportToMyAbuDhabiNet(
                mOPFn,
                mLyr,
                theMap.Projection,
                ExtFunctions.GetProjByEPSG(4326),
                false,
                SignType.addressUnitNumberSign);
        }

        private void removeBINGSatelliteImageryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmMain.lyrBingSatellite != null && theMap.Layers.Contains(frmMain.lyrBingSatellite))
            {
                theMap.Layers.Remove(frmMain.lyrBingSatellite);
            }
            frmMain.lyrBingSatellite.Dispose();
            frmMain.lyrBingSatellite = null;
        }

        private void sQLforMyabudhabinetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dlgSelectDBs.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                this.Log("Please select one or more files...");
                return;
            }

            var mFileInfo = new FileInfo(dlgSelectDBs.FileNames[0]);

            dlgSaveFile.FileName = mFileInfo.DirectoryName + "\\myabudhabi.net.sql";
            dlgSaveFile.Title = "Please select output filename";
            dlgSaveFile.Filter = "SQL file|*.sql";
            if (dlgSaveFile.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                this.Log("Please select an output filename");
                return;
            }
            try
            {
                ExtFunctions.ExportMultipleToMyAbuDhabiNet(this, dlgSelectDBs.FileNames, dlgSaveFile.FileName);
                Log("Operation completed");

            }
            catch (Exception ex)
            {
                Log("Operation aborted: " + ex.Message);
            }
        }

        private void fileGDBforADMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Log("Starting export", true);

            dlgSelectDBs.Title = "Please select one or more *.accdb files as submitted by the contractors";
            if (dlgSelectDBs.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                this.Log("Please select one or more files...");
                return;
            }

            var mFileInfo = new FileInfo(dlgSelectDBs.FileNames[0]);

            dlgSelectFolder.RootFolder = Environment.SpecialFolder.Desktop;
            dlgSelectFolder.SelectedPath = mFileInfo.DirectoryName;

            dlgSelectFolder.Description = "Please select a FileGDB to append to - or a directory to create a new FileGDB...";
            if (dlgSelectFolder.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                this.Log("Please select an output filename");
                return;
            }
            string mSelectedFolder = dlgSelectFolder.SelectedPath;
            if (mSelectedFolder.EndsWith(".gdb"))
            {
                Log(String.Format("Appending to existing ESRI FileGDB ({0})", mSelectedFolder));
            }
            else
            {
                int mIdx = 1;
                var mFolder = mSelectedFolder + "\\OnwaniFGDB.gdb";
                while (Directory.Exists(mFolder))
                {
                    mFolder = mSelectedFolder + "\\OnwaniFGDB" + mIdx.ToString("000") + ".gdb";
                    mIdx++;
                }
                Log(String.Format("Using ESRI FileGDB ({0})", mFolder));
                mSelectedFolder = mFolder;
            }

            try
            {
                ExtFunctions.ExportMultipleToOGR(this, dlgSelectDBs.FileNames, mSelectedFolder);
            }
            catch (Exception ex)
            {
                Log("Operation aborted: " + ex.Message);
            }
        }

        private void addPlotIDsToFGDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dlgSelectFolder.Description = "Please select Onwani FileGDB (created by this tool)";
            if (dlgSelectFolder.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            var mOnwaniFileGDB = dlgSelectFolder.SelectedPath;

            var mPlotShapefile = PlotImport.GetShapefileName();

            if (!File.Exists(mPlotShapefile))
            {
                Log("No plot file loaded, please import one using the 'Import zone, sector, plot' command");
                return;
            }

            Dictionary<string, string> mFldsToCopy = new Dictionary<string, string>() {
                {"ZONETPSSNA", "ZONETPSS"},
                {"SECTORTPSS", "SECTORTPSS"},
                {"PLOTNUMBER", "PLOTNUMBER"} 
            };

            double[] mBufferSteps = new double[] { 0, 0.1, 0.25, 0.5, 0.75, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            string mFeatClassName = "Address_unit_signs";

            Log("Adding zone, sector and plot to address unit signs");
            try
            {
                ExtFunctions.AddPlotIDsToAddressDB(this, mOnwaniFileGDB, mPlotShapefile, mFldsToCopy, "PLOT", mBufferSteps, mFeatClassName);
                Log("Completed process");
            }
            catch (Exception ex)
            {
                Log("Operation aborted: " + ex.Message);
            }

        }

        private void cleanFileGDBStreetAndDistrictNamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dlgSelectFolder.Description = "Please select Onwani FileGDB to clean (previously created with this tool)";
            if (dlgSelectFolder.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            var mTgtFileGDB = dlgSelectFolder.SelectedPath;

            dlgOpenMdbFile.Title = "Please select latest 'adm-adr.mdb' PGeoDB for road names";
            dlgOpenMdbFile.Filter = "ESRI Personal Geodatabase|*.mdb";
            dlgOpenMdbFile.FileName = "adm-adr.mdb";
            if (dlgOpenMdbFile.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            var mSrcPGeoDB = dlgOpenMdbFile.FileName;
            
            ExtFunctions.CleanFileGDBNames(this, mTgtFileGDB, mSrcPGeoDB, "Address_unit_signs");
            ExtFunctions.CleanFileGDBNames(this, mTgtFileGDB, mSrcPGeoDB, "Street_name_signs");
            ExtFunctions.CleanFileGDBNames(this, mTgtFileGDB, mSrcPGeoDB, "Address_guide_sign");

            Log("Completed process");

        }

        private void exportFileGDBToMyabudhabinetSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dlgSelectFolder.Description = "Please select Onwani FileGDB to export to SQL (previously created with this tool)";
            if (dlgSelectFolder.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            if (!dlgSelectFolder.SelectedPath.EndsWith(".gdb")) return;

            dlgSaveFile.Title = "Please select where to save the myabudhabi.net SQL file";
            dlgSaveFile.Filter = "myabudhabi.net SQL-file|*.sql";
            dlgSaveFile.FileName = "myabudhabi.net.sql";
            if (dlgSaveFile.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            var mExportResult = ExtFunctions.ExportFileGDBToMyAbuDhabiDotNetSQL(this, dlgSelectFolder.SelectedPath, dlgSaveFile.FileName, 25, false);

            Log("Completed process");
        }

        private void importAddressingDistrictsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dlgOpenMdbFile.Title = "Please select an addressing database";
            dlgOpenMdbFile.FileName = "*.mdb";
            dlgOpenMdbFile.Filter = "Addressing Database|*.mdb";

            if (dlgOpenMdbFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var mDistrictsFeatureSet = ExtFunctions.GetDistrictsFeatureSetFromAdmAdrMdb(ref this.pgBar, dlgOpenMdbFile.FileName, 0);
                    if (mDistrictsFeatureSet == null)
                    {
                        return;
                    }
                    var mDistrictsLayer = ExtFunctions.GetFeatureLayer(theMap.Layers, mDistrictsFeatureSet, "Districts", MapSymbols.PolygonSymbol(Color.Transparent, Color.Red), KnownCoordinateSystems.Projected.UtmWgs1984.WGS1984UTMZone40N);
                    
                    mDistrictsLayer.DataSet.ExportToShapeUsingOgr(DistrictImport.GetShapefileName());

                    mDistrictsLayer.Reproject(theMap.Projection);
                    theMap.Refresh();


                    // Set properties
                    Properties.Settings.Default.DistrictFilePresent = true;
                    Properties.Settings.Default.DistrictImportFileDate = DateTime.Now;

                    Log("Operation completed, saved imported districts to: " + DistrictImport.GetShapefileName());
                }
                catch (Exception ex)
                {
                    Log("Operation aborted: " + ex.Message);
                    Log("Look for issues with duplicate district abbreviations and make sure that you have selected an 'adm-adr' ESRI Personal Geodatabase file that contains districts...");
                }
            }

        }

        private void identifyRoadDefinitionSuspectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Add select dialog here...
            dlgOpenMdbFile.Filter = "Addressing Personal Geodatabase|*.mdb";
            dlgOpenMdbFile.FileName = "*.mdb";
            if (dlgOpenMdbFile.ShowDialog() == DialogResult.OK)
            {
                dlgSaveFile.Title = "Select log file location";
                dlgSaveFile.InitialDirectory = Path.GetDirectoryName(dlgOpenMdbFile.FileName);
                dlgSaveFile.FileName = Path.GetFileNameWithoutExtension(dlgOpenMdbFile.FileName) + "-log.xlsx";
                dlgSaveFile.Filter = "Excel log files|*-log.xlsx";
                if (dlgSaveFile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ExtFunctions.AnalyzeRoadBoundingBoxes(dlgOpenMdbFile.FileName, dlgSaveFile.FileName, Log);
                        Log("Operation completed");
                    }
                    catch (Exception ex)
                    {
                        Log("Operation aborted: " + ex.Message);
                    }
                }
            }
        }

        private void testQRCodesOfSelectedLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                IFeatureLayer mLayer;
                string mDistrictsShapefile = Application.StartupPath + "/GisData/districts.shp";

                // Empty the list of processed QR-codes
                QRLib.ResetQRCodes();

                var mQrTestResults = new List<QrTestResult>();

                if (null == (mLayer = ExtFunctions.GetSelectedPointLayer(theMap)))
                {
                    Log("The selected layer is not a point feature");
                    return;
                }
                else
                {
                    dlgSaveFile.FileName = DateTime.Now.ToString("yyyyMMdd") + "-qrtest.log.xlsx";
                    dlgSaveFile.Title = "Please select a log file location (optional)";
                    dlgSaveFile.Filter = "Excel log files|*.log.xslx";
                    if (dlgSaveFile.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    {
                        QRLib.setLogFile(dlgSaveFile.FileName);
                    }
                    bool HasQRCode = false;
                    foreach (var mColumn in mLayer.DataSet.GetColumns())
                    {
                        if (mColumn.ColumnName == "QR_CODE")
                        {
                            HasQRCode = true;
                            break;
                        }
                    }
                    if (!HasQRCode)
                    {
                        Log("No QR code in selected layer");
                    }
                    else
                    {
                        foreach (var mFeature in mLayer.DataSet.Features)
                        {
                            var mQRCode = mFeature.DataRow["QR_CODE"].ToString();
                            if (mFeature.FeatureType == FeatureType.Point)
                            {
                                DotSpatial.Topology.Point mPoint = (DotSpatial.Topology.Point)mFeature.BasicGeometry;
                                var mResult = mQRCode.TestQRCode(mDistrictsShapefile, false, mPoint.X, mPoint.Y, true);
                                if (mResult.HasIssue)
                                {
                                    Log(mResult);
                                }
                                mQrTestResults.Add(mResult);
                            }
                            else
                            {
                                var mResult = mQRCode.TestQRCode(
                                    districtsShapefile: mDistrictsShapefile,
                                    checkForDuplicates: true);
                                Log(mResult);
                                mQrTestResults.Add(mResult);
                            }
                            Application.DoEvents();
                        }
                    }

                    using (var mCsvWriter = new CsvHelper.CsvWriter(new ExcelSerializer(dlgSaveFile.FileName)))
                    {
                        mCsvWriter.WriteRecords(mQrTestResults);
                        Log("Processed " + mQrTestResults.Count + " records...");
                        Log("Wrote output to " + dlgSaveFile.FileName + "...");
                        Log("Operation completed");
                    }

                }
            }
            catch (Exception ex)
            {
                Log("Operation aborted: " + ex.Message);
            }
        }

        private void importZoneSectorAndPlotsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var mFrm = new frmPlotImport(Log);
                mFrm.ShowDialog();
            }
            catch (Exception ex)
            {
                Log("Operation aborted: " + ex.Message);
                throw;
            }
        }

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var updateCheck = new UpdateCheck("http://myabudhabi.net/latest");
                if (updateCheck.IsValid)
                {
                    updateCheck.CheckForUpdates(Log);
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }

        private void cleanMainAddressingDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dlgOpenMdbFile.Title = "Please select a copy of the addressing geodatabase (adm-adr.mdb)";
            dlgOpenMdbFile.FileName = "*.mdb";
            dlgOpenMdbFile.Filter = "ESRI Personal Geodatabase file|*.mdb";

            if (dlgOpenMdbFile.ShowDialog() != DialogResult.OK)
            {
                Log("Please select a filename");
                return;
            }

            using (AdmAdrCleaner dataCleaner = new AdmAdrCleaner(dlgOpenMdbFile.FileName, Log))
            {
                Log("Starting cleaning");
                dataCleaner.RemoveNullGeometries();
                dataCleaner.RemoveDuplicates();
                dataCleaner.RemoveOverlappingGeometries();
                dataCleaner.BlankNamesWhereNotApproved();
                Log("Done");
            }
        }

        private void exportSelectedDatabasesToToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exportSelectedAddressUnitLayerToToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            Gdal.SetConfigOption("SHAPE_ENCODING", "UTF-8");
            using (DataSource ds = Ogr.Open(DistrictImport.GetShapefileName(), 0))
            {
                using (OSGeo.OGR.Layer l = ds.GetLayerByIndex(0))
                {
                    
                    OSGeo.OGR.Feature f;

                    while (null != (f = l.GetNextFeature()))
                    {
                        this.Log(f.GetFieldAsString("NAMEARABIC"));
                    }
                }
            }
            //var dlgSelectPGeo = new OpenFileDialog
            //{
            //    Title = "Please select a Personal Geodatabase",
            //    Filter = "Microsoft Access database|*.accdb",
            //    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            //};

            //var dlgSelectFileGDB = new FolderBrowserDialog
            //{
            //    RootFolder = Environment.SpecialFolder.Desktop,
            //    Description = "Please select an ESRI FileGeodatabasee (a folder with .gdb ending)"
            //};

            //if (dlgSelectFileGDB.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            //{
            //    return;
            //};

            //if (!dlgSelectFileGDB.SelectedPath.EndsWith(".gdb"))
            //{
            //    Log("Selected folder does not have the correct *.gdb extension");
            //    return;
            //}

            //using (OSGeo.OGR.DataSource districtDataSource = OSGeo.OGR.Ogr.Open(dlgSelectFileGDB.SelectedPath, 0))
            //{
            //    if (districtDataSource == null)
            //    {
            //        Log("The data source does not exist");
            //    }

            //    using (OSGeo.OGR.Layer lyr = districtDataSource.GetLayerByName("Street_name_signs"))
            //    {

            //        if (lyr == null)
            //        {
            //            Log("The layer could not be opened");
            //            return;
            //        }

            //        OSGeo.OGR.Feature f;
            //        var districtLayer = new List<OgrFeature>();
            //        var of = new OgrStreetNameSign();
            //        while (null != (f = lyr.GetNextFeature()))
            //        {
            //            of.PopulateFromOgrFeature(f);
            //            districtLayer.Add(of);
            //            Log(of.QR_CODE);
            //        }
            //        Log(districtLayer.Count);
            //    }
            //}

            //var s = new SignTestTableANS();
            //Log(s.CreateStatement());
        }

        private void exportAddressingDistrictsToMyabudhabinetSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var exportResult = ExtFunctions.ExportDistrictsToMyAbuDhabiNet();
                Log(exportResult.GetMessages());
                if (exportResult.Success == true)
                {
                    Log("Operation succeeded");
                }
                else
                {
                    Log("Operation failed");
                }
            }
            catch (Exception ex)
            {
                Log("An error occurred: " + ex.Message);
                Log("Operation failed");
            }

        }

    }
}
