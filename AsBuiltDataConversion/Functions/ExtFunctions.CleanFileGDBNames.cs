using OSGeo.OGR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Norplan.Adm.AsBuiltDataConversion.TypeExtensions;
using OSGeo.GDAL;
using System.Diagnostics;
using System.Threading;

namespace Norplan.Adm.AsBuiltDataConversion.Functions
{
    partial class ExtFunctions
    {
        public static Boolean CleanFileGDBNames(frmMain pFrm, string pTgtFileGDB, string pSrcPGeoMDB, string pTgtFeatClass)
        {
            try
            {
                // Connect to database
                var mOdbcConn = new OdbcConnection("Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=" + pSrcPGeoMDB + ";Uid=Admin;Pwd=;");

                // Load road names
                pFrm.Log("Start loading names");

                var mRoadNames = new Dictionary<int, DataRow>();

                var mDataAdapter = new OdbcDataAdapter(sqlStatements.selectRoadNamesSQL, mOdbcConn);
                var mDataTable = new DataTable();
                mDataAdapter.Fill(mDataTable);

                int ctr1 = 0;
                int total1 = mDataTable.Rows.Count;

                foreach (DataRow mRow in mDataTable.Rows)
                {
                    int mRoadId;
                    if (int.TryParse(mRow["ADRROADID"].ToString(), out mRoadId))
                    {
                        mRoadNames.Add(mRoadId, mRow);
                    }
                    ctr1++;
                    if (ctr1 % 50 == 0)
                    {
                        Application.DoEvents();
                    }
                }
                pFrm.Log("Done loading road names");

                mOdbcConn.Close();
                mDataAdapter.Dispose();
                mDataAdapter = null;
                mOdbcConn.Dispose();
                mOdbcConn = null;

                // Load district names
                Gdal.SetConfigOption("SHAPE_ENCODING", "UTF-8");

                DataSource districtDataSource = Ogr.Open(DistrictImport.GetShapefileName(), 0);
                OSGeo.OGR.Layer districtLayer = districtDataSource.GetLayerByIndex(0);
                if (districtLayer == null)
                {
                    pFrm.Log("Could not load district names layer");
                    goto CleanUpDistricts;
                }

                pFrm.Log("Done loading district names");

                var mFileGDBDrv = OSGeo.OGR.Ogr.GetDriverByName("FileGDB");
                var mTgtDSrc = mFileGDBDrv.Open(pTgtFileGDB, 1);
                var mTgtLyr = mTgtDSrc.GetLayerByName(pTgtFeatClass);

                if (mTgtLyr == null)
                {
                    pFrm.Log("Could not open layer " + pTgtFeatClass + " for update");
                    goto CleanUp;
                }
                pFrm.Log("Opened target FileGDB for update");

                Feature mFeat = null;

                int mNumTgtFeats = mTgtLyr.GetFeatureCount(1);

                // Clean all existing district and street names
                pFrm.Log("Processing");

                for (int idxFeature = 0; idxFeature < mNumTgtFeats; idxFeature++)
                {
                    if (null != (mFeat = mTgtLyr.GetFeature(idxFeature)))
                    {
                        Geometry mGeometry = mFeat.GetGeometryRef();
                        double[] p = new double[2];
                        mGeometry.GetPoint(0, p);

                        if (pTgtFeatClass == "Address_unit_signs")
                        {
                            int mRoadID = mFeat.GetFieldAsInteger("ROADID");
                            mFeat.SetField("ROADNAME_EN", mRoadNames.GetStringOrNull(mRoadID, "NAMEENGLISH"));
                            mFeat.SetField("ROADNAME_AR", mRoadNames.GetStringOrNull(mRoadID, "NAMEARABIC"));
                            mFeat.SetField("ROADNAME_POP_EN", null);
                            mFeat.SetField("ROADNAME_POP_AR", null);
                            mFeat.SetField("DESCRIPTION_EN", mRoadNames.GetStringOrNull(mRoadID, "DESCRIPTIONENGLISH"));
                            mFeat.SetField("DESCRIPTION_AR", mRoadNames.GetStringOrNull(mRoadID, "DESCRIPTIONARABIC"));
                        }
                        else if (pTgtFeatClass == "Street_name_signs")
                        {
                            int mRoadID1 = mFeat.GetFieldAsInteger("ROADID_P1");
                            int mRoadID2 = mFeat.GetFieldAsInteger("ROADID_P2");
                            mFeat.SetField("ROADNAME_EN_P1", mRoadNames.GetStringOrNull(mRoadID1, "NAMEENGLISH"));
                            mFeat.SetField("ROADNAME_AR_P1", mRoadNames.GetStringOrNull(mRoadID1, "NAMEARABIC"));
                            mFeat.SetField("ROADNAME_EN_P2", mRoadNames.GetStringOrNull(mRoadID2, "NAMEENGLISH"));
                            mFeat.SetField("ROADNAME_AR_P2", mRoadNames.GetStringOrNull(mRoadID2, "NAMEARABIC"));
                        }
                        else if (pTgtFeatClass == "Address_guide_sign")
                        {
                            int mRoadID = mFeat.GetFieldAsInteger("ROADID");
                            string tmpRoadEn = mRoadNames.GetStringOrNull(mRoadID, "NAMEENGLISH");
                            string tmpRoadAr = mRoadNames.GetStringOrNull(mRoadID, "NAMEARABIC");
                            mFeat.SetField("ROADNAME_EN", tmpRoadEn);
                            mFeat.SetField("ROADNAME_AR", tmpRoadAr);
                        }

                        districtLayer.SetSpatialFilter(mGeometry);
                        using (Feature matchDistrict = districtLayer.GetFeature(0))
                        {
                            if (matchDistrict != null
                                && matchDistrict.IsFieldSet("NAMELATIN")
                                && matchDistrict.IsFieldSet("NAMEARABIC"))
                            {
                                string tmpDistEn = matchDistrict.GetFieldAsString("NAMELATIN");
                                string tmpDistAr = matchDistrict.GetFieldAsString("NAMEARABIC");
                                mFeat.SetField("DISTRICT_EN", tmpDistEn);
                                mFeat.SetField("DISTRICT_AR", tmpDistAr);
                            }
                            else
                            {
                                mFeat.SetField("DISTRICT_EN", null);
                                mFeat.SetField("DISTRICT_AR", null);
                                pFrm.Log("No district name match for " + pTgtFeatClass + " with coordinates: " + p[0].ToString() + ", " + p[1].ToString());
                            }
                        }
                        
                        Application.DoEvents();

                        mTgtLyr.SetFeature(mFeat);

                    DoEventsOnBatchSize:

                        if (idxFeature % 250 == 0 || idxFeature == mNumTgtFeats)
                        {
                            pFrm.Log("Processed " + idxFeature + " out of" + mNumTgtFeats + " features");
                            //Thread.Sleep(250);
                            mTgtLyr.SyncToDisk();
                        }

                        if (mGeometry != null)
                        {
                            mGeometry.Dispose();
                        }

                        if (mFeat != null)
                        {
                            mFeat.Dispose();
                        }

                    }

                }

            CleanUp:

                if (mTgtLyr != null)
                {
                    mTgtLyr.Dispose();
                }
                if (mTgtDSrc != null)
                {
                    mTgtDSrc.Dispose();
                }
                if (mFileGDBDrv != null)
                {
                    mFileGDBDrv.Dispose();
                }

            CleanUpDistricts:

                if (districtLayer != null)
                {
                    districtLayer.Dispose();
                }

                if (districtDataSource != null)
                {
                    districtDataSource.Dispose();
                }

            }
            catch (Exception ex)
            {
                pFrm.Log(ex.Message);
                pFrm.Log(ex.StackTrace);
                return false;
            }
            return true;
        }
    }
}
