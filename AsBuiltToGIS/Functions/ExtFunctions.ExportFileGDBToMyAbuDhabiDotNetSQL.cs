using AsBuiltToGIS.FeatureTypes;
using OSGeo.OGR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace AsBuiltToGIS.Functions
{
    partial class ExtFunctions
    {

        public static RetVal ExportFileGDBToMyAbuDhabiDotNetSQL(frmMain pFrm, string pSrcFileGDB, string pSrcFeatClass, string pSQLFileName, int pBatchSize = 25, bool pAppend = false)
        {
            var mRetVal = new RetVal(RetValStatus.success, null);

            // Open FileGDB
            var mFileGDBDrv = OSGeo.OGR.Ogr.GetDriverByName("FileGDB");
            var mSrcDSrc = mFileGDBDrv.Open(pSrcFileGDB, 1);
            var mSrcLyr = mSrcDSrc.GetLayerByName(pSrcFeatClass);

            if (mSrcLyr != null)
            {
                pFrm.Log("Opened source FileGDB for reading");
            }

            // Loop, create statements, write file

            var mRecords = new MyAbuDhabiDotNetRecord();

            var mStreamWriter = new StreamWriter(pSQLFileName, pAppend, Encoding.UTF8, 1024);

            Feature mFeat = null;

            int mNumFeatures = mSrcLyr.GetFeatureCount(1);
            int idxFeat = 0;
            NumberFormatInfo mNumFormat = new CultureInfo("en-US", false).NumberFormat;

            var mSrcProj = ExtFunctions.GetSpatialReferenceByEPSG(32640);
            var mTgtProj = ExtFunctions.GetSpatialReferenceByEPSG(4326);
            var mTransformation = new OSGeo.OSR.CoordinateTransformation(mSrcProj, mTgtProj);

            while (null != (mFeat = mSrcLyr.GetNextFeature()))
            {

                var mGeom = mFeat.GetGeometryRef();
                mGeom.Transform(mTransformation);
                Envelope mEnvelope = new Envelope();
                mGeom.GetEnvelope(mEnvelope);
                string mWkt;
                mGeom.ExportToWkt(out mWkt);

                mRecords.AddToBuffer(
                    mFeat.GetFieldAsString("QR_CODE"),
                    mFeat.GetFieldAsString("ADDRESSUNITNR"),
                    mFeat.GetFieldAsString("ROADNAME_EN").MySQLEscape(),
                    mFeat.GetFieldAsString("ROADNAME_AR").MySQLEscape(),
                    mFeat.GetFieldAsString("DISTRICT_EN").MySQLEscape(),
                    mFeat.GetFieldAsString("DISTRICT_AR").MySQLEscape(),
                    mEnvelope.MinX.ToString(mNumFormat),
                    mEnvelope.MinY.ToString(mNumFormat)
                    );

                idxFeat++;

                if (idxFeat % pBatchSize == 0 || idxFeat == mNumFeatures)
                {
                    var mSql = String.Format(sqlStatements.insertUpdateMyAbuDhabiNetSQL,
                        mRecords.GetBuffer());
                    mStreamWriter.WriteLine(mSql);
                }

            }
            mStreamWriter.Flush();
            mStreamWriter.Close();
            mStreamWriter.Dispose();

            mSrcProj.Dispose();
            mSrcProj = null;

            mTgtProj.Dispose();
            mTgtProj = null;

            mTransformation.Dispose();
            mTransformation = null;

            mSrcLyr.Dispose();
            mSrcLyr = null;
            
            mSrcDSrc.Dispose();
            mSrcDSrc = null;
            
            mFileGDBDrv.Dispose();
            mFileGDBDrv = null;

            return mRetVal;
        }

    }
}
