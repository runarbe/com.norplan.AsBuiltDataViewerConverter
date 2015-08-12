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
