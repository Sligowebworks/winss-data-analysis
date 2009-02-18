using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI.DataSets;

namespace SligoCS.DAL.WI
{
    public class DALActivities : DALWIBase
    {

        /// <summary>
        /// Implementor.  This function queries the v_ActivitiesSchoolDistState, with several optional stickyParameters.
        /// </summary>
        /// <returns></returns>
        public v_ActivitiesSchoolDistState GetActivitiesSchoolDistStateData(List<int> year,
            List<string> fullKey,
            List<int> schoolType,
            string SHOW,
            string clauseForCompareToSchoolsDistrict,
            bool useFullkeys,
            List<string> orderBy)
        {
            v_ActivitiesSchoolDistState ds = new v_ActivitiesSchoolDistState();

            StringBuilder sql = new StringBuilder();
        
            sql.Append("select * from v_ActivitiesSchoolDistState where");

            //Adds " ... AND ((year >= 1997) AND (year <= 2007)) ..."
            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.NONE, "year", year));

            //fullkey
            if (useFullkeys)
            {
                sql.Append(SQLHelper.WhereClauseValuesInList(
                    SQLHelper.WhereClauseJoiner.AND, "FullKey", fullKey));
            }
            else
            {
                sql.Append(" and ").Append(clauseForCompareToSchoolsDistrict);
            }

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "SchoolType", schoolType));

            if (SHOW == "COMM")
            {
                sql.Append(sql.Append(" and ActivityCode in ('RE','VO') "));
            }
            else
            {
                sql.Append(sql.Append(" and ActivityCode in ('AT','AC','MS') "));
            }


            //order by clause
            //sql.AppendFormat(" ORDER BY {0}", SQLHelper.ConvertToCSV(orderBy, false));
            sql.AppendFormat(SQLHelper.GetOrderByClause(orderBy));

            base.GetDS(ds, sql.ToString(), ds._v_ActivitiesSchoolDistState.TableName);
            return ds;
        }

    }
}
