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
            StringBuilder sb = new StringBuilder();

            if (Marshaller.GlobalValues.TmFrm.Key == TmFrmKeys.Legacy)
                sb.Append(@"SELECT * FROM v_HSCWWoDisSchoolDistStateEconELP where ");
            else
                sb.Append(@"SELECT * FROM v_HSCWWoDisSchoolDistStateEconELPXYearRate WHERE ");

            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.NONE, "SchoolType", Marshaller.stypList));

            //Adds " ... AND (SexCode in (1, 2)) ..."
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "SexCode", Marshaller.sexCodes));

            ////Adds " ... AND (RaceCode in (1, 2, 3, 4, 5)) ..."
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "RaceCode", Marshaller.raceCodes));

            //Adds " ... AND ((GradeCode >= 16) AND (GradeCode <= 64)) ..."
            sb.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.AND, "GradeCode", Marshaller.gradeCodes));

            //Adds " ... AND (DisabilityCode in (1, 2)) ..."
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "DisabilityCode", Marshaller.disabilityCodes));

            //Adds " ... AND (EconDisadvCode in (1, 2)) ..."
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "EconDisadv", Marshaller.econDisadvCodes));

            //Adds " ... AND (ELPCode in (1, 2)) ..."
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "ELPCode", Marshaller.ELPCodes));

            //Adds " ... AND ((year >= 1997) AND (year <= 2007)) ..."
            sb.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.AND, "year", Marshaller.years));

            //fullkey
            if (!Marshaller.compareSelectedFullKeys)
            {
                sb.Append(SQLHelper.WhereClauseValuesInList(
                    SQLHelper.WhereClauseJoiner.AND, "FullKey", Marshaller.fullkeylist));
            }
            else
            {
                sb.Append(" and ").Append(Marshaller.clauseForCompareSelected);
            }

            //Timeframe
            if (Marshaller.GlobalValues.TmFrm.Key != TmFrmKeys.Legacy)
            //if (false)
            {
                TmFrm frame = Marshaller.GlobalValues.TmFrm;

                sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "timeframe", 
                    ( (frame.Key == TmFrmKeys.All)
                    ? new List<int>(new int[] { 0,1}) 
                    : new List<int>( new int[] { 
                            ((frame.Key == TmFrmKeys.FourYear) ? 1 : 0)
                        }))));
            }

            //order by clause
            //sb.AppendFormat(" ORDER BY {0}", SQLHelper.ConvertToCSV(orderBy, false));
            sb.Append(SQLHelper.GetOrderByClause(Marshaller.orderByList));
            
            return sb.ToString();
        }

    }
}
