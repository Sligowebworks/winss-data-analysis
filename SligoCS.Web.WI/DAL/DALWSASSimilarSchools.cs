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
        static String FRMT_LESSTHAN = "[{0}]  <= {2}";
        static String FRMT_GREATERTHAN = "[{0}] >= {1}";
        static String FRMT_BETWEEN = FRMT_GREATERTHAN + " AND " + FRMT_LESSTHAN;
        static String FRMT_LESSTHAN_CHAR = "cast([{0}] as numeric)  <= {2}";
        static String FRMT_GREATERTHAN_CHAR = "cast([{0}] as numeric) >= {1}";
        static String FRMT_BETWEEN_CHAR = FRMT_GREATERTHAN_CHAR + " AND " + FRMT_LESSTHAN_CHAR;
        
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
                String frmt = String.Empty;
                Ranges = InitSimilarRanges(Marshaller);

                frmt = (globals.Sim.Key == SimKeys.AllSimilar) 
                    ? FRMT_BETWEEN
                    : FRMT_GREATERTHAN
                ;

                if (Ranges.Exists(globals.SIZE.Name))
                    whereclause.Add(FindAndFormatRange(FRMT_BETWEEN,  globals.SIZE.Name, Ranges, v_WSASDemographics.District_Size));

                if (Ranges.Exists(globals.SPEND.Name))
                {
                    String frmtSpend;
                    if (globals.Sim.Key == SimKeys.AllSimilar)
                        frmtSpend = FRMT_BETWEEN_CHAR;
                    else
                        frmtSpend = FRMT_LESSTHAN_CHAR;

                    whereclause.Add(FindAndFormatRange(frmtSpend, globals.SPEND.Name, Ranges, v_WSASDemographics.Cost_Per_Member));
                }

                if (Ranges.Exists(globals.ECON.Name))
                    whereclause.Add(FindAndFormatRange(frmt, globals.ECON.Name, Ranges, v_WSASDemographics.PctEcon));

                if (Ranges.Exists(globals.LEP.Name))
                    whereclause.Add(FindAndFormatRange(frmt, globals.LEP.Name, Ranges, v_WSASDemographics.PctLEP));

                if (Ranges.Exists(globals.DISABILITY.Name))
                    whereclause.Add(FindAndFormatRange(frmt, globals.DISABILITY.Name, Ranges, v_WSASDemographics.PctDisabled));

                if (globals.Sim.Key == SimKeys.Outperform)
                {
                    String col;
                    if (globals.SORT.Key == SORTKeys.AdvPlusProf)
                        col = (globals.WOW.Key == WOWKeys.WKCE)? v_WSASDemographics.PCTAdvPlusPCTPrf : v_WSASDemographics.AdvancedPlusProficientTotalWSAS;
                    else
                        col = (globals.WOW.Key == WOWKeys.WKCE) ? v_WSASDemographics.Percent_Advanced : v_WSASDemographics.AdvancedWSAS;

                     //Marshaller initialized above in InitRanges...
                    string val = Marshaller.Database.GetStringColumn(col);
                    Decimal toss; //only required by TryParse

                    //If not suppressed, then add constraint:
                    if (Decimal.TryParse(val, out toss))
                        whereclause.Add("cast(dbo.[ConvertNonNumericCodesToZeroFloat]([" + col + "]) as float) >= " + val);
                }

                sql.Append(String.Join(" AND ", whereclause.ToArray()));

                //Put it all back together
                sql.Insert(0, initial[0] + " WHERE ");
                sql.Append(" AND ");
                sql.Append(initial[1]);
            }
            else
            {
                sql = new StringBuilder(demographics);
            }

            //before we add our custom order by, check that the framework hasn't added one:
             if (sql.ToString().ToLower().Contains("order by"))
            { //remove the framework order-by-clause:
                int start = sql.ToString().IndexOf("order by",StringComparison.CurrentCultureIgnoreCase);
                sql.Remove(start, sql.Length - start);
            }

            sql.Append(OrderByClause(Marshaller));

            return sql.ToString();
        }
        public string BuildWsasSimilarCurrentAgencyQuery(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            StringBuilder sql = new StringBuilder();
            String dbObject = "v_WSASDemographics";
            Marshaller.InitFullkeyList();

            sql.Append(SQLHelper.SelectColumnListFromWhereFormat(Marshaller.SelectListFromVisibleColumns(), dbObject));

            sql.Append(SQLHelper.WhereClauseSingleValueOrInclusiveRange(SQLHelper.WhereClauseJoiner.NONE, "year", Marshaller.years));

            String fullkey = SligoCS.BL.WI.FullKeyUtils.GetMaskedFullkey(Marshaller.GlobalValues.FULLKEY, Marshaller.GlobalValues.OrgLevel);
            sql.Append(SQLHelper.WhereClauseEquals(SQLHelper.WhereClauseJoiner.AND, "FullKey",fullkey));
            
            //and grade = '4' and subjectid = '1RE' and
             if (Marshaller.GlobalValues.Grade.Value == GradeKeys.AllDisAgg && Marshaller.GlobalValues.SuperDownload.Key != SupDwnldKeys.True)
            {
                sql.Append(" AND GradeCode <> 99 ");
            }
            else
            {
                sql.Append(Marshaller.GradeCodesClause(SQLHelper.WhereClauseJoiner.AND, "GradeCode", dbObject));
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

            String floatToTop = (Marshaller.GlobalValues.Sim.Key == SimKeys.AllSimilar)
                ? String.Empty
                : "  (case fullkey when  '" + fullkey + "' then fullkey end) desc, "
            ;

            order.Append(" ORDER BY ");

            if (Marshaller.GlobalValues.WOW.Key == WOWKeys.WSASCombined
               && Marshaller.GlobalValues.SORT.Key == SORTKeys.AdvPlusProf)
                order.Append(floatToTop +" (cast(dbo.[ConvertNonNumericCodesToZeroFloat](AdvancedPlusProficientTotalWSAS) as numeric(9,1)) )   desc ");

            if (Marshaller.GlobalValues.WOW.Key == WOWKeys.WSASCombined
                && Marshaller.GlobalValues.SORT.Key == SORTKeys.Advanced)
                order.Append(floatToTop + " cast(dbo.[ConvertNonNumericCodesToZeroFloat](AdvancedWSAS) as numeric(9,1)) desc");

            //WKCE, Advanced + Proficient:
            if (Marshaller.GlobalValues.WOW.Key == WOWKeys.WKCE
                && Marshaller.GlobalValues.SORT.Key == SORTKeys.AdvPlusProf)
                order.Append(floatToTop + " (cast(dbo.[ConvertNonNumericCodesToZeroFloat]([PCTAdvPlusPCTPrf]) as numeric(9,1)))  desc ");

            //WKCE, Advanced:
            if (Marshaller.GlobalValues.WOW.Key == WOWKeys.WKCE
               && Marshaller.GlobalValues.SORT.Key == SORTKeys.Advanced)
                order.Append(floatToTop + " cast(dbo.[ConvertNonNumericCodesToZeroFloat]([Percent Advanced]) as numeric(9,1)) desc ");

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
//            throw new Exception(Marshaller.Database.SQL);

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
            Double spend;
            int min, max;

            if (!Double.TryParse(strSpend, out spend)) spend = 0;

            min = 0;

            if (spend < 9500.01)
            {
                max = 9500;
            }
            else if (spend < 10500.01)
            {
                max = 10500;
            }
            else if (spend < 11500.01)
            {
                max = 11500;
            }
             else 
             {
                 max = Int32.MaxValue;
             }

             SimilarRange range = new SimilarRange();
             range.MeasureName = Marshaller.GlobalValues.SPEND.Name;
             range.Min = min.ToString() + ".0";
             range.Max = max.ToString() + ".0"; 

             return range;
        }
        private SimilarRange InitSizeRange(SligoCS.BL.WI.QueryMarshaller Marshaller)
        {
            String strSize = Marshaller.Database.GetStringColumn(v_WSASDemographics.District_Size);
            Int32 size = Int32.Parse(strSize);
            Int32 min, max;

            if (size < 501)
            {
                min = 1;
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
                max = Int32.MaxValue; //"infinity"
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

                if (Name == Marshaller.GlobalValues.ECON.Name
                     && meas < 75.01)
                { max = 75; }
                else
                    max = 100; // "infinity"
            }

            SimilarRange range = new SimilarRange();
            range.MeasureName = Name;
            range.Min = min.ToString();
            range.Max = max.ToString();

            return range;
        }
#endregion
        public String FindAndFormatRange( String format, String find, SimilarRanges ranges, String field )
        {
            SimilarRange range = ranges.Find(find);
            return 
                String.Format(format,
                    field, range.Min, range.Max)
            ;
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
