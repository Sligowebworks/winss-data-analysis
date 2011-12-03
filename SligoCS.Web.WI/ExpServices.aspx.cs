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
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.WI.WebSupportingClasses.WI;
using ChartFX.WebForms;

namespace SligoCS.Web.WI
{
    public partial class ExpServices : PageBaseWI
    {
        protected override void OnInitComplete(EventArgs e)
        {
            //Disable School Level
            if (UserValues.OrgLevel.Key == OrgLevelKeys.School)
            {
                GlobalValues.OrgLevel.Value = GlobalValues.OrgLevel.Range[OrgLevelKeys.District];
                pnlMessage.Visible = true;
            }

            GlobalValues.TrendStartYear = 2001;
            GlobalValues.LatestYear = 2010;

            //STYP not supported
            GlobalValues.OverrideSchoolTypeWhenOrgLevelIsSchool_Complete += PageBaseWI.DisableSchoolType;

            base.OnInitComplete(e);
        }
        protected override SligoCS.DAL.WI.DALWIBase InitDatabase()
        {
            return new DALExpulsionServicesAndReturns();
        }
        protected override GridView InitDataGrid()
        {
            return PostExpulsionDataGrid;
        }
        protected override Chart InitGraph()
        {
            return barChart;
        }
        protected override string SetPageHeading()
        {
            return "What happens after students are expelled?";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSetTitle = GetTitleWithoutGroupForSchoolTypeUnsupported("Post Expulsion - Post Expulsion Services");

            PostExpulsionDataGrid.AddSuperHeader(DataSetTitle);

            set_state();
            setBottomLink();

            SetUpChart(DataSetTitle);
        }
        private void SetUpChart(String graphTitle)
        {
            barChart.SelectedSortBySecondarySort = false;
            try
            {
                barChart.Title = graphTitle;
                if (GlobalValues.CompareTo.Key == CompareToKeys.Years)
                {
                    barChart.LabelColumns = new List<String>(new String[]
                        {
                            vExpulsionServicesAndReturns.YearFormatted
                        });
                }
                if (GlobalValues.CompareTo.Key == CompareToKeys.OrgLevel)
                {
                    barChart.LabelColumns = new List<String>(new String[]
                        {
                            vExpulsionServicesAndReturns.OrgLevelLabel
                        });
                }
                if (GlobalValues.CompareTo.Key == CompareToKeys.SelDistricts
                    || GlobalValues.CompareTo.Key == CompareToKeys.SelSchools)
                {
                    barChart.LabelColumns = new List<String>(new String[]
                        {
                            vExpulsionServicesAndReturns.Name
                        });
                }
                if (GlobalValues.CompareTo.Key == CompareToKeys.Current)
                {
                    barChart.LabelColumns = new List<String>(new String[]
                        {
                            vExpulsionServicesAndReturns.OrgLevelLabel
                        });
                }
                barChart.MeasureColumns = new List<String>(new String[]
                    {
                        vExpulsionServicesAndReturns.Total_NUM_Students_without_Disabilities_Offered_Post_Expulsion_Services,
                        vExpulsionServicesAndReturns.Total_NUM_Students_without_Disabilities_Not_Offered_Post_Expulsion_Services  
                    }
                );
                barChart.AxisYDescription = "Number of Expelled Students\nwithout Disabilities";
            }
            catch (Exception ex)
            {
                if (GlobalValues.TraceLevels > 0) throw ex;
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        public override System.Collections.Generic.List<string> GetVisibleColumns(SligoCS.Web.WI.WebSupportingClasses.WI.Group viewBy, SligoCS.Web.WI.WebSupportingClasses.WI.OrgLevel orgLevel, SligoCS.Web.WI.WebSupportingClasses.WI.CompareTo compareTo, SligoCS.Web.WI.WebSupportingClasses.WI.STYP schoolType)
        {
            List<string> cols = base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);

            cols.Add(vExpulsionServicesAndReturns.Total_NUM_Students_without_Disabilities_Expelled);
            cols.Add(vExpulsionServicesAndReturns.Total_NUM_Students_without_Disabilities_Offered_Post_Expulsion_Services);
            cols.Add(vExpulsionServicesAndReturns.Total_NUM_Students_without_Disabilities_Not_Offered_Post_Expulsion_Services);
            cols.Add(vExpulsionServicesAndReturns.PRC_of_Expelled_Students_without_Disabilities_Offered_Post_Expulsion_Services);

            return cols;
        }
        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(
                    WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(
                        WI.displayed_obj.dataLinksPanel, true);
        }
        private void setBottomLink()
        {
            BottomLinkViewProfile1.DistrictCd = GlobalValues.DistrictCode;
        }
    }
}
