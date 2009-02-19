using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI.DataSets;

namespace SligoCS.DAL.WI
{
    public class DALAttendance : DALWIBase
    {


        public v_AttendanceWWoDisSchoolDistState GetAttendanceData(List<int> raceCode,
            List<int> sexCode,
            List<int> disabilityCode,
            List<int> year,
            List<string> fullKey,
            List<int> gradeCodeRange,
            List<int> schoolType,
            List<int> econDisadv,
            List<int> ELPCode,
            List<string> orderBy)
        {
            v_AttendanceWWoDisSchoolDistState ds = new v_AttendanceWWoDisSchoolDistState();

            StringBuilder sb = new StringBuilder();
            sb.Append("select * from v_AttendanceWWoDisSchoolDistState where ");

            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.NONE, "SchoolType", schoolType));


            //Adds " ... AND (SexCode in (1, 2)) ..."
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "SexCode", sexCode));

            ////Adds " ... AND (RaceCode in (1, 2, 3, 4, 5)) ..."
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "RaceCode", raceCode));

            ////Adds " ... AND ((GradeCode >= 16) AND (GradeCode <= 64)) ..."
            sb.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.AND, "GradeCode", gradeCodeRange));

            //Adds " ... AND (DisabilityCode in (1, 2)) ..."
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "DisabilityCode", disabilityCode));

            //Adds " ... AND (EconDisadvCode in (1, 2)) ..."
            //sb.Append(SQLHelper.WhereClauseCSV(SQLHelper.WhereClauseJoiner.AND, "EconDisadvCode", econDisadv));

            //Adds " ... AND (ELPCode in (1, 2)) ..."
            //sb.Append(SQLHelper.WhereClauseCSV(SQLHelper.WhereClauseJoiner.AND, "ELPCode", ELPCode));


            //Adds " ... AND ((year >= 1997) AND (year <= 2007)) ..."
            sb.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.AND, "year", year));

            //fullkey
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "FullKey", fullKey));


            //order by clause
            //sb.AppendFormat(" ORDER BY {0}", SQLHelper.ConvertToCSV(orderBy, false));
            sb.Append(SQLHelper.GetOrderByClause(orderBy));
            
            base.GetDS(ds, sb.ToString(), ds._v_AttendanceWWoDisSchoolDistState.TableName);
            return ds;
        }

    }
}
