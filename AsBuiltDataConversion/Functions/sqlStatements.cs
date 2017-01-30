using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Norplan.Adm.AsBuiltDataConversion.Functions
{
    public static class sqlStatements
    {
        public const string selectRoadNamesSQL = "SELECT ADRROADID, NAMEENGLISH, NAMEARABIC, NAMEPOPULARENGLISH, NAMEPOPULARARABIC, ROADTYPE, ADRDISTRICTID, DESCRIPTIONENGLISH, DESCRIPTIONARABIC, APPROVED FROM ADM_ADRROAD WHERE APPROVED = 1";
        public const string selectAddressUnitsSQL = "SELECT fc_addressUnitNumberSign.* FROM fc_addressUnitNumberSign;";

        public const string selectAddressGuideSignsSQL = "SELECT fc_addressGuideSign.* FROM fc_addressGuideSign";

        public const string selectStreetNameSignsSQL = "SELECT fc_streetNameSign.* FROM fc_streetNameSign";

        public const string insertUpdateMyAbuDhabiNetSQL = "INSERT INTO signtest (p_uri, ft_table, s_desc, s_signtype, s_adr_unit_num, s_sname_desc, s_sname, s_sname_desc_ar, s_sname_ar, s_dname, s_dname_ar, loc_x, loc_y) VALUES {0} ON DUPLICATE KEY UPDATE ft_table = VALUES(ft_table), s_desc = VALUES(s_desc), s_signtype = VALUES(s_signtype), s_adr_unit_num = VALUES(s_adr_unit_num), s_sname_desc = VALUES(s_sname_desc), s_sname = VALUES(s_sname), s_sname_desc_ar = VALUES(s_sname_desc_ar), s_sname_ar = VALUES(s_sname_ar), s_dname = VALUES(s_dname), s_dname_ar = VALUES(s_dname_ar), loc_x = VALUES(loc_x), loc_y = VALUES(loc_y);";

        public const string createDistrictTableSQL = "CREATE TABLE IF NOT EXISTS `districts` (`id` int(11) NOT NULL AUTO_INCREMENT,`districtname_ar` varchar(250) DEFAULT NULL,`districtname_en` varchar(250) DEFAULT NULL,`geom` geometry DEFAULT NULL,`districtabbreviation` varchar(10) DEFAULT NULL, PRIMARY KEY (`id`)) ENGINE=InnoDB DEFAULT CHARSET=latin1;";

        public const string emptyDistrictsSQL = "DELETE FROM `districts`;";

        public const string insertDistrictsSQL = "INSERT INTO `districts` (`districtname_ar`, `districtname_en`, `geom`, `districtabbreviation`) VALUES('{0}', '{1}', ST_GeomFromText('{2}'), '{3}');\n";

    }

}
