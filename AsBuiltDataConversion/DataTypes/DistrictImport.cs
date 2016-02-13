using OSGeo.OGR;
using OSGeo.OSR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Norplan.Adm.AsBuiltDataConversion
{
    class DistrictImport : IDisposable
    {

        /// <summary>
        /// An OGR Layer object
        /// </summary>
        private Layer DistrictImportLayer;

        /// <summary>
        /// An OGR DataSource object
        /// </summary>
        private DataSource DistrictImportDataSource;

        /// <summary>
        /// Name of output shapefile
        /// </summary>
        public string ShapefileName;

        public static string GetShapefileName()
        {

            string mPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string mAppPath = Path.Combine(mPath, "AsBuiltDataConversion");
            string mGisDataPath = Path.Combine(mAppPath, "GisData");
            
            if (!Directory.Exists(mAppPath))
            {
                Directory.CreateDirectory(mAppPath);
            }

            if (!Directory.Exists(mGisDataPath))
            {
                Directory.CreateDirectory(mGisDataPath);
            }

            string ShapefileName = Path.Combine(mGisDataPath, "districts.shp");
            return ShapefileName;
        }

        public DistrictImport()
        {

            this.ShapefileName = GetShapefileName();

            if (File.Exists(this.ShapefileName))
            {
                File.Delete(this.ShapefileName);
            }
            using (var mDrv = Ogr.GetDriverByName("ESRI Shapefile"))
            {

            }
        }

        public void CreateIndex()
        {
            this.DistrictImportDataSource.ExecuteSQL("CREATE SPATIAL INDEX ON " + Path.GetFileNameWithoutExtension(this.ShapefileName), null, null);
        }

        public void Dispose()
        {
            DistrictImportLayer.Dispose();
        }
    }
}
