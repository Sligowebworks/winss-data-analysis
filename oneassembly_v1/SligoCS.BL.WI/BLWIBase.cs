using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SligoCS.DAL.WI;

namespace SligoCS.BL.WI
{
    /// <summary>
    /// This will be the base class for all Wisconsin BLs, not entities.
    /// </summary>
    public abstract class BLWIBase 
    {
        #region class level variables
        protected string sql = string.Empty;
        protected int _trendStartYear = 1997;  //default for Retention.  Override in derived classes as necessary.
        protected int _currentYear = 2007;     //Note: CurrentYear = 2008; default for FLANDERS.  Override in derived classes as necessary.
        protected int _gradeCode = 98;          //default for Retention.  Override in derived classes as necessary.
        protected List<string> _fullKeys; //contains OrigFullKey + several potential other fullkeys and/or their masks.
        protected string _clauseForCompareToSchoolsDistrict; //contains OrigFullKey + several potential other fullkeys and/or their masks.
        
        private string _origFullKey;
        private List<int> _years;
        private SchoolType _schoolType;
        private CompareTo _compareTo;
        private List<string> _compareToSchools;
        private S4orALL _s4orALL;
        private ViewByGroup _viewBy;
        private OrgLevel _orgLevel;
        private SRegion _sRegion;

        private string _sCounty = string.Empty;
        private string _sAthleticConf = string.Empty;
        private string _sCESA = string.Empty;

#endregion

        #region public properties
        /// <summary>
        /// Typically, SQL is considered private to the DAL.  However, one of the Wisconsin
        /// development specs was to allow for trace information, including the SQL statement, 
        /// to appear on the page under certain circumstances.  To protect the privacy of the SQL, 
        /// and to ensure encapsulation, the public SQL property is read-only.
        /// </summary>
        public string SQL { get { return sql; } }

        public string OrigFullKey { get { return _origFullKey; } set { _origFullKey = value; } }
        public SchoolType SchoolType { get { return _schoolType; } set { _schoolType = value; } }
        public CompareTo CompareTo { get { return _compareTo; } set { _compareTo = value; } }
        public List<string> CompareToSchoolsOrDistrict 
            { get { return _compareToSchools; } set { _compareToSchools = value; } }
        public S4orALL S4orALL { get { return _s4orALL; } set { _s4orALL = value; } }
        public SRegion SRegion { get { return _sRegion; } set { _sRegion = value; } }

        public int TrendStartYear { get { return _trendStartYear; } }
        public int CurrentYear { get { return _currentYear; } }
        public List<int> Years { get { return _years; } set { _years = value; } }
        public ViewByGroup ViewBy { get { return _viewBy; } set { _viewBy = value; } }
        public OrgLevel OrgLevel { get { return _orgLevel; } set { _orgLevel = value; } }

        public string SCounty { get { return _sCounty; } set { _sCounty = value; } }
        public string SAthleticConf { get { return _sAthleticConf; } set { _sAthleticConf = value; } }
        public string SCESA { get { return _sCESA; } set { _sCESA = value; } }


#endregion

        #region public functions

        /// <summary>
        /// Converts the enumerated type into a user friendly string.
        /// </summary>
        /// <param name="compareTo"></param>
        /// <returns></returns>
        public static string Convert(CompareTo compareTo)
        {
            string retval = string.Empty;
            if (compareTo == CompareTo.CURRENTONLY)
                retval = "Current only";
            else if (compareTo == CompareTo.DISTSTATE)
                retval = "District / State";
            else if (compareTo == CompareTo.PRIORYEARS)
                retval = "Prior Years";
            else if (compareTo == CompareTo.SELDISTRICTS)
                retval = "Selected Districts";
            else if (compareTo == CompareTo.SELSCHOOLS)
                retval = "Selected Schools";

            return retval;
        }

        /// <summary>
        /// Converts the enumerated type into a user-friendly string.
        /// </summary>
        /// <param name="viewBy"></param>
        /// <returns></returns>
        public static string Convert(ViewByGroup viewBy)
        {
            string retval = viewBy.ToString();
            if (viewBy == ViewByGroup.AllStudentsFAY)
                retval = "All Students";
            else if (viewBy == ViewByGroup.RaceEthnicity)
                retval = "Race / Ethnicity";
            else if (viewBy == ViewByGroup.EconDisadv)
                retval = "Economic Status";
            else if (viewBy == ViewByGroup.ELP)
                retval = "English Language Proficiency";
            return retval;
        }

