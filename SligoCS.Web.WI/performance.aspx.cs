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
using SligoCS.Web.Base.PageBase.WI;

using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.Web.WI
{
    public partial class performance : PageBaseWI
    {
        /// <summary>
        /// Control ID's
        /// </summary>
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

        protected void Page_Load(object sender, EventArgs e)
        {
            set_state();
            set_list_links();
            check_wrct();
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
            string orgLevel = string.Empty; 
            
            SerializableDictionary<String, String> range = GlobalValues.GraphFile.Range;

            set_list_item_link("How did students perform on the Wisconsin Reading Comprehension Test? (Last administered March 2005)", 
                "~/" + GraphFileKeys.GWRCT, list_items.wrct);
            set_list_item_link("What percent of students did not advance to the next grade level?",
                "~/" + GraphFileKeys.RETENTION, list_items.retention);
            // set_list_item_link("How did students perform on advanced placement tests?", "AP", "~/APTestsPage.aspx", list_items.advanced_placement);

            //to fix #1017, so comment out the if statement below. #1017 : performance page - not showing all questions at state level 
            if (GlobalValues.HIGHGRADE == 64 || base.GlobalValues.OrgLevel.Key == OrgLevelKeys.State )
            {
                set_list_item_link("What are the high school completion rates? (Schools with grade 12 only)",
                    "~/" + GraphFileKeys.HIGHSCHOOLCOMPLETION, list_items.hs_completion);
                set_list_item_link("What are students' postgraduation plans? (Schools with grade 12 only)", 
                    "~/" + GraphFileKeys.POSTGRADPLAN, list_items.postgrad_plan);

                pnl_grade_12.Visible = true;
            }
        }

        private void set_list_item_link(string link_text, string nav_file_path, list_items placeholder_name)
        {
            ContentPlaceHolder CPH = (ContentPlaceHolder)this.Page.Master.FindControl("ContentPlaceHolder1");
            string controlID = placeholder_name.ToString();

            HyperLink list_item_link = new HyperLink();

            list_item_link.ID = placeholder_name + "_link";
            list_item_link.Text = link_text;
            String[] path = Page.Request.FilePath.Split("/".ToCharArray());
            String file = path[path.Length - 1];

            String queryString = UserValues.GetQueryString("Qquad", file);

            list_item_link.NavigateUrl = nav_file_path + queryString;
            list_item_link.Visible = true;

            if (placeholder_name == list_items.wrct)
            { // WRCT no longer available
                list_item_link.Enabled = false;
            }

            Control c = CPH.FindControl(controlID);
            if (c != null)
            {
                c.Controls.Add(list_item_link);
                c.Visible = true;
            }
        }

        private void check_wrct()
        {
            DAL.WI.DALWRCT dal =  new DAL.WI.DALWRCT();
            GlobalValues.SQL = dal.GetWRCT(FullKeyUtils.GetMaskedFullkey(GlobalValues.FULLKEY, GlobalValues.OrgLevel));
            QueryMarshaller.AssignQuery(dal, GlobalValues.SQL);
           
            if (QueryMarshaller.Database.DataSet.Tables[0].Rows.Count > 0)
                pnl_WRCT.Visible = true;

            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.District)
                pnl_HasG3.Visible = true;

            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.School)
                pnl_HasG3.Visible = true;
        }
    }
}
