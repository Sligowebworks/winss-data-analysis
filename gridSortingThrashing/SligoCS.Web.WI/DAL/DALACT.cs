using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.Web.WI.DAL.DataSets;

namespace SligoCS.DAL.WI
{
    public class DALACT : DALWIBase
    {

        public v_ACT GetACTData(List<int> raceCode,
            List<int> sexCode,
            List<int> disabilityCode,
            List<int> year,
            List<string> fullKey,
            List<int> gradeCodeRange,
            List<int> schoolType,
            List<int> econDisadv,
            List<int> ELPCode,
            string clauseForCompareToSchoolsDistrict,
            bool useFullkeys,
            List<string> orderBy)
        {
            v_ACT ds = new v_ACT();

            StringBuilder sb = new StringBuilder();
            sb.Append("select * from v_ACT where ");

            //sb.Append(SQLHelper.WhereClauseCSV(SQLHelper.WhereClauseJoiner.NONE, "STYP", schoolType));
            
            ////Adds " ... AND (SexCode in (1, 2)) ..."
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.NONE, "Sex", sexCode));

            ////Adds " ... AND (RaceCode in (1, 2, 3, 4, 5)) ..."
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "Race", raceCode));

            ////Adds " ... AND ((GradeCode >= 16) AND (GradeCode <= 64)) ..."
            //sb.Append(SQLHelper.WhereClauseMinAndMaxInclusive(SQLHelper.WhereClauseJoiner.AND, "GradeCode", gradeCodeRange));

            ////Adds " ... AND (DisabilityCode in (1, 2)) ..."
            //sb.Append(SQLHelper.WhereClauseCSV(SQLHelper.WhereClauseJoiner.AND, "DisabilityCode", disabilityCode));

            ////Adds " ... AND (EconDisadvCode in (1, 2)) ..."
            ////sb.Append(SQLHelper.WhereClauseCSV(SQLHelper.WhereClauseJoiner.AND, "EconDisadvCode", econDisadv));

            ////Adds " ... AND (ELPCode in (1, 2)) ..."
            ////sb.Append(SQLHelper.WhereClauseCSV(SQLHelper.WhereClauseJoiner.AND, "ELPCode", ELPCode));


            ////Adds " ... AND ((year >= 1997) AND (year <= 2007)) ..."
            sb.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.AND, "year", year));

            if (useFullkeys)
            {
                sb.Append(SQLHelper.WhereClauseValuesInList(
                    SQLHelper.WhereClauseJoiner.AND, "FullKey", fullKey));
            }
            else
            {
                sb.Append(" and ").Append( clauseForCompareToSchoolsDistrict );
            }

            //order by clause
            //sb.AppendFormat(" ORDER BY {0}", SQLHelper.ConvertToCSV(orderBy, false));
            sb.Append(SQLHelper.GetOrderByClause(orderBy));

            base.GetDS(ds, sb.ToString(), ds._v_ACT.TableName);
            return ds;
        }

        public v_ACT GetAllSchoolsInCounty(string county, string fullKey, int year)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select year ,YearFormatted,fullkey, schooltype, [School Name] FROM v_ACT where county = '{0}' or fullkey = '{1}'  [year]= {2}", county, fullKey, year);

            v_ACT ds = new v_ACT();
            base.GetDS(ds, sql.ToString(), ds._v_ACT.TableName);
            return ds;
        }

        public v_ACT GetAllSchoolsInAthleticConf(string ConferenceKey, string fullKey, int year)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select year ,YearFormatted,fullkey, schooltype, [School Name] FROM v_ACT where ConferenceKey = '{0}' or fullkey = '{1}'  [year]= {2}", ConferenceKey, fullKey, year);

            v_ACT ds = new v_ACT();
            base.GetDS(ds, sql.ToString(), ds._v_ACT.TableName);
            return ds;
        }

        public v_ACT GetAllSchoolsInCESA(string CESA, string fullKey, int year)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select year ,YearFormatted,fullkey, schooltype, [School Name] FROM v_ACT where CESA = '{0}' or fullkey = '{1}'  [year]= {2}", CESA, fullKey, year);

            v_ACT ds = new v_ACT();
            base.GetDS(ds, sql.ToString(), ds._v_ACT.TableName);
            return ds;
        }

    }
}

//For all schools in a county 
//(vACT.county = '47' or vACT.fullkey = '032527040040' )
//(vACT.county = '47' or vACT.fullkey = '03252703XXXX' )


//For all schools in a Athletic Conference
//(ConferenceKey = '45' or vACT.fullkey =

//For all schools in a cesa
//(vACT.CESA = '05' or vACT.fullkey = '032527040040' )