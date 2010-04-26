using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SligoCS.DAL.WI;
using SligoCS.BL.WI;
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.Base.WebServerControls.WI;

using SligoCS.Web.WI.WebSupportingClasses.WI;
using SligoCS.Web.WI.WebUserControls;

namespace SligoCS.Web.WI
{
    public partial class StateTestsSimilarSchools : PageBaseWI
    {
        protected override DALWIBase InitDatabase()
        {
            return new DALWSASSimilarSchools();
        }
        protected override GridView InitDataGrid()
        {
            return SimilarDataGrid;
        }
        protected override ChartFX.WebForms.Chart InitGraph()
        {
            return barChart;
        }
        //protected override DataSet InitDataSet()
        //protected override NavViewByGroup InitNavRowGroups()
        protected override string SetPageHeading()
        {
            return "Are there similar** districts that might provide ideas to try?";
        }
        protected override void OnInit(EventArgs e)
        {
            OnCheckPrerequisites += new CheckPrerequisitesHandler(StateTestsSimilarSchools_OnCheckPrerequisites);
            base.OnInit(e);
        }

        void StateTestsSimilarSchools_OnCheckPrerequisites(PageBaseWI page, EventArgs args)
        {
            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.State) return;

            if (FullKeyUtils.GetOrgLevelFromFullKeyX(GlobalValues.FULLKEY).Key == OrgLevelKeys.State)
                OnRedirectUser += InitialAgencyRedirect;
        }
        protected override void OnInitComplete(EventArgs e)
        {
            GlobalValues.Year = 2010;

            //Disable "All Tested Subjects"
            //TODO:
            //if (GlobalValues.SubjectID.Key == SubjectIDKeys.AllTested) GlobalValues.SubjectID.Key = SubjectIDKeys.Reading;
            //nlrSubject.LinkControlAdded += new LinkControlAddedHandler(disableAllSubjects_LinkControlAdded);

            //View By Group Unsupported.
            GlobalValues.Group.Key = GroupKeys.All;
            //SchoolType Unsupported.
            GlobalValues.OverrideSchoolTypeWhenOrgLevelIsSchool_Complete += PageBaseWI.DisableSchoolType;
            //Compare To Unsupported.
            GlobalValues.CompareTo.Key = CompareToKeys.Current;
            //StateLevel Unsupported.
            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.State) GlobalValues.OrgLevel.Key = OrgLevelKeys.District;

            GlobalValues.FAYCode.Key = FAYCodeKeys.FAY;

            GlobalValues.OverrideByNavLinksNotPresent(GlobalValues.SubjectID, nlrSubject, SubjectIDKeys.Reading);
            GlobalValues.OverrideByNavLinksNotPresent(GlobalValues.Grade, nlrGrade, GradeKeys.Combined_PreK_12);

            GlobalValues.GradeCodesActive = StateTestsScatterplot.getGradeCodeRange(this);
            if (!GlobalValues.GradeCodesActive.Contains(GlobalValues.Grade.Value)) GlobalValues.Grade.Value = GlobalValues.GradeCodesActive[0];  //default to minimum grade

            nlrGrade.LinkControlAdded += new LinkControlAddedHandler(StateTestsScatterplot.disableGradeLinks_LinkControlAdded);

            //TODO:
            //GlobalValues.OverrideByNavLinksNotPresent(GlobalValues.Level, nlrLevel, LevelKeys.AdvancedProficient);
            //if (GlobalValues.Level.Key == LevelKeys.All) GlobalValues.Level.Key = LevelKeys.AdvancedProficient;
            //nlrLevel.LinkControlAdded += new LinkControlAddedHandler(disableLevels_LinkControlAdded);

            if (GlobalValues.WOW.Key == WOWKeys.WSASCombined
                && (GlobalValues.Level.Key == LevelKeys.WAA_ELL || GlobalValues.Level.Key == LevelKeys.WAA_SwD))
                GlobalValues.Level.Key = LevelKeys.NoWSAS;



            //if (GlobalValues.NoChce.Key == NoChceKeys.On)
            string strParam = String.Empty;
            object objParam;
            objParam = StickyParameter.GetParamFromUser(GlobalValues.NoChce.Name);
            strParam = (objParam == null) ? String.Empty : objParam.ToString();

