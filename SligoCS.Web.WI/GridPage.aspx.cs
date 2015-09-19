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
using SligoCS.Web.WI.DAL.DataSets;
using SligoCS.BL.WI;
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.WI.HelperClasses;
using System.Text;
using SligoCS.Web.WI.WebSupportingClasses.WI;

using ChartFX.WebForms;

namespace SligoCS.Web.WI
{

    /// <summary>
    /// This page was the initial demonstration of the SligoDataGrid.
    /// This page is also known as the Retention page.
    /// </summary>
    public partial class GridPage : PageBaseWI
    {

        protected v_RetentionWWoDisEconELPSchoolDistState _ds = 
            new v_RetentionWWoDisEconELPSchoolDistState();
        protected BLRetention _retention = new BLRetention();
        
        private string graphTitle = string.Empty;
        protected override Chart InitGraph()
        {
            return barChart;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            Page.Master.EnableViewState = false;

            base.PrepBLEntity(_retention);

            //set the page heading
            ((SligoCS.Web.WI.WI)Page.Master).SetPageHeading(
                "What percent of students did not advance to the next grade level?");

            _ds = _retention.GetRetentionData2();
            
            SetLinkChangeSelectedSchoolOrDistrict(ChangeSelectedSchoolOrDistrict);
            
            SetVisibleColumns2(SligoDataGrid2, _ds, _retention.ViewBy,
                _retention.OrgLevel, _retention.CompareTo, GlobalValues.STYP);
            
            GlobalValues.SQL = _retention.SQL;

            this.SligoDataGrid2.DataSource = _ds;
            
            this.SligoDataGrid2.DataBind();

            graphTitle = GetTitle("Retention Rate", _retention);

            this.SligoDataGrid2.AddSuperHeader(graphTitle);

            this.SligoDataGrid2.ShowSuperHeader = true;

            if (base.GlobalValues.DETAIL != null && 
                    base.GlobalValues.DETAIL.ToString() == "NO")
            {
                this.SligoDataGrid2.Visible = false;
            }

            set_state();
            setBottomLink(_retention);

            ////Notes:  graph 
            GraphPanel.Visible = false;
            if ( CheckIfGraphPanelVisible(GraphPanel) == true)
            {
                SetUpChart(_ds);
            }
            //// graph 
        }

        /// <summary>
        ///  Set up graph 
        /// </summary>
        /// <param name="ds"></param>
        private void SetUpChart(v_RetentionWWoDisEconELPSchoolDistState ds)
        {
            try
            {
                graphTitle = graphTitle.Replace("<br/>", "\n");
                barChart.Title = graphTitle;

                //Axis Y Settings
                //int axisYStep = 1;
                //int axisYMin = 0;
                //int axisYMax = 10;
                //ArrayList friendlyAxisYName = new ArrayList();
                //GetAxisYSetting(ds, ref axisYStep, ref axisYMin, 
                //                    ref axisYMax, ref friendlyAxisYName);
                //barChart.AxisYStep = axisYStep;
                //barChart.AxisYMin = axisYMin;
                //barChart.AxisYMax = axisYMax;
                //barChart.FriendlyAxisYName = friendlyAxisYName;

                barChart.MaxRateInResult = GetMaxRateInResult(ds);

                if (_retention.ViewBy.Key == GroupKeys.All)
                {
                    List<String> friendlyAxisXName = new List<String>();
                    friendlyAxisXName.Add("All Students");
                    barChart.FriendlyAxisXName = friendlyAxisXName;
                }

                barChart.AxisYDescription = "Retention Rate";
                                
                //Bind Data Source & Display
                barChart.DisplayColumnName = 
                    ds._v_RetentionWWoDisEconELPSchoolDistState.
                                Retention_RateColumn.ColumnName;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }


        private double GetMaxRateInResult(v_RetentionWWoDisEconELPSchoolDistState ds)
        {
            double maxRateInResult = 0;

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                try
                {
                    if (Convert.ToDouble(
                            row[ds._v_RetentionWWoDisEconELPSchoolDistState.
                                Retention_RateColumn.ColumnName].ToString())
                                        > maxRateInResult)
                    {
                        maxRateInResult = Convert.ToDouble
                                (row[ds._v_RetentionWWoDisEconELPSchoolDistState.
                                    Retention_RateColumn.ColumnName].ToString());
                    }
                }
                catch
                {
                    continue;
                }
            }
            return maxRateInResult;
        }
        
        //private void GetAxisYSetting(v_RetentionWWoDisEconELPSchoolDistState ds, 
        //    ref int axisYStep, 
        //    ref int axisYMin, 
        //    ref int axisYMax, 
        //    ref ArrayList axisYLabels)
        //{
        //    double maxRetenionInResult = 0;

