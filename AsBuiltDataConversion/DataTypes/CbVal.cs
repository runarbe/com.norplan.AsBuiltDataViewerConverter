using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Norplan.Adm.AsBuiltDataConversion.DataTypes
{
    class CbVal
    {
        public string Key;
        public string Val;

        public CbVal(string pKey, string pVal)
        {
            this.Key = pKey;
            this.Val = pVal;
        }
    }
}