        /// <summary>
        /// Converts the enumerated type into a user friendly string.
        /// </summary>
        /// <param name="schoolType"></param>
        /// <returns></returns>
        public static string Convert(SchoolType schoolType)
        {
            string retval = string.Empty;
            //if (schoolType == SchoolType.AllTypes)
            //    retval = "All";
            //else if (schoolType == SchoolType.Elem)
            //    retval = "Elementary";
            //else if (schoolType == SchoolType.ElSec)
            //    retval = "Elementary / Secondary";
            //else if (schoolType == SchoolType.Hi)
            //    retval = "High";
            //else if (schoolType == SchoolType.Mid)
            //    retval = "Mid/Jr Hi";
            //else if (schoolType == SchoolType.StateSummary)
            //    retval = "State Summary";

            if (schoolType == SchoolType.AllTypes)
                retval = "(All School Types)";
            else if (schoolType == SchoolType.Elem)
                retval = "Elem. Schools";
            else if (schoolType == SchoolType.ElSec)
                retval = "El/Sec Combined Schls";
            else if (schoolType == SchoolType.Hi)
                retval = "High Schools";
            else if (schoolType == SchoolType.Mid)
                retval = "Mid/Jr Hi Schools";
            else if (schoolType == SchoolType.StateSummary)
                retval = "Summary";
            return retval;
        }

        //Notes for graph
        public virtual string GetViewByColumnName()
        {
            string retval = string.Empty;
            if ((OrgLevel != OrgLevel.School) && (SchoolType == SchoolType.AllTypes))
            {
                retval = CommonColumnNamesForGraph.SchooltypeLabel.ToString();
            }
            else
            {
                switch (ViewBy)
                {
                    case ViewByGroup.RaceEthnicity:
                        retval = CommonColumnNamesForGraph.RaceShortLabel.ToString();
                        break;

                    case ViewByGroup.Gender:
                        retval = CommonColumnNamesForGraph.SexLabel.ToString();
                        break;

                    case ViewByGroup.Grade:
                        retval = CommonColumnNamesForGraph.GradeShortLabel.ToString();
                        break;

                    case ViewByGroup.Disability:
                        retval = CommonColumnNamesForGraph.DisabilityLabel.ToString();
                        break;

                    case ViewByGroup.EconDisadv:
                        retval = CommonColumnNamesForGraph.ShortEconDisadvLabel.ToString();
                        break;

                    case ViewByGroup.ELP:
                        retval = CommonColumnNamesForGraph.ShortELPLabel.ToString();
                        break;

                    case ViewByGroup.AllStudentsFAY:
                        //Only need a column which has the same value in all rows and is not null
                        //This column has no meaning in the grid(table) display, just used for graph
                        retval = CommonColumnNamesForGraph.SexLabel.ToString();
                        break;
                }
            }
            return retval;
        }

        //Notes For Graph
        public virtual string GetCompareToColumnName()
        {
            string retval = string.Empty;
            switch (CompareTo)
            {
                case CompareTo.DISTSTATE:
                    retval = CommonColumnNamesForGraph.OrgLevelLabel.ToString();
                    break;

                case CompareTo.SELSCHOOLS:
                case CompareTo.CURRENTONLY:
                    if (OrgLevel == OrgLevel.District)
                    {
                        retval = CommonColumnNamesForGraph.District_Name.ToString().Replace("_", " ");
                    }
                    else if (OrgLevel == OrgLevel.School)
                    {
                        retval = CommonColumnNamesForGraph.School_Name.ToString().Replace("_", " ");
                    }
                    break;

                case CompareTo.PRIORYEARS:
                    retval = CommonColumnNamesForGraph.YearFormatted.ToString();
                    break;

            }
            return retval;
        }

        //Notes for graph
        public string GetSchoolType()
        {
            StringBuilder sb = new StringBuilder();

            if (CompareTo == CompareTo.SELSCHOOLS)
            {
                if (S4orALL != S4orALL.AllSchoolsOrDistrictsIn
                    && OrgLevel == OrgLevel.District)
                {
                    sb.AppendFormat(Convert(SchoolType));
                }
            }
            else
            {
                if (CompareTo != CompareTo.CURRENTONLY)
                {
                    sb.AppendFormat(Convert(SchoolType));
                }
            }

            return sb.ToString();
        }

        #endregion

        #region protected functions

