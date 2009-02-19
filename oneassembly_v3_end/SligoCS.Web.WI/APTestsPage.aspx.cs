using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;
using SligoCS.BLL.WI;
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.WI.HelperClasses;
using SligoCS.Web.Base.WebSupportingClasses.WI;
using StickyParamsEnum =
        SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.QStringVar;


namespace SligoCS.Web.WI
{

    /// <summary>
    /// This page was the initial demonstration of the SligoDataGrid.
    /// This page is also known as the APTests page.
    /// </summary>
    public partial class APTestsPage : PageBaseWI
    {

        protected v_AP_TESTS _ds = new v_AP_TESTS();
        protected BLAP_Tests APTests = null;
        private StickyParameter stickyParameter = null;

        public APTestsPage()
        {
            //For the AP Page, we have an extra stickyParameter SubjectID not used anywhere else...
            StickyParameter = new StickyParameter();
            stickyParameter = StickyParameter;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Master.EnableViewState = false;
            if (BLWIBase.GetOrgLevel(stickyParameter.ORGLEVEL)
                 == OrgLevel.School)
            {
                string qString =
                  ParamsHelper.GetQueryString(stickyParameter,
                   StickyParamsEnum.ORGLEVEL.ToString(),
                   "District");
                qString = ParamsHelper.ReplaceQueryString(qString,
                    StickyParamsEnum.FULLKEY.ToString(),
                    BLWIBase.GetMaskedFullkey(StickyParameter.FULLKEY, OrgLevel.District));
                //qString = ParamsHelper.ReplaceQueryString(qString,
                //    "MR", "Yes");  //"MR" is for page re-driected
                Response.Redirect("~/APTestsPage.aspx" + qString, true);
            }
            else
            {

                APTests = new BLAP_Tests();

                base.PrepBLEntity(APTests);

                //set the page heading
                ((SligoCS.Web.WI.WI)Page.Master).SetPageHeading(
                    "How did students perform on college admissions and placement tests?");

                _ds = APTests.GetAPTestData();

                CheckSelectedSchoolOrDistrict(APTests);
                SetLinkChangeSelectedSchoolOrDistrict(
                    APTests, ChangeSelectedSchoolOrDistrict);

                SetVisibleColumns2(SligoDataGrid2, _ds, APTests.ViewBy,
                    APTests.OrgLevel, APTests.CompareTo, APTests.SchoolType);
                StickyParameter.SQL = APTests.SQL;

                this.SligoDataGrid2.DataSource = _ds;
                this.SligoDataGrid2.DataBind();

                string title = "Advanced Placement Program Exams - All Subjects" +
                        base.GetViewByInTitle(APTests.ViewBy) +
                        base.GetOrgName(APTests.OrgLevel) + "<br/>" + 
                        base.GetYearRangeInTitle(APTests.Years) + 
                        base.GetCompareToInTitle(
                            APTests.OrgLevel, APTests.CompareTo,
                            APTests.SchoolType, APTests.S4orALL, 
                            GetRegionString());

                this.SligoDataGrid2.AddSuperHeader(title);

                set_state();
                setBottomLink(APTests);

                //Notes for graph
                SetUpChart(title);
            }
        }

        /// <summary>
        ///  Set up graph 
        /// </summary>
        /// <param name="ds"></param>
        private void SetUpChart(string graphTitle)
        {
            try
            {
                graphTitle = graphTitle.Replace("<br/>", "\n");
                barChart.Title = graphTitle;


                if (APTests.ViewBy == ViewByGroup.AllStudentsFAY)
                {
                    ArrayList friendlyAxisXName = new ArrayList();
                    friendlyAxisXName.Add("All Students");
                    barChart.FriendlyAxisXName = friendlyAxisXName;
                }

                barChart.AxisYMin = 0;
                barChart.AxisYMax = 100;
                barChart.AxisYStep = 10;
                ArrayList axisYName = new ArrayList();
                for (int i = 0; i < 11; i++)
                {
                    axisYName.Add(Convert.ToString(i * 10));
                }
                barChart.FriendlyAxisYName = axisYName;

                barChart.AxisYDescription = "% of All Exams Passed";

                //Bind Data Source & Display
                barChart.BLBase = APTests;
                barChart.DisplayColumnName = "% of Exams Passed";
                barChart.RawDataSource = _ds.Tables[0];
                barChart.DataBind();

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.Mixed_Header_Graphics1, true);
        }


        private void setBottomLink(BLWIBase baseBL)
        {
            BottomLinkViewReport1.Year = baseBL.CurrentYear.ToString();
            BottomLinkViewReport1.DistrictCd = GetDistrictCdByFullKey();
            BottomLinkViewReport1.SchoolCd = GetSchoolCdByFullKey();
            BottomLinkViewProfile1.DistrictCd = GetDistrictCdByFullKey();
        }

