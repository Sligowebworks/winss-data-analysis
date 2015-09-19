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
    public partial class questions : PageBaseWI
    {
        public enum link_items
        {
            performance,
            offerings,
            attendance,
            demographics
        }

        protected DataSet _ds2 = null;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (GlobalValues.GraphFile.Key != GraphFileKeys.BLANK_REDIRECT_PAGE)
            {
                OnRedirectUser +=
                delegate()
                {
                    string redirectedUrl = "~/" + GlobalValues.GraphFile.Key + UserValues.GetBaseQueryString();
                    Response.Redirect(redirectedUrl, true);
                };
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            set_state();
            set_links();
            //get_urls(base.GlobalValues);
        }

        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.Big_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.InitSelSchoolInfo1, false);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.DistrictMapInfo1, false);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.DistrictInfo1, false);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.WI_DPI_Disclaim1, false);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.DataAnalysisInfo1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.School_br_Panel, false);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.District_br_Panel, false);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.State_br_Panel, false);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.ViewTitlePanel, false);
            ((SligoCS.Web.WI.WI)Page.Master).SetlinkSchoolDistrictPanelWidth(185);
            Page.EnableViewState = false;
        }

        private void set_links()
        {
            set_link_item(base.GlobalValues, "How are students performing academically?", "~/" + link_items.performance + ".aspx", link_items.performance, "pic1");
            set_link_item(base.GlobalValues, "What programs, staff, and money are available?", "~/" + link_items.offerings + ".aspx", link_items.offerings, "pic2");
            set_link_item(base.GlobalValues, "What about attendance and behavior?", "~/" + link_items.attendance + ".aspx", link_items.attendance, "pic3");
            set_link_item(base.GlobalValues, "What are student demographics?", "~/" + link_items.demographics + ".aspx", link_items.demographics, "pic4");
        }

        private void set_link_item(StickyParameter myParams, string link_text, string nav_url, link_items placeholder_name, string pic_name)
        {
            ContentPlaceHolder CPH = (ContentPlaceHolder)this.Page.Master.FindControl("ContentPlaceHolder1");
            
            string controlID = placeholder_name.ToString();

            Image myImage = new Image();
            myImage.AlternateText = link_text;
            myImage.ImageUrl = "~/images/" + pic_name + ".gif";
            myImage.Attributes.Add("name", pic_name);

            HyperLink item_link = new HyperLink();
            item_link.ID = placeholder_name + "_link";
            item_link.Text = link_text;
            item_link.NavigateUrl = nav_url
               + UserValues.GetBaseQueryString();
            
            item_link.Attributes.Add("onMouseOver", "img_hot('" + pic_name + "')");
            item_link.Attributes.Add("onMouseOut", "img_cool('" + pic_name + "')");
            item_link.Controls.Add(myImage);

            Control c = CPH.FindControl(controlID);
            if (c != null)
            {
                c.Controls.Add(item_link);
                c.Visible = true;
            }
        }
    }
}
