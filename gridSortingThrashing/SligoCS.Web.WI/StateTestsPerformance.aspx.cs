using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SligoCS.BL.WI;
using SligoCS.DAL.WI;
using SligoCS.Web.WI.DAL.DataSets;
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.WI.WebSupportingClasses.WI;
using SligoCS.Web.Base.WebServerControls.WI;
using SligoCS.Web.WI.HelperClasses;
using ChartFX.WebForms;

namespace SligoCS.Web.WI
{
    public partial class StateTestsPerformance : PageBaseWI
    {
        protected v_WSAS  _ds =  new v_WSAS();
        private string graphTitle = string.Empty;

        protected override DALWIBase InitDatabase()
        {
            return new DALWSAS();
        }
        protected override DataSet InitDataSet()
        {
            return _ds;
        }
        protected override GridView InitDataGrid()
        {
            return StateTestsDataGrid;
        }
        protected override string SetPageHeading()
        {
            return "How did students perform on state test at grades 3-8 and 10?";
        }
        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
            GlobalValues.Grade = 99;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            GlobalValues globe = GlobalValues;

            String titlePrefix = "WKCE";

            if (GlobalValues.WkceWsas.Key == WkceWsasKeys.Wsas)
                titlePrefix = titlePrefix + "/WAA Combined";

            graphTitle = GetTitle("");

            set_state();
            setBottomLink();

            StateTestsDataGrid.AddSuperHeader(graphTitle);

            StateTestsDataGrid.SetVisibleColumns(
                GetVisibleColumns(
                    globe.Group, globe.OrgLevel, globe.CompareTo, globe.STYP
                    )
                );
            
            if (base.GlobalValues.DETAIL != null && 
                    base.GlobalValues.DETAIL.ToString() == "NO")
            {
                StateTestsDataGrid.Visible = false;
            }
            GraphPanel.Visible = false;
            if ( CheckIfGraphPanelVisible(GraphPanel) == true)
            {
                // SetUpChart(_ds);
            }
        }
        public override List<string> GetVisibleColumns(Group viewBy, OrgLevel orgLevel, CompareTo compareTo, STYP schoolType)
        {
            List<String> columns = base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);

            columns.Add("Enrolled");
            columns.Add("Percent Minimal");
            columns.Add("MinPerfWSAS");
            columns.Add("Percent Basic");
            columns.Add("BasicWSAS");
            columns.Add("Percent Proficient");
            columns.Add("ProficientWSAS");
            columns.Add("Percent Advanced");
            columns.Add("AdvancedWSAS");

            return columns;
        }
        protected void StateTestsDataGrid_RowDataBound(Object sender, GridViewRowEventArgs e)
        {

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
            BottomLinkViewReport1.Year = GlobalValues.Year.ToString();
            BottomLinkViewReport1.DistrictCd = GetDistrictCdByFullKey();
            BottomLinkViewReport1.SchoolCd = GetSchoolCdByFullKey();
            BottomLinkViewProfile1.DistrictCd = GetDistrictCdByFullKey();
        }
        
    }
}

