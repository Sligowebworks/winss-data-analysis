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
        private GlobalValues user = null;

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
            string controlID = objectToBeDisplayed.ToString();
            Control c = this.FindControl(controlID);
            if (c != null)
            {
                c.Visible = visible;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            //set the URLs for the left hand hyperlinks
            if (!Page.IsPostBack)
            {
                SetLeftHandLinksText();
                SetDataLinks();
                SetHideShowNumbersLink();
                SetScatterplotLink();
                SetReadAboutLink();
                SetGlossaryLink();

                string qsChangeSchoolOrDistrict = user.GetQueryString(
                    "FULLKEY", FullKeyUtils.StateFullKey(globals.FULLKEY));
                qsChangeSchoolOrDistrict = QueryStringUtils.ReplaceQueryString(qsChangeSchoolOrDistrict,
                    globals.OrgLevel.Name, globals.OrgLevel.Range[OrgLevelKeys.State]);
                qsChangeSchoolOrDistrict = QueryStringUtils.ReplaceQueryString(qsChangeSchoolOrDistrict,
                    "DN", String.Empty);
                qsChangeSchoolOrDistrict = QueryStringUtils.ReplaceQueryString(qsChangeSchoolOrDistrict,
                     "SN", String.Empty);

                this.ChangeSchoolOrDistrict.NavigateUrl = globals.CreateURL("~/selschool.aspx", qsChangeSchoolOrDistrict);

                ViewTitle.Text = globals.OrgLevel.Key + " View";
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
            user = ((PageBaseWI)Page).UserValues;
            
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
            myJs.Attributes.Add("src", ResolveUrl("~/js/trans_popup.js"));
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
            myJs.Attributes.Add("src", ResolveUrl("~/js/cookies.js"));
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
            myJs.Attributes.Add("src", ResolveUrl("~/js/image_manager.js"));
            this.Page.Header.Controls.Add(myJs);
        }

        // this function is here to replace the css link script in  
        // the WI.Master page and is called from Page_Load
        private void add_CssLink()
        {
            HtmlGenericControl myCssLink = new HtmlGenericControl();
            myCssLink.TagName = "LINK";
            myCssLink.Attributes.Add("REL", "stylesheet");
            myCssLink.Attributes.Add("HREF", ResolveUrl("~/dpi_pc.css"));
            myCssLink.Attributes.Add("TYPE", "text/css");
            this.Page.Header.Controls.Add(myCssLink);
        }

        /// <summary>
        /// Sets the link text for "School", "District", and "State" on the left hand side of page.
        /// Per comments in bug 513 Comment 3
        /// </summary>
        private void SetLeftHandLinksText()
        {
            string schoolName = string.Empty;
            string districtName = string.Empty;
            string school_NavigateUrl = string.Empty;
            string district_NavigateUrl = string.Empty;
            string queryString = user.GetBaseQueryString();
            string schoolType = string.Empty;

            linkState.ForeColor = System.Drawing.Color.White;

            //if (Page is PageBaseWI)
             PageBaseWI typedPage = (PageBaseWI)Page;

             schoolName = typedPage.GlobalValues.Agency.Schoolname;
             districtName = typedPage.GlobalValues.Agency.DistrictName;
           
            if (!String.IsNullOrEmpty(schoolName))
            {
                linkSchool.Text = schoolName;
                queryString = QueryStringUtils.ReplaceQueryString(queryString, globals.OrgLevel.Name, globals.OrgLevel.Range[OrgLevelKeys.School]);

                if (globals.OrgLevel.Key != OrgLevelKeys.School && user.OrgLevel.Key == OrgLevelKeys.School)
                {//detect override of state
                    linkSchool.Visible = false;
                } 
                else if (globals.OrgLevel.Key == OrgLevelKeys.School)
                {
                    school_NavigateUrl = String.Empty;
                }
                else
                {
                    school_NavigateUrl = queryString.ToString();
                }
            }
            else
            {
                linkSchool.Text = "Show Schools";
                school_NavigateUrl = globals.CreateURL("~/SchoolScript.aspx" , globals.GetQueryString("SEARCHTYPE", "SC"));                
            }
            linkSchool.NavigateUrl = school_NavigateUrl;

            // linkDistrict.Text = districtName;
            if (!String.IsNullOrEmpty(districtName) && districtName.Trim() != "Entire State")
            {
                linkDistrict.Text = districtName;
                queryString = QueryStringUtils.ReplaceQueryString(queryString, globals.OrgLevel.Name, globals.OrgLevel.Range[OrgLevelKeys.District]);
                
                if (Request.QueryString["OrgLevel"] != null)
                {
                    if (Request.QueryString["OrgLevel"].ToString() != string.Empty)
                    {
                        if (globals.OrgLevel.Key != OrgLevelKeys.District && user.OrgLevel.Key == OrgLevelKeys.District)
                        {//detect override of state
                            linkDistrict.Visible = false;
                        }
                        else if (globals.OrgLevel.Key == OrgLevelKeys.District)
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
            if (globals.OrgLevel.Key != OrgLevelKeys.State && user.OrgLevel.Key == OrgLevelKeys.State)
            {
                // Detect an override
                linkState.Visible = false;
            }
        }

        private void SetDataLinks()
        {

            SetDataLink(
                districtHomePage,
                "District Home Page",
                globals.Agency.DistrictURL
            );
                  
            SetDataLink(
                schoolHomePage,
                "School Home Page",
                globals.Agency.SchoolURL
            );
           
        }
        private void SetDataLink(HyperLink link, String text, String url)
        {
            if (String.IsNullOrEmpty(url))
            {
                schoolHomePage.Visible = false;
                return;
            }

            link.Text = text;

            link.NavigateUrl = "javascript:void(0)";
            link.Attributes.Add("Onclick", "popup('" + url + "'); return true;");
            link.Attributes.Add("onmouseover", "window.status='" + url + "'; return true;");
            link.Attributes.Add("onmouseout", "window.status=''; return true;");
        }

        private void SetReadAboutLink()
        {
            string zGraph = globals.GraphFile.Key.ToString();
            string readAbout_NavigateUrl = string.Empty; 

            if (zGraph != null && zGraph.ToString() != String.Empty)
            {
                switch (zGraph.ToString())
                {
                    case GraphFileKeys.RETENTION:
                        readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_ret_q%26amp%3Ba";
                        break;
                    //case  Constants.GRAPH_FILE_GGRADRATE:
                    //    readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_grad_q%26a";
                    //    break;
                    case GraphFileKeys.HIGHSCHOOLCOMPLETION:
                        readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_grad_q%26a";
                        break;
                    case GraphFileKeys.DROPOUTS:
                        readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_drop_q%26amp%3Ba";
                        break;
                    case GraphFileKeys.ATTENDANCE:
                        readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_att_q%26amp%3Ba";
                        break;
                    case GraphFileKeys.TRUANCY:
                        readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_tru_q%26amp%3Ba";
                        break;
                    case GraphFileKeys.STAFF:
                        readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_staff_q&a";
                        break;
                    case GraphFileKeys.MONEY:
                        readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_money_q%26amp%3Ba";
                        break;
                    case GraphFileKeys.SUSPEXPINCIDENTS:
                        readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_discip_q%26amp%3Ba";
                        break;
                    case GraphFileKeys.SUSPENSIONS:
                        readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_discip_q%26amp%3Ba";
                        break;
                    case GraphFileKeys.EXPULSIONS:
                        readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_discip_q%26amp%3Ba";
                        break;
                    case GraphFileKeys.SUSPENSIONSDAYSLOST:
                        readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_discip_q%26amp%3Ba";
                        break;
                    case GraphFileKeys.EXPULSIONSDAYSLOST:
                        readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_discip_q%26amp%3Ba";
                        break;
                    case GraphFileKeys.GEXPLENGTH:
                        readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_discip_q%26amp%3Ba";
                        break;
                    case GraphFileKeys.GEXPSERVICES:
                        readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_discip_q%26amp%3Ba";
                        break;
                    case GraphFileKeys.GEXPRETURNS:
                        readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_discip_q%26amp%3Ba";
                        break;
                    case GraphFileKeys.GROUPS:
                        readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_demog_q%26amp%3Ba";
                        break;
                    //case  Constants.GRAPH_FILE_SUSPEXP:
                    //    readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_discip_q%26amp%3Ba";
                    //    break;
                    //case  Constants.GRAPH_FILE_SSACTIVITIES:
                    //    readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_activi_q%26amp%3Ba";
                    //    break;
                    case GraphFileKeys.ActivitiesPartic:
                        readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_activi_q%26amp%3Ba";
                        break;
                    case GraphFileKeys.ActivityOffer:
                        readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_activi_q%26amp%3Ba";
                        break;
                    case GraphFileKeys.POSTGRADPLAN:
                        readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_post_q%26amp%3Ba";
                        break;
                    case GraphFileKeys.AP:
                        readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_colleg_q%26amp%3Ba";
                        break;
                    case GraphFileKeys.ACT:
                        readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_colleg_q%26amp%3Ba";
                        break;
                    case GraphFileKeys.GGRADREQS:
                        readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_gradrq_q%26amp%3Ba";
                        break;
                    //case  Constants.GRAPH_FILE_GCOURSEOFFER:
                    //    readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_course_q%26amp%3Ba";
                    //    break;
                    //case  Constants.GRAPH_FILE_GCOURSETAKE:
                    //    readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_course_q%26amp%3Ba";
                    //    break;
                    //case  Constants.GRAPH_FILE_TEACHERQUALIFICATIONSSCATTER:
                    //    readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_teach_q%26a";
                    //    break;
                    case GraphFileKeys.TEACHERQUALIFICATIONS:
                    case GraphFileKeys.TQSCATTERPLOT:
                        readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_teach_q%26a";
                        break;
                    case GraphFileKeys.DISABILITIES:
                        readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_demog_q%26amp%3Ba";
                        break;
                    case GraphFileKeys.CoursesOffered:
                        readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_course_q%26amp%3Ba";
                        break;
                    case GraphFileKeys.CoursesTaken:
                        readAbout_NavigateUrl = "http://spr.dpi.wi.gov/spr_course_q%26amp%3Ba";
                        break;
                    case GraphFileKeys.GWRCT:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/oea_hist/wrct";
                        break;
                    default:
                        readAbout_NavigateUrl = "http://dpi.wi.gov/oea_kce_q%26amp%3Ba";
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

            //if ((Request.QueryString["DETAIL"] != null) && (Request.QueryString["DETAIL"].ToString() != string.Empty))
            //{
                if (globals != null)
                {
                    string detailVal = Request.QueryString["DETAIL"];
                    if (detailVal != "NO")
                    {
                        HideShowNumbers_NavigateUrl = user.GetQueryString("DETAIL", "NO");
                        HideShowNumbers_Text = "Hide Numbers";
                    }
                    else
                    {
                        HideShowNumbers_NavigateUrl = user.GetQueryString("DETAIL", "YES");
                        HideShowNumbers_Text = "Show Numbers";
                    }
                }
            //}
            HideShowNumbers.NavigateUrl = HideShowNumbers_NavigateUrl;
            HideShowNumbers.Text = HideShowNumbers_Text;
            HideShowNumbers.Visible = true;
        }


        private void SetScatterplotLink()
        {
            string linktext = string.Empty;
            string url = string.Empty;
            String page = Page.Request.FilePath;

            String[] nameParts = page.Split('/');
            int last = nameParts.GetLength(0) - 1;
            page = nameParts[last];

           if (
                page.ToUpper() == GraphFileKeys.TQSCATTERPLOT.ToUpper()
                || page.ToUpper() == GraphFileKeys.StateTestsScatter.ToUpper()
                )
               linktext = "Back to Bar Chart";

           else 
                linktext = "Scatterplot";

            if (page.ToUpper() == GraphFileKeys.TQSCATTERPLOT.ToUpper())
                url = GraphFileKeys.TEACHERQUALIFICATIONS + user.GetBaseQueryString();
            else if (page.ToUpper() == GraphFileKeys.TEACHERQUALIFICATIONS.ToUpper() && globals.OrgLevel.Key == OrgLevelKeys.State)
                url = "javascript:popup('http://www.dpi.wi.gov/winss_tq_scatter')";
            else if (page.ToUpper() == GraphFileKeys.TEACHERQUALIFICATIONS.ToUpper())
                url = GraphFileKeys.TQSCATTERPLOT + user.GetBaseQueryString();
            else if (page.ToUpper() == GraphFileKeys.CompareContinuing.ToUpper()
                   || page.ToUpper() == GraphFileKeys.StateTests.ToUpper()
                   || page.ToUpper() == GraphFileKeys.StateTestsSimilar.ToUpper())
            {
                url = (globals.OrgLevel.Key != OrgLevelKeys.State) ?
                    GraphFileKeys.StateTestsScatter + user.GetBaseQueryString() :
                    "javascript:popup('http://www.dpi.wi.gov/winss_kcescatter')"; ;
            }
            else if (page.ToUpper() == GraphFileKeys.StateTestsScatter.ToUpper())
                url = GraphFileKeys.StateTests + user.GetBaseQueryString();
            //url = "javascript:popup('http://www.dpi.wi.gov/winss_kcescatter')";
            else
            {
                url = "";
                ScatterplotLink.ForeColor = System.Drawing.Color.LightGray;
            }

            ScatterplotLink.NavigateUrl = url;
            ScatterplotLink.Text = linktext;
            // not sure this next line works - if link sometimes fails to appear, try forcing to true or commenting out 
            ScatterplotLink.Visible = !(String.IsNullOrEmpty(linktext));
        }

        private void SetGlossaryLink()
        {
            string quadrant = globals.Qquad;
            string Glossary_NavigateUrl = string.Empty;

            switch (quadrant)
            {
                case "performance.aspx":
                    Glossary_NavigateUrl = "http://winss.dpi.wi.gov/winss_perfacademic_glossary";
                    break;
                case "attendance.aspx":
                    Glossary_NavigateUrl = "http://winss.dpi.wi.gov/winss_attendbehave_glossary";
                    break;
                case "offerings.aspx":
                    Glossary_NavigateUrl = "http://winss.dpi.wi.gov/winss_available_glossary";
                    break;
                case "demographics.aspx":
                    Glossary_NavigateUrl = "http://winss.dpi.wi.gov/winss_studentdemo_glossary";
                    break;
                default:
                    Glossary_NavigateUrl = "http://winss.dpi.wi.gov/winss_perfacademic_glossary";
                    break;
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
                if (globals.TraceLevels != TraceStateUtils.TraceLevels.none)
                {
                    Response.Write(
                            TraceStateUtils.GetTrace(globals)
                     );
                }
            }

            linkSchool.ForeColor = System.Drawing.Color.White;
            linkDistrict.ForeColor = System.Drawing.Color.White;
            linkState.ForeColor = System.Drawing.Color.White;
        }        
    }
}
