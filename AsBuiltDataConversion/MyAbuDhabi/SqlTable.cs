using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Norplan.Adm.AsBuiltDataConversion.MyAbuDhabi
{
    public abstract class SqlTable
    {
        /// <summary>
        /// A table name to use for SQL statements
        /// </summary>
        private String TableName = null;

        /// <summary>
        /// Set the name of the table to use for SQL statements
        /// </summary>
        /// <param name="tableName"></param>
        private void setTableName(String tableName)
        {
            TableName = tableName;
        }

        /// <summary>
        /// Get the name of the table to use for SQL statements
        /// </summary>
        /// <returns></returns>
        private String getTableName()
        {
            return !String.IsNullOrEmpty(TableName) ? TableName : this.GetType().Name.ToLower();
        }

        private List<SqlFieldDefinition> getSQLFieldDefinitions()
        {
            var l = new List<SqlFieldDefinition>();

            foreach (var prop in this.GetType().GetProperties())
            {
                SqlAttribute fieldAttributes = null;

                Object[] o = prop.GetCustomAttributes(typeof(SqlAttribute), true);
                if (o.Count() > 1)
                {
                    fieldAttributes = o[0] as SqlAttribute;
                }

                l.Add(new SqlFieldDefinition(prop.Name, prop.PropertyType, fieldAttributes));
            }
            return l;
        }

        private List<SqlFieldDefinition> getNonPrimaryKeyProperties()
        {
            return getSQLFieldDefinitions().Where(d => d.Attributes.IsPrimaryKey == false).ToList();
        }

        private string getFieldDefinitionStatements()
        {
            var defn = new List<String>();

            foreach (var prop in getSQLFieldDefinitions())
            {
                defn.Add(String.Format("{0} {1},", prop.Name, prop.SQLDataType));
            }

            return String.Join(", ", defn);
        }

        public string CreateStatement()
        {
            var s = String.Format("CREATE TABLE IF NOT EXISTS {0} ({1})", getTableName(), getFieldDefinitionStatements());
            return s;
        }

        public string TruncateStatement()
        {
            var s = String.Format("DELETE FROM {0}", getTableName());
            return s;
        }

        public string UpsertPartialStatement()
        {
            var s = String.Format("ON DUPLICATE KEY UPDATE ", getTableName());
            return s;
        }

        public string InsertPartialStatement()
        {
            var s = String.Format("INSERT INTO {0} VALUES ", getTableName());
            return s;
        }

        public string ValuesPartialStatement()
        {
            var s = "()";
            return s;
        }

    }
}
