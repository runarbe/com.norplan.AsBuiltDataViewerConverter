using OSGeo.OGR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Norplan.Adm.AsBuiltDataConversion.TypeExtensions;

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

                string mSql = "SELECT ADRROADID, NAMEENGLISH, NAMEARABIC, NAMEPOPULARENGLISH, NAMEPOPULARARABIC, ROADTYPE, ADRDISTRICTID, APPROVED FROM ADM_ADRROAD WHERE APPROVED = 1";

                var mRoadNames = new Dictionary<int, DataRow>();

                var mDataAdapter = new OdbcDataAdapter(mSql, mOdbcConn);
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

                // Load district names
                var mDistNames = new Dictionary<string, DataRow>();
                var mSql2 = "SELECT \"ABBREVIATION\", \"NAMEENGLISH\", \"NAMEARABIC\" FROM ADRDISTRICT WHERE \"APPROVED\" = 'Y'";

                var mDataAdapter2 = new OdbcDataAdapter(mSql2, mOdbcConn);
                var mDataTable2 = new DataTable();
                mDataAdapter2.Fill(mDataTable2);
                
                ctr1 = 0;
                total1 = mDataTable2.Rows.Count;

                foreach (DataRow mRow in mDataTable2.Rows)
                {
                    string mDistAbbrev = mRow["ABBREVIATION"].ToString().ToLower().Trim();
                    if (!String.IsNullOrEmpty(mDistAbbrev))
                    {
                        mDistNames.Add(mDistAbbrev, mRow);
                    }
                    ctr1++;
                    if (ctr1 % 50 == 0)
                    {
                        Application.DoEvents();
                    }
                }

                pFrm.Log("Done loading district names");

                mOdbcConn.Close();
                mDataAdapter = null;
                mOdbcConn = null;

                var mFileGDBDrv = OSGeo.OGR.Ogr.GetDriverByName("FileGDB");
                var mTgtDSrc = mFileGDBDrv.Open(pTgtFileGDB, 1);
                var mTgtLyr = mTgtDSrc.GetLayerByName(pTgtFeatClass);

                if (mTgtLyr != null)
                {
                    pFrm.Log("Opened target FileGDB for update");
                }

                Feature mFeat = null;

                int mNumTgtFeats = mTgtLyr.GetFeatureCount(1);

                // Clean all existing district and street names
                pFrm.Log("Processing");
                int idxDelete = 0;
                while (null != (mFeat = mTgtLyr.GetNextFeature()))
                {
                    int mRoadID = mFeat.GetFieldAsInteger("ROADID");
                    string mDistrictID = mFeat.GetFieldAsString("DISTRICTID").ToLower().Trim();

                    mFeat.SetField("ROADNAME_EN", mRoadNames.GetStringOrNull(mRoadID, "NAMEENGLISH"));
                    mFeat.SetField("ROADNAME_AR", mRoadNames.GetStringOrNull(mRoadID, "NAMEARABIC"));
                    mFeat.SetField("ROADNAME_POP_EN", null);
                    mFeat.SetField("ROADNAME_POP_AR", null);
                    mFeat.SetField("DISTRICT_EN", mDistNames.GetStringOrNull(mDistrictID, "NAMEENGLISH"));
                    mFeat.SetField("DISTRICT_AR", mDistNames.GetStringOrNull(mDistrictID, "NAMEARABIC"));

                    mTgtLyr.SetFeature(mFeat);
                    idxDelete++;
                    if (idxDelete % 500 == 0 || idxDelete == mNumTgtFeats)
                    {
                        pFrm.Log("Processed " + idxDelete + " out of" + mNumTgtFeats + " features");
                        Application.DoEvents();
                    }

                }

                mTgtLyr.Dispose();
                mTgtDSrc.Dispose();
                mFileGDBDrv.Dispose();

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
