using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SligoCS.DAL.WI.DataSets;
using SligoCS.BLL.WI;
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.Base.WebSupportingClasses.WI;
using SligoCS.Web.WI.HelperClasses;
using StickyParamsEnum =
        SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.QStringVar;

namespace SligoCS.Web.WI
{
    /// <summary>
    /// Compare your district with up to four other districts or 
    /// with all districts in any location you choose. 
    /// </summary>
    public partial class selMultiDistricts : PageBaseWI
    {
        protected v_AgencyFull _ds = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Master.EnableViewState = true;
            ParamsHelper.LoadFromContext(StickyParameter);

            if (!Page.IsPostBack)
            {
                populateDropdownlists();
                setRadioButtons();
            }
            set_state();
            buildDistrictList();
            
        }

        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                    (WI.displayed_obj.Big_Header_Graphics1, true);

            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                    (WI.displayed_obj.selMultiSchoolsDirections1, true);

            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                    (WI.displayed_obj.InitSelSchoolInfo1, false);

            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                    (WI.displayed_obj.linkSchoolDistrictPanel, false);

            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                    (WI.displayed_obj.DistrictMapInfo1, false);

            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                (WI.displayed_obj.DistrictInfo1, false);

            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                    (WI.displayed_obj.WI_DPI_Disclaim1, false);

            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                    (WI.displayed_obj.DataAnalysisInfo1, false);
        }

        protected void buildDistrictList()
        {
            NameValueCollection selectedDistrictList = (NameValueCollection)
                 HttpContext.Current.Session[SligoCS.BLL.WI.Constants.
                    MULTI_DISTRICT_DISTRICT_LIST_IN_SESSION];
            if (selectedDistrictList != null && selectedDistrictList.Count > 0)
            {
                IEnumerator enu = selectedDistrictList.Keys.GetEnumerator();
                while (enu.MoveNext())
                {
                    string key = (enu.Current).ToString();
                    TableRow newRow = new TableRow();
                    TableCell newCell = new TableCell();
                    newCell.ColumnSpan = 2;
                    newCell.CssClass = "smtext";
                    newCell.BorderStyle = BorderStyle.Double;
                    newCell.BorderWidth = 2;
                    newCell.Text = "<b>/" + selectedDistrictList[key].Trim() + "</b>";

                    newRow.Cells.Add(newCell);
                    TableDistrictList.Rows.Add(newRow);
                }
            }
        }

        private void populateDropdownlists()
        {
            BLAgencyFull agencyBL = new BLAgencyFull();

            // get initial selected value of dropdown list
            string fullKey = Request.QueryString[StickyParamsEnum.FULLKEY.ToString()].ToString();
            v_AgencyFull agencyDS = agencyBL.GetAgencyByFullKey( fullKey);
            string selectedCounty = string.Empty;
            string selectedConferenceKey = string.Empty;
            string selectedCESA = string.Empty;
            foreach (DataRow row in agencyDS.Tables[0].Rows)
            {
                selectedCounty = row[agencyDS._v_AgencyFull.CountyColumn.ColumnName].ToString();
                selectedConferenceKey = row[agencyDS._v_AgencyFull.ConferenceKeyColumn.ColumnName].ToString();
                selectedCESA = row[agencyDS._v_AgencyFull.CESAColumn.ColumnName].ToString();
            }

            // populate county dropdown list
            v_AgencyFull countyDS = agencyBL.GetCountyList();
            string countySQL = agencyBL.SQL;
            CountyDropdownlist.DataSource = countyDS;

            CountyDropdownlist.DataValueField = 
                countyDS._v_AgencyFull.CountyColumn.ColumnName;
            CountyDropdownlist.DataTextField = 
                countyDS._v_AgencyFull.CountyNameColumn.ColumnName;
            CountyDropdownlist.ClearSelection() ;
            CountyDropdownlist.DataBind();            
            //if (Request.QueryString[StickyParamsEnum.SCounty.ToString()] != null)
            //{
            //    CountyDropdownlist.SelectedValue = 
            //        Request.QueryString[StickyParamsEnum.SCounty.ToString()].ToString();
            //}
            //else 
                if (string.IsNullOrEmpty(selectedCounty) == false)
            {
                CountyDropdownlist.SelectedValue = selectedCounty;
            }

            // populate Athletic Conf dropdown list
            BLAthleticConf AthleticConfBL = new BLAthleticConf();
            v_Athletic_Conf AthleticConfDS = AthleticConfBL.GetAthleticConfList();
            string AthleticConfSQL = AthleticConfBL.SQL;
            AthleticConferenceDropdownlist.DataSource = AthleticConfDS;

            AthleticConferenceDropdownlist.DataValueField =
                AthleticConfDS._v_Athletic_Conf.ConferenceKeyColumn.ColumnName;
            AthleticConferenceDropdownlist.DataTextField =
                AthleticConfDS._v_Athletic_Conf.NameColumn.ColumnName;
            AthleticConferenceDropdownlist.ClearSelection();
            AthleticConferenceDropdownlist.DataBind();
            //if (Request.QueryString[StickyParamsEnum.SAthleticConf.ToString()] != null)
            //{
            //    AthleticConferenceDropdownlist.SelectedValue =
            //        Request.QueryString[StickyParamsEnum.SAthleticConf.ToString()].ToString();
            //}
            //else 
                if (string.IsNullOrEmpty ( selectedConferenceKey) == false ) 
            {
                AthleticConferenceDropdownlist.SelectedValue = selectedConferenceKey;
            }

            // populate CESA dropdown list
            v_AgencyFull CESADS = agencyBL.GetCESAList();
            string CESASQL = agencyBL.SQL;
            CESADropdownlist.DataSource = CESADS;

            CESADropdownlist.DataValueField =
                CESADS._v_AgencyFull.CESAColumn.ColumnName;
            CESADropdownlist.DataTextField =
                CESADS._v_AgencyFull.CESAColumn.ColumnName;
            CESADropdownlist.DataTextFormatString = "Cooperative Ed Serv Agcy {0}";
            CESADropdownlist.ClearSelection();
            CESADropdownlist.DataBind();
            //if (Request.QueryString[StickyParamsEnum.SCESA.ToString()] != null)
            //{
            //    CESADropdownlist.SelectedValue =
            //        Request.QueryString[StickyParamsEnum.SCESA.ToString()].ToString();
            //}
            //else 
                if (string.IsNullOrEmpty(selectedCESA) == false)
            {
                CESADropdownlist.SelectedValue = selectedCESA;
            }

            StickyParameter.SQL = countySQL + "     " + AthleticConfSQL + "      " + CESASQL + "   ";
        }
        
        private void setRadioButtons()
        {
            if (HttpContext.Current.Session
                [SligoCS.BLL.WI.Constants.MULTI_DISTRICT_FOUR_DISTRICT] != null)
            {
                string fourDistrict = HttpContext.Current.Session
                    [SligoCS.BLL.WI.Constants.MULTI_DISTRICT_FOUR_DISTRICT].ToString();
                if (fourDistrict == SligoCS.BLL.WI.Constants.FALSE) 
                {
                    Radio4Districts.Checked = false;
                    RadioAllDistricts.Checked = true;
                }
            }
        }

        protected void CountyButton_Click(object sender, EventArgs e)
        {
            string link = string.Empty;
            string qs = ParamsHelper.GetQueryString(StickyParameter,
                        StickyParamsEnum.SCounty.ToString(),
                         CountyDropdownlist.SelectedValue.ToString());
            qs = ParamsHelper.ReplaceQueryString(qs,
        StickyParamsEnum.S4orALL.ToString(),
         ((int)SligoCS.BLL.WI.S4orALL.FourSchoolsOrDistrictsIn).ToString());


            HttpContext.Current.Session
                [SligoCS.BLL.WI.Constants.MULTI_DISTRICT_FOUR_DISTRICT]
                    = Radio4Districts.Checked.ToString();
            HttpContext.Current.Session
                [SligoCS.BLL.WI.Constants.MULTI_DISTRICT_REGION_TYPE]
                    = SligoCS.BLL.WI.Constants.MULTI_DISTRICT_REGION_TYPE_COUNTY;
            
            if (RadioAllDistricts.Checked)
            {
                qs = ParamsHelper.ReplaceQueryString(qs,
                    StickyParamsEnum.S4orALL.ToString(),
                    ((int)SligoCS.BLL.WI.S4orALL.AllSchoolsOrDistrictsIn).ToString());

                qs = ParamsHelper.ReplaceQueryString(qs,
                    StickyParamsEnum.SRegion.ToString(),
                    ((int) SligoCS.BLL.WI.SRegion.County).ToString());

                if (StickyParameter.GraphFile.ToString() !=
                            Constants.BLANK_REDIRECT_PAGE)
                {
                    link = "~/" + PageUtil.GetMappedWebPage(StickyParameter.GraphFile) + qs;
                }
            }
            else
            {
                link = "~/selMultiDistrictsList.aspx" + qs;
            }
            
            Response.Redirect(link, true);       
        }

        protected void AthleticConferenceButton_Click(object sender, EventArgs e)
        {
            string link = string.Empty;

            string qs = ParamsHelper.GetQueryString(StickyParameter,
                StickyParamsEnum.SAthleticConf.ToString(),
                AthleticConferenceDropdownlist.SelectedValue.ToString());
            qs = ParamsHelper.ReplaceQueryString(qs,
        StickyParamsEnum.S4orALL.ToString(),
         ((int)SligoCS.BLL.WI.S4orALL.FourSchoolsOrDistrictsIn).ToString());


            HttpContext.Current.Session
                [SligoCS.BLL.WI.Constants.MULTI_DISTRICT_FOUR_DISTRICT]
                    = Radio4Districts.Checked.ToString();
            HttpContext.Current.Session
                [SligoCS.BLL.WI.Constants.MULTI_DISTRICT_REGION_TYPE]
                    = SligoCS.BLL.WI.Constants.MULTI_DISTRICT_REGION_TYPE_ATHL_CONF;
            
            if (RadioAllDistricts.Checked)
            {
                qs = ParamsHelper.ReplaceQueryString(qs,
                    StickyParamsEnum.S4orALL.ToString(),
                    ((int)SligoCS.BLL.WI.S4orALL.AllSchoolsOrDistrictsIn).ToString());

                qs = ParamsHelper.ReplaceQueryString(qs,
                    StickyParamsEnum.SRegion.ToString(),
                    ((int)SligoCS.BLL.WI.SRegion.AthleticConf).ToString());

                if (StickyParameter.GraphFile.ToString() !=
                            Constants.BLANK_REDIRECT_PAGE)
                {
                    link = "~/" + PageUtil.GetMappedWebPage(StickyParameter.GraphFile) + qs;
                }

            }
            else
            {
                link = "~/selMultiDistrictsList.aspx" + qs;
            }
            Response.Redirect(link, true);
        }

        protected void CESAButton_Click(object sender, EventArgs e)
        {
            string link = string.Empty;

            string qs = ParamsHelper.GetQueryString(StickyParameter,
                StickyParamsEnum.SCESA.ToString(),
                CESADropdownlist.SelectedValue.ToString());
            qs = ParamsHelper.ReplaceQueryString(qs,
        StickyParamsEnum.S4orALL.ToString(),
         ((int)SligoCS.BLL.WI.S4orALL.FourSchoolsOrDistrictsIn).ToString());


            HttpContext.Current.Session
                [SligoCS.BLL.WI.Constants.MULTI_DISTRICT_FOUR_DISTRICT]
                    = Radio4Districts.Checked.ToString();
            HttpContext.Current.Session
                [SligoCS.BLL.WI.Constants.MULTI_DISTRICT_REGION_TYPE]
                    = SligoCS.BLL.WI.Constants.MULTI_DISTRICT_REGION_TYPE_CESA;
            
            if (RadioAllDistricts.Checked)
            {
                qs = ParamsHelper.ReplaceQueryString(qs,
                    StickyParamsEnum.S4orALL.ToString(),
                    ((int)SligoCS.BLL.WI.S4orALL.AllSchoolsOrDistrictsIn).ToString());

                qs = ParamsHelper.ReplaceQueryString(qs,
                    StickyParamsEnum.SRegion.ToString(),
                    ((int)SligoCS.BLL.WI.SRegion.CESA).ToString());

                if (StickyParameter.GraphFile.ToString() !=
                            Constants.BLANK_REDIRECT_PAGE)
                {
                    link = "~/" + PageUtil.GetMappedWebPage(StickyParameter.GraphFile) + qs;
                }

            }
            else
            {
                link = "~/selMultiDistrictsList.aspx" + qs;
            }
            
            Response.Redirect(link, true);
        }

        protected void BackToGraph_Click(object sender, EventArgs e)
        {
            string multiDistrictInSession = string.Empty;
            if (HttpContext.Current.Session[SligoCS.BLL.WI.Constants.
                                    MULTI_DISTRICT_DISTRICT_KEYS] != null)
            {
                multiDistrictInSession =
                    HttpContext.Current.Session[SligoCS.BLL.WI.Constants.
                                    MULTI_DISTRICT_DISTRICT_KEYS].ToString();
            }
            string qs = ParamsHelper.GetQueryString(StickyParameter,
                StickyParamsEnum.SDistrictFullKeys.ToString(),
                multiDistrictInSession );
            qs = ParamsHelper.ReplaceQueryString(qs,
        StickyParamsEnum.S4orALL.ToString(),
         ((int)SligoCS.BLL.WI.S4orALL.FourSchoolsOrDistrictsIn).ToString());



            if (StickyParameter.GraphFile.ToString() !=
                        Constants.BLANK_REDIRECT_PAGE)
            {
                string redirectedUrl = "~/" +
                    PageUtil.GetMappedWebPage(StickyParameter.GraphFile) + qs + "&B2G=1";
                Response.Redirect(redirectedUrl, true);
            }

        }

        public override DataSet GetDataSet()
        {
            return _ds;
        }
    }
}
