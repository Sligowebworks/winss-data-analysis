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
using SligoCS.DAL.WI.DataSets;
using SligoCS.BL.WI;
using SligoCS.Web.WI.PageBase.WI;
using SligoCS.Web.WI.HelperClasses;
using System.Text;
using SligoCS.Web.WI.WebSupportingClasses.WI;
using ChartFX.WebForms;

namespace SligoCS.Web.WI
{
    public partial class StateTestsPerformance : PageBaseWI
    {
        protected v_WSAS  _ds =
            new v_WSAS();
        protected BLStateTestsPerformance blStateTests = new BLStateTestsPerformance();

        private string graphTitle = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Master.EnableViewState = false;

            base.PrepBLEntity(blStateTests);

            List<string> Subjects = new List<string>();
            Subjects.Add("1RE");
            //temp for testing purposes
            _ds = blStateTests.GetData(99, Subjects, GenericsListHelper.GetPopulatedList(50));

            CheckSelectedSchoolOrDistrict(blStateTests);
          SetLinkChangeSelectedSchoolOrDistrict(
              blStateTests, ChangeSelectedSchoolOrDistrict);

          SetVisibleColumns2(SligoDataGrid2, _ds, blStateTests.ViewBy,
             blStateTests.OrgLevel, blStateTests.CompareTo, blStateTests.SchoolType);

          StickyParameter.SQL = blStateTests.SQL;

          this.SligoDataGrid2.DataSource = _ds;

         this.SligoDataGrid2.DataBind();


          graphTitle = GetTitle("Dropout Rate", 
                            blStateTests,
                            GetRegionString());

            this.SligoDataGrid2.AddSuperHeader(graphTitle);

            this.SligoDataGrid2.ShowSuperHeader = true;

            if (base.StickyParameter.DETAIL != null && 
                    base.StickyParameter.DETAIL.ToString() == "NO")
            {
                this.SligoDataGrid2.Visible = false;
            }
            //testing
            this.SligoDataGrid2.Visible = true;

            set_state();
            setBottomLink(blStateTests);
            GraphPanel.Visible = false;
            if ( CheckIfGraphPanelVisible(blStateTests, GraphPanel) == true)
            {
                // SetUpChart(_ds);
            }
        }

        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(
                    WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(
                        WI.displayed_obj.dataLinksPanel, true);
        }

        private void setBottomLink(BLWIBase baseBL)
        {
            BottomLinkViewReport1.Year = baseBL.CurrentYear.ToString();
            BottomLinkViewReport1.DistrictCd = GetDistrictCdByFullKey();
            BottomLinkViewReport1.SchoolCd = GetSchoolCdByFullKey();
            BottomLinkViewProfile1.DistrictCd = GetDistrictCdByFullKey();
        }
        public override List<string> GetVisibleColumns(ViewByGroup viewBy, OrgLevel orgLevel, CompareTo compareTo, SchoolType schoolType)
        {
            List<string> cols = base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);

            if ((compareTo == CompareTo.SELSCHOOLS) || (compareTo == CompareTo.CURRENTONLY))
            {
                //When user selects "Compare To = Selected Schools" the first column in the grid
                //should show the school name, but with no header.                
                //cols.Add(_ds._v_WSAS.LinkdedName.ColumnName);
            }

           

            return cols;
        }
        protected void SligoDataGrid2_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    //decode the link to specific schools, so that it appears as a normal URL link.
            //    int colIndex = SligoDataGrid2.FindBoundColumnIndex(  _ds._v_WSAS.LinkedName.ColumnName);
            //    e.Row.Cells[colIndex].Text = Server.HtmlDecode(e.Row.Cells[colIndex].Text);

            //   //Does not apply to Retention page.
            //   SetOrgLevelRowLabels(_Dropout, SligoDataGrid2, e.Row);

            //    //// replace long race label with shourt race label
            //    FormatHelper formater = new FormatHelper();
            //    formater.SetRaceAbbr(SligoDataGrid2, e.Row,
            //        _ds._v_WSAS.RaceLabelColumn.ToString());

            //}
        }

    public override DataSet GetDataSet()
        {
            return _ds;
        }
    }
}