        protected List<string> GetOrderByList(DataTable dt, CompareTo compareTo, string origFullKey)
        {
            List<string> retval = new List<string>();

            //Comments block from Bug #357:
            //PRIMARY SORT: The primary sort key for the Order By is set by Compare To:
            //- Prior Years: ORDER BY XXXFullkeyXXX yearColumn //(See Bug #357 Comment #3), 
            //- District/State: ORDER BY OrgLevelLabel, 
            //- Selected Schools: order by (case fullkey when '01361903XXXX' then
            //OrgLevelLabel else ltrim(Name) end), [yes, this uses a CASE statement in the
            //ORDER BY so that the initial agency selected by the user floats to the top,
            //then alphabetical thereafter]
            //- Current Data: ORDER BY Fullkey, [doesn't really matter, but can serve as a
            //good placeholder]

            if (compareTo == CompareTo.PRIORYEARS)
            {
                //See bug #357 Comment #3...
                //Also see Bug #403:
                //2. Reverse order of years in the table when Compare To: Prior Years is selected so most current year is first.
                if (dt.Columns.Contains(OrderByCols.year.ToString()))
                    retval.Add(OrderByCols.year.ToString() + " DESC");
            }

            else if (compareTo == CompareTo.DISTSTATE)
            {
                if (dt.Columns.Contains(OrderByCols.OrgLevelLabel.ToString()))
                    retval.Add(OrderByCols.OrgLevelLabel.ToString());
            }

            else if (compareTo == CompareTo.SELSCHOOLS)
            {
                if ((dt.Columns.Contains(OrderByCols.fullkey.ToString()))
                    && (dt.Columns.Contains(OrderByCols.OrgLevelLabel.ToString()))
                    && (dt.Columns.Contains(OrderByCols.Name.ToString())))
                {
                    string temp = string.Format("(case {0} when '{1}' then {2} else ltrim({3}) end)",
                        OrderByCols.fullkey.ToString(),
                        GetMaskedFullkey ( origFullKey, this.OrgLevel),
                        OrderByCols.OrgLevelLabel.ToString(),
                        OrderByCols.Name.ToString());
                    retval.Add(temp);
                }
            }
            else if (compareTo == CompareTo.CURRENTONLY)
            {
                if (dt.Columns.Contains(OrderByCols.fullkey.ToString()))
                    retval.Add(OrderByCols.fullkey.ToString());
            }

            //Comments block from Bug #357:
            //SECONDARY SORT: 
            //Note that, in some of my later pages, I've found that, after the primary sort,
            //I can I can sort statically on "SchooltypeLabel , SexCode, Racecode, GradeCode,
            //Disabilitycode" in (almost?) all cases - since only one of these will be broken
            //out, the sort order doesn't actually need to be changed - this may be the best
            //way to go...

            if (dt.Columns.Contains(OrderByCols.SchoolTypeLabel.ToString()))
                retval.Add(OrderByCols.SchoolTypeLabel.ToString());

            if (dt.Columns.Contains(OrderByCols.SexCode.ToString()))
                retval.Add(OrderByCols.SexCode.ToString());
            if (dt.Columns.Contains(OrderByCols.RaceCode.ToString()))
                retval.Add(OrderByCols.RaceCode.ToString());
            if (dt.Columns.Contains(OrderByCols.GradeCode.ToString()))
                retval.Add(OrderByCols.GradeCode.ToString());
            if (dt.Columns.Contains(OrderByCols.DisabilityCode.ToString()))
                retval.Add(OrderByCols.DisabilityCode.ToString());

            //Comments block from Bug #357:
            //In older pages, the secondary sort key for the Order By is set by Schooltype if
            //Schooltype = 1 (all types) - otherwise it's set by the View By code column - if
            //View BY = All Students, a placeholder column that all of the same values in it
            //(like GradeCode) is often put here: 
            //SexCode (sometimes GenderCode)
            //RaceCode
            //GradeCode
            //DisabilityCode

            //TERTIARY SORT: 
            //Retention and Dropouts don't have any custom links - but where a page does have
            //custom links and parameters, they may need to be added here (e.g.,
            //School-supported activities:

            return retval;
        }

