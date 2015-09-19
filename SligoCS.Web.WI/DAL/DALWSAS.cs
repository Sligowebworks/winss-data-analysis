using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.DAL.WI
{
    public class DALWSAS : DALWIBase
    {
        public override string  BuildSQL(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            StringBuilder sql = new StringBuilder();
            String dbObject = "v_wsas";

            sql.Append(SQLHelper.SelectStarFromWhereFormat(dbObject));

            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.NONE, "year", Marshaller.years));

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "SexCode", Marshaller.sexCodes));

            // working around odd SQL Server performance issue by not using (RaceCode in ('1', '2', '3', '4', '5', '6', '8')) on Select/AllSchoolsIn
            if (Marshaller.GlobalValues.S4orALL.Key == S4orALLKeys.AllSchoolsOrDistrictsIn && Marshaller.GlobalValues.CompareTo.Key == CompareToKeys.SelSchools && Marshaller.GlobalValues.Group.Key == GroupKeys.Race)
            {
                sql.Append(" AND (RaceCode <> '9') ");
            }
            else
            {
                sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "RaceCode", Marshaller.raceCodes));
            }

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "DisabilityCode", Marshaller.disabilityCodes));

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "EconDisadvCode", Marshaller.econDisadvCodes));

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "ELPCode", Marshaller.ELPCodes));

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "Migrantcode", Marshaller.migrantCodes));

            if (Marshaller.GlobalValues.CompareTo.Key != CompareToKeys.OrgLevel 
                || Marshaller.GlobalValues.FAYCode.Key == FAYCodeKeys.NonFAY)
                sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "FAYCode", Marshaller.FAYCodes));
            else 
            {
                sql.Append(" AND ( (FullKey NOT in ('XXXXXXXXXXXX') ");
                sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "FAYCode", Marshaller.FAYCodes));
                sql.Append(")  OR ");
                sql.Append("FullKey in ('XXXXXXXXXXXX') AND faycode <> 2");
                sql.Append(")");
            }
            
            Marshaller.gradeCodes.ObeyForceDisAgg = true;

            if (Marshaller.GlobalValues.Grade.Value == GradeKeys.AllDisAgg && Marshaller.GlobalValues.SuperDownload.Key != SupDwnldKeys.True)
            {
                sql.Append(" AND GradeCode <> 99 ");
            }
            else
            {
                sql.Append(Marshaller.GradeCodesClause(SQLHelper.WhereClauseJoiner.AND, "GradeCode", dbObject));
            }

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "SubjectID", Marshaller.WsasSubjectCodes));

            if (Marshaller.GlobalValues.S4orALL.Key == S4orALLKeys.AllSchoolsOrDistrictsIn
                && Marshaller.GlobalValues.CompareTo.Key == CompareToKeys.SelSchools
                )
            {// only include schooltype at school level
                sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "SchoolType", Marshaller.stypList));
            }
            
            sql.Append(Marshaller.FullkeyClause(SQLHelper.WhereClauseJoiner.AND, "FullKey"));

            sql.Append(SQLHelper.GetOrderByClause(Marshaller.orderByList));

            return sql.ToString();
        }

        public static String WSASGrades(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select distinct gradecode from v_WSAS where ");

            if (SligoCS.BL.WI.FullKeyUtils.GetOrgLevelFromFullKeyX(Marshaller.GlobalValues.FULLKEY).Key == OrgLevelKeys.School)
            { //Regardless of OrgLevel, use the fullkey if a school has been selected
                sql.Append("year = (select max(year) from v_WSAS where fullkey = '");
                sql.Append(Marshaller.GlobalValues.FULLKEY);
                sql.Append("' and year <= '" + Marshaller.GlobalValues.Year.ToString() + "')");
            }
            else
            {
                sql.Append("year <= '" + Marshaller.GlobalValues.Year.ToString() + "'");
            }
            sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "fullkey", Marshaller.GlobalValues.FULLKEY));
            sql.Append(" order by gradecode asc ");
            return sql.ToString();
        }

    }
}
