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
    public partial class SuspensionsDaysLost : PageBaseWI
    {

        protected override void OnInitComplete(EventArgs e)
        {
            GlobalValues.Year = 2010;
            GlobalValues.Grade.Key = GradeKeys.Combined_PreK_12;
            if (GlobalValues.Group.Key == GroupKeys.Disability)
            {
                GlobalValues.TrendStartYear = 2005;
            }
            else
            {
                GlobalValues.TrendStartYear = 1999;
            }
            base.OnInitComplete(e);
        }
        protected override NavViewByGroup InitNavRowGroups()
        {
            nlrVwByGroup.LinksEnabled = NavViewByGroup.EnableLinksVector.Disability;
            return nlrVwByGroup;
        }
        protected override DALWIBase InitDatabase()
        {
            return new DALSuspensionsDaysLost();
        }
        protected override ChartFX.WebForms.Chart InitGraph()
        {
            return barChart;
        }
        protected override GridView InitDataGrid()
        {
            return SuspensionsDataGrid;
        }
        protected override string SetPageHeading()
        {
            return "What percentage of days were lost due to suspensions and expulsions?";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSetTitle = GetTitle("Days Lost Due to Suspensions");

            SuspensionsDataGrid.AddSuperHeader(DataSetTitle);

            set_state();
            setBottomLink();

            GraphPanel.Visible = false;
            if (CheckIfGraphPanelVisible( GraphPanel) == true)
            {
                SetUpChart(DataSet);
            }
        }
        private void SetUpChart(DataSet ds)
        {
            try
            {
                barChart.Title = DataSetTitle;

                barChart.MaxRateInResult = GetMaxRateInResult(ds);

                if (GlobalValues.Group.Key == GroupKeys.All)
                {
                    List<String> friendlyAxisXName = new List<String>();
                    // blanks out "All Students" when ViewBy=All
                    friendlyAxisXName.Add("");
                    barChart.FriendlyAxisXNames = friendlyAxisXName;
                }
                barChart.AxisYDescription = "Days Suspended as % of\nPossible Attendance Days";

                barChart.DisplayColumnName = v_SuspensionsDaysLostSchoolDistState.Percent_of_Days_Suspended;
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
                            row[v_SuspensionsDaysLostSchoolDistState.Percent_of_Days_Suspended].ToString()) > maxRateInResult)
                    {
                        maxRateInResult = Convert.ToDouble(row[v_SuspensionsDaysLostSchoolDistState.Percent_of_Days_Suspended].ToString());
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

            retval.Add(v_SuspensionsDaysLostSchoolDistState.Total_Enrollment_PreK12);
            retval.Add(v_SuspensionsDaysLostSchoolDistState.Possible_Days_Attendance);
            retval.Add(v_SuspensionsDaysLostSchoolDistState.Number_of_Days_Suspended);
            retval.Add(v_SuspensionsDaysLostSchoolDistState.Percent_of_Days_Suspended);
            return retval;
        }
    }
}
