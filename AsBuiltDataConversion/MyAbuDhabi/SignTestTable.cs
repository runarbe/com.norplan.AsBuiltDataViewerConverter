using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Norplan.Adm.AsBuiltDataConversion.MyAbuDhabi
{
    public abstract class SignTestTable : SqlTable
    {
        [SqlAttribute(Index = true)]
        public int ID {get; set;}

        [SqlAttribute(IsPrimaryKey = true, Index = true)]
        public string p_uri { get; set; }

        [SqlAttribute]
        public string ft_table { get; set; }
        
        [SqlAttribute]
        public string ft_id { get; set; }
        
        [SqlAttribute]
        public string s_desc { get; set; }
        
        [SqlAttribute]
        public string s_sign_type_code { get; set; }
        
        [SqlAttribute]
        public string s_signtype { get; set; }
        
        [SqlAttribute]
        public string s_adr_unit_num { get; set; }
        
        [SqlAttribute]
        public string s_photo { get; set; }
        
        [SqlAttribute]
        public string s_map { get; set; }
        
        [SqlAttribute]
        public string s_sname_desc { get; set; }
        
        [SqlAttribute]
        public string s_sname { get; set; }
        
        [SqlAttribute]
        public string s_sname_ar { get; set; }
        
        [SqlAttribute]
        public string s_sname_desc_ar { get; set; }
        
        [SqlAttribute]
        public string s_dname { get; set; }
        
        [SqlAttribute]
        public string s_dname_ar { get; set; }
        
        [SqlAttribute]
        public double loc_x { get; set; }
        
        [SqlAttribute]
        public double loc_y { get; set; }
        
        [SqlAttribute]
        public DateTime s_updated { get; set; }

    }
}
