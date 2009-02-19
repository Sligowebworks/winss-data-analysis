using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI.DataSets;

namespace SligoCS.DAL.WI
{
    public class DALRetention : DALWIBase
    {
        public v_RetentionWWoDisEconELPSchoolDistState 
            GetRetentionData2(
                List<int> raceCode,
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
            v_RetentionWWoDisEconELPSchoolDistState ds = new v_RetentionWWoDisEconELPSchoolDistState();

            StringBuilder sb = new StringBuilder();
            sb.Append("select * from v_RetentionWWoDisEconELPSchoolDistState where ");

            SQLHelper sql = new SQLHelper();

            sb.Append(sql.WhereClauseCSV(SQLHelper.WhereClauseJoiner.NONE, "SchoolType", schoolType));

            //Adds " ... AND (SexCode in (1, 2)) ..."
            sb.Append(sql.WhereClauseCSV(SQLHelper.WhereClauseJoiner.AND, "SexCode", sexCode));

            //Adds " ... AND (RaceCode in (1, 2, 3, 4, 5)) ..."
            sb.Append(sql.WhereClauseCSV(SQLHelper.WhereClauseJoiner.AND, "RaceCode", raceCode));

            //Adds " ... AND ((GradeCode >= 16) AND (GradeCode <= 64)) ..."
            sb.Append(sql.WhereClauseMinAndMaxInclusive(SQLHelper.WhereClauseJoiner.AND, "GradeCode", gradeCodeRange));

            //Adds " ... AND (DisabilityCode in (1, 2)) ..."
            sb.Append(sql.WhereClauseCSV(SQLHelper.WhereClauseJoiner.AND, "DisabilityCode", disabilityCode));

            //BR 
            //Adds " ... AND (EconDisadvCode in (1, 2)) ..."
            sb.Append(sql.WhereClauseCSV(SQLHelper.WhereClauseJoiner.AND, "EconDisadvCode", econDisadv));

            //Adds " ... AND (ELPCode in (1, 2)) ..."
            sb.Append(sql.WhereClauseCSV(SQLHelper.WhereClauseJoiner.AND, "ELPCode", ELPCode));


            //Adds " ... AND ((year >= 1997) AND (year <= 2007)) ..."
            sb.Append(sql.WhereClauseMinAndMaxInclusive(SQLHelper.WhereClauseJoiner.AND, "year", year));

            if (useFullkeys)
            {
                sb.Append(sql.WhereClauseCSV(
                    SQLHelper.WhereClauseJoiner.AND, "FullKey", fullKey));
            }
            else
            {
                sb.Append(" and ").Append( clauseForCompareToSchoolsDistrict );
            }

            //order by clause
            //sb.AppendFormat(" ORDER BY {0}", sql.ConvertToCSV(orderBy, false));
            sb.Append(sql.GetOrderByClause(orderBy));

            base.GetDS(ds, sb.ToString(), ds._v_RetentionWWoDisEconELPSchoolDistState.TableName);
            return ds;
        }

    }
}
