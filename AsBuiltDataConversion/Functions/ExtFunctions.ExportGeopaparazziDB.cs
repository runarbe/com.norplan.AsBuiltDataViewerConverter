using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Norplan.Adm.AsBuiltDataConversion.DataTypes;
using DotSpatial.Data;
using DotSpatial.Symbology;
using OSGeo.OGR;
using OSGeo.OSR;
using System.Data.SQLite;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
using DotSpatial.Projections;
using System.Globalization;

namespace Norplan.Adm.AsBuiltDataConversion.Functions
{
    public static partial class ExtFunctions
    {
        public static ReturnValue ExportToGeopaparazzi(IPointLayer pLyr, string pOPFn, ProjectionInfo pSrcProjection, ProjectionInfo pTgtProjection)
        {
            NumberFormatInfo mNumFormat = new CultureInfo("en-US", false).NumberFormat;

            var mReturnValue = new ReturnValue();
            var mTgtSRS = ExtFunctions.GetSpatialReferenceByEPSG(4326);

            bool mTransformationRequired = pSrcProjection != pTgtProjection ? true : false;

            if (mTransformationRequired)
            {
                pLyr.Reproject(pTgtProjection);
            }

            // Set source and target directories
            var mSrcGPDir = Application.StartupPath + "\\Templates\\geopaparazzi";
            var mTgtGPDir = Application.StartupPath + "\\Temp\\geopaparazzi";

            // If temp directory exists, delete it
            if (Directory.Exists(mTgtGPDir))
            {
                Directory.Delete(mTgtGPDir, true);
                Debug.WriteLine("Deleted directory");
            }

            // Copy template
            Utilities.DirectoryCopy(mSrcGPDir, mTgtGPDir, true);
            Debug.WriteLine("Copied directory");

            var mConnStr = @"Data Source=" + mTgtGPDir + "\\geopaparazzi.db;PRAGMA journal_mode=PERSIST;";
            var mSdb = new SQLiteConnection(mConnStr);

            mSdb.Open();
            var mCmd = new SQLiteCommand("begin", mSdb);
            mCmd.ExecuteNonQuery();

            foreach (IFeature mFeature in pLyr.DataSet.Features)
            {
                var mGeom = OSGeo.OGR.Geometry.CreateFromWkb(mFeature.ToBinary());

                //mFeature.Transform(mTransformation);

                mCmd.CommandText = String.Format("INSERT INTO notes (lon, lat, altim, ts, text, cat, type) values ({0},{1},0,'{2}','{3}','POI',0)",
                    mGeom.GetX(0).ToString(mNumFormat),
                    mGeom.GetY(0).ToString(mNumFormat),
                    DateTime.Now.ToString("yyyy-MM-dd h:m:s"),
                    mFeature.DataRow["ADDRESSUNITNR"] + ", " + mFeature.DataRow["ROADNAME_EN"]).Replace("Street", "").Trim();
                Debug.WriteLine(mCmd.CommandText);
                mCmd.ExecuteNonQuery();

            }

            mCmd.CommandText = "end";
            mCmd.ExecuteNonQuery();
            mCmd.Dispose();
            mSdb.Close();
            mSdb.Dispose();

            var mFiles = new List<string>() {
                mTgtGPDir + "\\geopaparazzi.db",
                mTgtGPDir + "\\tags.json",
                mTgtGPDir + "\\media\\empty.txt" };

            var mNames = new List<string>() {
                "geopaparazzi\\geopaparazzi.db",
                "geopaparazzi\\tags.json",
                "geopaparazzi\\media\\empty.txt"
            };

            var mZipFile = ZipFile.Create(pOPFn);
            mZipFile.BeginUpdate();

            for (int i = 0; i < mFiles.Count(); i++)
            {
                mZipFile.Add(mFiles[i], mNames[i]);
            }
            mZipFile.CommitUpdate();
            mZipFile.Close();

            if (mTransformationRequired)
            {
                pLyr.Reproject(pSrcProjection);
            }

            return mReturnValue;

        }

    }
}