using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Norplan.Adm.AsBuiltDataConversion.Functions;
using DotSpatial;
using DotSpatial.Data;
using DotSpatial.Topology;

namespace Norplan.Adm.AsBuiltDataConversion.FeatureTypes
{
    class AddressGuideSignFeature : AdmAdrFeature
    {
        public AddressGuideSignFeature(Database pDbx)
        {
            this.dbx = pDbx;

            // Set feature type
            this.FeatureType = DotSpatial.Topology.FeatureType.Point;
            AddCol("ADDRESSGUIDESIGNID", typeof(int));
            AddCol("QR_CODE", typeof(string), 500);
            AddCol("SIGNTYPE", typeof(string), 50);
            AddCol("AUNRANGE", typeof(string), 200);
            AddCol("ROADID", typeof(int));
            AddCol("DISTRICTID", typeof(string), 100);
        }


        public FeatureSet PopulateFromTable()
        {
            DataTable pTable = this.GetTable(sqlStatements.selectAddressGuideSignsSQL);

            foreach (DataRow mRow in pTable.Rows)
            {
                var mXY = ExtFunctions.GetXYFromRow(mRow);
                if (mXY != null)
                {
                    var mPoint = new DotSpatial.Topology.Point(mXY[0], mXY[1]);
                    var mFeature = this.AddFeature(mPoint);

                    mFeature.DataRow.BeginEdit();

                    mFeature.DataRow["ADDRESSGUIDESIGNID"] = mRow["id"];
                    mFeature.DataRow["QR_CODE"] = this.GetQRCode(mRow, pTable);
                    mFeature.DataRow["SIGNTYPE"] = mRow["signType"];
                    mFeature.DataRow["AUNRANGE"] = mRow["serialNumberOfSign"];
                    mFeature.DataRow["ROADID"] = mRow["road_id"];
                    mFeature.DataRow["DISTRICTID"] = mRow["district_id"];
                    mFeature.DataRow.EndEdit();
                }
            }

            return this;

        }


    }
}
