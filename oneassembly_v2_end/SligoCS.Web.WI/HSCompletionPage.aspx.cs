using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;
using SligoCS.BL.WI;
using SligoCS.Web.WI.PageBase.WI;
using SligoCS.Web.WI.HelperClasses;
using SligoCS.Web.WI.WebSupportingClasses.WI;
using StickyParamsEnum =
        SligoCS.Web.WI.WebSupportingClasses.WI.StickyParameter.QStringVar;

namespace SligoCS.Web.WI
{

    /// <summary>
    /// This page was the initial demonstration of the SligoDataGrid.
    /// This page is also known as the HSCompletion page.
    /// </summary>
    public partial class HSCompletionPage : PageBaseWI
    {

        protected v_HSCWWoDisSchoolDistState _ds = null;
        BLHSCompletion HSCompletion = new BLHSCompletion();
        private string graphTitle = string.Empty;


        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Master.EnableViewState = false;

            base.PrepBLEntity(HSCompletion);

            CheckSelectedSchoolOrDistrict(HSCompletion);
            SetLinkChangeSelectedSchoolOrDistrict(
                HSCompletion, ChangeSelectedSchoolOrDistrict);
            
            //set the page heading
            ((SligoCS.Web.WI.WI)Page.Master).SetPageHeading(
                "What are the high school completion rates?");
            
            int startYear = HSCompletion.TrendStartYear;
            if (StickyParameter.HSC == StickyParameter.HSC_ALL)
            {
                startYear = 2004;
            }
            _ds = HSCompletion.GetHSCompletionData(startYear);

            SetVisibleColumns2(SligoDataGrid2, _ds, 
                HSCompletion.ViewBy, HSCompletion.OrgLevel, 
                HSCompletion.CompareTo, HSCompletion.SchoolType);
            StickyParameter.SQL = HSCompletion.SQL;

            this.SligoDataGrid2.DataSource = _ds;
            this.SligoDataGrid2.DataBind();
 
            this.SligoDataGrid2.AddSuperHeader(getTitle(HSCompletion));

            this.SligoDataGrid2.ShowSuperHeader = true;

            if (base.StickyParameter.DETAIL != null &&
                    base.StickyParameter.DETAIL.ToString() == "NO")
            {
                this.SligoDataGrid2.Visible = false;
            }
            setBottomLink(HSCompletion);
            set_state();

            ////Notes:  For graph 
            GraphPanel.Visible = false;
            if (CheckIfGraphPanelVisible(HSCompletion, GraphPanel) == true)
            {
                //SetUpChart(_ds);
            }
            ////  For graph 
        }

        private void setBottomLink(BLWIBase baseBL)
        {
            BottomLinkViewReport1.Year = baseBL.CurrentYear.ToString();
            BottomLinkViewReport1.DistrictCd = GetDistrictCdByFullKey();
            BottomLinkViewReport1.SchoolCd = GetSchoolCdByFullKey();
            //BottomLinkViewProfile1.DistrictCd = GetDistrictCdByFullKey();
        }

        private string getTitle(BLHSCompletion bl)
        {

            string dataType = string.Empty;

            if (StickyParameter.HSC == StickyParameter.HSC_ALL)
            {
                dataType = "All Credential Types";
            }
            else if (StickyParameter.HSC == StickyParameter.HSC_CERT)
            {
                dataType = "Certificate";
            }
            else if (StickyParameter.HSC == StickyParameter.HSC_HSED)
            {
                dataType = "HSED";
            }
            else if (StickyParameter.HSC == StickyParameter.HSC_REG)
            {
                dataType = "Regular Diploma";
            }
            else if (StickyParameter.HSC == StickyParameter.HSC_COMB)
            {
                dataType = "Combined";
            }

             string result = "High School Completion Rate - " + dataType + "<br/>" +
                base.GetViewByInTitle(bl.ViewBy) + 
                base.GetOrgName(bl.OrgLevel) + "<br/>" +
                        base.GetYearRangeInTitle(bl.Years) + 
                        base.GetCompareToInTitle(
                            bl.OrgLevel, bl.CompareTo,
                            bl.SchoolType, bl.S4orALL, GetRegionString());
             return result.Replace("- All Students", "All Students" );
        }

        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.dataLinksPanel, true);
        }

        public override List<string> GetVisibleColumns(ViewByGroup viewBy, OrgLevel orgLevel, CompareTo compareTo, SchoolType schoolType)
        {
            List<string> retval = base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);
            retval.Remove(CommonColumnNames.SchooltypeLabel.ToString());
            retval.Add("Total Enrollment Grade 12");
            retval.Add("Total Expected to Complete High School");
            retval.Add("Cohort_Dropouts_Count");
            retval.Add("Students Who Reached the Maximum Age");
            retval.Add("Certificates");
            retval.Add("HSEDs");
            retval.Add("Regular Diplomas");

            return retval;
        }

        protected void SligoDataGrid2_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //decode the link to specific schools, so that it appears as a normal URL link.
                int colIndex = SligoDataGrid2.FindBoundColumnIndex(
                    _ds._v_HSCWWoDisSchoolDistState.
                    LinkedNameColumn.ColumnName);
                e.Row.Cells[colIndex].Text = Server.HtmlDecode(
                    e.Row.Cells[colIndex].Text);


                //format the numerical values
                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row, _ds._v_HSCWWoDisSchoolDistState.
                    Total_Enrollment_Grade_12Column.ColumnName,
                    Constants.FORMAT_RATE_03);

                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row, _ds._v_HSCWWoDisSchoolDistState.
                    Total_Expected_to_Complete_High_SchoolColumn.ColumnName,
                    Constants.FORMAT_RATE_03);

                
                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row, _ds._v_HSCWWoDisSchoolDistState.
                    Cohort_Dropouts_CountColumn.ColumnName,
                    Constants.FORMAT_RATE_01_PERC);
                
                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row, _ds._v_HSCWWoDisSchoolDistState.
                    Students_Who_Reached_the_Maximum_AgeColumn.ColumnName,
                    Constants.FORMAT_RATE_01_PERC);
                
                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row, _ds._v_HSCWWoDisSchoolDistState.
                    CertificatesColumn.ColumnName, 
                    Constants.FORMAT_RATE_01_PERC);

                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row, _ds._v_HSCWWoDisSchoolDistState.
                    HSEDsColumn.ColumnName,
                    Constants.FORMAT_RATE_01_PERC);
                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row, _ds._v_HSCWWoDisSchoolDistState.
                    Regular_DiplomasColumn.ColumnName,
                    Constants.FORMAT_RATE_01_PERC);



                FormatHelper formater = new FormatHelper();
                formater.SetRaceAbbr(SligoDataGrid2, e.Row, 
                    _ds._v_HSCWWoDisSchoolDistState.RaceLabelColumn.ToString());

            }
        }


        public override DataSet GetDataSet()
        {
            return _ds;
        }

    }
}
