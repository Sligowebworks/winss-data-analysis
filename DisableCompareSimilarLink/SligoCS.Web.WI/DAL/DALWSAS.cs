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

            sql.Append("SELECT * FROM v_wsas WHERE ");

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

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "FAYCode", Marshaller.FAYCodes));

            if (Marshaller.GlobalValues.Grade.Value == GradeKeys.AllDisAgg)
            {
                sql.Append(" AND GradeCode <> 99 ");
            }
            else
            {
                sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.AND, "GradeCode", Marshaller.gradeCodes));
            }

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "SubjectID", Marshaller.WsasSubjectCodes));

            if (Marshaller.GlobalValues.S4orALL.Key == S4orALLKeys.AllSchoolsOrDistrictsIn
                && Marshaller.GlobalValues.CompareTo.Key == CompareToKeys.SelSchools
                )
            {// only include schooltype at school level
                sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "SchoolType", Marshaller.stypList));
            }
            
            //fullkey
            if (Marshaller.compareSelectedFullKeys)
            {
                sql.Append(SQLHelper.WhereClauseJoiner.AND).Append(Marshaller.clauseForCompareSelected);
            }
            else
            {
                sql.Append(SQLHelper.WhereClauseValuesInList(
                    SQLHelper.WhereClauseJoiner.AND, "FullKey", Marshaller.fullkeylist));
            }

            sql.Append(SQLHelper.GetOrderByClause(Marshaller.orderByList));

            return sql.ToString();
        }

        public static String WSASGrades(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select distinct gradecode from v_WSAS where ");
            sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.NONE, "year", Marshaller.GlobalValues.Year.ToString()));
            sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "fullkey", SligoCS.BL.WI.FullKeyUtils.GetMaskedFullkey(Marshaller.GlobalValues.FULLKEY,Marshaller.GlobalValues.OrgLevel)));
            sql.Append(" order by gradecode asc ");
            return sql.ToString();
        }

    }
}
