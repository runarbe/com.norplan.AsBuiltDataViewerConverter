using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace AsBuiltToGIS.Functions
{
    public static class SLib
    {
        public static SQLiteConnection Dbx;

        public static void Create(string pFilename)
        {
            SLib.Dbx = new SQLiteConnection(@"Data Source=" + pFilename + ";PRAGMA journal_mode=PERSIST;");
            SLib.Dbx.Open();
            return;
        }

        public static bool TableExists(string pTableName)
        {
            var mSql = String.Format("SELECT name FROM sqlite_master WHERE type='table' AND name='{0}';",
                pTableName);
            var mReader = SLib.ExecuteReader(mSql);
            if (mReader.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static SQLiteCommand Cmd(string pSQL)
        {
            if (SLib.Dbx != null && SLib.Dbx.State != System.Data.ConnectionState.Open)
            {
                SLib.Dbx.Open();
            }

            return new SQLiteCommand(pSQL, SLib.Dbx);
        }

        public static int ExecuteNonQuery(string pSQL) {
            var mCmd = SLib.Cmd(pSQL);
            return mCmd.ExecuteNonQuery();
        }

        public static SQLiteDataReader ExecuteReader(string pSQL)
        {
            var mCmd = SLib.Cmd(pSQL);
            return mCmd.ExecuteReader();
        }

        public static object ExecuteScalar(string pSQL)
        {
            var mCmd = SLib.Cmd(pSQL);
            return mCmd.ExecuteScalar();
        }

        public static int GetLastInsertId()
        {
            return (int)SLib.ExecuteScalar("SELECT last_insert_rowid();");
        }

    }
}
