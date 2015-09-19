using System;
using System.Collections.Generic;
using System.Text;
using SligoCS.Web.WI.WebSupportingClasses.WI;


namespace SligoCS.DAL.WI
{
    public class DALPOST_GRAD_INTENT : DALWIBase
    {
        public override string  BuildSQL(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM v_POST_GRAD_INTENT WHERE ");

            //fullkey
            if (Marshaller.compareSelectedFullKeys)
            {
                sql.Append(Marshaller.clauseForCompareSelected);
            }
            else
            {
                sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.NONE, "FullKey", Marshaller.fullkeylist));
            }

            //view does not support schooltype, except for schools
            String compareToKey = Marshaller.GlobalValues.CompareTo.Key;
            if (compareToKey == CompareToKeys.SelSchools && Marshaller.GlobalValues.S4orALL.Key == S4orALLKeys.AllSchoolsOrDistrictsIn)
                sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "SchoolType", Marshaller.stypList));

            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.AND, "year", Marshaller.years));

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "Race", Marshaller.raceCodes));

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "Sex", Marshaller.sexCodes));

            sql.Append(SQLHelper.GetOrderByClause(Marshaller.orderByList));

            return sql.ToString();
        }
    }
}
