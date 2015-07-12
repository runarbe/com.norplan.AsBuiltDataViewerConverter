using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using AsBuiltToGIS.Functions;
using DotSpatial;
using DotSpatial.Data;
using DotSpatial.Topology;
using System.Diagnostics;

namespace AsBuiltToGIS.FeatureTypes
{
    class AddressUnitFeature : AdmAdrFeature
    {
        public AddressUnitFeature(Database pDbx)
        {
            this.dbx = pDbx;

            // Set feature type
            this.FeatureType = DotSpatial.Topology.FeatureType.Point;
            // ADDRESSUNITID
            this.DataTable.Columns.Add(new DataColumn("ADDRESSUNITID", typeof(int)));
            // ROADID
            this.DataTable.Columns.Add(new DataColumn("ROADID", typeof(int)));
            // AREA ABBREVIATION
            this.DataTable.Columns.Add(new DataColumn("DISTRICTID", typeof(string)));
            // ADDRESSUNITNR
            this.DataTable.Columns.Add(new DataColumn("ADDRESSUNITNR", typeof(string)));
            // ROADNAME_EN
            this.DataTable.Columns.Add(new DataColumn("ROADNAME_EN", typeof(string)));
            // ROADNAME_AR
            this.DataTable.Columns.Add(new DataColumn("ROADNAME_AR", typeof(string)));
            // ROADNAME_POP_EN
            this.DataTable.Columns.Add(new DataColumn("ROADNAME_POP_EN", typeof(string)));
            // ROADNAME_POP_AR
            this.DataTable.Columns.Add(new DataColumn("ROADNAME_POP_AR", typeof(string)));
            // DISTRICT_EN
            this.DataTable.Columns.Add(new DataColumn("DISTRICT_EN", typeof(string)));
            // DISTRICT_AR
            this.DataTable.Columns.Add(new DataColumn("DISTRICT_AR", typeof(string)));
            // MUNICIPALITY_EN
            this.DataTable.Columns.Add(new DataColumn("MUNICIPALITY_EN", typeof(string)));
            // MUNICIPALITY_AR
            this.DataTable.Columns.Add(new DataColumn("MUNICIPALITY_AR", typeof(string)));
            // QR-CODE
            this.DataTable.Columns.Add(new DataColumn("QR_CODE", typeof(string)));
        }

        public FeatureSet PopulateFromTable()
        {
            DataTable pTable = this.GetTable(sqlStatements.selectAddressUnitsSQL);

            foreach (DataRow mRow in pTable.Rows)
            {
                var mXY = ExtFunctions.GetXYFromRow(mRow);
                if (mXY != null)
                {
                    var mPoint = new DotSpatial.Topology.Point(mXY[0], mXY[1]);

                    var mFeature = this.AddFeature(mPoint);

                    mFeature.DataRow.BeginEdit();

                    mFeature.DataRow["ADDRESSUNITID"] = mRow["addressUnitNumber"].ToString();
                    mFeature.DataRow["ROADID"] = mRow["road_id"].ToString();
                    mFeature.DataRow["ADDRESSUNITNR"] = mRow["addressUnitNumber"].ToString();
                    mFeature.DataRow["ROADNAME_EN"] = mRow["NAMEENGLISH"].ToString();
                    mFeature.DataRow["ROADNAME_AR"] = mRow["NAMEARABIC"].ToString();
                    mFeature.DataRow["ROADNAME_POP_EN"] = "";
                    mFeature.DataRow["ROADNAME_POP_AR"] = "";
                    mFeature.DataRow["DISTRICT_EN"] = mRow["DISTRICT_EN"].ToString();
                    mFeature.DataRow["DISTRICT_AR"] = mRow["DISTRICT_AR"].ToString();
                    mFeature.DataRow["DISTRICTID"] = mRow["district_id"].ToString();
                    mFeature.DataRow["MUNICIPALITY_EN"] = Utilities.LABEL_ABUDHABI_EN;
                    mFeature.DataRow["MUNICIPALITY_AR"] = Utilities.LABEL_ABUDHABI_AR;
                    mFeature.DataRow["QR_CODE"] = this.GetQRCode(mRow, pTable);
                    mFeature.DataRow.EndEdit();
                }
            }

            return this;

        }
    }

}
