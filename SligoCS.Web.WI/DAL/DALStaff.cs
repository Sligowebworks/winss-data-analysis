using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.DAL.WI
{
    public class DALStaff : DALWIBase
    {
       public override string  BuildSQL(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            StringBuilder sql = new StringBuilder();
            
            sql.Append(SQLHelper.SelectStarFromWhereFormat("v_StaffFull"));

            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.NONE, "year", Marshaller.years));

            sql.Append(Marshaller.FullkeyClause(SQLHelper.WhereClauseJoiner.AND, "FullKey"));

            sql.Append(SQLHelper.GetOrderByClause(Marshaller.orderByList));

            return sql.ToString();
        }
    }
}
