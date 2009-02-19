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
            Page.Master.EnableViewState = false;

            ParamsHelper.LoadFromContext(StickyParameter);
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
            set_list_item_link(base.StickyParameter, "What percent of students attend school each day?", "ATTENDANCE", "/SligoWI/AttendancePage.aspx", list_items.attend);
            set_list_item_link(base.StickyParameter, "What percent of students are habitually truant?", "TRUANCY", "/SligoWI/Truancy.aspx", list_items.truant);
            set_list_item_link(base.StickyParameter, "Do students participate in school supported activities?", "SSACTIVITIES", "/SligoWI/ActivitiesParticipate.aspx", list_items.activities);
            set_list_item_link(base.StickyParameter, "What courses are students taking?", "GCOURSETAKE", "/SligoWI/CoursesTaking.aspx", list_items.courses);
            set_list_item_link(base.StickyParameter, "What percentage of students were suspended or expelled last year?", "SUSPEXP", "/SligoWI/SuspendExpel.aspx", list_items.expelled);
            set_list_item_link(base.StickyParameter, "What percentage of school days were lost due to suspension or expulsion?", "SUSPEXPDAYSLOST", "/SligoWI/SuspExpDaysLost.aspx", list_items.lost_school_days);
            set_list_item_link(base.StickyParameter, "What types of incidents resulted in suspensions or expulsions?", "SUSPEXPINCIDENTS", "/SligoWI/SuspExpIncidents.aspx", list_items.incidents);
            set_list_item_link(base.StickyParameter, "What happens after students are expelled?", "GEXPSERVICES", "/SligoWI/AfterExpell.aspx", list_items.after_expelled);
            set_list_item_link(base.StickyParameter, "How many students dropped out of school last year?", "DROPOUTS", "/SligoWI/Dropouts.aspx", list_items.dropouts);
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
            
            queryString = ParamsHelper.ReplaceQueryString(queryString, StickyParameter.QStringVar.zBackTo.ToString(), "attendance.aspx");

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

        public override DataSet GetDataSet()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
