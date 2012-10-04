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

using SligoCS.DAL.WI;
using SligoCS.BL.WI;
using SligoCS.BL.WI.Utilities;
using SligoCS.Web.Base.PageBase.WI;

using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.Web.WI
{
    /// <summary>
    /// Compare your school with up to four other 
    /// schools or with all schools in any location you choose. 
    /// </summary>
    public partial class ChooseSelectedList : PageBaseWI
    {
        protected DataSet _ds = null;
        protected DataSet _dsSelected = null;
        protected string selectedFullkeys = string.Empty;
        
        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
        }
        protected override void OnAssociateCompareSelectedToOrgLevel(GlobalValues user, GlobalValues app)
        {
            //base.OnAssociateCompareSelectedToOrgLevel(user, app);
            //Disable auto-association of CompareSelected with OrgLevel
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            MessagePanel.Visible = false;
            Message.Text = String.Format(
                "Only four (4) {0} can be selected at a time. You must remove a currently selected {0} in the list on the right by clicking on the name before you can add another {0}.",
            (GlobalValues.CompareTo.Key == CompareToKeys.SelSchools) ? "schools" : "districts"
            );

            set_state();

            if (GlobalValues.CompareTo.Key == CompareToKeys.SelSchools)
                selectedFullkeys = GlobalValues.SSchoolFullKeys;
            if (GlobalValues.CompareTo.Key == CompareToKeys.SelDistricts)
                selectedFullkeys = GlobalValues.SDistrictFullKeys;

            selectedFullkeys = ProcessUserRequest( selectedFullkeys );

            PersistSelection(selectedFullkeys);

            populateAvailableSchoolsGrid(
                BLUtil.GetCommaDeliFullKeyString(selectedFullkeys + FullKeyUtils.GetMaskedFullkey(GlobalValues.FULLKEY, GlobalValues.OrgLevel))
            );
            populateSelectedGrid( 
                BLUtil.GetCommaDeliFullKeyString(selectedFullkeys)
            );
        }

        private void PersistSelection(String fullkeys)
        {
            //throw new Exception(fullkeys);
            if (GlobalValues.CompareTo.Key == CompareToKeys.SelSchools)
            {
                UserValues.SSchoolFullKeys = GlobalValues.SSchoolFullKeys = fullkeys;
                if (!UserValues.inQS.Contains("SSchoolFullKeys")) UserValues.inQS.Add("SSchoolFullKeys");
            }
            if (GlobalValues.CompareTo.Key == CompareToKeys.SelDistricts)
            {
                UserValues.SDistrictFullKeys = GlobalValues.SDistrictFullKeys = fullkeys;
                if (!UserValues.inQS.Contains("SDistrictFullKeys")) UserValues.inQS.Add("SDistrictFullKeys");
            }
        }

        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                (WI.displayed_obj.Big_Header_Graphics1, true);

            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                (WI.displayed_obj.LeftPanel, false);
        }

        private String ProcessUserRequest(String fullkeys)
        {
            if (fullkeys == null) fullkeys = String.Empty;

            if (!String.IsNullOrEmpty(Request.QueryString["Add"]) )
            {
                if (fullkeys.Length >= 48)
                {// 4 Already Selected, don't add
                    MessagePanel.Visible = true;
                    return fullkeys;
                }
               
                String added = QueryStringUtils.ContentFilterDecode(Request.QueryString["Add"]);
                fullkeys += added;
            }
            else if (!String.IsNullOrEmpty(fullkeys) && !String.IsNullOrEmpty(Request.QueryString["Rem"]))
            {
                String remove = QueryStringUtils.ContentFilterDecode(Request.QueryString["Rem"]);
                fullkeys = fullkeys.Replace(remove, "");
            }

            return fullkeys;
        }

        private void populateAvailableSchoolsGrid(String selectedAndCurrentCSV)
        {
            NoAppropriateLabel.Text = String.Format(
                "No appropriate {0} of this type in this location.",
                (GlobalValues.CompareTo.Key == CompareToKeys.SelSchools) ? "Schools" : "Districts"
            );

            if (!String.IsNullOrEmpty(selectedAndCurrentCSV)) 
            {
                GlobalValues.SQL = getAvailableQuery(selectedAndCurrentCSV);
                if (!String.IsNullOrEmpty(GlobalValues.SQL))
                {
                    //throw new Exception(GlobalValues.SQL);
                    QueryMarshaller.AssignQuery(new DALAgencies(), GlobalValues.SQL);
                    _ds = QueryMarshaller.Database.DataSet.Copy();
                }
            }
            if (_ds != null && _ds.Tables[0].Rows.Count > 0)
            {
                gvAvailable.DataSource = DataTableSorter.SortTable(_ds.Tables[0], "DistrictName, SchoolName");
                gvAvailable.DataBind();
                gvAvailable.ShowHeader = false;
            }
            else
            {
                NoAppropriateMessage.Visible = true;
                gvAvailable.Visible = false;
            }
        }

        private void populateSelectedGrid(String selectedCSV)
        {
            GlobalValues.SQL =
                (GlobalValues.CompareTo.Key == CompareToKeys.SelSchools) ?
                DALAgencies.GetSelectedSchoolsSQL(selectedCSV)
                : DALAgencies.GetSelectedDistrictsSQL(selectedCSV)
                ;
            QueryMarshaller.AssignQuery(new DALAgencies(), GlobalValues.SQL);
            _dsSelected = QueryMarshaller.Database.DataSet.Copy();

            if (_dsSelected != null && _dsSelected.Tables[0].Rows.Count > 0)
            {
                gvSelected.DataSource = DataTableSorter.SortTable(_dsSelected.Tables[0], "SchoolName, DistrictName");
                gvSelected.DataBind();
            }
            gvSelected.ShowHeader = false;
            PanelSelected.Visible = (gvSelected.Rows.Count > 0);
            PanelNoChosen.Visible = !PanelSelected.Visible;
        }

        private String getAvailableQuery(String selectedAndCurrent)
        {
            String Key = GlobalValues.SRegion.Key;
            String compare = GlobalValues.CompareTo.Key;
            String sql = String.Empty;

            if (compare == CompareToKeys.SelSchools)
            {
                if (Key == SRegionKeys.County)
                    sql = DALAgencies.GetSchoolsInCountySQL(GlobalValues.SCounty, int.Parse(GlobalValues.STYP.Value), selectedAndCurrent);//fullkeys to be excluded

                if (Key == SRegionKeys.AthleticConf)
                    sql = DALAgencies.GetSchoolsInAthleticConfSQL(Int32.Parse(GlobalValues.SAthleticConf), int.Parse(GlobalValues.STYP.Value), selectedAndCurrent); //fullkeys to be excluded

                if (Key == SRegionKeys.CESA)
                    sql = DALAgencies.GetSchoolsInCESASQL(GlobalValues.SCESA, int.Parse(GlobalValues.STYP.Value), selectedAndCurrent); //fullkeys to be excluded
            }

            if (compare == CompareToKeys.SelDistricts)
            {
                if (Key == SRegionKeys.County)
                    sql = DALAgencies.GetDistrictsInCountyAndExcludeListSQL(GlobalValues.SCounty, selectedAndCurrent);//fullkeys to be excluded

                if (Key == SRegionKeys.AthleticConf)
                    sql = DALAgencies.GetDistrictsInAthleticConfAndExcludeListSQL(Int32.Parse(GlobalValues.SAthleticConf), selectedAndCurrent); //fullkeys to be excluded

                if (Key == SRegionKeys.CESA)
                    sql = DALAgencies.GetDistrictsInCESASQL(GlobalValues.SCESA, selectedAndCurrent); //fullkeys to be excluded
            }

            return sql;
        }

        protected void ChooseFromAnother_Click(object sender, EventArgs e)
        {
            String link = "~/ChooseSelected.aspx" + UserValues.GetBaseQueryString();
            Response.Redirect(link, true);
        }

        protected void BackToGraph_Click(object sender, EventArgs e)
        {
            //to satisfy redirect prerequisites, let them compare to themself:
            if (String.IsNullOrEmpty(selectedFullkeys)) PersistSelection(GlobalValues.FULLKEY);

            string qs = UserValues.GetBaseQueryString();

            if (GlobalValues.GraphFile.Key != GraphFileKeys.BLANK_REDIRECT_PAGE)
            {
                string redirectedUrl = "~/" + GlobalValues.GraphFile.Key + qs + "&B2G=1";
                Response.Redirect(redirectedUrl, true);
            }
        }

        public string GetQueryStringForAdding(String name, String value)
        {
            return UserValues.GetQueryString(name, value);
        }
    }
}