        protected virtual void GetViewByList(ViewByGroup viewBy, 
            OrgLevel orgLevel, 
            out List<int> sexCodes, 
            out List<int> raceCodes, 
            out List<int> disabilityCodes, 
            out List<int> gradeCodes,
            out List<int> econDisadvCodes,
            out List<int> ELPCodes)
        {
            sexCodes = GenericsListHelper.GetPopulatedList(9);
            raceCodes = GenericsListHelper.GetPopulatedList(9);
            disabilityCodes = GenericsListHelper.GetPopulatedList(9);
            econDisadvCodes = GenericsListHelper.GetPopulatedList(9);
            ELPCodes = GenericsListHelper.GetPopulatedList(9);

            //default value for grade code == 98.
            gradeCodes = GenericsListHelper.GetPopulatedList(_gradeCode);            

            //prepare values in each Generics List.
            //The values below were laid down in the original Wisconsin website.
            //Since the upgrade of the website did NOT include changing the database design, 
            //the Business Layer is an good location to store these literal values.
            if (ViewBy == ViewByGroup.Gender)
                //replace default list with explicit list
                sexCodes = GenericsListHelper.GetPopulatedList(1, 2);
            else if (ViewBy == ViewByGroup.RaceEthnicity)
                raceCodes = GenericsListHelper.GetPopulatedList(1, 2, 3, 4, 5);
            else if (ViewBy == ViewByGroup.Grade)
            {
                //OrgLevel orgLevel = GetOrgLevelFromFullKey(OrigFullKey);
                if (orgLevel == OrgLevel.School)
                    gradeCodes = GenericsListHelper.GetPopulatedList(52, 64);
                else
                    gradeCodes = GenericsListHelper.GetPopulatedList(16, 64);
            }
            else if (ViewBy == ViewByGroup.Disability)
            {
                disabilityCodes = GenericsListHelper.GetPopulatedList(1, 2);                        
            }
            else if (ViewBy == ViewByGroup.EconDisadv)
            {
                econDisadvCodes = GenericsListHelper.GetPopulatedList(1, 2);
            }
            else if (ViewBy == ViewByGroup.ELP)
            {
                ELPCodes = GenericsListHelper.GetPopulatedList(1, 2);
            }
        }

        protected virtual List<int> GetYearList(int startYear)
        {
            List<int> years = GenericsListHelper.GetPopulatedList(_currentYear);
            if (CompareTo == CompareTo.PRIORYEARS)
            {
                if (ViewBy == ViewByGroup.Disability)
                {
                    //see bug 516, then Bug 670.
                    years = GenericsListHelper.GetPopulatedList(startYear, _currentYear);
                }
                else
                {
                    years = GenericsListHelper.GetPopulatedList(startYear, _currentYear);
                }
            }

            return years;
        }

        protected virtual List<int> GetYearList()
        {
            //From Bug #354:
            //When "Compare To = Prior years", the WHERE year parameter must be something
            //like "Year >= 1997 AND Year <= 2007" = otherwise should be "Year = 2007"...pulling
            //rows for all years doesn't work because sometime there are rows for earlier or
            //later years that we don't want to show....

            //Note that, in fact, it should be 
            //"Year >= $TrendStartYear AND Year <= $CurrentYear"
            //or "Year = $CurrentYear"
            //and that $TrendStartYear and $CurrentYear will differ from page to page and
            //will need to be easily configurable...

            List<int> years = GenericsListHelper.GetPopulatedList(_currentYear);
            if (CompareTo == CompareTo.PRIORYEARS)
            {
                if (ViewBy == ViewByGroup.Disability)
                {
                    //see bug 516, then Bug 670.
                    years = GenericsListHelper.GetPopulatedList(2003, _currentYear);
                }
                else
                {
                    years = GenericsListHelper.GetPopulatedList(_trendStartYear, _currentYear);
                }
            }

            return years;
        }

