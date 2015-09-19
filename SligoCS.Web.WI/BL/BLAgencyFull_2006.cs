using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;


namespace SligoCS.BL.WI
{
    public class BLAgencyFull_2006 : EntityWIBase
    {
        public v_AgencyFull_2006 GetSchool(string fullKey, int year)
        {
            DALAgencyFull_2006 school = new DALAgencyFull_2006();
            // string masked = GetMaskedFullkey(fullKey, OrgLevel.School);
            // v_AgencyFull_2006 ds = school.GetSchool(masked, year);
            v_AgencyFull_2006 ds = school.GetSchool(fullKey, year);
            this.sql = school.SQL;
            return ds;
        }
        public v_AgencyFull_2006 GetSchoolByName(string name_first_alpha)
        {
            DALAgencyFull_2006 school = new DALAgencyFull_2006();
            v_AgencyFull_2006 ds = school.GetSchoolByName(name_first_alpha);
            this.sql = school.SQL;
            return ds;
        }
        public v_AgencyFull_2006 GetSchoolByDistrictKey(string District_Key)
        {
            DALAgencyFull_2006 school = new DALAgencyFull_2006();
            v_AgencyFull_2006 ds = school.GetSchoolByDistrictKey(District_Key);
            this.sql = school.SQL;
            return ds;
        }
        public v_AgencyFull_2006 GetDistrictByName(string name_first_alpha)
        {
            DALAgencyFull_2006 district = new DALAgencyFull_2006();
            v_AgencyFull_2006 ds = district.GetDistrictByName(name_first_alpha);
            this.sql = district.SQL;
            return ds;
        }
        public v_AgencyFull_2006 GetDistrictByCESA(string cesa_number, string var_hs)
        {
            DALAgencyFull_2006 district = new DALAgencyFull_2006();
            v_AgencyFull_2006 ds = district.GetDistrictByCESA(cesa_number, var_hs);
            this.sql = district.SQL;
            return ds;
        }
        public v_AgencyFull_2006 GetCountyByName(string name_first_alpha)
        {
            DALAgencyFull_2006 county = new DALAgencyFull_2006();
            v_AgencyFull_2006 ds = county.GetCountyByName(name_first_alpha);
            this.sql = county.SQL;
            return ds;
        }
        public v_AgencyFull_2006 GetSchoolBySubstr(string school_substr)
        {
            DALAgencyFull_2006 school = new DALAgencyFull_2006();
            v_AgencyFull_2006 ds = school.GetSchoolBySubstr(school_substr);
            this.sql = school.SQL;
            return ds;
        }
        public v_AgencyFull_2006 GetDistrictBySubstr(string district_substr)
        {
            DALAgencyFull_2006 district = new DALAgencyFull_2006();
            v_AgencyFull_2006 ds = district.GetDistrictBySubstr(district_substr);
            this.sql = district.SQL;
            return ds;
        }
        public v_AgencyFull_2006 GetCountyBySubstr(string county_substr)
        {
            DALAgencyFull_2006 county = new DALAgencyFull_2006();
            v_AgencyFull_2006 ds = county.GetCountyBySubstr(county_substr);
            this.sql = county.SQL;
            return ds;
        }
    }
}
