using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Norplan.Adm.AsBuiltDataConversion.DataTypes
{
    public class GeoObjects
    {
        const string gtype_address = "address";

        public int id { get; set; }
        public double left { get; set; }
        public double bottom { get; set; }
        public double right { get; set; }
        public double top { get; set; }
        public string namepart { get; set; }
        public int? numberpart { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string gtype { get; set; }

        public GeoObjects()
        {
            gtype = gtype_address;
        }

    }
}
