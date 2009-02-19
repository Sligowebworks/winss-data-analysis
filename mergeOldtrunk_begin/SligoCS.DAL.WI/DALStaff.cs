using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI.DataSets;

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

            SQLHelper sql = new SQLHelper();

           //Adds " ... AND ((year >= 1997) AND (year <= 2007)) ..."
            sb.Append(sql.WhereClauseMinAndMaxInclusive(SQLHelper.WhereClauseJoiner.NONE, "year", year));

            ////fullkey
            //sb.Append(sql.WhereClauseCSV(SQLHelper.WhereClauseJoiner.NONE, "FullKey", fullKey));
            if (useFullkeys)
            {
                sb.Append(sql.WhereClauseCSV(SQLHelper.WhereClauseJoiner.AND, "FullKey", fullKey));
            }
            else
            {
                sb.Append(" and ").Append(clauseForCompareToSchoolsDistrict);
            }

            //order by clause
            //sb.AppendFormat(" ORDER BY {0}", sql.ConvertToCSV(orderBy, false));
            sb.Append(sql.GetOrderByClause(orderBy));


            base.GetDS(ds, sb.ToString(), ds._v_Staff.TableName);
            return ds;
        }

    }
}
