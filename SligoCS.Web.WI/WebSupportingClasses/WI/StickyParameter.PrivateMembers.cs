using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
    public partial class StickyParameter
    {
        #region private variables
        private string fULLKEY;//: 013619040022 (unique key identifying initial agency selected when user entered site)  
        private GraphFile graphFile = new GraphFile(); //: the graph-Page/Question user is viewing, used for auto-redirects
        private String qquad;
        private CompareTo compareTo = new CompareTo();//: PRIORYEARS (used to control display text; SQL concatenation; links; and maybe more) 
        protected OrgLevel oRGLEVEL = new OrgLevel();// = (String)InitializeProperty("OrgLevel");//: SC (used to control display text; SQL concatenation; links; and maybe more) );
        private Group group = new Group();//: AllStudentsFAY (used to control display text; SQL concatenation; links; and maybe more) TYPECODE: 3 (session code for schooltype links and maybe other schooltype functionality)     
        private string dN;//: Milwaukee (District paramName) 
        private string sN;//: Madison Hi (School paramName) 
        private string dETAIL;//: YES (used to control whether or not the detail table shows on the page - default is YES - controlled by link on every page)
        private string numSchools;//: (used in selected Schools/Districts view) 
        protected STYP sTYP = new STYP(); // Initialized by GlobalValues.InitShcoolType() //: 9 (URL code for schooltype links and maybe other schooltype functionality) in 
        private int year;//: 2007 (indicates current year of data shown on page - used in query and titles) 
        private int trendStartYear;//: 9999 (indicates which year is the first year in Prior Years view - used in query) 
        private int latestYear;//: 9999 (indicates which year is the last year in Prior Years view - used in query also the default value of Year property) 
        private int conferenceKey;//: 27 (used in SQL concatenation; selected schools queries (schools from the same Athletic Conference); and maybe more)         
        private int districtID;//: 3619 (not sure where this is used; maybe in queries - may be deprecated) 

        private Level _level = new Level();
        private ACTSubj _actSubject = new ACTSubj();
        private string _cred = string.Empty;
        private RevExp revexp = new RevExp();
        private CT _cost = new CT();
        private StaffRatio _staffRatio = new StaffRatio();
        private PostGradPlan _plan = new PostGradPlan();
        private TQShow _tqShow = new TQShow();
        private TQSubjects _tqSubjectsTaught = new TQSubjects();
        private TQTeacherVariable _tqTeacherSP = new TQTeacherVariable();
        private TQSubjectsSP _tqSubjectsTaughtSP = new TQSubjectsSP();
        private TQRelateTo _tqRelatedTo = new TQRelateTo();
        private TQLocation _tqLocation = new TQLocation();

        private Grade grade = new Grade();
        private int gradeBreakout;
        private int gradeBreakoutLAG;
        private int gradeBreakoutEDISA;
        
        private HighSchoolCompletion hSC = new HighSchoolCompletion();
        private TmFrm tmFrm = new TmFrm();

        private string sDistrictFullKeys; // aggregated string of 4 full keys: 12345678zzzz2345678zzzz000000000000000000000000
        private string sSchoolFullKeys;   // aggregated string padding 12 0s: 1234567890098723456789999000000000000000000000000
        private string sCounty;   // county ID "13"
        private string sAthleticConf; // two digits Athletic Conference ID: "03"
        private string sCESA; // two digits CESA ID: "05"
        private S4orALL s4orALL = new S4orALL();// = int.Parse(InitializeProperty("S4orALL"));  // 1 for 4 schools or districts in a county.Athletic conference or CESA, 2 for all schools or deistricts
        private SRegion sRegion = new SRegion(); // 1 county, 2 for Ath Conf 3 for CESA

        private CourseTypeID _courseTypeID = new CourseTypeID(); // For Coursework pages 
        private WMAS _WMASID1 = new WMAS(); // For Coursework pages 
        private Show _show = new Show(); // For Activities pages 
        private WkceWsas wkceWsas = new WkceWsas(); //StateTestPerformance Page
        
        private TraceStateUtils.TraceLevels traceBreakout;

        private GRSbj grsbj = new GRSbj();
        private PrDis prdis = new PrDis();

        private DnlRaw dnlraw = new DnlRaw();
        private Incident incident = new Incident();
        private Weapon weapon = new Weapon();
        private WOW wow = new WOW();
        private SubjectID subjectid = new SubjectID();

        //StateTests Scatterplot
        private Rel rel = new Rel();
        private LF lf = new LF();
        private Group2 group2 = new Group2();

        //Similar Schools
        private SORT sort = new SORT();
        private DISABILITY disable = new DISABILITY();
        private ECON econ = new ECON();
        private LEP lep = new LEP();
        private SIZE size = new SIZE();
        private SPEND spend = new SPEND();
        private NoChce nochce = new NoChce();
        private Sim sim = new Sim();

        #endregion
    }
}
