using OSGeo.OGR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Norplan.Adm.AsBuiltDataConversion.Functions
{
    public static class TypeExt
    {
        public static string MySQLType(this Type dotNetDataType)
        {
            string dataType;

            switch (dotNetDataType.Name.ToLower())
            {
                case "int":
                case "long":
                    dataType = "INTEGER";
                    break;
                case "double":
                case "float":
                    dataType = "DOUBLE";
                    break;
                case "string":
                default:
                    dataType = "VARCHAR(255)";
                    break;
            }
            return dataType;
        }

        public static FieldType OgrType(this Type dotNetDataType)
        {
            switch (dotNetDataType.Name.ToLower())
            {
                case "int":
                case "long":
                    return FieldType.OFTInteger;
                case "double":
                case "float":
                    return FieldType.OFTReal;
                case "string":
                default:
                    return FieldType.OFTString;
            }
        }

        public static string SQLiteType(this Type dotNetDataType)
        {
            string dataType;

            switch (dotNetDataType.Name.ToLower())
            {
                case "int":
                case "long":
                    dataType = "INTEGER";
                    break;
                case "double":
                case "float":
                    dataType = "REAL";
                    break;
                case "string":
                default:
                    dataType = "TEXT";
                    break;
            }
            return dataType;
        }

    }
}
