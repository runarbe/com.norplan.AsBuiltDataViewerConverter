using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsBuiltToGIS.Functions
{
    public static class sqlStatements
    {
        public const string selectAddressUnitsSQL = "SELECT fc_addressUnitNumberSign.*, ADM_ADRROAD.NAMEENGLISH, ADM_ADRROAD.NAMEARABIC, ADM_ADRROAD.NAMEPOPULARENGLISH,ADM_ADRROAD.NAMEPOPULARARABIC, ADRDISTRICT.NAMEENGLISH AS DISTRICT_EN, ADRDISTRICT.NAMEARABIC AS DISTRICT_AR FROM ADRDISTRICT RIGHT JOIN (ADM_ADRROAD RIGHT JOIN fc_addressUnitNumberSign ON ADM_ADRROAD.ADRROADID = fc_addressUnitNumberSign.road_id) ON ADRDISTRICT.ADRDISTRICTID = ADM_ADRROAD.ADRDISTRICTID;";

        public const string selectAddressGuideSignsSQL = "SELECT fc_addressGuideSign.*, ADM_ADRROAD.NAMEENGLISH AS STREETNAME_EN, ADM_ADRROAD.NAMEARABIC AS STREETNAME_AR, ADRDISTRICT.NAMEENGLISH AS DISTRICT_EN, ADRDISTRICT.NAMEARABIC AS DISTRICT_AR FROM (fc_addressGuideSign LEFT JOIN ADM_ADRROAD ON fc_addressGuideSign.road_id = ADM_ADRROAD.ADRROADID) LEFT JOIN ADRDISTRICT ON fc_addressGuideSign.district_id = ADRDISTRICT.ABBREVIATION;";

        public const string selectStreetNameSignsSQL = "SELECT fc_streetNameSign.*, ADM_ADRROAD.NAMEENGLISH AS STREETNAME_EN, ADM_ADRROAD.NAMEARABIC AS STREETNAME_AR, ADRDISTRICT.NAMEENGLISH AS DISTRICT_EN, ADRDISTRICT.NAMEARABIC AS DISTRICT_AR FROM (fc_streetNameSign LEFT JOIN ADM_ADRROAD ON fc_streetNameSign.road_id_p1 = ADM_ADRROAD.ADRROADID) LEFT JOIN ADRDISTRICT ON fc_streetNameSign.district_id_p1 = ADRDISTRICT.ABBREVIATION;";

        public const string insertUpdateMyAbuDhabiNetSQL = "INSERT INTO signtest (p_uri, ft_table, s_desc, s_signtype, s_adr_unit_num, s_sname_desc, s_sname, s_sname_desc_ar, s_sname_ar, s_dname, s_dname_ar, loc_x, loc_y) VALUES {0} ON DUPLICATE KEY UPDATE ft_table = VALUES(ft_table), s_desc = VALUES(s_desc), s_signtype = VALUES(s_signtype), s_adr_unit_num = VALUES(s_adr_unit_num), s_sname_desc = VALUES(s_sname_desc), s_sname = VALUES(s_sname), s_sname_desc_ar = VALUES(s_sname_desc_ar), s_sname_ar = VALUES(s_sname_ar), s_dname = VALUES(s_dname), s_dname_ar = VALUES(s_dname_ar), loc_x = VALUES(loc_x), loc_y = VALUES(loc_y);";

    }

}
