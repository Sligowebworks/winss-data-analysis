using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.DAL.WI
{
    public class DALGrad_Reqs : DALWIBase
    {
        public override string  BuildSQL(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM v_Grad_Reqs WHERE ");

            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.NONE, "year", Marshaller.years));

            //fullkey
            if (Marshaller.compareSelectedFullKeys)
            {
                sql.Append(" AND " + Marshaller.clauseForCompareSelected);
            }
            else
            {
                sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "FullKey", Marshaller.fullkeylist));
            }

            List<String> subjects;
            if (Marshaller.GlobalValues.GRSbj.Key == GRSbjKeys.StateLaw)
            {
                subjects = new List<String>( new String[] {"1", "7"});
            }
            else
            {
                subjects = new List<String>(new String[] { "8", "13" });
            }
            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.AND, "subjectID", subjects));

            //order by clause
            //sb.AppendFormat(" ORDER BY {0}", SQLHelper.ConvertToCSV(orderBy, false));
            sql.Append(SQLHelper.GetOrderByClause(Marshaller.orderByList));

            return sql.ToString();
        }
    }
}
