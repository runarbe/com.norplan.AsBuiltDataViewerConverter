using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Norplan.Adm.AsBuiltDataConversion.Functions
{
    public static class StringExt
    {
        public static string SanitizeForUseAsLayerName(this string pString)
        {
            return pString.Replace(' ', '_');
        }

        public static string MySQLEscape(this string pStr)
        {
            return pStr.Replace(@"\", @"\\").Replace("'", @"\'");
        }

    }

}
