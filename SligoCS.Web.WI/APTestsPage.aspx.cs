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

    /// <summary>
    /// This page was the initial demonstration of the SligoDataGrid.
    /// This page is also known as the APTests page.
    /// </summary>
    public partial class APTestsPage : PageBaseWI
    {
        protected override DALWIBase InitDatabase()
        {
            return new DALAP_TESTS();
        }
        protected override ChartFX.WebForms.Chart InitGraph()
        {
            return barChart;
        }
        protected override GridView InitDataGrid()
        {
            return APTestsDataGrid;
        }
        protected override SligoCS.Web.WI.WebUserControls.NavViewByGroup InitNavRowGroups()
        {
            nlrVwByGroup.LinksEnabled = SligoCS.Web.WI.WebUserControls.NavViewByGroup.EnableLinksVector.Race;
            return nlrVwByGroup;
        }
        protected override void  OnInitComplete(EventArgs e)
        {
            GlobalValues.Grade.Key = GradeKeys.Grades_9_12_Combined;

            GlobalValues.TrendStartYear = 1997;
            GlobalValues.CurrentYear = 2011;

            if (UserValues.OrgLevel.Key == OrgLevelKeys.School) 
                GlobalValues.OrgLevel.Value = GlobalValues.OrgLevel.Range[OrgLevelKeys.District];

            //STYP not supported
            GlobalValues.OverrideSchoolTypeWhenOrgLevelIsSchool_Complete += PageBaseWI.DisableSchoolType;

            QueryMarshaller.RaceDisagCodes.Add((int)QueryMarshaller.RaceCodes.RaceEth_NA);
            if (QueryMarshaller.RaceDisagCodes.Contains((int)QueryMarshaller.RaceCodes.Comb))
                QueryMarshaller.RaceDisagCodes.Remove((int)QueryMarshaller.RaceCodes.Comb);

            base.OnInitComplete(e);
        }
       
        protected override string SetPageHeading()
        {
            return "How did students perform on college admissions and placement tests?";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
                DataSetTitle = GetTitleForSchoolTypeUnsupported("Advanced Placement Program Exams - All Subjects");

                APTestsDataGrid.AddSuperHeader(DataSetTitle);

                set_state();
                setBottomLink();

                //Notes for graph
                SetUpChart(DataSetTitle);
        }

        /// <summary>
        ///  Set up graph 
        /// </summary>
        /// <param name="ds"></param>
        private void SetUpChart(string graphTitle)
        {
            try
            {
                graphTitle = graphTitle.Replace("<br/>", "\n");
                barChart.Title = graphTitle;

                if (GlobalValues.Group.Key == GroupKeys.All)
                {
                    List<String> friendlyAxisXName = new List<String>();
                    friendlyAxisXName.Add("All Students");
                    barChart.FriendlyAxisXNames = friendlyAxisXName;
                }

                barChart.AxisYMin = 0;
                barChart.AxisYMax = 100;
                barChart.AxisYStep = 10;
                List<String> axisYName = new List<String>();
                for (int i = 0; i < 11; i++)
                {
                    axisYName.Add(Convert.ToString(i * 10) + "%");
                }
                barChart.FriendlyAxisYNames = axisYName;

                barChart.AxisYDescription = "% of All Exams Scores 3 or Above";

                //Bind Data Source & Display
                barChart.DisplayColumnName = v_AP_TESTS.PRC_of_Exams_Passed;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.dataLinksPanel, true);

        }

        private void setBottomLink()
        {
            BottomLinkViewProfile1.DistrictCd = GlobalValues.DistrictCode;
        }

        public override List<string> GetVisibleColumns()
        {
            List<string> retval = 
                base.GetVisibleColumns();
            //page doesn't support school level
           /* if ((compareTo.Key == CompareToKeys.SelDistricts) || (compareTo.Key == CompareToKeys.Current))
            {
                //When user selects "Compare To = Selected Schools" the first column in the grid
                retval.Add(v_AP_TESTS.LinkedDistrictName);
            }*/

            retval.Add(v_AP_TESTS.enrollment);
            retval.Add(v_AP_TESTS.NUM_Taking_Exams);
            retval.Add(v_AP_TESTS.PRC_Taking_Exams);
            retval.Add(v_AP_TESTS.NUM_Exams_Taken);
            retval.Add(v_AP_TESTS.PRC_of_Exams_Passed);
            retval.Add(v_AP_TESTS.NUM_Exams_Passed);
            return retval;
        }

        protected override SortedList<string, string> GetDownloadRawColumnLabelMapping()
        {
            SortedList<String,String> replace = base.GetDownloadRawColumnLabelMapping();

            replace.Add(v_AP_TESTS.NUM_Exams_Passed, "number_scores_3_or_above");
            replace.Add(v_AP_TESTS.PRC_of_Exams_Passed, "percent_scores_3_or_above");

            return replace;
        }
    }
}
