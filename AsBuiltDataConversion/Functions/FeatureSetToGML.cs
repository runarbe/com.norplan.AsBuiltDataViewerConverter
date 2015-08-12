using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OSGeo.OGR;
using System.Diagnostics;

namespace Norplan.Adm.AsBuiltDataConversion.Functions
{
    class FeatureSetToGML : FeatureSetToOGR
    {
        public override Layer Open(string pLayerName)
        {
            // Setup driver
            Driver drv = Ogr.GetDriverByName("GML");
            if (drv == null)
            {
                Debug.WriteLine("Could not get driver");
                return null;
            }

            // Create a datasource
            DataSource ds = drv.CreateDataSource(@"C:\users\runarbe\desktop\test\test.gml", null);
            if (ds == null)
            {
                Debug.WriteLine("Could not get datasource");
                return null;
            }

            // Create a layer
            OSGeo.OGR.Layer l = ds.CreateLayer("AddressUnits", null, wkbGeometryType.wkbPoint, null);
            if (l == null)
            {
                Debug.WriteLine("Could not create layer");
                return null;
            }

            return l;
        }
    }
}
