using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AsBuiltToGIS.Functions;
using DotSpatial.Symbology;

namespace AsBuiltToGIS.Forms
{
    public partial class frmMySQLConnection : Form
    {
        frmMain PForm;
        IPointLayer Layer;

        public frmMySQLConnection(frmMain pParent, IPointLayer pLayer)
        {
            InitializeComponent();
            this.PForm = pParent;
            this.Layer = pLayer;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.PForm.Log("Cancelled export to MySQL");
            Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            var mDrivers = Utilities.GetOdbcDriverNames();

            var mCStr = MySQLLib.MakeConnString(
                cbMySQLDriver.Text,
                tbMySQLServer.Text,
                tbMySQLSchema.Text,
                tbMySQLPort.Text,
                tbMySQLUser.Text,
                tbMySQLPass.Text
                );

            ExtFunctions.ExportFeatureLayerToMySQL(mCStr, this.Layer, tbMySQLSchema.Text, chkBoxTruncate.Checked, 32640, 4326);

            Close();

        }

        private void frmMySQLConnection_Load(object sender, EventArgs e)
        {
            btnExport.Enabled = false;

            var mMySQLDriverList = new List<string>();
            var mDrivers = Utilities.GetOdbcDriverNames();
            foreach (string mDriverName in mDrivers)
            {
                if (mDriverName.Contains("MySQL"))
                {
                    mMySQLDriverList.Add(mDriverName);
                    btnExport.Enabled = true;
                }
            }
            cbMySQLDriver.DataSource = mMySQLDriverList;
            cbMySQLDriver.Refresh();

        }

    }
}
