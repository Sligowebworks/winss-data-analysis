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

            StringBuilder sb = new StringBuilder();
            sb.Append("select * from v_TeacherQualificationsScatterPlot where ");

            SQLHelper sql = new SQLHelper();

            sb.Append(sql.WhereClauseCSV(SQLHelper.WhereClauseJoiner.NONE, "SchoolTypeCode", schoolType));

            sb.Append(sql.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "year", year));

            ////fullkey
            //sb.Append(sql.WhereClauseCSV(
            //        SQLHelper.WhereClauseJoiner.AND, "FullKey", fullKey));
            
            string st = " and right(fullkey,1) = 'X' and left(fullkey,1) <> 'X' " ;
            if (orgLevel == "School")
            {
                st = " and right(fullkey,1) <> 'X' and left(fullkey,1) <> 'X' ";
            }

            sb.Append(st);

            if (locationCode == "CESA")
            {
                sb.Append ( sql.WhereClauseEquals (SQLHelper.WhereClauseJoiner.AND, "CESA", cesa));
            }
            else if (locationCode == "COUNTY")
            {
                sb.Append ( sql.WhereClauseEquals (SQLHelper.WhereClauseJoiner.AND, "County", county.ToString()));
            }

            //LinkSubjectCode
            sb.Append(sql.WhereClauseEquals (SQLHelper.WhereClauseJoiner.AND, "LinkSubjectCode", subjectCode));

            //Related key
            sb.Append(sql.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "RelateToKey", relatedToKey));


            //order by clause
            //sb.AppendFormat(" ORDER BY {0}", sql.ConvertToCSV(orderBy, false));
            sb.Append(sql.GetOrderByClause(orderBy));
            
            base.GetDS(ds, sb.ToString(), 
                ds.v_TeacherQualificationsScatterplot.TableName);
            return ds;
        }

    }    
}
