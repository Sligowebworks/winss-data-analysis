using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SligoCS.Web.WI.WebSupportingClasses.WI;
using SligoCS.DAL.WI;

namespace SligoCS.BL.WI
{
    public class QueryMarshaller
    {
        #region properties (and constructors)
        private GlobalValues globals;
        private DALWIBase database;
        private List<int> raceDisagCodes;
        private DataColumnCollection dataColumns;

        public List<String> years, FAYCodes, fullkeylist;

        public QueryArgumentsWithDisagg stypList, gradeCodes, sexCodes, raceCodes, disabilityCodes, econDisadvCodes, migrantCodes, ELPCodes, WsasSubjectCodes, WMASCodes, CourseTypeCodes, ActivityCodes, GradReqSubjCodes, TQSubjectCodes, CostTypeCodes;
        
        public String clauseForCompareSelected;
        /// <summary>
        /// NOTE This enum is BAD! Race Codes are not constant across years.
        /// Luckily, NA and Combined codes are consistent across years, and the other values are not being used in a consequential way, as of Mar 2011.
        /// USE WITH CAUTION.
        /// </summary>
        public  enum RaceCodes :int
        {
            Amer_Indian = 0,
            Asian = 1,
            Black = 2,
            Hisp_Any_Race = 3,
            Pacific_Islander = 4,
            White = 5,
            Two_Races = 6,
            RaceEth_NA = 7,
            Comb = 8,
            All_Races = 9
        };

        private static Dictionary<Int16, Int16> gradeCodeFloorMap;
        protected static Dictionary<Int16, Int16> GradeCodeFloorMap
        {
            get
            {
                if (gradeCodeFloorMap == null)
                {
                    gradeCodeFloorMap = new Dictionary<Int16, Int16>();
                    gradeCodeFloorMap.Add(99, 12);   // Default Floor
                    gradeCodeFloorMap.Add(98, 16);   //Kindergarten
                    gradeCodeFloorMap.Add(94, 40);  // Grade 6
                    gradeCodeFloorMap.Add(95, 44);  //Grade 7
                    gradeCodeFloorMap.Add(96, 48);  //Grade 9
                    gradeCodeFloorMap.Add(97, 56); // Grade 10
                    gradeCodeFloorMap.Add(0, 28);    //WSAS floor
                }

                return gradeCodeFloorMap;
            }
        }

        private List<String> _orderByList;

        public List<String> orderByList
        {
            get 
            { 
                if(_orderByList == null) _orderByList = BuildOrderByList(DataColumns);

                return _orderByList; 
            }
            set { _orderByList = value; }
        }
        public DALWIBase Database
        {
            get 
            { 
                if (database == null) throw new Exception("DALWIBASE obj not set");
                return database;
            }
            set { database = value; }
        }

        public DataColumnCollection DataColumns
        {
            get
            {
                dataColumns =  
                    (Database.DataSet.Tables.Count < 1) ? new DataTable().Columns
                    : Database.DataSet.Tables[0].Columns;
                return dataColumns;
            }
        }

        #endregion //properties

        public QueryMarshaller()
            :this(new GlobalValues()) {        }

       public  QueryMarshaller(GlobalValues initGlobals)
        {
            globals = initGlobals;
            InitQueryArgumentObjects();
       }

        private void InitQueryArgumentObjects() 
        {
            sexCodes = new QueryArgumentsWithDisagg(globals);
            raceCodes = new QueryArgumentsWithDisagg(globals);
            disabilityCodes = new QueryArgumentsWithDisagg(globals);
            migrantCodes = new QueryArgumentsWithDisagg(globals);
            econDisadvCodes = new QueryArgumentsWithDisagg(globals);
            ELPCodes = new QueryArgumentsWithDisagg(globals);
            gradeCodes = new QueryArgumentsWithDisagg(globals);

            stypList = new QueryArgumentsWithDisagg(globals);
            WMASCodes = new QueryArgumentsWithDisagg(globals);
            WsasSubjectCodes = new QueryArgumentsWithDisagg(GlobalValues);
            CourseTypeCodes = new QueryArgumentsWithDisagg(globals);
            ActivityCodes = new QueryArgumentsWithDisagg(globals);
            GradReqSubjCodes = new QueryArgumentsWithDisagg(globals);
            TQSubjectCodes = new QueryArgumentsWithDisagg(globals);
            CostTypeCodes = new QueryArgumentsWithDisagg(globals);

            stypList.ObeyForceDisAgg = true;
            WMASCodes.ObeyForceDisAgg = true;
            CourseTypeCodes.ObeyForceDisAgg = true;
            GradReqSubjCodes.ObeyForceDisAgg = true;
            TQSubjectCodes.ObeyForceDisAgg = true;
            CostTypeCodes.ObeyForceDisAgg = true;
        }
        
