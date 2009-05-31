using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.Web.WI.DAL.DataSets;

namespace SligoCS.DAL.WI
{
    public class DALStaff : DALWIBase
    {

        public v_Staff GetStaffData(
            List<int> year,
            List<string> fullKey,
            string clauseForCompareToSchoolsDistrict,
            bool useFullkeys,
            List<string> orderBy)
        {
            v_Staff ds = new v_Staff();

            StringBuilder sb = new StringBuilder();
            sb.Append("select * from v_Staff where ");

            //Adds " ... AND ((year >= 1997) AND (year <= 2007)) ..."
            sb.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.NONE, "year", year));

            ////fullkey
            //sb.Append(SQLHelper.WhereClauseCSV(SQLHelper.WhereClauseJoiner.NONE, "FullKey", fullKey));
            if (useFullkeys)
            {
                sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "FullKey", fullKey));
            }
            else
            {
                sb.Append(" and ").Append(clauseForCompareToSchoolsDistrict);
            }

            //order by clause
            //sb.AppendFormat(" ORDER BY {0}", SQLHelper.ConvertToCSV(orderBy, false));
            sb.Append(SQLHelper.GetOrderByClause(orderBy));


            base.GetDS(ds, sb.ToString(), ds._v_Staff.TableName);
            return ds;
        }

    }
}
