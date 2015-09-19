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
    public partial class Expulsions : PageBaseWI
    {
        protected override void OnInitComplete(EventArgs e)
        {
            GlobalValues.CurrentYear = 2010;
            
            if (GlobalValues.Group.Key == GroupKeys.Disability)
            {
                GlobalValues.TrendStartYear = 2003;
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
      
        protected override ChartFX.WebForms.Chart InitGraph()
        {
            return barChart;
        }
        protected override GridView InitDataGrid()
        {
            return ExpulsionsDataGrid;
        }
        protected override DALWIBase InitDatabase()
        {
            return new DALExpulsions();
        }
        protected override string SetPageHeading()
        {
            return "What percentage of students were suspended or expelled?";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSetTitle = GetTitle("Expulsions");

            ExpulsionsDataGrid.AddSuperHeader(DataSetTitle);

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
                
                barChart.MaxRateInResult = GetMaxRateInResult(ds);

                if (GlobalValues.Group.Key == GroupKeys.All)
                {
                    List<String> friendlyAxisXName = new List<String>();
                    friendlyAxisXName.Add("All Students");
                    barChart.FriendlyAxisXNames = friendlyAxisXName;
                }
                barChart.AxisYDescription = "Percent of Students Enrolled";

                //Bind Data Source & Display
                barChart.DisplayColumnName =
                    v_ExpulsionsDis.Expulsion_Percent;
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
                            row[v_ExpulsionsDis.Expulsion_Percent].ToString()) > maxRateInResult)
                    {
                        maxRateInResult = Convert.ToDouble
                                (row[v_ExpulsionsDis.Expulsion_Percent].ToString());
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

            retval.Add(v_ExpulsionsDis.Total_Enrollment_PreK12);
            retval.Add(v_ExpulsionsDis.Number_of_Students_Expelled);
            retval.Add(v_ExpulsionsDis.Expulsion_Percent);
            return retval;
        }
    }
}
