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
using SligoCS.BL.WI;
using SligoCS.Web.WI.PageBase.WI;
using SligoCS.Web.WI.HelperClasses;
using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.Web.WI
{

    /// <summary>
    /// This page was the initial demonstration of the SligoDataGrid.
    /// This page is also known as the Attendance page.
    /// </summary>
    public partial class PostGradIntentPage: PageBaseWI
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public PostGradIntentPage()
        {
            this.StickyParameter = new StickyParameter();
        }

        protected v_POST_GRAD_INTENT _ds = new v_POST_GRAD_INTENT();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Master.EnableViewState = false;

            BLPOST_GRAD_INTENT grad = new BLPOST_GRAD_INTENT();

            base.PrepBLEntity(grad);

            //set the page heading
            ((SligoCS.Web.WI.WI)Page.Master).SetPageHeading("What are students’ postgraduation plans?");

            _ds = grad.GetPostGradIntent();
            SetVisibleColumns2(SligoDataGrid2, _ds, grad.ViewBy, grad.OrgLevel, grad.CompareTo, grad.SchoolType);
            StickyParameter.SQL = grad.SQL;

            this.SligoDataGrid2.DataSource = _ds;
            this.SligoDataGrid2.DataBind();
            this.SligoDataGrid2.AddSuperHeader( base.GetTitle("Postgraduation Plans", grad));
            this.SligoDataGrid2.ShowSuperHeader = true;

            set_state();
        }

        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.dataLinksPanel, true);
        }

        public override List<string> GetVisibleColumns(ViewByGroup viewBy, OrgLevel orgLevel, CompareTo compareTo, SchoolType schoolType)
        {
            StickyParameter.PostGradPlans plans = StickyParameter.GetPostGradPlan(StickyParameter.PLAN);
            //StickyParameter.PostGradPlans plans = StickyParameter.PostGradPlans.All;
            List<String> retval = GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType, plans);
            return retval;
        }

        /// <summary>
        /// Overload to include 'Show' stickyParameter.
        /// </summary>
        /// <param name="viewBy"></param>
        /// <param name="orgLevel"></param>
        /// <param name="compareTo"></param>
        /// <param name="schoolType"></param>
        /// <returns></returns>
        public List<string> GetVisibleColumns(ViewByGroup viewBy, OrgLevel orgLevel, CompareTo compareTo, SchoolType schoolType, StickyParameter.PostGradPlans postGradPlan)
        {
            List<string> retval = new List<string>();// base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);
            if (compareTo == CompareTo.PRIORYEARS)
                retval.Add(_ds._v_POST_GRAD_INTENT.PriorYearColumn.ColumnName);

            retval.Add(_ds._v_POST_GRAD_INTENT.Number_of_GraduatesColumn.ColumnName);

            if (viewBy == ViewByGroup.Gender)
                retval.Add(_ds._v_POST_GRAD_INTENT.SexDescColumn.ColumnName);
            else if (viewBy == ViewByGroup.RaceEthnicity)
                retval.Add(_ds._v_POST_GRAD_INTENT.RaceDescColumn.ColumnName);

            if (compareTo == CompareTo.DISTSTATE)
                retval.Add(_ds._v_POST_GRAD_INTENT.DistStateColumn.ColumnName);

            if (postGradPlan == StickyParameter.PostGradPlans.All)
            {
                retval.Add(_ds._v_POST_GRAD_INTENT.___4_Year_CollegeColumn.ColumnName);
                retval.Add(_ds._v_POST_GRAD_INTENT.___Voc_Tech_CollegeColumn.ColumnName);
                retval.Add(_ds._v_POST_GRAD_INTENT.___EmploymentColumn.ColumnName);
                retval.Add(_ds._v_POST_GRAD_INTENT.___MilitaryColumn.ColumnName);
                retval.Add(_ds._v_POST_GRAD_INTENT.___Job_TrainingColumn.ColumnName);
                retval.Add(_ds._v_POST_GRAD_INTENT.___MiscellaneousColumn.ColumnName);
            }
            else if (postGradPlan == StickyParameter.PostGradPlans.FourYear)
            {
                retval.Add(_ds._v_POST_GRAD_INTENT._Number_4_Year_CollegeColumn.ColumnName);
                retval.Add(_ds._v_POST_GRAD_INTENT.___4_Year_CollegeColumn.ColumnName);
            }
            return retval;
        }


        protected void SligoDataGrid2_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //decode the link to specific schools, so that it appears as a normal URL link.
                //int colIndex = SligoDataGrid2.FindBoundColumnIndex(_ds._v_POST_GRAD_INTENT.LinkedNameColumn.ColumnName);
                //e.Row.Cells[colIndex].Text = Server.HtmlDecode(e.Row.Cells[colIndex].Text);


                ////format the numerical values
                //SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _ds._v_POST_GRAD_INTENT._Enrollment_PreK_12Column.ColumnName, Constants.FORMAT_RATE_03);
                //SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _ds._v_POST_GRAD_INTENT.Actual_Days_Of_AttendanceColumn.ColumnName, Constants.FORMAT_RATE_03);
                //SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _ds._v_POST_GRAD_INTENT.Possible_Days_Of_AttendanceColumn.ColumnName, Constants.FORMAT_RATE_03);
                //SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _ds._v_POST_GRAD_INTENT.Attendance_RateColumn.ColumnName, Constants.FORMAT_RATE_01_PERC);


                //FormatHelper formater = new FormatHelper();
                //formater.SetRaceAbbr(SligoDataGrid2, e.Row, _ds._v_POST_GRAD_INTENT.RaceLabelColumn.ToString());

            }
        }


        public override DataSet GetDataSet()
        {
            return _ds;
        }

    }
}
