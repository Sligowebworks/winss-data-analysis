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

        public questions()
        {
            //Change the standard StickyParameter into a more specialized one for use with this page.
            //BR: not need for a inhereted StickyParameter class, all sticky parameters goes to StickyParameter.cs
            this.StickyParameter = new StickyParameter();
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Master.EnableViewState = false;

            //StickyParameter typedStickyParameter = null;
            set_state();
            //typedStickyParameter = new StickyParameter();
            //this.StickyParameter = typedStickyParameter;
            //            ParamsHelper.LoadFromContext(StickyParameter);

            set_links();
            get_urls(base.StickyParameter);
            
            //BR: auto redirect
            if (StickyParameter.GraphFile.ToString() != Constants.BLANK_REDIRECT_PAGE)
            {
                string redirectedUrl = "~/" + 
                    PageUtil.GetMappedWebPage(StickyParameter.GraphFile) + 
                    ParamsHelper.GetQueryString(   StickyParameter,
                       StickyParameter.QStringVar.GraphFile.ToString(),
                       StickyParameter.GraphFile);
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
            ParamsHelper.LoadFromContext(StickyParameter);

            set_link_item(base.StickyParameter, "How are students performing academically?", "/SligoWI/" + link_items.performance + ".aspx", link_items.performance, "pic1");
            set_link_item(base.StickyParameter, "What programs, staff, and money are available?", "/SligoWI/" + link_items.offerings + ".aspx", link_items.offerings, "pic2");
            set_link_item(base.StickyParameter, "What about attendance and behavior?", "/SligoWI/" + link_items.attendance + ".aspx", link_items.attendance, "pic3");
            set_link_item(base.StickyParameter, "What are student demographics?", "/SligoWI/" + link_items.demographics + ".aspx", link_items.demographics, "pic4");
        }

        private void get_urls(StickyParameter myParams)
        {
            string myFullkey = StickyParameter.FULLKEY;
            if (myFullkey.IndexOf('Z') > 0) 
            {
                myFullkey = myFullkey.Substring(0, (myFullkey.IndexOf('Z')));
            }

            BLAgencyFull agencyFull = new BLAgencyFull();
            base.PrepBLEntity(agencyFull);
            _ds2 = agencyFull.GetSchool(myFullkey, 1996);

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
               + ParamsHelper.GetQueryString(StickyParameter, string.Empty, string.Empty);
            
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

        public override DataSet GetDataSet()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