            //When no Choice On, then override everything
            //also remove it from the QS
            //When no other options are chosen, then Set Globals to No Choice On

            if (StickyParameter.inQS.Contains(GlobalValues.NoChce.Name)
                && GlobalValues.NoChce.Key == NoChceKeys.On)
            {
                UserValues.SPEND.Key = GlobalValues.SPEND.Key = SPENDKeys.Off;
                UserValues.SIZE.Key = GlobalValues.SIZE.Key = SIZEKeys.Off;
                UserValues.ECON.Key = GlobalValues.ECON.Key = ECONKeys.Off;
                UserValues.DISABILITY.Key = GlobalValues.DISABILITY.Key = DISABILITYKeys.Off;
                UserValues.LEP.Key = GlobalValues.LEP.Key = LEPKeys.Off;

                StickyParameter.inQS.Remove(GlobalValues.NoChce.Name);
            }

            if (!
                (GlobalValues.SPEND.Key == SPENDKeys.On
                || GlobalValues.SIZE.Key == SIZEKeys.On
                || GlobalValues.ECON.Key == ECONKeys.On
                || GlobalValues.DISABILITY.Key == DISABILITYKeys.On
                || GlobalValues.LEP.Key == LEPKeys.On)
                )
            {
                GlobalValues.NoChce.Key = NoChceKeys.On;
            }

            base.OnInitComplete(e);

            if (GlobalValues.Grade.Key == GradeKeys.Combined_PreK_12)
                DataGrid.Columns.FieldsChanged += new EventHandler(StateTestsScatterplot.renameEnrolledColumn_FieldsChanged);

            nlrCompareTo.ShowSimilarSchoolsLink = true;

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSetTitle = GetTitleWithoutGroupForSchoolTypeUnsupported(
                    ((GlobalValues.WOW.Key == WOWKeys.WSASCombined) ?
                        WOWKeys.WSASCombined : WOWKeys.WKCE)
                        + " - " + (
                        (GlobalValues.Grade.Key == GradeKeys.Combined_PreK_12
                        || GlobalValues.Grade.Key == GradeKeys.AllDisAgg)
                        ? String.Empty
                        : "Grade "
                        ) + GlobalValues.Grade.Key
                        + " - " + GlobalValues.SubjectID.Key 
                    )
                ;

            DataSetTitle = DataSetTitle.Replace(
                    GlobalValues.GetOrgName(),
                    GlobalValues.GetOrgName() + "Compared to Other "
                    + ((GlobalValues.OrgLevel.Key == OrgLevelKeys.School) ? "Schools" : "Districts") + " in "
                    + ((GlobalValues.LF.Key == LFKeys.State) ? "Entire State" :
                    (GlobalValues.LF.Key == LFKeys.CESA) ? "CESA " + GlobalValues.Agency.CESA.Trim() :
                    GlobalValues.Agency.CountyName.Trim()
                    )
                    );

            if (GlobalValues.FAYCode.Key == FAYCodeKeys.FAY)
                DataSetTitle = DataSetTitle.Replace(GlobalValues.GetOrgName(), GlobalValues.GetOrgName().Trim() + "  FAY ");

            DataSetTitle =
                DataSetTitle.Replace(TitleBuilder.GetYearRangeInTitle(QueryMarshaller.years),
                     "November " + (GlobalValues.Year - 1).ToString() + " Data");

            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.School)
            {
                DataSetTitle = DataSetTitle.Replace(TitleBuilder.GetSchoolTypeInTitle(GlobalValues.STYP), String.Empty);
            }

            SetUpChart();

            set_state();
            setBottomLink();
        }
        private void SetUpChart()
        {

        }
        public override List<string> GetVisibleColumns(Group viewBy, OrgLevel orgLevel, CompareTo compareTo, STYP schoolType)
        {
            return base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);
        }
        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                (WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                (WI.displayed_obj.dataLinksPanel, true);
        }
        private void setBottomLink()
        {
            BottomLinkViewProfile.DistrictCd = GlobalValues.DistrictCode;
        }
    }
}
