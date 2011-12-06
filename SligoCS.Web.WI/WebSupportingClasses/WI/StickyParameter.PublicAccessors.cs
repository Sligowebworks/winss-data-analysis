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
        public String FULLKEY { get { if (fULLKEY == null) fULLKEY = InitializeProperty("FULLKEY"); return fULLKEY; } set { fULLKEY = value; } }
        /// <summary>
        /// Question Quadrant Filename
        /// </summary>
        public String Qquad { get { if (qquad == null) qquad = InitializeProperty("Qquad"); return qquad; } set { qquad = value; } }
        public CompareTo CompareTo { get { return compareTo; } set { compareTo = value; } }
        public Group Group { get { return group; } set { group = value; } }
        public string DN { get { if (dN == null) dN = InitializeProperty("DN"); return dN; } set { dN = value; } }
        public string SN { get { if (sN == null) sN = InitializeProperty("SN");  return sN; } set { sN = value; } }
        public STYP STYP { get { return sTYP; } set { sTYP = value; } }
        public OrgLevel OrgLevel { get { return oRGLEVEL; } set { oRGLEVEL = value; } }
        public string DETAIL { get { if (dETAIL == null) dETAIL = InitializeProperty("DETAIL"); return dETAIL; } set { dETAIL = value; } }
        public GraphFile GraphFile { get { return graphFile; } set { graphFile = value; } }

        /// <summary>
        /// Read-only. To set a specific year, first set ForceCurrentYear = true and use CurrentYear property.
        /// Value depends on CompareTo selection and range of TrendStartYear:CurrentYear.
        /// </summary>
        public int Year 
        {  
            get 
            {
                year = Convert.ToInt16(GetParamFromUser("Year"));

                if ((CompareTo.Key == CompareToKeys.Years
                    || !(TrendStartYear <= year && year <= CurrentYear)
                    || !inQS.Contains("Year"))
                    && CurrentYear != int.Parse(QueryStringUtils.GetParamDefault("CurrentYear").ToString())
                    ) 
                        year = CurrentYear;

                    if (ForceCurrentYear) year = CurrentYear;

                return year; 
            }
        }
        public int TrendStartYear { get { if (trendStartYear == 0)trendStartYear = Convert.ToInt16(InitializeProperty("TrendStartYear"));  return trendStartYear; } set { trendStartYear = value; } }
        public int CurrentYear { get { if (latestYear == 0) latestYear = Convert.ToInt16(InitializeProperty("LatestYear")); return latestYear; } set { latestYear = value; } }
        public Boolean ForceCurrentYear = false;
        public int ConferenceKey { get { if (conferenceKey == 0) conferenceKey = Convert.ToInt16(InitializeProperty("ConferenceKey")); return conferenceKey; } set { conferenceKey = value; } }
        public int DistrictID { get { return districtID; } set { districtID = value; } }

        public Grade Grade { get { return grade; } set { grade = value; } }
        //The next three properties contain the bit fields for the Grade Breakouts for Normal, LAG, and EDISA.
        // jdj: used only in WSAS (aka EDISA) pages - student performance
        public int GradeBreakout { get { if (gradeBreakout == 0) gradeBreakout = Convert.ToInt16(InitializeProperty("GradeBreakout")); return gradeBreakout; } set { gradeBreakout = value; } }
        public int GradeBreakoutLAG { get {if (gradeBreakoutLAG == 0) gradeBreakoutLAG = Convert.ToInt16(InitializeProperty("GradeBreakoutLAG")); return gradeBreakoutLAG; } set { gradeBreakoutLAG = value; } }
        public int GradeBreakoutEDISA { get { if (gradeBreakoutEDISA == 0) gradeBreakoutEDISA = Convert.ToInt16(InitializeProperty("GradeBreakoutEDISA")); return gradeBreakoutEDISA; } set { gradeBreakoutEDISA = value; } }
        
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
        public string NumSchools { get { if (numSchools == null) numSchools = InitializeProperty("NumSchools");  return numSchools; } set { numSchools = value; } }
        // jdj: used in TeacherQualifications scatterplot
        public TQRelateTo TQRelateTo { get { return _tqRelatedTo; } set { _tqRelatedTo = value; } }
        // jdj: used on the HighSchoolCompletion (HSC) page
        public HighSchoolCompletion HighSchoolCompletion { get { return hSC; } set { hSC = value; } }
        public TmFrm TmFrm { get { return tmFrm; } set { tmFrm = value; } }
        // For Selected School/District
        public string SDistrictFullKeys { get { if (sDistrictFullKeys == null) sDistrictFullKeys = InitializeProperty("SDistrictFullKeys");  return sDistrictFullKeys; } set { sDistrictFullKeys = value; } }
        public string SSchoolFullKeys { get { if (sSchoolFullKeys == null) sSchoolFullKeys = InitializeProperty("SSchoolFullKeys");  return sSchoolFullKeys; } set { sSchoolFullKeys = value; } }
        public string SCounty { get { if (sCounty == null) sCounty = InitializeProperty("SCounty");  return sCounty; } set { sCounty = value; } }
        public string SAthleticConf { get { if (sAthleticConf == null) sAthleticConf = InitializeProperty("SAthleticConf");  return sAthleticConf; } set { sAthleticConf = value; } }
        public string SCESA { get { if (sCESA == null) sCESA = InitializeProperty("SCESA");  return sCESA; } set { sCESA = value; } }
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

        //jdj: used on the money page - cost per member
        public CT CT { get { return _cost; } set { _cost = value; } }
        public TraceStateUtils.TraceLevels TraceLevels { get { if (traceBreakout == 0) traceBreakout = (TraceStateUtils.TraceLevels)Enum.Parse(typeof(TraceStateUtils.TraceLevels), InitializeProperty("TraceLevels"));  return traceBreakout; } set { traceBreakout = value; } }

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
        public Sim Sim { get { return sim; } set { sim = value; } }

        #endregion
    }
}
