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
    public partial class DropOuts : PageBaseWI
    {
        protected override DALWIBase InitDatabase()
        {
            return new DALDropouts();
        }
        protected override Chart InitGraph()
        {
            return barChart;
        }
        protected override GridView InitDataGrid()
        {
            return DropoutsDataGrid;
        }
        protected override void OnInitComplete(EventArgs e)
        {            
            GlobalValues.Year = 2009;
            GlobalValues.Grade.Key = GradeKeys.Grades_7_12_Combined;

            if (GlobalValues.Group.Key == GroupKeys.Disability)
            {
                GlobalValues.TrendStartYear = 2003;
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

            base.OnInitComplete(e);
        }
        protected override SligoCS.Web.WI.WebUserControls.NavViewByGroup InitNavRowGroups()
        {
            nlrVwByGroup.LinksEnabled = SligoCS.Web.WI.WebUserControls.NavViewByGroup.EnableLinksVector.EconElp;
            return nlrVwByGroup;
        }
        protected override string SetPageHeading()
        {
            return "What is the drop out rate?";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSetTitle = GetTitle("Dropout Rate");

            DropoutsDataGrid.AddSuperHeader(DataSetTitle);

            set_state();
            setBottomLink();

            SetUpChart(DataSet);
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
                    friendlyAxisXName.Add("All Students");
                    barChart.FriendlyAxisXNames = friendlyAxisXName;
                }

                barChart.AxisYDescription = "Dropout Rate";
                barChart.DisplayColumnName = v_DropoutsWWoDisEconELPSchoolDistState.Drop_Out_Rate;
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
                    if (Convert.ToDouble(
                            row[v_DropoutsWWoDisEconELPSchoolDistState.Drop_Out_Rate].ToString())
                                        > maxRateInResult)
                    {
                        maxRateInResult = Convert.ToDouble(row[v_DropoutsWWoDisEconELPSchoolDistState.Drop_Out_Rate].ToString());
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
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(
                    WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(
                        WI.displayed_obj.dataLinksPanel, true);
        }
        private void setBottomLink()
        {
            BottomLinkViewProfile1.DistrictCd = GlobalValues.DistrictCode;
        }
        public override List<string> GetVisibleColumns(Group viewBy, OrgLevel orgLevel, CompareTo compareTo, STYP schoolType)
        {
            List<string> cols = base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);

            cols.Add(v_DropoutsWWoDisEconELPSchoolDistState.Enrollment);
            cols.Add(v_DropoutsWWoDisEconELPSchoolDistState.Students_expected_to_complete_the_school_term);
            cols.Add(v_DropoutsWWoDisEconELPSchoolDistState.Students_who_completed_the_school_term);
            cols.Add(v_DropoutsWWoDisEconELPSchoolDistState.Drop_Outs);
            cols.Add(v_DropoutsWWoDisEconELPSchoolDistState.Drop_Out_Rate);

            return cols;
        }
    }
}
