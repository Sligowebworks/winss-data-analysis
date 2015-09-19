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

using ChartFX.WebForms;

namespace SligoCS.Web.WI
{
    public partial class MoneyPage : PageBaseWI
    {
        protected override DALWIBase InitDatabase()
        {
            if (GlobalValues.RevExp.Key == RevExpKeys.Revenue)
                return new DALRevenue();
            else
                return new DALExpenditure();
        }
        protected override GridView InitDataGrid()
        {
            if (GlobalValues.RevExp.Key == RevExpKeys.Revenue)
            {
                //ExpenditureDataGrid.Visible = false;
                return RevenueDataGrid;
            }
            else
            {
                //RevenueDataGrid.Visible = false;
                return ExpenditureDataGrid;
            }
        }
        protected override ChartFX.WebForms.Chart InitGraph()
        {
            return barChart;
        }
        protected override void OnInitComplete(EventArgs e)
        {
            GlobalValues.TrendStartYear = 1999;
            GlobalValues.CurrentYear = 2010;

            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.School)
            {
                GlobalValues.OrgLevel.Value = GlobalValues.OrgLevel.Range[OrgLevelKeys.District];
                pnlMessage.Visible = true;
            }

            //School-Type Not Supported
            GlobalValues.OverrideSchoolTypeWhenOrgLevelIsSchool_Complete += PageBaseWI.DisableSchoolType;
        
            base.OnInitComplete(e);
        }
        protected override string SetPageHeading()
        {
            return "How much money is received and spent in this district?";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (GlobalValues.RevExp.Key == RevExpKeys.Revenue)
                nlrType.Visible = false;

            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.School  
                    && GlobalValues.OrgLevel.Key == OrgLevelKeys.District)
            {
                this.DistrictDataProvided.Text = "School level data are not available. ";
            }
            else
            { 
                this.DistrictDataProvided.Text = string.Empty;
            }

            if (GlobalValues.RevExp.Key == RevExpKeys.Revenue)
            {
                DataSetTitle = GetTitleWithoutGroupForSchoolTypeUnsupported(GlobalValues.RevExp.Key);
            }
            else
            {
                DataSetTitle = GetTitleWithoutGroupForSchoolTypeUnsupported(GlobalValues.CT.Key + " Per Member ");
            }            

            ((SligoCS.Web.WI.WebSupportingClasses.WinssDataGrid)DataGrid).AddSuperHeader(DataSetTitle);

            SetupChart();

