using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.DAL.WI
{
    public class DALACT : DALWIBase
    {
        public override string  BuildSQL(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            StringBuilder sql = new StringBuilder();
            String dbObject = "v_ACT";
            
            sql.Append(SQLHelper.SelectStarFromWhereFormat(dbObject));

            ////Adds " ... AND (SexCode in (1, 2)) ..."
            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.NONE, "Sex", Marshaller.sexCodes));

            ////Adds " ... AND (RaceCode in (1, 2, 3, 4, 5)) ..."
            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "Race", Marshaller.raceCodes));

            ////Adds " ... AND ((GradeCode >= 16) AND (GradeCode <= 64)) ..."
            //sql.Append(SQLHelper.WhereClauseMinAndMaxInclusive(SQLHelper.WhereClauseJoiner.AND, "GradeCode", gradeCodeRange));

            /*
            ////Adds " ... AND (DisabilityCode in (1, 2)) ..."
            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "DisabilityCode", Marshaller.disabilityCodes));

            ////Adds " ... AND (EconDisadvCode in (1, 2)) ..."
            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "EconDisadvCode", Marshaller.econDisadvCodes));

            ////Adds " ... AND (ELPCode in (1, 2)) ..."
            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "ELPCode", Marshaller.ELPCodes));
            */

            ////Adds " ... AND ((year >= 1997) AND (year <= 2007)) ..."
            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.AND, "year", Marshaller.years));

            sql.Append(Marshaller.STYPClause(SQLHelper.WhereClauseJoiner.AND, "SchoolType", dbObject));

            sql.Append(Marshaller.FullkeyClause(SQLHelper.WhereClauseJoiner.AND, "FullKey"));
            
            //order by clause
            sql.Append(SQLHelper.GetOrderByClause(Marshaller.orderByList));

            return sql.ToString();
        }
    }
}