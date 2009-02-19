using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI.DataSets;

namespace SligoCS.DAL.WI
{
    public class DALExpenditure : DALWIBase
    {

        public v_Expend_2 GetExpendData(
            List <string> CT,
            List<int> years,
            List<string> fullKey,
            string clauseForCompareToSchoolsDistrict,
            bool useFullkeys,
            List<string> orderBy)
        {
            v_Expend_2 ds = new v_Expend_2();

            StringBuilder sb = new StringBuilder();
            sb.Append("select * from v_Expend_2 where ");

            SQLHelper sql = new SQLHelper();
 
             ////fullkey
            //sb.Append(sql.WhereClauseCSV(SQLHelper.WhereClauseJoiner.NONE, "FullKey", fullKey));
            if (useFullkeys)
            {
                sb.Append(sql.WhereClauseCSV(
                    SQLHelper.WhereClauseJoiner.NONE, "FullKey", fullKey));
            }
            else
            {
                sb.Append(clauseForCompareToSchoolsDistrict);
            }

            ////CT for cost category
            sb.Append(sql.WhereClauseCSV(SQLHelper.WhereClauseJoiner.AND, "CT", CT));

            ////Adds " ... AND ((year >= 1997) AND (year <= 2007)) ..."
            sb.Append(sql.WhereClauseMinAndMaxInclusive(SQLHelper.WhereClauseJoiner.AND, "year", years));

            ////order by clause
            sb.Append(sql.GetOrderByClause(orderBy));

            base.GetDS(ds, sb.ToString(), ds._v_Expend_2.TableName);
            return ds;
        }
    }
}
