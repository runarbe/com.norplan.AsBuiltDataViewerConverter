using OSGeo.OGR;
using OSGeo.OSR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AsBuiltToGIS
{
    class PlotImport : IDisposable
    {

        /// <summary>
        /// An OGR Layer object
        /// </summary>
        private Layer PlotImportLayer;

        /// <summary>
        /// An OGR DataSource object
        /// </summary>
        private DataSource PlotImportDataSource;

        /// <summary>
        /// Name of output shapefile
        /// </summary>
        public string ShapefileName;



        public static string GetShapefileName()
        {

            string mPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            string ShapefileName = Path.Combine(mPath, "GisData", "plots.shp");
            return ShapefileName;
        }

        public PlotImport()
        {

            this.ShapefileName = GetShapefileName();

            if (File.Exists(this.ShapefileName))
            {
                File.Delete(this.ShapefileName);
            }
            using (var mDrv = Ogr.GetDriverByName("ESRI Shapefile"))
            {
                this.PlotImportDataSource = mDrv.CreateDataSource(this.ShapefileName, null);
                var mSpatialReference = new SpatialReference(null);
                mSpatialReference.ImportFromEPSG(32640);
                this.PlotImportLayer = this.PlotImportDataSource.CreateLayer("PlotShapefile", mSpatialReference, wkbGeometryType.wkbPolygon, null);
                this.PlotImportLayer.CreateField(new FieldDefn("ZONETPSSNA", FieldType.OFTString), 0);
                this.PlotImportLayer.CreateField(new FieldDefn("SECTORTPSS", FieldType.OFTString), 0);
                this.PlotImportLayer.CreateField(new FieldDefn("PLOTNUMBER", FieldType.OFTString), 0);
                return;
            }
        }

        public void StartTransaction()
        {
            this.PlotImportLayer.StartTransaction();
        }

        public void CommitTransaction()
        {
            this.PlotImportLayer.CommitTransaction();
            this.PlotImportDataSource.SyncToDisk();
        }

        public void CreateIndex()
        {
            this.PlotImportDataSource.ExecuteSQL("CREATE SPATIAL INDEX ON " + Path.GetFileNameWithoutExtension(this.ShapefileName), null, null);
        }

        public void AddPlot(Geometry geometry, string zone, string sector, string plot)
        {
            var mFeature = new Feature(PlotImportLayer.GetLayerDefn());
            mFeature.SetGeometry(geometry);
            mFeature.SetField("ZONETPSSNA", zone);
            mFeature.SetField("SECTORTPSS", sector);
            mFeature.SetField("PLOTNUMBER", plot);
            this.PlotImportLayer.CreateFeature(mFeature);
        }

        public void Dispose()
        {
            PlotImportLayer.Dispose();
        }
    }
}
