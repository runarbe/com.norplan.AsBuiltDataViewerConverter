using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using AsBuiltToGIS.Functions;
using DotSpatial;
using DotSpatial.Data;
using DotSpatial.Topology;

namespace AsBuiltToGIS.FeatureTypes
{
    class AddressGuideSignFeature : AdmAdrFeature
    {
        public AddressGuideSignFeature()
        {
            // Set feature type
            this.FeatureType = DotSpatial.Topology.FeatureType.Point;
            this.DataTable.Columns.Add(new DataColumn("SIGNTYPE", typeof(string)));
            this.DataTable.Columns.Add(new DataColumn("SERIALNUMBEROFSIGN", typeof(string)));
            this.DataTable.Columns.Add(new DataColumn("DISTRICT", typeof(string)));
            this.DataTable.Columns.Add(new DataColumn("QR_CODE", typeof(string)));
        }


        public FeatureSet PopulateFromTable(Database pDbx)
        {
            this.dbx = pDbx;

            DataTable pTable = this.GetTable(sqlStatements.selectAddressGuideSignsSQL);

            foreach (DataRow mRow in pTable.Rows)
            {
                var mXY = ExtFunctions.GetXYFromRow(mRow);
                if (mXY != null)
                {
                    var mPoint = new DotSpatial.Topology.Point(mXY[0], mXY[1]);
                    var mFeature = this.AddFeature(mPoint);

                    mFeature.DataRow.BeginEdit();

                    mFeature.DataRow["SIGNTYPE"] = mRow["signType"];
                    mFeature.DataRow["SERIALNUMBEROFSIGN"] = mRow["serialNumberOfSign"];
                    mFeature.DataRow["DISTRICT"] = mRow["district_id"];
                    mFeature.DataRow["QR_CODE"] = this.GetQRCode(mRow, pTable);
                    mFeature.DataRow.EndEdit();
                }
            }

            return this;

        }


    }
}
