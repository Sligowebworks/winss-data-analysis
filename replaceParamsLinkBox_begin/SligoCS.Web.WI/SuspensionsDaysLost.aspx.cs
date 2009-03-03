using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
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

namespace SligoCS.Web.WI
{
    public partial class SuspensionsDaysLost : PageBaseWI
    {
        protected v_Suspensions _ds = 
            new v_Suspensions();
        
        BLSuspensionsDaysLost _suspensionsDaysLost = new BLSuspensionsDaysLost();

        private string graphTitle = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

            Page.Master.EnableViewState = false;

            base.PrepBLEntity(_suspensionsDaysLost);

            CheckSelectedSchoolOrDistrict(_suspensionsDaysLost);
            SetLinkChangeSelectedSchoolOrDistrict(
                _suspensionsDaysLost, ChangeSelectedSchoolOrDistrict);

            ((SligoCS.Web.WI.WI)Page.Master).SetPageHeading(
                "What percentage of days were lost due to suspensions and expulsions?");
            _ds = _suspensionsDaysLost.GetSuspensionsDaysLostData();
            SetVisibleColumns2(SligoDataGrid1, _ds, _suspensionsDaysLost.ViewBy,
                _suspensionsDaysLost.OrgLevel, _suspensionsDaysLost.CompareTo, _suspensionsDaysLost.SchoolType);
            StickyParameter.SQL = _suspensionsDaysLost.SQL;

            this.SligoDataGrid1.DataSource = _ds;
            this.SligoDataGrid1.DataBind();

            graphTitle = GetTitle("Days Lost Due to Suspensions",
                 _suspensionsDaysLost,
                 GetRegionString());

            SligoDataGrid1.AddSuperHeader(graphTitle);

            if (base.StickyParameter.DETAIL != null &&
                    base.StickyParameter.DETAIL.ToString() == "NO")
            {
                this.SligoDataGrid1.Visible = false;
            }

            //if (StickyParameter.CompareTo != CompareTo.PRIORYEARS.ToString())
            //{
            //    DefPanel.Visible = false;
            //}

            set_state();
            setBottomLink(_suspensionsDaysLost);

            ////Notes:  graph 
            GraphPanel.Visible = false;
            if (CheckIfGraphPanelVisible(_suspensionsDaysLost, GraphPanel) == true)
            {
                SetUpChart(_ds);
            }
            //// graph 
        }

        /// <summary>
        ///  Set up graph 
        /// </summary>
        /// <param name="ds"></param>
        private void SetUpChart(v_Suspensions ds)
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

                if (_suspensionsDaysLost.ViewBy == ViewByGroup.AllStudentsFAY)
                {
                    ArrayList friendlyAxisXName = new ArrayList();
                    friendlyAxisXName.Add("All Students");
                    barChart.FriendlyAxisXName = friendlyAxisXName;
                }
                barChart.AxisYDescription = "Days Lost Due to Suspensions";

                //Bind Data Source & Display
                barChart.BLBase = _suspensionsDaysLost;
                barChart.DisplayColumnName =
                    ds._v_Suspensions.Percent_of_Days_SuspendedColumn.ColumnName;
                barChart.RawDataSource = ds.Tables[0];
                barChart.DataBind();
            
