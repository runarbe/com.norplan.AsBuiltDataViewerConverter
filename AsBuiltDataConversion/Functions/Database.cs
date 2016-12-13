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
        /// Database outputShapefileName
        /// </summary>
        public string DbFilename = "";
        /// <summary>
        /// Name of directory where database is stored
        /// </summary>
        public string DbDirectory = "";
        /// <summary>
        /// Database base outputShapefileName
        /// </summary>
        public string DbBaseName = "";

        /// <summary>
        /// A command object
        /// </summary>
        private OleDbCommand _cmd = null;

        /// <summary>
        /// A data adapter object
        /// </summary>
        private OleDbDataAdapter _dataAdapter = null;

        /// <summary>
        /// Database connection
        /// </summary>
        private OleDbConnection _dbConnection = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pOPFn">The outputShapefileName of the access 2010+ database to open</param>
        public Database(string dbFileName)
        {
            this.DbFilename = dbFileName;
            var dbFileInfo = new FileInfo(dbFileName);

            this.DbBaseName = dbFileInfo.Name.Split('.')[0];
            this.DbDirectory = dbFileInfo.DirectoryName;
            this.Open();
            this._cmd = new OleDbCommand("", this._dbConnection);
        }

        /// <summary>
        /// Executes SQL
        /// </summary>
        /// <param name="sqlStatement">The SQL statement to execute, typically an 'INSERT' or 'UPDATE'</param>
        /// <returns>The number of rows affected or -1 on error</returns>
        public int Execute(string sqlStatement)
        {
            if (this._dbConnection == null)
            {
                this.Open();
            }
            try
            {
                this._cmd.CommandText = sqlStatement;
                return this._cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// Queries SQL
        /// </summary>
        /// <param name="sqlStatement">The SQL statement to execute, typically fieldAttributes SELECT</param>
        /// <returns>A .NET DataTable object containing the records returned by the statement</returns>
        public DataTable Query(string sqlStatement)
        {
            if (this._dbConnection == null)
            {
                this.Open();
            }
                this._dataAdapter = new OleDbDataAdapter(sqlStatement, this._dbConnection);
            var dataSet = new DataSet();
            this._dataAdapter.Fill(dataSet);
            return dataSet.Tables[0];
        }

        public string QuerySingle(string sqlStatement)
        {
            if (this._dbConnection == null)
            {
                this.Open();
            }
            this._cmd = new OleDbCommand(sqlStatement, this._dbConnection);
            return this._cmd.ExecuteScalar().ToString();
        }

        /// <summary>
        /// Open fieldAttributes new database connection
        /// </summary>
        public void Open()
        {
            string connectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Jet OLEDB:Database Password=thePassword;", this.DbFilename);

            this._dbConnection = new OleDbConnection(connectionString);

            try
            {
                this._dbConnection.Open();

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
            if (this._dbConnection != null && this._dbConnection.State != ConnectionState.Closed)
            {
                this._dbConnection.Close();
            }
            this._dbConnection = null;
        }

        /// <summary>
        /// Disposes of the object and frees up resources associated with it
        /// </summary>
        public void Dispose()
        {
            this.Close();
        }
    }
}
