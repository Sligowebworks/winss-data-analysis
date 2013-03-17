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
using SligoCS.Web.Base.WebServerControls.WI;

using SligoCS.Web.WI.WebSupportingClasses.WI;
using SligoCS.Web.WI.WebUserControls;

namespace SligoCS.Web.WI 
{
    
    public partial class Truancy : PageBaseWI
    {
        protected override void OnInitComplete(EventArgs e)
        {            
            GlobalValues.Grade.Key = GradeKeys.Grades_K_12;
            GlobalValues.CurrentYear = 2012;
            GlobalValues.TrendStartYear = 1997;

            //Don't show combined groups at District Level, until support is added in the data import.
            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.District
                && QueryMarshaller.RaceDisagCodes.Contains((int)QueryMarshaller.RaceCodes.Comb))
                QueryMarshaller.RaceDisagCodes.Remove((int)QueryMarshaller.RaceCodes.Comb);

            base.OnInitComplete(e);
        }
        protected override NavViewByGroup InitNavRowGroups()
        {
            nlrVwByGroup.LinksEnabled = NavViewByGroup.EnableLinksVector.Grade;
            return nlrVwByGroup;
        }
        protected override ChartFX.WebForms.Chart InitGraph()
        {
            return barChart;
        }
        protected override DALWIBase InitDatabase()
        {
            return new DALTruancy();
        }
        protected override GridView InitDataGrid()
        {
            return TruancyDataGrid;
        }
        protected override string SetPageHeading()
        {
            return "What percent of students are habitually truant?";
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            nlrCompareTo.LinkRow.LinkControlAdded += CompareToLinkHandler;

            DataSetTitle = GetTitle("Truancy Rate");

            TruancyDataGrid.AddSuperHeader(DataSetTitle);

            if (GlobalValues.CompareTo.Key != CompareToKeys.Years)
            {
                DefPanel.Visible = true;
            }

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

                //Axis Y Settings
                //int axisYStep = 1;
                //int axisYMin = 0;
                //int axisYMax = 10;
                //ArrayList friendlyAxisYName = new ArrayList();
                //GetAxisYSetting(ds, ref axisYStep, ref axisYMin, 
                //                    ref axisYMax, ref friendlyAxisYName);
                //barChart.AxisYStep = axisYStep;
                //barChart.AxisYMin = axisYMin;
                //barChart.AxisYMax = axisYMax;
                //barChart.FriendlyAxisYName = friendlyAxisYName;    

                //barChart.LegendBox.ContentLayout = ChartFX.WebForms.ContentLayout.Center;
                //barChart.LegendBox.PlotAreaOnly = false;

                if (GlobalValues.Group.Key == GroupKeys.All)
                {
                    List<String> friendlyAxisXName = new List<String>();
                    friendlyAxisXName.Add("All Students");
                    barChart.FriendlyAxisXNames = friendlyAxisXName;
                }
                barChart.AxisYDescription = "Truancy Rate";

                barChart.DisplayColumnName = v_TruancySchoolDistState.Truancy_Rate;

                barChart.MaxRateInResult = GraphBarChart.GetMaxRateInColumn(ds.Tables[0], barChart.DisplayColumnName);

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
                    if (Convert.ToDouble(row[v_TruancySchoolDistState.Truancy_Rate].ToString())
                                        > maxRateInResult)
                    {
                        maxRateInResult = Convert.ToDouble(row[v_TruancySchoolDistState.Truancy_Rate].ToString());
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

        public override System.Collections.Generic.List<string> GetVisibleColumns()
        {
            List<string> retval = base.GetVisibleColumns();

            retval.Add(v_TruancySchoolDistState.Total_Enrollment_K12);
            retval.Add(v_TruancySchoolDistState.Number_of_Students_Habitually_Truant);
            retval.Add(v_TruancySchoolDistState.Truancy_Rate);

            return retval;
        }
        
        protected void CompareToLinkHandler(HyperLinkPlus theLink)
        {
            //example event handler;
        }
        /*private void ViewByLinkAdded(HyperLinkPlus theLink)
        {
            if (GlobalValues.STYP.Key == STYPKeys.AllTypes)
            {
                if (theLink.ID != "linkGroupAll")
                    theLink.Enabled = false;
            }

            if (!theLink.Enabled)
            {
                GlobalValues.Group.Value = GlobalValues.Group.Range[GroupKeys.All];
            }

        }*/
    }
}