        /// <summary>
        /// Marshalls and Executes the AutoQuery, be sure that prerequisites have been assigned before calling.
        /// </summary>
        public void AutoQuery(DALWIBase dalObj)
        {
            if (GlobalValues == null) throw new Exception("GlobalValues object not assigned");
            Database = dalObj;
            InitLists(); // might have already been called, but will catch any changes
            globals.SQL = Database.SQL = Database.BuildSQL(this);
            if (GlobalValues.SuperDownload.Key == SupDwnldKeys.True && ((GlobalValues.TraceLevels & TraceStateUtils.TraceLevels.sql) != 0))
                throw new Exception(Database.SQL);
            Database.Query();
        }
        /// <summary>
        /// Performs no checks, simply queries using the properties already assigned.    
        /// Be sure to call: InitLists(); assign a DAL(Database) Object; and a SQL string.
        /// </summary>
        public void ManualQuery()
        {
            if ((globals.TraceLevels & TraceStateUtils.TraceLevels.sql) 
                == TraceStateUtils.TraceLevels.sql) 
                globals.SQL = globals.SQL + "//" + Database.SQL;

            Database.Query();
        }
        public void AssignQuery(DALWIBase dal, String query)
        {
            Database = dal;
            Database.SQL = query;
            Database.Query();
        }
        /// <summary>
        /// Analyzes Application State and marshalls Lists used in Querying.
        /// </summary>
        public void InitLists()
        {
            SetSubsSTYPlist(stypList);

            sexCodes.DisAggValues = delegate  () { return new List<String>( new String[] { "1","2","8"}); };
            sexCodes.ArgSub = delegate()
                { return
                    (globals.Group.Key == GroupKeys.Gender || globals.Group.Key == GroupKeys.RaceGender)
                      ? sexCodes.DisAggValues()
                      : new List<String>(new String[1] { "9" });
                };

            raceCodes.DisAggValues = delegate() { return RaceDisagCodes.ConvertAll<String>(SQLHelper.ConvertIntToString); };
            raceCodes.ArgSub = delegate()
                { return 
                  (globals.Group.Key == GroupKeys.Race || globals.Group.Key == GroupKeys.RaceGender) 
                      ? raceCodes.DisAggValues()
                      : new List<String>(new String[] {((int)RaceCodes.All_Races).ToString() });
                };

            disabilityCodes.DisAggValues = delegate() { return new List<String>(new string[] { "1", "2" }); };
            disabilityCodes .ArgSub = delegate()
            { return 
                    (globals.Group.Key == GroupKeys.Disability)
                    ? disabilityCodes.DisAggValues()
                    : new List<String>(new String[] { "9" });
            };

            migrantCodes.DisAggValues = delegate() { return new List<String>(new String[] { "1", "2" }); };
            migrantCodes.ArgSub = delegate()
            { return  (globals.Group.Key == GroupKeys.Migrant) 
                    ? migrantCodes.DisAggValues()
                    : new List<String>(new String[] { "9" });
            };
            
            econDisadvCodes.DisAggValues = delegate () {return new List<String>( new String[] {"1","2"}); };
            econDisadvCodes.ArgSub = delegate()
            { return (globals.Group.Key == GroupKeys.EconDisadv) 
                ? econDisadvCodes.DisAggValues()
                : new List<String>(new String[] {"9" });
            };

            ELPCodes.DisAggValues = delegate() {return new List<String>( new String[] { "1","2"}); };
            ELPCodes.ArgSub = delegate()
            { return (globals.Group.Key == GroupKeys.EngLangProf)
                    ? ELPCodes.DisAggValues()
                    : new List<String>(new String[] { "9" });
            };

            gradeCodes.DisAggValues = delegate()
            {
                SetLowGradeFloors(globals);
                return new List<String>(new String[] {
                    globals.LOWGRADE.ToString(),
                    globals.HIGHGRADE.ToString()
                    });
            };
            gradeCodes.ArgSub = delegate()
            { return
                  (globals.Group.Key == GroupKeys.Grade
                  || GlobalValues.Grade.Key == GradeKeys.AllDisAgg)
                  ? gradeCodes.DisAggValues()
                  : new List<String>(new String[] { GlobalValues.Grade.Value });
            };

            if (globals.Group.Key == GroupKeys.Grade) gradeCodes.ObeyForceDisAgg = true;

            years = (globals.CompareTo.Key != CompareToKeys.Years) ?
                    new List<String>(new String[] { globals.Year.ToString() })
                    : new List<String>(new String[] { globals.TrendStartYear.ToString(), globals.Year.ToString() });

            WMASCodes.ArgSub = delegate()
            {
                return new List<String>(new String [] {GlobalValues.WMAS.Value});
            };
            WMASCodes.DisAggValues = delegate()
            {
                List<String> list = new List<string>();

                foreach (String key in GlobalValues.WMAS.Range.Keys)
                {
                        list.Add(GlobalValues.WMAS.Range[key]);
                }
                return list;
            };

            CourseTypeCodes.ArgSub = delegate()
            {
                return new List<String>(new String[] { GlobalValues.CourseTypeID.Value });
            };
            CourseTypeCodes.DisAggValues = delegate()
            {
                List<string> list = new List<string>();

                foreach (String key in GlobalValues.CourseTypeID.Range.Keys)
                {
                    list.Add(GlobalValues.CourseTypeID.Range[key]);
                }
                return list;
            };

            ActivityCodes.ArgSub = delegate()
            {
                List<String> list = new List<String>();
                if (GlobalValues.Show.Key == ShowKeys.Community)
                {
                    list.AddRange(new String[] { "RE", "VO" });
                }
                else//( show.Value == ShowKeys.Extracurricular)
                {
                    list.AddRange(new String[] { "AT", "AC", "MS" });
                }
                return list;
            };
            ActivityCodes.DisAggValues = delegate()
            {
                return new List<String>
                    (new String[] { "AT", "AC", "MS", "RE", "VO" });
            };

            GradReqSubjCodes.ArgSub = delegate()
            {
                List<String> subjects;
                if (GlobalValues.GRSbj.Key == GRSbjKeys.StateLaw)
                {
                    subjects = new List<String>(new String[] { "1", "7" });
                }
                else
                {
                    subjects = new List<String>(new String[] { "8", "13" });
                }
                return subjects;
            };
            GradReqSubjCodes.DisAggValues = delegate()
            {
                return new List<String>(new String[] { "1", "13" });
            };

            TQSubjectCodes.ArgSub = delegate(){ return new List<String>(new String[] { GlobalValues.TQSubjects.Value });};
            TQSubjectCodes.DisAggValues = delegate()
            {
                List<String> list = new List<String>();
                foreach (string key in GlobalValues.TQSubjects.Range.Keys)
                {
                    if (GlobalValues.TQSubjects.Key != TQSubjectsKeys.All)
                        list.Add(GlobalValues.TQSubjects.Range[key]);
                }
                return list;
            };

            CostTypeCodes.ArgSub = delegate()
            {
                return new List<string>
                    (new String[] {"CC", GlobalValues.CT });
            };
            CostTypeCodes.DisAggValues = delegate()
            {
                List<String> ct = new List<string>(new String[] { "CC" });
                foreach (String key in GlobalValues.CT.Range.Keys)
                {
                    ct.Add(GlobalValues.CT.Range[key]);
                } 
                
                return ct;
            };

            InitFullkeyList();

            FAYCodes = new List<String>();

            if (globals.FAYCode.Key == FAYCodeKeys.CompareFayALL)
            {
                FAYCodes.Add("2");
                FAYCodes.Add("9");
            }
            else if (globals.FAYCode.Key == FAYCodeKeys.FAY)
            {
                FAYCodes.Add("2");
            }
            else
            {
                FAYCodes.Add("9");
            }

            WsasSubjectCodes.DisAggValues =  delegate()
            {
                List<String> list = new List<string>();

                foreach (String key in GlobalValues.SubjectID.Range.Keys)
                {
                    if (key != SubjectIDKeys.AllTested)
                        list.Add(GlobalValues.SubjectID.Range[key]);
                }
                return list;
            };

            WsasSubjectCodes.ArgSub = delegate() 
            {
                if (GlobalValues.SubjectID.Key == SubjectIDKeys.AllTested)
                { return WsasSubjectCodes.DisAggValues(); }
                else
                {
                    return new List<String>(new string[] { GlobalValues.SubjectID.Value });
                }
            };

            clauseForCompareSelected = BuildClauseForCompareToSelected();

            if (OnListsInitialized != null) OnListsInitialized(this);
        }

