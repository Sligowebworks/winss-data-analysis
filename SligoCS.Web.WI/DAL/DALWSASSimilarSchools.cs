using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using SligoCS.Web.WI.WebSupportingClasses.WI;

using SligoCS.DAL.WI;

namespace SligoCS.DAL.WI
{
    public class DALWSASSimilarSchools : DALWIBase
   {
        public override string BuildSQL(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            StringBuilder sql = new StringBuilder();

            string demographics =  new DALWSASDemographics().BuildSQL(Marshaller);

            if (Marshaller.GlobalValues.Sim.Key == SimKeys.Default)
                demographics = demographics.Replace("SELECT", "SELECT top 6 ");

            if (Marshaller.GlobalValues.NoChce.Key != NoChceKeys.On)
            {
                String[] initial = demographics.Split(
                    (new string[1] { "WHERE" }),
                    StringSplitOptions.None
                    );

                GlobalValues globals = Marshaller.GlobalValues;
                List<String> whereclause = new List<String>();
                Ranges = InitSimilarRanges(Marshaller);

               if (Ranges.Exists(globals.SIZE.Name))
                   whereclause.Add(FindAndFormatRange(globals.SIZE.Name, Ranges, v_WSASDemographics.Enrolled));

                if (Ranges.Exists(globals.SPEND.Name))
                    whereclause.Add(FindAndFormatRangeChar(globals.SPEND.Name, Ranges, v_WSASDemographics.Cost_Per_Member));

                if (Ranges.Exists(globals.ECON.Name))
                    whereclause.Add(FindAndFormatRange(globals.ECON.Name, Ranges, v_WSASDemographics.PctEcon));

                if (Ranges.Exists(globals.LEP.Name))
                    whereclause.Add(FindAndFormatRange(globals.LEP.Name, Ranges, v_WSASDemographics.PctLEP));

                if (Ranges.Exists(globals.DISABILITY.Name))
                    whereclause.Add(FindAndFormatRange(globals.DISABILITY.Name, Ranges, v_WSASDemographics.PctDisabled));

                if (globals.Sim.Key == SimKeys.Outperform)
                {
                    String col;
                    if (globals.SORT.Key == SORTKeys.AdvPlusProf)
                        col = v_WSASDemographics.PCTAdvPlusPCTPrf;
                    else
                        col = v_WSASDemographics.Percent_Advanced;

                     //Marshaller initialized above in InitRanges...
                    string val = Marshaller.Database.GetStringColumn(col);
                    short toss; //only required by TryParse

                    //If not suppressed, then add constraint:
                    if (Int16.TryParse(val, out toss))
                        whereclause.Add("[" + col+ "] > " + val );
                }

                sql.Append(String.Join(" AND ", whereclause.ToArray()));

                //Put it all back together
                sql.Insert(0, initial[0] + " WHERE ");
                sql.Append(" AND ");
                sql.Append(initial[1]);
            }
            else
            {
                sql.Remove(0, sql.Length);
                sql.Append(demographics);
            }

            sql.Append(OrderByClause(Marshaller));

            return sql.ToString();
        }
        public string BuildWsasSimilarCurrentAgencyQuery(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            StringBuilder sql = new StringBuilder();
            Marshaller.InitFullkeyList();

            //sql.Append("SELECT fullkey,  [PctEcon], [PctLEP], [PctDisabled], [PctWhite], [PctBlack], [PctHisp], [PctAsian], [PctAmInd], [Cost Per Member], [District Size]");
            sql.Append("SELECT * ");
            sql.Append(" FROM v_WSASDemographics WHERE");

            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.NONE, "year", Marshaller.years));
             sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "fullkey",
                 SligoCS.BL.WI.FullKeyUtils.GetMaskedFullkey(Marshaller.GlobalValues.FULLKEY,
                 Marshaller.GlobalValues.OrgLevel)));
            
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
            String fullkey = SligoCS.BL.WI.FullKeyUtils.GetMaskedFullkey(Marshaller.GlobalValues.FULLKEY, Marshaller.GlobalValues.OrgLevel);

            order.Append(" ORDER BY ");

            if (Marshaller.GlobalValues.WOW.Key == WOWKeys.WSASCombined
               && Marshaller.GlobalValues.SORT.Key == SORTKeys.AdvPlusProf)
                order.Append("  (case fullkey when  '" + fullkey + "' then fullkey end) desc, (cast(dbo.ConvertNonNumericCodesToZero(ProficientWSAS) as numeric(9,1)) + cast(dbo.ConvertNonNumericCodesToZero(AdvancedWSAS) as numeric(9,1)))   desc, cast(dbo.ConvertNonNumericCodesToZero(AdvancedWSAS) as numeric(9,1)) desc, cast(dbo.ConvertNonNumericCodesToZero(ProficientWSAS) as numeric(9,1)) ");

            if (Marshaller.GlobalValues.WOW.Key == WOWKeys.WSASCombined
                && Marshaller.GlobalValues.SORT.Key == SORTKeys.Advanced)
                order.Append("  (case fullkey when '" + fullkey + "' then fullkey end) desc, cast(dbo.ConvertNonNumericCodesToZero(AdvancedWSAS) as numeric(9,1))");

            //WKCE, Advanced + Proficient:
            if (Marshaller.GlobalValues.WOW.Key == WOWKeys.WKCE
                && Marshaller.GlobalValues.SORT.Key == SORTKeys.AdvPlusProf)
                order.Append("  (case fullkey when  '" + fullkey + "' then fullkey end) desc, (cast(dbo.ConvertNonNumericCodesToZero([Percent Proficient]) as numeric(9,1)) + cast(dbo.ConvertNonNumericCodesToZero([Percent Advanced]) as numeric(9,1)))  desc, cast(dbo.ConvertNonNumericCodesToZero([Percent Advanced]) as numeric(9,1)) desc, cast(dbo.ConvertNonNumericCodesToZero([Percent Proficient]) as numeric(9,1)) ");

            //WKCE, Advanced:
            if (Marshaller.GlobalValues.WOW.Key == WOWKeys.WKCE
               && Marshaller.GlobalValues.SORT.Key == SORTKeys.Advanced)
                order.Append(" (case fullkey when  '" + fullkey + "' then fullkey end) desc, cast(dbo.ConvertNonNumericCodesToZero([Percent Advanced]) as numeric(9,1)) ");

            if (order.Length > 1) order.Append(", ");
            order.Append("[" + v_WSASDemographics.District_Name + "]");

            return order.ToString();
        }
        public  SimilarRanges InitSimilarRanges(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            SimilarRanges ranges = new SimilarRanges();

            DataSet ds = Marshaller.Database.DataSet;
            Marshaller.InitLists();
            Marshaller.Database.SQL = BuildWsasSimilarCurrentAgencyQuery(Marshaller);
            Marshaller.ManualQuery();

            GlobalValues globals = Marshaller.GlobalValues;

           
            if (globals.SIZE.Key == SIZEKeys.On) 
                ranges.Add(InitSizeRange(Marshaller));

            if (globals.SPEND.Key == SPENDKeys.On)
                ranges.Add(InitSpendingRange(Marshaller));

            if (globals.ECON.Key == ECONKeys.On)
                ranges.Add(InitDemographicsRange(Marshaller, globals.ECON.Name, v_WSASDemographics.PctEcon));

            if (globals.LEP.Key == LEPKeys.On)
                ranges.Add(InitDemographicsRange(Marshaller, globals.LEP.Name, v_WSASDemographics.PctLEP));

            if (globals.DISABILITY.Key == DISABILITYKeys.On)
                ranges.Add(InitDemographicsRange(Marshaller, globals.DISABILITY.Name, v_WSASDemographics.PctDisabled));

            //StringBuilder sb = new StringBuilder();
            //ranges.ForEach(delegate(SimilarRange rng) { sb.Append(String.Format("name: {0}; min: {1}; max {2};", rng.MeasureName, rng.Min, rng.Max)); });
            //throw new Exception(sb.ToString());

            return ranges;
        }
        public SimilarRanges Ranges;

