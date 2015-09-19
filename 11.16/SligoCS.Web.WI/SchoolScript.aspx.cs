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
using SligoCS.DAL.WI;
using SligoCS.Web.Base.PageBase.WI;

using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.Web.WI
{
    public partial class SchoolScript : PageBaseWI
    {
        protected DataSet _ds = null;

        protected override void OnInitComplete(EventArgs e)
        {
            GlobalValues.ShortCircuitRedirectTests = true;
            OnCheckPrerequisites += new CheckPrerequisitesHandler(SchoolScript_OnCheckPrerequisites);
            base.OnInitComplete(e);
        }
        protected void SchoolScript_OnCheckPrerequisites(PageBaseWI page, EventArgs e)
        {
            //loosely setting prerequisites to any of required parameters... may want to restrict more?
            if (
                 (Request.QueryString["HS"] != null)
                ||
                 (Request.QueryString["L"] != null)
                //||
                 //(Request.QueryString["FULLKEY"] != null)
                ||
                (Request.QueryString["SEARCHTYPE"] != null)
            ) return;

            GlobalValues.ShortCircuitRedirectTests = false;

            //else:
            OnRedirectUser +=new RedirectUserHandler(SchoolScript_OnRedirectUser);
        }
        protected void SchoolScript_OnRedirectUser()
        {
            string filedest = "selschool.aspx";
            string qs = UserValues.GetBaseQueryString();
            string NavigateUrl = GlobalValues.CreateURL("~/" + filedest, qs);
            Response.Redirect(NavigateUrl, true);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            GlobalValues.ForceCurrentYear = true;
            GlobalValues.CurrentYear = 2007;
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

            f_key = GlobalValues.FULLKEY;
            

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

                    if ((alpha_fl != string.Empty) && (f_key == FullKeyUtils.StateFullKey(GlobalValues.FULLKEY)))
                    // if (alpha_fl != string.Empty)
                    {
                        QueryMarshaller.Database = new SligoCS.DAL.WI.DALAgencies();
                        QueryMarshaller.Database.SQL = SligoCS.DAL.WI.DALALLAgencies.GetSchoolsByStartNameSQL(alpha_fl);
                        QueryMarshaller.ManualQuery();
                        GlobalValues.SQL = GlobalValues.SQL + "//" + QueryMarshaller.Database.SQL;

                        SligoDataGrid1.DataSource = QueryMarshaller.Database.DataSet; 
                        SligoDataGrid1.DataBind();
                        SligoDataGrid1.AddSuperHeader( "Searching in SCHOOL by Letter " + alpha_fl);
                        SligoDataGrid1.Visible = true;
                        No_Records_Found("No Schools Found Starting with ", alpha_fl);
                    }
                    else
                    {
                        QueryMarshaller.Database = new SligoCS.DAL.WI.DALAgencies();
                        QueryMarshaller.Database.SQL = SligoCS.DAL.WI.DALALLAgencies.GetSchoolByDistrictKeySQL(f_key);
                        QueryMarshaller.ManualQuery();
                        GlobalValues.SQL = GlobalValues.SQL + " //" + QueryMarshaller.Database.SQL;
                        SligoDataGrid4.DataSource = QueryMarshaller.Database.DataSet; 
                        SligoDataGrid4.DataBind();
                        try
                        {
                            SligoDataGrid4.AddSuperHeader("Searching Schools in DISTRICT, " + QueryMarshaller.Database.DataSet.Tables[0].Rows[0][SligoCS.DAL.WI.v_AgencyFull.DistrictName].ToString());
                        }
                        catch
                        {
                        }
                        SligoDataGrid4.Visible = true;
                    }
                    break;
                case "DI":
                    GlobalValues.SQL = DALALLAgencies.GetDistrictByNameSQL(alpha_fl);
                    QueryMarshaller.AssignQuery(new DALAgencies(), GlobalValues.SQL);
                    SligoDataGrid2.DataSource = QueryMarshaller.Database.DataSet;
                    SligoDataGrid2.DataBind();
                    SligoDataGrid2.AddSuperHeader("Searching in DISTRICT by Letter " + alpha_fl);

                    SligoDataGrid2.Visible = true;
                    No_Records_Found("No Districts Found Starting with ", alpha_fl);
                    break;
                case "CO":
                    GlobalValues.SQL = DALALLAgencies.GetCountyByNameSQL(alpha_fl);
                    QueryMarshaller.AssignQuery(new DALAgencies(), GlobalValues.SQL);
                    SligoDataGrid3.DataSource = QueryMarshaller.Database.DataSet;
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

                    GlobalValues.SQL = DALALLAgencies.GetDistrictByCESASQL(alpha_fl, var_hs);
                    QueryMarshaller.AssignQuery(new DALAgencies(), GlobalValues.SQL);
                    SligoDataGrid5.DataSource = QueryMarshaller.Database.DataSet ;
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
            if (QueryMarshaller.Database.Table.Rows.Count == 0)
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
            result = UserValues.GetQueryString("FULLKEY", fullkey);
            
            // then replace or append addtional params and values
            result = QueryStringUtils.ReplaceQueryString(result, "SN", SchoolName);
            result = QueryStringUtils.ReplaceQueryString(result, "DN", DistrictName);
            //shouldn't be setting STYP in initial agency selection pages.
            //    result = QueryStringUtils.ReplaceQueryString(result, GlobalValues.STYP.Name, SchoolType);
            result = QueryStringUtils.ReplaceQueryString(result, "LOWGRADE", LowGrade);
            result = QueryStringUtils.ReplaceQueryString(result, "HIGHGRADE", HighGrade);
            result = QueryStringUtils.ReplaceQueryString(result, GlobalValues.OrgLevel.Name, GlobalValues.OrgLevel.Range[ orgLevel]);

            return result;
        }
    }
}
