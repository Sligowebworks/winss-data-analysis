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

using SligoCS.BL.WI;
using SligoCS.DAL.WI;
//using SligoCS.Web.WI.DAL.DataSets;
using SligoCS.Web.WI.WebUserControls;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
    /// <summary>
    /// Scope for globally utilized variables. Serves as additional encapsulation for User Parameters. That is, any programatic overrides should be on fields in this class rather than the raw StickyParamers Object. All tests of user input should be through this object.
    /// </summary>
    public class GlobalValues : QueryStringUtils
    {
        public GlobalValues() : base()
        {
            this.STYP = InitSchoolType();
            this.Grade = InitGrade();
        }
        
        #region properties
        private string sql;
        private string traceSql = String.Empty;
        private bool shortCircuitRedirectTests = false;
        private SligoCS.Web.Base.PageBase.WI.PageBaseWI myPage;

        public String SdprQS
        {
            get {
                if (this.OrgLevel.Key == OrgLevelKeys.District)
                {
                    return "&district=" + this.DistrictCode;
                }
                else if (this.OrgLevel.Key == OrgLevelKeys.School)
                {
                    return "&district=" + this.DistrictCode + "&compareTo=SCH";
                } else // if (this.OrgLevel.Key == OrgLevelKeys.State)
                {
                    return "&district=0007";
                }
            }
        }

        public SligoCS.Web.Base.PageBase.WI.PageBaseWI Page
        {
            get { return myPage; }
            set { myPage = value; }
        }

        private  FAYCode fayCode = new FAYCode();
        public FAYCode FAYCode { get { return fayCode; } set { fayCode = value; } }

        private List<String> gradeCodesActive = new List<String>();
        public List<String> GradeCodesActive { get { return gradeCodesActive; } set { gradeCodesActive = value; } }

        private SupDwnld superDownload = new SupDwnld();
        public SupDwnld SuperDownload { get { return superDownload; } set { superDownload = value; } }
        
        /// <summary>
        /// For Tracing, all assignments wil be cached for Tracing. Field may be assigned and used locally for a single SQL statement.
        /// Cache is kept in field, TraceSql.
        /// </summary>
        public string SQL { get { return sql; } set {traceSql +=  "<br />" + (sql = value); } }
        
        public String TraceSql
        {
            get { return traceSql; }
            set { traceSql = value; }
        }

        /// <summary>
        /// Stops all redirects raised by OnRedirectUser;
        /// As of now, only used for "show schools",
        /// i.e. SchoolScript link that changes OrgLevel
        /// because can trigger SelectedSchools Redirect erroneously
        /// </summary>
        public Boolean ShortCircuitRedirectTests
        {
            get { return shortCircuitRedirectTests; }
            set { shortCircuitRedirectTests = value; }
        }
        
        /// <summary>
        /// If STYP is set in the querystring, use that. 
        /// Otherwise, if the Fullkey is provided and matches a single school, do a
        ///     db lookup to determine the STYP,
        /// Otherwise, use the GlobalValues default STYP (3).
        /// </summary>
        /// <param name="fullKey"></param>
        /// <returns></returns>
        private STYP InitSchoolType()
        {
            string strParam = String.Empty;
            object objParam;

            objParam = STYP.GetParamFromUser(STYP.Name);

            strParam = (objParam == null)? String.Empty : objParam.ToString();
            
            //if not chosen by user, use the selected school, or the default
            if (strParam == String.Empty) strParam = Agency.STYP;

            STYP retval = new STYP();
            if (!String.IsNullOrEmpty(strParam)) retval.Value = strParam;
            
            return retval;
        }
        private Grade InitGrade()
        {
            string value = String.Empty;
            object objgrade = Grade.GetParamFromUser(Grade.Name);
            value = (objgrade == null) ? String.Empty : objgrade.ToString();

            //if not chosen by user, use the minimum grade:
            if (value == String.Empty)
            {
                value = Agency.LowGrade.ToString();
                //inconsistency in the agency table with the grademap values the Parameter class is based on
                if (value == "11") value = "12";
            }

            Grade grade = new Grade();
            grade.Value = value;
            return grade;
        }

        public string SFullKeys(OrgLevel level)
        {
            if (level.Key == OrgLevelKeys.School)
                return SSchoolFullKeys;
            else if (level.Key == OrgLevelKeys.District)
                return SDistrictFullKeys;
            else
                return null;
        }

        /// <summary>
        /// Returns a District Code from the current District or School Fullkey 
        /// </summary>
        /// <returns></returns>
        public String DistrictCode
        {
            get
            {
                String result = String.Empty;
                if (OrgLevel.Key == OrgLevelKeys.District
                    || OrgLevel.Key == OrgLevelKeys.School)
                {
                    result = FULLKEY.Substring(2, 4);
                }
                return result;
            }
        }
        /// <summary>
        /// Returns a School Code from the current Fullkey
        /// </summary>
        /// <returns></returns>
        public String SchoolCode
        {
            get
            {
                String result = String.Empty;
                if (OrgLevel.Key == OrgLevelKeys.School)
                {
                    result = FULLKEY.Substring(8, 4);
                }
                return result;
            }
        }

        private DALAgency agency;

        public DALAgency Agency
        {
            get {
                if (agency == null)
                {
                    agency = new DALAgency();
                    QueryMarshaller qm =  new QueryMarshaller(this);
                    SQL = agency.BuildSQL(qm);
                    qm.AssignQuery(agency, SQL);
                }
                return agency; 
            }
        }

        public String GetOrgName()
        {
            if (OrgLevel.Key == OrgLevelKeys.School)
                return Agency.Schoolname.Trim();
            if (OrgLevel.Key == OrgLevelKeys.District)
                return Agency.DistrictName.Trim();
            // else, State
            return "Entire State";
        }
        private int highgrade;
        public int HIGHGRADE { 
            get 
            { 
                if (highgrade == 0) highgrade = Agency.HighGrade;
                return highgrade; 
            } 
            set { highgrade = value; } 
        }
        private int lowgrade;
        public int LOWGRADE
        { 
            get 
            {
                if (lowgrade == 0) lowgrade = Agency.LowGrade;
                return lowgrade; 
            } 
            set { lowgrade = value; }
        }

#endregion

#region Constraints on Parameters
        public static void associateCompareSelectedToOrgLevel(GlobalValues user, GlobalValues app)
        {
            //CompareSelected Overrides are propagated to the User Values so that Dialogues (ChooseSelected) can pick up overrides

            if (app.OrgLevel.Key == OrgLevelKeys.District
                && app.CompareTo.Key == CompareToKeys.SelSchools)
                user.CompareTo.Value = app.CompareTo.Value = app.CompareTo.Range[CompareToKeys.SelDistricts];

            if (app.OrgLevel.Key == OrgLevelKeys.School
            && app.CompareTo.Key == CompareToKeys.SelDistricts)
                user.CompareTo.Value = app.CompareTo.Value = app.CompareTo.Range[CompareToKeys.SelSchools];
        }
        /// <summary>
        /// Forces CompareTo to Prior Years when State Level is selected
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        public void OverrideCompareToWhenOrgLevelIsState(Object sender, EventArgs e)
        {
            if (OrgLevel.Key == OrgLevelKeys.State
                && !(CompareTo.Key == CompareToKeys.Current || CompareTo.Key == CompareToKeys.Years)
                )
            {
                CompareTo.Value = CompareTo.Range[CompareToKeys.Years];
            }
        }
        public void OverrideSchoolTypeWhenGroupIsGrade(Object sender, EventArgs e)
        {
            if (Group.Key == GroupKeys.Grade
                && STYP.Key != STYPKeys.StateSummary
                && OrgLevel.Key != OrgLevelKeys.School)
            {
                STYP.Value = STYP.Range[STYPKeys.StateSummary];
            }
        }
        public void OverrideSchoolTypeWhenOrgLevelIsSchool(Object sender, EventArgs e)
        {
            if (OrgLevel.Key == OrgLevelKeys.School)
                STYP.Value = Agency.STYP;

            if (OverrideSchoolTypeWhenOrgLevelIsSchool_Complete != null) OverrideSchoolTypeWhenOrgLevelIsSchool_Complete(this, new EventArgs());
        }
        public event EventHandler OverrideSchoolTypeWhenOrgLevelIsSchool_Complete;

        public void OverrideGroupWhenSchoolTypeIsAll(Object sender, EventArgs e)
        {
            if (STYP.Key == STYPKeys.AllTypes
                && CompareTo.Key != CompareToKeys.Current)
            {
                Group.Value = Group.Range[GroupKeys.All];
            }
        }
        public void OverrideGroupByLinksShown(NavViewByGroup.EnableLinksVector show)
        {
            bool ovr = false;
            ovr =
                (
                    Group.Key == GroupKeys.Gender
                    && show < NavViewByGroup.EnableLinksVector.Gender
                )
                ||
                (
                    Group.Key == GroupKeys.Race
                    && show < NavViewByGroup.EnableLinksVector.Race
                )
                ||
                (
                    Group.Key == GroupKeys.RaceGender
                    && (show < NavViewByGroup.EnableLinksVector.RaceGender
                    || !Page.NavRowGroups.AddRaceGender)
                )
                ||
                (
                    Group.Key == GroupKeys.Grade
                    && show < NavViewByGroup.EnableLinksVector.Grade
                )
                ||
                (
                       Group.Key == GroupKeys.Disability
                       && show < NavViewByGroup.EnableLinksVector.Disability
                )
                ||
                (
                   (Group.Key == GroupKeys.EconDisadv
                   || Group.Key == GroupKeys.EngLangProf)
                   && show < NavViewByGroup.EnableLinksVector.EconElp
                )
                ||
                (
                       Group.Key == GroupKeys.Migrant
                       && show < NavViewByGroup.EnableLinksVector.Migrant
                )
            ;

            if (ovr) Group.Value = Group.Range[GroupKeys.All];
            if (TraceLevels > 0 && ovr) Page.Response.Write("OverrideGroupByLinksShown::" + show);
        }

        /// <summary>
        /// Allows for aspx to be used to fully configure which options are available
        /// Utilizes ParameterValues.OverrideIfNotInList() to override.
        /// If the current selection is not configured in the page aspx, then the defaultValue argument is applied
        /// </summary>
        /// <param name="param"></param>
        /// <param name="nlr"></param>
        /// <param name="defaultValue">Can be a Value or a Key. </param>
        public void OverrideByNavLinksNotPresent(ParameterValues param, NavigationLinkRow nlr, String defaultValue)
        {
            if (!param.Range.ContainsValue(defaultValue) && param.Range.ContainsKey(defaultValue))
            {// argument was a key, not a value, so convert
                defaultValue = param.Range[defaultValue];
            }

            SligoCS.Web.Base.WebServerControls.WI.HyperLinkPlus lnk;
            List<String> values = new List<String>();

            foreach (SligoCS.Web.Base.WebServerControls.WI.HyperLinkPlus ctrl in nlr.NavigationLinks)
            {
                lnk = (SligoCS.Web.Base.WebServerControls.WI.HyperLinkPlus)ctrl;
                values.Add(lnk.ParamValue.ToString());
            }

            if (values.Count > 0) param.OverrideIfNotInList(values, defaultValue);
        }
        public void OverrideLowGradeHighGradeForPriorYears(Object sender, EventArgs e)
        {
            if (!(CompareTo.Key == CompareToKeys.Years && Group.Key == GroupKeys.Grade))
                return;

            DALLowGradeHighGradePY dal = new DALLowGradeHighGradePY();
            QueryMarshaller qm = new QueryMarshaller(this);

            System.Collections.Hashtable lowhi = dal.GetLowGradeHiGradePY(qm);
            if (lowhi == null) return;

            this.LOWGRADE = int.Parse((string)lowhi["low"]);
            this.HIGHGRADE = int.Parse((string)lowhi["hi"]);

            SligoCS.BL.WI.QueryMarshaller.SetLowGradeFloors(this);
        }
        public void OverrideWhenDownloadStateWide()
        {
            if (SuperDownload.Key == SupDwnldKeys.True)
            {
                CompareTo.Key = CompareToKeys.SelSchools;
                S4orALL.Key = S4orALLKeys.AllSchoolsOrDistrictsIn;
                SRegion.Key = SRegionKeys.Statewide;
            }
        }
#endregion
    }
}
