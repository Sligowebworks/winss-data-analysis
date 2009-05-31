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
using SligoCS.Web.WI.DAL.DataSets;
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.WI.HelperClasses;
using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.Web.WI
{
    public partial class SelSchool : PageBaseWI
    {
        protected v_AgencyFull _ds = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            set_state();
        }

        private void No_Records_Found(string label_text, string search_var)
        {
            if (_ds._v_AgencyFull.Count == 0)
            {
                No_Records_Found_Label.Text = label_text + "<i>" + search_var + "</i>.<br />Please try again. ";
                No_Records_Found_Label.Visible = true;
            }
        }

        private void set_state()
        {          
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.Big_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.linkSchoolDistrictPanel, false);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.InitSelSchoolInfo1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.DistrictMapInfo1, false);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.DistrictInfo1, false);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.WI_DPI_Disclaim1, false);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.DataAnalysisInfo1, false);
        }


        protected void btnDistrict_Click(object sender, EventArgs e)
        {
            string district_substr = string.Empty;

            if (this.DistrictName.Text != string.Empty)
            {
                district_substr = this.DistrictName.Text;

                pnl_sdc.Visible = false;
                pnl_CESA_Map_Control.Visible = false;
                SligoDistrictGrid.Visible = true;
                
                ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.InitSelSchoolInfo1, false);
                ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.DistrictMapInfo1, true);
                ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.DistrictInfo1, false);
                ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.WI_DPI_Disclaim1, true);

                BLAgencyFull DistrictSet = new BLAgencyFull();

                _ds = DistrictSet.GetDistrictBySubstr(district_substr);
                GlobalValues.SQL = DistrictSet.SQL;
                SligoDistrictGrid.DataSource = _ds;
                SligoDistrictGrid.DataBind();
                SligoDistrictGrid.AddSuperHeader("Searching in DISTRICT for <i>" + district_substr + "</i>");
                No_Records_Found("No Districts Found like ", district_substr);
            }
        }

        protected void btnSchool_Click(object sender, EventArgs e)
        {
            string school_substr = string.Empty;

            if (this.txtSchoolName.Text != string.Empty)
            {
                school_substr = this.txtSchoolName.Text;

                pnl_sdc.Visible = false;
                pnl_CESA_Map_Control.Visible = false;
                SligoSchoolGrid.Visible = true;

                ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.InitSelSchoolInfo1, false);
                ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.DistrictMapInfo1, true);
                ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.DistrictInfo1, false);
                ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.WI_DPI_Disclaim1, true);

                BLAgencyFull SchoolSet = new BLAgencyFull();
                
                _ds = SchoolSet.GetSchoolBySubstr(school_substr);
                GlobalValues.SQL = SchoolSet.SQL;
                SligoSchoolGrid.DataSource = _ds;
                SligoSchoolGrid.DataBind();
                SligoSchoolGrid.AddSuperHeader( "Searching in SCHOOL for <i>" + school_substr + "</i>");
                No_Records_Found("No Schools Found like ", school_substr);
            }
        }

        protected void btnCounty_Click(object sender, EventArgs e)
        {
            string county_substr = string.Empty;

            if (this.CountyName.Text != string.Empty)
            {
                county_substr = this.CountyName.Text;

                pnl_sdc.Visible = false;
                pnl_CESA_Map_Control.Visible = false;
                SligoCountyGrid.Visible = true;

                ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.InitSelSchoolInfo1, false);
                ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.DistrictMapInfo1, true);
                ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.DistrictInfo1, false);
                ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.WI_DPI_Disclaim1, true);

                BLAgencyFull CountySet = new BLAgencyFull();                
                
                _ds = CountySet.GetCountyBySubstr(county_substr);
                GlobalValues.SQL = CountySet.SQL;
                SligoCountyGrid.DataSource = _ds;
                SligoCountyGrid.DataBind();
                SligoCountyGrid.AddSuperHeader("Searching in COUNTY for <i>" + county_substr + "</i>");
                No_Records_Found("No Counties Found like ", county_substr);
            }
        }

        public string GetQueryStringForMultipleParams(
                    string fullkey,
                    string SchoolName,
                    string DistrictName,
                    string SchoolType,
                    string LowGrade,
                    string HighGrade,
                    string orgLevel
                    )
        {
            string result = String.Empty;

            // First get QueryString from GlobalValues, also set the new fullkey value 
            result = GlobalValues.GetQueryString(GlobalValues.QStringVar.FULLKEY.ToString(), fullkey);

            // then replace or append addtional params and values
            result = QueryStringUtils.ReplaceQueryString(result, GlobalValues.QStringVar.SN.ToString(), SchoolName);
            result = QueryStringUtils.ReplaceQueryString(result, GlobalValues.QStringVar.DN.ToString(), DistrictName);
            result = QueryStringUtils.ReplaceQueryString(result, GlobalValues.QStringVar.STYP.ToString(), SchoolType);
            result = QueryStringUtils.ReplaceQueryString(result, GlobalValues.QStringVar.LOWGRADE.ToString(), LowGrade);
            result = QueryStringUtils.ReplaceQueryString(result, GlobalValues.QStringVar.HIGHGRADE.ToString(), HighGrade);
            result = QueryStringUtils.ReplaceQueryString(result, GlobalValues.QStringVar.OrgLevel.ToString(), orgLevel);

            return result;
        }
    }
}
