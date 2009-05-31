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
using SligoCS.Web.WI.DAL.DataSets;
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.WI.HelperClasses;
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

        protected v_AgencyFull _ds2 = null;
        protected BLAgencyFull _agency;

        protected void Page_Load(object sender, EventArgs e)
        {
            _agency = new BLAgencyFull();
            PrepBLEntity(_agency);
            set_state();
            set_links();
            get_urls(base.GlobalValues);
            
            //BR: auto redirect
            if (GlobalValues.GraphFile.Key != GraphFileKeys.BLANK_REDIRECT_PAGE)
            {
                string redirectedUrl = "~/" + GlobalValues.GraphFile.Key + UserValues.GetBaseQueryString();
                Response.Redirect(redirectedUrl, true);
            }
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
            set_link_item(base.GlobalValues, "How are students performing academically?", "/SligoWI/" + link_items.performance + ".aspx", link_items.performance, "pic1");
            set_link_item(base.GlobalValues, "What programs, staff, and money are available?", "/SligoWI/" + link_items.offerings + ".aspx", link_items.offerings, "pic2");
            set_link_item(base.GlobalValues, "What about attendance and behavior?", "/SligoWI/" + link_items.attendance + ".aspx", link_items.attendance, "pic3");
            set_link_item(base.GlobalValues, "What are student demographics?", "/SligoWI/" + link_items.demographics + ".aspx", link_items.demographics, "pic4");
        }

        private void get_urls(StickyParameter myParams)
        {
            string myFullkey = GlobalValues.FULLKEY;
            if (myFullkey.IndexOf('Z') > 0) 
            {
                myFullkey = myFullkey.Substring(0, (myFullkey.IndexOf('Z')));
            }

            _ds2 = _agency.GetSchool(myFullkey, 1996);

            try
            {
                HttpContext.Current.Session[SligoCS.BL.WI.Constants.DISTRICT_PAGE_URL] = _ds2.Tables[_ds2._v_AgencyFull.TableName].Rows[0]["DistrictWebAddress"].ToString();
            }
            catch
            {
                HttpContext.Current.Session[SligoCS.BL.WI.Constants.DISTRICT_PAGE_URL] = "http://www.milwaukee.k12.wi.us";
            }

            try
            {
                HttpContext.Current.Session[SligoCS.BL.WI.Constants.SCHOOL_PAGE_URL] = _ds2.Tables[_ds2._v_AgencyFull.TableName].Rows[0]["SchoolWebAddress"].ToString();
            }
            catch
            {
                HttpContext.Current.Session[SligoCS.BL.WI.Constants.SCHOOL_PAGE_URL] = "http://mpsportal.milwaukee.k12.wi.us";
            }
        }

        private void set_link_item(StickyParameter myParams, string link_text, string nav_url, link_items placeholder_name, string pic_name)
        {
            ContentPlaceHolder CPH = (ContentPlaceHolder)this.Page.Master.FindControl("ContentPlaceHolder1");
            
            string controlID = placeholder_name.ToString();

            Image myImage = new Image();
            myImage.AlternateText = link_text;
            myImage.ImageUrl = "/SligoWI/images/" + pic_name + ".gif";
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
