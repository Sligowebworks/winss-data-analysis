using System;
using System.Data;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SligoCS.Web.WI.DAL.DataSets;
using SligoCS.BL.WI;
using SligoCS.BL.WI.Utilities;
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.WI.HelperClasses;
using SligoCS.Web.WI.WebSupportingClasses.WI;
using StickyParamsEnum =
        SligoCS.Web.WI.WebSupportingClasses.WI.StickyParameter.QStringVar;


namespace SligoCS.Web.WI
{
    /// <summary>
    /// Compare your school with up to four other 
    /// schools or with all schools in any location you choose. 
    /// </summary>
    public partial class selMultiSchoolsList : PageBaseWI
    {
        protected v_AgencyFull _ds = null;
        protected v_AgencyFull _dsSelected = null;
        protected string newSSchoolFullKeys = string.Empty;
        protected BLAgencyFull _schools;

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
            _schools = new BLAgencyFull();
            PrepBLEntity(_schools);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            MessagePanel.Visible = false;

            set_state();

            updateSSchoolFullKeys();

            populateAvailableSchoolsGrid();

        }

        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                (WI.displayed_obj.Big_Header_Graphics1, true);

            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                (WI.displayed_obj.LeftPanel, false);

        }

        private void updateSSchoolFullKeys()
        {
            newSSchoolFullKeys =
                Request.QueryString[StickyParamsEnum.SSchoolFullKeys.ToString()];
            if (Request.QueryString["Add"] !=null )
            {
                string added = Request.QueryString["Add"];
                if (newSSchoolFullKeys != null && newSSchoolFullKeys.Length > 47)
                {
                    MessagePanel.Visible = true;
                }
                else
                {
                    newSSchoolFullKeys = newSSchoolFullKeys + added;
                }
            }
            else if (Request.QueryString["Rem"] != null)
            {
                string removed = Request.QueryString["Rem"];
                newSSchoolFullKeys = newSSchoolFullKeys.Replace 
                    (removed, "");
            }
            HttpContext.Current.Session[
                SligoCS.BL.WI.Constants.MULTI_SCHOOL_SCHOOL_KEYS]
                    = newSSchoolFullKeys;
        }

        private void populateAvailableSchoolsGrid()
        {
            if (HttpContext.Current.Session[SligoCS.BL.WI.Constants.
                                        MULTI_SCHOOL_REGION_TYPE] != null)
            {
                string multiSchoolInSession = string.Empty;
                if (HttpContext.Current.Session[SligoCS.BL.WI.Constants.
                                        MULTI_SCHOOL_SCHOOL_KEYS] != null)
                {
                    multiSchoolInSession = HttpContext.Current.Session
                        [SligoCS.BL.WI.Constants.MULTI_SCHOOL_SCHOOL_KEYS].ToString();
                }

                string selectedSchools = 
                    BLUtil.GetCommaDeliFullKeyString(multiSchoolInSession);
                string selectedAndCurrentSchools = multiSchoolInSession 
                    + Request.QueryString[StickyParamsEnum.FULLKEY.ToString()].ToString();
                selectedAndCurrentSchools = BLUtil.GetCommaDeliFullKeyString
                    (selectedAndCurrentSchools);
                if (GetStringFromSession(SligoCS.BL.WI.Constants.MULTI_SCHOOL_REGION_TYPE)
                        == SligoCS.BL.WI.Constants.MULTI_SCHOOL_REGION_TYPE_COUNTY)
                {
                    _ds = _schools.GetSchoolsInCounty(
                        Request.QueryString[StickyParamsEnum.SCounty.ToString()],
                        Int32.Parse(Request.QueryString[StickyParamsEnum.STYP.ToString()]),
                                selectedAndCurrentSchools); //fullkeys to be excluded
                }

                if (GetStringFromSession(SligoCS.BL.WI.Constants.MULTI_SCHOOL_REGION_TYPE)
                        == SligoCS.BL.WI.Constants.MULTI_SCHOOL_REGION_TYPE_ATHL_CONF)
                {
                    _ds = _schools.GetSchoolsInAthlicConf(Int32.Parse(
                        Request.QueryString[StickyParamsEnum.SAthleticConf.ToString()]),
                        Int32.Parse(Request.QueryString[StickyParamsEnum.STYP.ToString()]),
                        selectedAndCurrentSchools); //fullkeys to be excluded
                }

                if (GetStringFromSession(SligoCS.BL.WI.Constants.MULTI_SCHOOL_REGION_TYPE)
                        == SligoCS.BL.WI.Constants.MULTI_SCHOOL_REGION_TYPE_CESA)
                {
                    _ds = _schools.GetSchoolsInCESA(
                        Request.QueryString[StickyParamsEnum.SCESA.ToString()].ToString(),
                        Int32.Parse(Request.QueryString[StickyParamsEnum.STYP.ToString()]),
                                selectedAndCurrentSchools); //fullkeys to be excluded
                }

                GlobalValues.SQL = _schools.SQL;
               
                _dsSelected = _schools.GetSelectedSchools (selectedSchools);
                
            }
            gvSchools.DataSource = _ds;
            gvSchools.DataBind();
            gvSchools.ShowHeader = false;

            gvSelectedSchools.DataSource = _dsSelected;
            gvSelectedSchools.DataBind();
            gvSelectedSchools.ShowHeader = false;

            NameValueCollection selectedSchoolList = new NameValueCollection();

            if (gvSelectedSchools.Rows.Count > 0)
            {
                PanelSelectedSchools.Visible = true;
                PanelNoChosen.Visible = false;
                foreach (DataRow row in _dsSelected.Tables[0].Rows)
                {
                    string key = row[_dsSelected._v_AgencyFull.FullKeyColumn.ColumnName].ToString();
                    string schoolName = 
                        row[_dsSelected._v_AgencyFull.DistrictNameColumn.ColumnName].ToString().Trim()
                        + " - " + row[_dsSelected._v_AgencyFull.SchoolNameColumn.ColumnName].ToString().Trim();
                    if (selectedSchoolList[key] == null)
                    {
                        selectedSchoolList.Add(key, schoolName);
                    }
                    HttpContext.Current.Session[SligoCS.BL.WI.Constants.
                        MULTI_SCHOOL_SCHOOL_LIST_IN_SESSION] = selectedSchoolList;
                }
            }
            else
            {
                PanelNoChosen.Visible = true;
                PanelSelectedSchools.Visible = false;
                HttpContext.Current.Session[SligoCS.BL.WI.Constants.
                    MULTI_SCHOOL_SCHOOL_LIST_IN_SESSION] = new NameValueCollection();
            }
        }

        protected void ChooseFromAnother_Click(object sender, EventArgs e)
        {
            string multiSchoolInSession = string.Empty;
            if (HttpContext.Current.Session[SligoCS.BL.WI.Constants.
                                    MULTI_SCHOOL_SCHOOL_KEYS] != null)
            {
                multiSchoolInSession =
                    HttpContext.Current.Session[SligoCS.BL.WI.Constants.
                                    MULTI_SCHOOL_SCHOOL_KEYS].ToString();
            }

            string qs = GlobalValues.GetQueryString(StickyParamsEnum.SSchoolFullKeys.ToString(),
                multiSchoolInSession);
            string link = "~/selMultiSchools.aspx" + qs;
            Response.Redirect(link, true);
        }

        protected void BackToGraph_Click(object sender, EventArgs e)
        {
            string multiSchoolInSession = string.Empty;
            if (HttpContext.Current.Session[SligoCS.BL.WI.Constants.
                                    MULTI_SCHOOL_SCHOOL_KEYS] != null)
            {
                multiSchoolInSession =
                    HttpContext.Current.Session[SligoCS.BL.WI.Constants.
                                    MULTI_SCHOOL_SCHOOL_KEYS].ToString();
            }

            string qs = GlobalValues.GetQueryString(
                StickyParamsEnum.SSchoolFullKeys.ToString(),
                multiSchoolInSession);

            if (GlobalValues.GraphFile.Key != GraphFileKeys.BLANK_REDIRECT_PAGE)
            {
                string redirectedUrl = "~/" + GlobalValues.GraphFile.Key + qs + "&B2G=1";
                Response.Redirect(redirectedUrl, true);
            }
        }

        public string GetQueryStringForAdding()
        {
            string multiSchoolInSession = string.Empty;
            if (HttpContext.Current.Session[SligoCS.BL.WI.Constants.
                                    MULTI_SCHOOL_SCHOOL_KEYS] != null)
            {
                multiSchoolInSession =
                    HttpContext.Current.Session[SligoCS.BL.WI.Constants.
                                    MULTI_SCHOOL_SCHOOL_KEYS].ToString();
            }
            return GlobalValues.GetQueryString(
                StickyParamsEnum.SSchoolFullKeys.ToString(),
                multiSchoolInSession);
        }
    }
}
