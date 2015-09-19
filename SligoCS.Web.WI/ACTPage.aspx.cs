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

    public partial class ACTPage : PageBaseWI
    {
        protected SligoCS.Web.WI.WebSupportingClasses.WinssDataGrid ACTDataGrid = new SligoCS.Web.WI.WebSupportingClasses.WinssDataGrid();
        
        protected override DALWIBase InitDatabase()
        {
            return new DALACT();
        }
        protected override GridView  InitDataGrid()
        {
            if (ACTDataGrid == null) throw new Exception("is null");
            return ACTDataGrid; 
        }
        protected override ChartFX.WebForms.Chart InitGraph()
        {
            return barChart;
        }
        protected override SligoCS.Web.WI.WebUserControls.NavViewByGroup InitNavRowGroups()
        {
            nlrVwByGroup.LinksEnabled = SligoCS.Web.WI.WebUserControls.NavViewByGroup.EnableLinksVector.Race;
            return nlrVwByGroup;
        }
        protected override string SetPageHeading()
        {
            return "How did students perform on college admissions and placement tests?";
        }
        protected override void OnInitComplete(EventArgs e)
        {
            GlobalValues.Grade.Key = GradeKeys.Combined_PreK_12;
            GlobalValues.Year = 2010;

            //STYP not supported
            GlobalValues.OverrideSchoolTypeWhenOrgLevelIsSchool_Complete += PageBaseWI.DisableSchoolType;
           
            QueryMarshaller.RaceDisagCodes.Add((int)QueryMarshaller.RaceCodes.RaceEth_NA);
            QueryMarshaller.RaceDisagCodes.Remove((int)QueryMarshaller.RaceCodes.Comb);

            base.OnInitComplete(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSetTitle = GetTitleForSchoolTypeUnsupported("ACT Results - " + GlobalValues.ACTSubj.Key + " -").Replace("- -", "-");

            ACTDataGrid.AddSuperHeader(DataSetTitle);

            //Notes for graph
            SetUpChart(DataSetTitle, GlobalValues.ACTSubj);

            TextForRaceInBottomLink.Visible = (GlobalValues.Group.Key == GroupKeys.Race);
            set_state();
            setBottomLink();
        }

        /// <summary>
        ///  Set up graph 
        /// </summary>
        /// <param name="ds"></param>
        private void SetUpChart(string graphTitle, ACTSubj subject)
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
                List<String> axisYName = new List<String>();
                axisYName.Add("0.0");
                if (GlobalValues.CompareTo.Key == CompareToKeys.SelSchools ||
                        GlobalValues.CompareTo.Key == CompareToKeys.SelDistricts)
                {
                    barChart.AxisYMax = 37;
                    barChart.AxisYStep = 4;
                    for (int i = 1; i < 11; i++)
                    {
                        axisYName.Add(Convert.ToString(i * 4) + ".0");
                    }
                }
                else
                {
                    barChart.AxisYMax = 38;
                    barChart.AxisYStep = 3;
                    for (int i = 1; i < 14; i++)
                    {
                        axisYName.Add(Convert.ToString(i * 3) + ".0");
                    }
                }
                barChart.FriendlyAxisYNames = axisYName;
                barChart.AxisYDescription = "Average Score - " + subject.Key;

                Hashtable map = new Hashtable();

                map[ACTSubjKeys.English] = v_ACT.English;
                map[ACTSubjKeys.Math] = v_ACT.Math;
                map[ACTSubjKeys.Reading] = v_ACT.Reading;
                map[ACTSubjKeys.Science] = v_ACT.Science ;
                map[ACTSubjKeys.Composite] = v_ACT.Composite;

                barChart.DisplayColumnName = map[subject.Key].ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
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

        public override List<string> GetVisibleColumns(
            Group viewBy, OrgLevel orgLevel, 
            CompareTo compareTo, STYP schoolType)
        {
            List<string> retval =
                base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);

            retval.Add(v_ACT.Enrollment);
            retval.Add(v_ACT.PupilCount);
            retval.Add(v_ACT.Perc_Tested);
            retval.Add(GlobalValues.ACTSubj.Key);

            return retval;
        }

        protected override SortedList<string, string> GetDownloadRawColumnLabelMapping()
        {
            SortedList<string, string> newLabels = base.GetDownloadRawColumnLabelMapping();
            newLabels.Add(v_ACT.PupilCount, "number_tested");
            newLabels.Add(v_ACT.Perc_Tested, "percent_tested");
            newLabels.Add(v_ACT.Composite, "average_score_composite");
            return newLabels;
        }

        protected override List<string> GetDownloadRawVisibleColumns()
        {
            List<String> cols = base.GetDownloadRawVisibleColumns();

            if (GlobalValues.SuperDownload.Key == SupDwnldKeys.True)
            {
                foreach (String key in GlobalValues.ACTSubj.Range.Keys)
                {
                    cols.Add(key);
                }
            }
            return cols;
        }
    
    }
}