        //    //try
        //    //{
        //    //    maxRetenionInResult = Convert.ToDouble(ds.Tables[0].Compute("Max([" + ds._v_RetentionWWoDisEconELPSchoolDistState.Retention_RateColumn.ColumnName + "])", "").ToString());
        //    //}
        //    //catch
        //    //{
        //    //    maxRetenionInResult = 0;
        //    //}

        //    foreach (DataRow row in ds.Tables[0].Rows)
        //    {
        //        try
        //        {
        //            if (Convert.ToDouble(row[
        //                    ds._v_RetentionWWoDisEconELPSchoolDistState.
        //                        Retention_RateColumn.ColumnName].ToString())
        //                                                    > maxRetenionInResult)
        //            {
        //                maxRetenionInResult = Convert.ToDouble
        //                    (row[ds._v_RetentionWWoDisEconELPSchoolDistState.
        //                            Retention_RateColumn.ColumnName].ToString());
        //            }
        //        }
        //        catch
        //        {
        //            continue;
        //        }
        //    }

        //    if (maxRetenionInResult <= 10)
        //    {
        //        axisYMin = 0;
        //        axisYMax = 10;
        //        axisYStep = 1;
        //    }
        //    else if (maxRetenionInResult <= 24)
        //    {
        //        axisYMin = 0;
        //        axisYMax = 24;
        //        axisYStep = 2;
        //    }
        //    else if (maxRetenionInResult <= 48)
        //    {
        //        axisYMin = 0;
        //        axisYMax = 48;
        //        axisYStep = 4;
        //    }
        //    else if (maxRetenionInResult <= 96)
        //    {
        //        axisYMin = 0;
        //        axisYMax = 96;
        //        axisYStep = 8;
        //    }

        //    for (int i = axisYMin; i <= axisYMax; i = i + axisYStep)
        //    {
        //        axisYLabels.Add(i.ToString() + "%");
        //    }
        //}



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
            BottomLinkViewReport1.DistrictCd = GlobalValues.DistrictCode;
            BottomLinkViewReport1.SchoolCd = GlobalValues.SchoolCode;
            BottomLinkViewProfile1.DistrictCd = GlobalValues.DistrictCode;
        }

        public override List<string> GetVisibleColumns(Group viewBy, 
            OrgLevel orgLevel, CompareTo compareTo, STYP schoolType)
        {
            List<string> retval = base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);

            retval.Add(_ds._v_RetentionWWoDisEconELPSchoolDistState._Total_Enrollment__K_12_Column.ColumnName);
            retval.Add(_ds._v_RetentionWWoDisEconELPSchoolDistState.Completed_School_TermColumn.ColumnName);
            retval.Add(_ds._v_RetentionWWoDisEconELPSchoolDistState.Number_of_RetentionsColumn.ColumnName);
            retval.Add(_ds._v_RetentionWWoDisEconELPSchoolDistState.Retention_RateColumn.ColumnName);

            return retval;
        }

        protected void SligoDataGrid2_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //decode the link to specific schools, so that it appears as a normal URL link.
                int colIndex = SligoDataGrid2.FindBoundColumnIndex(
                    _ds._v_RetentionWWoDisEconELPSchoolDistState.LinkedNameColumn.ColumnName);
                e.Row.Cells[colIndex].Text = Server.HtmlDecode(e.Row.Cells[colIndex].Text);

                //format the numerical values
                SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _ds._v_RetentionWWoDisEconELPSchoolDistState.
                    _Total_Enrollment__K_12_Column.ColumnName, Constants.FORMAT_RATE_03);
                SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _ds._v_RetentionWWoDisEconELPSchoolDistState.
                    Completed_School_TermColumn.ColumnName, Constants.FORMAT_RATE_03);
                SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _ds._v_RetentionWWoDisEconELPSchoolDistState.
                    Number_of_RetentionsColumn.ColumnName, Constants.FORMAT_RATE_03);
                SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _ds._v_RetentionWWoDisEconELPSchoolDistState.
                    Retention_RateColumn.ColumnName, Constants.FORMAT_RATE_02_PERC);

                //Does not apply to Retention page.
                SligoDataGrid2.SetOrgLevelRowLabels(GlobalValues, e.Row);

                //// replace long race label with shourt race label
                FormatHelper formater = new FormatHelper();
                formater.SetRaceAbbr(SligoDataGrid2, e.Row, 
                    _ds._v_RetentionWWoDisEconELPSchoolDistState.RaceLabelColumn.ToString());

            }
        }

        public override DataSet GetDataSet()
        {
            return _ds;
        }

    }
}
