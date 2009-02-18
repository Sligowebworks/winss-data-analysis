using System;
using System.Collections.Generic;
using System.Text;
using SligoCS.BL.WI;

namespace SligoCS.Web.Base.WebSupportingClasses.WI
{
    [Serializable]
    public class StickyParameter
    {
        #region public properties
        // jdj: these properties are used in most pages
        public string FULLKEY { get { return fULLKEY; } set { fULLKEY = value; } }
        public string ORGLEVEL { get { return oRGLEVEL; } set { oRGLEVEL = value; } }
        public string CompareTo { get { return compareTo; } set { compareTo = value; } }
        public string Group { get { return group; } set { group = value; } }
        public string DN { get { return dN; } set { dN = value; } }
        public string SN { get { return sN; } set { sN = value; } }
        //BR: will move this SQL string away later
        public string SQL { get { return sql; } set { sql = value; } }
        public string DETAIL { get { return dETAIL; } set { dETAIL = value; } }
        public string GraphFile { get { return graphFile; } set { graphFile = value; } }

        public int STYP { get { return sTYP; } set { sTYP = value; } }
        public int COUNTY { get { return cOUNTY; } set { cOUNTY = value; } }
        public int YearLocal { get { return yearLocal; } set { yearLocal = value; } }
        public int TrendStartYearLocal { get { return trendStartYearLocal; } set { trendStartYearLocal = value; } }
        public int ConferenceKey { get { return conferenceKey; } set { conferenceKey = value; } }
        public int DistrictID { get { return districtID; } set { districtID = value; } }

        public string LOWGRADE { get { return lOWGRADE; } set { lOWGRADE = value; } }
        public string HIGHGRADE { get { return hIGHGRADE; } set { hIGHGRADE = value; } }
        //The next three properties contain the bit fields for the Grade Breakouts for Normal, LAG, and EDISA.
        // jdj: used only in WSAS (aka EDISA) pages - student performance
        public int GradeBreakout { get { return gradeBreakout; } set { gradeBreakout = value; } }
        public int GradeBreakoutLAG { get { return gradeBreakoutLAG; } set { gradeBreakoutLAG = value; } }
        public int GradeBreakoutEDISA { get { return gradeBreakoutEDISA; } set { gradeBreakoutEDISA = value; } }
        
        // promoted from previous derivate classes
        public string SubjectID { get { return _subjectID; } set { _subjectID = value; } }
        // jdj: used on the money page 
        public string RATIO { get { return _ratio; }  set { _ratio = value; } }
        // jdj: used on the HighSchoolCompletion (HSC) page
        public string CRED { get { return _cred; } set { _cred = value; } }
        // jdj: used on the staff page 
        public string STAFFRATIO { get { return _staffRatio; } set { _staffRatio = value; } }
        // jdj: used in PostGradIntentPage 
        public string PLAN { get { return _plan; } set { _plan = value; } }
        // jdj: used in TeacherQualifications
        public string TQShow { get { return _tqShow; } set { _tqShow = value; } }
        public string TQSubjects { get { return _tqSubjectsTaught; } set { _tqSubjectsTaught = value; } }
        public string TQTeacherVariable { get { return _tqTeacherVariable; } set { _tqTeacherVariable = value; } }
        public string TQRelatedTo { get { return _tqRelatedTo; } set { _tqRelatedTo = value; } }
        public string TQLocation { get { return _tqLocation; } set { _tqLocation = value; } }

        public string ZBackTo { get { return zBackTo; } set { zBackTo = value; } }
        
