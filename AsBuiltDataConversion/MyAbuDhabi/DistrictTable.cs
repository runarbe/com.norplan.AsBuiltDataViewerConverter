using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Norplan.Adm.AsBuiltDataConversion.MyAbuDhabi
{
    public abstract class DistrictTable : SqlTable
    {
        [SqlAttribute(IsPrimaryKey=true, Index = true)]
        public int ID {get; set;}

        [SqlAttribute(IsWkt= true)]
        public string geometry { get; set; }
        
        [SqlAttribute]
        public string name_arabic { get; set; }
        
        [SqlAttribute]
        public string name_latin { get; set; }

    }
}
