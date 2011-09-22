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
            
            sql.Append(SQLHelper.SelectStarFromWhereFormat("v_Revenues_2"));

            sql.Append(Marshaller.FullkeyClause(SQLHelper.WhereClauseJoiner.NONE, "FullKey"));

            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.AND, "year", Marshaller.years));

            sql.Append(SQLHelper.GetOrderByClause(Marshaller.orderByList));

            return sql.ToString();
        }
    }
}
