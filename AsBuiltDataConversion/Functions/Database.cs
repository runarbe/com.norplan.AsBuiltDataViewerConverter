using System;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Norplan.Adm.AsBuiltDataConversion.Functions
{
    /// <summary>
    /// Database
    /// </summary>
    public class Database : IDisposable
    {
        /// <summary>
        /// Database filename
        /// </summary>
        public string dbFilename = "";
        /// <summary>
        /// Name of directory where database is stored
        /// </summary>
        public string dbDir = "";
        /// <summary>
        /// Database base filename
        /// </summary>
        public string dbBasename = "";

        /// <summary>
        /// A command object
        /// </summary>
        OleDbCommand cmd = null;

        /// <summary>
        /// A data adapter object
        /// </summary>
        OleDbDataAdapter dataAdapter = null;

        /// <summary>
        /// Database connection
        /// </summary>
        OleDbConnection dbConnection = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pOPFn">The filename of the access 2010+ database to open</param>
        public Database(string pFilename)
        {
            this.dbFilename = pFilename;
            var mFileInfo = new FileInfo(pFilename);

            this.dbBasename = mFileInfo.Name.Split('.')[0];
            this.dbDir = mFileInfo.DirectoryName;
            this.Open();
        }

        /// <summary>
        /// Executes an SQL statement
        /// </summary>
        /// <param name="sql">Any SQL statement</param>
        /// <returns>Returns the number of rows affected or -1 on error</returns>
        public int Execute(string sql)
        {
            if (this.dbConnection == null)
            {
                this.Open();
            }
            try
            {
                this.cmd.CommandText = sql;
                this.cmd.Connection = dbConnection;
                return this.cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return -1;                
            }
        }

        public DataTable Query(string pSql)
        {
            if (this.dbConnection == null)
            {
                this.Open();
            }
            this.dataAdapter = new OleDbDataAdapter(pSql, this.dbConnection);
            var mDataset = new DataSet();
            this.dataAdapter.Fill(mDataset);
            return mDataset.Tables[0];
        }

        public string QuerySingle(string pSql)
        {
            if (this.dbConnection == null)
            {
                this.Open();
            }
            this.cmd = new OleDbCommand(pSql, this.dbConnection);
            return this.cmd.ExecuteScalar().ToString();
        }

        /// <summary>
        /// Open a new database connection
        /// </summary>
        public void Open()
        {
            string connectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Jet OLEDB:Database Password=thePassword;", this.dbFilename);

            this.dbConnection = new OleDbConnection(connectionString);

            try
            {
                this.dbConnection.Open();

            }
            catch (Exception)
            {
                Debug.WriteLine("Could not open database" + connectionString);
            }
            return;
        }

        /// <summary>
        /// Close database connection
        /// </summary>
        public void Close()
        {
            if (this.dbConnection != null && this.dbConnection.State != ConnectionState.Closed)
            {
                this.dbConnection.Close();
            }
            this.dbConnection = null;
        }


        public void Dispose()
        {
            this.Close();
        }
    }
}
