using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.DAL.WI
{
    public class DALAP_TESTS : DALWIBase
    {
        public override string BuildSQL(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {             
            StringBuilder sql = new StringBuilder();
            String dbObject = "v_AP_TESTS";

            sql.Append(SQLHelper.SelectStarFromWhereFormat(dbObject));

            //Adds " ... AND (SexCode in (1, 2)) ..."
            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.NONE, "Sex", Marshaller.sexCodes));

            //Adds " ... AND (RaceCode in (1, 2, 3, 4, 5)) ..."
            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "Race", Marshaller.raceCodes));

            sql.Append(Marshaller.GradeCodesClause(SQLHelper.WhereClauseJoiner.AND, "grade", dbObject));

            //Adds " ... AND ((year >= 1997) AND (year <= 2007)) ..."
            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.AND, "year", Marshaller.years));

            //Static required:
            sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "ExamCode", 99.ToString()));

            sql.Append(Marshaller.FullkeyClause(SQLHelper.WhereClauseJoiner.AND, "fullkey"));
            
            //order by clause
            sql.Append(SQLHelper.GetOrderByClause(Marshaller.orderByList));

            return sql.ToString();
        }
    }
}
