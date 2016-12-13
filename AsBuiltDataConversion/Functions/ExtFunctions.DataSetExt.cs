using DotSpatial.Data;
using DotSpatial.Topology.Utilities;
using OSGeo.OGR;
using OSGeo.OSR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Norplan.Adm.AsBuiltDataConversion.Functions
{
    public static class IFeatureSetExt
    {
        public static Boolean ExportToShapeUsingOgr(this IFeatureSet dataset, String outputShapefileName)
        {
            OSGeo.GDAL.Gdal.SetConfigOption("SHAPE_ENCODING", "UTF-8");

            var outputFolder = Path.GetDirectoryName(outputShapefileName);

            if (File.Exists(outputShapefileName))
            {
                var exts = new List<String> { ".dbf", ".shx", ".shp", ".prj" };
                foreach (var ext in exts)
                {
                    var fn = outputFolder + "\\" + Path.GetFileNameWithoutExtension(outputShapefileName) + ext;
                    if (File.Exists(fn))
                    {
                        File.Delete(fn);
                    }
                }
            }

            var drv = Ogr.GetDriverByName("ESRI Shapefile");
            if (drv == null)
            {
                return false;
            }
            var ds = drv.CreateDataSource(Path.GetDirectoryName(outputShapefileName), null);
            if (ds == null)
            {
                return false;
            }

            var fromSrs = new SpatialReference(null);
            fromSrs.ImportFromEPSG(32640);

            Utilities.LogDebug(Path.GetFileNameWithoutExtension(outputShapefileName));

            var lyr = ds.CreateLayer(Path.GetFileNameWithoutExtension(outputShapefileName), fromSrs, wkbGeometryType.wkbPolygon, null);
            var listOfFieldNames = new List<String>();
            var fieldMapping = new Dictionary<string, string>();

            foreach (System.Data.DataColumn dc in dataset.GetColumns())
            {
                var dbfName = dc.ColumnName.Dbfify(listOfFieldNames);
                listOfFieldNames.Add(dbfName);
                fieldMapping.Add(dc.ColumnName, dbfName);
                lyr.CreateField(new FieldDefn(dbfName, dc.DataType.OgrType()), 0);
            }

            var wktWriter = new WktWriter();

            foreach (var currentFeature in dataset.Features)
            {
                var f = new OSGeo.OGR.Feature(lyr.GetLayerDefn());
                foreach (var dc2 in fieldMapping)
                {
                    var srcGeom = currentFeature.BasicGeometry as DotSpatial.Topology.Geometry;
                    var wktGeom = wktWriter.Write(srcGeom);
                    Geometry shapeGeom = Geometry.CreateFromWkt(wktGeom);
                    f.SetGeometry(shapeGeom);
                    var val = currentFeature.DataRow[dc2.Key];
                    f.SetField(dc2.Value, val.ToString());
                }
                var s = lyr.CreateFeature(f);
            }

            if (lyr == null)
            {
                return false;
            }
            else
            {
                ds.ExecuteSQL("CREATE SPATIAL INDEX ON " + Path.GetFileNameWithoutExtension(outputShapefileName), null, "OGRSQL");
            }

            fromSrs.Dispose();
            lyr.Dispose();
            ds.Dispose();
            drv.Dispose();
            return true;
        }
    }
}
