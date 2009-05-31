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
using StickyParamsEnum =
        SligoCS.Web.WI.WebSupportingClasses.WI.StickyParameter.QStringVar;

namespace SligoCS.Web.WI
{

    public partial class AttendancePage : PageBaseWI
    {
        //protected v_AttendanceWWoDisSchoolDistStateEconELP _ds = null;
        protected DataSet _ds;
        
        protected override DataSet InitDataSet()
        {
            //_ds = new v_AttendanceWWoDisSchoolDistStateEconELP();
            _ds = new DataSet();
            return _ds;
        }
        protected override DALWIBase InitDatabase()
        {
            return new DALAttendance();
        }
        protected override GridView InitDataGrid()
        {
            return AttendanceDataGrid;
        }
        protected override string SetPageHeading()
        {
            return "What percent of students attend school each day?";
        }
        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
            GlobalValues.Grade = 99;

            if (GlobalValues.Group.Key == GroupKeys.Disability)
            {
                GlobalValues.TrendStartYear = 2005;
            }
            else if (GlobalValues.Group.Key == GroupKeys.EngLangProf
                    || GlobalValues.Group.Key == GroupKeys.EconDisadv)
            {
                GlobalValues.TrendStartYear = 2005;
            }
            else
            {
                GlobalValues.TrendStartYear = 1997;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            set_visible();
            AttendanceDataGrid.AddSuperHeader(GetTitle("Attendance Rate"));
            AttendanceDataGrid.SetVisibleColumns(
                GetVisibleColumns(GlobalValues.Group,
                    GlobalValues.OrgLevel,
                    GlobalValues.CompareTo,
                    GlobalValues.STYP)
                );

            //Initialize and DataBind the Graph
            GraphPanel.Visible = false;
            if (CheckIfGraphPanelVisible(GraphPanel) == true)
            {
                SetUpChart(_ds);
            }
        }
        private void SetUpChart(DataSet _ds)
        {
            try
            {
                if (GlobalValues.Group.Key == GroupKeys.All)
                {
                    ArrayList friendlyAxisXName = new ArrayList();
                    friendlyAxisXName.Add("All Students");
                    barChart.FriendlyAxisXName = friendlyAxisXName;
                }
                barChart.Title = base.GetTitle("Attendance Rate").Replace("<br/>", "\n");
                barChart.AxisYDescription = "Attendance Rate Percent of Students";
                //barChart.DisplayColumnName = _ds._v_AttendanceWWoDisSchoolDistStateEconELP.Attendance_RateColumn.ColumnName;
                barChart.DisplayColumnName = "Attendace Rate";
                barChart.RawDataSource = _ds.Tables[0];
                barChart.DataBind();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void set_visible()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.dataLinksPanel, true);
        }

        public override List<string> GetVisibleColumns(Group viewBy, OrgLevel orgLevel, CompareTo compareTo, STYP schoolType)
        {
            List<string> retval =  base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);
            retval.Add("Enrollment PreK-12");
            retval.Add("Actual Days Of Attendance");
            retval.Add("Possible Days Of Attendance");
            retval.Add("Attendance Rate");

            return retval;
        }

        protected void AttendanceDataGrid_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            //decode the link to specific schools, so that it appears as a normal URL link.
            //int colIndex = AttendanceDataGrid.FindBoundColumnIndex(_ds._v_AttendanceWWoDisSchoolDistStateEconELP.LinkedNameColumn.ColumnName);
            int colIndex = AttendanceDataGrid.FindBoundColumnIndex("LinkedName");
            e.Row.Cells[colIndex].Text = Server.HtmlDecode(e.Row.Cells[colIndex].Text);

            //hopefully this is going to implemented in the database::
            FormatHelper formater = new FormatHelper();
            //formater.SetRaceAbbr(AttendanceDataGrid, e.Row, _ds._v_AttendanceWWoDisSchoolDistStateEconELP.RaceLabelColumn.ToString());
            formater.SetRaceAbbr(AttendanceDataGrid, e.Row, "Race Label");
        }
    }
}
