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
    public partial class attendance : PageBaseWI
    {
        public enum list_items
        {
            attend,
            truant,
            activities,
            courses,
            expelled,
            lost_school_days,
            incidents,
            after_expelled,
            dropouts
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            set_state();
            set_list_links();
        }

        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.Small_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.AttendanceDataInfo1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.School_br_Panel, false);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.District_br_Panel, false);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.State_br_Panel, false);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.ViewTitlePanel, false);
            ((SligoCS.Web.WI.WI)Page.Master).SetlinkSchoolDistrictPanelWidth(185);
            Page.EnableViewState = false;
        }

        private void set_list_links()
        {
            set_list_item_link("What percent of students attend school each day?",
                "~/" + GraphFileKeys.ATTENDANCE, list_items.attend);
            set_list_item_link( "What percent of students are habitually truant?", 
                "~/" + GraphFileKeys.TRUANCY, list_items.truant);
            set_list_item_link("Do students participate in school supported activities?",
                "~/" + GraphFileKeys.ActivitiesPartic, list_items.activities);
            set_list_item_link( "What courses are students taking?", 
                "~/" + GraphFileKeys.CoursesTaken, list_items.courses);
            set_list_item_link( "What percentage of students were suspended or expelled last year?",
                "~/" + GraphFileKeys.SUSPENSIONS, list_items.expelled);
            set_list_item_link("What percentage of school days were lost due to suspension or expulsion?",
                "~/" + GraphFileKeys.SUSPENSIONSDAYSLOST, list_items.lost_school_days);
            set_list_item_link("What types of incidents resulted in suspensions or expulsions?",
                "~/" + GraphFileKeys.SUSPEXPINCIDENTS, list_items.incidents);
            set_list_item_link("What happens after students are expelled?", 
                "~/" + GraphFileKeys.GEXPLENGTH, list_items.after_expelled);
            set_list_item_link( "How many students dropped out of school last year?",
                "~/" + GraphFileKeys.DROPOUTS, list_items.dropouts);
        }
        private void set_list_item_link(string link_text, string nav_file_path, list_items placeholder_name)
        {
            ContentPlaceHolder CPH = (ContentPlaceHolder)this.Page.Master.FindControl("ContentPlaceHolder1");
            string controlID = placeholder_name.ToString();

            HyperLink list_item_link = new HyperLink();

            list_item_link.ID = placeholder_name + "_link";
            list_item_link.Text = link_text;
            String[] path = Page.Request.FilePath.Split("/".ToCharArray());
            String file = path[path.Length-1];

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
