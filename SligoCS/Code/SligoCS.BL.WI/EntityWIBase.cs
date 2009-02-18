using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SligoCS.BL.Base;

namespace SligoCS.BL.WI
{

    //AKA STYP
    public enum SchoolType 
    {
        AllTypes = 1,
        Elem = 6,
        Mid = 5,
        Hi = 3,
        ElSec = 7,
        StateSummary = 9
    }

    public enum ViewByGroup
    {
        AllStudentsFAY,
        Gender,
        RaceEthnicity,
        Grade,
        Disability
    }

    public enum CompareTo
    {
        PRIORYEARS,
        DISTSTATE,
        SELSCHOOLS,
        CURRENTONLY
    }

    public enum OrgLevel
    {
        School,
        District,
        State
    }


    public enum OrderByCols
    {
        year,
        OrgLevelLabel,
        fullkey,
        Name,
        SchoolTypeLabel,
        SexCode,
        RaceCode,
        GradeCode,
        DisabilityCode
    }



    /// <summary>
    /// This will be the base class for all Wisconsin entities.
    /// </summary>
    public abstract class EntityWIBase : EntityBase
    {
        #region class level variables
        protected string sql = string.Empty;
        protected int _trendStartYear;  //e.g. 1997 for Retention
        protected int _currentYear;     //e.g. 2006 for Retention
        private string _origFullKey;
        protected List<string> _fullKeys; //contains OrigFullKey + several potential other fullkeys and/or their masks.
        private SchoolType _schoolType;
        private CompareTo _compareTo;
        private List<string> _compareToSchools;
        private ViewByGroup _viewBy;
        private OrgLevel _orgLevel;
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
        public List<string> CompareToSchools { get { return _compareToSchools; } set { _compareToSchools = value; } }
        public ViewByGroup ViewBy { get { return _viewBy; } set { _viewBy = value; } }
        public OrgLevel OrgLevel { get { return _orgLevel; } set { _orgLevel = value; } }
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
                //retval.Add(ds._v_RetentionWWoDisSchoolDistState.fullkeyColumn.ColumnName);                
                if (dt.Columns.Contains(OrderByCols.year.ToString()))
                    retval.Add(OrderByCols.year.ToString());
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
                        origFullKey,
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




        protected List<string> GetFullKeysList(CompareTo compareTo, string origFullKey, List<string> compareToSchools)
        {
            List<string> retval = new List<string>();
            OrgLevel orgLevel = GetOrgLevelFromFullKey(origFullKey);


            if ((compareTo == CompareTo.PRIORYEARS) || (compareTo == CompareTo.CURRENTONLY))
                //When comparing to Prior Years or Current School Data, 
                //  the WHERE clause should just contain the given (nothing else).
                retval.Add(origFullKey);
            else if (compareTo == CompareTo.DISTSTATE)
            {
                //create all 3 variations of the original fullKey
                string schoolFullKey = GetMaskedFullkey(origFullKey, OrgLevel.School);
                string distFullKey = GetMaskedFullkey(origFullKey, OrgLevel.District);
                string stateFullKey = GetMaskedFullkey(origFullKey, OrgLevel.State);

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
            else if (compareTo == CompareTo.SELSCHOOLS)
            {
                //add the original fullkey
                retval.Add(origFullKey);

                //also add all of the CompareToSchools, but masked appropriately.
                foreach (string compareToSchool in compareToSchools)
                {
                    string maskedFullKey = GetMaskedFullkey(compareToSchool, orgLevel);
                    retval.Add(maskedFullKey);
                }
            }

            return retval;


        }



        protected string GetMaskedFullkey(string origFullKey, OrgLevel desiredOrgLevel)
        {
            string retval = origFullKey;
            OrgLevel orgLevel = GetOrgLevelFromFullKey(origFullKey);

            if (desiredOrgLevel == OrgLevel.School)
            {
                if ((orgLevel == OrgLevel.District) || (orgLevel == OrgLevel.State))
                {
                    //cannot create a school's fullkey from a District or State fullky.
                    retval = string.Empty;
                }

                //otherwise, continue using default value
            }
            else if (desiredOrgLevel == OrgLevel.District)
            {
                if (orgLevel == OrgLevel.State)
                {
                    //cannot create s District fullkey given a State fullkey.
                    retval = string.Empty;
                }
                else
                {
                    //otherwise, replace the last 4 digits of the fullkey
                    retval = origFullKey.Substring(0, 7) + "3XXXX";
                }
            }
            else
            {
                //orglevel == State
                retval = "XXXXXXXXXXXX";
            }

            return retval;
        }




        public static OrgLevel GetOrgLevelFromFullKey(string origFullKey)
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


        #endregion
    }
}
