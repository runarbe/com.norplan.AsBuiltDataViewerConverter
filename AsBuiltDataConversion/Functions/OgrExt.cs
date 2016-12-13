using OSGeo.OGR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Norplan.Adm.AsBuiltDataConversion.Functions;

namespace Norplan.Adm.AsBuiltDataConversion.Functions
{
    public static class OgrExt
    {
        public static  Geometry CTrans(this Geometry geom, int fromEpsg, int toEpsg = 4326) {
            var mSrcProj = ExtFunctions.GetSpatialReferenceByEPSG(32640);
            var mTgtProj = ExtFunctions.GetSpatialReferenceByEPSG(4326);
            var mTransformation = new OSGeo.OSR.CoordinateTransformation(mSrcProj, mTgtProj);
            if (fromEpsg != toEpsg)
            {
                geom.Transform(mTransformation);
            }
            mTransformation.Dispose();
            mSrcProj.Dispose();
            mTgtProj.Dispose();
            return geom;
        }

        public static Feature GetDistrictByPoint(this Layer lyr, Geometry g, int epsg = 32640)
        {

            if (epsg != 32640) {
                g.CTrans(epsg);
            }

            lyr.SetSpatialFilter(g);
            return lyr.GetNextFeature();
        }

        public static string GetFieldAsStringByGeom(this Layer pLayer, string pAttrName, Geometry pGeom)
        {
            var mRetVal = "";
            pLayer.SetSpatialFilter(pGeom);
            pLayer.ResetReading();
            using (Feature mFeature = pLayer.GetNextFeature())
            {
                if (mFeature != null)
                {
                    mRetVal = mFeature.GetFieldAsString(pAttrName);
                }
            }
            return mRetVal;
        }

        public static bool HasField(this Layer pLayer, string pFieldName)
        {
            var mLayerDefinition = pLayer.GetLayerDefn();
            var mFieldCount = mLayerDefinition.GetFieldCount();

            for (int i = 0; i < mFieldCount; i++)
            {
                if (mLayerDefinition.GetFieldDefn(i).GetName() == pFieldName)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Get fieldAttributes list of the field names in an OgrFeature definition
        /// </summary>
        /// <param name="f">Ogr.Feature</param>
        /// <returns></returns>
        public static List<String> GetFieldNames(this Feature f)
        {
            var l = new List<String>();

            for (var i = 0; i < f.GetFieldCount(); i++)
            {
                l.Add(f.GetFieldDefnRef(i).GetName());
            }
            return l;
        }

        public static bool HasLayer(this DataSource pDataSource, string pLayerName)
        {
            for (int i = 0; i < pDataSource.GetLayerCount(); i++)
            {
                Debug.WriteLine(pDataSource.GetLayerByIndex(i).GetName() + " compared to " + pLayerName);
                if (pDataSource.GetLayerByIndex(i).GetName() == pLayerName.SanitizeForUseAsLayerName())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
