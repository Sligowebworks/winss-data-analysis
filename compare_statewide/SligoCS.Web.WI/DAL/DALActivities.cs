using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.DAL.WI
{
    public class DALActivities : DALWIBase
    {

        public override string BuildSQL(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            StringBuilder sql = new StringBuilder();
        
            sql.Append("select * from v_ActivitiesSchoolDistState where ");

            //Adds " ... AND ((year >= 1997) AND (year <= 2007)) ..."
            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.NONE, "year", Marshaller.years));

            //fullkey
            if (Marshaller.compareSelectedFullKeys)
            {
                sql.Append(" and ").Append(Marshaller.clauseForCompareSelected);
            }
            else
            {
                sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "FullKey", Marshaller.fullkeylist));
            }

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "SchoolType", Marshaller.stypList));
                       
            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "ActivityCode", Marshaller.ActivityCodes));

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "grade", Marshaller.gradeCodes));

            sql.AppendFormat(SQLHelper.GetOrderByClause(Marshaller.orderByList));

          return sql.ToString();
        }
    }
}
