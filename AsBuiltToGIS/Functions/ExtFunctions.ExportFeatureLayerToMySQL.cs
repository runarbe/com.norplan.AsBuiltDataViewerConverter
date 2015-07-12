using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsBuiltToGIS.DataTypes;
using DotSpatial.Data;
using DotSpatial.Symbology;
using OSGeo.OGR;
using OSGeo.OSR;

namespace AsBuiltToGIS.Functions
{
    partial class ExtFunctions
    {
        public static ReturnValue ExportFeatureLayerToMySQL(string pConnStr, IPointLayer pLayer, string pSchemaName, bool pTruncateTable, int pSrcEPSG = 32640, int pTgtEPSG = 4326)
        {
            var mRV = new ReturnValue();
            var mSrcSRS = new SpatialReference(null);
            mSrcSRS.ImportFromEPSG(pSrcEPSG);

            var mTgtSRS = new SpatialReference(null);
            mTgtSRS.ImportFromEPSG(pTgtEPSG);

            var mTransformation = Osr.CreateCoordinateTransformation(mSrcSRS, mTgtSRS);

            MySQLLib.Create(pConnStr);

            // Check if table exists
            if (!MySQLLib.TableExists("geoobjects", pSchemaName))
            {
                var mSql = "delimiter $$ CREATE TABLE `geoobjects` ( `id` int(11) NOT NULL AUTO_INCREMENT, `geom` geometry NOT NULL, `left` float(10,6) NOT NULL, `bottom` float(10,6) NOT NULL, `right` float(10,6) NOT NULL, `top` float(10,6) NOT NULL, `namepart` varchar(250) DEFAULT NULL, `numberpart` int(11) DEFAULT NULL, `title` varchar(250) DEFAULT NULL, `description` text, `gtype` varchar(100) DEFAULT NULL, PRIMARY KEY (`id`), SPATIAL KEY `idxSpatial` (`geom`), FULLTEXT KEY `idxFullText` (`title`,`description`)) ENGINE=MyISAM AUTO_INCREMENT=2585 DEFAULT CHARSET=utf8$$";

                SLib.ExecuteNonQuery(mSql);
            }

            if (pTruncateTable)
            {
                var mSql = "DELETE FROM geoobjects";
                MySQLLib.ExecuteNonQuery(mSql);
            }

            foreach (IFeature mFeature in pLayer.DataSet.Features)
            {
                var mGeom = OSGeo.OGR.Geometry.CreateFromWkb(mFeature.ToBinary());

                if (pSrcEPSG != pTgtEPSG)
                {
                    mGeom.Transform(mTransformation);
                }

                Envelope mEnvelope = new Envelope();

                mGeom.GetEnvelope(mEnvelope);
                string mWkt;
                mGeom.ExportToWkt(out mWkt);

                var mSql = String.Format("INSERT INTO `admadr`.`geoobjects` (`geom`, `left`, `bottom`, `right`, `top`, `namepart`, `numberpart`, `title`, `description`, `gtype`) VALUES (GeomFromText('{0}'),{1},{2},{3},{4},'{5}',{6}, '{7}', '{8}', '{9}')",
                    mWkt,
                    mEnvelope.MinX,
                    mEnvelope.MinY,
                    mEnvelope.MaxX,
                    mEnvelope.MaxY,
                    Utilities.NormalizeString(mFeature.DataRow["ROADNAME_EN"].ToString()),
                    mFeature.DataRow["ADDRESSUNITNR"], // Function to convert to number?
                    mFeature.DataRow["ADDRESSUNITNR"] + ", " + Utilities.NormalizeString(mFeature.DataRow["ROADNAME_EN"].ToString()),
                    Utilities.GetVariants(mFeature.DataRow["ROADNAME_EN"].ToString()),
                    "address"
                    );

                MySQLLib.ExecuteNonQuery(mSql);

            }
            MySQLLib.Dbx.Close();
            MySQLLib.Dbx.Dispose();

            return mRV;
        }
    }
}
