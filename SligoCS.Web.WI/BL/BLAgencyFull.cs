using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.Web.WI.DAL.DataSets;


namespace SligoCS.BL.WI
{
    public class BLAgencyFull : BLWIBase
    {
        protected AgencyFull _ds;
        protected DALAgencyFull _agency;

        public BLAgencyFull(string DataSetType)
        {
            DALAgencyFull = NewDALAgencyFull(DataSetType);
        }

        public BLAgencyFull()
            : this("")
        {

        }

        public DALAgencyFull DALAgencyFull
        {
            get
            {
                if (_agency == null) _agency = NewDALAgencyFull();
                return _agency;
            }
            set { _agency = value; }
        }

        protected DALAgencyFull NewDALAgencyFull()
        {
            return NewDALAgencyFull("");
        }

        protected DALAgencyFull NewDALAgencyFull(string DataSetType)
        {
            return new DALAgencyFull(DataSetType);
        }

        public AgencyFull GetSchool(string fullKey, int year)
        {
            DALAgencyFull school = DALAgencyFull;
            // string masked = FullKeyUtils.GetMaskedFullkey(fullKey, OrgLevel.School);
            // AgencyFull ds = school.GetSchool(masked, year);
            _ds = school.GetSchool(fullKey, year);
            this.sql = this.sql + "||" + school.SQL;
            return _ds;
        }
        public AgencyFull GetSchoolByName(string name_first_alpha)
        {
            DALAgencyFull school = DALAgencyFull;
            _ds = school.GetSchoolByName(name_first_alpha, CurrentYear);
            this.sql = this.sql + "||" + school.SQL;
            return _ds;
        }
        public AgencyFull GetSchoolByDistrictKey(string District_Key)
        {
            DALAgencyFull school = DALAgencyFull;
            _ds = school.GetSchoolByDistrictKey(District_Key, CurrentYear);
            this.sql = this.sql + "||" + school.SQL;
            return _ds;
        }
        public AgencyFull GetDistrictByName(string name_first_alpha)
        {
            DALAgencyFull district = DALAgencyFull;
            _ds = district.GetDistrictByName(name_first_alpha, CurrentYear);
            this.sql = this.sql + "||" + district.SQL;
            return _ds;
        }
        public AgencyFull GetDistrictByCESA(string cesa_number, string var_hs)
        {
            DALAgencyFull district = DALAgencyFull;
            _ds = district.GetDistrictByCESA(cesa_number, var_hs, CurrentYear);
            this.sql = this.sql + "||" + district.SQL;
            return (AgencyFull)_ds;
        }
        public AgencyFull GetCountyByName(string name_first_alpha)
        {
            DALAgencyFull county = DALAgencyFull;
            _ds = county.GetCountyByName(name_first_alpha, CurrentYear);
            this.sql = this.sql + "||" + county.SQL;
            return _ds;
        }
        public AgencyFull GetSchoolBySubstr(string school_substr)
        {
            DALAgencyFull school = DALAgencyFull;
            _ds = school.GetSchoolBySubstr(school_substr, CurrentYear);
            this.sql = this.sql + "||" + school.SQL;
            return _ds;
        }
        public AgencyFull GetDistrictBySubstr(string district_substr)
        {
            DALAgencyFull district = DALAgencyFull;
            _ds = district.GetDistrictBySubstr(district_substr, CurrentYear);
            this.sql = this.sql + "||" + district.SQL;
            return _ds;
        }
        public AgencyFull GetCountyBySubstr(string county_substr)
        {
            DALAgencyFull county = DALAgencyFull;
            _ds = county.GetCountyBySubstr(county_substr, CurrentYear);
            this.sql = this.sql + "||" + county.SQL;
            return _ds;
        }

        public AgencyFull GetCountyList(int year)
        {
            DALAgencyFull agency = DALAgencyFull;
            _ds = agency.GetCountyList(year);
            this.sql = this.sql + "||" + agency.SQL;
            return _ds;
        }

        public AgencyFull GetCESAList(int year)
        {
            DALAgencyFull agency = DALAgencyFull;
            _ds = agency.GetCESAList(year);
            this.sql = this.sql + "||" + agency.SQL;
            return _ds;
        }

        public AgencyFull GetAllSchoolsInCounty
            (int county, string fullKey, int year)
        {
            DALAgencyFull agency = DALAgencyFull;
            _ds = agency.GetAllSchoolsInCounty(county, fullKey, year);
            this.sql = this.sql + "||" + agency.SQL;
            return _ds;
        }

        public AgencyFull GetSelectedSchools
            (int year, string selectedSchools)
        {
            DALAgencyFull agency = DALAgencyFull;
            _ds = agency.GetSelectedSchools(year, selectedSchools);
            this.sql = this.sql + "||" + agency.SQL;
            return _ds;
        }

        public AgencyFull GetSelectedDistricts
            (int year, string selectedDistricts)
        {
            DALAgencyFull agency = DALAgencyFull;
            _ds = agency.GetSelectedDistricts(year, selectedDistricts);
            this.sql = this.sql + "||" + agency.SQL;
            return _ds;
        }
        public AgencyFull GetCountyList()
        {
            return GetCountyList(CurrentYear);

        }

        public AgencyFull GetCESAList()
        {
            return GetCESAList(CurrentYear);
        }

