using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;

namespace Norplan.Adm.AsBuiltDataConversion.Functions
{
    public static class MySQLLib
    {
        public static OdbcConnection Dbx = null;

        public static string MakeConnString(
            string pDriverName,
            string pServer,
            string pSchema,
            string pPort,
            String pUser,
            string pPass)
        {
            return String.Format("Driver={{{0}}};Server={1};Database={2};Port={3};User={4};Password={5};Option=3;",
                pDriverName,
                pServer,
                pSchema,
                pPort,
                pUser,
                pPass);
        }

        public static void Create(string pConnStr)
        {
            MySQLLib.Dbx = new OdbcConnection(pConnStr);
            MySQLLib.Dbx.Open();
        }

        public static OdbcCommand Cmd(string pSql)
        {
            if (MySQLLib.Dbx == null || MySQLLib.Dbx.State != System.Data.ConnectionState.Open)
            {
                return null;
            }
            return new OdbcCommand(pSql, MySQLLib.Dbx);
        }

        public static bool TableExists(string pTableName, string pSchemaName)
        {
            var mSql = String.Format("SELECT * FROM information_schema.tables WHERE table_schema = '{0}' AND table_name = '{1}' LIMIT 1;", pSchemaName, pTableName);
            var mReader = MySQLLib.ExecuteReader(mSql);
            if (mReader.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static OdbcDataReader ExecuteReader(string pSql)
        {
            var mCmd = MySQLLib.Cmd(pSql);
            return mCmd.ExecuteReader();
        }

        public static object ExecuteScalar(string pSql)
        {
            var mCmd = MySQLLib.Cmd(pSql);
            return mCmd.ExecuteScalar();
        }

        public static int ExecuteNonQuery(string pSql)
        {
            var mCmd = MySQLLib.Cmd(pSql);
            return mCmd.ExecuteNonQuery();
        }

    }
}
