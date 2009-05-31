using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.Web.WI.DAL.DataSets;

namespace SligoCS.DAL.WI
{
    public class DALPOST_GRAD_INTENT : DALWIBase
    {

        public v_POST_GRAD_INTENT GetPostGradIntent(List<int> raceCode,
            List<int> sexCode,
            List<int> disabilityCode,
            List<int> year,
            List<string> fullKey,
            List<int> gradeCodeRange,
            List<int> schoolType,
            List<int> econDisadv,
            List<int> ELPCode,
            List<string> orderBy)
        {
            v_POST_GRAD_INTENT ds = new v_POST_GRAD_INTENT();

            StringBuilder sb = new StringBuilder();
            sb.Append("select * from v_POST_GRAD_INTENT where ");

            //sb.Append(SQLHelper.WhereClauseCSV(SQLHelper.WhereClauseJoiner.NONE, "STYP", schoolType));


            //Adds " ... AND (SexCode in (1, 2)) ..."
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.NONE, "Sex", sexCode));

            //Adds " ... AND (RaceCode in (1, 2, 3, 4, 5)) ..."
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "Race", raceCode));

            //Adds " ... AND ((GradeCode >= 16) AND (GradeCode <= 64)) ..."
            //sb.Append(SQLHelper.WhereClauseMinAndMaxInclusive(SQLHelper.WhereClauseJoiner.AND, "Grade", gradeCodeRange));

            //Adds " ... AND (DisabilityCode in (1, 2)) ..."
            //sb.Append(SQLHelper.WhereClauseCSV(SQLHelper.WhereClauseJoiner.AND, "Disability", disabilityCode));

            //Adds " ... AND (EconDisadvCode in (1, 2)) ..."
            //sb.Append(SQLHelper.WhereClauseCSV(SQLHelper.WhereClauseJoiner.AND, "EconDisadvCode", econDisadv));

            //Adds " ... AND (ELPCode in (1, 2)) ..."
            //sb.Append(SQLHelper.WhereClauseCSV(SQLHelper.WhereClauseJoiner.AND, "ELPCode", ELPCode));


            //Adds " ... AND ((year >= 1997) AND (year <= 2007)) ..."
            sb.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.AND, "year", year));

            //fullkey
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "FullKey", fullKey));


            //order by clause
            //sb.AppendFormat(" ORDER BY {0}", SQLHelper.ConvertToCSV(orderBy, false));
            sb.Append(SQLHelper.GetOrderByClause(orderBy));

            base.GetDS(ds, sb.ToString(), ds._v_POST_GRAD_INTENT.TableName);
            return ds;
        }
    }
}
