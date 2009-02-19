using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI.DataSets;

namespace SligoCS.DAL.WI
{
    public class DALAP_TESTS : DALWIBase
    {


        public v_AP_TESTS GetAPTestData(List<int> raceCode,
            List<int> sexCode,
            List<int> disabilityCode,
            List<int> year,
            List<string> fullKey,
            List<int> gradeCodeRange,
            List<int> schoolType,
            List<int> econDisadv,
            List<int> ELPCode,
            List<int> examCodes,
            string clauseForCompareToSchoolsDistrict,
            bool useFullkeys,
            List<string> orderBy)
        {             
            v_AP_TESTS ds = new v_AP_TESTS();

            StringBuilder sb = new StringBuilder();
            sb.Append("select * from v_AP_TESTS where ");

            //sb.Append(SQLHelper.WhereClauseCSV(SQLHelper.WhereClauseJoiner.NONE, "SchoolType", schoolType));


            //Adds " ... AND (SexCode in (1, 2)) ..."
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.NONE, "Sex", sexCode));

            //Adds " ... AND (RaceCode in (1, 2, 3, 4, 5)) ..."
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "Race", raceCode));

            //Adds " ... AND ((GradeCode >= 16) AND (GradeCode <= 64)) ..."
            sb.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.AND, "Grade", gradeCodeRange));

            //Adds " ... AND (DisabilityCode in (1, 2)) ..."
            //sb.Append(SQLHelper.WhereClauseCSV(SQLHelper.WhereClauseJoiner.AND, "DisabilityCode", disabilityCode));

            //Adds " ... AND (EconDisadvCode in (1, 2)) ..."
            //sb.Append(SQLHelper.WhereClauseCSV(SQLHelper.WhereClauseJoiner.AND, "EconDisadvCode", econDisadv));

            //Adds " ... AND (ELPCode in (1, 2)) ..."
            //sb.Append(SQLHelper.WhereClauseCSV(SQLHelper.WhereClauseJoiner.AND, "ELPCode", ELPCode));

            //Adds " ... AND (ExamCode in (99)) ..."
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "ExamCode", examCodes));

            //Adds " ... AND ((year >= 1997) AND (year <= 2007)) ..."
            sb.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.AND, "year", year));


            if (useFullkeys)
            {
                sb.Append(SQLHelper.WhereClauseValuesInList(
                    SQLHelper.WhereClauseJoiner.AND, "FullKey", fullKey));
            }
            else
            {
                sb.Append(" and ").Append(clauseForCompareToSchoolsDistrict);
            }

            //order by clause
            //sb.AppendFormat(" ORDER BY {0}", SQLHelper.ConvertToCSV_X(orderBy, false));
            sb.Append(SQLHelper.GetOrderByClause(orderBy));

            base.GetDS(ds, sb.ToString(), ds._v_AP_TESTS.TableName);
            return ds;
        }
    }
}
