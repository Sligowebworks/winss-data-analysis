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
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.WI.HelperClasses;
using SligoCS.Web.Base.WebSupportingClasses.WI;
using StickyParamsEnum =
        SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.QStringVar;

namespace SligoCS.Web.WI
{

    public partial class AttendancePage : PageBaseWI
    {
        protected v_AttendanceWWoDisSchoolDistState _ds = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Master.EnableViewState = false;
            
            set_visible();

            BLAttendance Attendance = new BLAttendance();
            base.PrepBLEntity(Attendance);

            //set the page heading
            ((SligoCS.Web.WI.WI)Page.Master).SetPageHeading("What percent of students attend school each day?");
            
            //Get the Data
            _ds = Attendance.GetAttendanceData();            
            SetVisibleColumns2(SligoDataGrid2, _ds, Attendance.ViewBy, Attendance.OrgLevel, Attendance.CompareTo, Attendance.SchoolType);
            StickyParameter.SQL = Attendance.SQL;

            //Initialize and DataBind the DataGrid
            SligoDataGrid2.DataSource = _ds;
            SligoDataGrid2.DataBind();
            SligoDataGrid2.AddSuperHeader( base.GetTitle("Attendance Rate", Attendance));
            SligoDataGrid2.ShowSuperHeader = true;

            //Initialize and DataBind the Graph
            GraphPanel.Visible = true;
            barChart.AxisYMin = 0;
            barChart.AxisYMax = 1000;
            barChart.AxisYStep = 100;
            ArrayList axisYName = new ArrayList();
            axisYName.Add("0");
            for (int i = 1; i < 11; i++)
            {
                axisYName.Add(Convert.ToString(i ) );
            }
            barChart.FriendlyAxisYName = axisYName;
            barChart.Type = StackedType.Normal;
            barChart.DataSource = _ds;
            barChart.Title = base.GetTitle("Attendance Rate", Attendance).Replace("<br/>", "\n");
            barChart.BLBase = Attendance;
            barChart.AxisYDescription = "Attendance Rate Percent of Students";
            barChart.AxisXDescription = "FIXME";
            //barChart.DisplayColumnName = _dsExp._v_Expend_2.Cost_Per_MemberColumn.ColumnName;
           // barChart.RawDataSource = RemoveTotalPreGraph(_dsExp).Tables[0];
            barChart.DataBind();

        }

        private void set_visible()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.dataLinksPanel, true);
        }
        
        public override List<string> GetVisibleColumns(ViewByGroup viewBy, OrgLevel orgLevel, CompareTo compareTo, SchoolType schoolType)
        {
            List<string> retval =  base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);
            retval.Add("Enrollment PreK-12");
            retval.Add("Actual Days Of Attendance");
            retval.Add("Possible Days Of Attendance");
            retval.Add("Attendance Rate");

            return retval;
        }


        protected void SligoDataGrid2_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //decode the link to specific schools, so that it appears as a normal URL link.
                int colIndex = SligoDataGrid2.FindBoundColumnIndex(_ds._v_AttendanceWWoDisSchoolDistState.LinkedNameColumn.ColumnName);
                e.Row.Cells[colIndex].Text = Server.HtmlDecode(e.Row.Cells[colIndex].Text);

                //format the numerical values
                SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _ds._v_AttendanceWWoDisSchoolDistState._Enrollment_PreK_12Column.ColumnName, Constants.FORMAT_RATE_03);
                SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _ds._v_AttendanceWWoDisSchoolDistState.Actual_Days_Of_AttendanceColumn.ColumnName, Constants.FORMAT_RATE_03);
                SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _ds._v_AttendanceWWoDisSchoolDistState.Possible_Days_Of_AttendanceColumn.ColumnName, Constants.FORMAT_RATE_03);
                SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _ds._v_AttendanceWWoDisSchoolDistState.Attendance_RateColumn.ColumnName, Constants.FORMAT_RATE_01_PERC);

                FormatHelper formater = new FormatHelper();
                formater.SetRaceAbbr(SligoDataGrid2, e.Row, _ds._v_AttendanceWWoDisSchoolDistState.RaceLabelColumn.ToString());
            }
        }

        public override DataSet GetDataSet()
        {
            return _ds;
        }

    }
}