        protected virtual string GetClauseForCompareToSchoolsDistrict(
            string OrigFullKey, 
            S4orALL S4orALL,
            CompareTo compareTo,
            OrgLevel orgLevel, 
            string SCounty,
            string SAthleticConf, 
            string SCESA,
            SRegion SRegion,
            out bool useFullkeys)
        {
            string result = string.Empty;
            useFullkeys = false;

            bool compareToSelectSchoolsDistricts = 
                (compareTo == CompareTo.SELSCHOOLS ||
                   compareTo == CompareTo.SELDISTRICTS);

            if ( S4orALL == S4orALL.FourSchoolsOrDistrictsIn)
            {
                useFullkeys = true;
                result = "''";
            }
            else if (compareToSelectSchoolsDistricts &&
                S4orALL == S4orALL.AllSchoolsOrDistrictsIn)
            {

                if (SRegion == SRegion.County)
                {
                    if (orgLevel == OrgLevel.School)
                    {
                        result = string.Format(
                        " ( ( {0} in ('{1}') and right(fullkey,1)<>'X' ) or FullKey in ('{2}')) ",
                        "County", SCounty, GetMaskedFullkey( OrigFullKey,this.OrgLevel));
                    }
                    else if (orgLevel == OrgLevel.District)
                    {
                        result = string.Format(
                        " ( ( {0} in ('{1}') and right(fullkey,1)='X' ) or FullKey in ('{2}')) ",
                        "County", SCounty, GetMaskedFullkey(OrigFullKey, this.OrgLevel));
                    }
                }

                if (SRegion == SRegion.CESA)
                {
                    if (orgLevel == OrgLevel.School)
                    {
                        result = string.Format(
                        " ( ( {0} in ('{1}') and right(fullkey,1)<>'X' ) or FullKey in ('{2}')) ",
                        "CESA", SCESA, GetMaskedFullkey(OrigFullKey, this.OrgLevel));
                    }
                    else if (orgLevel == OrgLevel.District)
                    {
                        result = string.Format(
                        " ( ( {0} in ('{1}') and right(fullkey,1)='X' ) or FullKey in ('{2}')) ",
                        "CESA", SCESA, GetMaskedFullkey(OrigFullKey, this.OrgLevel));
                    }
                }

                if (SRegion == SRegion.AthleticConf)
                {
                    if (orgLevel == OrgLevel.School)
                    {
                        result = string.Format(
                        " ( ( {0} in ('{1}') and right(fullkey,1)<>'X' ) or FullKey in ('{2}')) ",
                        "ConferenceKey", SAthleticConf, GetMaskedFullkey(OrigFullKey, this.OrgLevel));
                    }
                    else if (orgLevel == OrgLevel.District)
                    {
                        result = string.Format(
                        " ( ( {0} in ('{1}') and right(fullkey,1)='X' ) or FullKey in ('{2}')) ",
                        "ConferenceKey", SAthleticConf, GetMaskedFullkey(OrigFullKey, this.OrgLevel));
                    }
                }
            }
            else
            {
                useFullkeys = true;
                result = "''";
            }
               
            return result;

        }

        protected virtual List<string> GetFullKeysList(CompareTo compareTo, 
            OrgLevel orgLevel, string origFullKey, List<string> compareToSchoolsOrDistrict)
        {
            string theFullKey = string.Empty;

            //create all 3 variations of the original fullKey
            string schoolFullKey = GetMaskedFullkey(origFullKey, OrgLevel.School);
            string distFullKey = GetMaskedFullkey(origFullKey, OrgLevel.District);
            string stateFullKey = GetMaskedFullkey(origFullKey, OrgLevel.State);

            switch (orgLevel)
            {
                case OrgLevel.State:
                    theFullKey = stateFullKey;
                    break;
                case OrgLevel.District:
                    theFullKey = distFullKey;
                    break;
                case OrgLevel.School:
                    theFullKey = schoolFullKey;
                    break;
            }

            List<string> retval = new List<string>();

            //TODO:  change this to a parameter that is passed in.  --djw 5/11/08
            //OrgLevel origOrgLevel = GetOrgLevelFromFullKeyX(origFullKey);
            

            if ((compareTo == CompareTo.PRIORYEARS) || (compareTo == CompareTo.CURRENTONLY))
                //When comparing to Prior Years or Current School Data, 
                //  the WHERE clause should just contain the given (nothing else).

                retval.Add(theFullKey);
            else if (compareTo == CompareTo.DISTSTATE)
            {

                //always add the State fullkey to the list.
                retval.Add(stateFullKey);

                if (orgLevel == OrgLevel.School)
                {
                    //org level is school.  also add school & district full keys.
                    retval.Add(schoolFullKey);
                    retval.Add(distFullKey);
                }
                else if (orgLevel == OrgLevel.District)
                {
                    //org level is district.  Add District fullkey.
                    retval.Add(distFullKey);
                }
            }
            else if (compareTo == CompareTo.SELSCHOOLS )
            {
                //add the original fullkey
                retval.Add(origFullKey);

                // no need for other fullkeys if In all Schools Or Districts
                if (S4orALL == S4orALL.FourSchoolsOrDistrictsIn &&
                    compareToSchoolsOrDistrict != null )
                {
                    //also add all of the CompareToSchoolsOrDistrict, but masked appropriately.
                    foreach (string compareToSchool in compareToSchoolsOrDistrict)
                    {
                        string maskedFullKey = GetMaskedFullkey(compareToSchool, orgLevel);
                        retval.Add(maskedFullKey);
                    }
                }
            }

            return retval;

        }

