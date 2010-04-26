using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.DAL.WI
{
    public class DALWSASSimilarSchools : DALWIBase
   {
        public override string BuildSQL(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            StringBuilder sql = new StringBuilder();
            string demographics = (new DALWSASDemographics()).BuildSQL(Marshaller);

            if (Marshaller.GlobalValues.NoChce.Key != NoChceKeys.On)
            {
                String[] initial = demographics.Split(
                    (new string[1] { "WHERE" }),
                    StringSplitOptions.None
                    );

                ///Alias table, join, and open sub-query paren:
                sql.Append(" tbl INNER JOIN (");
                sql.Append(BuildWsasSimilarSubQuery(Marshaller));
                ///close sub-query paren, sub-query alias, join-on:
                sql.Append(") sub ON ");

                GlobalValues globals = Marshaller.GlobalValues;

                List<String> joincols = new List<String>();

                if (globals.SIZE.Key == SIZEKeys.On)
                    joincols.Add(String.Format("tbl.{0} >= sub.{0} AND tbl.{0}  <= 1000000", v_WSASDemographics.District_Size));

                if (globals.SPEND.Key == SPENDKeys.On)
                    joincols.Add(String.Format("tbl.{0} >= sub.{0} AND tbl.{0}  <= 1000000", v_WSASDemographics.Cost_Per_Member));

                if (globals.ECON.Key == ECONKeys.On)
                    joincols.Add(String.Format("tbl.{0} >= sub.{0} AND tbl.{0}  <= 1600", v_WSASDemographics.PctEcon));

                if (globals.LEP.Key == LEPKeys.On)
                    joincols.Add(String.Format("tbl.{0} >= sub.{0} AND tbl.{0}  <= 1600", v_WSASDemographics.PctLEP));

                if (globals.DISABILITY.Key == DISABILITYKeys.On)
                    joincols.Add(String.Format("tbl.{0} >= sub.{0} AND tbl.{0}  <= 1600", v_WSASDemographics.PctDisabled));

                sql.Append(String.Join(" AND ", joincols.ToArray()));

                //Put it all back together
                sql.Insert(0, initial[0]);
                sql.Append(" WHERE ");
                sql.Append(initial[1]);
            }
            else
            {
                sql.Remove(0, sql.Length);
                sql.Append(demographics);
            }

            sql.Append(OrderByClause(Marshaller));
            
            return sql.ToString().Replace("SELECT", "SELECT top 5 ");
        }
        public string BuildWsasSimilarSubQuery(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT [PctEcon], [PctLEP], [PctDisabled], [PctWhite], [PctBlack], [PctHisp], [PctAsian], [PctAmInd], [Cost Per Member], [District Size]");
            sql.Append(" FROM v_WSASDemographics WHERE");

            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.NONE, "year", Marshaller.years));
             sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "FullKey", Marshaller.fullkeylist));
            
            //and grade = '4' and subjectid = '1RE' and
             if (Marshaller.GlobalValues.Grade.Value == GradeKeys.AllDisAgg)
            {
                sql.Append(" AND GradeCode <> 99 ");
            }
            else
            {
                sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.AND, "GradeCode", Marshaller.gradeCodes));
            }

            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "SubjectID", Marshaller.WsasSubjectCodes));

            sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "RaceCode", "9"));
            sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "SexCode", "9"));
            sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "DisabilityCode", "9"));
            sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "EconDisadvCode", "9"));
            sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "ELPCode", "9"));
            sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "Migrantcode", "9"));
            sql.Append(SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, "FAYCode", Marshaller.FAYCodes));

            return sql.ToString();
        }
        public String OrderByClause(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            StringBuilder order = new StringBuilder();

            order.Append(" ORDER BY ");

            if (Marshaller.GlobalValues.WOW.Key == WOWKeys.WSASCombined
               && Marshaller.GlobalValues.SORT.Key == SORTKeys.AdvPlusProf)
                order.Append("(cast(dbo.ConvertNonNumericCodesToZero(ProficientWSAS) as numeric(9,1)) + cast(dbo.ConvertNonNumericCodesToZero(AdvancedWSAS) as numeric(9,1)))   desc, cast(dbo.ConvertNonNumericCodesToZero(AdvancedWSAS) as numeric(9,1)) desc, cast(dbo.ConvertNonNumericCodesToZero(ProficientWSAS) as numeric(9,1)) ");

            if (Marshaller.GlobalValues.WOW.Key == WOWKeys.WSASCombined
                && Marshaller.GlobalValues.SORT.Key == SORTKeys.Advanced)
                order.Append("cast(dbo.ConvertNonNumericCodesToZero(AdvancedWSAS) as numeric(9,1))");

            //WKCE, Advanced + Proficient:
            if (Marshaller.GlobalValues.WOW.Key == WOWKeys.WKCE
                && Marshaller.GlobalValues.SORT.Key == SORTKeys.AdvPlusProf)
                order.Append("(cast(dbo.ConvertNonNumericCodesToZero([Percent Proficient]) as numeric(9,1)) + cast(dbo.ConvertNonNumericCodesToZero([Percent Advanced]) as numeric(9,1)))  desc, cast(dbo.ConvertNonNumericCodesToZero([Percent Advanced]) as numeric(9,1)) desc, cast(dbo.ConvertNonNumericCodesToZero([Percent Proficient]) as numeric(9,1)) ");

            //WKCE, Advanced:
            if (Marshaller.GlobalValues.WOW.Key == WOWKeys.WKCE
               && Marshaller.GlobalValues.SORT.Key == SORTKeys.Advanced)
                order.Append("cast(dbo.ConvertNonNumericCodesToZero([Percent Advanced]) as numeric(9,1)) ");

            return order.ToString();
        }
    }
}
