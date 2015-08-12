using DotSpatial.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Norplan.Adm.AsBuiltDataConversion.FeatureTypes
{
    class District : FeatureSet
    {
        public District() : base()
        {

            this.FeatureType = DotSpatial.Topology.FeatureType.Unspecified;
            this.DataTable.Columns.Add(new DataColumn("DISTRICTABBREVIATION", typeof(string)));
            this.DataTable.Columns.Add(new DataColumn("NAMEARABIC", typeof(string)));
            this.DataTable.Columns.Add(new DataColumn("NAMELATIN", typeof(string)));
            this.DataTable.Columns.Add(new DataColumn("NAMEPOPULARARABIC", typeof(string)));
            this.DataTable.Columns.Add(new DataColumn("NAMEPOPULARLATIN", typeof(string)));
            this.DataTable.Columns.Add(new DataColumn("APPROVED", typeof(string)));
        }

        public bool AddNewRow(DotSpatial.Topology.IGeometry pPolygon, string pDistrictAbbreviation, string pNameArabic, string pNameLatin, string pNamePopularArabic = "", string pNamePopularLatin= "", string pApproved = "N") {
            var mFeature = this.AddFeature(pPolygon);
            mFeature.DataRow.BeginEdit();
            mFeature.DataRow["DISTRICTABBREVIATION"] = pDistrictAbbreviation;
            mFeature.DataRow["NAMEARABIC"] = pNameArabic;
            mFeature.DataRow["NAMELATIN"] = pNameLatin;
            mFeature.DataRow["NAMEPOPULARARABIC"] = pNamePopularArabic;
            mFeature.DataRow["NAMEPOPULARLATIN"] = pNamePopularLatin;
            mFeature.DataRow["APPROVED"] = pApproved;
            mFeature.DataRow.EndEdit();
            return true;
        }
    }
}