using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.DAL.WI
{
    public class DALSuspensions : DALWIBase
    {
        public override string  BuildSQL(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            StringBuilder sql = new StringBuilder();
            String dbObject = "v_SuspensionsSchoolDistState";

            sql.Append(Marshaller.SelectListFromVisibleColumns(dbObject));

            sql.Append(Marshaller.STYPClause(SQLHelper.WhereClauseJoiner.NONE, "SchoolType", dbObject));

            ////Adds " ... AND (SexCode in (1, 2)) ..."
            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "SexCode", Marshaller.sexCodes));

            //Adds " ... AND (RaceCode in (1, 2, 3, 4, 5)) ..."
            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "RaceCode", Marshaller.raceCodes));

            //Adds " ... AND ((GradeCode >= 16) AND (GradeCode <= 64)) ..."
            sql.Append(Marshaller.GradeCodesClause(SQLHelper.WhereClauseJoiner.AND, "GradeCode", dbObject));

            //Adds " ... AND (DisabilityCode in (1, 2)) ..."
            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "DisabilityCode", Marshaller.disabilityCodes));

            //Adds " ... AND ((year >= 1997) AND (year <= 2007)) ..."
            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.AND, "year", Marshaller.years));

            sql.Append(Marshaller.FullkeyClause(SQLHelper.WhereClauseJoiner.AND, "FullKey"));
            
            sql.AppendFormat(SQLHelper.GetOrderByClause(Marshaller.orderByList));

            return sql.ToString();
        }
    }
}
