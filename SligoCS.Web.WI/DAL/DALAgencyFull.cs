using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.Web.WI.DAL.DataSets;

namespace SligoCS.DAL.WI
{
    public class DALAgencyFull : DALWIBase
    {
        protected string _sqlTableName;

        public DALAgencyFull()
            : this("")
        {

        }

        public DALAgencyFull(string DataSetType)
        {
            if (DataSetType.Equals("DISTINCT"))
            {
                _sqlTableName = "v_AgencyFullDistinct";
            }
            else
            {
                //ThisDataSet = new AgencyFull();
                _sqlTableName = "v_AgencyFull";
            }
        }

        public AgencyFull GetSchool(string fullKey, int year)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT fullkey, DistrictName, CountyName, SchoolName, LowGrade, HighGrade, SchoolType, AgencyType, DistrictWebAddress, SchoolWebAddress FROM " + _sqlTableName + "  WHERE WINSS = 1 AND fullkey like '{0}%'", fullKey);
            sql.AppendFormat(" AND year = {0}", year);

            AgencyFull ds = new AgencyFull();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull.TableName);
            return ds;
        }

        public AgencyFull GetSchoolByName(string name_first_alpha, int year)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT fullkey, DistrictName, CountyName, Schoolname, LowGrade, HighGrade, SchoolType, AgencyType FROM " + _sqlTableName + "  WHERE WINSS = 1 AND SchoolName IS NOT NULL AND (AgencyTypeLabel = 'School' OR AgencyTypeLabel = 'Charter school') AND Year = {1} AND SchoolName LIKE '{0}%' Order By SchoolName,DistrictName", name_first_alpha, year.ToString());

            AgencyFull ds = new AgencyFull();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull.TableName);
            return ds;
        }

        public AgencyFull GetSchoolByDistrictKey(string District_Key, int year)
        {

            string District_Key_temp = string.Concat(District_Key.Substring(0, 6), "04");

            StringBuilder sql = new StringBuilder();

            sql.AppendFormat("SELECT distinct fullkey, DistrictName, CountyName, Schoolname, LowGrade, HighGrade, AgencyType, SchoolType FROM " + _sqlTableName + "  WHERE WINSS = 1 AND rtrim(ltrim(AgencyTypeLabelSort)) = 'School'  AND Year = {1} and left(fullkey,6) = left('{0}',6) and schooltype <> '' and ( left(right(fullkey,6),2) <> '14' or ( left(fullkey,8) = left('{0}',8) and rtrim(PartSchIndic) = 'PAR' ) ) Order By SchoolName, DistrictName", District_Key_temp, year.ToString());
            AgencyFull ds = new AgencyFull();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull.TableName);
            return ds;
        }

        public AgencyFull GetDistrictByName(string name_first_alpha, int year)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT distinct left(fullkey,8)+'XXXX' fullkey, DistrictName, CountyName, LowGrade, HighGrade,AgencyType, SchoolType FROM " + _sqlTableName + "  WHERE WINSS = 1 AND Year = {1} AND AgencyTypeLabel = 'District' AND DistrictName LIKE '{0}%' Order By DistrictName", name_first_alpha, year.ToString());

            AgencyFull ds = new AgencyFull();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull.TableName);
            return ds;
        }

        public AgencyFull GetDistrictByCESA(string cesa_number, string var_hs, int year)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat("SELECT distinct left(fullkey,8)+'XXXX' fullkey, DistrictName, CountyName, LowGrade, HighGrade, AgencyType, SchoolType FROM " + _sqlTableName + "  WHERE WINSS = 1 AND Year = {1} AND AgencyTypeLabel = 'District' AND CESA = '{0}'", cesa_number, year.ToString());
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
            AgencyFull ds = new AgencyFull();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull.TableName);
            return ds;
        }

        public AgencyFull GetCountyByName(string name_first_alpha, int year)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT fullkey, DistrictName, CountyName, Schoolname, LowGrade, HighGrade, AgencyType, SchoolType FROM " + _sqlTableName + "  WHERE AgencyTypeLabel = 'District' and WINSS = 1 AND Year = {1} AND CountyName LIKE '{0}%' Order By CountyName, DistrictName", name_first_alpha,  year.ToString());
            AgencyFull ds = new AgencyFull();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull.TableName);
            return ds;
        }

        public AgencyFull GetSchoolBySubstr(string school_substr, int year)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT fullkey, DistrictName, CountyName, Schoolname, LowGrade, HighGrade, AgencyType, SchoolType FROM " + _sqlTableName + "  WHERE SchoolName IS NOT NULL AND AgencyTypeLabel = 'School' and WINSS = 1 AND Year = {1} AND SchoolName LIKE '%{0}%' Order By Schoolname", school_substr, year.ToString());

            AgencyFull ds = new AgencyFull();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull.TableName);
            return ds;
        }

        public AgencyFull GetDistrictBySubstr(string district_substr, int year)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT distinct left(fullkey,8)+'XXXX' fullkey, DistrictName, CountyName, Schoolname, LowGrade, HighGrade, AgencyType, SchoolType FROM " + _sqlTableName + "  WHERE AgencyTypeLabel = 'District' and WINSS = 1 AND Year = {1} AND DistrictName LIKE '%{0}%' Order By DistrictName", district_substr, year.ToString());
            AgencyFull ds = new AgencyFull();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull.TableName);
            return ds;
        }

        public AgencyFull GetCountyBySubstr(string county_substr, int year)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT fullkey, DistrictName, CountyName, LowGrade, HighGrade, AgencyType, SchoolType FROM " + _sqlTableName + "  WHERE AgencyTypeLabel = 'District' and YEAR = {1} AND WINSS = 1 AND CountyName LIKE '%{0}%' Order By CountyName, DistrictName", county_substr, year.ToString());
            AgencyFull ds = new AgencyFull();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull.TableName);
            return ds;
        }

        public AgencyFull GetSchoolByCounty(string name_first_alpha, int year)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT fullkey, DistrictName, CountyName, Schoolname, LowGrade, HighGrade, SchoolType, AgencyType FROM " + _sqlTableName + "  WHERE WINSS = 1 AND SchoolName IS NOT NULL AND (AgencyTypeLabel = 'School' OR AgencyTypeLabel = 'Charter school') AND Year = {1} AND SchoolName LIKE '{0}%' Order By SchoolName,DistrictName", name_first_alpha, year.ToString());

            AgencyFull ds = new AgencyFull();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull.TableName);
            return ds;
        }

        public AgencyFull GetSelectedSchools(int year, string selectedSchools)
        {
            StringBuilder sql = new StringBuilder();
            AgencyFull ds = new AgencyFull();
            if (selectedSchools != null && selectedSchools.Length > 11)
            {
                sql.AppendFormat("select * FROM " + _sqlTableName + "  where [year]= {0} and schooltype is not null and fullkey in ({1}) order by [SchoolName]", year, selectedSchools);
            }
            else
            {
                //sql.AppendFormat("select * FROM " + _sqlTableName + "  where [year]= {0} and schooltype is not null and fullkey in ('000000000000') order by [SchoolName]", year);
                return ds;
            }

            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull.TableName);
            return ds;
        }

        public AgencyFull GetSelectedDistricts(int year, string selectedDistricts)
        {
            StringBuilder sql = new StringBuilder();
            AgencyFull ds = new AgencyFull();
            if (selectedDistricts != null && selectedDistricts.Length > 11)
            {
                sql.AppendFormat("select * FROM " + _sqlTableName + "  where [year]= {0} and agencytype = '03' and fullkey in ({1}) order by [DistrictName]", year, selectedDistricts);
            }
            else
            {
                return ds;
            }

            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull.TableName);
            return ds;
        }


        public AgencyFull GetCountyList(int year)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select distinct county, countyname FROM " + _sqlTableName + "  where [year]= {0}  and county is not null and county <> '74' and countyname is not null order by countyname", year);

            AgencyFull ds = new AgencyFull();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull.TableName);
            return ds;
        }

        public AgencyFull GetCESAList(int year)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select distinct cesa, CesaName FROM " + _sqlTableName + "  where [year]= {0} and cesa is not null  order by CesaName", year);

            AgencyFull ds = new AgencyFull();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull.TableName);
            return ds;
        }
        public AgencyFull GetAllSchoolsInCounty(int county, string fullKey, int year)
        {
            StringBuilder sql = new StringBuilder();
           // sql.AppendFormat("select year ,YearFormatted,fullkey, schooltype, [School paramName] FROM v_ACT where county = '{0}' or fullkey = '{1}' and [year]= {2} and cesa is not null order by [School paramName]", county, fullKey, year);
            sql.AppendFormat("select * FROM " + _sqlTableName + "  where ( county = '{0}' and [year]= {2} )or ( fullkey = '{1}' and [year]= {2} ) and schooltype is not null order by [SchoolName]", county,  fullKey, year);

            AgencyFull ds = new AgencyFull();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull.TableName);
            return ds;
        }

        public AgencyFull GetSchoolsInCounty(string county, int schoolType,
            int year, string selectedAndCurrentSchools)
        {
            StringBuilder sql = new StringBuilder();
            if (selectedAndCurrentSchools == string.Empty)
            {
                selectedAndCurrentSchools = "''";
            }
                sql.AppendFormat( "select * FROM " + _sqlTableName + "  " 
                        + " where county = '{0}' and [year]= {1} "
                        + " and right(fullkey,1) <> 'X' and Schooltype = {2} "
                        + " and left(right(fullkey,6),2) <> '14' "
                        + " and fullkey not in ({3}) "
                        + " order by name;",
                        county, year, schoolType, selectedAndCurrentSchools);

            AgencyFull ds = new AgencyFull();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull.TableName);
            return ds;
        }


        public AgencyFull GetSchoolsInAthlicConf(
            int conferenceKey, int schoolType,
             string selectedAndCurrentSchools)
        {
            StringBuilder sql = new StringBuilder();
            if (selectedAndCurrentSchools == string.Empty)
            {
                selectedAndCurrentSchools = "''";
            }

            sql.AppendFormat("select * FROM " + _sqlTableName + "   "
                    + " where  "
                    + " [year]=  (select max(year) from Athletic_Conf) "
                    + " and right(fullkey,1) <> 'X'  "
                    + " and left(right(fullkey,6),2) <> '14'  "
                    + " and fullkey not in ({0})  "
                    + " and Schooltype = {1} "
                    + " and ConferenceKey = {2} "
                    + " order by name;",
                      selectedAndCurrentSchools, schoolType, conferenceKey);

            AgencyFull ds = new AgencyFull();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull.TableName);
            return ds;
        }

        public AgencyFull GetSchoolsInCESA(
            string cesa, int schoolType,
            int year, string selectedAndCurrentSchools)
        {
            StringBuilder sql = new StringBuilder();
            if (selectedAndCurrentSchools == string.Empty)
            {
                selectedAndCurrentSchools = "''";
            }
            sql.AppendFormat("select * FROM " + _sqlTableName + "  "
                    + " where cesa = '{0}' and [year]= {1} "
                    + " and right(fullkey,1) <> 'X' and Schooltype = {2} "
                    + " and left(right(fullkey,6),2) <> '14' "
                    + " and fullkey not in ({3}) "
                    + " order by name;",
                    cesa, year, schoolType, selectedAndCurrentSchools);

            AgencyFull ds = new AgencyFull();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull.TableName);
            return ds;
        }
        //********************************************************************************


        public AgencyFull GetDistrictsInCounty(string county,
            int year, string selectedAndCurrentDistricts)
        {
            StringBuilder sql = new StringBuilder();
            if (selectedAndCurrentDistricts == string.Empty)
            {
                selectedAndCurrentDistricts = "''";
            }
            sql.AppendFormat("select * FROM " + _sqlTableName + "  "
                    + " where county = '{0}' and [year]= {1} "
                    + " and left(right(fullkey,6),2) <> '14' "
                    + " and fullkey not in ({2}) "
                    + " and agencytype = '03' "
                    + " order by name;",
                    county, year, selectedAndCurrentDistricts);

            AgencyFull ds = new AgencyFull();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull.TableName);
            return ds;
        }


        public AgencyFull GetDistrictsInAthlicConf(
            int conferenceKey,  string selectedAndCurrentDistricts)
        {
            StringBuilder sql = new StringBuilder();
            if (selectedAndCurrentDistricts == string.Empty)
            {
                selectedAndCurrentDistricts = "''";
            }
            sql.AppendFormat("select * FROM " + _sqlTableName + "  "
                    + " where conferenceKey = {0} and [year]=  (select max(year) from Athletic_Conf) "
                    + " and left(right(fullkey,6),2) <> '14' "
                    + " and fullkey not in ({1}) "
                    + " and agencytype = '03' "
                    +" order by name;",
                    conferenceKey,  selectedAndCurrentDistricts);

            AgencyFull ds = new AgencyFull();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull.TableName);
            return ds;
        }

        public AgencyFull GetDistrictsInCESA(
            string cesa, int year, string selectedAndCurrentDistricts)
        {
            StringBuilder sql = new StringBuilder();
            if (selectedAndCurrentDistricts == string.Empty)
            {
                selectedAndCurrentDistricts = "''";
            }
            sql.AppendFormat("select * FROM " + _sqlTableName + "  "
                    + " where cesa = '{0}' and [year]= {1} "
                    + " and left(right(fullkey,6),2) <> '14' "
                    + " and fullkey not in ({2}) "
                    + " and agencytype = '03' "
                    +" order by name;",
                    cesa, year, selectedAndCurrentDistricts);

            AgencyFull ds = new AgencyFull();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull.TableName);
            return ds;
        }

        public AgencyFull GetAgencyByFullKey(string fullkey, int year)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * FROM " + _sqlTableName + "  "
                    + " where [year]={0} and fullkey = '{1}' ", year, fullkey);

            AgencyFull ds = new AgencyFull();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull.TableName);
            return ds;
        }
        //********************************************************************************

        public string GetCountyNameByID(string county, int year)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select distinct county, countyname "
                +"FROM " + _sqlTableName + "  where [year]= {0}  and county = '{1}' "
                , year, county);

            AgencyFull ds = new AgencyFull();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull.TableName);
            return  GetScalarStringByID( ds, ds._v_AgencyFull.CountyNameColumn.ColumnName);
        }

        //public string GetAthleticConfNameByID(int conferenceKey, int year)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    sql.AppendFormat("select distinct county, countyname "
        //        + "FROM " + _sqlTableName + "  where [year]= {0}  and conferenceKey = '{1}' "
        //        , year, conferenceKey);

        //    AgencyFull ds = new AgencyFull();
        //    base.GetDS(ds, sql.ToString(), ds._v_AgencyFull.TableName);
        //    return GetScalarStringByID(ds, ds._v_AgencyFull.a.ColumnName);
        //}

        public string GetCESANameByID(string cesa, int year)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select distinct  cesa, CesaName  "
                + "FROM " + _sqlTableName + "  where [year]= {0}  and cesa = '{1}' "
                , year, cesa);

            AgencyFull ds = new AgencyFull();
            base.GetDS(ds, sql.ToString(), ds._v_AgencyFull.TableName);
            return GetScalarStringByID(ds, ds._v_AgencyFull.CESANameColumn.ColumnName);
        }

    }
}