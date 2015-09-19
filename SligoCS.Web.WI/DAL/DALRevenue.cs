using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.DAL.WI
{
    public class DALRevenue : DALWIBase
    {
        public override string BuildSQL(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM v_Revenues_2 WHERE ");

            ////fullkey
            if (!Marshaller.compareSelectedFullKeys)
            {
                sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.NONE, "FullKey", Marshaller.fullkeylist));
            }
            else
            {
                sql.Append(Marshaller.clauseForCompareSelected);
            }

            ////Adds " ... AND ((year >= 1997) AND (year <= 2007)) ..."
            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.AND, "year", Marshaller.years));

            ////order by clause
            sql.Append(SQLHelper.GetOrderByClause(Marshaller.orderByList));

            return sql.ToString();
        }
    }
}
