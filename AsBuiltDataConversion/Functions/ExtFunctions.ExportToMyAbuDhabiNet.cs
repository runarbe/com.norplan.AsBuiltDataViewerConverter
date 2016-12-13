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
            // Overwrite first entry
            bool pAppend = false;

            pFrm.Log("Starting to process selected files...");
            foreach (var mDbFilename in pInputFilenames)
            {
                var mDb = new Database(mDbFilename);

                var mGroup = new MapGroup();
                mGroup.LegendText = mDb.DbBaseName;

                var mAddressUnitFeatures = new AddressUnitFeature(mDb);
                mAddressUnitFeatures.PopulateFromTable();

                pFrm.Log("Number of address unit features in source: " + mAddressUnitFeatures.NumRows());

                IPointLayer mAddressUnitLayer = (IPointLayer)ExtFunctions.GetFeatureLayer(mGroup.Layers, mAddressUnitFeatures, LayerNames.AddressUnitSigns, MapSymbols.PointSymbol(SignColors.AddressUnitSign, 3), ExtFunctions.GetProjByEPSG(32640));
                var mAddressUnitResult = ExtFunctions.ExportToMyAbuDhabiNet(pOutputFilename, mAddressUnitLayer, ExtFunctions.GetProjByEPSG(4326), ExtFunctions.GetProjByEPSG(4326), pAppend, SignType.addressUnitNumberSign);
                pFrm.Log(mAddressUnitResult.GetMessages());

                //After first file-write, set append to true
                pAppend = true;

                var mSnsFeatures = new StreetNameSignFeature(mDb);
                mSnsFeatures.PopulateFromTable();
                pFrm.Log("Number of street name sign features in source: " + mSnsFeatures.NumRows());
                IPointLayer mSnsLayer = (IPointLayer)ExtFunctions.GetFeatureLayer(mGroup.Layers, mSnsFeatures, LayerNames.StreetNameSigns, MapSymbols.PointSymbol(SignColors.StreetNameSign, 3), ExtFunctions.GetProjByEPSG(32640));
                var mSnsResult = ExtFunctions.ExportToMyAbuDhabiNet(pOutputFilename, mSnsLayer, ExtFunctions.GetProjByEPSG(4326), ExtFunctions.GetProjByEPSG(4326), true, SignType.streetNameSign);
                pFrm.Log(mSnsResult.GetMessages());

                var mAgsFeatures = new AddressGuideSignFeature(mDb);
                mAgsFeatures.PopulateFromTable();
                pFrm.Log("Number of address guide sign features in source: " + mAgsFeatures.NumRows());
                IPointLayer mAgsLayer = (IPointLayer)ExtFunctions.GetFeatureLayer(mGroup.Layers, mAgsFeatures, LayerNames.AddressGuideSigns, MapSymbols.PointSymbol(SignColors.AddressGuideSign, 3), ExtFunctions.GetProjByEPSG(32640));
                var mAgsResult = ExtFunctions.ExportToMyAbuDhabiNet(pOutputFilename, mAgsLayer, ExtFunctions.GetProjByEPSG(4326), ExtFunctions.GetProjByEPSG(4326), true, SignType.addressGuideSign);
                pFrm.Log(mAgsResult.GetMessages());

                pFrm.Log("Completed parsing: " + mDbFilename);
                Application.DoEvents();
            }

            pFrm.Log("Wrote output to file: " + pOutputFilename);
        }


        public static ReturnValue ExportToMyAbuDhabiNet(string pSQLFilename, IPointLayer pLayer, ProjectionInfo pSrcProjection, ProjectionInfo pTgtProjection, Boolean pAppend = false, string pSignType = null)
        {
            NumberFormatInfo mNumFormat = new CultureInfo("en-US", false).NumberFormat;
            double[] mXY = new double[2];

            var mRV = new ReturnValue();

            var mTgtSRS = ExtFunctions.GetSpatialReferenceByEPSG(pTgtProjection.AuthorityCode);

            if (pSrcProjection != pTgtProjection)
            {
                pLayer.Reproject(pTgtProjection);
            }

            int
                mFeatureCount = pLayer.DataSet.Features.Count(),
                mBatchSize = 20,
                mBatchCounter = 1,
                mWriteCounter = 0;

            var mValues = new List<String>();

            var mStreamWriter = new StreamWriter(pSQLFilename, pAppend, Encoding.UTF8, 1024);

            foreach (IFeature mFeature in pLayer.DataSet.Features)
            {
                var mGeom = OSGeo.OGR.Geometry.CreateFromWkb(mFeature.ToBinary());

                Envelope mEnvelope = new Envelope();

                mGeom.GetEnvelope(mEnvelope);
                string mWkt;
                mGeom.ExportToWkt(out mWkt);

                if (pSignType == SignType.addressUnitNumberSign)
                {
                    mValues.Add(String.Format("('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}','{9}','{10}',{11},{12})",
                        mFeature.DataRow["QR_CODE"],
                        "ANS",
                        "Sign description",
                        SignType.addressUnitNumberSign,
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
                    mWriteCounter++;
                }
                else if (pSignType == SignType.streetNameSign)
                {
                    mValues.Add(String.Format("('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}','{9}','{10}',{11},{12})",
                        mFeature.DataRow["QR_CODE"],
                        "SNS",
                        "Sign description",
                        SignType.streetNameSign,
                        "null",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        mEnvelope.MinX.ToString(mNumFormat),
                        mEnvelope.MinY.ToString(mNumFormat)
                        ));
                    mWriteCounter++;
                }
                else if (pSignType == SignType.addressGuideSign)
                {
                    mValues.Add(String.Format("('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}','{9}','{10}',{11},{12})",
                        mFeature.DataRow["QR_CODE"],
                        "AGS",
                        "Sign description",
                        SignType.addressGuideSign,
                        "null",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        mEnvelope.MinX.ToString(mNumFormat),
                        mEnvelope.MinY.ToString(mNumFormat)
                        ));
                    mWriteCounter++;
                }


                if (mBatchCounter++ < mBatchSize)
                {
                    mBatchCounter++;
                }
                else
                {
                    var mSql = String.Format(sqlStatements.insertUpdateMyAbuDhabiNetSQL,
                        String.Join(",", mValues));
                    mBatchCounter = 1;
                    mValues.Clear();
                    mStreamWriter.WriteLine(mSql);
                }

            }
            mRV.AddMessage("Parsed " + mWriteCounter + " features to SQL");
            mStreamWriter.Flush();
            mStreamWriter.Close();
            return mRV;
        }

        public static ReturnValue ExportDistrictsToMyAbuDhabiNet()
        {
            var r = new ReturnValue();

            OSGeo.GDAL.Gdal.SetConfigOption("SHAPE_ENCODING", "UTF-8");

            var shpDir = Path.GetFileNameWithoutExtension(DistrictImport.GetShapefileName());
            var ds = Ogr.OpenShared(DistrictImport.GetShapefileName(), 0);
            if (ds == null)
            {
                r.AddMessage("Data source doesn't exist", true);
                return r;
            }

            var lyr = ds.GetLayerByIndex(0);
            if (lyr == null)
            {
                r.AddMessage("Layer doesn't destrict", true);
                return r;
            }

            OSGeo.OGR.Feature nf;

            var srcProj = new SpatialReference(null);
            var tgtProj = new SpatialReference(null);
            srcProj.ImportFromEPSG(32640);
            tgtProj.ImportFromEPSG(4326);

            var trans = new CoordinateTransformation(srcProj, tgtProj);

            string geomWkt;

            Geometry geometry;
            string districtname_ar;
            string districtname_en;
            string districtabbreviation;

            StringBuilder sql = new StringBuilder();

            //sql.AppendLine(sqlStatements.createDistrictTableSQL);
            sql.AppendLine(sqlStatements.emptyDistrictsSQL);

            while (null != (nf = lyr.GetNextFeature())) {

                geometry = nf.GetGeometryRef();

                // Transformation goes here
                geometry.Transform(trans);

                geometry.ExportToWkt(out geomWkt);

                districtabbreviation = nf.GetFieldAsString("DISTRICTAB");
                districtname_ar = nf.GetFieldAsString("NAMEARABIC");
                districtname_en = nf.GetFieldAsString("NAMELATIN");
                sql.AppendFormat(sqlStatements.insertDistrictsSQL, districtname_ar.MySQLEscape(), districtname_en.MySQLEscape(), geomWkt, districtabbreviation); 

            }

            var dlg = new SaveFileDialog();
            dlg.FileName = "districts.sql";
            dlg.Filter = "SQL files|*.sql";
            dlg.Title = "Select where to save districts SQL";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(dlg.FileName, sql.ToString());
                r.Success = true;
            }

            return r;
        }
    }
}
