using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.DAL.WI
{
    public class DALTeacherQualifications : DALWIBase
    {
       /* public v_TeacherQualifications GetTeacherQualifications(List<int> raceCode,
            List<int> sexCode,
            List<int> year,
            List<string> fullKey,
            List<int> schoolType,
            string subjectCode,
            string clauseForCompareToSchoolsDistrict,
            bool useFullkeys,
            List<string> orderBy)*/
        public override string  BuildSQL(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            //v_TeacherQualifications ds = new v_TeacherQualifications();

            StringBuilder sql = new StringBuilder();
            String dbObject = "v_TeacherQualifications";

            sql.Append(SQLHelper.SelectStarFromWhereFormat(dbObject));

            sql.Append(Marshaller.STYPClause(SQLHelper.WhereClauseJoiner.NONE, "SchoolTypeCode", dbObject));

            //Adds " ... AND (SexCode in (1, 2)) ..."
            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "SexCode", Marshaller.sexCodes));

            //Adds " ... AND (RaceCode in (1, 2, 3, 4, 5)) ..."
            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "RaceCode", Marshaller.raceCodes));

            //Adds " ... AND ((year >= 1997) AND (year <= 2007)) ..."
            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.AND, "year", Marshaller.years));

            //fullkey
            if (Marshaller.compareSelectedFullKeys)
            {
                sql.Append(" and ").Append(Marshaller.clauseForCompareSelected);
            }
            else
            {
                sql.Append(SQLHelper.WhereClauseValuesInList(
                    SQLHelper.WhereClauseJoiner.AND, "FullKey", Marshaller.fullkeylist));
            }
            

            //LinkSubjectCode
            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "LinkSubjectCode", Marshaller.TQSubjectCodes));

            //order by clause
            //sb.AppendFormat(" ORDER BY {0}", SQLHelper.ConvertToCSV(orderBy, false));
            sql.Append(SQLHelper.GetOrderByClause(Marshaller.orderByList));

            return sql.ToString();
        }

    }    
}
