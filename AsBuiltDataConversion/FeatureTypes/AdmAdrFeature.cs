using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotSpatial;
using DotSpatial.Data;
using DotSpatial.Topology;
using System.Data;
using System.Data.OleDb;
using Norplan.Adm.AsBuiltDataConversion.Functions;

namespace Norplan.Adm.AsBuiltDataConversion.FeatureTypes
{

    class AdmAdrFeature : FeatureSet
    {
        protected Database dbx;

        public string GetQRCode(DataRow pRow, DataTable pTable)
        {
            if (pTable.Columns.Contains("uriInQrCode") && pTable.Columns.Contains("autoUri"))
            {
                if (pRow["uriInQrCode"].ToString() != "")
                {
                    return pRow["uriInQrCode"].ToString();
                }
                else
                {
                    return pRow["autoUri"].ToString();
                }
            }
            else
            {
                return pRow["autoUri"].ToString();
            }

        }

        public DataTable GetTable(string pSqlStatement)
        {
            return this.dbx.Query(pSqlStatement);
        }

        protected bool AddCol(string pFieldName, Type pType) {
            this.DataTable.Columns.Add(new DataColumn(pFieldName, pType));
            return true;
        }

    }

}