            setBottomLink();
            set_state();
        }
        private void SetupChart()
        {
            barChart.AxisYMin = 0;
            barChart.AxisYMax = 28000;
            barChart.AxisYStep = 2000;
            List<String> axisYName = new List<String>();
            axisYName.Add("$0");
            for (int i = 1; i < 14; i++)
            {
                axisYName.Add("$" + Convert.ToString(i * 2) + ",000");
            }
            barChart.FriendlyAxisYNames = axisYName;
            barChart.Type = SligoCS.Web.WI.WebUserControls.GraphBarChart.StackedType.Normal;

            if (GlobalValues.RevExp.Key == RevExpKeys.Revenue)
            {
                barChart.Title = DataSetTitle;
                barChart.AxisYDescription = "Revenue per Member";
                barChart.DisplayColumnName = v_Revenues_2.Revenue_Per_Member;
                if (GlobalValues.CompareTo.Key == CompareToKeys.Current)
                {
                    barChart.LabelColumnName = WebSupportingClasses.ColumnPicker.GetCompareToColumnName(GlobalValues).ToString();
                    barChart.FriendlyAxisXNames = new List<String>(new String[] { String.Empty });
                }
                else
                {
                    barChart.LabelColumnName = WebSupportingClasses.ColumnPicker.GetCompareToColumnName(GlobalValues).ToString();
                }
                barChart.SeriesColumnName = v_Revenues_2.Category;
            }
            else if (GlobalValues.RevExp.Key == RevExpKeys.Expenditure)
            {
                GraphPanel.Visible = true;
                barChart.Title = DataSetTitle;
                barChart.AxisYDescription = "Cost per Member";
                barChart.DisplayColumnName = v_Expend_2.Cost_Per_Member;
                barChart.LabelColumnName = WebSupportingClasses.ColumnPicker.GetCompareToColumnName(GlobalValues);
                if (GlobalValues.CompareTo.Key == CompareToKeys.Current)
                {
                    barChart.LabelColumnName = WebSupportingClasses.ColumnPicker.GetCompareToColumnName(GlobalValues).ToString();
                    barChart.FriendlyAxisXNames = new List<String>(new String[] { String.Empty });
                }
                else
                {
                    barChart.LabelColumnName = WebSupportingClasses.ColumnPicker.GetCompareToColumnName(GlobalValues);
                }
                barChart.SeriesColumnName = v_Expend_2.Category;
            }

        }

        public override void DataBindGraph(Chart graph, DataSet ds)
        {
            ds = RemoveTotalForGraph(ds);
            base.DataBindGraph(graph, ds);
        }
        private DataSet RemoveTotalForGraph(DataSet ds)
        {
            DataSet dsReturn = ds.Copy();
            dsReturn.Clear();

            string token =
                (GlobalValues.CT.Key != CTKeys.CurrentEducation || GlobalValues.RevExp.Key == RevExpKeys.Revenue) ?
                "Total" :
                "Current"
            ;

            string condition = "[" + v_StaffFull.Category + "] not like '%" + token + "%'";
            foreach (DataRow row in ds.Tables[0].Select(condition))
            {
                dsReturn.Tables[0].ImportRow(row);
            }

            return dsReturn;
        }    

        private void setBottomLink()
        {
            BottomLinkViewProfile1.DistrictCd = GlobalValues.DistrictCode;
        }
        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.dataLinksPanel, true);
        }

        public override List<string> GetVisibleColumns(Group viewBy, OrgLevel orgLevel, CompareTo compareTo, STYP schoolType)
        {
            List<string> retval = base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);

            if (GlobalValues.RevExp.Key == RevExpKeys.Revenue)
            {
                if (retval.Contains(v_Revenues_2.YearFormatted))
                {
                    retval.Remove(v_Revenues_2.YearFormatted);
                    retval.Add(v_Revenues_2.PriorYear);
                }
                if (retval.Contains(v_Revenues_2.OrgSchoolTypeLabel))
                {
                    retval.Remove(v_Revenues_2.OrgSchoolTypeLabel);
                    retval.Add(v_Revenues_2.OrgSchoolTypeLabelWithMembers);
                }
                retval.Add(v_Revenues_2.Category);
                retval.Add(v_Revenues_2.Revenue);
                retval.Add(v_Revenues_2.Revenue_Per_Member);
                retval.Add(v_Revenues_2.Percent_of_Total);
            }
            else if (GlobalValues.RevExp.Key == RevExpKeys.Expenditure)
            {
                if (retval.Contains(v_Expend_2.YearFormatted))
                {
                    retval.Remove(v_Expend_2.YearFormatted);
                    retval.Add(v_Expend_2.PriorYear);
                }
                if (retval.Contains(v_Expend_2.OrgSchoolTypeLabel))
                {
                    retval.Remove(v_Expend_2.OrgSchoolTypeLabel);
                    retval.Add(v_Expend_2.OrgSchoolTypeLabelWithMembers);
                }
                retval.Add(v_Expend_2.Category);
                retval.Add(v_Expend_2.Cost);
                retval.Add(v_Expend_2.Cost_Per_Member);
                retval.Add(v_Expend_2.Percent_of_Total);
            }
            return retval;
        }

        protected override List<string> GetDownloadRawVisibleColumns()
        {
            List <String> newLabels = base.GetDownloadRawVisibleColumns();
            if (GlobalValues.RevExp.Key == RevExpKeys.Revenue)
            {
                if (newLabels.Contains(v_Revenues_2.PriorYear))
                {
                    int index = newLabels.IndexOf(v_Revenues_2.Category);
                    newLabels.Remove(v_Revenues_2.PriorYear);
                    newLabels.Insert(index, v_Revenues_2.Members);
                }
                if (newLabels.Contains(v_Revenues_2.OrgSchoolTypeLabelWithMembers))
                {
                    int index = newLabels.IndexOf(v_Revenues_2.Category);
                    newLabels.Remove(v_Revenues_2.OrgSchoolTypeLabelWithMembers);
                    newLabels.Insert(index, v_Revenues_2.Members);
                }
            }
            else if (GlobalValues.RevExp.Key == RevExpKeys.Expenditure)
            {
                if (newLabels.Contains(v_Expend_2.PriorYear))
                {
                    int index = newLabels.IndexOf(v_Expend_2.Category);
                    newLabels.Remove(v_Expend_2.PriorYear);
                    newLabels.Insert(index, v_Expend_2.Members);
                }
                if (newLabels.Contains(v_Expend_2.OrgSchoolTypeLabelWithMembers))
                {
                    int index = newLabels.IndexOf(v_Expend_2.Category);
                    newLabels.Remove(v_Expend_2.OrgSchoolTypeLabelWithMembers);
                    newLabels.Insert(index, v_Expend_2.Members);
                }
            }            
            return newLabels;
        }

    }
}
