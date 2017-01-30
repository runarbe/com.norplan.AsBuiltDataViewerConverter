using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Norplan.Adm.AsBuiltDataConversion.Functions
{
    public static class ObjExt
    {
        public static int ToInt(this Object o)
        {
            int i;
            int.TryParse(o.ToString(), out i);
            return i;
        }
    }
}
