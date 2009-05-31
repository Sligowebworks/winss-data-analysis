using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.Web.WI.DAL.DataSets;

namespace SligoCS.DAL.WI
{
    public class DALRevenue : DALWIBase
    {

        public override string BuildSQL(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            
        /*public v_Revenues_2 GetRevenueData(
            List<int> years,
            List<string> fullKey,
            string clauseForCompareToSchoolsDistrict,
            bool useFullkeys,
            List<string> orderBy)
        {*/
            v_Revenues_2 ds = new v_Revenues_2();

            StringBuilder sb = new StringBuilder();
            sb.Append("select * from v_Revenues_2 where ");

            ////fullkey
            //sb.Append(SQLHelper.WhereClauseCSV(SQLHelper.WhereClauseJoiner.NONE, "FullKey", fullKey));
            if (Marshaller.compareSelectedFullKeys)
            {
                sb.Append(SQLHelper.WhereClauseValuesInList(
                    SQLHelper.WhereClauseJoiner.NONE, "FullKey", Marshaller.fullkeylist));
            }
            else
            {
                sb.Append(Marshaller.clauseForCompareSelected);
            }

            ////Adds " ... AND ((year >= 1997) AND (year <= 2007)) ..."
            sb.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.AND, "year", Marshaller.years));

            ////order by clause
            sb.Append(SQLHelper.GetOrderByClause(Marshaller.orderByList));

            //base.GetDS(ds, sb.ToString(), ds._v_Revenues_2.TableName);
            //return ds;

            return sb.ToString();
        }
    }
}
