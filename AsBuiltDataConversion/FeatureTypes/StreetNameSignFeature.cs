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
    class StreetNameSignFeature : AdmAdrFeature
    {
        public StreetNameSignFeature(Database pDbx)
        {
            this.dbx = pDbx;

            // Set feature type
            FeatureType = DotSpatial.Topology.FeatureType.Point;
            AddCol("STREETNAMESIGNID", typeof(int));
            AddCol("QR_CODE", typeof(string), 500);
            AddCol("SERIALNUMBER", typeof(string), 50);
            AddCol("SIGNTYPE", typeof(string), 50);
            AddCol("AUNRANGE_P1", typeof(string), 200);
            AddCol("ROADID_P1", typeof(int));
            AddCol("DISTRICTID_P1", typeof(string), 100);
            AddCol("AUNRANGE_P2", typeof(string), 200);
            AddCol("ROADID_P2", typeof(int));
            AddCol("DISTRICTID_P2", typeof(string), 100);
        }

        public FeatureSet PopulateFromTable()
        {
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

                    mF["STREETNAMESIGNID"] = mRow["id"];
                    mF["QR_CODE"] = this.GetQRCode(mRow, pTable);
                    mF["SERIALNUMBER"] = mRow["serialNumberOfSign"];
                    mF["SIGNTYPE"] = mRow["signType"];
                    mF["AUNRANGE_P1"] = mRow["addressUnitRange_p1"];
                    mF["ROADID_P1"] = mRow["road_id_p1"];
                    mF["DISTRICTID_P1"] = mRow["district_id_p1"];
                    mF["AUNRANGE_P2"] = mRow["addressUnitRange_p2"];
                    mF["ROADID_P2"] = mRow["road_id_p2"];
                    mF["DISTRICTID_P2"] = mRow["district_id_p2"];
                    mF.EndEdit();
                }

            }
            return this;

        }

    }
}
