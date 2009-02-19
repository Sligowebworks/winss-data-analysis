using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI.DataSets;

namespace SligoCS.DAL.WI
{
    public class DALTeacherQualifications : DALWIBase
    {
        public v_TeacherQualifications GetTeacherQualifications(List<int> raceCode,
            List<int> sexCode,
            List<int> year,
            List<string> fullKey,
            List<int> schoolType,
            string subjectCode,
            string clauseForCompareToSchoolsDistrict,
            bool useFullkeys,
            List<string> orderBy)
        {
            v_TeacherQualifications ds = new v_TeacherQualifications();

            StringBuilder sb = new StringBuilder();
            sb.Append("select * from v_TeacherQualifications where ");

            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.NONE, "SchoolTypeCode", schoolType));


            //Adds " ... AND (SexCode in (1, 2)) ..."
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "SexCode", sexCode));

            //Adds " ... AND (RaceCode in (1, 2, 3, 4, 5)) ..."
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "RaceCode", raceCode));

            //Adds " ... AND ((year >= 1997) AND (year <= 2007)) ..."
            sb.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.AND, "year", year));

            //fullkey
            if (useFullkeys)
            {
                sb.Append(SQLHelper.WhereClauseValuesInList(
                    SQLHelper.WhereClauseJoiner.AND, "FullKey", fullKey));
            }
            else
            {
                sb.Append(" and ").Append(clauseForCompareToSchoolsDistrict);
            }

            //LinkSubjectCode
            sb.Append(SQLHelper.WhereClauseEquals (SQLHelper.WhereClauseJoiner.AND, "LinkSubjectCode", subjectCode));

            //order by clause
            //sb.AppendFormat(" ORDER BY {0}", SQLHelper.ConvertToCSV(orderBy, false));
            sb.Append(SQLHelper.GetOrderByClause(orderBy));
            
            base.GetDS(ds, sb.ToString(), ds._v_TeacherQualifications.TableName);
            return ds;
        }

    }    
}
