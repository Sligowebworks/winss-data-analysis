using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI.DataSets;

namespace SligoCS.DAL.WI
{
    public class DALCoursesTaken : DALWIBase
    {
        public v_Coursework GetData(
            List<string> fullKey,
            List<int> year,
            List<int> sexCodes,
            int Grade,
            int CourseTypeID,
            int WMASID1,
            string clauseForCompareToSchoolsDistrict,
            bool useFullkeys,
            List<string> orderBy)
        {
            v_Coursework ds = new v_Coursework();

            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM v_Coursework WHERE ");

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

            //Adds " ... AND (SexCode in (1, 2)) ..."
            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "Sex", sexCodes));

            sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "Grade", Grade.ToString()));

            sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "CourseTypeID", CourseTypeID.ToString()));

            sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "WMASID1", WMASID1.ToString()));

            sql.Append(SQLHelper.GetOrderByClause(orderBy));

            base.GetDS(ds, sql.ToString(), ds.v_COURSEWORK.TableName);
            return ds;
        }
    }
}
