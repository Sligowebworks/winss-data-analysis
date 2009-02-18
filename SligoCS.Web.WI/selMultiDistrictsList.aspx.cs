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

using SligoCS.DAL.WI.DataSets;
using SligoCS.BL.WI;
using SligoCS.BL.WI.Utilities;
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.WI.HelperClasses;
using SligoCS.Web.Base.WebSupportingClasses.WI;
using StickyParamsEnum =
        SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.QStringVar;


namespace SligoCS.Web.WI
{
    /// <summary>
    /// Compare your district with up to four other 
    /// districts or with all districts in any location you choose. 
    /// </summary>
    public partial class selMultiDistrictsList : PageBaseWI
    {
        protected v_AgencyFull _ds = null;
        protected v_AgencyFull _dsSelected = null;
        protected string newSDistrictFullKeys = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Master.EnableViewState = false;
            ParamsHelper.LoadFromContext(StickyParameter);
            MessagePanel.Visible = false;

            set_state();

            updateSDistrictFullKeys();

            populateAvailableDistrictsGrid();

        }

        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                (WI.displayed_obj.Big_Header_Graphics1, true);

            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                (WI.displayed_obj.LeftPanel, false);

        }

        private void updateSDistrictFullKeys()
        {
            newSDistrictFullKeys =
                Request.QueryString[StickyParamsEnum.SDistrictFullKeys.ToString()];
            if (Request.QueryString["Add"] !=null )
            {
                string added = Request.QueryString["Add"];
                if (newSDistrictFullKeys != null && newSDistrictFullKeys.Length > 47)
                {
                    MessagePanel.Visible = true;
                }
                else
                {
                    newSDistrictFullKeys = newSDistrictFullKeys + added;
                }
            }
            else if (Request.QueryString["Rem"] != null)
            {
                string removed = Request.QueryString["Rem"];
                newSDistrictFullKeys = newSDistrictFullKeys.Replace 
                    (removed, "");
            }
            HttpContext.Current.Session[
                SligoCS.BL.WI.Constants.MULTI_DISTRICT_DISTRICT_KEYS]
                    = newSDistrictFullKeys;
        }

        private void populateAvailableDistrictsGrid()
        {
            BLAgencyFull districts = new BLAgencyFull();

            if (HttpContext.Current.Session[SligoCS.BL.WI.Constants.
                                        MULTI_DISTRICT_REGION_TYPE] != null)
            {
                string multiDistrictInSession = string.Empty;
                if (HttpContext.Current.Session[SligoCS.BL.WI.Constants.
                                        MULTI_DISTRICT_DISTRICT_KEYS] != null)
                {
                    multiDistrictInSession = HttpContext.Current.Session
                        [SligoCS.BL.WI.Constants.MULTI_DISTRICT_DISTRICT_KEYS].ToString();
                }

                string selectedDistricts = 
                    BLUtil.GetCommaDeliFullKeyString(multiDistrictInSession);
                string selectedAndCurrentDistricts = multiDistrictInSession 
                    + Request.QueryString[StickyParamsEnum.FULLKEY.ToString()].ToString();
                selectedAndCurrentDistricts = BLUtil.GetCommaDeliFullKeyString
                         (selectedAndCurrentDistricts);
                if (GetStringFromSession(SligoCS.BL.WI.Constants.MULTI_DISTRICT_REGION_TYPE)
                        == SligoCS.BL.WI.Constants.MULTI_DISTRICT_REGION_TYPE_COUNTY)
                {
                    _ds = districts.GetDistrictsInCounty(
                        Request.QueryString[StickyParamsEnum.SCounty.ToString()],
                                selectedAndCurrentDistricts); //fullkeys to be excluded
                }

                if (GetStringFromSession(SligoCS.BL.WI.Constants.MULTI_DISTRICT_REGION_TYPE)
                        == SligoCS.BL.WI.Constants.MULTI_DISTRICT_REGION_TYPE_ATHL_CONF)
                {
                    _ds = districts.GetDistrictsInAthlicConf(Int32.Parse(
                        Request.QueryString[StickyParamsEnum.SAthleticConf.ToString()]),
                        selectedAndCurrentDistricts); //fullkeys to be excluded
                }

                if (GetStringFromSession(SligoCS.BL.WI.Constants.MULTI_DISTRICT_REGION_TYPE)
                        == SligoCS.BL.WI.Constants.MULTI_DISTRICT_REGION_TYPE_CESA)
                {
                    _ds = districts.GetDistrictsInCESA(
                        Request.QueryString[StickyParamsEnum.SCESA.ToString()].ToString(),
                                selectedAndCurrentDistricts); //fullkeys to be excluded
                }

                StickyParameter.SQL = districts.SQL;
               
                _dsSelected = districts.GetSelectedDistricts (selectedDistricts);
                
            }
            gvDistricts.DataSource = _ds;
            gvDistricts.DataBind();
            gvDistricts.ShowHeader = false;

            gvSelectedDistricts.DataSource = _dsSelected;
            gvSelectedDistricts.DataBind();
            gvSelectedDistricts.ShowHeader = false;

            NameValueCollection selectedDistrictList = new NameValueCollection();

            if (gvSelectedDistricts.Rows.Count > 0)
            {
                PanelSelectedDistricts.Visible = true;
                PanelNoChosen.Visible = false;
                foreach (DataRow row in _dsSelected.Tables[0].Rows)
                {
                    string key = row[_dsSelected._v_AgencyFull.FullKeyColumn.ColumnName].ToString();
                    string districtName =
                        row[_dsSelected._v_AgencyFull.DistrictNameColumn.ColumnName].ToString().Trim();
                    if (selectedDistrictList[key] == null)
                    {
                        selectedDistrictList.Add(key, districtName);
                    }
                    HttpContext.Current.Session[SligoCS.BL.WI.Constants.
                        MULTI_DISTRICT_DISTRICT_LIST_IN_SESSION] = selectedDistrictList;
                }
            }
            else
            {
                PanelNoChosen.Visible = true;
                PanelSelectedDistricts.Visible = false;
                HttpContext.Current.Session[SligoCS.BL.WI.Constants.
                    MULTI_DISTRICT_DISTRICT_LIST_IN_SESSION] = new NameValueCollection();
            }
        }

        protected void ChooseFromAnother_Click(object sender, EventArgs e)
        {
            string multiDistrictInSession = string.Empty;
            if (HttpContext.Current.Session[SligoCS.BL.WI.Constants.
                                    MULTI_DISTRICT_DISTRICT_KEYS] != null)
            {
                multiDistrictInSession =
                    HttpContext.Current.Session[SligoCS.BL.WI.Constants.
                                    MULTI_DISTRICT_DISTRICT_KEYS].ToString();
            }

            string qs = ParamsHelper.GetQueryString(StickyParameter,
                StickyParamsEnum.SDistrictFullKeys.ToString(),
                multiDistrictInSession);
            string link = "~/selMultiDistricts.aspx" + qs;
            Response.Redirect(link, true);
        }

        protected void BackToGraph_Click(object sender, EventArgs e)
        {
            string multiDistrictInSession = string.Empty;
            if (HttpContext.Current.Session[SligoCS.BL.WI.Constants.
                                    MULTI_DISTRICT_DISTRICT_KEYS] != null)
            {
                multiDistrictInSession =
                    HttpContext.Current.Session[SligoCS.BL.WI.Constants.
                                    MULTI_DISTRICT_DISTRICT_KEYS].ToString();
            }

            string qs = ParamsHelper.GetQueryString(StickyParameter,
                StickyParamsEnum.SDistrictFullKeys.ToString(),
                multiDistrictInSession);

            if (StickyParameter.GraphFile.ToString() !=
                        Constants.BLANK_REDIRECT_PAGE)
            {
                string redirectedUrl = "~/" +
                    PageUtil.GetMappedWebPage(StickyParameter.GraphFile) + qs + "&B2G=1";
                Response.Redirect(redirectedUrl, true);
            }
        }

        public string GetQueryStringForAdding()
        {
            string multiDistrictInSession = string.Empty;
            if (HttpContext.Current.Session[SligoCS.BL.WI.Constants.
                                    MULTI_DISTRICT_DISTRICT_KEYS] != null)
            {
                multiDistrictInSession =
                    HttpContext.Current.Session[SligoCS.BL.WI.Constants.
                                    MULTI_DISTRICT_DISTRICT_KEYS].ToString();
            }
            return ParamsHelper.GetQueryString(StickyParameter,
                StickyParamsEnum.SDistrictFullKeys.ToString(),
                multiDistrictInSession);
        }

        public override DataSet GetDataSet()
        {
            return _ds;
        }
    }
}
