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

using SligoCS.DAL.WI;
using SligoCS.BL.WI;
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.Web.WI
{
    /// <summary>
    /// Compare your school with up to four other schools or 
    /// with all schools in any location you choose. 
    /// </summary>
    public partial class ChooseSelected : PageBaseWI
    {
        const String paramFormat = "{0}={1}";
        protected override void OnAssociateCompareSelectedToOrgLevel(GlobalValues user, GlobalValues app)
        {
            //base.OnAssociateCompareSelectedToOrgLevel(user, app);
            //Disable auto-association of CompareSelected with OrgLevel
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Master.EnableViewState = true;

            SelectingTitle.Text = "Selecting Multiple " +
               ( (GlobalValues.CompareTo.Key == CompareToKeys.SelSchools) ? 
                "Schools"
                : "Districts")
            ;

            if (!Page.IsPostBack)
            {
                populateDropdownlists();
                setRadioButtons();
            }
            set_state();

            populateSelectedTable( buildSelectedList() );
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

        protected List<String> buildSelectedList()
        {
            List<String> selectedList = new List<String>();

            string fullkeys =
                (GlobalValues.CompareTo.Key == CompareToKeys.SelSchools) ?
                  GlobalValues.SSchoolFullKeys
                : GlobalValues.SDistrictFullKeys
            ;

            while(fullkeys != null && fullkeys.Length >= 12)
            {
                selectedList.Add(fullkeys.Substring(0,12));
                fullkeys = fullkeys.Remove(0, 12);
            }

            return selectedList;
        }

        protected void populateSelectedTable(List<String> selected)
        {
            if (selected .Count > 0)
            {
                GlobalValues.SQL =
                (GlobalValues.CompareTo.Key == CompareToKeys.SelSchools) ?
                DALAgencies.GetSelectedSchoolsSQL(GlobalValues.Year,
                    SligoCS.BL.WI.Utilities.BLUtil.GetCommaDeliFullKeyString(String.Join("", selected.ToArray())))
                : DALAgencies.GetSelectedDistrictsSQL(
                    SligoCS.BL.WI.Utilities.BLUtil.GetCommaDeliFullKeyString(String.Join("", selected.ToArray())))
                ;
                QueryMarshaller.AssignQuery(new DALAgencies(), GlobalValues.SQL);
                DataSet _dsSelected = QueryMarshaller.Database.DataSet.Copy();

                if (_dsSelected == null || _dsSelected.Tables[0].Rows.Count <= 0)
                    return;

                int distName = _dsSelected.Tables[0].Columns["DistrictName"].Ordinal;
                int schlName =  _dsSelected.Tables[0].Columns["SchoolName"].Ordinal;

                foreach (DataRow row in _dsSelected.Tables[0].Rows)
                {
                    TableRow newRow = new TableRow();
                    TableCell newCell = new TableCell();
                    newCell.ColumnSpan = 2;
                    newCell.CssClass = "smtext";
                    newCell.BorderStyle = BorderStyle.Double;
                    newCell.BorderWidth = 2;

                    if (GlobalValues.CompareTo.Key == CompareToKeys.SelSchools)
                        newCell.Text = "<b>" + row.ItemArray[distName] + "/" + row.ItemArray[schlName] + "</b>";
                    if (GlobalValues.CompareTo.Key == CompareToKeys.SelDistricts)
                        newCell.Text = "<b>/" + row.ItemArray[distName] + "</b>";

                    newRow.Cells.Add(newCell);
                    SelectedListTable.Rows.Add(newRow);
                }
            }
        }

        private void populateDropdownlists()
        {
            QueryMarshaller qm = QueryMarshaller;

            string selectedCounty = GlobalValues.Agency.County;
            string selectedConferenceKey = GlobalValues.Agency.ConferenceKey;
            string selectedCESA = GlobalValues.Agency.CESA;

            populateCountyDropdown(qm);
            populateAthleticConfDropdown(qm);
            populateCESADropdown(qm);

            if (string.IsNullOrEmpty(selectedCounty) == false)
            {
                CountyDropdownlist.SelectedValue = selectedCounty;
            }
            
            if (string.IsNullOrEmpty(selectedConferenceKey) == false)
            {
                AthleticConferenceDropdownlist.SelectedValue = selectedConferenceKey;
            }

            if (string.IsNullOrEmpty(selectedCESA) == false)
            {
                CESADropdownlist.SelectedValue = selectedCESA;
            }
        }

        private void populateCountyDropdown(QueryMarshaller qm)
        {
            GlobalValues.SQL = DALAgencies.GetCountyListSQL();
            qm.AssignQuery(new DALAgencies(), GlobalValues.SQL);

            CountyDropdownlist.DataSource = qm.Database.DataSet;
            CountyDropdownlist.DataValueField = v_AgencyFull.County;
            CountyDropdownlist.DataTextField = v_AgencyFull.CountyName;
            CountyDropdownlist.DataBind();            
        }

        private void populateAthleticConfDropdown(QueryMarshaller qm)
        {
            qm.AutoQuery(new DALAthleticConf());
            //trace:
            GlobalValues.SQL = qm.Database.SQL;

            AthleticConferenceDropdownlist.DataSource = qm.Database.DataSet;
            AthleticConferenceDropdownlist.DataValueField = v_Athletic_Conf.ConferenceKey;
            AthleticConferenceDropdownlist.DataTextField = v_Athletic_Conf.Name;
            AthleticConferenceDropdownlist.DataBind();
        }

        private void populateCESADropdown(QueryMarshaller qm)
        {
            GlobalValues.SQL  = DALAgencies.GetCESAListSQL();
            qm.AssignQuery(new DALAgencies(), GlobalValues.SQL);

            CESADropdownlist.DataSource = qm.Database.DataSet;
            CESADropdownlist.DataValueField = v_AgencyFull.CESA;
            CESADropdownlist.DataTextField = v_AgencyFull.CESA;
            CESADropdownlist.DataTextFormatString = "Cooperative Ed Serv Agcy {0}";
            CESADropdownlist.DataBind();
        }
        
        private void setRadioButtons()
        {
            Radio4Schools.Checked = (GlobalValues.S4orALL.Key == S4orALLKeys.FourSchoolsOrDistrictsIn);
            RadioAllSchools.Checked = (GlobalValues.S4orALL.Key == S4orALLKeys.AllSchoolsOrDistrictsIn);
        }

        protected void CountyButton_Click(object sender, EventArgs e)
        {
            List<String> parameters = new List<String>();

             parameters.Add(
                String.Format(paramFormat,
                "SCounty",
                 CountyDropdownlist.SelectedValue.ToString())
             );

             parameters.Add(
                 String.Format(paramFormat,
                 GlobalValues.SRegion.Name,
                 GlobalValues.SRegion.Range[SRegionKeys.County])
                 );

            RedirectUserAndSetS4OrAll(parameters);
        }

        protected void AthleticConferenceButton_Click(object sender, EventArgs e)
        {
            List<String> parameters = new List<String>();

            parameters.Add(
                String.Format(paramFormat,
                "SAthleticConf",
                AthleticConferenceDropdownlist.SelectedValue.ToString())
            );

            parameters.Add(
                String.Format(paramFormat,
                GlobalValues.SRegion.Name,
                GlobalValues.SRegion.Range[SRegionKeys.AthleticConf])
                );

            RedirectUserAndSetS4OrAll(parameters);
        }

        protected void CESAButton_Click(object sender, EventArgs e)
        {
            List<String> parameters = new List<String>();

            parameters.Add(
                String.Format(paramFormat,
                "SCESA",
                CESADropdownlist.SelectedValue.ToString())
            );

            parameters.Add(
                String.Format(paramFormat,
                GlobalValues.SRegion.Name,
                GlobalValues.SRegion.Range[SRegionKeys.CESA])
            );

            RedirectUserAndSetS4OrAll(parameters);
        }

        protected void StatewideButton_Click(object sender, EventArgs e)
        {
            List<String> parameters = new List<String>();

            parameters.Add(
                String.Format(paramFormat,
                GlobalValues.SRegion.Name,
                GlobalValues.SRegion.Range[SRegionKeys.Statewide])
            );

            RedirectUserAndSetS4OrAll(parameters);
        }

        private void RedirectUserAndSetS4OrAll(List<String> parameters)
        {
            String link;

            if (RadioAllSchools.Checked)
            {
                parameters.Add(
                    String.Format(paramFormat,
                    GlobalValues.S4orALL.Name,
                    GlobalValues.S4orALL.Range[S4orALLKeys.AllSchoolsOrDistrictsIn])
                 );

                link = "~/" + GlobalValues.GraphFile.Key;
            }
            else if (Radio4Schools.Checked)
            {
                parameters.Add(
                    String.Format(paramFormat,
                    GlobalValues.S4orALL.Name,
                    GlobalValues.S4orALL.Range[S4orALLKeys.FourSchoolsOrDistrictsIn])
                 );
                link = "~/ChooseSelectedList.aspx";
            }
            else
            {
                throw new Exception("S4 Or All not Set");
            }

            Response.Redirect(link + UserValues.GetQueryString(parameters.ToArray()), true);
        }

        protected void BackToGraph_Click(object sender, EventArgs e)
        {
            List<String> parameters = new List<string>();
            
            String selectedFullkeys = 
                (GlobalValues.CompareTo.Key == CompareToKeys.SelSchools)?
                  GlobalValues.SSchoolFullKeys
                : GlobalValues.SDistrictFullKeys
                ;
            String paramName = 
                (GlobalValues.CompareTo.Key == CompareToKeys.SelSchools)?
                "SSchoolFullKeys"
                : "SDistrictFullKeys"
            ;

            parameters.Add(
                String.Format(paramFormat,
                paramName,
                selectedFullkeys)
            );
            parameters.Add(
                    String.Format(paramFormat,
                    GlobalValues.S4orALL.Name,
                    GlobalValues.S4orALL.Range[S4orALLKeys.FourSchoolsOrDistrictsIn])
                 );
            parameters.Add("B2G=1");

            if (GlobalValues.GraphFile.Key != GraphFileKeys.BLANK_REDIRECT_PAGE)
                    Response.Redirect("~/" + GlobalValues.GraphFile.Key + UserValues.GetQueryString(parameters.ToArray()), true);

        }
    }
}