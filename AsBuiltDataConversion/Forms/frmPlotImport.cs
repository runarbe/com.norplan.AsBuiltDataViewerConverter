using OSGeo.OGR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Norplan.Adm.AsBuiltDataConversion.Functions;

namespace Norplan.Adm.AsBuiltDataConversion.Forms
{
    public partial class frmPlotImport : Form
    {
        private string ZoneShapefile;

        private string SectorPlotShapefile;

        public Action<string, bool> LogMsg;

        public frmPlotImport(Action<string,bool> pLogFunction)
        {
            this.LogMsg = pLogFunction;
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RefreshStatus()
        {
            if (Properties.Settings.Default.PlotImportFilePresent)
            {
                lblStatusMessage.Text = "A plot file dated " + Properties.Settings.Default.PlotImportFileDate.ToString("dd.MM.yyyy HH:mm") + " is loaded";
            }
            else
            {
                lblStatusMessage.Text = "No plot file is loaded";
            }
        }

        private void frmImportZoneSectorPlot_Load(object sender, EventArgs e)
        {
            RefreshStatus();
        }

        private void btnSelectSectorPlotFile_Click(object sender, EventArgs e)
        {
            if (dlgOpenShapefile.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                LogMsg("Aborting, please select a FileGeodatabase with sectors, plots...", true);
                return;
            }

            var mFields = new List<string>();

            using (var mDrv = Ogr.GetDriverByName("ESRI Shapefile"))
            using (var mDs = mDrv.Open(dlgOpenShapefile.FileName, 0))
            using (var mLayer = mDs.GetLayerByIndex(0))
            {
                var mLayerDefn = mLayer.GetLayerDefn();
                for (var i = 0; i < mLayerDefn.GetFieldCount(); i++)
                {
                    mFields.Add(mLayerDefn.GetFieldDefn(i).GetName());
                }
            }

            tbSectorPlotFile.Text = Path.GetFileName(dlgOpenShapefile.FileName);

            cbSectorField.BindingContext = new BindingContext();
            cbSectorField.DataSource = mFields;
            cbSectorField.Refresh();
            cbSectorField.Enabled = true;

            if (mFields.Contains("SECTORTPSS"))
            {
                cbSectorField.Text = "SECTORTPSS";
            }

            cbPlotField.BindingContext = new BindingContext();
            cbPlotField.DataSource = mFields;
            cbPlotField.Refresh();
            cbPlotField.Enabled = true;

            if (mFields.Contains("PLOTNUMBER"))
            {
                cbPlotField.Text = "PLOTNUMBER";
            }

            SectorPlotShapefile = dlgOpenShapefile.FileName;

        }

        private void btnSelectZoneFile_Click(object sender, EventArgs e)
        {
            if (dlgOpenShapefile.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                LogMsg("Aborting, please select a Shapefile with zones...", true);
                return;
            }

            var mFields = new List<string>();

            using (var mDrv = Ogr.GetDriverByName("ESRI Shapefile"))
            using (var mDs = mDrv.Open(dlgOpenShapefile.FileName, 0))
            using (var mLayer = mDs.GetLayerByIndex(0))
            {
                var mLayerDefn = mLayer.GetLayerDefn();
                for (var i = 0; i < mLayerDefn.GetFieldCount(); i++)
                {
                    mFields.Add(mLayerDefn.GetFieldDefn(i).GetName());
                }
            }

            tbZoneFile.Text = Path.GetFileName(dlgOpenShapefile.FileName);

            cbZoneField.BindingContext = new BindingContext();
            cbZoneField.DataSource = mFields;
            cbZoneField.Refresh();
            cbZoneField.Enabled = true;

            if (mFields.Contains("ZONETPSSNA"))
            {
                cbZoneField.Text = "ZONETPSSNA";
            }

            ZoneShapefile = dlgOpenShapefile.FileName;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.PlotImportFilePresent)
            {
                if (MessageBox.Show("Would you like to overwrite the existing plotimport file? (Dated: " + Properties.Settings.Default.PlotImportFileDate.ToString("dd.MM.yyyy hh:mm")+ ")", "Confirm overwrite", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                {
                    LogMsg("Operation cancelled by user", true);
                    return;
                }
            }
            if (File.Exists(ZoneShapefile)
                && File.Exists(SectorPlotShapefile)
                && !String.IsNullOrEmpty(cbZoneField.Text)
                && !String.IsNullOrEmpty(cbSectorField.Text)
                && !String.IsNullOrEmpty(cbPlotField.Text))
            {

                using (var mPlotImport = new PlotImport())
                using (var mDrv = Ogr.GetDriverByName("ESRI Shapefile"))
                using (var mZoneDatasource = mDrv.Open(ZoneShapefile, 0))
                using (var mZoneLayer = mZoneDatasource.GetLayerByIndex(0))
                using (var mSectorPlotDatasource = mDrv.Open(SectorPlotShapefile, 0))
                using (var mSectorPlotLayer = mSectorPlotDatasource.GetLayerByIndex(0))
                {
                    mZoneDatasource.ExecuteSQL("CREATE SPATIAL INDEX ON " + Path.GetFileNameWithoutExtension(mZoneDatasource.name),null, null);
                    mPlotImport.StartTransaction();
                    LogMsg(mPlotImport.ShapefileName, true);
                    var i = 0;
                    Feature mPlot;
                    var totalFeatures = mSectorPlotLayer.GetFeatureCount(1);
                    while (null != (mPlot = mSectorPlotLayer.GetNextFeature()))
                    {
                        var mGeom = mPlot.GetGeometryRef();
                        var mZoneName = mZoneLayer.GetFieldAsStringByGeom(cbZoneField.Text,
                            mGeom.PointOnSurface());

                        mPlotImport.AddPlot(mGeom,
                            mZoneName,
                            mPlot.GetFieldAsString(cbSectorField.Text),
                            mPlot.GetFieldAsString(cbPlotField.Text));

                        i++;
                        
                        if (i % 1000 == 0 || i == totalFeatures)
                        {
                            LogMsg("Processed: " + i, true);
                        }

                    }

                    mPlotImport.CommitTransaction();
                    mPlotImport.CreateIndex();

                    Properties.Settings.Default.PlotImportFilePresent = true;
                    Properties.Settings.Default.PlotImportFileDate = DateTime.Now;
                    RefreshStatus();
                    LogMsg("Operation completed", true);
                    this.Close();
                }

            }
            else
            {
                LogMsg("Please select appropriate input files", true);
            }

        }

        private void tlp_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
