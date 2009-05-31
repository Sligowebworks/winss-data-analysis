using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.Web.WI.DAL.DataSets;
using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.DAL.WI
{
    public class DALCourseOfferings : DALWIBase
    {
        public v_Course_Offerings GetData(
            List<string> fullKey,
            List<int> year,
            CourseTypeID CourseTypeID,
            WMAS WMASID1,
            string clauseForCompareToSchoolsDistrict,
            bool useFullkeys,
            List<string> orderBy)
        {
            v_Course_Offerings ds = new v_Course_Offerings();

            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM v_course_offerings WHERE ");

            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.NONE, "year", year));

            sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "CourseTypeID", CourseTypeID));

            sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "WMASID1", WMASID1));

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


            // jdj: must exclude null district records - workaround - databse object design is messy and needs refactoring
            sql.Append(" and linkeddistrictname IS NOT NULL ");

           // jdj: must exclude topic code 999 when selecting Subject: Other - workaround - databse object design is messy and needs refactoring
            if (WMASID1.Key == WMASKeys.Other)  sql.Append(" and v_course_offerings.topic <> '999'  ");

            sql.Append(SQLHelper.GetOrderByClause(orderBy));

            base.GetDS(ds, sql.ToString(), ds.v_COURSE_OFFERINGS.TableName);
            return ds;
        }
    }
}
