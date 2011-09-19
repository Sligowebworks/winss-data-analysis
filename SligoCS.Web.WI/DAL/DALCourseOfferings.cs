using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.DAL.WI
{
    public class DALCourseOfferings : DALWIBase
    {
        /*public v_Course_Offerings GetData(
            List<string> fullKey,
            List<int> year,
            CourseTypeID CourseTypeID,
            WMAS WMASID1,
            string clauseForCompareToSchoolsDistrict,
            bool useFullkeys,
            List<string> orderBy)*/
        public override string  BuildSQL(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM v_course_offerings WHERE ");

            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.NONE, "year", Marshaller.years));

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "CourseTypeID", Marshaller.CourseTypeCodes));

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "WMASID1", Marshaller.WMASCodes));

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
