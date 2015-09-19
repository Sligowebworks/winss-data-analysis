using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI.DataSets;

namespace SligoCS.DAL.WI
{
    public class DALAgencyFull_2006 : DALWIBase
    {
        public v_AgencyFull_2006 GetSchool(string fullKey, int year)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT fullkey, DistrictName, CountyName, SchoolName, LowGrade, HighGrade, SchoolType, AgencyType FROM v_AgencyFull_2006 WHERE fullkey like '{0}%'", fullKey);
            sql.AppendFormat(" AND year = {0}", year);
            v_AgencyFull_2006 ds = new v_AgencyFull_2006();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull_2006.TableName);
            return ds;
        }

        public v_AgencyFull_2006 GetSchoolByName(string name_first_alpha)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT fullkey, DistrictName, CountyName, Schoolname, LowGrade, HighGrade, SchoolType, AgencyType FROM v_AgencyFull_2006 WHERE SchoolName IS NOT NULL AND AgencyType > '03' AND Year = 2006 AND SchoolName LIKE '{0}%' Order By SchoolName,DistrictName", name_first_alpha);
            /*
            sql.AppendFormat("SELECT fullkey, DistrictName, CountyName, Schoolname, LowGrade, HighGrade, AgencyType FROM v_AgencyFull_2006 WHERE SchoolName IS NOT NULL AND DistrictName IS NOT NULL AND CountyName IS NOT NULL AND Year = 2006 AND SchoolName LIKE '{0}%' Order By SchoolName,DistrictName", name_first_alpha);
             */
            // sql.AppendFormat(" Order By {0}", "SchoolName,DistrictName");
            v_AgencyFull_2006 ds = new v_AgencyFull_2006();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull_2006.TableName);
            return ds;
        }

        public v_AgencyFull_2006 GetSchoolByDistrictKey(string District_Key)
        {

            string District_Key_temp = string.Concat(District_Key.Substring(0, 6),"04");
            //string District_Key_temp = "07329004";

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT fullkey, DistrictName, CountyName, Schoolname, LowGrade, HighGrade, AgencyType, SchoolType FROM v_AgencyFull_2006 WHERE fullkey LIKE '{0}%' Order By SchoolName, DistrictName", District_Key_temp);
            // sql.AppendFormat(" Order By {0}", "SchoolName, DistrictName");
            v_AgencyFull_2006 ds = new v_AgencyFull_2006();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull_2006.TableName);
            return ds;
        }
        public v_AgencyFull_2006 GetDistrictByName(string name_first_alpha)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT distinct left(fullkey,8)+'ZZZZ' fullkey, DistrictName, CountyName, LowGrade, HighGrade,AgencyType, SchoolType FROM v_AgencyFull_2006  WHERE Year = 2006 AND AgencyType in ('03','49') AND DistrictName LIKE '{0}%' Order By DistrictName", name_first_alpha);
            /*
sql.AppendFormat("SELECT fullkey, DistrictName, CountyName, Schoolname, LowGrade, HighGrade, AgencyType FROM v_AgencyFull_2006 WHERE SchoolName IS NULL AND DistrictName IS NOT NULL AND CountyName IS NOT NULL AND Year = 2006 AND DIstrictName LIKE '{0}%'Order By DistrictName", name_first_alpha);
             */
            // sql.AppendFormat(" Order By {0}", "DistrictName");
            v_AgencyFull_2006 ds = new v_AgencyFull_2006();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull_2006.TableName);
            return ds;
        }
        public v_AgencyFull_2006 GetDistrictByCESA(string cesa_number, string var_hs)
        {
            StringBuilder sql = new StringBuilder();
            /*
            sql.AppendFormat("SELECT fullkey, DistrictName, CountyName, LowGrade, HighGrade, AgencyType FROM v_AgencyFull_2006 WHERE SchoolName IS NULL AND DistrictName IS NOT NULL AND CountyName IS NOT NULL AND Year = 2006 AND CESA = '{0}'", cesa_number);
            */
            sql.AppendFormat("SELECT distinct left(fullkey,8)+'ZZZZ' fullkey, DistrictName, CountyName, LowGrade, HighGrade, AgencyType, SchoolType FROM v_AgencyFull_2006  WHERE Year = 2006 AND AgencyType in ('03','49') AND CESA = '{0}'", cesa_number);
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
            v_AgencyFull_2006 ds = new v_AgencyFull_2006();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull_2006.TableName);
            return ds;
        }

        public v_AgencyFull_2006 GetCountyByName(string name_first_alpha)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT fullkey, DistrictName, CountyName, Schoolname, LowGrade, HighGrade, AgencyType, SchoolType FROM v_AgencyFull_2006 WHERE AgencyType = '03' AND Year = 2006 AND CountyName LIKE '{0}%' Order By CountyName, DistrictName", name_first_alpha);
            /*
            sql.AppendFormat("SELECT fullkey, DistrictName, CountyName, Schoolname, LowGrade, HighGrade, AgencyType FROM v_AgencyFull_2006 WHERE SchoolName IS NULL AND DistrictName IS NOT NULL AND CountyName IS NOT NULL AND Year = 2006 AND CountyName LIKE '{0}%' Order By CountyName, DistrictName", name_first_alpha);
             */
            // sql.AppendFormat(" Order By {0}", "CountyName, DistrictName");
            v_AgencyFull_2006 ds = new v_AgencyFull_2006();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull_2006.TableName);
            return ds;
        }
        public v_AgencyFull_2006 GetSchoolBySubstr(string school_substr)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT fullkey, DistrictName, CountyName, Schoolname, LowGrade, HighGrade, AgencyType, SchoolType FROM v_AgencyFull_2006 WHERE SchoolName IS NOT NULL AND AgencyType > '03' AND Year = 2006 AND SchoolName LIKE '%{0}%' Order By Schoolname", school_substr);
            /*
            sql.AppendFormat("SELECT fullkey, DistrictName, CountyName, Schoolname, LowGrade, HighGrade, AgencyType FROM v_AgencyFull_2006 WHERE CountyName IS NOT NULL AND DistrictName IS NOT NULL AND SchoolName IS NOT NULL AND Year = 2006 AND SchoolName LIKE '%{0}%' Order By Schoolname", school_substr);
             */
            // sql.AppendFormat(" Order By {0}", "Schoolname");
            v_AgencyFull_2006 ds = new v_AgencyFull_2006();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull_2006.TableName);
            return ds;
        }
        public v_AgencyFull_2006 GetDistrictBySubstr(string district_substr)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT distinct left(fullkey,8)+'ZZZZ' fullkey, DistrictName, CountyName, Schoolname, LowGrade, HighGrade, AgencyType, SchoolType FROM v_AgencyFull_2006 WHERE AgencyType = '03' AND Year = 2006 AND DistrictName LIKE '%{0}%' Order By DistrictName", district_substr);
            /* sql.AppendFormat("SELECT fullkey, DistrictName, CountyName, Schoolname, LowGrade, HighGrade, AgencyType FROM v_AgencyFull_2006 WHERE CountyName IS NOT NULL AND DistrictName IS NOT NULL AND SchoolName IS NULL AND Year = 2006 AND DistrictName LIKE '%{0}%' Order By DistrictName", district_substr); */
            // sql.AppendFormat(" Order By {0}", "DistrictName");
            v_AgencyFull_2006 ds = new v_AgencyFull_2006();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull_2006.TableName);
            return ds;
        }
        public v_AgencyFull_2006 GetCountyBySubstr(string county_substr)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT fullkey, DistrictName, CountyName, LowGrade, HighGrade, AgencyType, SchoolType FROM v_AgencyFull_2006 WHERE AgencyType = '03' AND CountyName LIKE '%{0}%' Order By CountyName, DistrictName", county_substr);
            /*
                sql.AppendFormat("SELECT fullkey, DistrictName, CountyName, LowGrade, HighGrade, AgencyType FROM v_AgencyFull_2006 WHERE CountyName IS NOT NULL AND DistrictName IS NOT NULL  AND Year = 2006 AND SchoolName IS NULL  AND CountyName LIKE '%{0}%' Order By CountyName, DistrictName", county_substr);
             */
            // sql.AppendFormat(" Order By {0}", "CountyName");
            v_AgencyFull_2006 ds = new v_AgencyFull_2006();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull_2006.TableName);
            return ds;
        }
    }
}
