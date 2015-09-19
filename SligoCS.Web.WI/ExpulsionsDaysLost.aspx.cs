using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
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
    public partial class ExpulsionsDaysLost : PageBaseWI
    {
        protected global::SligoCS.Web.WI.WebSupportingClasses.WinssDataGrid ExpulsionsDataGrid;

        protected override ChartFX.WebForms.Chart InitGraph()
        {
            return barChart;
        }
        protected override DALWIBase InitDatabase()
        {
            return new DALExpulsionsDaysLost();
        }
        protected override GridView InitDataGrid()
        {
            return ExpulsionsDataGrid;
        }
        protected override void OnInitComplete(EventArgs e)
        {
            GlobalValues.LatestYear = 2010;

            if (GlobalValues.Group.Key == GroupKeys.Disability)
            {
                GlobalValues.TrendStartYear = 2005;
            }
            else
            {
                GlobalValues.TrendStartYear = 1999;
            }

            GlobalValues.Grade.Key = GradeKeys.Combined_PreK_12;

            //Don't show combined groups at District Level, until support is added in the data import.
            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.District
                && QueryMarshaller.RaceDisagCodes.Contains((int)QueryMarshaller.RaceCodes.Comb))
                QueryMarshaller.RaceDisagCodes.Remove((int)QueryMarshaller.RaceCodes.Comb);

            base.OnInitComplete(e);
        }
        protected override NavViewByGroup InitNavRowGroups()
        {
            nlrVwByGroup.LinksEnabled = NavViewByGroup.EnableLinksVector.Disability;
            return nlrVwByGroup;
        }
        protected override string SetPageHeading()
        {
            return "What percentage of days were lost due to suspensions and expulsions?";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSetTitle = GetTitle("Days Lost Due to Expulsions");

            ExpulsionsDataGrid.AddSuperHeader(DataSetTitle);

            set_state();
            setBottomLink();

            GraphPanel.Visible = false;
            if (CheckIfGraphPanelVisible(GraphPanel) == true)
            {
                SetUpChart(DataSet);
            }
        }

        /// <summary>
        ///  Set up graph 
        /// </summary>
        /// <param name="ds"></param>
        private void SetUpChart(DataSet ds)
        {
            try
            {
                barChart.Title = DataSetTitle;

                barChart.MaxRateInResult = GetMaxRateInResult(ds);

                if (GlobalValues.Group.Key == GroupKeys.All)
                {
                    List<String> friendlyAxisXName = new List<String>();
                    friendlyAxisXName.Add("");
                    barChart.FriendlyAxisXNames = friendlyAxisXName;
                }
                barChart.AxisYDescription = "Days Expelled as % of\nPossible Attendance Days";

                barChart.DisplayColumnName = v_Expulsions.Percent_of_Days_Expelled;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        private double GetMaxRateInResult (DataSet ds)
        {
            double maxRateInResult = 0;

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                try
                {
                    if (Convert.ToDouble(
                            row[v_Expulsions.Percent_of_Days_Expelled].ToString())  > maxRateInResult)
                    {
                        maxRateInResult = Convert.ToDouble
                                (row[v_Expulsions.Percent_of_Days_Expelled].ToString());
                    }
                }
                catch
                {
                    continue;
                }
            }
            return maxRateInResult;
        }
        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                (WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                (WI.displayed_obj.dataLinksPanel, true);
        }
        private void setBottomLink()
        {
            BottomLinkViewProfile1.DistrictCd = GlobalValues.DistrictCode;
        }
        public override System.Collections.Generic.List<string>
            GetVisibleColumns(Group viewBy, OrgLevel orgLevel, 
            CompareTo compareTo, STYP schoolType)
        {
            List<string> retval = base.GetVisibleColumns(
                        viewBy, orgLevel, compareTo, schoolType);

            retval.Add(v_Expulsions.Total_Enrollment_PreK12);
            retval.Add(v_Expulsions.Possible_Days_Attendance);
            retval.Add(v_Expulsions.Number_of_Days_Expelled);
            retval.Add(v_Expulsions.Percent_of_Days_Expelled);

            return retval;
        }
    }
}
