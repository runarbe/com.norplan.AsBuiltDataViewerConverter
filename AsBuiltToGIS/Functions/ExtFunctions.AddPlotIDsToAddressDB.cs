using OSGeo.OGR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AsBuiltToGIS.Functions
{
    partial class ExtFunctions
    {
        /// <summary>
        /// Transfers attributes from plot layer to address unit layer based on spatial intersection
        /// </summary>
        /// <param name="pFrm">The main application form</param>
        /// <param name="pTargetFileGDB">The filename of a FileGDB with addressing data</param>
        /// <param name="pSrcShapefile">The filename of a Shapefile with plot polygons</param>
        /// <param name="pFldsToCopy">A dictionary of source=>target field names</param>
        /// <param name="pMatchColPrefix">The prefix to assign to the match column</param>
        /// <param name="pBufSteps">An array of buffer steps</param>
        /// <param name="pFeatClassNm">The name of the feature class in the target FileGDB</param>
        /// <returns>True on success, false on error</returns>
        public static bool AddPlotIDsToAddressDB(frmMain pFrm,
            string pTargetFileGDB,
            string pSrcShapefile,
            Dictionary<string, string> pFldsToCopy,
            string pMatchColPrefix,
            double[] pBufSteps,
            string pFeatClassNm
            )
        {

            string mMatchColNm = pMatchColPrefix + "_MATCH";

            try
            {

                var mFileGDBDrv = OSGeo.OGR.Ogr.GetDriverByName("FileGDB");

                var mTgtDSrc = mFileGDBDrv.Open(pTargetFileGDB, 1);
                var mTgtLyr = mTgtDSrc.GetLayerByName(pFeatClassNm);
                if (mTgtLyr != null)
                {
                    pFrm.Log("Success opening target FileGDB");

                    DataSource mSrcDSrc = Ogr.Open(pSrcShapefile, 0);
                    Layer mSrcLyr = mSrcDSrc.GetLayerByIndex(0);
                    if (mSrcLyr != null)
                    {
                        pFrm.Log("Success opening source layer");
                    }

                    foreach (string mSrcFieldName in pFldsToCopy.Keys)
                    {
                        if (!mTgtLyr.HasField(pFldsToCopy[mSrcFieldName]) && mSrcLyr.HasField(mSrcFieldName))
                        {
                            //var mFieldDefn = mSrcLyr.GetNextFeature().GetFieldDefnRef(mSrcFieldName);
                            var mFieldDefn = new FieldDefn(pFldsToCopy[mSrcFieldName], FieldType.OFTString);
                            mFieldDefn.SetWidth(255);
                            mTgtLyr.CreateField(mFieldDefn, 1);
                        }
                    }

                    if (!mTgtLyr.HasField(mMatchColNm))
                    {
                        var mFieldDefn = new FieldDefn(mMatchColNm, FieldType.OFTString);
                        mFieldDefn.SetWidth(100);
                        mTgtLyr.CreateField(mFieldDefn, 1);
                    }

                    mTgtLyr.ResetReading();
                    Feature mFeat = null;
                    int idxAdr = 0;
                    int idxHit = 0;
                    int idxPass = 0;

                    mTgtLyr.StartTransaction();

                    for (int idxStep = 0; idxStep < pBufSteps.Length; idxStep++)
                    {
                        double mBuf = pBufSteps[idxStep];

                        idxPass++;
                        int idxAdrInner = 0;
                        mTgtLyr.SetAttributeFilter(mMatchColNm + " ='' OR " + mMatchColNm + " IS null");
                        mTgtLyr.ResetReading();
                        int mAdrCount = mTgtLyr.GetFeatureCount(1);
                        pFrm.Log("Pass #" + idxPass + " (" + mBuf + "m): " + mAdrCount + " features...");

                        while (null != (mFeat = mTgtLyr.GetNextFeature()))
                        {
                            Geometry mGeom;
                            if (mBuf == 0)
                            {
                                mGeom = mFeat.GetGeometryRef();
                            }
                            else
                            {
                                mGeom = mFeat.GetGeometryRef().Buffer(mBuf, 0);
                            }

                            mSrcLyr.ResetReading();
                            mSrcLyr.SetSpatialFilter(mGeom);
                            Feature mHit = mSrcLyr.GetNextFeature();
                            int mPlotCount = mSrcLyr.GetFeatureCount(1);
                            if (mHit != null && mPlotCount <= 2)
                            {
                                bool mUpd = false;

                                if (mPlotCount < 2)
                                {
                                    foreach (string mFieldName in pFldsToCopy.Keys)
                                    {
                                        var mFieldVal = mHit.GetFieldAsString(mFieldName);
                                        if (mFieldVal != null && mFieldVal.Length < 255)
                                        {
                                            mFeat.SetField(pFldsToCopy[mFieldName], mFieldVal);
                                            mUpd = true;
                                        }
                                    }

                                }
                                else
                                {
                                    mUpd = true;
                                }

                                if (mUpd)
                                {
                                    mFeat.SetField(pMatchColPrefix + "_MATCH", mPlotCount.ToString("00") + " hits within " + mBuf.ToString("00.00") + "m");
                                    mTgtLyr.SetFeature(mFeat);
                                    mUpd = false;
                                }
                                idxHit++;
                            }

                            idxAdr++;
                            idxAdrInner++;

                            if (idxAdrInner % 500 == 0 || idxAdrInner == mAdrCount)
                            {
                                pFrm.Log(String.Format("Processed: {0} out of {1} features ({2} matches)", idxAdrInner, mAdrCount, idxHit));
                                Application.DoEvents();
                            }
                        }

                    }

                    mSrcLyr.Dispose();
                }
                mTgtLyr.CommitTransaction();
                mTgtLyr.Dispose();
                mTgtDSrc.Dispose();
                mFileGDBDrv.Dispose();
            }
            catch (Exception ex)
            {
                pFrm.Log(ex.Message);
                return false;
            }

            return true;
        }
    }
}
