using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DotSpatial.Controls;

namespace AsBuiltToGIS.Forms
{
    public partial class frmLayout : Form
    {
        public frmLayout(Map pMap)
        {
            InitializeComponent();
            theLayoutControl.MapControl = pMap;
        }

        private void layoutMenuStrip1_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLayout_Load(object sender, EventArgs e)
        {
            theLayoutControl.ZoomFitToScreen();
        }
    }
}
