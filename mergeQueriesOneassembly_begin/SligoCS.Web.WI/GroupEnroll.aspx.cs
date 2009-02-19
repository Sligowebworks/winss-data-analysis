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

using SligoCS.DAL.WI.DataSets;
using SligoCS.BL.WI;
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.WI.HelperClasses;
using System.Text;
using SligoCS.Web.Base.WebSupportingClasses.WI;
using ChartFX.WebForms;

namespace SligoCS.Web.WI
{
    public partial class GroupEnroll : PageBaseWI
    {
        protected v_DropoutsWWoDisEconELPSchoolDistState _ds =
               new v_DropoutsWWoDisEconELPSchoolDistState();
        protected BLDropouts _Dropout = new BLDropouts();

        private string graphTitle = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Master.EnableViewState = false;

            base.PrepBLEntity(_Dropout);
            ((SligoCS.Web.WI.WI)Page.Master).SetPageHeading(
                "What is the enrollment by student group?");
            _ds = _Dropout.GetDropoutData2();
            CheckSelectedSchoolOrDistrict(_Dropout);
            SetLinkChangeSelectedSchoolOrDistrict(
                _Dropout, ChangeSelectedSchoolOrDistrict);
            SetVisibleColumns2(SligoDataGrid2, _ds, _Dropout.ViewBy,
               _Dropout.OrgLevel, _Dropout.CompareTo, _Dropout.SchoolType);

            StickyParameter.SQL = _Dropout.SQL;

            this.SligoDataGrid2.DataSource = _ds;

            this.SligoDataGrid2.DataBind();


            graphTitle = GetTitle("Dropout Rate",
                              _Dropout,
                              GetRegionString());

            this.SligoDataGrid2.AddSuperHeader(graphTitle);

            this.SligoDataGrid2.ShowSuperHeader = true;

            if (base.StickyParameter.DETAIL != null &&
                    base.StickyParameter.DETAIL.ToString() == "NO")
            {
                this.SligoDataGrid2.Visible = false;
            }

            set_state();
            setBottomLink(_Dropout);
            GraphPanel.Visible = false;
            if (CheckIfGraphPanelVisible(_Dropout, GraphPanel) == true)
            {
                SetUpChart(_ds);
            }
        }

        private void SetUpChart(v_DropoutsWWoDisEconELPSchoolDistState ds)
        {
            try
            {
                graphTitle = graphTitle.Replace("<br/>", "\n");
                barChart.Title = graphTitle;
                barChart.MaxRateInResult = GetMaxRateInResult(ds);

                if (_Dropout.ViewBy == ViewByGroup.AllStudentsFAY)
                {
                    ArrayList friendlyAxisXName = new ArrayList();
                    friendlyAxisXName.Add("All Students");
                    barChart.FriendlyAxisXName = friendlyAxisXName;
                }

                barChart.AxisYDescription = "Dropout Rate";
                barChart.BLBase = _Dropout;
                barChart.DisplayColumnName =
                    ds._v_DropoutsWWoDisEconELPSchoolDistState.Drop_Out_RateColumn.ColumnName;
                barChart.RawDataSource = ds.Tables[0];
                barChart.DataBind();

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }


        private double GetMaxRateInResult(v_DropoutsWWoDisEconELPSchoolDistState ds)
        {
            double maxRateInResult = 0;

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                try
                {
                    if (Convert.ToDouble(
                            row[ds._v_DropoutsWWoDisEconELPSchoolDistState.
                                Drop_Out_RateColumn.ColumnName].ToString())
                                        > maxRateInResult)
                    {
                        maxRateInResult = Convert.ToDouble
                                (row[ds._v_DropoutsWWoDisEconELPSchoolDistState.
                                    Drop_Out_RateColumn.ColumnName].ToString());
                    }
                }
                catch
                {
                    continue;
                }
            }
            return maxRateInResult;
        }



        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(
                    WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(
                        WI.displayed_obj.dataLinksPanel, true);
        }

        private void setBottomLink(BLWIBase baseBL)
        {
            BottomLinkViewReport1.Year = baseBL.CurrentYear.ToString();
            BottomLinkViewReport1.DistrictCd = GetDistrictCdByFullKey();
            BottomLinkViewReport1.SchoolCd = GetSchoolCdByFullKey();
            BottomLinkViewProfile1.DistrictCd = GetDistrictCdByFullKey();
        }
        public override List<string> GetVisibleColumns(ViewByGroup viewBy, OrgLevel orgLevel, CompareTo compareTo, SchoolType schoolType)
        {
            List<string> cols = base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);

            if ((compareTo == CompareTo.SELSCHOOLS) || (compareTo == CompareTo.CURRENTONLY))
            {
                //When user selects "Compare To = Selected Schools" the first column in the grid
                //should show the school name, but with no header.                
                cols.Add(_ds._v_DropoutsWWoDisEconELPSchoolDistState.NameColumn.ColumnName);
            }

            cols.Add(_ds._v_DropoutsWWoDisEconELPSchoolDistState.EnrollmentColumn.ColumnName);
            cols.Add(_ds._v_DropoutsWWoDisEconELPSchoolDistState.Students_expected_to_complete_the_school_termColumn.ColumnName);
            cols.Add(_ds._v_DropoutsWWoDisEconELPSchoolDistState.Students_who_completed_the_school_termColumn.ColumnName);
            cols.Add(_ds._v_DropoutsWWoDisEconELPSchoolDistState.Drop_OutsColumn.ColumnName);
            cols.Add(_ds._v_DropoutsWWoDisEconELPSchoolDistState.Drop_Out_RateColumn.ColumnName);

            return cols;
        }
        protected void SligoDataGrid2_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //decode the link to specific schools, so that it appears as a normal URL link.
                int colIndex = SligoDataGrid2.FindBoundColumnIndex(
                    _ds._v_DropoutsWWoDisEconELPSchoolDistState.LinkedNameColumn.ColumnName);
                e.Row.Cells[colIndex].Text = Server.HtmlDecode(e.Row.Cells[colIndex].Text);

                //format the numerical values
                SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _ds._v_DropoutsWWoDisEconELPSchoolDistState.EnrollmentColumn
                    .ColumnName, Constants.FORMAT_RATE_03);
                SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _ds._v_DropoutsWWoDisEconELPSchoolDistState.Students_who_completed_the_school_termColumn
                    .ColumnName, Constants.FORMAT_RATE_03);
                SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _ds._v_DropoutsWWoDisEconELPSchoolDistState.Students_expected_to_complete_the_school_termColumn
                  .ColumnName, Constants.FORMAT_RATE_03);
                SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _ds._v_DropoutsWWoDisEconELPSchoolDistState.Drop_OutsColumn
                    .ColumnName, Constants.FORMAT_RATE_03);
                SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _ds._v_DropoutsWWoDisEconELPSchoolDistState.
                    Drop_Out_RateColumn.ColumnName, Constants.FORMAT_RATE_02_PERC);

                ////Does not apply to Retention page.
                //SetOrgLevelRowLabels(_Dropout, SligoDataGrid2, e.Row);

                //// replace long race label with shourt race label
                FormatHelper formater = new FormatHelper();
                formater.SetRaceAbbr(SligoDataGrid2, e.Row,
                    _ds._v_DropoutsWWoDisEconELPSchoolDistState.RaceLabelColumn.ToString());

            }
        }

        public override DataSet GetDataSet()
        {
            return _ds;
        }
    }
}
