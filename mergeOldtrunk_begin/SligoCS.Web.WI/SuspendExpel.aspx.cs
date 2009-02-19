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
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.WI.HelperClasses;
using System.Text;
using SligoCS.Web.Base.WebSupportingClasses.WI;
using ChartFX.WebForms;


namespace SligoCS.Web.WI
{
    public partial class SuspendExpel : PageBaseWI
    {
        //TO DO: replace dropouts sql with sql specific for suspend/expell

        protected v_DropoutsWWoDisEconELPSchoolDistState  _ds = new v_DropoutsWWoDisEconELPSchoolDistState();
        protected BLDropouts _Dropout = new BLDropouts();

        private string graphTitle = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Master.EnableViewState = false;

            base.PrepBLEntity(_Dropout);
            _ds = _Dropout.GetDropoutData2();

            StickyParameter.SQL = _Dropout.SQL;

            CheckSelectedSchoolOrDistrict(_Dropout);
            SetLinkChangeSelectedSchoolOrDistrict(
                    _Dropout,
                    ChangeSelectedSchoolOrDistrict);

            ((SligoCS.Web.WI.WI)Page.Master).SetPageHeading(
                "What percentage of students were suspended or expelled?");

            SetVisibleColumns2(SligoDataGrid2,
                    _ds,
                    _Dropout.ViewBy,
                    _Dropout.OrgLevel,
                    _Dropout.CompareTo,
                    _Dropout.SchoolType);

            
            SligoDataGrid2.AddSuperHeader(graphTitle);
            SligoDataGrid2.ShowSuperHeader = true;

            SligoDataGrid2.DataSource = _ds;
            SligoDataGrid2.DataBind();
            
            if (base.StickyParameter.DETAIL != null &&
                    base.StickyParameter.DETAIL.ToString() == "NO")
            {
                this.SligoDataGrid2.Visible = false;
            }

            setBottomLink(_Dropout);
            
            GraphPanel.Visible = false;
            if (CheckIfGraphPanelVisible(_Dropout, GraphPanel) == true)
            {
                SetUpChart(_ds);
                barChart.RawDataSource = _ds.Tables[0];
                barChart.DataBind();
            }

            setVisibleState( WI.displayed_obj.Mixed_Header_Graphics1,  true );
            setVisibleState( WI.displayed_obj.dataLinksPanel,  true );
        }
        
        private void SetUpChart(v_DropoutsWWoDisEconELPSchoolDistState ds)
        {
            barChart.BLBase = _Dropout;
            
            barChart.DisplayColumnName = ds._v_DropoutsWWoDisEconELPSchoolDistState.Drop_Out_RateColumn.ColumnName;
            
            graphTitle = GetTitle("Dropout Rate",
                            _Dropout,
                            GetRegionString());
            barChart.Title = graphTitle.Replace("<br/>", "\n");

            barChart.AxisYDescription = "Dropout Rate";

            try
            {
                barChart.MaxRateInResult = GetMaxRateInResult(ds);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            if (_Dropout.ViewBy == ViewByGroup.AllStudentsFAY)
            {
                ArrayList friendlyAxisXName = new ArrayList();
                friendlyAxisXName.Add("All Students");
                barChart.FriendlyAxisXName = friendlyAxisXName;
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

        new public bool IsSchoolsDistrictsSelected()
        {
            bool result = false;

            S4orALL s4orAll = (S4orALL)Enum.Parse(typeof(S4orALL),
                StickyParameter.S4orALL.ToString());

            if (s4orAll == S4orALL.FourSchoolsOrDistrictsIn)
            {
                if (StickyParameter.ORGLEVEL == OrgLevel.School.ToString() &&
                    String.IsNullOrEmpty(StickyParameter.SSchoolFullKeys) == false)
                {
                    result = true;
                }
                if (StickyParameter.ORGLEVEL == OrgLevel.District.ToString() &&
                    String.IsNullOrEmpty(StickyParameter.SDistrictFullKeys) == false)
                {
                    result = true;
                }
            }

            if (s4orAll == S4orALL.AllSchoolsOrDistrictsIn)
            {
                SRegion sRegion = (SRegion)Enum.Parse(typeof(SRegion),
                    StickyParameter.SRegion.ToString());

                if (sRegion == SRegion.AthleticConf &&
                    String.IsNullOrEmpty(StickyParameter.SAthleticConf) == false)
                {
                    result = true;
                }
                if (sRegion == SRegion.CESA &&
                    String.IsNullOrEmpty(StickyParameter.SCESA) == false)
                {
                    result = true;
                }
                if (sRegion == SRegion.County &&
                    String.IsNullOrEmpty(StickyParameter.SCounty) == false)
                {
                    result = true;
                }

            }


            return result;
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

        private void setBottomLink(BLWIBase baseBL)
        {
            BottomLinkViewReport1.Year = baseBL.CurrentYear.ToString();
            BottomLinkViewReport1.DistrictCd = GetDistrictCdByFullKey();
            BottomLinkViewReport1.SchoolCd = GetSchoolCdByFullKey();
            BottomLinkViewProfile1.DistrictCd = GetDistrictCdByFullKey();
        }

        private void setVisibleState(WI.displayed_obj obj, bool show)
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(
                   obj, show);
        }
        public override DataSet GetDataSet()
        {
            return _ds;
        }
    }
}