#region Init Range Statements
        //http://dpi.wi.gov/oea/similar.html

        private SimilarRange InitSpendingRange(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            String strSpend = Marshaller.Database.GetStringColumn(v_WSASDemographics.Cost_Per_Member);
            short spend;
            int min, max;

            if (!Int16.TryParse(strSpend, out spend)) spend = 0;

            min = 0;

            if (spend < 9500)
            {
                max = 9500;
            }
            else if (spend < 10500)
            {
                max = 10500;
            }
            else if (spend < 11500)
            {
                max = 11500;
            }
             else 
             {
                 max = Int16.MaxValue;
             }

             SimilarRange range = new SimilarRange();
             range.MeasureName = Marshaller.GlobalValues.SPEND.Name;
             range.Min = min.ToString() + ".0";
             range.Max = max.ToString() + ".0"; 

             return range;
        }
        private SimilarRange InitSizeRange(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            String strSize = Marshaller.Database.GetStringColumn(v_WSASDemographics.Enrolled);
            int size = Int16.Parse(strSize);
            int min, max;

            if (size < 501)
            {
                min = 0;
                max = 500;
            }
            else if (size < 1001)
            {
                min = 501;
                max = 1000;
            }
            else if (size < 2001)
            {
                min = 1001;
                max = 2000;
            }
            else if (size < 10001)
            {
                min = 2001;
                max = 10000;
            }
            else
            {
                min = 10001;
                max = Int16.MaxValue; //"infinity"
            }

            SimilarRange range = new SimilarRange();
            range.MeasureName = Marshaller.GlobalValues.SIZE.Name;
            range.Min = min.ToString();
            range.Max = max.ToString();

            return range;
        }
        private SimilarRange InitDemographicsRange(SligoCS.BL.WI.QueryMarshaller Marshaller, String Name, String Column)
        {
            String strMeasure = Marshaller.Database.GetStringColumn(Column);
            double meas = Double.Parse(strMeasure);
            double min, max;

            if (meas < 10.01)
            {
                min = 0;
                max =10;
            }
            else if (meas < 25.01)
            {
                min = 10.01;
                max = 25;
            }
            else if (meas < 50.01)
            {
                min = 25.01;
                max = 50;
            }
            else //if (meas > 50)
            {
                if (Name == Marshaller.GlobalValues.ECON.Name
                    && meas > 75)
                { min = 75.01; }
                else    
                    min = 50.01;
                    
                max = float.MaxValue; // "infinity"
            }

            SimilarRange range = new SimilarRange();
            range.MeasureName = Name;
            range.Min = min.ToString();
            range.Max = max.ToString();

            return range;
        }
#endregion
        public String FindAndFormatRange(String find, SimilarRanges ranges, String field)
        {
            return FindAndFormatRange("[{0}] >= {1} AND [{0}]  <= {2}", find, ranges, field);
        }
        public String FindAndFormatRange( String format, String find, SimilarRanges ranges, String field )
        {
            SimilarRange range = ranges.Find(find);
            return 
                String.Format(format,
                    field, range.Min, range.Max)
            ;
        }
        public String FindAndFormatRangeChar( String find, SimilarRanges ranges, String field)
        {
            return FindAndFormatRange("[{0}] >= '{1}' AND [{0}]  <= '{2}'", find, ranges, field);
        }
    }
}
namespace System.Collections.Generic
{
    public struct SimilarRange
    {
        public String MeasureName;
        public String Min;
        public String Max;
    }
    public class SimilarRanges : List<SimilarRange>
    {
        public bool Exists(String MeasureName)
        {
            return this.Exists(
                delegate(SimilarRange r) { return r.MeasureName == MeasureName; }
            );
        }
        public SimilarRange Find(String MeasureName)
        {
            return this.Find(
                delegate(SimilarRange r) { return r.MeasureName == MeasureName; }
                );
        }
    }
}
