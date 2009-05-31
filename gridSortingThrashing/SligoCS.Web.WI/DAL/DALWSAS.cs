using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using SligoCS.Web.WI.DAL.DataSets;

namespace SligoCS.DAL.WI
{
    public class DALWSAS : DALWIBase
    {
/*        public v_WSAS GetData(
            List<int> year,
            List<string> fullKey,
            int Grade,
            List<string> SubjectID,
            List<int> Group,
            string clauseForCompareToSchoolsDistrict,
            bool useFullkeys,
            List<string> orderBy)
    */
        public override string  BuildSQL(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM v_wsas WHERE ");

            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.NONE, "year", Marshaller.years));

            //fullkey
            if (Marshaller.compareSelectedFullKeys)
            {
                sql.Append(SQLHelper.WhereClauseJoiner.AND).Append(Marshaller.clauseForCompareSelected);
            }
            else
            {
                sql.Append(SQLHelper.WhereClauseValuesInList(
                    SQLHelper.WhereClauseJoiner.AND, "FullKey", Marshaller.fullkeylist));
            }

            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.AND, "Grade", Marshaller.gradeCodes));
            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "Groupnum", new List<int>(50)));

            sql.Append(SQLHelper.GetOrderByClause(Marshaller.orderByList));

            return sql.ToString();
        }
    }
}
