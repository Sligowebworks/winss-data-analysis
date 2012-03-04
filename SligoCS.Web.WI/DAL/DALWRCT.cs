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
            String dbObject = "v_WRCT";

            sql.Append(SQLHelper.SelectColumnListFromWhereFormat(Marshaller.SelectListFromVisibleColumns(), dbObject));

            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.NONE, "year", Marshaller.years));

            sql.Append(Marshaller.FullkeyClause(SQLHelper.WhereClauseJoiner.AND, "fullkey"));

            //view does not support schooltype, except for schools
            if ((Marshaller.GlobalValues.CompareTo.Key == CompareToKeys.SelSchools || Marshaller.GlobalValues.CompareTo.Key == CompareToKeys.SelDistricts)
                && Marshaller.GlobalValues.S4orALL.Key == S4orALLKeys.AllSchoolsOrDistrictsIn)
                sql.Append(Marshaller.STYPClause(SQLHelper.WhereClauseJoiner.AND, "SchoolType", dbObject));

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
