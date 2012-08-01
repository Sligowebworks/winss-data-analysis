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

namespace SligoCS.Web.WI
{
    public partial class PostGradIntentPage: PageBaseWI
    {
        protected override void OnInitComplete(EventArgs e)
        {
            GlobalValues.TrendStartYear = 1997;
            GlobalValues.CurrentYear = 2011;

            //STYP not supported
            GlobalValues.OverrideSchoolTypeWhenOrgLevelIsSchool_Complete += PageBaseWI.DisableSchoolType;

            //Don't show combined groups at District Level, until support is added in the data import.
            if ((GlobalValues.OrgLevel.Key == OrgLevelKeys.District || GlobalValues.SuperDownload.Key == SupDwnldKeys.True)
                && QueryMarshaller.RaceDisagCodes.Contains((int) QueryMarshaller.RaceCodes.Comb))
                QueryMarshaller.RaceDisagCodes.Remove((int)QueryMarshaller.RaceCodes.Comb);

            base.OnInitComplete(e);
        }
        protected override DALWIBase InitDatabase()
        {
            return new DALPOST_GRAD_INTENT();
        }
        protected override ChartFX.WebForms.Chart InitGraph()
        {
            if (GlobalValues.PostGradPlan.Key == PostGradPlanKeys.All)
            {
                barChart.Visible = false;
                return horizChart;
            }
            else
            {
                horizChart.Visible = false;
                return barChart;
            }
        }
        protected override GridView InitDataGrid()
        {
            return PostGradDataGrid;
        }
        protected override string SetPageHeading()
        {
            return "What are students’ postgraduation plans?";
        }
        protected override SligoCS.Web.WI.WebUserControls.NavViewByGroup InitNavRowGroups()
        {
            nlrVwBy.LinksEnabled = SligoCS.Web.WI.WebUserControls.NavViewByGroup.EnableLinksVector.Race;
            return nlrVwBy;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSetTitle = "Postgraduation Plans - " + GlobalValues.PostGradPlan.Key;
            DataSetTitle = GetTitleForSchoolTypeUnsupported(DataSetTitle);

            PostGradDataGrid.AddSuperHeader(DataSetTitle);

            if (GlobalValues.PostGradPlan.Key == PostGradPlanKeys.All)
            {
                SetUpHorizChart();
            }
            else
            {
                SetUpBarChart();
            }

            if (GlobalValues.Group.Key == GroupKeys.Race)
            {
                List<String> grOrder = new List<string>(QueryMarshaller.BuildOrderByList(DataSet.Tables[0].Columns));
                
                if (GlobalValues.CompareTo.Key == CompareToKeys.Years)
                { //so race codes that vary across years can be sorted together
                    grOrder.Insert(0, v_POST_GRAD_INTENT.RaceLabel);
                }
                else
                {
                    grOrder.Insert(0, v_POST_GRAD_INTENT.Race);
                }
                
                horizChart.OrderBy = String.Join(",", grOrder.ToArray());
            }

            set_state();
        }

