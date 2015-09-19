using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.DAL.WI
{
    public class DALHSCompletion : DALWIBase
    {
        public override String BuildSQL(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            StringBuilder sql = new StringBuilder();
            String dbObject = "v_HSCWWoDisSchoolDistStateEconELPXYearRate";

            sql.Append(SQLHelper.SelectStarFromWhereFormat(dbObject));
           
            //Adds " ... AND (SexCode in (1, 2)) ..."
            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.NONE, "SexCode", Marshaller.sexCodes));

            ////Adds " ... AND (RaceCode in (1, 2, 3, 4, 5)) ..."
            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "RaceCode", Marshaller.raceCodes));

            sql.Append(Marshaller.GradeCodesClause(SQLHelper.WhereClauseJoiner.AND, "GradeCode", dbObject));

            //Adds " ... AND (DisabilityCode in (1, 2)) ..."
            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "DisabilityCode", Marshaller.disabilityCodes));

            //Adds " ... AND (EconDisadvCode in (1, 2)) ..."
            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "EconDisadv", Marshaller.econDisadvCodes));

            //Adds " ... AND (ELPCode in (1, 2)) ..."
            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "ELPCode", Marshaller.ELPCodes));

            //Adds " ... AND ((year >= 1997) AND (year <= 2007)) ..."
            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.AND, "year", Marshaller.years));

            sql.Append(Marshaller.FullkeyClause(SQLHelper.WhereClauseJoiner.AND, "FullKey"));

            //Special Case for Compare To OrgLevel
            //Distirct and State Levels should not reflect SchoolType
            if (Marshaller.GlobalValues.CompareTo.Key == CompareToKeys.OrgLevel)
            {
                sql.Append(SQLHelper.WhereClauseJoiner.AND);
                sql.Append("((");
                sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.NONE, "right(fullkey, 4)", "XXXX"));
                sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "SchoolType", Marshaller.GlobalValues.STYP.Range[STYPKeys.StateSummary]));
                sql.Append(") OR (");
                sql.Append(SQLHelper.WhereClauseNotEquals(SQLHelper.WhereClauseJoiner.NONE, "right(fullkey, 4)", "XXXX"));
                sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "SchoolType", Marshaller.stypList));
                sql.Append("))");
            }
            
            if (Marshaller.GlobalValues.CompareTo.Key != CompareToKeys.OrgLevel)
                sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "SchoolType", Marshaller.stypList));
            
            //Timeframe
            TmFrm frame = Marshaller.GlobalValues.TmFrm;

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "timeframe", 
                ( (frame.Key == TmFrmKeys.All)
                ? new List<int>(new int[] { 0,1}) 
                : new List<int>( new int[] { 
                        ((frame.Key == TmFrmKeys.FourYear) ? 1 : 0)
                    }))));

            //order by clause
            //sb.AppendFormat(" ORDER BY {0}", SQLHelper.ConvertToCSV(orderBy, false));
            sql.Append(SQLHelper.GetOrderByClause(Marshaller.orderByList));
            
            return sql.ToString();
        }

    }
}
