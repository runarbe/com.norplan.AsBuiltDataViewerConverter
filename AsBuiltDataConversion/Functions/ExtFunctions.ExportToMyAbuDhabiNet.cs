using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norplan.Adm.AsBuiltDataConversion.DataTypes;
using DotSpatial.Data;
using DotSpatial.Symbology;
using OSGeo.OGR;
using OSGeo.OSR;
using DotSpatial.Projections;
using System.IO;
using System.Globalization;
using System.Diagnostics;
using Norplan.Adm.AsBuiltDataConversion.FeatureTypes;
using DotSpatial.Controls;
using System.Windows.Forms;

namespace Norplan.Adm.AsBuiltDataConversion.Functions
{
    partial class ExtFunctions
    {

        public static void ExportMultipleToMyAbuDhabiNet(frmMain pFrm, string[] pInputFilenames, string pOutputFilename)
        {
            pFrm.Log("Starting to process selected files...");
            foreach (var mDbFilename in pInputFilenames)
            {
                var mDb = new Database(mDbFilename);
                var mAddressUnitFeatures = new AddressUnitFeature(mDb);
                mAddressUnitFeatures.PopulateFromTable();
                var mGroup = new MapGroup();
                mGroup.LegendText = mDb.dbBasename;
                IPointLayer mAddressUnitLayer = (IPointLayer)ExtFunctions.GetFeatureLayer(mGroup.Layers, mAddressUnitFeatures, LayerNames.AddressUnitSigns, MapSymbols.PointSymbol(SignColors.AddressUnitSign, 3), ExtFunctions.GetProjByEPSG(32640));
                ExtFunctions.ExportToMyAbuDhabiNet(pOutputFilename, mAddressUnitLayer, ExtFunctions.GetProjByEPSG(4326), ExtFunctions.GetProjByEPSG(4326), true);
                pFrm.Log("Completed parsing: " + mDbFilename);
                Application.DoEvents();
            }

            pFrm.Log("Wrote output to file: " + pOutputFilename);
        }


        public static ReturnValue ExportToMyAbuDhabiNet(string pSQLFilename, IPointLayer pANSLayer, ProjectionInfo pSrcProjection, ProjectionInfo pTgtProjection, Boolean pAppend = false)
        {
            NumberFormatInfo mNumFormat = new CultureInfo("en-US", false).NumberFormat;
            double[] mXY = new double[2];

            var mRV = new ReturnValue();

            var mTgtSRS = ExtFunctions.GetSpatialReferenceByEPSG(pTgtProjection.AuthorityCode);

            if (pSrcProjection != pTgtProjection)
            {
                pANSLayer.Reproject(pTgtProjection);
            }

            int
                mFeatureCount = pANSLayer.DataSet.Features.Count(),
                mBatchSize = 20,
                mBatchCounter = 1;

            var mValues = new List<String>();

            var mStreamWriter = new StreamWriter(pSQLFilename, pAppend, Encoding.UTF8, 1024);

            foreach (IFeature mFeature in pANSLayer.DataSet.Features)
            {

                var mGeom = OSGeo.OGR.Geometry.CreateFromWkb(mFeature.ToBinary());

                Envelope mEnvelope = new Envelope();

                mGeom.GetEnvelope(mEnvelope);
                string mWkt;
                mGeom.ExportToWkt(out mWkt);

                mValues.Add(String.Format("('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}','{9}','{10}',{11},{12})",
                    mFeature.DataRow["QR_CODE"],
                    "ADRPRINCIPALADDRESS",
                    "Sign description",
                    "Address unit sign",
                    mFeature.DataRow["ADDRESSUNITNR"],
                    "Street name description",
                    mFeature.DataRow["ROADNAME_EN"].ToString().MySQLEscape(),
                    "Street name description Arabic",
                    mFeature.DataRow["ROADNAME_AR"].ToString().MySQLEscape(),
                    mFeature.DataRow["DISTRICT_EN"].ToString().MySQLEscape(),
                    mFeature.DataRow["DISTRICT_AR"].ToString().MySQLEscape(),
                    mEnvelope.MinX.ToString(mNumFormat),
                    mEnvelope.MinY.ToString(mNumFormat)
                    ));

                if (mBatchCounter < mBatchSize)
                {
                    mBatchCounter++;
                }
                else
                {
                    //var mSql = String.Format("INSERT INTO signtest (p_uri, ft_table, s_desc, s_signtype, s_adr_unit_num, s_sname_desc, s_sname, s_sname_desc_ar, s_sname_ar, s_dname, s_dname_ar, loc_x, loc_y)"
                    //    + @" VALUES {0} ON DUPLICATE KEY UPDATE ft_table = VALUES(ft_table), s_desc = VALUES(s_desc), s_signtype = VALUES(s_signtype), s_adr_unit_num = VALUES(s_adr_unit_num), s_sname_desc = VALUES(s_sname_desc), s_sname = VALUES(s_sname), s_sname_desc_ar = VALUES(s_sname_desc_ar), s_sname_ar = VALUES(s_sname_ar), s_dname = VALUES(s_dname), s_dname_ar = VALUES(s_dname_ar), loc_x = VALUES(loc_x), loc_y = VALUES(loc_y);",
                    //    String.Join(",", mValues));
                    var mSql = String.Format(sqlStatements.insertUpdateMyAbuDhabiNetSQL,
                        String.Join(",", mValues));
                    mBatchCounter = 1;
                    mValues.Clear();
                    mStreamWriter.WriteLine(mSql);
                }

            }
            mStreamWriter.Flush();
            mStreamWriter.Close();
            return mRV;
        }
    }
}
