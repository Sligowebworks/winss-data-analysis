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
using SligoCS.Web.Base.WebServerControls.WI;

using SligoCS.Web.WI.WebSupportingClasses.WI;
using SligoCS.Web.WI.WebUserControls;
using ChartFX.WebForms;

namespace SligoCS.Web.WI
{

    /// <summary>
    /// This page was the initial demonstration of the SligoDataGrid.
    /// This page is also known as the Staff page.
    /// </summary>
    public partial class StaffPage : PageBaseWI
    {
        protected override void OnInitComplete(EventArgs e)
        {
            GlobalValues.TrendStartYear = 1997;
            GlobalValues.CurrentYear = 2011;

            //Disable School Level
            if (UserValues.OrgLevel.Key == OrgLevelKeys.School)
            {
                GlobalValues.OrgLevel.Value = GlobalValues.OrgLevel.Range[OrgLevelKeys.District];
                pnlMessage.Visible = true;
            }

            //School-Type Not Supported
            GlobalValues.OverrideSchoolTypeWhenOrgLevelIsSchool_Complete += PageBaseWI.DisableSchoolType;

            base.OnInitComplete(e);
        }
        protected override GridView InitDataGrid()
        {
            return StaffDataGrid;
        }
        protected override DALWIBase InitDatabase()
        {
            return new DALStaff();
        }
        protected override Chart InitGraph()
        {
            return barChart;
        }
        protected override string SetPageHeading()
        {
            return "What staff are available in this district?";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            List<String> order = 
                new List<String>(QueryMarshaller.BuildOrderByList(DataSet.Tables[0].Columns).ToArray());
            order.Add(v_StaffFull.Category);
            StaffDataGrid.OrderBy = barChart.OrderBy = String.Join(",", order.ToArray());

            DataSetTitle =
                (GlobalValues.StaffRatio.Key == StaffRatioKeys.Staff) ?
                v_StaffFull.FTE_Staff_per_100_Students :
                v_StaffFull.Ratio_of_Students_to_FTE_Staff
            ;
            DataSetTitle = GetTitleWithoutGroupForSchoolTypeUnsupported(DataSetTitle);

            StaffDataGrid.AddSuperHeader(DataSetTitle);


            if (GlobalValues.StaffRatio.Key == StaffRatioKeys.Staff)
                SetUpChart_Staff(DataSetTitle);
            else
                SetUpChart_Students(DataSetTitle);

            setBottomLink();
            set_state();
        }

        private void setBottomLink()
        {
            BottomLinkViewProfile1.DistrictCd = GlobalValues.DistrictCode;
        }

        private void SetUpChart_Students(String title)
        {
            barChart.Title = title;

            barChart.Type = SligoCS.Web.WI.WebUserControls.GraphBarChart.StackedType.No;

            barChart.AxisYDescription = "Students to FTE Staff";
            barChart.DisplayColumnName = v_StaffFull.Ratio_of_Students_to_FTE_Staff;
            barChart.SeriesColumnName = WebSupportingClasses.ColumnPicker.GetCompareToColumnName(GlobalValues);
            barChart.LabelColumnName = v_StaffFull.Category;

            //Customize Axis-Y settings
            barChart.AxisYMin = 0;
            barChart.AxisYStep = 30;
            barChart.AxisYMax = ((int)WebUserControls.GraphBarChart.GetMaxRateInColumn(DataSet.Tables[0], barChart.DisplayColumnName)) + 20;
        }
        private void SetUpChart_Staff(String title)
        {
            barChart.Title = title;
            barChart.Type = SligoCS.Web.WI.WebUserControls.GraphBarChart.StackedType.Normal;
            
            //Customize Axis-Y settings
            barChart.AxisYMin = 0;
            barChart.AxisYStep = 2;
            barChart.AxisYMax = ((int)WebUserControls.GraphBarChart.GetMaxRateInColumn(DataSet.Tables[0], barChart.DisplayColumnName)) + 20;

            barChart.AxisYDescription = "Staff per 100 Students";
            barChart.DisplayColumnName = v_StaffFull.FTE_Staff_per_100_Students;
            barChart.SeriesColumnName = v_StaffFull.Category; 
            barChart.LabelColumnName = WebSupportingClasses.ColumnPicker.GetCompareToColumnName(GlobalValues); 
        }
        public override void  DataBindGraph(Chart graph, DataSet ds)
        {

            if (GlobalValues.StaffRatio.Key == StaffRatioKeys.Staff)
                ds = RemoveTotalPreGraph(ds);
            base.DataBindGraph(graph, ds);
        }
        private DataSet RemoveTotalPreGraph(DataSet ds)
        {
            DataSet dsReturn = ds.Copy();
            dsReturn.Clear();
            
            string condition = "[" + v_StaffFull.Category + "] not like '%Total%'";
            foreach (DataRow row in ds.Tables[0].Select(condition))
            {
                dsReturn.Tables[0].ImportRow(row);
            }
            
            return dsReturn;
        }    

        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                (WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                (WI.displayed_obj.dataLinksPanel, true);
        }

        public override List<string> GetVisibleColumns()
        {
            List<string> retval = base.GetVisibleColumns();

            retval.Add(v_StaffFull.Category);
            retval.Add(v_StaffFull.Number_FTE_Staff);

            if (GlobalValues.StaffRatio.Key == StaffRatioKeys.Student)
                retval.Add(v_StaffFull.Ratio_of_Students_to_FTE_Staff);
            else
                retval.Add(v_StaffFull.FTE_Staff_per_100_Students);
            
            return retval;
        }

        public override List<string> GetDownloadRawVisibleColumns()
        {
            List<string> cols = base.GetDownloadRawVisibleColumns();
            int index = cols.IndexOf(v_StaffFull.Category);
            cols.Insert(index, v_StaffFull.enrollment);
            return cols;
        }

        protected override SortedList<string, string> GetDownloadRawColumnLabelMapping()
        {
            SortedList<string, string> newLabels  = base.GetDownloadRawColumnLabelMapping();
            newLabels.Add(v_StaffFull.Category, "staff_type");
            newLabels.Add(v_StaffFull.enrollment, "preK-12_enrollment");
            return newLabels;
        }
    }
}
