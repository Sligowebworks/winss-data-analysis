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

        public List<int> years, stypList, gradeCodes, sexCodes, raceCodes,
            disabilityCodes,  econDisadvCodes, migrantCodes, ELPCodes, FAYCodes;
        public List<String> fullkeylist, ActivityCodes, WsasSubjectCodes;
        
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
        public int AgencyYear
        {
            get { return GlobalValues.AgencyYear; }
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

        public Boolean compareSelectedFullKeys
        {
            get
            {
                return
                     (globals.CompareTo.Key == CompareToKeys.SelSchools
                    || globals.CompareTo.Key == CompareToKeys.SelDistricts)
                    ;
            }
        }

        public QueryMarshaller()
        {
            globals = new GlobalValues();
        }

       public  QueryMarshaller(GlobalValues initGlobals)
        {
            globals = initGlobals;
        }
        #endregion //properties
        /// <summary>
        /// Marshalls and Executes the AutoQuery, be sure that prerequisites have been assigned before calling.
        /// </summary>
        public void AutoQuery(DALWIBase dalObj)
        {
            if (GlobalValues == null) throw new Exception("GlobalValues object not assigned");
            Database = dalObj;
            InitLists(); // might have already been called, but will catch any changes
            globals.SQL = Database.SQL = Database.BuildSQL(this);
            Database.Query();
        }
        /// <summary>
        /// Performs no checks, simply queries using the properties already assigned.
        /// Be sure to call: InitLists(); assign a DAL(Database) Object; and a SQL string.
        /// </summary>
        public void ManualQuery()
        {
            if ((globals.TraceLevels & TraceStateUtils.TraceLevel.SQLStatement) 
                == TraceStateUtils.TraceLevel.SQLStatement) 
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
        /// analyses Application State and marshalls Lists used in Querying.
        /// </summary>
        public void InitLists()
        {
            System.Diagnostics.Debug.WriteLine("QueryMarshaller.InitLists() Entered");
            stypList = GetSchoolTypesList(globals.STYP);
            sexCodes = ( globals.Group.Key == GroupKeys.Gender || globals.Group.Key == GroupKeys.RaceGender)?
                    new List<int>(new int[3]{ 1,2,8 })
                    : new List<int>(new int[1]{9});
            raceCodes = (globals.Group.Key == GroupKeys.Race || globals.Group.Key == GroupKeys.RaceGender) ?
                    RaceDisagCodes
                    : new List<int>(new int[] {(int) RaceCodes.All_Races });
            disabilityCodes = (globals.Group.Key == GroupKeys.Disability) ?
                    new List<int>(new int[] { 1, 2 })
                    : new List<int>(new int[] { 9 });
            migrantCodes = (globals.Group.Key == GroupKeys.Migrant) ?
                    new List<int>(new int[] { 1, 2 })
                    : new List<int>(new int[] { 9 });
            if (globals.Group.Key == GroupKeys.Grade
                || GlobalValues.Grade.Key == GradeKeys.AllDisAgg)
            {
                SetLowGradeFloors(globals);
                gradeCodes = new List<int>(new int[] {
                    globals.LOWGRADE,
                    globals.HIGHGRADE
                    });
            }
           else
            {
                gradeCodes = new List<int>(new int[] {int.Parse(GlobalValues.Grade.Value)});
            }
            econDisadvCodes = (globals.Group.Key == GroupKeys.EconDisadv) ?
                    new List<int>(new int[] { 1, 2 })
                    : new List<int>(new int[] { 9 });
            ELPCodes = (globals.Group.Key == GroupKeys.EngLangProf) ?
                    new List<int>(new int[] { 1, 2 })
                    : new List<int>(new int[] { 9 });
            years = (globals.CompareTo.Key != CompareToKeys.Years) ?
                    new List<int>(new int[] { globals.Year })
                    : new List<int>(new int[] { globals.TrendStartYear, globals.Year });

            ActivityCodes = GetActivityCodesList(globals.Show);

            InitFullkeyList();

            if (globals.FAYCode.Key == FAYCodeKeys.CompareFayALL)
            {
                FAYCodes = new List<int>(new int[] { 1, 2, 9 });
            }
            else if (globals.FAYCode.Key == FAYCodeKeys.FAY)
            {
                FAYCodes = new List<int>(new int[]
                    { 
                        (globals.Group.Key == GroupKeys.All)?
                            2 : 9
                    });
            }
            else
            {
                FAYCodes = new List<int>(new int[] 
                    { 
                        (globals.Group.Key == GroupKeys.All)?
                            1 : 9 
                    });
            }
            WsasSubjectCodes = InitWsasSubjectList();

            clauseForCompareSelected = BuildClauseForCompareToSelected();

            if (OnListsInitialized != null) OnListsInitialized(this);
        }
        public delegate void ListsInitializedhandler( QueryMarshaller qm);
        public event ListsInitializedhandler OnListsInitialized;
        
        public List<String> InitWsasSubjectList()
        {
            List<String> list = new List<string>();
            
            if (GlobalValues.SubjectID.Key == SubjectIDKeys.AllTested)
            {
                foreach (String key in GlobalValues.SubjectID.Range.Keys)
                {
                    if (key != SubjectIDKeys.AllTested)
                        list.Add(GlobalValues.SubjectID.Range[key]);
                }
            }
            else
            {
                list.Add(GlobalValues.SubjectID.Value);
            }
            return list;
        }
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

            SligoCS.BL.WI.QueryMarshaller.SetLowGradeFloor(globals, 99, 12); // Default Floor
            SligoCS.BL.WI.QueryMarshaller.SetLowGradeFloor(globals, 98, 16); //Kindergarten
            SligoCS.BL.WI.QueryMarshaller.SetLowGradeFloor(globals, 94, 40); // Grade 6
            SligoCS.BL.WI.QueryMarshaller.SetLowGradeFloor(globals, 95, 44); //Grade 7
            SligoCS.BL.WI.QueryMarshaller.SetLowGradeFloor(globals, 96, 48); // Grade 9
            SligoCS.BL.WI.QueryMarshaller.SetLowGradeFloor(globals, 97, 56); // Grade 10
            SligoCS.BL.WI.QueryMarshaller.SetLowGradeFloor(globals, 0, 28); // WSAS floor
            
        }
        public static void SetLowGradeFloor(GlobalValues globals, int gradecode, int floor)
        {
            if (globals.Grade.Value == gradecode.ToString()
                    && globals.HIGHGRADE > floor
                    && globals.LOWGRADE < floor)
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
        delegate int getIntDelegate(String str);
        public List<int> GetSchoolTypesList(STYP schoolType)
        {
            List<int> schoolTypes;

            if (schoolType.Key != STYPKeys.AllTypes)
            {
                schoolTypes = new List<int>( new int[] {int.Parse(schoolType.Value)});
            }
            else
            {
                getIntDelegate getSchoolTypeInt = delegate (String str){return int.Parse(schoolType.Range[str]); };
                schoolTypes = new List<int>(new int[] {
                    getSchoolTypeInt(STYPKeys.Elem),
                    getSchoolTypeInt(STYPKeys.Mid),
                    getSchoolTypeInt(STYPKeys.Hi),
                    getSchoolTypeInt(STYPKeys.ElSec)
                    //, int.MinValue  // researching why this is in here
                });
            }
            return schoolTypes;
        }
        public List<String> GetActivityCodesList(Show show)
        {
            List<String> list = new List<String>();
            if (show.Key == ShowKeys.Community)
            {
                list.AddRange(new String[] { "RE", "VO" }); 
            }
            else//( show.Value == ShowKeys.Extracurricular)
            {
                list.AddRange(new String[] { "AT", "AC", "MS" }); 
            }
            return list;
        }
        public String BuildClauseForCompareToSelected()
        {
            String clause = String.Empty;
            String field = String.Empty;
            String value = String.Empty;
            String comparison =
                (globals.OrgLevel.Key == OrgLevelKeys.School) ?
                "<>" : "=";

            if (globals.S4orALL.Key == S4orALLKeys.AllSchoolsOrDistrictsIn)
            {
                if (globals.SRegion.Key == SRegionKeys.County)
                {
                    field = "County";
                    value = globals.SCounty;
                }
                else if (globals.SRegion.Key == SRegionKeys.CESA)
                {
                    field = "CESA";
                    value = globals.SCESA;
                }
                else if (globals.SRegion.Key == SRegionKeys.AthleticConf)
                {
                    field = "ConferenceKey";
                    value = globals.SAthleticConf;
                }

                clause = CompareSelectedClauseTemplate(comparison, field, value);
            }
            else
            {
                field = "fullkey";

                List<String> keylist = new List<String>();

                //add the original fullkey
                keylist.Add(FullKeyUtils.GetMaskedFullkey(globals.FULLKEY, globals.OrgLevel));
                keylist.AddRange(
                    FullKeyUtils.ParseFullKeyString(
                        globals.SFullKeys(globals.OrgLevel)
                    )
                );

                clause = SQLHelper.WhereClauseValuesInList(SQLHelper.WhereClauseJoiner.NONE, field, keylist);

                //MZD: don't think this is valid, selected keys are not used accross orglevel:
                /*foreach (String compareToSchool in FullKeyUtils.ParseFullKeyString(globals.SFullKeys(globals.OrgLevel)))
                {
                    String maskedFullKey = FullKeyUtils.GetMaskedFullkey(compareToSchool, globals.OrgLevel);
                    keylist.Add(maskedFullKey);
                }*/

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
        public List<String> BuildOrderByList(DataColumnCollection dataColumns)
        {
            List<String> orderby = new List<String>();

           /* if (GlobalValues.GraphFile.Key == GraphFileKeys.StateTestsSimilar)
            {// special column defined in DALWSASSImilarSchools:
                orderby.Add("sortcolumn DESC");
                orderby.Add("[" + v_WSASDemographics.District_Name + "]");
                return orderby;
            }*/

            //PRIMARY SORT: The primary sort key for the Order By is set by Compare To:

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

            //Comments block from Bug #357:
            //SECONDARY SORT: 

            foreach (String col in Enum.GetNames(typeof(SecondaryOrderByCols)))
            {
                if (dataColumns.Contains(col))
                orderby.Add(col);
            }

            return orderby;
        }
        public GlobalValues GlobalValues
        {
            get { return globals; }
        }
        /// <summary>
        /// Returns a SQL/ADO.NET Order By/Sort Expressoin;
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
                    if (globals.OrgLevel.Key == OrgLevelKeys.District
                        && raceDisagCodes.Contains((int)RaceCodes.Comb))
                            raceDisagCodes.Remove((int)RaceCodes.Comb);
                }

                if (GlobalValues.CompareTo.Key == CompareToKeys.OrgLevel
                    || GlobalValues.OrgLevel.Key == OrgLevelKeys.State)
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
    public enum SecondaryOrderByCols
    {
        SchoolTypeLabel,
        SexCode,
        RaceCode,
        GradeCode,
        DisabilityCode,
        EconDisadvCode,
        ELPCode,
        MigrantCode
    }
}
