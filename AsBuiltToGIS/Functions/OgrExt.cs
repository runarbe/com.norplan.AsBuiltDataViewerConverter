using OSGeo.OGR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AsBuiltToGIS.Functions;

namespace AsBuiltToGIS.Functions
{
    public static class OgrExt
    {
        public static bool hasField(this Layer pLayer, string pFieldName)
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

        public static bool hasLayer(this DataSource pDataSource, string pLayerName)
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
