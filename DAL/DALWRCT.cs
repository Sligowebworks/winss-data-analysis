using System;
using System.Collections.Generic;
using System.Text;
using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.DAL.WI
{
    public class DALWRCT : DALWIBase
    {
        public override string BuildSQL(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT *  FROM v_WRCT WHERE ");

            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.NONE, "year", Marshaller.years));

            if (Marshaller.compareSelectedFullKeys)
            {
                sql.Append(" and ").Append(Marshaller.clauseForCompareSelected);
                //view does not support schooltype, except for schools
                if (Marshaller.GlobalValues.S4orALL.Key == S4orALLKeys.AllSchoolsOrDistrictsIn)
                    sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "SchoolType", Marshaller.stypList));
            }
            else
            {
                sql.Append(SQLHelper.WhereClauseValuesInList(
                    SQLHelper.WhereClauseJoiner.AND, "FullKey", Marshaller.fullkeylist));
            }

            sql.AppendFormat(SQLHelper.GetOrderByClause(Marshaller.orderByList));

            return sql.ToString();
        }
        public String GetWRCT(string fullKey)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from v_WRCT where fullkey = '{0}'", fullKey);
            sql.AppendFormat("and (Enrolled <> '--' and [Advanced + Proficient Total] <> '--')");

            return sql.ToString();
        }
    }
}
