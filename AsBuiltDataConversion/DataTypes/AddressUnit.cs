using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Norplan.Adm.AsBuiltDataConversion.DataTypes;
using OSGeo.OGR;

namespace Norplan.Adm.AsBuiltDataConversion.DataTypes
{
    class AddressUnit
    {
        public DatFld ADDRESSUNITID = new DatFld("AUID", FieldType.OFTInteger, "id");
        public DatFld ROADID = new DatFld("RID", FieldType.OFTInteger, "road_id");
        public DatFld ADDRESSUNITNR = new DatFld("ANR",FieldType.OFTString, "addressUnitNumber", 10);
        public DatFld ROADNAME_EN = new DatFld("NAME_EN", FieldType.OFTString,"NAMEENGLISH", 50);
        public DatFld ROADNAME_AR = new DatFld("NAME_AR", FieldType.OFTString, "NAMEARABIC", 50);
        public DatFld ROADNAME_POP_EN = new DatFld("NAMEP_EN", FieldType.OFTString, "NAMEPOPULARENGLISH", 250);
        public DatFld ROADNAME_POP_AR = new DatFld("NAMEP_AR", FieldType.OFTString, "NAMEPOPULARARABIC", 250);
        public DatFld DISTRICT_EN = new DatFld("DIST_EN", FieldType.OFTString, "DISTRICT_EN", 80);
        public DatFld DISTRICT_AR = new DatFld("DIST_AR", FieldType.OFTString, "DISTRICT_AR", 80);
        public DatFld MUNICIPALITY_EN = new DatFld("MUN_EN", FieldType.OFTString, null, 50);
        public DatFld MUNICIPALITY_AR = new DatFld("MUN_AR", FieldType.OFTString, null, 50);
        public DatFld QR_CODE = new DatFld("QR_CODE",FieldType.OFTString, null, 250);
        
        public AddressUnit()
        {
        }

    }
}
