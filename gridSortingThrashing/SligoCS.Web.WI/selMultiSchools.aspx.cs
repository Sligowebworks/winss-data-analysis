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

using SligoCS.Web.WI.DAL.DataSets;
using SligoCS.BL.WI;
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.WI.WebSupportingClasses.WI;
using SligoCS.Web.WI.HelperClasses;
using StickyParamsEnum =
        SligoCS.Web.WI.WebSupportingClasses.WI.StickyParameter.QStringVar;

namespace SligoCS.Web.WI
{
    /// <summary>
    /// Compare your school with up to four other schools or 
    /// with all schools in any location you choose. 
    /// </summary>
    public partial class selMultiSchools : PageBaseWI
    {
        protected v_AgencyFull _ds = null;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Master.EnableViewState = true;
            
            if (!Page.IsPostBack)
            {
                populateDropdownlists();
                setRadioButtons();
            }
            set_state();
            buildSchoolList();
            
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

        protected void buildSchoolList()
        {
            NameValueCollection selectedSchoolList = (NameValueCollection)
                 HttpContext.Current.Session[SligoCS.BL.WI.Constants.
                    MULTI_SCHOOL_SCHOOL_LIST_IN_SESSION];
            if (selectedSchoolList != null && selectedSchoolList.Count > 0)
            {
                IEnumerator enu = selectedSchoolList.Keys.GetEnumerator();
                while (enu.MoveNext())
                {
                    string key = (enu.Current).ToString();
                    TableRow newRow = new TableRow();
                    TableCell newCell = new TableCell();
                    newCell.ColumnSpan = 2;
                    newCell.CssClass = "smtext";
                    newCell.BorderStyle = BorderStyle.Double;
                    newCell.BorderWidth = 2;
                    newCell.Text = "<b>/" + selectedSchoolList[key].Trim() + "</b>";

                    newRow.Cells.Add(newCell);
                    TableSchoolList.Rows.Add(newRow);
                }
            }
        }

        private void populateDropdownlists()
        {
            BLAgencyFull agencyBL = new BLAgencyFull();

            // get initial selected value of dropdown list
            string fullKey = Request.QueryString[StickyParamsEnum.FULLKEY.ToString()].ToString();
            v_AgencyFull agencyDS = agencyBL.GetAgencyByFullKey(fullKey);
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
                if (string.IsNullOrEmpty(selectedConferenceKey) == false)
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

            GlobalValues.SQL = countySQL + "     " + AthleticConfSQL + "      " + CESASQL + "   ";
        }
        
        private void setRadioButtons()
        {
            if (HttpContext.Current.Session
                [SligoCS.BL.WI.Constants.MULTI_SCHOOL_FOUR_SCHOOL] != null)
            {
                string fourSchool = HttpContext.Current.Session
                    [SligoCS.BL.WI.Constants.MULTI_SCHOOL_FOUR_SCHOOL].ToString();
                if (fourSchool == SligoCS.BL.WI.Constants.FALSE) 
                {
                    Radio4Schools.Checked = false;
                    RadioAllSchools.Checked = true;
                }
            }
        }

        protected void CountyButton_Click(object sender, EventArgs e)
        {
            string link = string.Empty;
            string qs = GlobalValues.GetQueryString(
                        StickyParamsEnum.SCounty.ToString(),
                         CountyDropdownlist.SelectedValue.ToString());
            qs = QueryStringUtils.ReplaceQueryString(qs,
                    GlobalValues.S4orALL.Name,
                    GlobalValues.S4orALL.Range[S4orALLKeys.FourSchoolsOrDistrictsIn]);

            HttpContext.Current.Session
                [SligoCS.BL.WI.Constants.MULTI_SCHOOL_FOUR_SCHOOL]
                    = Radio4Schools.Checked.ToString();
            HttpContext.Current.Session
                [SligoCS.BL.WI.Constants.MULTI_SCHOOL_REGION_TYPE]
                    = SligoCS.BL.WI.Constants.MULTI_SCHOOL_REGION_TYPE_COUNTY;
            
            if (RadioAllSchools.Checked)
            {
                qs = QueryStringUtils.ReplaceQueryString(qs,
                    GlobalValues.S4orALL.Name,
                    GlobalValues.S4orALL.Range[S4orALLKeys.AllSchoolsOrDistrictsIn]);

                qs = QueryStringUtils.ReplaceQueryString(qs,
                    GlobalValues.SRegion.Name,
                    GlobalValues.SRegion.Range[SRegionKeys.County]);

                if (GlobalValues.GraphFile.Key != GraphFileKeys.BLANK_REDIRECT_PAGE)
                {
                    link = "~/" + GlobalValues.GraphFile.Key + qs;
                }
            }
            else
            {
                link = "~/selMultiSchoolsList.aspx" + qs;
            }
            
            Response.Redirect(link, true);       
        }

