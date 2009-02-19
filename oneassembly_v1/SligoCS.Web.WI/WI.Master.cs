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

using SligoCS.DAL.WI.DataSets;
using SligoCS.Web.WI.WebServerControls.WI;

using SligoCS.BL.WI;
using SligoCS.Web.WI.PageBase.WI;
using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.Web.WI
{
    public partial class WI : System.Web.UI.MasterPage
    {

        public enum displayed_obj
        {
            DataAnalysisInfo1,
            AttendanceDataInfo1,
            DistrictInfo1,
            DistrictMapInfo1,
            InitSelSchoolInfo1,
            selMultiSchoolsDirections1,
            PlanningHelp1,
            StandardsPerformance1,
            StudentCharacteristics1,
            WI_DPI_Disclaim1,
            Big_Header_Graphics1,
            Small_Header_Graphics1,
            Mixed_Header_Graphics1,
            selMultiSchoolsDirections,
            linkSchoolDistrictPanel,
            dataLinksPanel,
            School_br_Panel,
            District_br_Panel,
            State_br_Panel, 
            ViewTitlePanel,
            LeftPanel
        }

        private StickyParameter stickyParameter = null;

        /// <summary>
        /// occurs before the Page_Load event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            if ((!Page.IsPostBack) && (Page is PageBaseWI))
            {
                //Load up the instance of StickyParameter from
                //  the current context (QString, then default XML).
                stickyParameter = ((PageBaseWI)Page).StickyParameter;
                ParamsHelper.LoadFromContext(stickyParameter);

                //TODO:  remove this when we hook up with Select School page.
                //stickyParameter.STYP = 3;  //bug 649

            }

        }

        public void SetPageHeading(string heading)
        {
            lblPageHeading.Text = heading;
        }

        public void SetlinkSchoolDistrictPanelWidth(Int32 width_value)
        {
            this.linkSchoolDistrictPanel.Width = width_value;
        }

        public void set_visible_state(displayed_obj objectToBeDisplayed, bool visible)
        {
            //By default, set all controls to .Visible = false first.

            string controlID = objectToBeDisplayed.ToString();
            Control c = this.FindControl(controlID);
            if (c != null)
            {
                c.Visible = visible;
            }
        }

        /// <summary>
        /// The master page's Page_Load event occurs AFTER the aspx page's Page_Load event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //set the URLs for the left hand hyperlinks
            if (stickyParameter != null)
            {
                //do not change these- see bug 841 && 888.
                //linkSchool.ParamValue = stickyParameter.FULLKEY;
                ////linkDistrict.ParamValue = EntityWIBase.GetMaskedFullkey(stickyParameter.FULLKEY, OrgLevel.District);
                ////linkState.ParamValue = EntityWIBase.GetMaskedFullkey(stickyParameter.FULLKEY, OrgLevel.State);
                ////lnkRetention.NavigateUrl = ParamsHelper.GetURL("/SligoWI/GridPage.aspx", StickyParameter.QStringVar.STYP.ToString(), "3");
                //lnkRetention.NavigateUrl = ParamsHelper.GetURL(stickyParameter, "/SligoWI/GridPage.aspx", string.Empty, string.Empty);
                //lnkDropouts.NavigateUrl = ParamsHelper.GetURL(stickyParameter, "/SligoWI/Demo.aspx", string.Empty, string.Empty);
                //lnkAttendance.NavigateUrl = ParamsHelper.GetURL(stickyParameter, "/SligoWI/AttendancePage.aspx", string.Empty, string.Empty);
                //lnkHSCompletion.NavigateUrl = ParamsHelper.GetURL(stickyParameter, "/SligoWI/HSCompletionPage.aspx", string.Empty, string.Empty);
                //lnkTruancy.NavigateUrl = ParamsHelper.GetURL(stickyParameter, "/SligoWI/Truancy.aspx", string.Empty, string.Empty);
                //lnkTeacherQual.NavigateUrl = ParamsHelper.GetURL(stickyParameter, "/SligoWI/TeacherQualifications.aspx", string.Empty, string.Empty);
                //lnkAPTests.NavigateUrl = ParamsHelper.GetURL(stickyParameter, "/SligoWI/APTestsPage.aspx", StickyParameter.QStringVar.GraphFile.ToString(), "AP");
                //lnkACT.NavigateUrl = ParamsHelper.GetURL(stickyParameter, "/SligoWI/ACTPage.aspx", StickyParameter.QStringVar.GraphFile.ToString(), "ACT");
                //lnkGradReq.NavigateUrl = ParamsHelper.GetURL(stickyParameter, "/SligoWI/GradReqsPage.aspx", string.Empty, string.Empty);
                //lnkPostGrad.NavigateUrl = ParamsHelper.GetURL(stickyParameter, "/SligoWI/PostGradIntentPage.aspx", string.Empty, string.Empty);
                //lnkMoney.NavigateUrl = ParamsHelper.GetURL(stickyParameter, "/SligoWI/MoneyPage.aspx", string.Empty, string.Empty) + "&RATIO=REVENUE&CT=TC";
                //lnkStaff.NavigateUrl = ((string)(ParamsHelper.GetURL(stickyParameter, "/SligoWI/StaffPage.aspx", string.Empty, string.Empty) + "&RATIO=STUDENTSTAFF")).Replace("013619040022", "ZZZZZZZZZZZZ");
                
                // move ViewState setting from master to pages
                //Page.EnableViewState = false;
                
                SetLeftHandLinksText();
                SetDataLinks();
                SetHideShowNumbersLink();
                SetReadAboutLink();
                SetGlossaryLink();

                string qsChangeSchoolOrDistrict = ParamsHelper.GetQueryString(stickyParameter, 
                    StickyParameter.QStringVar.FULLKEY.ToString(), "ZZZZZZZZZZZZ");
                qsChangeSchoolOrDistrict = ParamsHelper.ReplaceQueryString(qsChangeSchoolOrDistrict,
                    StickyParameter.QStringVar.ORGLEVEL.ToString(), "State");
                qsChangeSchoolOrDistrict = ParamsHelper.ReplaceQueryString(qsChangeSchoolOrDistrict,
                    StickyParameter.QStringVar.DN.ToString(), "None Chosen");
                qsChangeSchoolOrDistrict = ParamsHelper.ReplaceQueryString(qsChangeSchoolOrDistrict,
                     StickyParameter.QStringVar.SN.ToString(), "None Chosen");
                
                this.ChangeSchoolOrDistrict.NavigateUrl =
                   "~/selschool.aspx" + qsChangeSchoolOrDistrict;
                
                if (Request.QueryString["ORGLEVEL"] != string.Empty && Request.QueryString["ORGLEVEL"] != null)
                {
                    ViewTitle.Text = Request.QueryString["ORGLEVEL"]+" View";
                }
            }
            
            add_JS_image_manager();
            add_CssLink();
            add_JS_cookies();
            add_JS_popup();
        }

        // this function is here to replace the pop_up and transparency javascript in  
        // the WI.Master page and is called from Page_Load
        private void add_JS_popup()
        {
            HtmlGenericControl myJs = new HtmlGenericControl();
            myJs.TagName = "script";
            myJs.Attributes.Add("type", "text/javascript");
            myJs.Attributes.Add("language", "javascript");
            myJs.Attributes.Add("src", ResolveUrl("/SligoWI/js/trans_popup.js"));
            this.Page.Header.Controls.Add(myJs);
        }

        // this function is here to replace the cookie setting javascript in  
        // the WI.Master page and is called from Page_Load
        private void add_JS_cookies()
        {
            HtmlGenericControl myJs = new HtmlGenericControl();
            myJs.TagName = "script";
            myJs.Attributes.Add("type", "text/javascript");
            myJs.Attributes.Add("language", "javascript");
            myJs.Attributes.Add("src", ResolveUrl("/SligoWI/js/cookies.js"));
            this.Page.Header.Controls.Add(myJs);
        }

        // this function is here to replace the image rollover javascript in  
        // the WI.Master page and is called from Page_Load
        private void add_JS_image_manager()
        {
            HtmlGenericControl myJs = new HtmlGenericControl();
            myJs.TagName = "script";
            myJs.Attributes.Add("type", "text/javascript");
            myJs.Attributes.Add("language", "javascript");
            myJs.Attributes.Add("src", ResolveUrl("/SligoWI/js/image_manager.js"));
            this.Page.Header.Controls.Add(myJs);
        }

        // this function is here to replace the css link script in  
        // the WI.Master page and is called from Page_Load
        private void add_CssLink()
        {
            HtmlGenericControl myCssLink = new HtmlGenericControl();
            myCssLink.TagName = "LINK";
            myCssLink.Attributes.Add("REL", "stylesheet");
            myCssLink.Attributes.Add("HREF", "/SligoWI/dpi_pc.css");
            myCssLink.Attributes.Add("TYPE", "text/css");
            this.Page.Header.Controls.Add(myCssLink);
        }

        /// <summary>
        /// Sets the link text for "School", "District", and "State" on the left hand side of page.
        /// Per comments in bug 513 Comment 3
        /// </summary>
        private void SetLeftHandLinksText()
        {
            string schoolText = string.Empty;
            string districtText = string.Empty;
            string school_NavigateUrl = string.Empty;
            string district_NavigateUrl = string.Empty;
            string stateText = "<b>State</b>";
            string queryString = ParamsHelper.GetQueryString(stickyParameter, string.Empty, string.Empty);
            string schoolType = string.Empty;

            BLDistrict district = new BLDistrict();
            if (Page is PageBaseWI)
            {
                PageBaseWI typedPage = (PageBaseWI)Page;
                typedPage.PrepBLEntity(district);
                //string schoolType = typedPage.GetSchoolTypeLabel(district.CompareTo, district.SchoolType).Replace("-", ":").Trim();

                //see Bug 406
                //districtText += district.GetDistrictName(stickyParameter.FULLKEY, district.CurrentYear);
                schoolText = typedPage.GetOrgName(OrgLevel.School);
                districtText = typedPage.GetOrgName(OrgLevel.District, stickyParameter.FULLKEY);
                //districtText += schoolType;

                //stateText += schoolType;
            }
            //else
            //{
            //    districtText = "District";
            //}

            if (schoolText != null && schoolText.ToString() != String.Empty)
            {
                linkSchool.Text = schoolText;
                queryString = ParamsHelper.ReplaceQueryString(queryString, StickyParameter.QStringVar.ORGLEVEL.ToString(), "School");

                BLSchool school = new BLSchool();
                v_Schools ds = school.GetSchool(stickyParameter.FULLKEY, school.CurrentYear);
                if (ds._v_Schools.Count == 1)
                {
                    schoolType = ds._v_Schools[0].schooltype;
                }

                queryString = ParamsHelper.ReplaceQueryString(queryString, StickyParameter.QStringVar.STYP.ToString(),
                    schoolType);

                //school_NavigateUrl = ParamsHelper.GetQueryString(stickyParameter, string.Empty, string.Empty);
                if (Request.QueryString["ORGLEVEL"].ToString() != string.Empty && Request.QueryString["ORGLEVEL"] != null)
                {
                    if (Request.QueryString["ORGLEVEL"].ToString() == "School")
                    {
                        school_NavigateUrl = String.Empty;
                    }
                    else
                    {
                        school_NavigateUrl = queryString.ToString();
                    }
                }
            }
            else
            {
                linkSchool.Text = "Show Schools";
                school_NavigateUrl = "~/SchoolScript.aspx" + ParamsHelper.GetQueryString(stickyParameter, string.Empty, string.Empty) + "&SEARCHTYPE=SC&L=0";                
            }
            linkSchool.NavigateUrl = school_NavigateUrl;

            // linkDistrict.Text = districtText;
            if (districtText != null && districtText.ToString() != String.Empty)
            {
                linkDistrict.Text = districtText;
                queryString = ParamsHelper.ReplaceQueryString(queryString, StickyParameter.QStringVar.ORGLEVEL.ToString(), "District");
                queryString = ParamsHelper.ReplaceQueryString(queryString, StickyParameter.QStringVar.STYP.ToString(), "9");
                
                if (Request.QueryString["ORGLEVEL"] != null)
                {
                    if (Request.QueryString["ORGLEVEL"].ToString() != string.Empty)
                    {

                        if (Request.QueryString["ORGLEVEL"].ToString() == "District")
                        {
                            district_NavigateUrl = String.Empty;
                        }
                        else
                        {
                            district_NavigateUrl = queryString.ToString();
                        }
                    }
                }
            }
            else
            {
                linkDistrict.Text = "None Chosen";
            }
            linkDistrict.NavigateUrl = district_NavigateUrl;

            if (stickyParameter.CompareTo == CompareTo.SELSCHOOLS.ToString() ||
                stickyParameter.CompareTo == CompareTo.SELDISTRICTS.ToString())
            {
                linkState.Visible = false;
            }
            else 
            { 
                linkState.ForeColor = System.Drawing.Color.White;
                linkState.Text = stateText;
            }
        }

        private void SetDataLinks()
        {
            string districtURL = string.Empty;
            string schoolURL = string.Empty;
            schoolHomePage.Text = "School Home Page";
            districtHomePage.Text = "District Home Page";

            if (HttpContext.Current.Session[SligoCS.BL.WI.Constants.DISTRICT_PAGE_URL] != null &&
                HttpContext.Current.Session[SligoCS.BL.WI.Constants.DISTRICT_PAGE_URL].ToString() != String.Empty)
            {
                districtURL = HttpContext.Current.Session[SligoCS.BL.WI.Constants.DISTRICT_PAGE_URL].ToString();
            }

            if (HttpContext.Current.Session[SligoCS.BL.WI.Constants.SCHOOL_PAGE_URL] != null &&
                HttpContext.Current.Session[SligoCS.BL.WI.Constants.SCHOOL_PAGE_URL].ToString() != String.Empty)
            {
                schoolURL = HttpContext.Current.Session[SligoCS.BL.WI.Constants.SCHOOL_PAGE_URL].ToString();
            }

            if (schoolURL == String.Empty)
            {
                schoolHomePage.Visible = false;
            }

            //districtHomePage.NavigateUrl = districtURL;
            districtHomePage.NavigateUrl = "javascript:void(0)";
            districtHomePage.Attributes.Add("Onclick", "popup('" + districtURL + "'); return true;");
            districtHomePage.Attributes.Add("onmouseover", "window.status='"+districtURL+"'; return true;");
            districtHomePage.Attributes.Add("onmouseout", "window.status=''; return true;");


            // schoolHomePage.NavigateUrl = schoolURL;
            schoolHomePage.NavigateUrl = "javascript:void(0)";
            schoolHomePage.Attributes.Add("Onclick", "popup('" + schoolURL + "'); return true;");
            schoolHomePage.Attributes.Add("onmouseover", "window.status='" + schoolURL + "'; return true;");
            schoolHomePage.Attributes.Add("onmouseout", "window.status=''; return true;");
        }

        private void SetReadAboutLink()
        {
            string zGraph = Request.QueryString["GraphFile"];
            string readAbout_NavigateUrl = string.Empty; 

            if (zGraph != null && zGraph.ToString() != String.Empty)
            {
                switch (zGraph.ToString())
                {
                    case  Constants.GRAPH_FILE_RETENTION:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/ret_q&a.html";
                        break;
                    //case  Constants.GRAPH_FILE_GGRADRATE:
                    //    readAbout_NavigateUrl = "http://dpi.wi.gov/spr/grad_q&a.html";
                    //    break;
                    case  Constants.GRAPH_FILE_HIGHSCHOOLCOMPLETION:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/grad_q&a.html";
                        break;
                    case  Constants.GRAPH_FILE_DROPOUTS:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/drop_q&a.html";
                        break;
                    case  Constants.GRAPH_FILE_ATTENDANCE:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/att_q&a.html";
                        break;
                    case  Constants.GRAPH_FILE_TRUANCY:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/tru_q&a.html";
                        break;
                    case  Constants.GRAPH_FILE_STAFF:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/staff_q&a.html";
                        break;
                    case  Constants.GRAPH_FILE_MONEY:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/money_q&a.html";
                        break;
                    case  Constants.GRAPH_FILE_ETHNICENROLL:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/tru_q&a.html";
                        break;
                    case  Constants.GRAPH_FILE_SUSPEXPINCIDENTS:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/discip_q&a.html";
                        break;
                    case  Constants.GRAPH_FILE_GEXPSERVICES:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/discip_q&a.html";
                        break;
                    case  Constants.GRAPH_FILE_GROUPS:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/demog_q&a.html";
                        break;
                    //case  Constants.GRAPH_FILE_SUSPEXP:
                    //    readAbout_NavigateUrl = "http://dpi.wi.gov/spr/discip_q&a.html";
                    //    break;
                    //case  Constants.GRAPH_FILE_SSACTIVITIES:
                    //    readAbout_NavigateUrl = "http://dpi.wi.gov/spr/activi_q&a.html";
                    //    break;
                    case  Constants.GRAPH_FILE_GGRADPLAN:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/post_q&a.html";
                        break;
                    case  Constants.GRAPH_FILE_AP:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/colleg_q&a.html";
                        break;
                    case  Constants.GRAPH_FILE_ACT:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/colleg_q&a.html";
                        break;
                    case  Constants.GRAPH_FILE_GGRADREQS:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/gradrq_q&a.html";
                        break;
                    //case  Constants.GRAPH_FILE_GCOURSEOFFER:
                    //    readAbout_NavigateUrl = "http://dpi.wi.gov/spr/course_q&a.html";
                    //    break;
                    //case  Constants.GRAPH_FILE_GCOURSETAKE:
                    //    readAbout_NavigateUrl = "http://dpi.wi.gov/spr/course_q&a.html";
                    //    break;
                    //case  Constants.GRAPH_FILE_TEACHERQUALIFICATIONSSCATTER:
                    //    readAbout_NavigateUrl = "http://dpi.wi.gov/spr/teach_q&a.html";
                    //    break;
                    case  Constants.GRAPH_FILE_TEACHERQUALIFICATIONS:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/teach_q&a.html";
                        break;
                    case  Constants.GRAPH_FILE_DISABILITIES:
                        readAbout_NavigateUrl = "(http://dpi.wi.gov/spr/demog_q&a.html";
                        break;
                    case  Constants.GRAPH_FILE_GWRCT:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/oea/profgenl.html#description";
                        break;
                    default:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/oea/kce_q&a.html";
                        break;
                }

                //readAbout.NavigateUrl = readAbout_NavigateUrl.ToString();
                readAbout.NavigateUrl = "javascript:void(0)";
                readAbout.Attributes.Add("Onclick", "popup('" + readAbout_NavigateUrl.ToString() + "'); return true;");
            readAbout.Attributes.Add("onmouseover", "window.status='" + readAbout_NavigateUrl.ToString() + "'; return true;");
            readAbout.Attributes.Add("onmouseout", "window.status=''; return true;");
            }
        }

        private void SetHideShowNumbersLink()
        {
            string HideShowNumbers_Text = string.Empty;
            string HideShowNumbers_NavigateUrl = string.Empty;

            if ((Request.QueryString["DETAIL"] != null) && (Request.QueryString["DETAIL"].ToString() != string.Empty))
            {
                if (stickyParameter != null)
                {
                    string detailVal = Request.QueryString["DETAIL"];
                    if (detailVal != "NO")
                    {
                        HideShowNumbers_NavigateUrl = ParamsHelper.GetQueryString(stickyParameter, "DETAIL", "NO");
                        HideShowNumbers_Text = "Hide Numbers";
                    }
                    else
                    {
                        HideShowNumbers_NavigateUrl = ParamsHelper.GetQueryString(stickyParameter, "DETAIL", "YES");
                        HideShowNumbers_Text = "Show Numbers";
                    }
                }
            }
            HideShowNumbers.NavigateUrl = HideShowNumbers_NavigateUrl;
            HideShowNumbers.Text = HideShowNumbers_Text;
            HideShowNumbers.Visible = true;
        }

        private void SetGlossaryLink()
        {
            string zBackTo = Request.QueryString["ZBackTo"];
            string Glossary_NavigateUrl = string.Empty;

            if (zBackTo != null && zBackTo.ToString() != String.Empty)
            {
                switch (zBackTo.ToString())
                {
                    case "performance.aspx":
                        Glossary_NavigateUrl = "http://dpi.wi.gov/winss/perfacademic_glossary.html";
                        break;
                    case "attendance.aspx":
                        Glossary_NavigateUrl = "http://dpi.wi.gov/winss/attendbehave_glossary.html";
                        break;
                    case "offerings.aspx":
                        Glossary_NavigateUrl = "http://dpi.wi.gov/winss/available_glossary.html";
                        break;
                    case "demographics.aspx":
                        Glossary_NavigateUrl = "http://dpi.wi.gov/winss/studentdemo_glossary.html";
                        break;
                    default:
                        Glossary_NavigateUrl = "http://dpi.wi.gov/winss/perfacademic_glossary.html";
                        break;
                }
            }
            //Glossary.NavigateUrl = Glossary_NavigateUrl;
            Glossary.NavigateUrl = "javascript:void(0)";
            Glossary.Attributes.Add("Onclick", "popup('" + Glossary_NavigateUrl + "'); return true;");
            Glossary.Attributes.Add("onmouseover", "window.status='" + Glossary_NavigateUrl + "'; return true;");
            Glossary.Attributes.Add("onmouseout", "window.status=''; return true;");
            Glossary.Visible = true;
        }


        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (stickyParameter != null)
            {                            
                string traceVals = ParamsHelper.GetTrace ( stickyParameter);
                if (traceVals != string.Empty)
                {
                    traceVals = Server.HtmlEncode(traceVals);
                    Response.Write(traceVals);
                }
            }

            linkSchool.ForeColor = System.Drawing.Color.White;
            linkDistrict.ForeColor = System.Drawing.Color.White;
            linkState.ForeColor = System.Drawing.Color.White;
        }

        protected void Page_Unload(object sender, EventArgs e) 
        {
            HttpContext.Current.Session[SligoCS.BL.WI.Constants.CACHED_QUERY_STRING] = "";
        }
    }
}
