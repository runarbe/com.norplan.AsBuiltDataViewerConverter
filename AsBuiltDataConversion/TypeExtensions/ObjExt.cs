using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Norplan.Adm.AsBuiltDataConversion.TypeExtensions
{
    public static class ObjExt
    {
        public static int? AsIntegerOrNull(this object inputObject)
        {
            int? outValue = null;
            int tmpValue;

            if (int.TryParse(inputObject.ToString(), out tmpValue))
            {
                outValue = tmpValue;
            }

            return outValue;
        }

        /// <summary>
        /// Converts an object to an integer if possible
        /// </summary>
        /// <param name="inputObject"></param>
        /// <returns>Integer or exception</returns>
        public static int AsInteger(this object inputObject)
        {
            try
            {
                return int.Parse(inputObject.ToString());
            }
            catch (Exception)
            {
                throw new Exception("Could not convert value " + inputObject.ToString() + " to an integer");
            }
        }

    }
}
