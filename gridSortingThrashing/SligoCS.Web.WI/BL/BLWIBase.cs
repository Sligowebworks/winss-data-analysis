using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SligoCS.DAL.WI;
using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.BL.WI
{
    /// <summary>
    /// This will be the base class for all Wisconsin BLs, not entities.
    /// </summary>
    public class BLWIBase
    {

        #region deprecated
        protected STYP SchoolType;
        protected List<int> GetSchoolTypesList(STYP type)
        {
            return new List<int>();
        }
        protected virtual List<int> GetYearList(int startYear)
        {
            List<int> years = GenericsListHelper.GetPopulatedList(_currentYear);
            if (CompareTo.Key == CompareToKeys.Years)
            {
                if (ViewBy.Key == GroupKeys.Disability)
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
            if (CompareTo.Key == CompareToKeys.Years)
            {
                if (ViewBy.Key == GroupKeys.Disability)
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

        #endregion //deprecated


        #region class level variables
        protected string sql = string.Empty;
        protected int _trendStartYear = 1997;  //default for Retention.  Override in derived classes as necessary.
        protected int _currentYear = 2007;     //Note: CurrentYear = 2008; default for FLANDERS.  Override in derived classes as necessary.
        protected int _gradeCode = 98;          //default for Retention.  Override in derived classes as necessary.
        protected List<string> _fullKeys; //contains OrigFullKey + several potential other fullkeys and/or their masks.
        protected string _clauseForCompareToSchoolsDistrict; //contains OrigFullKey + several potential other fullkeys and/or their masks.
        
        private string _origFullKey;
        private List<int> _years;
        private CompareTo _compareTo;
        private List<string> _compareToSchools;
        private S4orALL _s4orALL;
        private Group _viewBy;
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
        public CompareTo CompareTo { get { return _compareTo; } set { _compareTo = value; } }
        public List<string> CompareToSchoolsOrDistrict 
            { get { return _compareToSchools; } set { _compareToSchools = value; } }
        public S4orALL S4orALL { get { return _s4orALL; } set { _s4orALL = value; } }
        public SRegion SRegion { get { return _sRegion; } set { _sRegion = value; } }

        public int TrendStartYear { get { return _trendStartYear; } }
        public int CurrentYear { get { return _currentYear; } }
        public List<int> Years { get { return _years; } set { _years = value; } }
        public Group ViewBy { get { return _viewBy; } set { _viewBy = value; } }
        public OrgLevel OrgLevel { get { return _orgLevel; } set { _orgLevel = value; } }

        public string SCounty { get { return _sCounty; } set { _sCounty = value; } }
        public string SAthleticConf { get { return _sAthleticConf; } set { _sAthleticConf = value; } }
        public string SCESA { get { return _sCESA; } set { _sCESA = value; } }


#endregion

                #region protected functions

        protected List<string> GetOrderByList(DataTable dt, CompareTo compareTo, string origFullKey)
        {
            List<string> retval = new List<string>();

            return retval;
        }

        protected virtual void GetViewByList(Group ViewBy, 
            OrgLevel orgLevel, 
            out List<int> sexCodes, 
            out List<int> raceCodes, 
            out List<int> disabilityCodes, 
            out List<int> gradeCodes,
            out List<int> econDisadvCodes,
            out List<int> ELPCodes)
        {
            sexCodes = new List<int>();
            raceCodes = new List<int>();
            disabilityCodes = new List<int>();
            gradeCodes = new List<int>();
            econDisadvCodes = new List<int>();
            ELPCodes = new List<int>();
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
            useFullkeys = false;
            return  String.Empty;
        }

        #endregion
    }
}