        public delegate void ListsInitializedhandler( QueryMarshaller qm);
        public event ListsInitializedhandler OnListsInitialized;
        
         /// <summary>
        /// prepare fullkeylist for use
        /// </summary>
        public void InitFullkeyList()
        {
            fullkeylist = BuildFullKeyList(globals.FULLKEY);
        }
        public static void SetLowGradeFloors(GlobalValues globals)
        {
            //kludge :
            if (globals.LOWGRADE < 12) globals.LOWGRADE = 12;

            int floor = GradeCodeFloorMap[Int16.Parse(globals.Grade.Value)];
            if ( globals.HIGHGRADE > floor && globals.LOWGRADE < floor)
                    globals.LOWGRADE = floor;
        }
        
        public List<String> BuildFullKeyList(String fullkey)
        {
            List<String> keylist = new List<string>();
            
            fullkey = FullKeyUtils.GetMaskedFullkey(fullkey, globals.OrgLevel);
            
            if ((globals.CompareTo.Key == CompareToKeys.Years) || (globals.CompareTo.Key == CompareToKeys.Current))
                //When comparing to Prior Years or Current School Data, 
                keylist.Add(fullkey);

            else if (globals.CompareTo.Key == CompareToKeys.OrgLevel)
            {
                //always add the State fullkey to the list.
                keylist.Add(FullKeyUtils.StateFullKey(fullkey));
                
                if (globals.OrgLevel.Key != OrgLevelKeys.State)
                {
                    //org level is District or School
                    keylist.Add(FullKeyUtils.DistrictFullKey(fullkey));
                }
                if (globals.OrgLevel.Key == OrgLevelKeys.School)
                {
                    //org level is school
                    keylist.Add(FullKeyUtils.SchoolFullKey(fullkey));
                }
            }

            return keylist;
        }
        public delegate String getStringDelegate(String str);
        public void SetSubsSTYPlist(QueryArgumentsWithDisagg schoolTypes)
        {
            schoolTypes.DisAggValues = delegate() 
            {
                getStringDelegate getSchoolType = delegate(String str) { return int.Parse(GlobalValues.STYP.Range[str]).ToString(); };

                List<String> list =  new List<String>(new String[] {
                    getSchoolType(STYPKeys.Elem),
                    getSchoolType(STYPKeys.Mid),
                    getSchoolType(STYPKeys.Hi),
                    getSchoolType(STYPKeys.ElSec)
                });

                if (schoolTypes.ForceDisAgg) list.Add(getSchoolType(STYPKeys.StateSummary));

                return list;
            };

            schoolTypes.ArgSub =  delegate()
            {
                if (GlobalValues.STYP.Key != STYPKeys.AllTypes)
                {
                    return new List<String>(new String[] { GlobalValues.STYP.Value });
                }
                else
                {
                    return schoolTypes.DisAggValues();
                }
            };
        }

