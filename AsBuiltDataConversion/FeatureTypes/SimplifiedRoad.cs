using DotSpatial.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Norplan.Adm.AsBuiltDataConversion.FeatureTypes
{
    class SimplifiedRoads : FeatureSet
    {
        public SimplifiedRoads() : base()
        {

            this.FeatureType = DotSpatial.Topology.FeatureType.Unspecified;
            this.DataTable.Columns.Add(new DataColumn("DISTRICTID", typeof(int)));
            this.DataTable.Columns.Add(new DataColumn("ROADID", typeof(int)));
            this.DataTable.Columns.Add(new DataColumn("NAMEARABIC", typeof(string)));
            this.DataTable.Columns.Add(new DataColumn("NAMELATIN", typeof(string)));
            this.DataTable.Columns.Add(new DataColumn("NAMEPOPULARARABIC", typeof(string)));
            this.DataTable.Columns.Add(new DataColumn("NAMEPOPULARLATIN", typeof(string)));
            this.DataTable.Columns.Add(new DataColumn("ROADCLASS", typeof(string)));
            this.DataTable.Columns.Add(new DataColumn("APPROVED", typeof(string)));
        }

        public bool AddNewRow(DotSpatial.Topology.IGeometry pLineString, int pDistrictId, int pRoadId, string pNameArabic, string pNameLatin, string pNamePopularArabic = "", string pNamePopularLatin= "", int pRoadClass = -1, int pApproved = 0) {
            var mFeature = this.AddFeature(pLineString);
            mFeature.DataRow.BeginEdit();
            mFeature.DataRow["DISTRICTID"] = pDistrictId;
            mFeature.DataRow["ROADID"] = pRoadId;
            mFeature.DataRow["NAMEARABIC"] = pNameArabic;
            mFeature.DataRow["NAMELATIN"] = pNameLatin;
            mFeature.DataRow["NAMEPOPULARARABIC"] = pNamePopularArabic;
            mFeature.DataRow["NAMEPOPULARLATIN"] = pNamePopularLatin;
            mFeature.DataRow["ROADCLASS"] = pRoadClass;
            mFeature.DataRow["APPROVED"] = pApproved;
            mFeature.DataRow.EndEdit();
            return true;
        }
    }
}