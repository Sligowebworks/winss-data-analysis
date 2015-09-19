using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.DAL.WI
{
    public class DALExpenditure : DALWIBase
    {

     public override string  BuildSQL(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            StringBuilder sql = new StringBuilder();
            String dbObject = "v_Expend_2";
            
            sql.Append(Marshaller.SelectListFromVisibleColumns(dbObject));

            sql.Append(Marshaller.FullkeyClause(SQLHelper.WhereClauseJoiner.NONE, "FullKey"));

            ////CT for cost category
            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "CT", Marshaller.CostTypeCodes));

            ////Adds " ... AND ((year >= 1997) AND (year <= 2007)) ..."
            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.AND, "year", Marshaller.years));

            ////order by clause
            sql.Append(SQLHelper.GetOrderByClause(Marshaller.orderByList));

            return sql.ToString();
        }
    }
}
