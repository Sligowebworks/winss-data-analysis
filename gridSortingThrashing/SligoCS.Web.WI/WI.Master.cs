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

using SligoCS.Web.WI.DAL.DataSets;
using SligoCS.Web.Base.WebServerControls.WI;

using SligoCS.BL.WI;
using SligoCS.Web.Base.PageBase.WI;
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

        private GlobalValues globals = null;

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
            globals = ((PageBaseWI)Page).GlobalValues;

            //set the URLs for the left hand hyperlinks
             if (!Page.IsPostBack)
            {
                //do not change these- see bug 841 && 888.
                //linkSchool.ParamValue = globalValues.FULLKEY;
                ////linkDistrict.ParamValue = FullKeyUtils.GetMaskedFullkey(globalValues.FULLKEY, OrgLevel.District);
                ////linkState.ParamValue = FullKeyUtils.GetMaskedFullkey(globalValues.FULLKEY, OrgLevel.State);
                ////lnkRetention.NavigateUrl = QueryStringUtils.CreateURL("/SligoWI/GridPage.aspx", GlobalValues.QStringVar.STYP.ToString(), "3");
                //lnkRetention.NavigateUrl = QueryStringUtils.CreateURL(globalValues, "/SligoWI/GridPage.aspx", string.Empty, string.Empty);
                //lnkDropouts.NavigateUrl = QueryStringUtils.CreateURL(globalValues, "/SligoWI/Demo.aspx", string.Empty, string.Empty);
                //lnkAttendance.NavigateUrl = QueryStringUtils.CreateURL(globalValues, "/SligoWI/AttendancePage.aspx", string.Empty, string.Empty);
                //lnkHSCompletion.NavigateUrl = QueryStringUtils.CreateURL(globalValues, "/SligoWI/HSCompletionPage.aspx", string.Empty, string.Empty);
                //lnkTruancy.NavigateUrl = QueryStringUtils.CreateURL(globalValues, "/SligoWI/Truancy.aspx", string.Empty, string.Empty);
                //lnkTeacherQual.NavigateUrl = QueryStringUtils.CreateURL(globalValues, "/SligoWI/TeacherQualifications.aspx", string.Empty, string.Empty);
                //lnkAPTests.NavigateUrl = QueryStringUtils.CreateURL(globalValues, "/SligoWI/APTestsPage.aspx", GlobalValues.QStringVar.GraphFile.ToString(), "AP");
                //lnkACT.NavigateUrl = QueryStringUtils.CreateURL(globalValues, "/SligoWI/ACTPage.aspx", GlobalValues.QStringVar.GraphFile.ToString(), "ACT");
                //lnkGradReq.NavigateUrl = QueryStringUtils.CreateURL(globalValues, "/SligoWI/GradReqsPage.aspx", string.Empty, string.Empty);
                //lnkPostGrad.NavigateUrl = QueryStringUtils.CreateURL(globalValues, "/SligoWI/PostGradIntentPage.aspx", string.Empty, string.Empty);
                //lnkMoney.NavigateUrl = QueryStringUtils.CreateURL(globalValues, "/SligoWI/MoneyPage.aspx", string.Empty, string.Empty) + "&RATIO=REVENUE&CT=TC";
                //lnkStaff.NavigateUrl = ((string)(QueryStringUtils.CreateURL(globalValues, "/SligoWI/StaffPage.aspx", string.Empty, string.Empty) + "&RATIO=STUDENTSTAFF")).Replace("013619040022", "ZZZZZZZZZZZZ");
                
                // move ViewState setting from master to pages
                //Page.EnableViewState = false;
                
                SetLeftHandLinksText();
                SetDataLinks();
                SetHideShowNumbersLink();
                SetScatterplotLink();
                SetReadAboutLink();
                SetGlossaryLink();

                string qsChangeSchoolOrDistrict = globals.GetQueryString( 
                    StickyParameter.QStringVar.FULLKEY.ToString(), "ZZZZZZZZZZZZ");
                qsChangeSchoolOrDistrict = QueryStringUtils.ReplaceQueryString(qsChangeSchoolOrDistrict,
                    StickyParameter.QStringVar.OrgLevel.ToString(), globals.OrgLevel.Range[OrgLevelKeys.State]);
                qsChangeSchoolOrDistrict = QueryStringUtils.ReplaceQueryString(qsChangeSchoolOrDistrict,
                    StickyParameter.QStringVar.DN.ToString(), "None Chosen");
                qsChangeSchoolOrDistrict = QueryStringUtils.ReplaceQueryString(qsChangeSchoolOrDistrict,
                     StickyParameter.QStringVar.SN.ToString(), "None Chosen");
                
                this.ChangeSchoolOrDistrict.NavigateUrl = globals.CreateURL(
                   "~/selschool.aspx" , qsChangeSchoolOrDistrict);
                
                ViewTitle.Text = globals.OrgLevel.Key +" View";
                
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
            string queryString = globals.GetQueryString(string.Empty, string.Empty);
            string schoolType = string.Empty;

            BLDistrict district = new BLDistrict();
            if (Page is PageBaseWI)
            {
                PageBaseWI typedPage = (PageBaseWI)Page;
                typedPage.PrepBLEntity(district);
                //string schoolType = typedPage.GetSchoolTypeLabel(district.CompareToEnum, district.STYP).Replace("-", ":").Trim();

                //see Bug 406
                //districtText += district.GetDistrictName(globalValues.FULLKEY, district.CurrentYear);
                schoolText = GlobalValues.GetOrgName(OrgLevelKeys.School, globals.FULLKEY);
                districtText = GlobalValues.GetOrgName(OrgLevelKeys.District, globals.FULLKEY);
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
                queryString = QueryStringUtils.ReplaceQueryString(queryString, StickyParameter.QStringVar.OrgLevel.ToString(), globals.OrgLevel.Range[OrgLevelKeys.School]);

                BLSchool school = new BLSchool();
                v_Schools ds = school.GetSchool(globals.FULLKEY, school.CurrentYear);
                if (ds._v_Schools.Count == 1)
                {
                    schoolType = ds._v_Schools[0].schooltype;
                }

                queryString = QueryStringUtils.ReplaceQueryString(queryString, StickyParameter.QStringVar.STYP.ToString(),
                    schoolType);

                //school_NavigateUrl = QueryStringUtils.GetBaseQueryString(globalValues, string.Empty, string.Empty);
                if (Request.QueryString["OrgLevel"] != null && Request.QueryString["OrgLevel"].ToString() != string.Empty)
                {
                    if (globals.OrgLevel.Key == OrgLevelKeys.School)
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
                school_NavigateUrl = globals.CreateURL("~/SchoolScript.aspx" , 
                    QueryStringUtils.ReplaceQueryString(
                        QueryStringUtils.ReplaceQueryString(globals.GetBaseQueryString(), 
                        "SEARCHTYPE", "SC"),
                    "L", "0")
                    );                
            }
            linkSchool.NavigateUrl = school_NavigateUrl;

            // linkDistrict.Text = districtText;
            if (districtText != null && districtText.ToString() != String.Empty)
            {
                linkDistrict.Text = districtText;
                queryString = QueryStringUtils.ReplaceQueryString(queryString, globals.OrgLevel.Name, globals.OrgLevel.Range[OrgLevelKeys.District]);
                queryString = QueryStringUtils.ReplaceQueryString(queryString, globals.STYP.Name, globals.STYP.Range[STYPKeys.StateSummary]);
                
                if (Request.QueryString["OrgLevel"] != null)
                {
                    if (Request.QueryString["OrgLevel"].ToString() != string.Empty)
                    {

                        if (globals.OrgLevel.Key == OrgLevelKeys.District)
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

            if (globals.CompareTo.Key == CompareToKeys.SelSchools ||
                globals.CompareTo.Key == CompareToKeys.SelDistricts)
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
            string zGraph = globals.GraphFile.Key.ToString();
            string readAbout_NavigateUrl = string.Empty; 

            if (zGraph != null && zGraph.ToString() != String.Empty)
            {
                switch (zGraph.ToString())
                {
                    case GraphFileKeys.GRAPH_FILE_RETENTION:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/ret_q&a.html";
                        break;
                    //case  Constants.GRAPH_FILE_GGRADRATE:
                    //    readAbout_NavigateUrl = "http://dpi.wi.gov/spr/grad_q&a.html";
                    //    break;
                    case GraphFileKeys.GRAPH_FILE_HIGHSCHOOLCOMPLETION:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/grad_q&a.html";
                        break;
                    case GraphFileKeys.GRAPH_FILE_DROPOUTS:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/drop_q&a.html";
                        break;
                    case GraphFileKeys.GRAPH_FILE_ATTENDANCE:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/att_q&a.html";
                        break;
                    case GraphFileKeys.GRAPH_FILE_TRUANCY:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/tru_q&a.html";
                        break;
                    case GraphFileKeys.GRAPH_FILE_STAFF:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/staff_q&a.html";
                        break;
                    case GraphFileKeys.GRAPH_FILE_MONEY:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/money_q&a.html";
                        break;
                    case GraphFileKeys.GRAPH_FILE_ETHNICENROLL:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/tru_q&a.html";
                        break;
                    case GraphFileKeys.GRAPH_FILE_SUSPEXPINCIDENTS:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/discip_q&a.html";
                        break;
                    case GraphFileKeys.GRAPH_FILE_GEXPSERVICES:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/discip_q&a.html";
                        break;
                    case GraphFileKeys.GRAPH_FILE_GROUPS:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/demog_q&a.html";
                        break;
                    //case  Constants.GRAPH_FILE_SUSPEXP:
                    //    readAbout_NavigateUrl = "http://dpi.wi.gov/spr/discip_q&a.html";
                    //    break;
                    //case  Constants.GRAPH_FILE_SSACTIVITIES:
                    //    readAbout_NavigateUrl = "http://dpi.wi.gov/spr/activi_q&a.html";
                    //    break;
                    case GraphFileKeys.GRAPH_FILE_GGRADPLAN:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/post_q&a.html";
                        break;
                    case GraphFileKeys.GRAPH_FILE_AP:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/colleg_q&a.html";
                        break;
                    case GraphFileKeys.GRAPH_FILE_ACT:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/colleg_q&a.html";
                        break;
                    case GraphFileKeys.GRAPH_FILE_GGRADREQS:
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
                    case GraphFileKeys.GRAPH_FILE_TEACHERQUALIFICATIONS:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/spr/teach_q&a.html";
                        break;
                    case GraphFileKeys.GRAPH_FILE_DISABILITIES:
                        readAbout_NavigateUrl = "(http://dpi.wi.gov/spr/demog_q&a.html";
                        break;
                    case GraphFileKeys.GRAPH_FILE_GWRCT:
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
                if (globals != null)
                {
                    string detailVal = Request.QueryString["DETAIL"];
                    if (detailVal != "NO")
                    {
                        HideShowNumbers_NavigateUrl = globals.GetQueryString("DETAIL", "NO");
                        HideShowNumbers_Text = "Hide Numbers";
                    }
                    else
                    {
                        HideShowNumbers_NavigateUrl = globals.GetQueryString("DETAIL", "YES");
                        HideShowNumbers_Text = "Show Numbers";
                    }
                }
            }
            HideShowNumbers.NavigateUrl = HideShowNumbers_NavigateUrl;
            HideShowNumbers.Text = HideShowNumbers_Text;
            HideShowNumbers.Visible = true;
        }


        private void SetScatterplotLink()
        {
            string ScatterplotLink_Text = string.Empty;
            string ScatterplotLink_NavigateUrl = string.Empty;
            bool showScatterplotLink = false;

            if (Request.Path.ToUpper().
                    Contains("TEACHERQUALIFICATIONSSCATTERPLOT.ASPX"))
            {
                if (globals != null)
                {
                    ScatterplotLink_NavigateUrl = "TeacherQualifications.aspx" +
                        globals.GetQueryString("", "");
                            ScatterplotLink_Text = "Back to Bar Chart";
 
                }
                showScatterplotLink = true;
            }
            else if(Request.Path.ToUpper().
                    Contains("TEACHERQUALIFICATIONS.ASPX"))
            {
                ScatterplotLink_NavigateUrl = "TeacherQualificationsScatterPlot.aspx" +
                    globals.GetQueryString( "", "");
                ScatterplotLink_Text = "Scatterplot";
                showScatterplotLink = true;
            }

            ScatterplotLink.NavigateUrl =  ScatterplotLink_NavigateUrl;
            ScatterplotLink.Text = ScatterplotLink_Text;
            ScatterplotLink.Visible = showScatterplotLink;
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
            if (globals != null)
            {                            
                if (globals.TraceLevels != StickyParameter.TraceLevel.None)
                {
                    Response.Write(
                         Server.HtmlEncode(
                            QueryStringUtils.GetTrace(globals)
                     ));
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
