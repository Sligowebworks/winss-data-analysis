using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI.DataSets;

namespace SligoCS.DAL.WI
{
    public class DALGrad_Reqs : DALWIBase
    {

        public v_Grad_Reqs GetGradReqs(List<int> raceCode,
            List<int> sexCode,
            List<int> disabilityCode,
            List<int> year,
            List<string> fullKey,
            List<int> gradeCodeRange,
            List<int> schoolType,
            List<int> econDisadv,
            List<int> ELPCode,
            List<int> subjectID,
            List<string> orderBy)
        {
            v_Grad_Reqs ds = new v_Grad_Reqs();

            StringBuilder sb = new StringBuilder();
            sb.Append("select * from v_Grad_Reqs where ");

            //sb.Append(SQLHelper.WhereClauseCSV(SQLHelper.WhereClauseJoiner.NONE, "SchoolType", schoolType));


            //Adds " ... AND (SexCode in (1, 2)) ..."
            //sb.Append(SQLHelper.WhereClauseCSV(SQLHelper.WhereClauseJoiner.AND, "SexCode", sexCode));

            //Adds " ... AND (RaceCode in (1, 2, 3, 4, 5)) ..."
            //sb.Append(SQLHelper.WhereClauseCSV(SQLHelper.WhereClauseJoiner.AND, "RaceCode", raceCode));

            //Adds " ... AND ((GradeCode >= 16) AND (GradeCode <= 64)) ..."
            //sb.Append(SQLHelper.WhereClauseMinAndMaxInclusive(SQLHelper.WhereClauseJoiner.AND, "GradeCode", gradeCodeRange));

            //Adds " ... AND (DisabilityCode in (1, 2)) ..."
            //sb.Append(SQLHelper.WhereClauseCSV(SQLHelper.WhereClauseJoiner.AND, "DisabilityCode", disabilityCode));

            //Adds " ... AND (EconDisadvCode in (1, 2)) ..."
            //sb.Append(SQLHelper.WhereClauseCSV(SQLHelper.WhereClauseJoiner.AND, "EconDisadvCode", econDisadv));

            //Adds " ... AND (ELPCode in (1, 2)) ..."
            //sb.Append(SQLHelper.WhereClauseCSV(SQLHelper.WhereClauseJoiner.AND, "ELPCode", ELPCode));


            //Adds " ... AND ((year >= 1997) AND (year <= 2007)) ..."
            sb.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.NONE, "year", year));

            //fullkey
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "FullKey", fullKey));


            //Adds " ... AND ((subjectid >= 1) AND (subjectid <= 7)) ..."
            sb.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.AND, "subjectID", subjectID));



            //order by clause
            //sb.AppendFormat(" ORDER BY {0}", SQLHelper.ConvertToCSV(orderBy, false));
            sb.Append(SQLHelper.GetOrderByClause(orderBy));

            base.GetDS(ds, sb.ToString(), ds._v_Grad_Reqs.TableName);
            return ds;
        }
    }
}
