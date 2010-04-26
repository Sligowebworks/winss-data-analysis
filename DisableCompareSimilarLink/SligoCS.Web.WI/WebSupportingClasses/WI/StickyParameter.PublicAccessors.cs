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
        #region public properties
        // jdj: these properties are used in most pages
        public string FULLKEY { get { return fULLKEY; } set { fULLKEY = value; } }
        /// <summary>
        /// Question Quadrant Filename
        /// </summary>
        public String Qquad { get { return qquad; } set { qquad = value; } }
        public CompareTo CompareTo { get { return compareTo; } set { compareTo = value; } }
        public Group Group { get { return group; } set { group = value; } }
        public string DN { get { return dN; } set { dN = value; } }
        public string SN { get { return sN; } set { sN = value; } }
        public STYP STYP { get { return sTYP; } set { sTYP = value; } }
        public OrgLevel OrgLevel { get { return oRGLEVEL; } set { oRGLEVEL = value; } }
        public string DETAIL { get { return dETAIL; } set { dETAIL = value; } }
        public GraphFile GraphFile { get { return graphFile; } set { graphFile = value; } }

        public int COUNTY { get { return cOUNTY; } set { cOUNTY = value; } }
        public int Year { get { return year; } set { year = value; } }
        public int TrendStartYear { get { return trendStartYear; } set { trendStartYear = value; } }
        public int ConferenceKey { get { return conferenceKey; } set { conferenceKey = value; } }
        public int DistrictID { get { return districtID; } set { districtID = value; } }

        public Grade Grade { get { return grade; } set { grade = value; } }
        //The next three properties contain the bit fields for the Grade Breakouts for Normal, LAG, and EDISA.
        // jdj: used only in WSAS (aka EDISA) pages - student performance
        public int GradeBreakout { get { return gradeBreakout; } set { gradeBreakout = value; } }
        public int GradeBreakoutLAG { get { return gradeBreakoutLAG; } set { gradeBreakoutLAG = value; } }
        public int GradeBreakoutEDISA { get { return gradeBreakoutEDISA; } set { gradeBreakoutEDISA = value; } }
        
        //WRCT Page
        public Level Level { get { return _level; } set { _level = value; } }
        // ACT page
        public ACTSubj ACTSubj { get { return _actSubject; } set { _actSubject = value; } }
        // jdj: used on the money page 
        public RevExp RevExp { get { return revexp; } set { revexp = value; } }
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
        public string SSchoolFullKeys { get { return sSchoolFullKeys; } set { sSchoolFullKeys = value; } }
        public string SCounty { get { return sCounty; } set { sCounty = value; } }
        public string SAthleticConf { get { return sAthleticConf; } set { sAthleticConf = value; } }
        public string SCESA { get { return sCESA; } set { sCESA = value; } }
        public S4orALL S4orALL
        {
            get { return s4orALL; }
            set { s4orALL = value; }
        }
        public SRegion SRegion
        {
            get { return sRegion; }
            set { sRegion = value; }
        }

        // For WSAS scatter plot page
        public int LepPctG4 { get { return lepPctG4; } set { lepPctG4 = value; } }
        public int SdisPctG4 { get { return sdisPctG4; } set { sdisPctG4 = value; } }
        public int EconPctG4 { get { return econPctG4; } set { econPctG4 = value; } }
        public int LepPctG8 { get { return lepPctG8; } set { lepPctG8 = value; } }
        public int SdisPctG8 { get { return sdisPctG8; } set { sdisPctG8 = value; } }
        public int EconPctG8 { get { return econPctG8; } set { econPctG8 = value; } }
        public int LepPctG10 { get { return lepPctG10; } set { lepPctG10 = value; } }
        public int SdisPctG10 { get { return sdisPctG10; } set { sdisPctG10 = value; } }
        public int EconPctG10 { get { return econPctG10; } set { econPctG10 = value; } }

        //jdj: used on the money page - cost per member
        public CT CT { get { return _cost; } set { _cost = value; } }
        public TraceStateUtils.TraceLevel TraceLevels { get { return traceBreakout; } set { traceBreakout = value; } }

        //jdj: used on coursework pages
        public CourseTypeID CourseTypeID { get { return _courseTypeID; } set { _courseTypeID = value; } }
        public WMAS WMAS { get { return _WMASID1; } set { _WMASID1 = value; } }
        //jdj: used on activities pages
        /// <summary>
        /// Activities Page, Extra-Co and Community
        /// </summary>
        public Show Show { get { return _show; } set { _show = value; } }

        public WkceWsas WkceWsas { get { return wkceWsas; } set { wkceWsas = value; } }

        /// <summary>
        /// GradReqs Page, Show links
        /// </summary>
        public GRSbj GRSbj { get { return grsbj; } set { grsbj = value; } }
        /// <summary>
        /// Enrollment Primary Disabilities 
        /// </summary>
        public PrDis PrDis { get { return prdis; } set { prdis = value; } }

        public DnlRaw DnlRaw { get { return dnlraw; } set { dnlraw = value; } }
        public Incident Incident { get { return incident; } set { incident = value; } }
        public Weapon Weapon { get { return weapon; } set { weapon = value; } }
        public WOW WOW { get { return wow; } set { wow = value; } }
        public SubjectID SubjectID { get { return subjectid; } set { subjectid = value; } }

        //StateTests Scatterplot
        public Rel Rel { get { return rel; } set { rel = value; } }
        public LF LF { get { return lf; } set { lf = value; } }
        public Group2 Group2 { get { return group2; } set { group2 = value; } }

        public SORT SORT { get { return sort; } set { sort = value; } }
        public DISABILITY DISABILITY { get { return disable; } set { disable = value; } }
        public ECON ECON { get { return econ; } set { econ = value; } }
        public LEP LEP { get { return lep; } set { lep = value; } }
        public SIZE SIZE { get { return size; } set { size = value; } }
        public SPEND SPEND { get { return spend; } set { spend = value; } }
        public NoChce NoChce { get { return nochce; } set { nochce = value; } }

        #endregion
    }
}