        // For Selected School/District
        public string NumSchools { get { return numSchools; } set { numSchools = value; } }
        // jdj: used in TeacherQualifications scatterplot
        public string RelateToTQS { get { return relateToTQS; } set { relateToTQS = value; } }
        // jdj: used on the Primary Disabilities page
        public string DISABILITY { get { return dISABILITY; } set { dISABILITY = value; }}
        // jdj: used on the HighSchoolCompletion (HSC) page
        public string HSC { get { return hSC; } set { hSC = value; } }
        // For Selected School/District
        public string SDistrictFullKeys { get { return sDistrictFullKeys; } set { sDistrictFullKeys = value; } }
        public string SSchoolFullKeys { get { return sSchoolFullKeys; } set { sSchoolFullKeys = value; }}
        public string SelectedFullkeys(OrgLevel level)
        {
            switch (level)
            {
                case OrgLevel.School:
                    return SSchoolFullKeys;
                case OrgLevel.District:
                    return SDistrictFullKeys;
                default:
                    return string.Empty;
            }
            
        }
        public string SCounty { get { return sCounty; } set { sCounty = value; }}
        public string SAthleticConf { get { return sAthleticConf; } set { sAthleticConf = value; }}
        public string SCESA { get { return sCESA; } set { sCESA = value; }}
        public int S4orALL { get { return s4orALL; } set { s4orALL = value; } }
        public int SRegion { get { return sRegion; } set { sRegion = value; } }

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
        public string CT { get { return _cost; } set { _cost = value; } }
        public TraceLevel TraceLevels { get { return traceBreakout; } set { traceBreakout = value; } }

        //jdj: used on coursework pages
        public int CourseTypeID { get { return _courseTypeID; } set { _courseTypeID = value; } }
        public int WMASID1 { get { return _WMASID1; } set { _WMASID1 = value; } }
        //jdj: used on coursework and activities pages
        public int Grade { get { return _grade; } set { _grade = value; } }
        //jdj: used on activities pages
        public string SHOW { get { return _show; } set { _show = value; } }
        
        #endregion

        #region private variables
        private string fULLKEY;//: 013619040022 (unique key identifying initial agency selected when user entered site) 
        private string graphFile;//: HIGHSCHOOLCOMPLETION (all pages use graphshell.asp as a wrapper - this stickyParameter specifies which include file should be used for a given page) 
        private string compareTo;//: PRIORYEARS (used to control display text; SQL concatenation; links; and maybe more) 
        private string oRGLEVEL;//: SC (used to control display text; SQL concatenation; links; and maybe more) 
        private string lOWGRADE;  //9
        private string hIGHGRADE; //36
        private string group;//: AllStudentsFAY (used to control display text; SQL concatenation; links; and maybe more) TYPECODE: 3 (session code for schooltype links and maybe other schooltype functionality)     
        private string dN;//: Milwaukee (District Name) 
        private string sN;//: Madison Hi (School Name) 
        private string dETAIL;//: YES (used to control whether or not the detail table shows on the page - default is YES - controlled by link on every page) 
        private string numSchools;//: (used in selected Schools/Districts view) 
        private string zBackTo;//: performance.asp (also navigation: referring URL for getting back to the four-part graphical menu) 
        private string sql;

        private int sTYP;//: 9 (URL code for schooltype links and maybe other schooltype functionality) 
        private int cOUNTY;//: 40 (County ID - used in URL? used in SQL concatenation; selected schools queries (schools from the same county); and maybe more) 
        private int yearLocal;//: 2007 (indicates current year of data shown on page - used in query and titles) 
        private int trendStartYearLocal;//: 1997 (indicates which year is the first year in Prior Years view - used in query) 
        private int conferenceKey;//: 27 (used in SQL concatenation; selected schools queries (schools from the same Athletic Conference); and maybe more)         
        private int districtID;//: 3619 (not sure where this is used; maybe in queries - may be deprecated) 

        private string _subjectID = string.Empty;
        private string _cred = string.Empty;
        private string _ratio = string.Empty;
        private string _cost = string.Empty;
        private string _staffRatio = string.Empty;
        private string _plan;
        private string _tqShow = string.Empty;
        private string _tqSubjectsTaught = string.Empty;
        private string _tqTeacherVariable = string.Empty;
        private string _tqRelatedTo = string.Empty;
        private string _tqLocation= string.Empty;

        private int gradeBreakout;
        private int gradeBreakoutLAG;
        private int gradeBreakoutEDISA;

        private string relateToTQS = string.Empty;
        private string dISABILITY = string.Empty;
        private string hSC = string.Empty;

