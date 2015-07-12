using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OSGeo.OGR;

namespace AsBuiltToGIS.DataTypes
{
    class DatFld
    {
        public string shortName;
        public FieldType type;
        public string value = null;
        public int length = 50;
        public string srcField = null;

        public DatFld(string pShortName, FieldType pType, string pSrcField = null, int pLength = 100)
        {
            this.shortName = pShortName;
            this.type = pType;
            this.length = pLength;
            this.srcField = pSrcField;
        }

    }
}
