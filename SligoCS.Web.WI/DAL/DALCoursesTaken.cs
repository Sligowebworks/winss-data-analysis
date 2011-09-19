using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.DAL.WI
{
    public class DALCoursesTaken : DALWIBase
    {
        public override string  BuildSQL(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            StringBuilder sql = new StringBuilder();
            String dbObject = "v_Coursework";

            sql.Append(SQLHelper.SelectStarFromWhereFormat(dbObject));

            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.NONE, "year", Marshaller.years));

            sql.Append(Marshaller.FullkeyClause(SQLHelper.WhereClauseJoiner.AND, "FullKey"));

            //Adds " ... AND (SexCode in (1, 2)) ..."
            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "Sex", Marshaller.sexCodes));

            sql.Append(Marshaller.GradeCodesClause(SQLHelper.WhereClauseJoiner.AND, "Grade", dbObject));

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "CourseTypeID", Marshaller.CourseTypeCodes));

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "WMASID1", Marshaller.WMASCodes));

            sql.Append(SQLHelper.GetOrderByClause(Marshaller.orderByList));

            return sql.ToString();
        }
    }
}
