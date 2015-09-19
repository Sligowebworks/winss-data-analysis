using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.DAL.WI
{
    public class DALALLAgencies : DALWIBase
    {
        private const String FROM = " FROM v_AgencyFullDistinct ";
        private const String SELECT_STAR_WHERE = "SELECT *" + FROM + "WHERE ";

        public static String GetDistrictBySubstrSQL(string district_substr)
        {
            district_substr = SQLSafeString(district_substr);

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT distinct left(fullkey,8)+'XXXX' fullkey, DistrictName, CountyName, Schoolname, LowGrade, HighGrade, AgencyType, SchoolType  " + FROM + "  WHERE AgencyTypeLabel = 'District' and WINSS = 1 AND DistrictName LIKE '%{0}%' Order By DistrictName", district_substr);
            return sql.ToString();
        }
        public static String GetSchoolBySubstrSQL(string school_substr)
        {
            school_substr = SQLSafeString(school_substr);

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT fullkey, DistrictName, CountyName, Schoolname, LowGrade, HighGrade, AgencyType, SchoolType  " + FROM + "  WHERE SchoolName IS NOT NULL AND (AgencyTypeLabel = 'School' OR AgencyTypeLabel = 'Charter school') and WINSS = 1 AND SchoolName LIKE '%{0}%' Order By Schoolname", school_substr);

            return sql.ToString();
        }
        public static String GetCountyBySubstrSQL(string county_substr)
        {
            county_substr = SQLSafeString(county_substr);

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT fullkey, DistrictName, CountyName, Schoolname, LowGrade, HighGrade, AgencyType, SchoolType " + FROM + "  WHERE AgencyTypeLabel = 'District'  AND WINSS = 1 AND CountyName LIKE '%{0}%' Order By CountyName, DistrictName", county_substr);

            return sql.ToString();
        }
        public static String GetSchoolsByStartNameSQL(string name_first_alpha)
        {
            name_first_alpha = SQLSafeString(name_first_alpha);

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT fullkey, DistrictName, CountyName, Schoolname, LowGrade, HighGrade, SchoolType, AgencyType " + FROM + "  WHERE WINSS = 1 AND SchoolName IS NOT NULL AND (AgencyTypeLabel = 'School' OR AgencyTypeLabel = 'Charter school') AND SchoolName LIKE '{0}%' Order By SchoolName,DistrictName", name_first_alpha);

            return sql.ToString();
        }
        public static String GetSchoolByDistrictKeySQL(string District_Key)
        {
            District_Key = SQLSafeString(District_Key);

            string District_Key_temp = string.Concat(District_Key.Substring(0, 6), "04");

            StringBuilder sql = new StringBuilder();

            sql.AppendFormat("SELECT distinct fullkey, DistrictName, CountyName, Schoolname, LowGrade, HighGrade, AgencyType, SchoolType  " + FROM + "  WHERE WINSS = 1 AND rtrim(ltrim(AgencyTypeLabelSort)) = 'School'  and left(fullkey,6) = left('{0}',6) and schooltype <> '' and ( left(right(fullkey,6),2) <> '14' or ( left(fullkey,8) = left('{0}',8) and rtrim(PartSchIndic) = 'PAR' ) ) Order By SchoolName, DistrictName", District_Key_temp);

            return sql.ToString();
        }
        public static String GetCountyByNameSQL(string name_first_alpha)
        {
            name_first_alpha = SQLSafeString(name_first_alpha);

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT fullkey, DistrictName, CountyName, SchoolName, LowGrade, HighGrade, AgencyType, SchoolType " + FROM + "  WHERE AgencyTypeLabel = 'District' and WINSS = 1 AND  CountyName LIKE '{0}%' Order By CountyName, DistrictName", name_first_alpha);

            return sql.ToString();
        }
        public static String GetDistrictByNameSQL(string name_first_alpha)
        {
            name_first_alpha = SQLSafeString(name_first_alpha);

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT distinct left(fullkey,8)+'XXXX' fullkey, SchoolName, DistrictName, CountyName, LowGrade, HighGrade,AgencyType, SchoolType " + FROM + "  WHERE WINSS = 1 AND AgencyTypeLabel = 'District' AND DistrictName LIKE '{0}%' Order By DistrictName", name_first_alpha);

            return sql.ToString();
        }
        public static String GetDistrictByCESASQL(string cesa_number, string var_hs)
        {
            cesa_number = SQLSafeString(cesa_number);
            var_hs = SQLSafeString(var_hs);

            StringBuilder sql = new StringBuilder();

            sql.AppendFormat("SELECT distinct left(fullkey,8)+'XXXX' fullkey, DistrictName, CountyName, LowGrade, HighGrade, AgencyType, SchoolType  " + FROM + "  WHERE WINSS = 1 AND AgencyTypeLabel = 'District' AND CESA = '{0}'", cesa_number);
            if ((var_hs == "1") || (var_hs == "0"))
            {
                if (var_hs == "1")
                {
                    sql.AppendFormat(" AND (UHSIndic = 'UHS' OR UHSIndic is Null)");
                }
                else
                {
                    sql.AppendFormat(" AND (UHSIndic = 'ELEM' OR UHSIndic is Null)");
                }
            }
            sql.AppendFormat(" Order By {0}", "DistrictName");
            return sql.ToString();
        }
        public String GetCountyNameByID(string county)
        {
            county = SQLSafeString(county);

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select distinct county, countyname "
                + FROM + "  where county = '{0}' "
                , county);

            SQL = sql.ToString(); Query();

            return GetStringColumn(v_AgencyFull.CountyName);
        }
        public string GetCESANameByID(string cesa)
        {
            cesa = SQLSafeString(cesa);

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select distinct  cesa, CesaName  "
                + FROM + "  where cesa = '{0}' ", cesa);
            SQL = sql.ToString(); Query();

            return GetStringColumn(v_AgencyFull.CESAName);
        }
    }
}
