using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI.DataSets;

namespace SligoCS.DAL.WI
{
    public class DALExpulsionServices :DALWIBase
    {
        public v_ExpulsionServices GetExpulsionServicesData( 
            List<int> year,
            List<string> fullKey,
             string clauseForCompareToSchoolsDistrict,
            bool useFullkeys,
            List<string> orderBy
            )
        {
            v_ExpulsionServices ds = new v_ExpulsionServices();
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM vExpulsionServices WHERE ");

            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.NONE, "year", year));

            if (useFullkeys)
            {
                sql.Append(SQLHelper.WhereClauseValuesInList(
                    SQLHelper.WhereClauseJoiner.AND, "FullKey", fullKey));
            }
            else
            {
                sql.Append(" and ").Append(clauseForCompareToSchoolsDistrict);
            }

             sql.Append(SQLHelper.GetOrderByClause(orderBy));

            base.GetDS(ds, sql.ToString(), ds.vExpulsionServices.TableName);
            return ds;
        }

        public vExpulsionServicesAndReturns GetExpulsionAndReturnsData(
            List<int> year,
            List<string> fullKey,
             string clauseForCompareToSchoolsDistrict,
            bool useFullkeys,
            List<string> orderBy
            )
        {
            vExpulsionServicesAndReturns ds = new vExpulsionServicesAndReturns();
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM vExpulsionServicesAndReturns WHERE ");

            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.NONE, "year", year));

            if (useFullkeys)
            {
                sql.Append(SQLHelper.WhereClauseValuesInList(
                    SQLHelper.WhereClauseJoiner.AND, "FullKey", fullKey));
            }
            else
            {
                sql.Append(" and ").Append(clauseForCompareToSchoolsDistrict);
            }

            sql.Append(SQLHelper.GetOrderByClause(orderBy));

            base.GetDS(ds, sql.ToString(), ds._vExpulsionServicesAndReturns.TableName);
            return ds;
        }

    }
}
