using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Reflection;
using System.Web;
using System.Web.SessionState;
using System.Xml;

using SligoCS.BL.WI;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
    /// <summary>
    /// Basically a Struct, for User Input Parameters.
    /// It will populate its properties by looking at the QueryString first, 
    /// then default value in 'StickyParameterDefaults.xml'.
    /// </summary>
    [Serializable]
    public class StickyParameter
    {
        private static Dictionary<string, string> defaultValues = new Dictionary<string, string>();
         
#region constructor and initializers
        public StickyParameter()
        {
        }

        private static void InitializeProperty(Object This, PropertyInfo property)
            // NOT USED!
        {
             if (property.PropertyType ==  typeof (ParameterValues))
            {// not necessary
                return;
            }
            object typedValue = null;
            object ovalue = GetParamFromUser(property.Name);
            if (ovalue == null) ovalue = GetParamDefault(property.Name);
            typedValue = Convert.ChangeType(ovalue, property.PropertyType);
            property.SetValue(This, typedValue, null);
        }
        public static String InitializeProperty(String name)
        {
           object obj =  GetParamFromUser(name);
            if (obj == null) obj = GetParamDefault(name);
            if (obj.ToString() == String.Empty) obj = null;
            return (obj != null) ? obj.ToString() : null;
        }
        /// <summary>
        /// Gets a single Sticky Parameter from the context:  Querystring first,
        /// then Default value from xml file 'StickyParameterDefaults.xml'.
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns>class, object</returns>
        public static object GetParamDefault(string paramName)
        {
            object ovalue = null;

            if (paramName.Contains("."))
            {
                paramName = paramName.Substring(paramName.LastIndexOf(".") + 1);
            }

            if (paramName != "Item")
            {
             //if (DefaultValues.ContainsKey(paramName))
                try
                {
                    //ovalue = ConfigurationManager.AppSettings[name];
                    ovalue = DefaultValues[paramName];
                }
                catch (Exception e)
                {
                    throw new Exception("ParamName[" + paramName + "], not found in configuration", e);
                    }
            }

            return ovalue;
        }
        public static Object GetParamFromUser(String paramName)
        {
            NameValueCollection queryString = new NameValueCollection();
            
            if (paramName.Contains("."))
            {
                paramName = paramName.Substring(paramName.LastIndexOf(".") + 1);
            }
            if (HttpContext.Current != null && HttpContext.Current.Request.QueryString.Count > 0)
            {
                queryString = HttpContext.Current.Request.QueryString;
            }
            return queryString[paramName];
        }

        /// <summary>
        /// Reads the system default values from XML file 'StickyParameterDefaults.xml'
        /// </summary>
        private static Dictionary<string, string> DefaultValues
        {
            get
            {
                if (defaultValues.Count == 0)
                {

                    string path = "..\\..\\..\\SligoCS.Web.WI\\StickyParameterDefaults.xml";
                    //If the default values haven't already been loaded, read them from XML.
                    if ((HttpContext.Current != null) && (HttpContext.Current.Server != null))
                    {
                        path = HttpContext.Current.Server.MapPath("~/ParameterDefaults.xml");
                    }

                    if (File.Exists(path))
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(path);
                        if (doc.LastChild.Name == "StickyParameter")
                        {
                            XmlNode stickyParameterNode = doc.LastChild;
                            foreach (XmlNode child in stickyParameterNode.ChildNodes)
                            {
                                if (!defaultValues.ContainsKey(child.Name))
                                {
                                    defaultValues.Add(child.Name, child.InnerText);
                                }
                            }
                        }
                    }
                }
                return defaultValues;
            }
        }
