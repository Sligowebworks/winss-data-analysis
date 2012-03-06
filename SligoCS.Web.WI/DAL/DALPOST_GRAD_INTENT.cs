using System;
using System.Collections.Generic;
using System.Text;
using SligoCS.Web.WI.WebSupportingClasses.WI;


namespace SligoCS.DAL.WI
{
    public class DALPOST_GRAD_INTENT : DALWIBase
    {
        public override string  BuildSQL(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            StringBuilder sql = new StringBuilder();
            String dbObject = "v_POST_GRAD_INTENT";

            sql.Append(Marshaller.SelectListFromVisibleColumns(dbObject));

            sql.Append(Marshaller.FullkeyClause(SQLHelper.WhereClauseJoiner.NONE, "fullkey"));

            sql.Append(Marshaller.STYPClause(SQLHelper.WhereClauseJoiner.AND, "SchoolType", dbObject));

            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.AND, "year", Marshaller.years));

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "Race", Marshaller.raceCodes));

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "Sex", Marshaller.sexCodes));

            sql.Append(SQLHelper.GetOrderByClause(Marshaller.orderByList));

            return sql.ToString();
        }
    }
}
