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

    public partial class Suspensions : PageBaseWI
    {
        
        protected override ChartFX.WebForms.Chart InitGraph()
        {
            return barChart;
        }
        protected override GridView InitDataGrid()
        {
            return SuspensionsDataGrid;
        }
        protected override DALWIBase InitDatabase()
        {
            return new DALSuspensions();
        }
        protected override void OnInitComplete(EventArgs e)
        {
            GlobalValues.Year = 2009;
            GlobalValues.Grade.Key = GradeKeys.Combined_PreK_12;
            if (GlobalValues.Group.Key == GroupKeys.Disability)
            {
                GlobalValues.TrendStartYear = 2003;
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

        protected override string SetPageHeading()
        {
            return "What percentage of students were suspended or expelled?";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSetTitle = GetTitle("Suspensions");

            SuspensionsDataGrid.AddSuperHeader(DataSetTitle);

            set_state();
            setBottomLink();

            SetUpChart(DataSet);
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
                    friendlyAxisXName.Add("All Students");
                    barChart.FriendlyAxisXNames = friendlyAxisXName;
                }
                barChart.AxisYDescription = "Percent of Students Enrolled";

                //Bind Data Source & Display
                barChart.DisplayColumnName = v_SuspensionsDis.Suspension_Percent;
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
                            row[v_SuspensionsDis.Suspension_Percent].ToString())
                                        > maxRateInResult)
                    {
                        maxRateInResult = Convert.ToDouble
                                (row[v_SuspensionsDis.Suspension_Percent].ToString());
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

            retval.Add(v_SuspensionsDis.Total_Enrollment_PreK12);
            retval.Add(v_SuspensionsDis.Number_of_Students_Suspended);
            retval.Add(v_SuspensionsDis.Suspension_Percent);
            return retval;
        }
    }
}
