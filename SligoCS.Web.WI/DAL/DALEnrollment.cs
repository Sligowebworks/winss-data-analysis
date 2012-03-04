using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.BL.WI;

namespace SligoCS.DAL.WI
{
    public class DALEnrollment : DALWIBase
    {
        public override string BuildSQL(QueryMarshaller Marshaller)
        {
            StringBuilder sql = new StringBuilder();
            String dbObject;

            if (Marshaller.GlobalValues.Group.Key == SligoCS.Web.WI.WebSupportingClasses.WI.GroupKeys.EngLangProf)
                dbObject = "v_LEPSchoolDistState ";
            else if (Marshaller.GlobalValues.Group.Key == SligoCS.Web.WI.WebSupportingClasses.WI.GroupKeys.EconDisadv)
                dbObject = "v_ErateRollups ";
            else
                dbObject = "v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat ";

            sql.Append(SQLHelper.SelectColumnListFromWhereFormat(Marshaller.SelectListFromVisibleColumns(), dbObject));
            
            //School Types
            sql.Append(Marshaller.STYPClause(SQLHelper.WhereClauseJoiner.NONE, "SchoolType", dbObject));

            //years
            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.AND, "year", Marshaller.years));

            sql.Append(Marshaller.FullkeyClause(SQLHelper.WhereClauseJoiner.AND, "FullKey"));

            //order by clause
            sql.AppendFormat(SQLHelper.GetOrderByClause(Marshaller.orderByList));

            return sql.ToString();
        }
    }
}