        private void SetUpBarChart()
        {
            barChart.Title = DataSetTitle;

            barChart.AxisYDescription = "Percent of Students";

            if (GlobalValues.Group.Key == GroupKeys.All)
            {
                barChart.FriendlyAxisXNames = new List<String>(new String[] {"All Students"});
            }

            if (GlobalValues.PostGradPlan.Key == PostGradPlanKeys.FourYr)
            {
                barChart.DisplayColumnName = v_POST_GRAD_INTENT.PRC_4Year_College;
            }
            else if (GlobalValues.PostGradPlan.Key == PostGradPlanKeys.Employment)
            {
                barChart.DisplayColumnName = v_POST_GRAD_INTENT.PRC_Employment;
            }
            else if (GlobalValues.PostGradPlan.Key == PostGradPlanKeys.Military)
            {
                barChart.DisplayColumnName = v_POST_GRAD_INTENT.PRC_Military;
            }
            else if (GlobalValues.PostGradPlan.Key == PostGradPlanKeys.NoResponse)
            {
                barChart.DisplayColumnName = v_POST_GRAD_INTENT.PRC_No_Response;
            }
            else if (GlobalValues.PostGradPlan.Key == PostGradPlanKeys.Other)
            {
                barChart.DisplayColumnName = v_POST_GRAD_INTENT.PRC_Other_Plans;
            }
            else if (GlobalValues.PostGradPlan.Key == PostGradPlanKeys.SeekEmploy)
            {
                barChart.DisplayColumnName = v_POST_GRAD_INTENT.PRC_Seeking_Employment;
            }
            else if (GlobalValues.PostGradPlan.Key == PostGradPlanKeys.Training)
            {
                barChart.DisplayColumnName = v_POST_GRAD_INTENT.PRC_Job_Training;
            }
            else if (GlobalValues.PostGradPlan.Key == PostGradPlanKeys.Undecided)
            {
                barChart.DisplayColumnName = v_POST_GRAD_INTENT.PRC_Undecided;
            }
            else if (GlobalValues.PostGradPlan.Key == PostGradPlanKeys.VocTechCollege)
            {
                barChart.DisplayColumnName = v_POST_GRAD_INTENT.PRC_VocTech_College;
            }

            barChart.MaxRateInResult = WebUserControls.GraphBarChart.GetMaxRateInColumn(DataSet.Tables[0], barChart.DisplayColumnName);
        }
        private void SetUpHorizChart()
        {
            horizChart.Title = DataSetTitle;

            horizChart.SelectedSortBySecondarySort = false;

            horizChart.AxisY.Step = 10;
            horizChart.AxisY.Staggered = true;
            horizChart.AxisY.Max = 103;
            horizChart.YAxisSuffix = "\\%";

            if (GlobalValues.CompareTo.Key == CompareToKeys.Years)
            {
                horizChart.LabelColumns = new List<String>(new String[] 
                {
                    v_POST_GRAD_INTENT.YearFormatted.Trim()
                });
            }
            else if (GlobalValues.CompareTo.Key == CompareToKeys.OrgLevel)
            {
                horizChart.LabelColumns = new List<String>(new String[] 
                {
                    v_POST_GRAD_INTENT.OrgSchoolTypeLabel.Trim()
                });
            }
            else if (GlobalValues.CompareTo.Key == CompareToKeys.SelDistricts | GlobalValues.CompareTo.Key == CompareToKeys.SelSchools)
            {
                horizChart.LabelColumns = new List<String>(new String[] 
                {
                    v_POST_GRAD_INTENT.Name.Trim()
                });
            }
            else
            {
                horizChart.LabelColumns = new List<String>(new String[]
                    {
                        v_POST_GRAD_INTENT.SchoolTypeLabel.Trim()
                    });
            }

            if (GlobalValues.Group.Key != GroupKeys.All)
            {
                horizChart.SelectedSortBySecondarySort = true;

                // remove schooltype summary label when viewing CompareTo:Current
                if (GlobalValues.CompareTo.Key == CompareToKeys.Current)
                {
                    horizChart.SelectedSortBySecondarySort = false;
                    horizChart.LabelColumns.Clear();
                }

                if (GlobalValues.Group.Key == GroupKeys.Gender)
                    horizChart.LabelColumns.Insert(0, v_POST_GRAD_INTENT.SexLabel.Trim());

                if (GlobalValues.Group.Key == GroupKeys.Race)
                    horizChart.LabelColumns.Insert(0, v_POST_GRAD_INTENT.RaceShortLabel.Trim());
            }
            horizChart.MeasureColumns = new List<String>(new String[]
                {
                    v_POST_GRAD_INTENT.PRC_4Year_College,
                    v_POST_GRAD_INTENT.PRC_VocTech_College,
                    v_POST_GRAD_INTENT.PRC_Employment,
                    v_POST_GRAD_INTENT.PRC_Military,
                    v_POST_GRAD_INTENT.PRC_Job_Training,
                    v_POST_GRAD_INTENT.PRC_Miscellaneous
                });

            horizChart.AxisYDescription = "Percent of Students";
        }

        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.dataLinksPanel, true);
        }

        public override List<string> GetVisibleColumns()
        {
            List<string> cols = base.GetVisibleColumns();

            cols.Add(v_POST_GRAD_INTENT.Number_of_Graduates);

            if (GlobalValues.PostGradPlan.Key == PostGradPlanKeys.All)
            {
                cols.Add(v_POST_GRAD_INTENT.PRC_4Year_College);
                cols.Add(v_POST_GRAD_INTENT.PRC_VocTech_College);
                cols.Add(v_POST_GRAD_INTENT.PRC_Employment);
                cols.Add(v_POST_GRAD_INTENT.PRC_Military);
                cols.Add(v_POST_GRAD_INTENT.PRC_Job_Training);
                cols.Add(v_POST_GRAD_INTENT.PRC_Miscellaneous);
            }
            else if (GlobalValues.PostGradPlan.Key == PostGradPlanKeys.FourYr)
            {
                cols.Add(v_POST_GRAD_INTENT.Number_4Year_College);
                cols.Add(v_POST_GRAD_INTENT.PRC_4Year_College);
            }
            else if (GlobalValues.PostGradPlan.Key == PostGradPlanKeys.Employment)
            {
                cols.Add(v_POST_GRAD_INTENT.Number_Employment);
                cols.Add(v_POST_GRAD_INTENT.PRC_Employment);
            }
            else if (GlobalValues.PostGradPlan.Key == PostGradPlanKeys.Military)
            {
                cols.Add(v_POST_GRAD_INTENT.Number_Military);
                cols.Add(v_POST_GRAD_INTENT.PRC_Military);
            }
            else if (GlobalValues.PostGradPlan.Key == PostGradPlanKeys.NoResponse)
            {
                cols.Add(v_POST_GRAD_INTENT.Number_No_Response);
                cols.Add(v_POST_GRAD_INTENT.PRC_No_Response);
            }
            else if (GlobalValues.PostGradPlan.Key == PostGradPlanKeys.Other)
            {
                cols.Add(v_POST_GRAD_INTENT.Number_Other_Plans);
                cols.Add(v_POST_GRAD_INTENT.PRC_Other_Plans);
            }
            else if (GlobalValues.PostGradPlan.Key == PostGradPlanKeys.SeekEmploy)
            {
                cols.Add(v_POST_GRAD_INTENT.Number_Seeking_Employment);
                cols.Add(v_POST_GRAD_INTENT.PRC_Seeking_Employment);
            }
            else if (GlobalValues.PostGradPlan.Key == PostGradPlanKeys.Training)
            {
                cols.Add(v_POST_GRAD_INTENT.Number_Job_Training);
                cols.Add(v_POST_GRAD_INTENT.PRC_Job_Training);
            }
            else if (GlobalValues.PostGradPlan.Key == PostGradPlanKeys.Undecided)
            {
                cols.Add(v_POST_GRAD_INTENT.Number_Undecided);
                cols.Add(v_POST_GRAD_INTENT.PRC_Undecided);
            }
            else if (GlobalValues.PostGradPlan.Key == PostGradPlanKeys.VocTechCollege)
            {
                cols.Add(v_POST_GRAD_INTENT.Number_VocTech_College);
                cols.Add(v_POST_GRAD_INTENT.PRC_VocTech_College);
            }

            return cols;
        }
        public override List<string> GetDownloadRawVisibleColumns()
        {
            List <String> cols = base.GetDownloadRawVisibleColumns();
            if (GlobalValues.PostGradPlan.Key == PostGradPlanKeys.All)
            {
                int index = cols.IndexOf(v_POST_GRAD_INTENT.PRC_4Year_College);
                cols.Insert(index, v_POST_GRAD_INTENT.Number_4Year_College);
                index = cols.IndexOf(v_POST_GRAD_INTENT.PRC_VocTech_College);
                cols.Insert(index, v_POST_GRAD_INTENT.Number_VocTech_College);
                index = cols.IndexOf(v_POST_GRAD_INTENT.PRC_Employment);
                cols.Insert(index, v_POST_GRAD_INTENT.Number_Employment);
                index = cols.IndexOf(v_POST_GRAD_INTENT.PRC_Military);
                cols.Insert(index, v_POST_GRAD_INTENT.Number_Military);
                index = cols.IndexOf(v_POST_GRAD_INTENT.PRC_Job_Training);
                cols.Insert(index, v_POST_GRAD_INTENT.Number_Job_Training);

                index = cols.IndexOf(v_POST_GRAD_INTENT.PRC_Miscellaneous);
                cols.Insert(index, v_POST_GRAD_INTENT.Number_Miscellaneous);
                cols.Insert(index, v_POST_GRAD_INTENT.PRC_No_Response);
                cols.Insert(index, v_POST_GRAD_INTENT.Number_No_Response);
                cols.Insert(index, v_POST_GRAD_INTENT.PRC_Undecided);
                cols.Insert(index, v_POST_GRAD_INTENT.Number_Undecided);
                cols.Insert(index, v_POST_GRAD_INTENT.PRC_Other_Plans);
                cols.Insert(index, v_POST_GRAD_INTENT.Number_Other_Plans);
                cols.Insert(index, v_POST_GRAD_INTENT.PRC_Seeking_Employment);
                cols.Insert(index, v_POST_GRAD_INTENT.Number_Seeking_Employment);

            }
            return cols;
        }
    }
}
