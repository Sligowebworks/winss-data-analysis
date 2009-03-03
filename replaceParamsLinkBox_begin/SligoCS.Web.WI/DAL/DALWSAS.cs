using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI.DataSets;

namespace SligoCS.DAL.WI
{
    public class DALWSAS : DALWIBase
    {
        public v_WSAS GetData(
            List<int> year,
            List<string> fullKey,
            int Grade,
            List<string> SubjectID,
            List<int> Group,
            string clauseForCompareToSchoolsDistrict,
            bool useFullkeys,
            List<string> orderBy)
        {
            v_WSAS ds = new v_WSAS();

            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM v_wsas WHERE ");

            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.NONE, "year", year));

            //fullkey
            if (useFullkeys)
            {
                sql.Append(SQLHelper.WhereClauseValuesInList(
                    SQLHelper.WhereClauseJoiner.AND, "FullKey", fullKey));
            }
            else
            {
                sql.Append(SQLHelper.WhereClauseJoiner.AND).Append(clauseForCompareToSchoolsDistrict);
            }

            sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "Grade", Grade.ToString()));

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "Groupnum", Group));

            sql.Append(SQLHelper.GetOrderByClause(orderBy));

            base.GetDS(ds, sql.ToString(), ds._v_WSAS.TableName);
            return ds;
        }
    }
}
