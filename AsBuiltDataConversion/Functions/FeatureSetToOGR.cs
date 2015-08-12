using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OSGeo.OGR;

namespace Norplan.Adm.AsBuiltDataConversion.Functions
{
    public abstract class FeatureSetToOGR
    {
        public abstract Layer Open(string pLayerName);

        public bool Initialize(string pLayerName)
        {
            var mLayer = this.Open(pLayerName);
            return true;
        }

    }
}
