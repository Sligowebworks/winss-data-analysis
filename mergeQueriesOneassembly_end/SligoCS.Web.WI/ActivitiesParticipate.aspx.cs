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
    public partial class ActivitiesParticipate : PageBaseWI
    {
        protected v_ActivitiesSchoolDistState _ds =
              new v_ActivitiesSchoolDistState();
        protected BLActivities _Activities = new BLActivities();

        private string graphTitle = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Master.EnableViewState = false;

            base.PrepBLEntity(_Activities);

            ((SligoCS.Web.WI.WI)Page.Master).SetPageHeading(
                "What school-supported activities are offered?");

            //jdj: may need to add parameters here

            //jdj: check parameters below
            _ds = _Activities.getActivitiesQuery(StickyParameter.SHOW);

            CheckSelectedSchoolOrDistrict(_Activities);

            SetLinkChangeSelectedSchoolOrDistrict(
                _Activities, ChangeSelectedSchoolOrDistrict);

            //jdj: check parameters below
            SetVisibleColumns2(SligoDataGrid2, _ds, _Activities.ViewBy,
               _Activities.OrgLevel, _Activities.CompareTo, _Activities.SchoolType);

            StickyParameter.SQL = _Activities.SQL;

            this.SligoDataGrid2.DataSource = _ds;

            this.SligoDataGrid2.DataBind();


            graphTitle = GetTitle("Extra-Co-Curricular Activities",
                              _Activities,
                              GetRegionString());

            this.SligoDataGrid2.AddSuperHeader(graphTitle);

            this.SligoDataGrid2.ShowSuperHeader = true;

            if (base.StickyParameter.DETAIL != null &&
                    base.StickyParameter.DETAIL.ToString() == "NO")
            {
                this.SligoDataGrid2.Visible = false;
            }

            //testing
            this.SligoDataGrid2.Visible = true;

            set_state();

            setBottomLink(_Activities);

            // GraphPanel.Visible = false;

            // if (CheckIfGraphPanelVisible(_Activities, GraphPanel) == true)
            {
                //jdj: testing
                //SetUpChart(_ds);
            }
        }

        //jdj: testing
        /*
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

              barChart.AxisYDescription = "Offerings Per School (Average)";
                
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

      */

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
                cols.Add(_ds._v_ActivitiesSchoolDistState.NameColumn.ColumnName);
            }

            return cols;
        }
        protected void SligoDataGrid2_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            //jdj: testing

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //decode the link to specific schools, so that it appears as a normal URL link.
                int colIndex = SligoDataGrid2.FindBoundColumnIndex(_ds._v_ActivitiesSchoolDistState.LinkedDistrictNameColumn.ColumnName);
                e.Row.Cells[colIndex].Text = Server.HtmlDecode(e.Row.Cells[colIndex].Text);

                //int colIndex = SligoDataGrid2.FindBoundColumnIndex(_ds._v_ActivitiesSchoolDistState.NameColumn.ColumnName);

                //format the numerical values
                //    SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _ds._v_DropoutsWWoDisEconELPSchoolDistState.EnrollmentColumn.ColumnName, Constants.FORMAT_RATE_03);

                //    SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _ds._v_DropoutsWWoDisEconELPSchoolDistState.Students_who_completed_the_school_termColumn.ColumnName, Constants.FORMAT_RATE_03);

                //    SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _ds._v_DropoutsWWoDisEconELPSchoolDistState.Students_expected_to_complete_the_school_termColumn.ColumnName, Constants.FORMAT_RATE_03);

                //    SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _ds._v_DropoutsWWoDisEconELPSchoolDistState.Drop_OutsColumn.ColumnName, Constants.FORMAT_RATE_03);

                //    SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _ds._v_DropoutsWWoDisEconELPSchoolDistState.Drop_Out_RateColumn.ColumnName, Constants.FORMAT_RATE_02_PERC);

                ////Does not apply to Retention page.
                SetOrgLevelRowLabels(_Activities, SligoDataGrid2, e.Row);

                //// replace long race label with shourt race label
                //    FormatHelper formater = new FormatHelper();
                //    formater.SetRaceAbbr(SligoDataGrid2, e.Row,
                //        _ds._v_DropoutsWWoDisEconELPSchoolDistState.RaceLabelColumn.ToString());

            }
        }

        public override DataSet GetDataSet()
        {
            return _ds;
        }
    }
}