        public static string GetMaskedFullkey(string origFullKey, OrgLevel desiredOrgLevel)
        {
            string retval = origFullKey;
            OrgLevel origOrgLevel = GetOrgLevelFromFullKeyX(origFullKey);

            if (desiredOrgLevel == OrgLevel.School)
            {
                if ((origOrgLevel == OrgLevel.District) || (origOrgLevel == OrgLevel.State))
                {
                    //cannot create a school's fullkey from a District or State fullky.
                    retval = string.Empty;
                }

                //otherwise, continue using default value
            }
            else if (desiredOrgLevel == OrgLevel.District)
            {
                if (origOrgLevel == OrgLevel.State)
                {
                    //cannot create s District fullkey given a State fullkey.
                    retval = string.Empty;
                }
                else
                {
                    //otherwise, replace the last 4 digits of the fullkey
                    //retval = origFullKey.Substring(0, 7) + "3XXXX"; //BR: shoulb be "3ZZZZ" for out going URL
                    //to fix Charter school issues
                    retval = origFullKey.Substring(0, 6) + "03XXXX"; //BR: shoulb be "03ZZZZ" for out going URL
                }
            }
            else
            {
                //orglevel == State
                retval = "XXXXXXXXXXXX"; //BR: shoulb be "ZZZZZZZZZZZZ" for out going URL 6/08
            }

            return retval;
        }

        protected static OrgLevel GetOrgLevelFromFullKeyX(string origFullKey)
        {
            OrgLevel orgLevel = OrgLevel.School;
            if (origFullKey.Length != 12)
                throw new Exception("FullKey must be 12 characters long.");

            if (origFullKey.StartsWith("X"))
                orgLevel = OrgLevel.State;
            else if (origFullKey.Contains("X"))
                orgLevel = OrgLevel.District;

            return orgLevel;
        }

        public static OrgLevel GetOrgLevel(string ORGLEVEL)
        {
            //keep for backwards compatibility with old site.
            if (ORGLEVEL.ToLower() == "sc")
                ORGLEVEL = OrgLevel.School.ToString();
            else if (ORGLEVEL.ToLower() == "di")
                ORGLEVEL = OrgLevel.District.ToString();
            else if (ORGLEVEL.ToLower() == "st")
                ORGLEVEL = OrgLevel.State.ToString();

            OrgLevel retval = (OrgLevel)Enum.Parse(typeof(OrgLevel), ORGLEVEL);
            return retval;
        }

        protected List<int> GetSchoolTypesList(SchoolType schoolType)
        {
            List<int> schoolTypes;

            if (schoolType != SchoolType.AllTypes)
            {
                schoolTypes = GenericsListHelper.GetPopulatedList((int)this.SchoolType);
            }
            else
            {
                schoolTypes = GenericsListHelper.GetPopulatedList((int)SchoolType.Elem,
                    (int)SchoolType.Mid,
                    (int)SchoolType.Hi,
                    (int)SchoolType.ElSec,
                    int.MinValue);
            }
            return schoolTypes;
        }

        protected string FullKeyEncode(string fullKey)
        {
            return fullKey.ToLower().Replace(
                Constants.FULL_KEY_INTERNAL_LETTER.ToLower(),
                Constants.FULL_KEY_INTERNAL_LETTER);
        }

        protected List<string> FullKeyEncode(List<string> fullKeys)
        {
            List<string> fullKeysEncoded = new List<string>();
            foreach (string fullkey in fullKeys)
            {
                fullKeysEncoded.Add(FullKeyEncode(fullkey));
            }
            return fullKeysEncoded;
        }

        protected string FullKeyDecode(string fullKey)
        {
            // convert from "z/Z" to "X"
            return fullKey.ToLower().Replace(
                Constants.FULL_KEY_INTERNAL_LETTER.ToLower(),
                Constants.FULL_KEY_EXTERNAL_LETTER );
        }

        protected List<string> FullKeyDecode(List<string> fullKeys)
        {
            List<string> fullKeysDecoded = new List<string>();
            foreach (string fullkey in fullKeys)
            {
                fullKeysDecoded.Add(FullKeyDecode(fullkey));
            }
            return fullKeysDecoded;
        }
        #endregion
    }
}
