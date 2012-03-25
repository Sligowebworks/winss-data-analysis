using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.DAL.WI
{
    public class DALWSASDemographics : DALWIBase
    {
        public override string BuildSQL(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            StringBuilder sql = new StringBuilder();
            GlobalValues globals = Marshaller.GlobalValues;
            String dbObject =
                ((globals.SubjectID.Key == SubjectIDKeys.Reading
                || globals.SubjectID.Key == SubjectIDKeys.Math) ?
                "v_WSASDemographics"
                : "v_WSASDemographics4810"
                    );

            sql.Append(Marshaller.SelectListFromVisibleColumns(dbObject));

            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.NONE, "year", Marshaller.years));

            //fullkey
            if (Marshaller.GlobalValues.OrgLevel.Key == OrgLevelKeys.District)
                sql.Append(" AND right(fullkey,6) = '03XXXX' ");// all districts in LOCATION
            else //School Level -- State not Supported
                sql.Append(" AND right(fullkey,4) <> 'XXXX' ");//- all school in LOCATION

            //Location
            if (Marshaller.GlobalValues.LF.Key == LFKeys.CESA)
                sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "CESA", Marshaller.GlobalValues.Agency.CESA));
            else if (Marshaller.GlobalValues.LF.Key == LFKeys.County)
                sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "county", Marshaller.GlobalValues.Agency.County));
            //else // State


            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "Racecode", Marshaller.raceCodes));

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "sexcode", Marshaller.sexCodes));

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "DisabilityCode", Marshaller.disabilityCodes));

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "migrantcode", Marshaller.migrantCodes));

            sql.Append(Marshaller.GradeCodesClause(SQLHelper.WhereClauseJoiner.AND, "GradeCode", dbObject));

            sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "FAYCode", "2"));

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "EconDisadvCode", Marshaller.econDisadvCodes));

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "ELPCode", Marshaller.ELPCodes));

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "SubjectID", Marshaller.WsasSubjectCodes));

            sql.AppendFormat(SQLHelper.GetOrderByClause(Marshaller.orderByList));

            return sql.ToString();
        }
    }
}
