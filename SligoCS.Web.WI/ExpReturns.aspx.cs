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
    public partial class ExpReturns : PageBaseWI
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
            GlobalValues.CurrentYear = 2013;

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
            DataSetTitle = GetTitleWithoutGroupForSchoolTypeUnsupported("Post Expulsion - Returns to School");

            PostExpulsionDataGrid.AddSuperHeader(DataSetTitle);

            set_state();

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
                       vExpulsionServicesAndReturns.Students_Who_Returned_to_School,
                        vExpulsionServicesAndReturns.Students_Who_Didnt_Return_to_School
                    }
                );
                barChart.AxisYDescription = "Number of Expelled Students\nEligible to Return to School";
            }
            catch (Exception ex)
            {
                if (GlobalValues.TraceLevels > 0) throw ex;
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        public override System.Collections.Generic.List<string> GetVisibleColumns()
        {
            List<string> cols = base.GetVisibleColumns();

            cols.Add(vExpulsionServicesAndReturns.Students_Eligible_to_Return_to_School);
            cols.Add(vExpulsionServicesAndReturns.Students_Who_Returned_to_School);
            cols.Add(vExpulsionServicesAndReturns.Students_Who_Didnt_Return_to_School);
            cols.Add(vExpulsionServicesAndReturns.PRC_of_Eligibles_Who_Return);

            return cols;
        }
        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(
                    WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(
                        WI.displayed_obj.dataLinksPanel, true);
        }
    }
}