#endregion
        #region public properties
        // jdj: these properties are used in most pages
        public string FULLKEY { get { return fULLKEY; } set { fULLKEY = value; } }
        //public string OrgLevel { get { return oRGLEVEL; } set { oRGLEVEL = value; } }
        public CompareTo CompareTo { get { return compareTo; } set { compareTo = value; } }
        public Group Group { get { return group; } set { group = value; } }
        public string DN { get { return dN; } set { dN = value; } }
        public string SN { get { return sN; } set { sN = value; } }
        public STYP STYP{ get { return sTYP; }set { sTYP = value; }}
        public OrgLevel OrgLevel { get { return oRGLEVEL; } set { oRGLEVEL = value;} }
        //BR: will move this SQL string away later
        public string SQL { get { return sql; } set { sql = value; } }
        public string DETAIL { get { return dETAIL; } set { dETAIL = value; } }
        public GraphFile GraphFile { get { return graphFile; } set { graphFile = value; } }

        public int COUNTY { get { return cOUNTY; } set { cOUNTY = value; } }
        public int Year { get { return year; } set { year = value; } }
        public int TrendStartYear { get { return trendStartYear; } set { trendStartYear = value; } }
        public int ConferenceKey { get { return conferenceKey; } set { conferenceKey = value; } }
        public int DistrictID { get { return districtID; } set { districtID = value; } }

        //The next three properties contain the bit fields for the Grade Breakouts for Normal, LAG, and EDISA.
        // jdj: used only in WSAS (aka EDISA) pages - student performance
        public int GradeBreakout { get { return gradeBreakout; } set { gradeBreakout = value; } }
        public int GradeBreakoutLAG { get { return gradeBreakoutLAG; } set { gradeBreakoutLAG = value; } }
        public int GradeBreakoutEDISA { get { return gradeBreakoutEDISA; } set { gradeBreakoutEDISA = value; } }
        public int HIGHGRADE { get { return highgrade; } set { highgrade = value; } }
            //= int.Parse(InitializeProperty("HIGHGRADE"));
        
        // ACT page
        public ACTSubj ACTSubj { get { return _actSubject; } set { _actSubject = value; } }
        // jdj: used on the money page 
        public Ratio Ratio { get { return _ratio; } set { _ratio = value; } }
        // jdj: used on the HighSchoolCompletion (HSC) page
        //public string CRED { get { return _cred; } set { _cred = value; } }
        // jdj: used on the staff page 
        public StaffRatio StaffRatio { get { return _staffRatio; } set { _staffRatio = value; } }
        // jdj: used in PostGradIntentPage 
        public PostGradPlan PostGradPlan { get { return _plan; } set { _plan = value; } }
        // jdj: used in TeacherQualifications
        public TQShow TQShow { get { return _tqShow; } set { _tqShow = value; } }
        public TQSubjects TQSubjects { get { return _tqSubjectsTaught; } set { _tqSubjectsTaught = value; } }
        // Scatter Plot
        public TQSubjectsSP TQSubjectsSP { get { return _tqSubjectsTaughtSP; } set { _tqSubjectsTaughtSP = value; } }
        public TQTeacherVariable TQTeacherVariable { get { return _tqTeacherSP; } set { _tqTeacherSP = value; } }
        public TQLocation TQLocation { get { return _tqLocation; } set { _tqLocation = value; } }

        // For Selected School/District
        public string NumSchools { get { return numSchools; } set { numSchools = value; } }
        // jdj: used in TeacherQualifications scatterplot
        public TQRelateTo TQRelateTo { get { return _tqRelatedTo; } set { _tqRelatedTo = value; } }
         // jdj: used on the HighSchoolCompletion (HSC) page
        public HighSchoolCompletion HighSchoolCompletion { get { return hSC; } set { hSC = value; } }
        // For Selected School/District
        public string SDistrictFullKeys { get { return sDistrictFullKeys; } set { sDistrictFullKeys = value; } }
        public string SSchoolFullKeys { get { return sSchoolFullKeys; } set { sSchoolFullKeys = value; }}
        public string SelectedFullkeys(OrgLevel level)
        {
           if (level.Key == OrgLevelKeys.School)
                    return SSchoolFullKeys;
           else if (level.Key == OrgLevelKeys.District)
                    return SDistrictFullKeys;
           else
                    return string.Empty;
        }
        public string SCounty { get { return sCounty; } set { sCounty = value; }}
        public string SAthleticConf { get { return sAthleticConf; } set { sAthleticConf = value; }}
        public string SCESA { get { return sCESA; } set { sCESA = value; }}
        public S4orALL S4orALL 
        {
            get { return s4orALL; }
            set { s4orALL = value;  }
        }
        public SRegion SRegion 
        {
            get { return sRegion; }
            set { sRegion = value; }
        }

        // For WSAS scatter plot page
        public int LepPctG4 { get { return lepPctG4; } set { lepPctG4 = value; }}
        public int SdisPctG4 { get { return sdisPctG4; } set { sdisPctG4 = value; }}
        public int EconPctG4 { get { return econPctG4; } set { econPctG4 = value; }}
        public int LepPctG8 { get { return lepPctG8; } set { lepPctG8 = value; }}
        public int SdisPctG8 { get { return sdisPctG8; } set { sdisPctG8 = value; }}
        public int EconPctG8 { get { return econPctG8; } set { econPctG8 = value; }}
        public int LepPctG10 { get { return lepPctG10; } set { lepPctG10 = value; }}
        public int SdisPctG10 { get { return sdisPctG10; } set { sdisPctG10 = value; }}
        public int EconPctG10 { get { return econPctG10; } set { econPctG10 = value; }}

        //jdj: used on the money page - cost per member
        public CT CT { get { return _cost; } set { _cost = value; } }
        public TraceLevel TraceLevels { get { return traceBreakout; } set { traceBreakout = value; } }

        //jdj: used on coursework pages
        public CourseTypeID CourseTypeID { get { return _courseTypeID; } set { _courseTypeID = value; } }
        public WMAS WMAS { get { return _WMASID1; } set { _WMASID1 = value; } }
        //jdj: used on coursework and activities pages
        public int Grade { get { return _grade; } set { _grade = value; } }
        //jdj: used on activities pages
        public Show Show { get { return _show; } set { _show = value; } }

        public WkceWsas WkceWsas { get { return wkceWsas; } set { wkceWsas = value; } }
        
        #endregion

        #region private variables
        private string fULLKEY = InitializeProperty("FULLKEY");//: 013619040022 (unique key identifying initial agency selected when user entered site)  
        private GraphFile graphFile = new GraphFile();//: HIGHSCHOOLCOMPLETION (all pages use graphshell.asp as a wrapper - this globalValues specifies which include file should be used for a given page) 
        private CompareTo compareTo = new CompareTo();//: PRIORYEARS (used to control display text; SQL concatenation; links; and maybe more) 
        protected OrgLevel oRGLEVEL = new OrgLevel();// = (String)InitializeProperty("OrgLevel");//: SC (used to control display text; SQL concatenation; links; and maybe more) );
        private Group group = new Group();//: AllStudentsFAY (used to control display text; SQL concatenation; links; and maybe more) TYPECODE: 3 (session code for schooltype links and maybe other schooltype functionality)     
        private string dN = InitializeProperty("DN");//: Milwaukee (District paramName) 
        private string sN = InitializeProperty("SN");//: Madison Hi (School paramName) 
        private string dETAIL = InitializeProperty("DETAIL");//: YES (used to control whether or not the detail table shows on the page - default is YES - controlled by link on every page)
        private string numSchools = InitializeProperty("NumSchools");//: (used in selected Schools/Districts view) 
        //private string zBackTo = InitializeProperty("ZBACKTO");//: performance.asp (also navigation: referring URL for getting back to the four-part graphical menu) 
        private string sql = InitializeProperty("SQL");

        protected STYP sTYP = new STYP(); // Initialized by GlobalValues.InitShcoolType() //: 9 (URL code for schooltype links and maybe other schooltype functionality) in 
        private int cOUNTY = Convert.ToInt16( InitializeProperty("COUNTY"));//: 40 (County ID - used in URL? used in SQL concatenation; selected schools queries (schools from the same county); and maybe more) 
        private int year = Convert.ToInt16(InitializeProperty("Year"));//: 2007 (indicates current year of data shown on page - used in query and titles) 
        private int trendStartYear = Convert.ToInt16(InitializeProperty("TrendStartYear"));//: 1997 (indicates which year is the first year in Prior Years view - used in query) 
        private int conferenceKey = Convert.ToInt16(InitializeProperty("ConferenceKey"));//: 27 (used in SQL concatenation; selected schools queries (schools from the same Athletic Conference); and maybe more)         
        private int districtID;//: 3619 (not sure where this is used; maybe in queries - may be deprecated) 

        private ACTSubj _actSubject = new ACTSubj();
        private string _cred = string.Empty;
        private Ratio _ratio = new Ratio();
        private CT _cost = new CT();
        private StaffRatio _staffRatio = new StaffRatio();
        private PostGradPlan _plan = new PostGradPlan();
        private TQShow _tqShow = new TQShow();
        private TQSubjects _tqSubjectsTaught = new TQSubjects();
        private TQTeacherVariable _tqTeacherSP = new TQTeacherVariable();
        private TQSubjectsSP _tqSubjectsTaughtSP = new TQSubjectsSP();
        private TQRelateTo _tqRelatedTo = new TQRelateTo();
        private TQLocation _tqLocation = new TQLocation();

        private int gradeBreakout = Convert.ToInt16(InitializeProperty("GradeBreakout"));
        private int gradeBreakoutLAG = Convert.ToInt16(InitializeProperty("GradeBreakoutLAG"));
        private int gradeBreakoutEDISA = Convert.ToInt16(InitializeProperty("GradeBreakoutEDISA"));
        private int highgrade = Convert.ToInt16(InitializeProperty("HIGHGRADE"));

        private HighSchoolCompletion hSC = new HighSchoolCompletion();

        private string sDistrictFullKeys = string.Empty; // aggregated string of 4 full keys: 12345678zzzz2345678zzzz000000000000000000000000
        private string sSchoolFullKeys = string.Empty;   // aggregated string padding 12 0s: 1234567890098723456789999000000000000000000000000
        private string sCounty = string.Empty;   // county ID "13"
        private string sAthleticConf = string.Empty;  // two digits Athletic Conference ID: "03"
        private string sCESA = string.Empty;  // two digits CESA ID: "05"
        private S4orALL s4orALL = new S4orALL();// = int.Parse(InitializeProperty("S4orALL"));  // 1 for 4 schools or districts in a county.Athletic conference or CESA, 2 for all schools or deistricts
        private SRegion sRegion = new SRegion(); // 1 county, 2 for Ath Conf 3 for CESA

        private int lepPctG4 = Convert.ToInt16(InitializeProperty("LepPctG4")); // For WSAS scatter plot page
        private int sdisPctG4 = Convert.ToInt16(InitializeProperty("SdisPctG4")); // For WSAS scatter plot page
        private int econPctG4 = Convert.ToInt16(InitializeProperty("EconPctG4")); // For WSAS scatter plot page
        private int lepPctG8 = Convert.ToInt16(InitializeProperty("LepPctG8")); // For WSAS scatter plot page
        private int sdisPctG8 = Convert.ToInt16(InitializeProperty("SdisPctG8")); // For WSAS scatter plot page
        private int econPctG8 = Convert.ToInt16(InitializeProperty("EconPctG8")); // For WSAS scatter plot page
        private int lepPctG10 = Convert.ToInt16(InitializeProperty("LepPctG10")); // For WSAS scatter plot page
        private int sdisPctG10 = Convert.ToInt16(InitializeProperty("SdisPctG10")); // For WSAS scatter plot page
        private int econPctG10 = Convert.ToInt16(InitializeProperty("EconPctG10")); // For WSAS scatter plot page

        private CourseTypeID _courseTypeID = new CourseTypeID(); // For Coursework pages 
        private WMAS _WMASID1 = new WMAS(); // For Coursework pages 
        private Show _show = new Show(); // For Activities pages 
        private WkceWsas wkceWsas = new WkceWsas(); //StateTestPerformance Page
        private int _grade = Convert.ToInt16(InitializeProperty("Grade"));


        private TraceLevel traceBreakout = (TraceLevel)Enum.Parse(typeof(TraceLevel), InitializeProperty("TraceLevels"));

        #endregion

        #region constants and enums

        public enum QStringVar
        {
            FULLKEY,//: 013619040022 (unique key identifying initial agency selected when user entered site) 
            GraphFile,//: HIGHSCHOOLCOMPLETION (all pages use graphshell.asp as a wrapper - this globalValues specifies which include file should be used for a given page) 
            CompareTo,//: PRIORYEARS (used to control display text, SQL concatenation, links, and maybe more) 
            OrgLevel,//: SC (used to control display text, SQL concatenation, links, and maybe more) 
            LOWGRADE,//:  9
            HIGHGRADE, //: 36
            Group,//: AllStudentsFAY (used to control display text, SQL concatenation, links, and maybe more) TYPECODE: 3 (session code for schooltype links and maybe other schooltype functionality) 
            STYP,//: 9 (URL code for schooltype links and maybe other schooltype functionality) 
            DN,//: Milwaukee (District paramName) 
            SN,//: Madison Hi (School paramName) 
            DETAIL,//: YES (used to control whether or not the detail table shows on the page - default is YES - controlled by link on every page) 
            COUNTY,//: 40 (County ID - used in URL? used in SQL concatenation, selected schools queries (schools from the same county), and maybe more) 
            YearLocal,//: 2007 (indicates current year of data shown on page - used in query and titles) 
            TrendStartYearLocal,//: 1997 (indicates which year is the first year in Prior Years view - used in query) 
            WhichSchool,//: 4 (used in selected Schools/Districts view) 
            NumSchools,//: (used in selected Schools/Districts view) 
            ConferenceKey,//: 27 (used in SQL concatenation, selected schools queries (schools from the same Athletic Conference), and maybe more)         
            DistrictID,//: 3619 (not sure where this is used, maybe in queries - may be deprecated) 
            zBackTo,//: performance.asp (also navigation: referring URL for getting back to the four-part graphical menu) 
            FileQuery,//: (used in the Download Raw Data feature - holds SQL that is executed and exported to the CSV) 
            CT,//: Cost on Money page
            
            // jdj: add coursework and other vars here?
            SDistrictFullKeys,
            SSchoolFullKeys,
            SCounty,
            SAthleticConf,
            SCESA,
            S4orALL,
            SRegion,
            TraceLevel,//: trace level
        }

        // jdj: used on the District Requirements for Graduation page
        public enum CREDVALS
        {
            R, //Required by state
            A  //Additional requirements
        }

        public CREDVALS GetCredVals()
        {
            return GetCredVals(_cred);
        }
        public CREDVALS GetCredVals(string _cred)
        {
            CREDVALS retval = CREDVALS.R;
            if (_cred.ToLower() == "a")
                retval = CREDVALS.A;

            return retval;
        }

        // jdj: used for grade properties on WSAS/EDISA pages only - new bitwise approach
        /// <summary>
        /// Use this enum as a BitMap to display a school's grades & lag grades.
        /// </summary>
        [Flags()]
        public enum GradeLevels
        {
            None = 0,

            HasPreK = 1,
            HasK = 2,//: F
            HasG1 = 4,//: F
            HasG2 = 8,//: F
            HasG3 = 16,//: F
            HasG4 = 32,//: F
            HasG5 = 64,//: F
            HasG6 = 128,//: F
            HasG7 = 256,//: F
            HasG8 = 512,//: F
            HasG9 = 1024,//: T
            HasG10 = 2048,//: T
            HasG11 = 4096,//: T
            HasG12 = 8192,//: T
        }

        // jdj: used for grade properties on non-WSAS/EDISA pages only - low-grade/high-grade range approach
        /// <summary>
        /// Used for backwards compatibility with old site.
        /// </summary>
        public enum OldGradeLevels
        {
            Birth_Through_Age_2 = 2,
            EEN_for_Age_3 = 3,
            EEN_for_Age_4 = 4,
            EEN_for_Age_5 = 5,
            Title_1_Preschool = 6,
            Head_Start = 8,
            TBD = 9,
            Four_Year_Old_Kindergarten = 10,
            Pre_Kindergarten = 12,
            Kinder = 16,
            Grade_1 = 20,
            Grade_2 = 24,
            Grade_3 = 28,
            Grade_4 = 32,
            Grade_5 = 36,
            Grade_6 = 40,
            Grade_7 = 44,
            Grade_8 = 48,
            Grade_9 = 52,
            Grade_10 = 56,
            Grade_11 = 60,
            Grade_12 = 64,
            Elementary_School_Type__Prek_12 = 70,
            Middle_Junior_High_School_Type__Prek_12 = 71,
            High_School_Type__PreK_12 = 72,
            Combined_Elementary_Secondary_School_Type__Prek_12 = 73,
            Elementary_School_Type__K_12 = 74,
            Middle_Junior_High_School_Type__K_12 = 75,
            High_School_Type___K_12 = 76,
            Combined_Elementary_Secondary_School_Type_K_12 = 77,
            Elementary_School_Type_Grades_7_12 = 78,
            Middle_Junior_High_School_Type_Grades_7_12 = 79,
            High_School_Type_Grades_7_12 = 80,
            Comb_Elem_Second_School_Type_Grades_7_12 = 81,
            Elementary_School_Type_Grades_9_12 = 82,
            Middle_Junior_High_School_Type_Grades_9_12 = 83,
            High_School_Type_Grades_9_12 = 84,
            Combined_Elem_Secondary__Grades_9_12 = 85,
            Elementary_School_Type_Grades_10_12 = 86,
            Middle_Junior_High_School_Type_Grades_10_12 = 87,
            High_School_Type_Grades_10_12 = 88,
            Combined_Elem_Secondary_School_Type_Grades_10_12 = 89,
            Elementary_School_Type_Grades_6_12 = 90,
            Middle_Junior_High_School_Type_Grades_6_12 = 91,
            High_School_Type_Grades_6_12 = 92,
            Combined_Elem_Secondary_School_Type_Grades_6_12 = 93,
            Grades_6_12_Combined = 94,
            Grades_7_12_Combined = 95,
            Grades_9_12_Combined = 96,
            Grades_10_12_Combined = 97,
            Grades_K_12 = 98,
            Grades_PreK_12 = 99

        }

        [Flags()]
        // jdj: used for on-page debugging - controls what displays at the top of the page - 0 is default
        public enum TraceLevel
        {
            None = 0,
            CurrentNameValPairs = 1,
            DefaultNameValuePairs = 2,
            SQLStatement = 4
        }

        public enum GradeType
        {
            // jdj: used for grade properties on non-WSAS/EDISA pages only 
            Normal = 1,
            // jdj: used to necessary for WSAS/EDISA pages only - probably not needed anymore
            LAG = 2,
            // jdj: used for grade properties on WSAS/EDISA pages only 
            EDISA = 4
        }

        #endregion

        #region Grade Breakout methods
        
        //BR: will move this out of this entity class 
        public GradeLevels GetGradeBreakout(GradeType gradeType)
        {
            int val = 0;
            // jdj: used for grade properties on non-WSAS/EDISA pages only 
            if (gradeType == GradeType.Normal)
                val = gradeBreakout;
            // jdj: used to necessary for WSAS/EDISA pages only - probably not needed anymore
            else if (gradeType == GradeType.LAG)
                val = gradeBreakoutLAG;
            // jdj: used for grade properties on WSAS/EDISA pages only 
            else if (gradeType == GradeType.EDISA)
                val = gradeBreakoutEDISA;
            GradeLevels retval = GetGradeBreakout(val);

            return retval;
        }
        private GradeLevels GetGradeBreakout(int gradeBreakout)
        {
            GradeLevels retval = GradeLevels.None;
            try
            {
                retval = (GradeLevels)Enum.Parse(typeof(GradeLevels), gradeBreakout.ToString());
            }
            catch
            {
                //do nothing
            }
            return retval;
        }

        /// <summary>
        /// Converts from the old grade level codes to the new grade level codes.
        /// Some pages and selections require support for ranges of grades - thus, some of these return multiple grade values
        /// </summary>
        /// <param name="oldGradeLevel"></param>
        /// <returns></returns>
        public GradeLevels GetGradeBreakout(OldGradeLevels oldGradeLevel)
        {
            GradeLevels retval = GradeLevels.None;

            switch (oldGradeLevel)
            {
                case OldGradeLevels.Birth_Through_Age_2:
                    retval = GradeLevels.HasPreK;
                    break;
                case OldGradeLevels.EEN_for_Age_3:
                    retval = GradeLevels.HasPreK;
                    break;
                case OldGradeLevels.EEN_for_Age_4:
                    retval = GradeLevels.HasPreK;
                    break;
                case OldGradeLevels.EEN_for_Age_5:
                    retval = GradeLevels.HasPreK;
                    break;
                case OldGradeLevels.Title_1_Preschool:
                    retval = GradeLevels.HasPreK;
                    break;
                case OldGradeLevels.Head_Start:
                    retval = GradeLevels.HasPreK;
                    break;
                case OldGradeLevels.TBD:
                    retval = GradeLevels.HasPreK;
                    break;
                case OldGradeLevels.Four_Year_Old_Kindergarten:
                    retval = GradeLevels.HasK;
                    break;
                case OldGradeLevels.Pre_Kindergarten:
                    retval = GradeLevels.HasPreK;
                    break;
                case OldGradeLevels.Kinder:
                    retval = GradeLevels.HasK;
                    break;
                case OldGradeLevels.Grade_1:
                    retval = GradeLevels.HasG1;
                    break;
                case OldGradeLevels.Grade_2:
                    retval = GradeLevels.HasG2;
                    break;
                case OldGradeLevels.Grade_3:
                    retval = GradeLevels.HasG3;
                    break;
                case OldGradeLevels.Grade_4:
                    retval = GradeLevels.HasG4;
                    break;
                case OldGradeLevels.Grade_5:
                    retval = GradeLevels.HasG5;
                    break;
                case OldGradeLevels.Grade_6:
                    retval = GradeLevels.HasG6;
                    break;
                case OldGradeLevels.Grade_7:
                    retval = GradeLevels.HasG7;
                    break;
                case OldGradeLevels.Grade_8:
                    retval = GradeLevels.HasG8;
                    break;
                case OldGradeLevels.Grade_9:
                    retval = GradeLevels.HasG9;
                    break;
                case OldGradeLevels.Grade_10:
                    retval = GradeLevels.HasG10;
                    break;
                case OldGradeLevels.Grade_11:
                    retval = GradeLevels.HasG11;
                    break;
                case OldGradeLevels.Grade_12:
                    retval = GradeLevels.HasG12;
                    break;
                case OldGradeLevels.Elementary_School_Type__Prek_12:
                    retval = GradeLevels.HasPreK | GradeLevels.HasK | GradeLevels.HasG1 | GradeLevels.HasG2
                        | GradeLevels.HasG3 | GradeLevels.HasG4 | GradeLevels.HasG5 | GradeLevels.HasG6
                        | GradeLevels.HasG7 | GradeLevels.HasG8 | GradeLevels.HasG9
                        | GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
                case OldGradeLevels.Middle_Junior_High_School_Type__Prek_12:
                    retval = GradeLevels.HasPreK | GradeLevels.HasK | GradeLevels.HasG1 | GradeLevels.HasG2
                        | GradeLevels.HasG3 | GradeLevels.HasG4 | GradeLevels.HasG5 | GradeLevels.HasG6
                        | GradeLevels.HasG7 | GradeLevels.HasG8 | GradeLevels.HasG9
                        | GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
                case OldGradeLevels.High_School_Type__PreK_12:
                    retval = GradeLevels.HasPreK | GradeLevels.HasK | GradeLevels.HasG1 | GradeLevels.HasG2
                        | GradeLevels.HasG3 | GradeLevels.HasG4 | GradeLevels.HasG5 | GradeLevels.HasG6
                        | GradeLevels.HasG7 | GradeLevels.HasG8 | GradeLevels.HasG9
                        | GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
                case OldGradeLevels.Combined_Elementary_Secondary_School_Type__Prek_12:
                    retval = GradeLevels.HasPreK | GradeLevels.HasK | GradeLevels.HasG1 | GradeLevels.HasG2
                        | GradeLevels.HasG3 | GradeLevels.HasG4 | GradeLevels.HasG5 | GradeLevels.HasG6
                        | GradeLevels.HasG7 | GradeLevels.HasG8 | GradeLevels.HasG9
                        | GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
                case OldGradeLevels.Elementary_School_Type__K_12:
                    retval = GradeLevels.HasK | GradeLevels.HasG1 | GradeLevels.HasG2
                        | GradeLevels.HasG3 | GradeLevels.HasG4 | GradeLevels.HasG5 | GradeLevels.HasG6
                        | GradeLevels.HasG7 | GradeLevels.HasG8 | GradeLevels.HasG9
                        | GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
                case OldGradeLevels.Middle_Junior_High_School_Type__K_12:
                    retval = GradeLevels.HasK | GradeLevels.HasG1 | GradeLevels.HasG2
                        | GradeLevels.HasG3 | GradeLevels.HasG4 | GradeLevels.HasG5 | GradeLevels.HasG6
                        | GradeLevels.HasG7 | GradeLevels.HasG8 | GradeLevels.HasG9
                        | GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
                case OldGradeLevels.High_School_Type___K_12:
                    retval = GradeLevels.HasK | GradeLevels.HasG1 | GradeLevels.HasG2
                        | GradeLevels.HasG3 | GradeLevels.HasG4 | GradeLevels.HasG5 | GradeLevels.HasG6
                        | GradeLevels.HasG7 | GradeLevels.HasG8 | GradeLevels.HasG9
                        | GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
                case OldGradeLevels.Combined_Elementary_Secondary_School_Type_K_12:
                    retval = GradeLevels.HasK | GradeLevels.HasG1 | GradeLevels.HasG2
                        | GradeLevels.HasG3 | GradeLevels.HasG4 | GradeLevels.HasG5 | GradeLevels.HasG6
                        | GradeLevels.HasG7 | GradeLevels.HasG8 | GradeLevels.HasG9
                        | GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
                case OldGradeLevels.Elementary_School_Type_Grades_7_12:
                    retval = GradeLevels.HasG7 | GradeLevels.HasG8 | GradeLevels.HasG9
                        | GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
                case OldGradeLevels.Middle_Junior_High_School_Type_Grades_7_12:
                    retval = GradeLevels.HasG7 | GradeLevels.HasG8 | GradeLevels.HasG9
                        | GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
                case OldGradeLevels.High_School_Type_Grades_7_12:
                    retval = GradeLevels.HasG7 | GradeLevels.HasG8 | GradeLevels.HasG9
                        | GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
                case OldGradeLevels.Comb_Elem_Second_School_Type_Grades_7_12:
                    retval = GradeLevels.HasG7 | GradeLevels.HasG8 | GradeLevels.HasG9
                        | GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
                case OldGradeLevels.Elementary_School_Type_Grades_9_12:
                    retval = GradeLevels.HasG9
                        | GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
                case OldGradeLevels.Middle_Junior_High_School_Type_Grades_9_12:
                    retval = GradeLevels.HasG9
                        | GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
                case OldGradeLevels.High_School_Type_Grades_9_12:
                    retval = GradeLevels.HasG9
                        | GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
                case OldGradeLevels.Combined_Elem_Secondary__Grades_9_12:
                    retval = GradeLevels.HasG9
                        | GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
                case OldGradeLevels.Elementary_School_Type_Grades_10_12:
                    retval = GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
                case OldGradeLevels.Middle_Junior_High_School_Type_Grades_10_12:
                    retval = GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
                case OldGradeLevels.High_School_Type_Grades_10_12:
                    retval = GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
                case OldGradeLevels.Combined_Elem_Secondary_School_Type_Grades_10_12:
                    retval = GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
                case OldGradeLevels.Elementary_School_Type_Grades_6_12:
                    retval = GradeLevels.HasG6
                        | GradeLevels.HasG7 | GradeLevels.HasG8 | GradeLevels.HasG9
                        | GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
                case OldGradeLevels.Middle_Junior_High_School_Type_Grades_6_12:
                    retval = GradeLevels.HasG6
                        | GradeLevels.HasG7 | GradeLevels.HasG8 | GradeLevels.HasG9
                        | GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
                case OldGradeLevels.High_School_Type_Grades_6_12:
                    retval = GradeLevels.HasG6
                        | GradeLevels.HasG7 | GradeLevels.HasG8 | GradeLevels.HasG9
                        | GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
                case OldGradeLevels.Combined_Elem_Secondary_School_Type_Grades_6_12:
                    retval = GradeLevels.HasG6
                        | GradeLevels.HasG7 | GradeLevels.HasG8 | GradeLevels.HasG9
                        | GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
                case OldGradeLevels.Grades_6_12_Combined:
                    retval = GradeLevels.HasG6
                        | GradeLevels.HasG7 | GradeLevels.HasG8 | GradeLevels.HasG9
                        | GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
                case OldGradeLevels.Grades_7_12_Combined:
                    retval = GradeLevels.HasG7 | GradeLevels.HasG8 | GradeLevels.HasG9
                        | GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
                case OldGradeLevels.Grades_9_12_Combined:
                    retval = GradeLevels.HasG9
                        | GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
                case OldGradeLevels.Grades_10_12_Combined:
                    retval = GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
                case OldGradeLevels.Grades_K_12:
                    retval = GradeLevels.HasK | GradeLevels.HasG1 | GradeLevels.HasG2
                        | GradeLevels.HasG3 | GradeLevels.HasG4 | GradeLevels.HasG5 | GradeLevels.HasG6
                        | GradeLevels.HasG7 | GradeLevels.HasG8 | GradeLevels.HasG9
                        | GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
                case OldGradeLevels.Grades_PreK_12:
                    retval = GradeLevels.HasPreK | GradeLevels.HasK | GradeLevels.HasG1 | GradeLevels.HasG2
                        | GradeLevels.HasG3 | GradeLevels.HasG4 | GradeLevels.HasG5 | GradeLevels.HasG6
                        | GradeLevels.HasG7 | GradeLevels.HasG8 | GradeLevels.HasG9
                        | GradeLevels.HasG10 | GradeLevels.HasG11 | GradeLevels.HasG12;
                    break;
            }

            return retval;

        }
        #endregion
    }
}