                //jdj: need to add number of days suspended column
            
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private double GetMaxRateInResult (v_Suspensions ds)
        {
            double maxRateInResult = 0;

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                try
                {
                    if (Convert.ToDouble(
                            row[ds._v_Suspensions.Percent_of_Days_SuspendedColumn.ColumnName].ToString())
                                        > maxRateInResult)
                    {
                        maxRateInResult = Convert.ToDouble
                                (row[ds._v_Suspensions.Percent_of_Days_SuspendedColumn.ColumnName].ToString());
                    }
                }
                catch
                {
                    continue;
                }
            }
            return maxRateInResult;
        }

        //private void GetAxisYSetting(v_TruancySchoolDistState ds, 
        //    ref int axisYStep, 
        //    ref int axisYMin, 
        //    ref int axisYMax, 
        //    ref ArrayList axisYLabels)
        //{
        //    double maxTruancyInResult = 0;

        //    foreach (DataRow row in ds.Tables[0].Rows)
        //    {
        //        try
        //        {
        //            if (Convert.ToDouble(
        //                    row[ds._v_TruancySchoolDistState.
        //                        Truancy_RateColumn.ColumnName].ToString())
        //                                > maxTruancyInResult)
        //            {
        //                maxTruancyInResult = Convert.ToDouble
        //                        (row[ds._v_TruancySchoolDistState.
        //                            Truancy_RateColumn.ColumnName].ToString());
        //            }
        //        }
        //        catch
        //        {
        //            continue;
        //        }
        //    }

        //    if (maxTruancyInResult < 10)
        //    {
        //        axisYMin = 0;
        //        axisYMax = 10;
        //        axisYStep = 1;
        //    }
        //    else if (maxTruancyInResult < 20)
        //    {
        //        axisYMin = 0;
        //        axisYMax = 20;
        //        axisYStep = 2;
        //    }

        //    else if (maxTruancyInResult < 30)
        //    {
        //        axisYMin = 0;
        //        axisYMax = 30;
        //        axisYStep = 3;
        //    }
 
        //    else if (maxTruancyInResult < 50)
        //    {
        //        axisYMin = 0;
        //        axisYMax = 50;
        //        axisYStep = 5;
        //    }
        //    else if (maxTruancyInResult < 80)
        //    {
        //        axisYMin = 0;
        //        axisYMax = 80;
        //        axisYStep = 10;
        //    }
        //    else if (maxTruancyInResult < 100)
        //    {
        //        axisYMin = 0;
        //        axisYMax = 100;
        //        axisYStep = 10;
        //    }
        //    else
        //    {
        //        axisYMin = 0;
        //        axisYMax = 120;
        //        axisYStep = 10;
        //    }

        //    for (int i = axisYMin; i <= axisYMax; i = i + axisYStep)
        //    {
        //        axisYLabels.Add(i.ToString() + "%");
        //    }
        //}

        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                (WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                (WI.displayed_obj.dataLinksPanel, true);
        }

        private void setBottomLink(BLWIBase baseBL)
        {
            BottomLinkViewReport1.Year = baseBL.CurrentYear.ToString();
            BottomLinkViewReport1.DistrictCd = GetDistrictCdByFullKey();
            BottomLinkViewReport1.SchoolCd = GetSchoolCdByFullKey();
            BottomLinkViewProfile1.DistrictCd = GetDistrictCdByFullKey();
        }

        public override System.Collections.Generic.List<string> 
            GetVisibleColumns(ViewByGroup viewBy, OrgLevel orgLevel, 
            CompareTo compareTo, SchoolType schoolType)
        {
            List<string> retval = base.GetVisibleColumns(
                        viewBy, orgLevel, compareTo, schoolType);

            retval.Add(_ds._v_Suspensions._Total_Enrollment_PreK_12Column.ColumnName);
            retval.Add(_ds._v_Suspensions.Possible_Days_AttendanceColumn.ColumnName);
            //jdj: need to add number of days suspended column
            retval.Add(_ds._v_Suspensions.Percent_of_Days_SuspendedColumn.ColumnName);
            return retval;
        }
        

        protected void SligoDataGrid1_RowDataBound
            (object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //decode the link to specific schools, so that it appears as a normal URL link.
                int colIndex = SligoDataGrid1.FindBoundColumnIndex(
                    _ds._v_Suspensions.linkednameColumn.ColumnName);
                e.Row.Cells[colIndex].Text = Server.HtmlDecode(e.Row.Cells[colIndex].Text);

                ////format the numerical values                
                SligoDataGrid1.SetCellToFormattedDecimal(e.Row,
                    _ds._v_Suspensions.Percent_of_Days_SuspendedColumn.ColumnName, 
                    Constants.FORMAT_RATE_01_PERC);

                //Does not apply to Retention page.
                SetOrgLevelRowLabels(_suspensionsDaysLost, SligoDataGrid1, e.Row);

                //// replace long race label with short race label
                FormatHelper formater = new FormatHelper();
                formater.SetRaceAbbr(SligoDataGrid1, e.Row,
                    _ds._v_Suspensions.RaceLabelColumn.ToString());

            }

        }

        public override DataSet GetDataSet()
        {
            return _ds;
        }
        
        //
        #region GetSampleData

        ////private void GetSampleData()
        ////{
        ////    //select [year], YearFormatted, fullkey, agencykey, CESA, county, schooltype, SchoolTypeLabel, 
        ////    //[District Number], [School Number], charter, OrgLevelLabel, [Name], [District Name], [School Name], 
        ////    //GradeCode, GradeLabel, GradeShortLabel, RaceCode, RaceLabel, RaceShortLabel, SexCode, SexLabel, 
        ////    //[Student Group], StudentGroupLabel, LinkedName, LinkedDistrictName, LinkedSchoolName, [Total Enrollment (K-12)], suppressed, [Number of Students Habitually Truant], [Truancy Rate] from dbo.v_TruancySchoolDistState

        ////    DataColumn col1 = new DataColumn("year");
        ////    DataColumn col2 = new DataColumn("YearFormatted");
        ////    DataColumn col3 = new DataColumn("agencykey");
        ////    DataColumn col4 = new DataColumn("CESA");
        ////    DataColumn col5 = new DataColumn("county");
        ////    DataColumn col6 = new DataColumn("schooltype");
        ////    DataColumn col7 = new DataColumn("SchoolTypeLabel");
        ////    DataColumn col8 = new DataColumn("District Number");
        ////    DataColumn col9 = new DataColumn("School Number");
        ////    DataColumn col10 = new DataColumn("charter");
        ////    DataColumn col11 = new DataColumn("OrgLevelLabel");
        ////    DataColumn col12 = new DataColumn("Name");
        ////    DataColumn col13 = new DataColumn("District Name");
        ////    DataColumn col14 = new DataColumn("School Name");
        ////    DataColumn col15 = new DataColumn("GradeCode");
        ////    DataColumn col16 = new DataColumn("GradeLabel");
        ////    DataColumn col17 = new DataColumn("GradeShortLabel");
        ////    DataColumn col18 = new DataColumn("RaceCode");
        ////    DataColumn col19 = new DataColumn("RaceLabel");
        ////    DataColumn col20 = new DataColumn("RaceShortLabel");
        ////    DataColumn col21 = new DataColumn("SexCode");
        ////    DataColumn col22 = new DataColumn("SexLabel");
        ////    DataColumn col23 = new DataColumn("Student Group");
        ////    DataColumn col24 = new DataColumn("StudentGroupLabel");
        ////    DataColumn col25 = new DataColumn("LinkedName");
        ////    DataColumn col26 = new DataColumn("LinkedDistrictName");
        ////    DataColumn col27 = new DataColumn("LinkedSchoolName");
        ////    DataColumn col28 = new DataColumn("Total Enrollment (K-12)");
        ////    DataColumn col29 = new DataColumn("suppressed");
        ////    DataColumn col30 = new DataColumn("Number of Students Habitually Truant");
        ////    DataColumn col31 = new DataColumn("Truancy Rate");
                       
        ////    DataTable dt = new DataTable();
        ////    dt.Columns.Add(col1);
        ////    dt.Columns.Add(col2);
        ////    dt.Columns.Add(col3);
        ////    dt.Columns.Add(col4);
        ////    dt.Columns.Add(col5);
        ////    dt.Columns.Add(col6);
        ////    dt.Columns.Add(col7);
        ////    dt.Columns.Add(col8);
        ////    dt.Columns.Add(col9);
        ////    dt.Columns.Add(col10);
        ////    dt.Columns.Add(col11);
        ////    dt.Columns.Add(col12);
        ////    dt.Columns.Add(col13);
        ////    dt.Columns.Add(col14);
        ////    dt.Columns.Add(col15);
        ////    dt.Columns.Add(col16);
        ////    dt.Columns.Add(col17);
        ////    dt.Columns.Add(col18);
        ////    dt.Columns.Add(col19);
        ////    dt.Columns.Add(col20);
        ////    dt.Columns.Add(col21);
        ////    dt.Columns.Add(col22);
        ////    dt.Columns.Add(col23);
        ////    dt.Columns.Add(col24);
        ////    dt.Columns.Add(col25);
        ////    dt.Columns.Add(col26);
        ////    dt.Columns.Add(col27);
        ////    dt.Columns.Add(col28);
        ////    dt.Columns.Add(col29);
        ////    dt.Columns.Add(col30);
        ////    dt.Columns.Add(col31);

        ////    DataRow Dr;
        ////    Dr = dt.NewRow(); 
        ////    Dr[0]="2004";
        ////    Dr[1] = "Current School";
        ////    Dr[2] = "1";
        ////    Dr[3] = "1";
        ////    Dr[4] = "1";
        ////    Dr[5] = "US";
        ////    Dr[6] = "";
        ////    Dr[7] = "";
        ////    Dr[8] = "";
        ////    Dr[9] = "";
        ////    Dr[10] = "";
        ////    Dr[11] = "";
        ////    Dr[12] = "";
        ////    Dr[13] = "High Schools";
        ////    Dr[14] = "";
        ////    Dr[15] = "A";
        ////    Dr[16] = "";
        ////    Dr[17] = "";
        ////    Dr[18] = "";
        ////    Dr[19] = "";
        ////    Dr[20] = "";
        ////    Dr[21] = "";
        ////    Dr[22] = "";
        ////    Dr[23] = "";
        ////    Dr[24] = "";
        ////    Dr[25] = "";
        ////    Dr[26] = "High Schools";
        ////    Dr[27] = "1,577";
        ////    Dr[28] = "Y";
        ////    Dr[29] = "1,340";
        ////    Dr[30] = "55%";
        ////    dt.Rows.Add(Dr);

        ////    Dr = dt.NewRow();
        ////    Dr[0] = "2004";
        ////    Dr[1] = "District";
        ////    Dr[2] = "1";
        ////    Dr[3] = "1";
        ////    Dr[4] = "1";
        ////    Dr[5] = "US";
        ////    Dr[6] = "";
        ////    Dr[7] = "";
        ////    Dr[8] = "";
        ////    Dr[9] = "";
        ////    Dr[10] = "";
        ////    Dr[11] = "";
        ////    Dr[12] = "";
        ////    Dr[13] = "High Schools";
        ////    Dr[14] = "";
        ////    Dr[15] = "B";
        ////    Dr[16] = "";
        ////    Dr[17] = "";
        ////    Dr[18] = "";
        ////    Dr[19] = "";
        ////    Dr[20] = "";
        ////    Dr[21] = "";
        ////    Dr[22] = "";
        ////    Dr[23] = "";
        ////    Dr[24] = "";
        ////    Dr[25] = "";
        ////    Dr[26] = "High Schools";
        ////    Dr[27] = "24,500";
        ////    Dr[28] = "Y";
        ////    Dr[29] = "18,300";
        ////    Dr[30] = "20.54%";
        ////    dt.Rows.Add(Dr);

            
        ////    Dr = dt.NewRow();
        ////    Dr[0] = "2004";
        ////    Dr[1] = "State";
        ////    Dr[2] = "1";
        ////    Dr[3] = "1";
        ////    Dr[4] = "1";
        ////    Dr[5] = "US";
        ////    Dr[6] = "";
        ////    Dr[7] = "";
        ////    Dr[8] = "";
        ////    Dr[9] = "";
        ////    Dr[10] = "";
        ////    Dr[11] = "";
        ////    Dr[12] = "";
        ////    Dr[13] = "High Schools";
        ////    Dr[14] = "";
        ////    Dr[15] = "";
        ////    Dr[16] = "";
        ////    Dr[17] = "";
        ////    Dr[18] = "";
        ////    Dr[19] = "";
        ////    Dr[20] = "";
        ////    Dr[21] = "";
        ////    Dr[22] = "";
        ////    Dr[23] = "";
        ////    Dr[24] = "";
        ////    Dr[25] = "";
        ////    Dr[26] = "High Schools";
        ////    Dr[27] = "290,500";
        ////    Dr[28] = "Y";
        ////    Dr[29] = "45,400";
        ////    Dr[30] = "72.4%";
        ////    dt.Rows.Add(Dr);

        ////    this.SligoDataGrid1.DataSource = dt;
        ////    SligoDataGrid1.DataBind();


        ////}

        #endregion
    }
}
