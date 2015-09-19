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
    public partial class Offerings : PageBaseWI
    {
        public enum list_items
        {
            activities,
            requirements,
            advanced,
            staff,
            qualifications,
            money,
            finance
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Master.EnableViewState = false;

            set_state();
            set_list_links();
        }

        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.Small_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.PlanningHelp1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.School_br_Panel, false);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.District_br_Panel, false);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.State_br_Panel, false);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.ViewTitlePanel, false);
            ((SligoCS.Web.WI.WI)Page.Master).SetlinkSchoolDistrictPanelWidth(185);
            Page.EnableViewState = false;
        }

        private void set_list_links()
        {
            set_list_item_link("What school-supported activities are offered?",
                 "~/" + GraphFileKeys.ActivityOffer, list_items.activities);
            set_list_item_link("What are the district requirements for high school graduation?",
                 "~/" + GraphFileKeys.GGRADREQS, list_items.requirements);
            set_list_item_link("What advanced courses are offered?",
                 "~/" + GraphFileKeys.CoursesOffered, list_items.advanced);
            set_list_item_link("What staff are available in this district?",
                 "~/" + GraphFileKeys.STAFF, list_items.staff);
            set_list_item_link("What are the qualifications of teachers?",
                 "~/" + GraphFileKeys.TEACHERQUALIFICATIONS, list_items.qualifications);
            set_list_item_link("How much money is received and spent in this district?",
                 "~/" + GraphFileKeys.MONEY, list_items.money);
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

            Control c = CPH.FindControl(controlID);
            if (c != null)
            {
                c.Controls.Add(list_item_link);
                c.Visible = true;
            }
        }
    }
}
