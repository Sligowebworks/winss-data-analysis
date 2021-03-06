using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.DAL.WI
{
    public class DALAgencies : DALWIBase
    {
        private const String FROM = " FROM v_AgencyFull ";
        private const String SELECT_DISTRICT_WHERE = "SELECT DISTINCT DistrictName, fullkey " + FROM + " WHERE ";
        private const string SELECT_SCHOOL_WHERE = "SELECT DISTINCT SchoolName, DistrictName, fullkey " + FROM + " WHERE ";

        /// <summary>
        /// NOT USED AS OF Jan '10 --mzd
        /// </summary>
        /// <param name="Marshaller"></param>
        /// <returns></returns>
        public override string BuildSQL(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            throw new Exception("An error has occured. Please copy the URL from your browser and email the site administrator.");
            StringBuilder sql = new StringBuilder();
            String dbObject = "v_AgencyFull";

            sql.Append(Marshaller.SelectListFromVisibleColumns(dbObject));

            sql.Append(Marshaller.FullkeyClause(SQLHelper.WhereClauseJoiner.NONE, "FullKey"));

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "AND", Marshaller.years));

            return sql.ToString();
        }
        public  static String GetCountyListSQL()
        {
            StringBuilder sql = new StringBuilder(" SELECT DISTINCT county, CountyName ");
            sql.Append(FROM);
            sql.Append(" WHERE county is not null and county <> '74' and countyname is not null AND  agencytype = '02' ORDER BY countyname");

            return sql.ToString();
        }
        public static String GetCESAListSQL()
        {
            StringBuilder sql = new StringBuilder("SELECT DISTINCT  cesa, CesaName");
            sql.Append(FROM);
            sql.Append(" WHERE cesa is not null  ORDER BY CesaName");

            return sql.ToString();
        }
        public static String GetDistrictsInAthleticConfAndExcludeListSQL(
            int conferenceKey, string selectedAndCurrentDistrictsExcluded)
        {
            StringBuilder sql = new StringBuilder("SELECT DISTINCT fullkey, conferenceKey, DistrictName, SchoolName ");
            if (selectedAndCurrentDistrictsExcluded == string.Empty)
            {
                selectedAndCurrentDistrictsExcluded = "''";
            }
            sql.Append(FROM);

            sql.AppendFormat(" WHERE conferenceKey = {0} and left(right(fullkey,6),2) <> '14' and fullkey not in ({1}) and agencytype = '03' ", conferenceKey, selectedAndCurrentDistrictsExcluded);
          
            return sql.ToString();
        }
        public static String  GetDistrictsInCountyAndExcludeListSQL(string county, string selectedAndCurrentDistrictsExcluded)
        {
            StringBuilder sql = new StringBuilder();
            if (selectedAndCurrentDistrictsExcluded == string.Empty)
            {
                selectedAndCurrentDistrictsExcluded = "''";
            }
            sql.AppendFormat(SELECT_SCHOOL_WHERE
                    + " county = '{0}' "
                    + " and left(right(fullkey,6),2) <> '14' "
                    + " and fullkey not in ({1}) "
                    + " and agencytype = '03' "
                    + " order by DistrictName;",
                    county, selectedAndCurrentDistrictsExcluded);

            return sql.ToString();
        }
        public static String GetDistrictsInCESASQL(string cesa, string selectedAndCurrentDistrictsExcluded)
        {
            StringBuilder sql = new StringBuilder();
            if (String.IsNullOrEmpty(selectedAndCurrentDistrictsExcluded )) selectedAndCurrentDistrictsExcluded = "''";

            sql.AppendFormat(SELECT_SCHOOL_WHERE
                    + " cesa = '{0}' "
                    + " and left(right(fullkey,6),2) <> '14' "
                    + " and fullkey not in ({1}) "
                    + " and agencytype = '03' "
                    + " order by DistrictName;",
                    cesa, selectedAndCurrentDistrictsExcluded);

            return sql.ToString();
        }
        public static String GetSelectedDistrictsSQL( string selectedDistricts)
        {
            if (String.IsNullOrEmpty(selectedDistricts))
            {
                selectedDistricts = "''";
            }
            
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(SELECT_SCHOOL_WHERE + "  agencytype = '03' and fullkey in ({0}) order by [DistrictName]", selectedDistricts);

            return sql.ToString();
        }
        public static String GetSchoolsInCountySQL(string county, int schoolType, string selectedAndCurrentSchools)
        {
            StringBuilder sql = new StringBuilder();
            if (selectedAndCurrentSchools == string.Empty)
            {
                selectedAndCurrentSchools = "''";
            }
            sql.AppendFormat(SELECT_SCHOOL_WHERE
                    + "  county = '{0}' "
                    + " and right(fullkey,1) <> 'X' and Schooltype = {1} "
                    + " and left(right(fullkey,6),2) <> '14' "
                    + " and fullkey not in ({2}) "
                    + " order by DistrictName;",
                    county, schoolType, selectedAndCurrentSchools);

            return sql.ToString();
        }
        public static String GetSchoolsInAthleticConfSQL(int conferenceKey, int schoolType,  string selectedAndCurrentSchools)
        {
            StringBuilder sql = new StringBuilder();
            if (selectedAndCurrentSchools == string.Empty)
            {
                selectedAndCurrentSchools = "''";
            }

            sql.AppendFormat(SELECT_SCHOOL_WHERE
                    + " right(fullkey,1) <> 'X'  "
                    + " and left(right(fullkey,6),2) <> '14'  "
                    + " and fullkey not in ({0})  "
                    + " and Schooltype = {1} "
                    + " and ConferenceKey = {2} "
                    + " order by DistrictName;",
                      selectedAndCurrentSchools, schoolType, conferenceKey);

            return sql.ToString();
        }
        public static String GetSchoolsInCESASQL(string cesa, int schoolType, string selectedAndCurrentSchools)
        {
            StringBuilder sql = new StringBuilder();
            if (selectedAndCurrentSchools == string.Empty)
            {
                selectedAndCurrentSchools = "''";
            }
            sql.AppendFormat(SELECT_SCHOOL_WHERE
                    + " cesa = '{0}'  "
                    + " and right(fullkey,1) <> 'X' and Schooltype = {1} "
                    + " and left(right(fullkey,6),2) <> '14' "
                    + " and fullkey not in ({2}) "
                    + " order by SchoolName;",
                    cesa, schoolType, selectedAndCurrentSchools);

            return sql.ToString();
        }
        public static String GetSelectedSchoolsSQL(string selectedSchools)
        {
            StringBuilder sql = new StringBuilder();
            if (String.IsNullOrEmpty(selectedSchools))
            {
                selectedSchools = "''";
            }

            // if (selectedSchools != null && selectedSchools.Length > 11)
            sql.AppendFormat(SELECT_SCHOOL_WHERE 
                + "  schooltype is not null and fullkey in ({0}) "
                +"order by [SchoolName]", selectedSchools);

            return sql.ToString();
        }
    }
}