        private string sDistrictFullKeys = string.Empty; // aggregated string of 4 full keys: 12345678zzzz2345678zzzz000000000000000000000000
        private string sSchoolFullKeys = string.Empty;   // aggregated string padding 12 0s: 1234567890098723456789999000000000000000000000000
        private string sCounty = string.Empty;   // county ID "13"
        private string sAthleticConf = string.Empty;  // two digits Athletic Conference ID: "03"
        private string sCESA = string.Empty;  // two digits CESA ID: "05"
        private int s4orALL;  // 1 for 4 schools or districts in a county.Athletic conference or CESA, 2 for all schools or deistricts
        private int sRegion;  // 1 county, 2 for Ath Conf 3 for CESA

        private int lepPctG4; // For WSAS scatter plot page
        private int sdisPctG4; // For WSAS scatter plot page
        private int econPctG4; // For WSAS scatter plot page
        private int lepPctG8; // For WSAS scatter plot page
        private int sdisPctG8; // For WSAS scatter plot page
        private int econPctG8; // For WSAS scatter plot page
        private int lepPctG10; // For WSAS scatter plot page
        private int sdisPctG10; // For WSAS scatter plot page
        private int econPctG10; // For WSAS scatter plot page

        private int _courseTypeID; // For Coursework pages 
        private int _WMASID1; // For Coursework pages 
        private int _grade; // For Coursework and Activities pages 
        private string _show; // For Activities pages 
        
        private TraceLevel traceBreakout;
        #endregion

        #region constants and enums

        public enum QStringVar
        {
            FULLKEY,//: 013619040022 (unique key identifying initial agency selected when user entered site) 
            GraphFile,//: HIGHSCHOOLCOMPLETION (all pages use graphshell.asp as a wrapper - this stickyParameter specifies which include file should be used for a given page) 
            CompareTo,//: PRIORYEARS (used to control display text, SQL concatenation, links, and maybe more) 
            ORGLEVEL,//: SC (used to control display text, SQL concatenation, links, and maybe more) 
            LOWGRADE,//:  9
            HIGHGRADE, //: 36
            Group,//: AllStudentsFAY (used to control display text, SQL concatenation, links, and maybe more) TYPECODE: 3 (session code for schooltype links and maybe other schooltype functionality) 
            STYP,//: 9 (URL code for schooltype links and maybe other schooltype functionality) 
            DN,//: Milwaukee (District Name) 
            SN,//: Madison Hi (School Name) 
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

