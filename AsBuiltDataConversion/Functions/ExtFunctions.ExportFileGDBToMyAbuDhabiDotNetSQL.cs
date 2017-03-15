using DotSpatial.Controls;
using DotSpatial.Data;
using DotSpatial.Projections;
using Norplan.Adm.AsBuiltDataConversion.DataTypes;
using Norplan.Adm.AsBuiltDataConversion.FeatureTypes;
using Norplan.Adm.AsBuiltDataConversion.MyAbuDhabi;
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
        public static ReturnValue ExportFileGDBToMyAbuDhabiDotNetSQL(frmMain pFrm, string pSrcFileGDB, string pSQLFileName, int pBatchSize = 25, bool pAppend = false)
        {
            var mRetVal = new ReturnValue(true);

            NumberFormatInfo mNumFormat = new CultureInfo("en-US", false).NumberFormat;

            // Open FileGDB
            var mFileGDBDrv = OSGeo.OGR.Ogr.GetDriverByName("FileGDB");
            var mSrcDSrc = mFileGDBDrv.Open(pSrcFileGDB, 1);

            if (mSrcDSrc == null)
            {
                pFrm.Log("Could not open ESRI FGDB: " + pSrcFileGDB, true);
                mRetVal.Success = false;
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
            OSGeo.OGR.Feature mFeat = null;

            OSGeo.GDAL.Gdal.SetConfigOption("SHAPE_ENCODING", "UTF-8");
            DataSource mDistrictsDataSource = Ogr.Open(DistrictImport.GetShapefileName(), 0);
            Layer mDistricts = null;
            if (mDistrictsDataSource != null)
            {
                mDistricts = mDistrictsDataSource.GetLayerByIndex(0);
            }

            var mRecords = new SignRecord();

            // Process address unit signs
            var mAUSLyr = mSrcDSrc.GetLayerByName("Address_unit_signs");
            if (mAUSLyr == null)
            {
                mRetVal.AddMessage("Could not find address units layer", true);
                return mRetVal;
            }

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
                    var mDistrictInfo = mDistricts.GetDistrictByPoint(mGeom, 32640);

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
                        mDistrictInfo != null ? mDistrictInfo.GetFieldAsString("NAMELATIN").MySQLEscape() : "",
                        mDistrictInfo != null ? mDistrictInfo.GetFieldAsString("NAMEARABIC").MySQLEscape() : "",
                        mEnvelope.MinX.ToString(mNumFormat),
                        mEnvelope.MinY.ToString(mNumFormat),
                        mFeat.GetFieldAsString("DESCRIPTION_AR").MySQLEscape(),
                        mFeat.GetFieldAsString("DESCRIPTION_EN").MySQLEscape(),
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

            if (mSNSLyr == null)
            {
                mRetVal.AddMessage("Could not find layer Street_name_signs", true);
                return mRetVal;
            }

            if (mSNSLyr != null)
            {
                Debug.WriteLine(mSNSLyr);
                pFrm.Log("Opened Street_name_signs layer from FileGDB for reading...");

                int mNumFeatures = mSNSLyr.GetFeatureCount(1);
                int idxFeat = 0;
                mFeat = null;

                while (null != (mFeat = mSNSLyr.GetNextFeature()))
                {

                    var mGeom = mFeat.GetGeometryRef();
                    Envelope mOrigEnvelope = new Envelope();
                    mGeom.GetEnvelope(mOrigEnvelope);
                    var mDistrictInfo = mDistricts.GetDistrictByPoint(mGeom, 32640);

                    mGeom.Transform(mTransformation);
                    Envelope mEnvelope = new Envelope();
                    mGeom.GetEnvelope(mEnvelope);
                    string mWkt;
                    mGeom.ExportToWkt(out mWkt);

                    mRecords.AddToBuffer(
                        mFeat.GetFieldAsString("QR_CODE"),
                        "NULL",
                        mFeat.GetFieldAsString("ROADNAME_EN_P1").MySQLEscape() + "/" + mFeat.GetFieldAsString("ROADNAME_EN_P2").MySQLEscape(),
                        mFeat.GetFieldAsString("ROADNAME_AR_P1").MySQLEscape() + "/" + mFeat.GetFieldAsString("ROADNAME_AR_P2").MySQLEscape(),
                        mDistrictInfo != null ? mDistrictInfo.GetFieldAsString("NAMELATIN").MySQLEscape() : "",
                        mDistrictInfo != null ? mDistrictInfo.GetFieldAsString("NAMEARABIC").MySQLEscape() : "",
                        mEnvelope.MinX.ToString(mNumFormat),
                        mEnvelope.MinY.ToString(mNumFormat),
                        "",
                        "",
                        "Street_name_signs",
                        SignType.streetNameSign,
                        ""
                        );

                    idxFeat++;

                    if (idxFeat % pBatchSize == 0 || idxFeat == mNumFeatures)
                    {
                        var mSql = String.Format(sqlStatements.insertUpdateMyAbuDhabiNetSQL,
                            mRecords.GetBuffer());
                        mStreamWriter.WriteLine(mSql);
                        Debug.WriteLine(mSql);
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

                mSNSLyr.Dispose();
                mSNSLyr = null;

            }

            // Process address guide signs
            var mAGSLyr = mSrcDSrc.GetLayerByName("Address_guide_sign");

            if (mAGSLyr == null)
            {
                mRetVal.AddMessage("Could not find layer Address_guide_sign", true);
                return mRetVal;
            }

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
                    var mDistrictInfo = mDistricts.GetDistrictByPoint(mGeom, 32640);

                    mGeom.Transform(mTransformation);
                    Envelope mEnvelope = new Envelope();
                    mGeom.GetEnvelope(mEnvelope);
                    string mWkt;
                    mGeom.ExportToWkt(out mWkt);

                    mRecords.AddToBuffer(
                        mFeat.GetFieldAsString("QR_CODE"),
                        "NULL",
                        mFeat.GetFieldAsString("ROADNAME_EN").MySQLEscape(),
                        mFeat.GetFieldAsString("ROADNAME_AR").MySQLEscape(),
                        mDistrictInfo != null ? mDistrictInfo.GetFieldAsString("NAMELATIN").MySQLEscape() : null,
                        mDistrictInfo != null ? mDistrictInfo.GetFieldAsString("NAMEARABIC").MySQLEscape() : null,
                        mEnvelope.MinX.ToString(mNumFormat),
                        mEnvelope.MinY.ToString(mNumFormat),
                        "",
                        "",
                        "Address_guide_sign",
                        SignType.addressGuideSign,
                        "");

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