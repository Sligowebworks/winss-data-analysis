using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.DAL.WI
{
    public class DALCourseOfferings : DALWIBase
    {
        public override string  BuildSQL(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            StringBuilder sql = new StringBuilder();
            String dbObject = "v_course_offerings";
            
            sql.Append(SQLHelper.SelectColumnListFromWhereFormat(Marshaller.SelectListFromVisibleColumns(), dbObject));

            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.NONE, "year", Marshaller.years));

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "CourseTypeID", Marshaller.CourseTypeCodes));

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "WMASID1", Marshaller.WMASCodes));

            sql.Append(Marshaller.FullkeyClause(SQLHelper.WhereClauseJoiner.AND, "FullKey"));
            
            // jdj: must exclude null district records - workaround - databse object design is messy and needs refactoring
            sql.Append(" and linkeddistrictname IS NOT NULL ");

           // jdj: must exclude topic code 999 when selecting Subject: Other - workaround - databse object design is messy and needs refactoring
           // mzd: Except when viewing CAPP
            if (Marshaller.GlobalValues.WMAS.Key == WMASKeys.Other && Marshaller.GlobalValues.CourseTypeID.Key != CourseTypeIDKeys.CAPP)  sql.Append(" and v_course_offerings.topic <> '999'  ");

            sql.Append(SQLHelper.GetOrderByClause(Marshaller.orderByList));

            return sql.ToString();
        }
    }
}
