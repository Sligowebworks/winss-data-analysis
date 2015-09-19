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

using SligoCS.Web.WI.WebSupportingClasses.WI;
using SligoCS.DAL.WI;
using SligoCS.BL.WI;
using SligoCS.Web.Base.PageBase.WI;


namespace SligoCS.Web.WI
{
    public partial class GradReqsPage : PageBaseWI
    {
        protected override void OnInitComplete(EventArgs e)
        {
            GlobalValues.TrendStartYear = 1997;
            GlobalValues.Year = 2010;

            //Disable School Level
            if (UserValues.OrgLevel.Key == OrgLevelKeys.School)
            {
                GlobalValues.OrgLevel.Value = GlobalValues.OrgLevel.Range[OrgLevelKeys.District];
                pnlMessage.Visible = true;
            }

            //STYP not supported
            GlobalValues.OverrideSchoolTypeWhenOrgLevelIsSchool_Complete += PageBaseWI.DisableSchoolType;
           
            base.OnInitComplete(e);
        }
        protected override DALWIBase InitDatabase()
        {
            return new DALGrad_Reqs();
        }
        protected override GridView InitDataGrid()
        {
            return GradReqsDataGrid;
        }
        protected override ChartFX.WebForms.Chart InitGraph()
        {
            return barChart;
        }
        protected override string SetPageHeading()
        {
            return "What are the requirements for high school graduation?";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            String[] prefix = new String[2];
            prefix[0] =  "Graduation Requirements";
            
            GRSbj subjs = GlobalValues.GRSbj;
            if (subjs.Key == GRSbjKeys.Additional)
                prefix[1] = "Additional Subjects";
            else
                prefix[1] = "Required Subjects";

            DataSetTitle = GetTitleWithoutGroupForSchoolTypeUnsupported(String.Join(" - ", prefix));

            GradReqsDataGrid.AddSuperHeader(DataSetTitle);

            List<String> order = new List<string>(QueryMarshaller.BuildOrderByList(DataSet.Tables[0].Columns).ToArray());
            order.Add( v_Grad_Reqs.Subjectid);

            GradReqsDataGrid.OrderBy = barChart.OrderBy = String.Join(",", order.ToArray());

            GraphPanel.Visible = false;
            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.State) SetUpChart(DataSetTitle);
            set_state();
        }

        private void SetUpChart(String title)
        {
            GraphPanel.Visible = true;
            try
            {
                barChart.Title = title.Replace("<br/>", "\n");
            
                 barChart.AxisYDescription = "% of Districts\nExceeding State Requirements";

                barChart.DisplayColumnName = v_Grad_Reqs.PRC_of_Districts_Where_Credit_Requirements_Exceed_State_Law;

                barChart.MaxRateInResult = WebUserControls.GraphBarChart.GetMaxRateInColumn(DataSet.Tables[0], barChart.DisplayColumnName);

                if (GlobalValues.CompareTo.Key == CompareToKeys.Current)
                {
                    barChart.SeriesColumnName = v_Grad_Reqs.Subject;
                    barChart.FriendlyAxisXName = new List<string>( new String[] { String.Empty});
                }
                else // (GlobalValues.CompareTo.Key == CompareToKeys.Years)
                {
                    barChart.LabelColumnName = v_Grad_Reqs.Subject;
                    barChart.SeriesColumnName = v_Grad_Reqs.YearFormatted;
                }

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

        public override List<string> GetVisibleColumns(Group viewBy, OrgLevel orgLevel, CompareTo compareTo, STYP schoolType)
        {
            List<string> retval =  base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);

            retval.Add(v_Grad_Reqs.Subject);

            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.District)
            {
                retval.Add(v_Grad_Reqs.Credits_Required_by_District);
                retval.Add(v_Grad_Reqs.Credits_Required_by_State);
                retval.Add(v_Grad_Reqs.District_Requirements_Meet_or_Exceed_Law);
            }

            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.State)
            {
                retval.Add(v_Grad_Reqs.Credits_Required_by_State);
                retval.Add(v_Grad_Reqs.NUM_of_Districts_with_Grade_12);
                retval.Add(v_Grad_Reqs.Average_Number_of_Credits_Required_by_Districts);
                retval.Add(v_Grad_Reqs.NUM_of_Districts_Where_Credit_Requirements_Exceed_State_Law);
            }
            retval.Add(v_Grad_Reqs.PRC_of_Districts_Where_Credit_Requirements_Exceed_State_Law);
            return retval;
        }
    }
}
