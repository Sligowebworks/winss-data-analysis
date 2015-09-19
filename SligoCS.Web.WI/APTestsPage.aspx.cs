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
            GlobalValues.Year = 2009;

            if (UserValues.OrgLevel.Key == OrgLevelKeys.School) 
                GlobalValues.OrgLevel.Value = GlobalValues.OrgLevel.Range[OrgLevelKeys.District];

            //ACT page links here insufficiently
            //UserValues.GraphFile.Value = UserValues.GraphFile.Range[GraphFileKeys.AP];

            //STYP not supported
            GlobalValues.OverrideSchoolTypeWhenOrgLevelIsSchool_Complete += PageBaseWI.DisableSchoolType;
           
            // supporting additional Race/Ethnicity option - No response - race = (1,2,3,4,5,8)
            QueryMarshaller.RaceDisagCodes.Add(8);

            base.OnInitComplete(e);

            APTestsDataGrid.Columns.FieldsChanged += new EventHandler(RenameExamsPassed_FieldsChanged);
        }

        void RenameExamsPassed_FieldsChanged(object sender, EventArgs e)
        {
            DataControlFieldCollection columns = (DataControlFieldCollection)sender;

            foreach (DataControlField field in columns)
            {
                if (field.HeaderText == "# Exams Passed" || field.HeaderText == "% of Exams Passed")
                    field.HeaderText = field.HeaderText.Replace("Exams Passed", "Scores 3 or Above");
            }
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
                barChart.DisplayColumnName = "% of Exams Passed";
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

        public override List<string> GetVisibleColumns(Group viewBy, OrgLevel orgLevel, CompareTo compareTo, STYP schoolType)
        {
            List<string> retval = 
                base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);
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
    }
}
