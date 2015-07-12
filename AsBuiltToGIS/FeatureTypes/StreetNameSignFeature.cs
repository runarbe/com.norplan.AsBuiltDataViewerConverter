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
    class StreetNameSignFeature : AdmAdrFeature
    {
        public StreetNameSignFeature()
        {
            // Set feature type
            FeatureType = DotSpatial.Topology.FeatureType.Point;
            AddCol("SIGNTYPE", typeof(String));
            AddCol("SERIALNUMBEROFSIGN", typeof(string));
            AddCol("DISTRICT_P1", typeof(string));
            AddCol("STREETNAME_P1", typeof(string));
            AddCol("DISTRICT_P2", typeof(string));
            AddCol("STREETNAME_P2", typeof(string));
            AddCol("QR_CODE", typeof(string));
        }

        public FeatureSet PopulateFromTable(Database pDbx)
        {
            this.dbx = pDbx;

            DataTable pTable = this.GetTable(sqlStatements.selectStreetNameSignsSQL);

            foreach (DataRow mRow in pTable.Rows)
            {
                var mXY = ExtFunctions.GetXYFromRow(mRow);
                if (mXY != null)
                {
                    var mPoint = new DotSpatial.Topology.Point(mXY[0], mXY[1]);
                    var mFeature = this.AddFeature(mPoint);

                    var mF = mFeature.DataRow;

                    mF.BeginEdit();

                    mF["SIGNTYPE"] = mRow["signType"];
                    mF["SERIALNUMBEROFSIGN"] = mRow["serialNumberOfSign"];
                    mF["DISTRICT_P1"] = mRow["district_id_p1"];
                    mF["STREETNAME_p1"] = mRow["STREETNAME_EN"];
                    mF["QR_CODE"] = this.GetQRCode(mRow, pTable);
                    
                    mF.EndEdit();
                }

            }
            return this;

        }

    }
}
