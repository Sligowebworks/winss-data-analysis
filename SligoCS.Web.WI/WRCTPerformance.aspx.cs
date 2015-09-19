using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SligoCS.BL.WI;
using SligoCS.DAL.WI;
using SligoCS.Web.Base.PageBase.WI;

using SligoCS.Web.WI.WebSupportingClasses.WI;

using ChartFX.WebForms;

namespace SligoCS.Web.WI
{
    public partial class WRCTPerformance : PageBaseWI
    {

        protected override DALWIBase InitDatabase()
        {
            return new DALWRCT();
        }
        protected override GridView InitDataGrid()
        {
            return WrctDataGrid;
        }
        protected override Chart InitGraph()
        {
            return barChart;
        }
        protected override string SetPageHeading()
        {
            return "How did students perform on the WRCT at grade 3? (Results available through March 2005)";
        }
        protected override void OnInitComplete(EventArgs e)
        {
            GlobalValues.CurrentYear = 2005;
            GlobalValues.TrendStartYear = 1998;

            GlobalValues.OverrideSchoolTypeWhenOrgLevelIsSchool_Complete += PageBaseWI.DisableSchoolType;

            GlobalValues.OverrideByNavLinksNotPresent(GlobalValues.Level, nlrLevel, LevelKeys.Advanced);

            //ViewByGroup Not Supported:
            GlobalValues.Group.Value = GlobalValues.Group.Range[GroupKeys.All];

            base.OnInitComplete(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            string title = "WRCT* - Grade 3 - Reading " + WebSupportingClasses.TitleBuilder.newline+ GlobalValues.Level.Key;
            DataSetTitle = GetTitleWithoutGroupForSchoolTypeUnsupported(title);
            DataSetTitle = DataSetTitle.Replace(
                TitleBuilder.GetYearRangeInTitle(new List<String> (new String[] {GlobalValues.Year.ToString()})),
                "March " + GlobalValues.Year.ToString() + " Results");

            WrctDataGrid.AddSuperHeader(DataSetTitle);

            List<String> order = QueryMarshaller.BuildOrderByList(DataSet.Tables[0].Columns);
            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.School)
            {
                order.Insert(0, v_WRCT.OrgSchoolTypeLabel);
            }
            WrctDataGrid.OrderBy = String.Join(",", order.ToArray());
            barChart.OrderBy = String.Join(",", order.ToArray());

            SetUpChart();

            set_state();
        }
        private void SetUpChart()
        {
            barChart.Title = DataSetTitle;
            barChart.FriendlyAxisXNames = new List<String>(new String[] { "All Students" });
            barChart.AxisYDescription = "Percent of All Students Enrolled";

            List<String> col = new List<String>();
            getMeasureColumn(col);
            barChart.DisplayColumnName = col[0];

            barChart.MaxRateInResult = WebUserControls.GraphBarChart.GetMaxRateInColumn(DataSet.Tables[0], barChart.DisplayColumnName);

            barChart.SeriesColumnName = WebSupportingClasses.ColumnPicker.GetCompareToColumnName(GlobalValues);
            barChart.LabelColumnName = WebSupportingClasses.ColumnPicker.GetViewByColumnName(GlobalValues);
        }
        public override List<string> GetVisibleColumns()
        {
            List<String> cols  = base.GetVisibleColumns();
            Level wrct = GlobalValues.Level;

            cols.Add(v_WRCT.Enrolled);

            getMeasureColumn(cols);

            return cols;
        }
        private void getMeasureColumn(List<string> cols)
        {
            Level wrct = GlobalValues.Level;

            if (wrct.Key == LevelKeys.Advanced)
                cols.Add(v_WRCT.Percent_Advanced);
            if (wrct.Key == LevelKeys.AdvancedProficient)
                cols.Add(v_WRCT.Advanced__Proficient_Total);
            if (wrct.Key == LevelKeys.AdvProfBas)
                cols.Add(v_WRCT.Advanced__Proficient__Basic_Total);
            if (wrct.Key == LevelKeys.MinimumPerf)
                cols.Add(v_WRCT.Percent_Minimal_Performance);
            if (wrct.Key == LevelKeys.MinPerfNotTested)
                cols.Add(v_WRCT.Minimal_Performance__Not_Tested_on_WRCT_Total);
            if (wrct.Key == LevelKeys.NotTested)
                cols.Add(v_WRCT.Percent_Not_Tested);
        }
        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                (WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                (WI.displayed_obj.dataLinksPanel, true);
        }
    }
}
