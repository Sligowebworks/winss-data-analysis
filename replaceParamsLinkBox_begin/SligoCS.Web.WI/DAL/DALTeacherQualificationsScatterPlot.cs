using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI.DataSets;

namespace SligoCS.DAL.WI
{
    public class DALTeacherQualificationsScatterPlot : DALWIBase
    {
        public v_TeacherQualificationsScatterPlot 
            GetTeacherQualificationsScatterPlot(
                string year,
                List<string> fullKey,
                List<int> schoolType,
                string subjectCode,
                string teacherVariableCode,
                string relatedToKey,
                string locationCode,
                int county,
                string cesa,
                string orgLevel,
                List<string> orderBy)
        {
            v_TeacherQualificationsScatterPlot ds = 
                new v_TeacherQualificationsScatterPlot();

            StringBuilder sql = new StringBuilder();
            sql.Append("select * from v_TeacherQualificationsScatterPlot where ");

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.NONE, "SchoolTypeCode", schoolType));

            sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "year", year));

            ////fullkey
            //sb.Append(sql.WhereClauseCSV(
            //        SQLHelper.WhereClauseJoiner.AND, "FullKey", fullKey));
            
            string st = " and right(fullkey,1) = 'X' and left(fullkey,1) <> 'X' " ;
            if (orgLevel == "School")
            {
                st = " and right(fullkey,1) <> 'X' and left(fullkey,1) <> 'X' ";
            }

            sql.Append(st);

            if (locationCode == "CESA")
            {
                sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "CESA", cesa));
            }
            else if (locationCode == "COUNTY")
            {
                sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "County", county.ToString()));
            }

            //LinkSubjectCode
            sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "LinkSubjectCode", subjectCode));

            //Related key
            sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "RelateToKey", relatedToKey));


            //order by clause
            //sb.AppendFormat(" ORDER BY {0}", sql.ConvertToCSV(orderBy, false));
            sql.Append(SQLHelper.GetOrderByClause(orderBy));
            
            base.GetDS(ds, sql.ToString(), 
                ds.v_TeacherQualificationsScatterplot.TableName);
            return ds;
        }

    }    
}
