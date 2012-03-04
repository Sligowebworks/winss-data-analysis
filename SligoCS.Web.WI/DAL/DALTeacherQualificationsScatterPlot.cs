using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.DAL.WI
{
    public class DALTeacherQualificationsScatterPlot : DALWIBase
    {
        /*public v_TeacherQualificationsScatterPlot 
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
                List<string> orderBy)*/
        public override string  BuildSQL(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            StringBuilder sql = new StringBuilder();
            String dbObject = "v_TeacherQualificationsScatterPlot";

            sql.Append(SQLHelper.SelectColumnListFromWhereFormat(Marshaller.SelectListFromVisibleColumns(), dbObject));
            
            sql.Append(Marshaller.STYPClause(SQLHelper.WhereClauseJoiner.NONE, "SchoolTypeCode", dbObject));

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "year", Marshaller.years));

           if (Marshaller.GlobalValues.TQLocation.Key == TQLocationKeys.CESA)
            {
                sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "CESA", Marshaller.GlobalValues.Agency.CESA));
            }
            else if (Marshaller.GlobalValues.TQLocation.Key == TQLocationKeys.County)
            {
                sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "County", Marshaller.GlobalValues.Agency.County));
            }

            //LinkSubjectCode
            sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "LinkSubjectCode", Marshaller.GlobalValues.TQSubjectsSP.Value));

            //Related key
            sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "RelateToKey", Marshaller.GlobalValues.TQRelateTo.Value));

            if (Marshaller.GlobalValues.OrgLevel.Key == OrgLevelKeys.School)
                sql.Append(" and right(fullkey,1) <> 'X' and left(fullkey,1) <> 'X' ");
            else
                sql.Append(" and right(fullkey,1) = 'X' and left(fullkey,1) <> 'X' ");


            //order by clause
            //sb.AppendFormat(" ORDER BY {0}", sql.ConvertToCSV(orderBy, false));
            sql.Append(SQLHelper.GetOrderByClause(Marshaller.orderByList));

            return sql.ToString();
        }

    }    
}