        public String STYPClause(SQLHelper.WhereClauseJoiner join, String field, String dbObject)
        {
            String clause;

             if (stypList.ForceDisAgg && GlobalValues.Group.Key == GroupKeys.Grade)
            {
                clause =  "  " + SQLHelper.GetJoinerString(join) + " ( SchoolType in ('9') AND right(fullkey, 1) = 'X' OR right(fullkey, 1) <> 'X' "
                    + SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.AND, field, stypList) + " ) ";
            }
            else
            {
                clause =  SQLHelper.WhereClauseValuesInList(join, field, stypList);
            }

            return clause +" "+ SQLHelper.GetJoinerString(SQLHelper.WhereClauseJoiner.AND) + " AgencyType IN ('03', '04', '4C', '49', 'XX')";
        }

        public String GradeCodesClause(SQLHelper.WhereClauseJoiner join, String field, String dbObject )
        {
            if (gradeCodes.ForceDisAgg)
            {
                return BuildAutoGradeCodeClause(join, field, dbObject);
            }
            else
            {
                return SQLHelper.WhereClauseSingleValueOrInclusiveRange(join, field, gradeCodes);
            }
        }

        public String BuildAutoGradeCodeClause(SQLHelper.WhereClauseJoiner join, String field, String dbObject)
        {
            int floor = GradeCodeFloorMap[Int16.Parse(globals.Grade.Value)];
            return "  "+SQLHelper.GetJoinerString(join)+" " + 
                String.Format(@"(
    {0} >= (select top 1  CASE WHEN lowgrade < {2} THEN {2} ELSE lowgrade END from tblAgencyFull where  {1}.year = tblAgencyFull.year and {1}.fullkey = tblAgencyFull.fullkey)
    AND {0} <= (select top 1 highgrade from tblAgencyFull where {1}.year = tblAgencyFull.year and {1}.fullkey = tblAgencyFull.fullkey)
    OR fullkey = 'XXXXXXXXXXXX'  
    OR {0}='99'
) "
        , field
        , dbObject
        , floor
            );
        }

        public String FullkeyClause(SQLHelper.WhereClauseJoiner join, String field)
        {
            if (globals.CompareTo.Key == CompareToKeys.SelSchools
                    || globals.CompareTo.Key == CompareToKeys.SelDistricts)
            {
                return " "+SQLHelper.GetJoinerString(join)+" "+BuildClauseForCompareToSelected();
            }
            else
            {
                return SQLHelper.WhereClauseValuesInList(join, field, fullkeylist);
            }
        }

        public String BuildClauseForCompareToSelected()
        {
            String clause = String.Empty;
            String region = String.Empty;
            String value = String.Empty;
            String comparison =
                (globals.OrgLevel.Key == OrgLevelKeys.School) ?
                "<>" : "=";

            if (globals.S4orALL.Key == S4orALLKeys.AllSchoolsOrDistrictsIn)
            {
                if (globals.SRegion.Key == SRegionKeys.County)
                {
                    region = "County";
                    value = globals.SCounty;
                }
                else if (globals.SRegion.Key == SRegionKeys.CESA)
                {
                    region = "CESA";
                    value = globals.SCESA;
                }
                else if (globals.SRegion.Key == SRegionKeys.AthleticConf)
                {
                    region = "ConferenceKey";
                    value = globals.SAthleticConf;
                }

                if (globals.SRegion.Key == SRegionKeys.Statewide)
                    clause = CompareSelectedClauseStatewideTemplate(comparison);
                else
                    clause = CompareSelectedClauseTemplate(comparison, region, value);
            }
            else
            {
                region = "fullkey";

                List<String> keylist = new List<String>();

                //add the original fullkey
                keylist.Add(FullKeyUtils.GetMaskedFullkey(globals.FULLKEY, globals.OrgLevel));
                keylist.AddRange(
                    FullKeyUtils.ParseFullKeyString(
                        globals.SFullKeys(globals.OrgLevel)
                    )
                );

                clause = SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.NONE, region, keylist);
            }
            return clause;
        }

        private String CompareSelectedClauseTemplate( String Operator, String Name, String Value)
        {
            String template = " ( ( {0} in ('{1}') and right(fullkey,1){2}'X' ) or FullKey in ('{3}')) ";
            
            return String.Format(template,
               Name, Value, Operator, 
                FullKeyUtils.GetMaskedFullkey(globals.FULLKEY, globals.OrgLevel)
                );
        }
        private String CompareSelectedClauseStatewideTemplate(String Operator)
        {
            String template = " ( ( right(fullkey,1){0}'X' ) or FullKey in ('{1}')) ";

            //return an inocuous where clause:
            if (GlobalValues.SuperDownload.Key == SupDwnldKeys.True) return " 1=1 ";

            return String.Format(template,
                Operator,
                FullKeyUtils.GetMaskedFullkey(globals.FULLKEY, globals.OrgLevel)
                );
        }
        public List<String> BuildOrderByList(DataColumnCollection dataColumns)
        {
            List<String> orderby = new List<String>();

            BuildCompareToOrderBy(orderby, dataColumns);

            BuildOrderBySecondarySort(orderby, dataColumns);            

            return orderby;
        }
        public void BuildCompareToOrderBy(List<String> orderby, DataColumnCollection dataColumns)
        {
            if (globals.CompareTo.Key == CompareToKeys.Years)
            {
                if (dataColumns.Contains(PrimaryOrderByCols.year.ToString())
                    )
                    orderby.Add(PrimaryOrderByCols.year.ToString() + " ASC");
            }
            else if (globals.CompareTo.Key == CompareToKeys.OrgLevel)
            {
                if (dataColumns.Contains(PrimaryOrderByCols.OrgLevelLabel.ToString()))
                    orderby.Add(PrimaryOrderByCols.OrgLevelLabel.ToString());
            }

            else /*if (globals.CompareTo.Key == CompareToKeys.SelSchools
                || globals.CompareTo.Key == CompareToKeys.SelDistricts
                ||globals.CompareTo.Key == CompareToKeys.Current)*/
            {
                if (dataColumns.Contains(PrimaryOrderByCols.Name.ToString()))
                    orderby.Add(PrimaryOrderByCols.Name.ToString());
            }
        }
        public void BuildOrderBySecondarySort(List<String> orderby, DataColumnCollection dataColumns)
        {
            const String stypelabel = "SchoolTypeLabel";
            if (dataColumns.Contains(stypelabel))
                orderby.Add(stypelabel);

            BuildOrderByViewByGroup(orderby, dataColumns);
        }
        public void BuildOrderByViewByGroup(List<String> orderby, DataColumnCollection dataColumns)
        {
            foreach (String col in Enum.GetNames(typeof(ViewByGroupOrderByCols)))
            {
                if (dataColumns.Contains(col))
                    orderby.Add(col);
            }
        }
        public GlobalValues GlobalValues
        {
            get { return globals; }
        }
        /// <summary>
        /// Returns a SQL/ADO.NET Order By/sort Expressoin;
        /// Caution: uses DataColumns property, which will have different values depending on 
        /// the state of the Database.DataSet. Recommend to call after the DataSet has been filled.
        /// </summary>
        /// <returns></returns>
        public String GetOrderByString()
        {
            System.Text.StringBuilder orderby = new System.Text.StringBuilder();

            (BuildOrderByList(DataColumns)).ForEach(
                delegate(String item)
                {
                    orderby.Append(
                        ((orderby.Length > 0) ? ", " : String.Empty) +
                        item
                    );
                }
            );
            return orderby.ToString();
        }
        /// <summary>
        /// Values used to initialize QueryMarshaller.raceCode List when Race Dimension is Disaggregated. 
        /// May be set before query-time to determine what race break-outs to return.
        /// </summary>
        public List<int> RaceDisagCodes
        {
            get 
            {
                if (raceDisagCodes == null)
                {
                    raceDisagCodes = new List<int>(
                        new int[] 
                        { 
                            (int) RaceCodes.Amer_Indian,
                            (int) RaceCodes.Asian,
                           (int)  RaceCodes.Black,
                            (int) RaceCodes.Hisp_Any_Race,
                            (int) RaceCodes.Pacific_Islander,
                           (int)  RaceCodes.Two_Races,
                            (int) RaceCodes.White,
                            (int) RaceCodes.Comb                            
                        });
                }

                if ((GlobalValues.CompareTo.Key == CompareToKeys.OrgLevel || GlobalValues.OrgLevel.Key == OrgLevelKeys.State)
                    && GlobalValues.SuperDownload.Key != SupDwnldKeys.True)
                {
                    raceDisagCodes.Remove((int)RaceCodes.Comb);
                }

                return raceDisagCodes; 
            }
            set { raceDisagCodes = value; }
        }
    }
    public enum PrimaryOrderByCols
    {
        year,
        OrgLevelLabel,
        fullkey,
        Name
    }
    public enum ViewByGroupOrderByCols
    {
        SexCode,
        RaceCode,
        GradeCode,
        DisabilityCode,
        EconDisadvCode,
        ELPCode,
        MigrantCode
    }
}
