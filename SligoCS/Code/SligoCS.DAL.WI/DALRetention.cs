using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI.DataSets;

namespace SligoCS.DAL.WI
{
    public class DALRetention : DALWIBase
    {

        /// <summary>
        /// Overload.
        /// </summary>
        /// <param name="raceCode"></param>
        /// <param name="sexCode"></param>
        /// <param name="disabilityCode"></param>
        /// <param name="year"></param>
        /// <param name="fullKey"></param>
        /// <param name="gradeCode"></param>
        /// <param name="schoolType"></param>
        /// <returns></returns>
        //public v_RetentionWWoDisSchoolDistState GetRetentionData(int raceCode,
        //    int sexCode,
        //    int disabilityCode,
        //    int year,
        //    string fullKey,
        //    int gradeCode,
        //    int schoolType)
        //{

        //    //convert individual int or string into a generic List with 1 element.
        //    List<int> raceCodes = new List<int>();
        //    List<int> sexCodes = new List<int>();
        //    List<int> disabilityCodes = new List<int>();
        //    List<int> years = new List<int>();
        //    List<string> fullKeys = new List<string>();
        //    List<int> gradeCodes = new List<int>();
        //    List<int> schoolTypes = new List<int>();

        //    raceCodes.Add(raceCode);
        //    sexCodes.Add(sexCode);
        //    disabilityCodes.Add(disabilityCode);
        //    years.Add(year);
        //    fullKeys.Add(fullKey);
        //    gradeCodes.Add(gradeCode);
        //    schoolTypes.Add(schoolType);

        //    v_RetentionWWoDisSchoolDistState ds = GetRetentionData(raceCodes, sexCodes, disabilityCodes, years, fullKeys , gradeCodes, schoolTypes);
        //    return ds;
        //}


        /// <summary>
        /// Implementor.  This function queries the v_RetentionWWoDisSchoolDistState, with several optional parameters.
        /// </summary>
        /// <param name="raceCode"></param>
        /// <param name="sexCode"></param>
        /// <param name="disabilityCode"></param>
        /// <param name="year"></param>
        /// <param name="fullKey"></param>
        /// <param name="gradeCodeRange">Min and Max grade codes (both are inclusive for range.)</param>
        /// <param name="schoolType"></param>
        /// <returns></returns>
        public v_RetentionWWoDisSchoolDistState GetRetentionData(List<int> raceCode,
            List<int> sexCode,
            List<int> disabilityCode,
            List<int> year,
            List<string> fullKey,
            List<int> gradeCodeRange,
            List<int> schoolType, 
            List<string> orderBy)
        {
            v_RetentionWWoDisSchoolDistState ds = new v_RetentionWWoDisSchoolDistState();

            StringBuilder sb = new StringBuilder();
            sb.Append("select * from v_RetentionWWoDisSchoolDistState where ");

            SQLHelper sql = new SQLHelper();

            sb.Append(sql.WhereClauseCSV(SQLHelper.WhereClauseJoiner.NONE, "SchoolType", schoolType));


            //Adds " ... AND (SexCode in (1, 2)) ..."
            sb.Append(sql.WhereClauseCSV(SQLHelper.WhereClauseJoiner.AND, "SexCode", sexCode));

            //Adds " ... AND (RaceCode in (1, 2, 3, 4, 5)) ..."
            sb.Append(sql.WhereClauseCSV(SQLHelper.WhereClauseJoiner.AND, "RaceCode", raceCode));

            //Adds " ... AND ((GradeCode >= 16) AND (GradeCode <= 64)) ..."
            sb.Append(sql.WhereClauseMinAndMaxInclusive(SQLHelper.WhereClauseJoiner.AND, "GradeCode", gradeCodeRange));

            //Adds " ... AND (DisabilityCode in (1, 2)) ..."
            sb.Append(sql.WhereClauseCSV(SQLHelper.WhereClauseJoiner.AND, "DisabilityCode", disabilityCode));

            //Adds " ... AND ((year >= 1997) AND (year <= 2006)) ..."
            sb.Append(sql.WhereClauseMinAndMaxInclusive(SQLHelper.WhereClauseJoiner.AND, "year", year));

            //fullkey
            sb.Append(sql.WhereClauseCSV(SQLHelper.WhereClauseJoiner.AND, "FullKey", fullKey));


            //order by clause
            sb.AppendFormat(" ORDER BY {0}", sql.ConvertToCSV(orderBy, false));


            base.GetDS(ds, sb.ToString(), ds._v_RetentionWWoDisSchoolDistState.TableName);
            return ds;
        }

    }
}
