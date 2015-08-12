using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norplan.Adm.AsBuiltDataConversion.DataTypes;
using DotSpatial.Symbology;
using System.IO;
using OSGeo.OSR;
using System.Windows.Forms;
using DotSpatial.Data;
using DotSpatial.Projections;

namespace Norplan.Adm.AsBuiltDataConversion.Functions
{
    partial class ExtFunctions
    {
        public static ReturnValue ExportToSQLiteDatabase(IPointLayer pLyr, string pOPFn, ProjectionInfo pSrcProj, ProjectionInfo pTgtProj)
        {
            var mRV = new ReturnValue(true);

            var mReturnValue = new ReturnValue();
            var mTgtSRS = ExtFunctions.GetSpatialReferenceByEPSG(pTgtProj.AuthorityCode);

            bool mTransformationRequired = pSrcProj != pTgtProj ? true : false;

            if (mTransformationRequired)
            {
                pLyr.Reproject(pTgtProj);
            }

            SLib.Create(pOPFn);

            // Check if table exists
            if (!SLib.TableExists("geoobjects"))
            {
                var mSql = "CREATE TABLE geoobjects (title TEXT, description TEXT, gtype TEXT, left REAL, bottom REAL, right REAL, top REAL, wkt TEXT)";
                SLib.ExecuteNonQuery(mSql);
            }

            SLib.ExecuteNonQuery("BEGIN");

            foreach (IFeature mFeature in pLyr.DataSet.Features)
            {
                var mGeom = OSGeo.OGR.Geometry.CreateFromWkb(mFeature.ToBinary());

                string mWkt;
                mGeom.ExportToWkt(out mWkt);

                var mSql = String.Format("INSERT INTO geoobjects (title, description, gtype, left, bottom, right, top, wkt) values ('{0}','{1}', '{2}', {3}, {4}, {5}, {6}, '{7}')",
                    mFeature.DataRow["ADDRESSUNITNR"] + ", " + mFeature.DataRow["ROADNAME_EN"],
                    "",
                    "address",
                    mGeom.GetX(0),
                    mGeom.GetY(0),
                    mGeom.GetX(0),
                    mGeom.GetY(0),
                    mWkt
                    );

                SLib.ExecuteNonQuery(mSql);

            }
            SLib.ExecuteNonQuery("END");
            SLib.Dbx.Close();
            SLib.Dbx.Dispose();

            if (mTransformationRequired)
            {
                pLyr.Reproject(pSrcProj);
            }

            return mRV;
        }
    }
}