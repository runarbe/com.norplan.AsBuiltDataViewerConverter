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
            if (String.IsNullOrEmpty(pStr))
            {
                return "";
            }
            return pStr.Replace(@"\", @"\\").Replace("'", @"\'");
        }

        public static string Dbfify(this string pStr, List<string> str)
        {
            if (pStr.Length > 10)
            {
                pStr = pStr.Substring(0, 10);
            }

            if (str.Contains(pStr))
            {
                int i = 1;
                pStr = pStr.Substring(0, 8);

                while (str.Contains(pStr + "_" + i.ToString()))
                {
                    i++;
                }

                pStr = pStr + "_" + i.ToString();

            }

            return pStr;

        }
    }
}
