using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norplan.Adm.AsBuiltDataConversion.Functions;

namespace Norplan.Adm.AsBuiltDataConversion.MyAbuDhabi
{
    public class SqlFieldDefinition
    {
        public string Name;
        public string SQLDataType;
        public SqlAttribute Attributes;

        public SqlFieldDefinition(string name, Type dataType, SqlAttribute attributes = null)
        {
            Name = name;
            SQLDataType = dataType.SQLiteType();
            if (attributes == null)
            {
                Attributes = new SqlAttribute();
            }
        }

    }
}