        public enum ACTSubjects
        {
            Reading,
            English,
            Math,
            Science,
            Composite
        }
        public ACTSubjects GetACTSubject(string subjectID)
        {
            ACTSubjects subject = ACTSubjects.Reading;
            switch (subjectID.ToLower())
            {
                case "1re":
                    subject = ACTSubjects.Reading;
                    break;

                case "2la":
                    subject = ACTSubjects.English;
                    break;

                case "3ma":
                    subject = ACTSubjects.Math;
                    break;

                case "4sc":
                    subject = ACTSubjects.Science;
                    break;

                case "0as":
                    subject = ACTSubjects.Composite;
                    break;
            }

            return subject;
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

        // jdj: Used for post grad intentions page
        public enum PostGradPlans
        {
            All,
            FourYear,
            VocTecCollege,
            Employment,
            Military,
            JobTraining,
            SeekEmployment,
            Other,
            Undecided,
            NoResponse
        }
        // jdj: Used for post grad intentions page
        public PostGradPlans GetPostGradPlan(string plan)
        {
            PostGradPlans retval = PostGradPlans.All;
            if (plan != null)
            {
                switch (plan.ToLower())
                {
                    case "all":
                        retval = PostGradPlans.All;
                        break;

                    case "4-yr":
                        retval = PostGradPlans.FourYear;
                        break;

                    case "voc":
                        retval = PostGradPlans.VocTecCollege;
                        break;

                    case "emp":
                        retval = PostGradPlans.Employment;
                        break;

                    case "military":
                        retval = PostGradPlans.Military;
                        break;

                    case "job":
                        retval = PostGradPlans.JobTraining;
                        break;

                    case "seekemp":
                        retval = PostGradPlans.SeekEmployment;
                        break;

                    case "other":
                        retval = PostGradPlans.Other;
                        break;

                    case "undecided":
                        retval = PostGradPlans.Undecided;
                        break;

                    case "no-resp":
                        retval = PostGradPlans.NoResponse;
                        break;

                }
            }
            return retval;
        }

        // jdj: Used for Teacher Qualifications page
        public enum TQShowEnum
        {
            Wisconsin_License_Status,
            District_Experience,
            Total_Experience,
            Highest_Degree,
            ESEA_Qualified,
        }


        // jdj: Used for Teacher Qualifications page
        public enum TQSubjectsTaughtEnum
        {
            English_Language_Arts,
            Mathematics,
            Science,
            Social_Studies,
            Foreign_Languages,
            The_Arts_Art_And_Design_Dance_Music_Theatre,
            Elementary_All_Subjects,
            Special_Education_Core_Subjects,
            Core_Subjects_Summary,
            Special_Education_Summary,
            Summary_All_Subjects

        }

        // jdj: Used for Teacher Qualifications page
        public enum TQTeacherVariableEnum
        {
            Percent_Full_Wisconsin_License,
            Percent_Emergency_Wisconsin_License,
            Percent_No_License_For_Assignment,
            Percent_5_or_More_Years_District_Experience,
            Percent_5_or_More_Years_Total_Experience,
            Percent_Masters_or_Higher_Degree,
            Percent_ESEA_Qualified,
        }

        // jdj: Used for Teacher Qualifications page
        public TQTeacherVariableEnum GetTQTeacherVariable()
        {
            TQTeacherVariableEnum retval = 0;
            switch (TQTeacherVariable.ToUpper())
            {

                case "PFWL":
                    retval = TQTeacherVariableEnum.Percent_Full_Wisconsin_License;
                    break;
                case "PEWL":
                    retval = TQTeacherVariableEnum.Percent_Emergency_Wisconsin_License;
                    break;
                case "PNLFA":
                    retval = TQTeacherVariableEnum.Percent_No_License_For_Assignment;
                    break;
                case "P5MYDE":
                    retval = TQTeacherVariableEnum.Percent_5_or_More_Years_District_Experience;
                    break;
                case "P5MYTE":
                    retval = TQTeacherVariableEnum.Percent_5_or_More_Years_Total_Experience;
                    break;
                case "PMHD":
                    retval = TQTeacherVariableEnum.Percent_Masters_or_Higher_Degree;
                    break;
                case "PEQ":
                    retval = TQTeacherVariableEnum.Percent_ESEA_Qualified;
                    break;
            }

            return retval;

        }


        // jdj: Used for Teacher Qualifications page
        public enum TQRelatedToEnum
        {
            District_Spending,
            District_Size,
            School_Size,
            Percent_Economically_Disadvantaged,
            Percent_Limited_English_Proficient,
            Percent_Students_with_Disabilities,
            Percent_Am_Indian,
            Percent_Asian,
            Percent_Black,
            Percent_Hispanic,
            Percent_White,
        }

        // jdj: Used for Teacher Qualifications page
        public TQRelatedToEnum GetTQRelatedTo()
        {
            TQRelatedToEnum retval = 0;
            switch (TQRelatedTo.ToUpper())
            {

                case "SPENDING":
                    retval = TQRelatedToEnum.District_Spending;
                    break;
                case "DISTSIZE":
                    retval = TQRelatedToEnum.District_Size;
                    break;
                case "SCHOOLSIZE":
                    retval = TQRelatedToEnum.School_Size;
                    break;
                case "ECONOMICSTATUS":
                    retval = TQRelatedToEnum.Percent_Economically_Disadvantaged;
                    break;
                case "ENGLISHPROFICIENCY":
                    retval = TQRelatedToEnum.Percent_Limited_English_Proficient;
                    break;
                case "DISABILITY":
                    retval = TQRelatedToEnum.Percent_Students_with_Disabilities;
                    break;
                case "NATIVE":
                    retval = TQRelatedToEnum.Percent_Am_Indian;
                    break;
                case "ASIAN":
                    retval = TQRelatedToEnum.Percent_Asian;
                    break;
                case "BLACK":
                    retval = TQRelatedToEnum.Percent_Black;
                    break;
                case "HISPANIC":
                    retval = TQRelatedToEnum.Percent_Hispanic;
                    break;
                case "WHITE":
                    retval = TQRelatedToEnum.Percent_White;
                    break;
            }

            return retval;

        }

        // jdj: Used for Teacher Qualifications page
        public enum TQLocationEnum
        {
            Entire_State,
            My_CESA, 
            My_County,
        }

        // jdj: Used for Teacher Qualifications page
        public TQLocationEnum GetTQLocation()
        {
            TQLocationEnum retval = 0;
            switch (TQLocation.ToUpper())
            {

                case "STATE":
                    retval = TQLocationEnum.Entire_State;
                    break;

                case "CESA":
                    retval = TQLocationEnum.My_CESA;
                    break;

                case "COUNTY":
                    retval = TQLocationEnum.My_County;
                    break;

            }

            return retval;

        }


        
        // jdj: Used for Teacher Qualifications page
        public TQSubjectsTaughtEnum GetTQSubjectsTaught()
        {
            TQSubjectsTaughtEnum retval = 0;
            switch (TQSubjects.ToUpper())
            {
                case "ELA":
                    retval = TQSubjectsTaughtEnum.English_Language_Arts;
                    break;

                case "MATH":
                    retval = TQSubjectsTaughtEnum.Mathematics;
                    break;

                case "SCI":
                    retval = TQSubjectsTaughtEnum.Science;
                    break;

                case "SOC":
                    retval = TQSubjectsTaughtEnum.Social_Studies;
                    break;

                case "FLANG":
                    retval = TQSubjectsTaughtEnum.Foreign_Languages;
                    break;

                case "ARTS":
                    retval = TQSubjectsTaughtEnum.The_Arts_Art_And_Design_Dance_Music_Theatre;
                    break;

                case "ELSUBJ":
                    retval = TQSubjectsTaughtEnum.Elementary_All_Subjects;
                    break;

                case "SPCORE":
                    retval = TQSubjectsTaughtEnum.Special_Education_Core_Subjects;
                    break;

                case "CORESUM":
                    retval = TQSubjectsTaughtEnum.Core_Subjects_Summary;
                    break;

                case "SPSUM":
                    retval = TQSubjectsTaughtEnum.Special_Education_Summary;
                    break;

                case "SUMALL":
                    retval = TQSubjectsTaughtEnum.Summary_All_Subjects;
                    break;

            }

            return retval;
        }

        // jdj: Used for Teacher Qualifications page
        public TQShowEnum GetTQShow()
        {
            TQShowEnum retval = 0;
            switch (TQShow.ToUpper())
            {

                case "LICSTAT":
                    retval = TQShowEnum.Wisconsin_License_Status;
                    break;
                case "DISTEXP":
                    retval = TQShowEnum.District_Experience;
                    break;
                case "TOTEXP":
                    retval = TQShowEnum.Total_Experience;
                    break;
                case "DEGR":
                    retval = TQShowEnum.Highest_Degree;
                    break;
                case "ESEAHIQ":
                    retval = TQShowEnum.ESEA_Qualified;
                    break;
            }

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

        // jdj: used for High School Completion page only 
        public static readonly string HSC_ALL = "All";
        public static readonly string HSC_CERT = "Cert";
        public static readonly string HSC_HSED = "HSED";
        public static readonly string HSC_REG = "Reg";
        public static readonly string HSC_COMB = "Comb";

        // jdj: used for money page only 
        public static readonly string RATIO_REVENUE = "REVENUE";
        public static readonly string RATIO_EXPENDITURE = "EXPENDITURE";
        public static readonly string COST_TOTAL_COST = "TC";
        public static readonly string COST_TOTAL_EDUCATION_COST = "TE";
        public static readonly string COST_CURRENT_EDUCATION_COST = "CE";

        public static readonly string RATIO_STUDENT_TO_STAFF = "STUDENTSTAFF";
        public static readonly string RATIO_STAFF_TO_STUDENT = "STAFFSTUDENT";

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