        public AgencyFull GetAllSchoolsInCounty
            (int county, string fullKey)
        {
            return GetAllSchoolsInCounty(county, fullKey, CurrentYear);

        }

        public AgencyFull GetSelectedSchools(string selectedSchools)
        {
            return GetSelectedSchools(CurrentYear, selectedSchools);

        }

        public AgencyFull GetSelectedDistricts(string selectedDistricts)
        {
            return GetSelectedDistricts(CurrentYear, selectedDistricts);

        }
        /// <summary>
        /// selectedAndCurrentSchools - to be excluded
        /// </summary>
        /// <param name="county"></param>
        /// <param name="selectedAndCurrentSchools"></param>
        /// <returns></returns>
        public AgencyFull GetSchoolsInCounty
            (string county, int schoolType, string selectedAndCurrentSchools)
        {
            return GetSchoolsInCounty(county, schoolType, CurrentYear, selectedAndCurrentSchools);
        }

        public AgencyFull GetSchoolsInCounty
            (string county, int schoolType, int year, string selectedAndCurrentSchools)
        {
            DALAgencyFull agency = DALAgencyFull;
            _ds = agency.GetSchoolsInCounty(
                county, schoolType, year, selectedAndCurrentSchools);
            this.sql = this.sql + "||" + agency.SQL;
            return _ds;
        }

        // - for Athelitic conference
        public AgencyFull GetSchoolsInAthlicConf
            (int conferenceKey, int schoolType,
            string selectedAndCurrentSchools)
        {
            return GetSchoolsInAthlicConf(
                conferenceKey, schoolType,
            CurrentYear, selectedAndCurrentSchools);
        }

        public AgencyFull GetSchoolsInAthlicConf
            (int conferenceKey, int schoolType,
            int year, string selectedAndCurrentSchools)
        {
            DALAgencyFull agency = DALAgencyFull;
            _ds = agency.GetSchoolsInAthlicConf(
                conferenceKey, schoolType,
                     selectedAndCurrentSchools);
            this.sql = this.sql + "||" + agency.SQL;
            return _ds;
        }

        // - for CESA 
        public AgencyFull GetSchoolsInCESA
            (string cesa, int schoolType,
            string selectedAndCurrentSchools)
        {
            return GetSchoolsInCESA(
                cesa, schoolType,
                CurrentYear, selectedAndCurrentSchools);
        }

        public AgencyFull GetSchoolsInCESA
            (string cesa, int schoolType,
            int year, string selectedAndCurrentSchools)
        {
            DALAgencyFull agency = DALAgencyFull;
            _ds = agency.GetSchoolsInCESA
                (cesa, schoolType, year, selectedAndCurrentSchools);
            this.sql = this.sql + "||" + agency.SQL;
            return _ds;
        }

        //********************************************************************************

        public AgencyFull GetDistrictsInCounty
            (string county,
            string selectedAndCurrentDistricts)
        {
            return GetDistrictsInCounty(county,
                CurrentYear, selectedAndCurrentDistricts);
        }

        public AgencyFull GetDistrictsInCounty
            (string county, int year, string selectedAndCurrentDistricts)
        {
            DALAgencyFull agency = DALAgencyFull;
            _ds = agency.GetDistrictsInCounty(
                county, year, selectedAndCurrentDistricts);
            this.sql = this.sql + "||" + agency.SQL;
            return _ds;
        }

        public AgencyFull GetDistrictsInAthlicConf
            (int conferenceKey,
             string selectedAndCurrentDistricts)
        {
            DALAgencyFull agency = DALAgencyFull;
            _ds = agency.GetDistrictsInAthlicConf(
                conferenceKey,
                     selectedAndCurrentDistricts);
            this.sql = this.sql + "||" + agency.SQL;
            return _ds;
        }

        // - for CESA 
        public AgencyFull GetDistrictsInCESA
            (string cesa,
            string selectedAndCurrentDistricts)
        {
            return GetDistrictsInCESA(
                cesa,
                CurrentYear, selectedAndCurrentDistricts);
        }

        public AgencyFull GetDistrictsInCESA
            (string cesa,
            int year, string selectedAndCurrentDistricts)
        {
            DALAgencyFull agency = DALAgencyFull;
            _ds = agency.GetDistrictsInCESA
                (cesa, year, selectedAndCurrentDistricts);
            this.sql = this.sql + "||" + agency.SQL;
            return _ds;
        }

        public AgencyFull GetAgencyByFullKey(string fullkey)
        {
            return GetAgencyByFullKey(fullkey, CurrentYear);
        }

        public AgencyFull GetAgencyByFullKey
            (string fullkey, int year)
        {
            DALAgencyFull agency = DALAgencyFull;
            _ds = agency.GetAgencyByFullKey
                (fullkey, year);
            this.sql = this.sql + "||" + agency.SQL;
            return _ds;
        }

        //********************************************************************************

        public string GetCountyNameByID(string county)
        {
            DALAgencyFull agency = DALAgencyFull;
            return agency.GetCountyNameByID(county, CurrentYear);
        }

        public string GetCESANameByID(string cesa )
        {
            DALAgencyFull agency = DALAgencyFull;
            return agency.GetCESANameByID(cesa, CurrentYear);
        }


    }
}
