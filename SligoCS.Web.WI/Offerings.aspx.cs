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
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.WI.HelperClasses;
using SligoCS.Web.Base.WebSupportingClasses.WI;

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

            ParamsHelper.LoadFromContext(StickyParameter);
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
            set_list_item_link(base.StickyParameter, "What school-supported activities are offered?", "SSACTIVITIES", "/SligoWI/ActivitiesOffer.aspx", list_items.activities);
            set_list_item_link(base.StickyParameter, "What are the district requirements for high school graduation?", "GGRADREQS", "/SligoWI/GradReqsPage.aspx", list_items.requirements);
            set_list_item_link(base.StickyParameter, "What advanced courses are offered?", "GCOURSEOFFER", "/SligoWI/CoursesOffer.aspx", list_items.advanced);
            set_list_item_link(base.StickyParameter, "What staff are available in this district?", "STAFF", "/SligoWI/StaffPage.aspx", list_items.staff);
            set_list_item_link(base.StickyParameter, "What are the qualifications of teachers?", "TEACHERQUALIFICATIONS", "/SligoWI/TeacherQualifications.aspx", list_items.qualifications);
            set_list_item_link(base.StickyParameter, "How much money is received and spent in this district?", "MONEY", "/SligoWI/MoneyPage.aspx", list_items.money);
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

            queryString = ParamsHelper.ReplaceQueryString(queryString, StickyParameter.QStringVar.zBackTo.ToString(), "offerings.aspx");

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
