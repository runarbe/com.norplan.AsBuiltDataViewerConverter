using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Norplan.Adm.AsBuiltDataConversion.Functions;
using DotSpatial;
using DotSpatial.Data;
using DotSpatial.Topology;
using System.Diagnostics;

namespace Norplan.Adm.AsBuiltDataConversion.FeatureTypes
{
    class AddressUnitFeature : AdmAdrFeature
    {
        public AddressUnitFeature(Database pDbx)
        {
            this.dbx = pDbx;

            // Set feature type
            this.FeatureType = DotSpatial.Topology.FeatureType.Point;
            // ADDRESSUNITID
            this.AddCol("ADDRESSUNITID", typeof(int));
            // ROADID
            this.AddCol("ROADID", typeof(int));
            // AREA ABBREVIATION
            this.AddCol("DISTRICTID", typeof(string), 100);
            // ADDRESSUNITNR
            this.AddCol("ADDRESSUNITNR", typeof(string), 15);
            // ROADNAME_EN
            this.AddCol("ROADNAME_EN", typeof(string), 200);
            // ROADNAME_AR
            this.AddCol("ROADNAME_AR", typeof(string), 200);
            // ROADNAME_POP_EN
            this.AddCol("ROADNAME_POP_EN", typeof(string), 200);
            // ROADNAME_POP_AR
            this.AddCol("ROADNAME_POP_AR", typeof(string),200);
            // DISTRICT_EN
            this.AddCol("DISTRICT_EN", typeof(string), 200);
            // DISTRICT_AR
            this.AddCol("DISTRICT_AR", typeof(string), 200);
            // MUNICIPALITY_EN
            this.AddCol("MUNICIPALITY_EN", typeof(string), 100);
            // MUNICIPALITY_AR
            this.AddCol("MUNICIPALITY_AR", typeof(string), 100);
            // QR-CODE
            this.AddCol("QR_CODE", typeof(string), 500);
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

                    mFeature.DataRow["ADDRESSUNITID"] = mFeature.Fid;
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
