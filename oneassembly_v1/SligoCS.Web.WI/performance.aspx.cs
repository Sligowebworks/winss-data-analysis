using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SligoCS.BL.WI;
using SligoCS.DAL.WI.DataSets;
using SligoCS.Web.WI.PageBase.WI;
using SligoCS.Web.WI.HelperClasses;
using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.Web.WI
{
    public partial class performance : PageBaseWI
    {
        public enum list_items
        {   
            state_tests,
            all_vs_continuing,
            wrct,
            retention,
            hs_completion,
            postgrad_plan,
            advanced_placement,
            coll_admit_placement
        }

        protected v_WRCT _ds = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Master.EnableViewState = false;

            ParamsHelper.LoadFromContext(StickyParameter);
            set_state();
            set_list_links();
            check_wrct(base.StickyParameter);
        }

        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.Small_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.StandardsPerformance1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.School_br_Panel, false);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.District_br_Panel, false);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.State_br_Panel, false);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.ViewTitlePanel, false);
            ((SligoCS.Web.WI.WI)Page.Master).SetlinkSchoolDistrictPanelWidth(185);
            Page.EnableViewState = false;
        }

        private void set_list_links()
        {
            string myHIGHGRADE = string.Empty;
            string orgLevel = string.Empty; 
            if (Request.QueryString["HIGHGRADE"] != null)
            {
                myHIGHGRADE = Request.QueryString["HIGHGRADE"];
            }

            if (Request.QueryString["ORGLEVEL"] != null)
            {
                orgLevel = Request.QueryString["ORGLEVEL"];
            }
            set_list_item_link(base.StickyParameter, "How did students perform on state tests at grades 3-8 and 10?", "GEDISA", "/SligoWI/StateTestsPerformance.aspx", list_items.state_tests);
            set_list_item_link(base.StickyParameter, "How did performance of all students enrolled compare to continuing students only?", "EDISACONTINUING", "/SligoWI/ComparePerformance.aspx", list_items.all_vs_continuing);
            set_list_item_link(base.StickyParameter, "How did students perform on the Wisconsin Reading Comprehension Test? (Last administered March 2005)", Constants.GRAPH_FILE_GWRCT, "/SligoWI/WRCTPerformance.aspx", list_items.wrct);
            set_list_item_link(base.StickyParameter, "What percent of students did not advance to the next grade level?", Constants.GRAPH_FILE_RETENTION, "/SligoWI/GridPage.aspx", list_items.retention);
            // set_list_item_link(base.StickyParameter, "How did students perform on advanced placement tests?", "AP", "/SligoWI/APTestsPage.aspx", list_items.advanced_placement);

            //to fix #1017, so comment out the if statement below. #1017 : performance page - not showing all questions at state level 
            if (myHIGHGRADE.Equals("64") || orgLevel == "State" )
            {
                set_list_item_link(base.StickyParameter, "How did students perform on college admissions and placement tests?", "ACT", "/SligoWI/ACTPage.aspx", list_items.coll_admit_placement);
                set_list_item_link(base.StickyParameter, "What are the high school completion rates? (Schools with grade 12 only)", Constants.GRAPH_FILE_HIGHSCHOOLCOMPLETION, "/SligoWI/HSCompletionPage.aspx", list_items.hs_completion);
                set_list_item_link(base.StickyParameter, "What are students' postgraduation plans? (Schools with grade 12 only", Constants.GRAPH_FILE_GGRADPLAN, "/SligoWI/PostGradIntentPage.aspx", list_items.postgrad_plan);
                pnl_grade_12.Visible = true;
            }
        }

        private void set_list_item_link(StickyParameter myParams, string link_text, string graphfile_name, string nav_url, list_items placeholder_name)
        {
            ContentPlaceHolder CPH = (ContentPlaceHolder)this.Page.Master.FindControl("ContentPlaceHolder1");
            string controlID = placeholder_name.ToString();

            HyperLink list_item_link = new HyperLink();

            list_item_link.ID = placeholder_name + "_link";
            list_item_link.Text = link_text;

            // First, use ParamsHelper.GetQueryString() to get the query string from StickyParameter, also replace GraphFile param
            // note that "GraphFile" should be replaced by using enum 'StickyParameter.QStringVar.GraphFile.ToString()'

            string queryString = ParamsHelper.GetQueryString(StickyParameter, StickyParameter.QStringVar.GraphFile.ToString(), graphfile_name);

            // then use string replacement directly for 2nd param (uncomment the following if meet your need )

            queryString = ParamsHelper.ReplaceQueryString(queryString, StickyParameter.QStringVar.zBackTo.ToString(), "performance.aspx");

            // then appending the query string to nav_url
            list_item_link.NavigateUrl = nav_url + queryString;

            list_item_link.Visible = true;

            Control c = CPH.FindControl(controlID);
            if (c != null)
            {
                c.Controls.Add(list_item_link);
                c.Visible = true;
            }
        }

        private void check_wrct(StickyParameter myParams)
        {
            string myORGLEVEL = StickyParameter.ORGLEVEL.ToString();

            BLWRCT wrct = new BLWRCT();
            base.PrepBLEntity(wrct);
            _ds = wrct.GetWRCT(StickyParameter.FULLKEY);
           
            if (_ds._v_WRCT.Count > 0)
                pnl_WRCT.Visible = true;

            if (myORGLEVEL.Equals("DI"))
                pnl_HasG3.Visible = true;

            if (myORGLEVEL.Equals("SC"))
                pnl_HasG3.Visible = true;
        }

        public override DataSet GetDataSet()
        {
            return _ds;
        }
    }
}
