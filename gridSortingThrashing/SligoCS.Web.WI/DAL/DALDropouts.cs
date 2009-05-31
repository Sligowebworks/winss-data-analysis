using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.Web.WI.DAL.DataSets;

namespace SligoCS.DAL.WI
{
    public class DALDropouts : DALWIBase 
    {
        /// <summary>
        /// Returns a strongly typed dataset.
        /// </summary>
        /// <returns></returns>
        //[Obsolete("Use function GetDropoutData2 instead.")]
        public v_dropoutsWWoDisSchoolDistState GetDropoutData(List<string> fullKey,
            List<int> schoolType,
            List<int> years,
            List<int> sexCodes,
            List<int> raceCodes,
            List<int> disabilityCodes,
            List<int> gradeCodes,
            List<string> orderBy)
        {

            StringBuilder sb = new StringBuilder();
            
            sb.Append("select * from v_dropoutsWWoDisSchoolDistState WHERE ");

            //School Types
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.NONE, "SchoolType", schoolType));



            //Adds " ... AND (SexCode in (1, 2)) ..."
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "SexCode", sexCodes));

            //Adds " ... AND (RaceCode in (1, 2, 3, 4, 5)) ..."
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "RaceCode", raceCodes));

            //Adds " ... AND ((GradeCode >= 16) AND (GradeCode <= 64)) ..."
            sb.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.AND, "GradeCode", gradeCodes));

            //Adds " ... AND (DisabilityCode in (1, 2)) ..."
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "DisabilityCode", disabilityCodes));



            //fullkey
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "FullKey", fullKey));

            ////years
            //sb.Append(SQLHelper.WhereClauseMinAndMaxInclusive(SQLHelper.WhereClauseJoiner.AND, "year", years));


            //order by clause
            //sb.AppendFormat(" ORDER BY {0}", SQLHelper.ConvertToCSV(orderBy, false));
            sb.Append(SQLHelper.GetOrderByClause(orderBy));

            v_dropoutsWWoDisSchoolDistState ds = new v_dropoutsWWoDisSchoolDistState();

            base.GetDS(ds, sb.ToString(), ds.v_DropoutsWWoDisSchoolDistState.TableName);

            return ds;
        }






        public v_DropoutsWWoDisEconELPSchoolDistState GetDropoutData2(List<string> fullKey,
            List<int> schoolType,
            List<int> years,
            List<int> sexCodes,
            List<int> raceCodes,
            List<int> disabilityCodes,
            List<int> gradeCodes,
            List<int> econDisadvCodes,
            List<int> ELPCodes,
            List<string> orderBy)
        {

            StringBuilder sb = new StringBuilder();
            SQLHelper sql = new SQLHelper();


            //TODO:  remove top 100
            sb.Append("select * from v_DropoutsWWoDisEconELPSchoolDistState WHERE ");

            //School Types
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.NONE, "SchoolType", schoolType));



            //Adds " ... AND (SexCode in (1, 2)) ..."
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "SexCode", sexCodes));

            //Adds " ... AND (RaceCode in (1, 2, 3, 4, 5)) ..."
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "RaceCode", raceCodes));

            //Adds " ... AND ((GradeCode >= 16) AND (GradeCode <= 64)) ..."
            sb.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.AND, "GradeCode", gradeCodes));

            //Adds " ... AND (DisabilityCode in (1, 2)) ..."
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "DisabilityCode", disabilityCodes));

            //Adds " ... AND (EconDisadvCode in (1, 2)) ..."
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "EconDisadvCode", econDisadvCodes));

            //Adds " ... AND (ELPCode in (1, 2)) ..."
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "ELPCode", ELPCodes));



            //fullkey
            sb.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "FullKey", fullKey));

            //years
            sb.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.AND, "year", years));


            //order by clause
            //sb.AppendFormat(" ORDER BY {0}", SQLHelper.ConvertToCSV(orderBy, false));
            sb.Append(SQLHelper.GetOrderByClause(orderBy));

            v_DropoutsWWoDisEconELPSchoolDistState ds = new v_DropoutsWWoDisEconELPSchoolDistState();

            base.GetDS(ds, sb.ToString(), ds._v_DropoutsWWoDisEconELPSchoolDistState.TableName);

            return ds;
        }
    }
}
