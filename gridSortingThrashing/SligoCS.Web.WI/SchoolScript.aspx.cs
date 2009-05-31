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
    public partial class SchoolScript : PageBaseWI
    {

        protected v_AgencyFull _ds = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            SligoDataGrids_not_visible();
            set_state();
            init_display();
        }

        private void init_display()
        {
            string alpha_fl = "A";
            string var_hs = string.Empty;
            string f_key = string.Empty;
            string s_type = string.Empty;

            if (Request.QueryString["HS"] != string.Empty)
            {
                var_hs = Request.QueryString["HS"];
            }

            if (Request.QueryString["L"] != string.Empty)
            {
                alpha_fl = Request.QueryString["L"];
            }

            if (Request.QueryString["FULLKEY"] != null)
            {
                f_key = Request.QueryString["FULLKEY"];
            }

            if (Request.QueryString["SEARCHTYPE"] != null)
            {
                s_type = Request.QueryString["SEARCHTYPE"];
            }

            Load_Display(s_type, alpha_fl, f_key, var_hs);
        }


        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.Big_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.linkSchoolDistrictPanel, false);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.InitSelSchoolInfo1, false);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.DistrictMapInfo1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.DistrictInfo1, false);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.WI_DPI_Disclaim1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.DataAnalysisInfo1, false);
        }
        
        protected void Load_Display(string s_type, string alpha_fl, string f_key, string var_hs) 
        {
            switch (s_type)
            {
                case "SC":
                    ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.InitSelSchoolInfo1, false);
                    ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.DistrictMapInfo1, false);
                    ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.DistrictInfo1, true);
                    ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.WI_DPI_Disclaim1, true);

                    if ((alpha_fl != string.Empty) && (f_key == "ZZZZZZZZZZZZ"))
                    // if (alpha_fl != string.Empty)
                    {
                        BLAgencyFull SchoolSet = new BLAgencyFull("DISTINCT");
                        base.PrepBLEntity(SchoolSet);
                        _ds = SchoolSet.GetSchoolByName(alpha_fl);
                        GlobalValues.SQL = SchoolSet.SQL;
                       
                        SligoDataGrid1.DataSource = _ds;
                        SligoDataGrid1.DataBind();
                        SligoDataGrid1.AddSuperHeader( "Searching in SCHOOL by Letter " + alpha_fl);
                        SligoDataGrid1.Visible = true;
                        No_Records_Found("No Schools Found Starting with ", alpha_fl);
                    }
                    else
                    {
                        BLAgencyFull DataSet = new BLAgencyFull("DISTINCT");
                        base.PrepBLEntity(DataSet);
                        _ds = DataSet.GetSchoolByDistrictKey(GlobalValues.FULLKEY);
                        GlobalValues.SQL = DataSet.SQL;
                        SligoDataGrid4.DataSource = _ds;
                        SligoDataGrid4.DataBind();
                        try
                        {
                            SligoDataGrid4.AddSuperHeader("Searching Schools in DISTRICT, " + _ds.Tables[_ds._v_AgencyFull.TableName].Rows[0]["DistrictName"].ToString());
                        }
                        catch
                        {
                        }
                        SligoDataGrid4.Visible = true;
                    }
                    break;
                case "DI":
                    BLAgencyFull DistrictSet = new BLAgencyFull("DISTINCT");
                    base.PrepBLEntity(DistrictSet);
                    _ds = DistrictSet.GetDistrictByName(alpha_fl);
                    GlobalValues.SQL = DistrictSet.SQL;
                    SligoDataGrid2.DataSource = _ds;
                    SligoDataGrid2.DataBind();
                    SligoDataGrid2.AddSuperHeader("Searching in DISTRICT by Letter " + alpha_fl);

                    SligoDataGrid2.Visible = true;
                    No_Records_Found("No Districts Found Starting with ", alpha_fl);
                    break;
                case "CO":
                    BLAgencyFull CountySet = new BLAgencyFull("DISTINCT");
                    base.PrepBLEntity(CountySet);
                    _ds = CountySet.GetCountyByName(alpha_fl);
                    GlobalValues.SQL = CountySet.SQL;
                    SligoDataGrid3.DataSource = _ds;
                    SligoDataGrid3.DataBind();
                    SligoDataGrid3.AddSuperHeader("Searching in COUNTY by Letter " + alpha_fl);
                    SligoDataGrid3.Visible = true;
                    No_Records_Found("No Counties Found Starting with ", alpha_fl);
                    break;
                case "CE":
                    if (alpha_fl.Length == 1)
                    {
                        string pad = "0";
                        alpha_fl = pad + alpha_fl;
                    }
                    CESA_maps(alpha_fl, var_hs);

                    BLAgencyFull DistrictCESASet = new BLAgencyFull("DISTINCT");
                    base.PrepBLEntity(DistrictCESASet);
                    _ds = DistrictCESASet.GetDistrictByCESA(alpha_fl, var_hs);
                    GlobalValues.SQL = DistrictCESASet.SQL;
                    SligoDataGrid5.DataSource = _ds;
                    SligoDataGrid5.DataBind();

                    SligoDataGrid5.AddSuperHeader( "Results for CESA " + alpha_fl);

                    if ((var_hs == "1") || (var_hs == "0"))
                    {
                        if (var_hs == "1")
                        {
                            SligoDataGrid5.AddSuperHeader( SligoDataGrid5.SuperHeaderText + " with Union High School Districts");
                        }
                        else
                        {
                            SligoDataGrid5.AddSuperHeader(SligoDataGrid5.SuperHeaderText + " with Elementary School Districts");
                        }
                    }

                    SligoDataGrid5.Visible = true;
                    break;
            }        
        }

        private void No_Records_Found(string label_text, string search_var)
        {
            if (_ds._v_AgencyFull.Count == 0)
            {
                Label1.Text = label_text + "<i>" + search_var + "</i>.<br />Please try again.";
                Label1.Visible = true;
            }
        }

        protected void SligoDataGrids_not_visible()
        {
            //SligoDataGrid.Visible = false;
            SligoDataGrid1.Visible = false;
            SligoDataGrid2.Visible = false;
            SligoDataGrid3.Visible = false;
            SligoDataGrid4.Visible = false;
            SligoDataGrid5.Visible = false;
        }

        protected void SligoDataGrid2_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < SligoDataGrid2.Columns.Count; i++)
                {
                    DataControlField col = SligoDataGrid2.Columns[i];
                    if (col is BoundField)
                    {
                        //we don't want to be case sensitive here.
                        if (((BoundField)col).DataField.ToLower() == "DistrictName".ToLower())
                        {
                           e.Row.Cells[i].Text = Server.UrlEncode(e.Row.Cells[i].Text);
                        }
                    }
                }
            }
        }


        public void Label_Hover_Test()
        {
            // Test
            Response.Write(@"<script language='javascript'>alert('hello');</script>");
        }
        
        private void CESA_maps(string cesa_id, string var_hs)
        {
            switch (cesa_id)
            {
                case "01":
                    if (var_hs == "1") {
                         pnl_Map_1_2.Visible = true; }
                    else {
                        pnl_Map_1.Visible = true;   }
                    break;
                case "02":
                    if (var_hs == "1") {
                        pnl_Map_2_2.Visible = true; }
                    else {
                        pnl_Map_2.Visible = true; }
                    break;
                case "03":
                    pnl_Map_3.Visible = true;
                    break;
                case "04":
                    pnl_Map_4.Visible = true;
                    break;
                case "05":
                    pnl_Map_5.Visible = true;
                    break;
                case "06":
                    if (var_hs == "1")   {
                        pnl_Map_6_2.Visible = true;  }
                    else  {
                        pnl_Map_6.Visible = true; }
                    break;
                case "07":
                    pnl_Map_7.Visible = true;
                    break;
                case "08":
                    pnl_Map_8.Visible = true;
                    break;
                case "09":
                    if (var_hs == "1") {
                        pnl_Map_9_2.Visible = true;  }
                    else {
                        pnl_Map_9.Visible = true; }
                    break;
                case "10":
                    pnl_Map_10.Visible = true;
                    break;
                case "11":
                    pnl_Map_11.Visible = true;
                    break;
                case "12":
                    pnl_Map_12.Visible = true;
                    break;
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
            string result=String.Empty;
            
            // First get QueryString from GlobalValues, also set the new fullkey value 
            result = GlobalValues.GetQueryString(GlobalValues.QStringVar.FULLKEY.ToString(), fullkey);
            
            // then replace or append addtional params and values
            result = QueryStringUtils.ReplaceQueryString(result, GlobalValues.QStringVar.SN.ToString(), SchoolName);
            result = QueryStringUtils.ReplaceQueryString(result, GlobalValues.QStringVar.DN.ToString(), DistrictName);
            result = QueryStringUtils.ReplaceQueryString(result, GlobalValues.QStringVar.STYP.ToString(), SchoolType);
            result = QueryStringUtils.ReplaceQueryString(result, GlobalValues.QStringVar.LOWGRADE.ToString(), LowGrade);
            result = QueryStringUtils.ReplaceQueryString(result, GlobalValues.QStringVar.HIGHGRADE.ToString(), HighGrade);
            result = QueryStringUtils.ReplaceQueryString(result, GlobalValues.QStringVar.OrgLevel.ToString(), GlobalValues.OrgLevel.Range[ orgLevel]);

            return result;

        }


        public string GetShowSchoolQueryStringForMultipleParams(
            string fullkey
            )
        {
            string result = String.Empty;

            // First get QueryString from GlobalValues, also set the new fullkey value 
            result = GlobalValues.GetQueryString(GlobalValues.QStringVar.FULLKEY.ToString(), fullkey);
            return result;

        }
    }
}
