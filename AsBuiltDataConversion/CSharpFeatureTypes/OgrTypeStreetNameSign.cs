using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norplan.Adm.AsBuiltDataConversion.FeatureTypes;
using Norplan.Adm.AsBuiltDataConversion.MyAbuDhabi;
using Norplan.Adm.AsBuiltDataConversion.DataTypes;

namespace Norplan.Adm.AsBuiltDataConversion.CSharpFeatureTypes
{
    public class OgrStreetNameSign : OgrFeature
    {
        public int STREETNAMESIGNID { get; set; }
        public string QR_CODE { get; set; }
        public string SERIALNUMBER { get; set; }
        public string SIGNTYPE { get; set; }
        public string AUNRANGE_P1 { get; set; }
        public int ROADID_P1 { get; set; }
        public int DISTRICTID_P1 { get; set; }
        public string AUNRANGE_P2 { get; set; }
        public int? ROADID_P2 { get; set; }
        public int? DISTRICTID_P2 { get; set; }

        public bool IsValid()
        {
            bool isValid = true;
            if (STREETNAMESIGNID == 0) isValid = false;
            if (!Uri.IsWellFormedUriString(QR_CODE, UriKind.Absolute)) isValid = false;
            if (String.IsNullOrEmpty(SERIALNUMBER)) isValid = false;
            return isValid;
        }

        public OgrStreetNameSign()
        {

        }

        public SignTestTableSNS AsSignRecord()
        {
            var s = new SignTestTableSNS();
            s.p_uri = this.QR_CODE;
            return s;
        }

    }

 
}
