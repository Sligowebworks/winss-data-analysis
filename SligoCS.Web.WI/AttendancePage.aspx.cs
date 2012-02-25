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
using SligoCS.BL.WI;
using SligoCS.Web.Base.PageBase.WI;

using SligoCS.Web.WI.WebSupportingClasses.WI;
using SligoCS.Web.WI.WebUserControls;

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
        protected override ChartFX.WebForms.Chart InitGraph()
        {
            return barChart;
        }
        protected override NavViewByGroup InitNavRowGroups()
        {
            nlrVwBy.LinksEnabled = NavViewByGroup.EnableLinksVector.EconElp;

            return nlrVwBy;
        }
        protected override string SetPageHeading()
        {
            return "What percent of students attend school each day?";
        }
        protected override void OnInitComplete(EventArgs e)
        {            
            GlobalValues.Grade.Key = GradeKeys.Combined_PreK_12;
            GlobalValues.CurrentYear = 2010;

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

            //Don't show combined groups at District Level, until support is added in the data import.
            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.District
                && QueryMarshaller.RaceDisagCodes.Contains((int)QueryMarshaller.RaceCodes.Comb))
                QueryMarshaller.RaceDisagCodes.Remove((int)QueryMarshaller.RaceCodes.Comb);

            base.OnInitComplete(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            set_visible();
            DataSetTitle = GetTitle("Attendance Rate");
            AttendanceDataGrid.AddSuperHeader(DataSetTitle);

            SetUpChart(_ds);
        }
        private void SetUpChart(DataSet _ds)
        {
            try
            {
                barChart.AxisY.LabelsFormat.Decimals = 0;
                barChart.AxisY.DataFormat.Decimals = 1;
                barChart.AxisY.LabelsFormat.Format = ChartFX.WebForms.AxisFormat.Percentage;
                barChart.AxisY.DataFormat.Format = ChartFX.WebForms.AxisFormat.Percentage;
                barChart.AxisY.ScaleUnit = 100;
                barChart.AxisY.Step = 10;

                //barChart.LegendBox.ContentLayout = ChartFX.WebForms.ContentLayout.Center;
                //barChart.LegendBox.PlotAreaOnly = false;

                //if (GlobalValues.STYP.Key == STYPKeys.Elem)
                 if (GlobalValues.Group.Key == GroupKeys.All)
                {
                    List<String> friendlyAxisXName = new List<String>();
                    friendlyAxisXName.Add("All Students");
                    barChart.FriendlyAxisXNames = friendlyAxisXName;
                }
                barChart.Title = DataSetTitle;
                barChart.AxisYDescription = "Attendance Rate";
                barChart.DisplayColumnName = "Attendance Rate";
                //barChart.Data[0, i] = Convert.ToDouble(_ds.Tables[0].Rows[i][v_HSCWWoDisSchoolDistStateEconELP.Regular_Diplomas].ToString());
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

        public override List<string> GetVisibleColumns()
        {
            List<string> retval =  base.GetVisibleColumns();
            retval.Add("Enrollment PreK-12");
            retval.Add("Actual Days Of Attendance");
            retval.Add("Possible Days Of Attendance");
            retval.Add("Attendance Rate");

            return retval;
        }
    }
}
