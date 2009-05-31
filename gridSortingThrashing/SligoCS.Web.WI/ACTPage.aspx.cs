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
using SligoCS.Web.WI.DAL.DataSets;
using SligoCS.BL.WI;
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.WI.HelperClasses;
using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.Web.WI
{

    /// <summary>
    /// This page was the initial demonstration of the SligoDataGrid.
    /// This page is also known as the Attendance page.
    /// </summary>
    public partial class ACTPage : PageBaseWI
    {

        protected v_ACT _ds = new v_ACT();
        protected BLACT _entity;
        
        protected override void  OnInitComplete(EventArgs e)
        {
 	         base.OnInitComplete(e);
          _entity = new BLACT();
            PrepBLEntity(_entity);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Master.EnableViewState = false;

            //set the page heading
            ((SligoCS.Web.WI.WI)Page.Master).SetPageHeading(
                "How did students perform on college admissions and placement tests?");

            _ds = _entity.GetACTData();

            CheckSelectedSchoolOrDistrict(_entity);
            SetLinkChangeSelectedSchoolOrDistrict(ChangeSelectedSchoolOrDistrict);

            SetVisibleColumns2(SligoDataGrid2, _ds, _entity.ViewBy,
                _entity.OrgLevel, _entity.CompareTo, GlobalValues.STYP);
            GlobalValues.SQL = _entity.SQL;

            this.SligoDataGrid2.DataSource = _ds;
            this.SligoDataGrid2.DataBind();

            string title = "ACT Results - " + GlobalValues.ACTSubj + " -" +
                TitleBuilder.GetViewByInTitle(_entity.ViewBy) +
                GlobalValues.GetOrgName() + "<br/>" +
                TitleBuilder.GetYearRangeInTitle(_entity.Years) +
                TitleBuilder.GetCompareToInTitle(
                    _entity.OrgLevel, _entity.CompareTo,
                    GlobalValues.STYP, _entity.S4orALL, 
                    GetRegionString());
           
            this.SligoDataGrid2.AddSuperHeader(title.Replace("- -", "-"));

            this.SligoDataGrid2.ShowSuperHeader = true;

            if (GlobalValues.Group.Key == GroupKeys.Race)
            {
                TextForRaceInBottomLink.Visible = true;
            }
            else
            {
                TextForRaceInBottomLink.Visible = false;
            }

            set_state();

            setBottomLink(_entity);

            //Notes for graph
            SetUpChart(title, GlobalValues.ACTSubj);
            
        }

        /// <summary>
        ///  Set up graph 
        /// </summary>
        /// <param name="ds"></param>
        private void SetUpChart(string graphTitle, ACTSubj subject)
        {
            try
            {
                graphTitle = graphTitle.Replace("<br/>", "\n");
                barChart.Title = graphTitle;


                if (_entity.ViewBy.Key == GroupKeys.All)
                {
                    ArrayList friendlyAxisXName = new ArrayList();
                    friendlyAxisXName.Add("All Students");
                    barChart.FriendlyAxisXName = friendlyAxisXName;
                }

                barChart.AxisYMin = 0;
                ArrayList axisYName = new ArrayList();
                axisYName.Add("0.0");
                if (GlobalValues.CompareTo.Key == CompareToKeys.SelSchools ||
                        GlobalValues.CompareTo.Key == CompareToKeys.SelDistricts)
                {
                    barChart.AxisYMax = 40;
                    barChart.AxisYStep = 4;
                    for (int i = 1; i < 11; i++)
                    {
                        axisYName.Add(Convert.ToString(i * 4) + ".0");
                    }
                }
                else
                {
                    barChart.AxisYMax = 39;
                    barChart.AxisYStep = 3;
                    for (int i = 1; i < 14; i++)
                    {
                        axisYName.Add(Convert.ToString(i * 3) + ".0");
                    }
                }
                barChart.FriendlyAxisYName = axisYName;

                barChart.AxisYDescription = "Average Score - " + subject;

                //Bind Data Source & Display
                //Subject name is the value column name 
                barChart.DisplayColumnName = subject;
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
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(
                WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(
                WI.displayed_obj.dataLinksPanel, true);
        }

        private void setBottomLink(BLWIBase baseBL)
        {
            BottomLinkViewReport1.Year = baseBL.CurrentYear.ToString();
            BottomLinkViewReport1.DistrictCd = GetDistrictCdByFullKey();
            BottomLinkViewReport1.SchoolCd = GetSchoolCdByFullKey();
            BottomLinkViewProfile1.DistrictCd = GetDistrictCdByFullKey();
        }

        public override List<string> GetVisibleColumns(
            Group viewBy, OrgLevel orgLevel, 
            CompareTo compareTo, STYP schoolType)
        {
            
            List<string> retval = GetVisibleColumns(
                viewBy, orgLevel, compareTo, schoolType, GlobalValues.ACTSubj);
            return retval;
        }

        public List<string> GetVisibleColumns(Group viewBy, 
            OrgLevel orgLevel, CompareTo compareTo, STYP schoolType, 
            ACTSubj  subject)
        {
            List<string> retval =
                base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);

            //retval.Add(_ds._v_ACT.PriorYearsColumn.ColumnName);

            //if (viewBy == ViewByGroup.RaceEthnicity)
            //{
            //    retval.Add(_ds._v_ACT.RaceLabelColumn.ColumnName);
            //}
            //else if (viewBy == ViewByGroup.Gender)
            //{
            //    retval.Add(_ds._v_ACT.SexLabelColumn.ColumnName);
            //}

            retval.Add(_ds._v_ACT.EnrollmentColumn.ColumnName);
            retval.Add(_ds._v_ACT.PupilCountColumn.ColumnName);
            retval.Add(_ds._v_ACT.Perc_TestedColumn.ColumnName);
            retval.Add(subject);

            return retval;
        }


        protected void SligoDataGrid2_RowDataBound(
            Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int colIndex = SligoDataGrid2.FindBoundColumnIndex(
                    _ds._v_ACT.LinkedNameColumn.ColumnName);
                if (colIndex > -1)
                {
                    e.Row.Cells[colIndex].Text =
                        Server.HtmlDecode(e.Row.Cells[colIndex].Text);
                }

                ////format the numerical values
                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row, _ds._v_ACT.Perc_TestedColumn.ColumnName, Constants.FORMAT_RATE_01_PERC);
                
                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row, _ds._v_ACT.ReadingColumn.ColumnName, Constants.FORMAT_RATE_01);
                
                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row, _ds._v_ACT.EnglishColumn.ColumnName, Constants.FORMAT_RATE_01);
                
                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row, _ds._v_ACT.MathColumn.ColumnName, Constants.FORMAT_RATE_01);
                
                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row, _ds._v_ACT.ScienceColumn.ColumnName, Constants.FORMAT_RATE_01);
                
                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row, _ds._v_ACT.CompositeColumn.ColumnName, Constants.FORMAT_RATE_01);

                SligoDataGrid2.SetOrgLevelRowLabels(GlobalValues, e.Row);

            }
        }
    }
}
