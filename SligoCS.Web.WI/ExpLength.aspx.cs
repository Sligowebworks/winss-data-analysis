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

using System.Text;
using SligoCS.Web.WI.WebSupportingClasses.WI;
using ChartFX.WebForms;

namespace SligoCS.Web.WI
{
    public partial class ExpLength : PageBaseWI
    {
        protected override void OnInitComplete(EventArgs e)
        {
            //Disable School Level
            if (UserValues.OrgLevel.Key == OrgLevelKeys.School)
            {
                GlobalValues.OrgLevel.Value = GlobalValues.OrgLevel.Range[OrgLevelKeys.District];
                pnlMessage.Visible = true;
            }

            GlobalValues.TrendStartYear = 2001;
            GlobalValues.CurrentYear = 2006;
            GlobalValues.ForceCurrentYear = true;

            //STYP not supported
            GlobalValues.OverrideSchoolTypeWhenOrgLevelIsSchool_Complete += PageBaseWI.DisableSchoolType;

            base.OnInitComplete(e);
        }
        protected override DALWIBase InitDatabase()
        {
            return new DALExpulsionServices();
        }
        protected override GridView InitDataGrid()
        {
            return PostExpulsionDataGrid;
        }
        protected override Chart InitGraph()
        {
            return barChart;
        }
        protected override string SetPageHeading()
        {
            return "What happens after students are expelled?";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSetTitle = GetTitleWithoutGroupForSchoolTypeUnsupported("Post Expulsion - Length of Expulsion");

            PostExpulsionDataGrid.AddSuperHeader(DataSetTitle);

//            PostExpulsionDataGrid.SelectedSortBySecondarySort = true;
            List<String> order = QueryMarshaller.BuildOrderByList(DataSet.Tables[0].Columns);
            order.Insert(1, vExpulsionServices.Expulsion_Type);
            PostExpulsionDataGrid.OrderBy = String.Join(",", order.ToArray());
            
            set_state();

            SetUpChart(DataSetTitle);
            }

        private void SetUpChart(String graphTitle)
        {
            try
            {
                barChart.Title = graphTitle;
                if (GlobalValues.CompareTo.Key == CompareToKeys.Years)
                {
                    barChart.LabelColumns = new List<String>(new String[]
                        {
                            vExpulsionServices.Expulsion_Type,
                            vExpulsionServices.YearFormatted
                        });
                }
                if (GlobalValues.CompareTo.Key == CompareToKeys.OrgLevel
                    || GlobalValues.CompareTo.Key == CompareToKeys.SelDistricts
                    || GlobalValues.CompareTo.Key == CompareToKeys.SelSchools
                    || GlobalValues.CompareTo.Key == CompareToKeys.Current)
                {
                    barChart.LabelColumns = new List<String>(new String[]
                        {
                            vExpulsionServices.Expulsion_Type,
                            vExpulsionServices.OrgLevelLabel
                        });
                }
                barChart.MeasureColumns = new List<String>(new String[]
                    {
                        vExpulsionServices.NUM_One_Year_or_Less, 
                        vExpulsionServices.NUM_More_Than_One_Year,
                        vExpulsionServices.NUM_Permanent
                    }
                );
                barChart.AxisYDescription = "Number of Expelled Students";
            }
            catch (Exception ex)
            {
                if (GlobalValues.TraceLevels > 0) throw ex;
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

           public override List<string> GetVisibleColumns(Group viewBy, OrgLevel orgLevel, CompareTo compareTo, STYP schoolType)
            {
                List<string> cols = base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);

                cols.Add(vExpulsionServices.Expulsion_Type);
                cols.Add(vExpulsionServices.Total_NUM_Expelled);
                cols.Add(vExpulsionServices.NUM_One_Year_or_Less);
                cols.Add(vExpulsionServices.NUM_More_Than_One_Year);
                cols.Add(vExpulsionServices.NUM_Permanent);

                return cols;
            }
    }
}