        protected void AthleticConferenceButton_Click(object sender, EventArgs e)
        {
            string link = string.Empty;

            string qs = GlobalValues.GetQueryString(
                StickyParamsEnum.SAthleticConf.ToString(),
                AthleticConferenceDropdownlist.SelectedValue.ToString());
            qs = QueryStringUtils.ReplaceQueryString(qs,
                    GlobalValues.S4orALL.Name,
                    GlobalValues.S4orALL.Range[S4orALLKeys.FourSchoolsOrDistrictsIn]);

            HttpContext.Current.Session
                [SligoCS.BL.WI.Constants.MULTI_SCHOOL_FOUR_SCHOOL]
                    = Radio4Schools.Checked.ToString();
            HttpContext.Current.Session
                [SligoCS.BL.WI.Constants.MULTI_SCHOOL_REGION_TYPE]
                    = SligoCS.BL.WI.Constants.MULTI_SCHOOL_REGION_TYPE_ATHL_CONF;
            
            if (RadioAllSchools.Checked)
            {
                qs = QueryStringUtils.ReplaceQueryString(qs,
                    GlobalValues.S4orALL.Name,
                    GlobalValues.S4orALL.Range[S4orALLKeys.AllSchoolsOrDistrictsIn]);

                qs = QueryStringUtils.ReplaceQueryString(qs,
                    GlobalValues.SRegion.Name,
                    GlobalValues.SRegion.Range[SRegionKeys.AthleticConf]);

                if (GlobalValues.GraphFile.Key != GraphFileKeys.BLANK_REDIRECT_PAGE)
                {
                    link = "~/" + GlobalValues.GraphFile.Key + qs;
                }

            }
            else
            {
                link = "~/selMultiSchoolsList.aspx" + qs;
            }
            Response.Redirect(link, true);
        }

        protected void CESAButton_Click(object sender, EventArgs e)
        {
            string link = string.Empty;

            string qs = GlobalValues.GetQueryString(
                StickyParamsEnum.SCESA.ToString(),
                CESADropdownlist.SelectedValue.ToString());
            qs = QueryStringUtils.ReplaceQueryString(qs,
                GlobalValues.S4orALL.Name,
                GlobalValues.S4orALL.Range[S4orALLKeys.FourSchoolsOrDistrictsIn]);

            HttpContext.Current.Session
                [SligoCS.BL.WI.Constants.MULTI_SCHOOL_FOUR_SCHOOL]
                    = Radio4Schools.Checked.ToString();
            HttpContext.Current.Session
                [SligoCS.BL.WI.Constants.MULTI_SCHOOL_REGION_TYPE]
                    = SligoCS.BL.WI.Constants.MULTI_SCHOOL_REGION_TYPE_CESA;
            
            if (RadioAllSchools.Checked)
            {
                qs = QueryStringUtils.ReplaceQueryString(qs,
                    GlobalValues.S4orALL.Name,
                    GlobalValues.S4orALL.Range[S4orALLKeys.AllSchoolsOrDistrictsIn]);

                qs = QueryStringUtils.ReplaceQueryString(qs,
                        GlobalValues.SRegion.Name,
                        GlobalValues.SRegion.Range[SRegionKeys.CESA]);

                if (GlobalValues.GraphFile.Key != GraphFileKeys.BLANK_REDIRECT_PAGE)
                {
                    link = "~/" + GlobalValues.GraphFile.Key + qs;
                }

            }
            else
            {
                link = "~/selMultiSchoolsList.aspx" + qs;
            }
            
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
                multiSchoolInSession );
            qs = QueryStringUtils.ReplaceQueryString(qs,
                    GlobalValues.S4orALL.Name,
                    GlobalValues.S4orALL.Range[S4orALLKeys.FourSchoolsOrDistrictsIn]);

            if (GlobalValues.GraphFile.Key != GraphFileKeys.BLANK_REDIRECT_PAGE)
            {
                string redirectedUrl = "~/" + GlobalValues.GraphFile.Key + qs + "&B2G=1";
                Response.Redirect(redirectedUrl, true);
            }

        }

        public override DataSet GetDataSet()
        {
            return _ds;
        }
    }
}
