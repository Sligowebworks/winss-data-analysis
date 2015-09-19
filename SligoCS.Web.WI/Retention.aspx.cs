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
    public partial class Retention : PageBaseWI
    {
        protected override void OnInitComplete(EventArgs e)
        {
            GlobalValues.Grade.Key = GradeKeys.Grades_K_12;
            GlobalValues.CurrentYear = 2010;

            if (GlobalValues.Group.Key == GroupKeys.Disability)
            {
                GlobalValues.TrendStartYear = 2005;
            }
            else if (GlobalValues.Group.Key == GroupKeys.EngLangProf
                    || GlobalValues.Group.Key == GroupKeys.EconDisadv)
            {
                GlobalValues.TrendStartYear = 2005;
            }
            else
            {
                GlobalValues.TrendStartYear = 1997;
            }

           nlrCompareTo.LinkRow.LinkControlAdded += CompareToLinkHandler;

           //Don't show combined groups at District Level, until support is added in the data import.
           if (GlobalValues.OrgLevel.Key == OrgLevelKeys.District
               && QueryMarshaller.RaceDisagCodes.Contains((int)QueryMarshaller.RaceCodes.Comb))
               QueryMarshaller.RaceDisagCodes.Remove((int)QueryMarshaller.RaceCodes.Comb);

            base.OnInitComplete(e);
        }
        protected override ChartFX.WebForms.Chart InitGraph()
        {
            return barChart;
        }
        protected override DALWIBase InitDatabase()
        {
            return new DALRetention();
        }
        protected override GridView InitDataGrid()
        {
            return RetentionDataGrid;
        }
        protected override NavViewByGroup InitNavRowGroups()
        {
            nlrVwByGroup.LinksEnabled = NavViewByGroup.EnableLinksVector.EconElp;
            return nlrVwByGroup;
        }
        protected override string SetPageHeading()
        {
            return "What percent of students did not advance to the next grade level?";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSetTitle = GetTitle("Retention Rate");

            RetentionDataGrid.AddSuperHeader(DataSetTitle);
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

                barChart.MaxRateInResult = GetMaxRateInResult(ds);

                if (GlobalValues.Group.Key == GroupKeys.All)
                {
                    List<String> friendlyAxisXName = new List<String>();
                    friendlyAxisXName.Add("All Students");
                    barChart.FriendlyAxisXNames = friendlyAxisXName;
                }
                barChart.AxisYDescription = "Retention Rate";

                barChart.DisplayColumnName = v_RetentionWWoDisEconELPSchoolDistState.Retention_Rate;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private double GetMaxRateInResult(DataSet ds)
        {
            double maxRateInResult = 0;

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                try
                {
                    if (Convert.ToDouble(row[v_RetentionWWoDisEconELPSchoolDistState.Retention_Rate].ToString())
                                        > maxRateInResult)
                    {
                        maxRateInResult = Convert.ToDouble(row[v_RetentionWWoDisEconELPSchoolDistState.Retention_Rate].ToString());
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

        public override List<string> GetVisibleColumns(Group viewBy, OrgLevel orgLevel, CompareTo compareTo, STYP schoolType)
        {
            List<string> cols = base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);

            cols.Add(v_RetentionWWoDisEconELPSchoolDistState.Total_Enrollment_K12);
            cols.Add(v_RetentionWWoDisEconELPSchoolDistState.Completed_School_Term);
            cols.Add(v_RetentionWWoDisEconELPSchoolDistState.Number_of_Retentions);
            cols.Add(v_RetentionWWoDisEconELPSchoolDistState.Retention_Rate);
            
            return cols;
        }
        protected void CompareToLinkHandler(HyperLinkPlus theLink)
        {
            //example event handler;
        }
    }
}
