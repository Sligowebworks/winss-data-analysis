using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.Web.WI.DAL.DataSets;

namespace SligoCS.DAL.WI
{
    public class DALAttendance : DALWIBase
    {

        /*
        public v_AttendanceWWoDisSchoolDistStateEconELP GetAttendanceData(List<int> raceCode,
            List<int> sexCode,
            List<int> disabilityCode,
            List<int> year,
            List<string> fullKey,
            List<int> gradeCodeRange,
            List<int> schoolType,
            List<int> econDisadv,
            List<int> ELPCode,
            List<string> orderBy)*/
        public override string  BuildSQL(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
           // v_AttendanceWWoDisSchoolDistStateEconELP ds = new v_AttendanceWWoDisSchoolDistStateEconELP();

            StringBuilder sb = new StringBuilder();
            sb.Append("select * from v_AttendanceWWoDisSchoolDistStateEconELP where ");

            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.NONE, "SchoolType", Marshaller.stypList));

            //Adds " ... AND (SexCode in (1, 2)) ..."
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "SexCode", Marshaller.sexCodes));

            ////Adds " ... AND (RaceCode in (1, 2, 3, 4, 5)) ..."
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "RaceCode", Marshaller.raceCodes));

            ////Adds " ... AND ((GradeCode >= 16) AND (GradeCode <= 64)) ..."
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

            //order by clause
            //sb.AppendFormat(" ORDER BY {0}", SQLHelper.ConvertToCSV(orderBy, false));
            //sb.Append(SQLHelper.GetOrderByClause(Marshaller.orderByList));

            return sb.ToString();
        }
    }
}