        //protected void Page_PreRender(object sender, EventArgs e)
        //{
        //    //call this (a second time) to change CompareTo : Selected School to CompareTo: Selected Districts.
        //    ParamsLinkBox1.SetDisplayTextForLinks(APTests.OrgLevel);
        //}


        /// <summary>
        /// Overrid PrepBLEntity, such that org level cannot be school level.
        /// </summary>
        /// <param name="entity"></param>
        public override void PrepBLEntity(BLWIBase entity)
        {
            base.PrepBLEntity(entity);

            if (entity.OrgLevel == OrgLevel.School)
            {
                //For AP page:  school level data are not available.  Use District instead.
                entity.OrigFullKey = BLWIBase.GetMaskedFullkey(entity.OrigFullKey, OrgLevel.District);
                entity.OrgLevel = OrgLevel.District;
                this.StickyParameter.ORGLEVEL = OrgLevel.District.ToString();
            }
        }


        public override List<string> GetVisibleColumns(ViewByGroup viewBy, OrgLevel orgLevel, CompareTo compareTo, SchoolType schoolType)
        {
            List<string> retval = 
                base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);

            //if (compareTo == CompareTo.PRIORYEARS)
            //    retval.Add(_ds._v_AP_TESTS.PriorYearColumn.ColumnName);
            //else
            //    retval.Add(_ds._v_AP_TESTS.DistStateColumn.ColumnName);

            //if (viewBy == ViewByGroup.RaceEthnicity)
            //    retval.Add(_ds._v_AP_TESTS.RaceLabelColumn.ColumnName);

            if ((compareTo == CompareTo.SELSCHOOLS) || (compareTo == CompareTo.CURRENTONLY))
            {
                //When user selects "Compare To = Selected Schools" the first column in the grid
                //should show the school name, but with no header.                
                retval.Add(_ds._v_AP_TESTS.LinkedDistrictNameColumn.ColumnName);
            }

            retval.Add(_ds._v_AP_TESTS.enrollmentColumn.ColumnName);
            retval.Add(_ds._v_AP_TESTS.___Taking_ExamsColumn.ColumnName);
            retval.Add(_ds._v_AP_TESTS.____Taking_ExamsColumn.ColumnName);
            retval.Add(_ds._v_AP_TESTS.___Exams_TakenColumn.ColumnName);
            retval.Add(_ds._v_AP_TESTS.___Exams_PassedColumn.ColumnName);
            retval.Add(_ds._v_AP_TESTS.___of_Exams_PassedColumn.ColumnName);
            return retval;
        }


        protected void SligoDataGrid2_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int colIndex = SligoDataGrid2.FindBoundColumnIndex(
                    _ds._v_AP_TESTS.LinkedDistrictNameColumn.ColumnName);
                if (colIndex > -1)
                {
                    e.Row.Cells[colIndex].Text =
                        Server.HtmlDecode(e.Row.Cells[colIndex].Text);
                }
                ;
                ////format the numerical values
                SligoDataGrid2.SetCellToFormattedDecimal(e.Row,
                    _ds._v_AP_TESTS.enrollmentColumn.ColumnName,
                    Constants.FORMAT_RATE_0_NO_DOT);

                SligoDataGrid2.SetCellToFormattedDecimal(e.Row,
                    _ds._v_AP_TESTS.___Taking_ExamsColumn.ColumnName,
                    Constants.FORMAT_RATE_0_NO_DOT);

                SligoDataGrid2.SetCellToFormattedDecimal(e.Row,
                    _ds._v_AP_TESTS.___Exams_TakenColumn.ColumnName,
                    Constants.FORMAT_RATE_0_NO_DOT);
                
                SligoDataGrid2.SetCellToFormattedDecimal(e.Row,
                   _ds._v_AP_TESTS.___Exams_PassedColumn.ColumnName,
                   Constants.FORMAT_RATE_0_NO_DOT);
               

                 SligoDataGrid2.SetCellToFormattedDecimal(e.Row, 
                    _ds._v_AP_TESTS.____Taking_ExamsColumn.ColumnName, 
                    Constants.FORMAT_RATE_01_PERC);

                SligoDataGrid2.SetCellToFormattedDecimal(e.Row, 
                    _ds._v_AP_TESTS.___of_Exams_PassedColumn.ColumnName, 
                    Constants.FORMAT_RATE_01_PERC);

                SetOrgLevelRowLabels(APTests, SligoDataGrid2, e.Row);
            }
        }


        public override DataSet GetDataSet()
        {
            return _ds;
        }

    }
}
