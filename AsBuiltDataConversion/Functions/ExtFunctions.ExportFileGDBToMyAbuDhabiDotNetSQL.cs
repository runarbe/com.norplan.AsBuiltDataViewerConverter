using DotSpatial.Controls;
using DotSpatial.Projections;
using Norplan.Adm.AsBuiltDataConversion.DataTypes;
using Norplan.Adm.AsBuiltDataConversion.FeatureTypes;
using OSGeo.OGR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Norplan.Adm.AsBuiltDataConversion.Functions
{
    partial class ExtFunctions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pFrm"></param>
        /// <param name="pSrcFileGDB"></param>
        /// <param name="pSQLFileName"></param>
        /// <param name="pBatchSize"></param>
        /// <param name="pAppend"></param>
        /// <returns></returns>
        public static RetVal ExportFileGDBToMyAbuDhabiDotNetSQL(frmMain pFrm, string pSrcFileGDB, string pSQLFileName, int pBatchSize = 25, bool pAppend = false)
        {
            var mRetVal = new RetVal(RetValStatus.success, null);

            NumberFormatInfo mNumFormat = new CultureInfo("en-US", false).NumberFormat;

            // Open FileGDB
            var mFileGDBDrv = OSGeo.OGR.Ogr.GetDriverByName("FileGDB");
            var mSrcDSrc = mFileGDBDrv.Open(pSrcFileGDB, 1);

            if (mSrcDSrc == null)
            {
                pFrm.Log("Could not open ESRI FGDB: " + pSrcFileGDB, true);
                mRetVal.status = RetValStatus.failure;
                return mRetVal;
            }

            // Create re-usable projection
            var mSrcProj = ExtFunctions.GetSpatialReferenceByEPSG(32640);
            var mTgtProj = ExtFunctions.GetSpatialReferenceByEPSG(4326);
            var mTransformation = new OSGeo.OSR.CoordinateTransformation(mSrcProj, mTgtProj);

            int mLineCount = 0;

            int mFileCount = 1;

            // Create file to write to
            var mStreamWriter = new StreamWriter(pSQLFileName + "." + mFileCount, pAppend, Encoding.UTF8, 1024);

            // Create re-usable feature object
            Feature mFeat = null;

            // Create re-usable districts object to ext
            IMapLayer mDistricts = ExtFunctions.GetLayerByName(pFrm.theMap, "Districts");
            if (mDistricts == null)
            {
                pFrm.Log("Could not find district layer");
                return mRetVal.Failure();
            }

            mDistricts.DataSet.Reproject(KnownCoordinateSystems.Projected.UtmWgs1984.WGS1984UTMZone40N);

            var mRecords = new MyAbuDhabiDotNetRecord();


            // Process address unit signs
            var mAUSLyr = mSrcDSrc.GetLayerByName("Address_unit_signs");

            if (mAUSLyr != null)
            {
                pFrm.Log("Opened Address_unit_signs layer from FileGDB for reading...");

                int mNumFeatures = mAUSLyr.GetFeatureCount(1);
                int idxFeat = 0;

                while (null != (mFeat = mAUSLyr.GetNextFeature()))
                {

                    var mGeom = mFeat.GetGeometryRef();
                    Envelope mOrigEnvelope = new Envelope();
                    mGeom.GetEnvelope(mOrigEnvelope);
                    var mDistrictInfo = mDistricts.GetDistrictByPoint(mOrigEnvelope.MinX, mOrigEnvelope.MinY);

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
                        mDistrictInfo[0].MySQLEscape(),
                        mDistrictInfo[1].MySQLEscape(),
                        mEnvelope.MinX.ToString(mNumFormat),
                        mEnvelope.MinY.ToString(mNumFormat),
                        "",
                        "",
                        "Address_unit_signs",
                        SignType.addressUnitNumberSign
                        );

                    idxFeat++;

                    if (idxFeat % pBatchSize == 0 || idxFeat == mNumFeatures)
                    {
                        var mSql = String.Format(sqlStatements.insertUpdateMyAbuDhabiNetSQL,
                            mRecords.GetBuffer());
                        mStreamWriter.WriteLine(mSql);
                        pFrm.pgBar.ProgressBar.Value = (idxFeat * 100) / mNumFeatures;
                        mLineCount++;
                        if (mLineCount % 500 == 0)
                        {
                            mFileCount++;
                            mStreamWriter.Flush();
                            mStreamWriter.Close();
                            mStreamWriter = new StreamWriter(pSQLFileName + "." + mFileCount, pAppend, Encoding.UTF8, 1024);
                            mLineCount = 0;
                        }
                    }



                }

                pFrm.Log("Done, processed " + idxFeat + " features...");

                mAUSLyr.Dispose();
                mAUSLyr = null;
            }

            // Process address unit signs
            var mSNSLyr = mSrcDSrc.GetLayerByName("Street_name_signs");

            if (mSNSLyr != null)
            {
                pFrm.Log("Opened Street_name_signs layer from FileGDB for reading...");

                int mNumFeatures = mSNSLyr.GetFeatureCount(1);
                int idxFeat = 0;
                mFeat = null;

                while (null != (mFeat = mSNSLyr.GetNextFeature()))
                {

                    var mGeom = mFeat.GetGeometryRef();
                    Envelope mOrigEnvelope = new Envelope();
                    mGeom.GetEnvelope(mOrigEnvelope);
                    var mDistrictInfo = mDistricts.GetDistrictByPoint(mOrigEnvelope.MinX, mOrigEnvelope.MinY);

                    mGeom.Transform(mTransformation);
                    Envelope mEnvelope = new Envelope();
                    mGeom.GetEnvelope(mEnvelope);
                    string mWkt;
                    mGeom.ExportToWkt(out mWkt);

                    mRecords.AddToBuffer(
                        mFeat.GetFieldAsString("QR_CODE"),
                        "",
                        "",
                        "",
                        mDistrictInfo[0].MySQLEscape(),
                        mDistrictInfo[1].MySQLEscape(),
                        mEnvelope.MinX.ToString(mNumFormat),
                        mEnvelope.MinY.ToString(mNumFormat),
                        "",
                        "",
                        "Street_name_signs",
                        SignType.streetNameSign
                        );

                    idxFeat++;

                    if (idxFeat % pBatchSize == 0 || idxFeat == mNumFeatures)
                    {
                        var mSql = String.Format(sqlStatements.insertUpdateMyAbuDhabiNetSQL,
                            mRecords.GetBuffer());
                        mStreamWriter.WriteLine(mSql);
                        pFrm.pgBar.ProgressBar.Value = (idxFeat * 100) / mNumFeatures;
                    }

                }

                pFrm.Log("Done, processed " + idxFeat + " features...");

                mSNSLyr.Dispose();
                mSNSLyr = null;

            }

            // Process address guide signs
            var mAGSLyr = mSrcDSrc.GetLayerByName("Address_guide_sign");

            if (mAGSLyr != null)
            {
                pFrm.Log("Opened Address_guide_sign layer from FileGDB for reading...");

                int mNumFeatures = mAGSLyr.GetFeatureCount(1);
                int idxFeat = 0;
                mFeat = null;

                while (null != (mFeat = mAGSLyr.GetNextFeature()))
                {

                    var mGeom = mFeat.GetGeometryRef();
                    Envelope mOrigEnvelope = new Envelope();
                    mGeom.GetEnvelope(mOrigEnvelope);
                    var mDistrictInfo = mDistricts.GetDistrictByPoint(mOrigEnvelope.MinX, mOrigEnvelope.MinY);

                    mGeom.Transform(mTransformation);
                    Envelope mEnvelope = new Envelope();
                    mGeom.GetEnvelope(mEnvelope);
                    string mWkt;
                    mGeom.ExportToWkt(out mWkt);

                    mRecords.AddToBuffer(
                        mFeat.GetFieldAsString("QR_CODE"),
                        "",
                        "",
                        "",
                        mDistrictInfo[0].MySQLEscape(),
                        mDistrictInfo[1].MySQLEscape(),
                        mEnvelope.MinX.ToString(mNumFormat),
                        mEnvelope.MinY.ToString(mNumFormat),
                        "",
                        "",
                        "Address_guide_sign",
                        SignType.addressGuideSign
                        );

                    idxFeat++;

                    if (idxFeat % pBatchSize == 0 || idxFeat == mNumFeatures)
                    {
                        var mSql = String.Format(sqlStatements.insertUpdateMyAbuDhabiNetSQL,
                            mRecords.GetBuffer());
                        mStreamWriter.WriteLine(mSql);
                        pFrm.pgBar.ProgressBar.Value = (idxFeat * 100) / mNumFeatures;
                    }

                }

                pFrm.Log("Done, processed " + idxFeat + " features...");

                mAGSLyr.Dispose();
                mAGSLyr = null;
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

            mSrcDSrc.Dispose();
            mSrcDSrc = null;

            mFileGDBDrv.Dispose();
            mFileGDBDrv = null;

            return mRetVal;

        }
    }
}