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
using SligoCS.Web.WI.WebSupportingClasses.WI;

using ChartFX.WebForms;

namespace SligoCS.Web.WI
{

    /// <summary>
    /// This page was the initial demonstration of the SligoDataGrid.
    /// This page is also known as the Staff page.
    /// </summary>
    public partial class StaffPage : PageBaseWI
    {

        protected v_Staff dsStaff;
        private string graphTitle = string.Empty;
        
        protected override DALWIBase InitDatabase()
        {
            return new DALStaff();
        }
        protected override DataSet InitDataSet()
        {
            v_Staff dsStaff = new v_Staff();
            return dsStaff;
        }
        protected override string SetPageHeading()
        {
            return "What staff are available in this district?";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            set_state();

            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.School)
            {
                //DataPanel.Visible = false;
                //NoDataForSchoolPanel.Visible = true;
                string qString = UserValues.GetBaseQueryString();
                qString = QueryStringUtils.ReplaceQueryString(
                    qString, 
                    GlobalValues.OrgLevel.Name,
                    GlobalValues.OrgLevel.Range[OrgLevelKeys.District]
                   );
                qString = QueryStringUtils.ReplaceQueryString(
                    qString,
                    "fullkey",
                    FullKeyUtils.DistrictFullKey(GlobalValues.FULLKEY)
                    );
                
                GraphPanel.Visible = false;

                //qString = QueryStringUtils.ReplaceQueryString(qString,
                //    "MR", "Yes");  //"MR" is for staff page re-driected
                //Response.Redirect("~/StaffPage.aspx" + qString, true);
            }
            else
            {
                ////NoDataForSchoolPanel.Visible = false;

                //if (Request.QueryString["MR"] == "Yes") //"MR" is for staff page re-driected
                //{
                //    this.DistrictDataProvided.Text = "District level data are provided. ";
                //}
                //else
                //{
                //    this.DistrictDataProvided.Text = string.Empty;
                //}

                dsStaff = (v_Staff)DataSet;

                SetVisibleColumns2(SligoDataGrid2, dsStaff, GlobalValues.Group,
                    GlobalValues.OrgLevel, GlobalValues.CompareTo, GlobalValues.STYP);

                this.SligoDataGrid2.DataSource = dsStaff;
                this.SligoDataGrid2.DataBind();

                graphTitle = GetTitle("Staff Rate<br/>");

                this.SligoDataGrid2.AddSuperHeader(graphTitle);

                this.SligoDataGrid2.ShowSuperHeader = true;

                if (base.GlobalValues.DETAIL != null &&
                        base.GlobalValues.DETAIL.ToString() == "NO")
                {
                    this.SligoDataGrid2.Visible = false;
                }

                setBottomLink();

                ////Notes:  graph 
                GraphPanel.Visible = false;
                if (CheckIfGraphPanelVisible( GraphPanel) == true)
                {
                    SetUpChart(dsStaff);
                }
                //// graph 
            }
        }

        private void setBottomLink()
        {
            BottomLinkViewReport1.Year = GlobalValues.Year.ToString();
            BottomLinkViewReport1.DistrictCd = GetDistrictCdByFullKey();
            BottomLinkViewReport1.SchoolCd = GetSchoolCdByFullKey();
            BottomLinkViewProfile1.DistrictCd = GetDistrictCdByFullKey();
        }

        /// <summary>
        ///  Set up graph 
        /// </summary>
        /// <param name="ds"></param>
        private void SetUpChart(v_Staff ds)
        {
            try
            {
                graphTitle = graphTitle.Replace("<br/>", "\n");
                barChart.Title = graphTitle;

                //Bind Data Source & Display
                if (GlobalValues.StaffRatio.Key == StaffRatioKeys.Student)
                {
                    //Customize Axis-Y settings
                    barChart.AxisYMin = 0;
                    barChart.AxisYStep = 20;
                    int maxValueInResult = (int)GetMaxValueInResult(ds);
                    int axisYMax = (maxValueInResult / 20) * 20 + 20;
                    if (maxValueInResult - axisYMax <= 10)
                    {
                        axisYMax += 20;
                    }
                    barChart.AxisYMax = axisYMax;

                    barChart.AxisYDescription = "Students to FTE Staff";
                    barChart.Type = StackedType.No;
                    barChart.DisplayColumnName =
                        ds._v_Staff.Ratio_of_Students_to_FTE_StaffColumn.ColumnName;

                    //Set Column paramName
                    barChart.LabelColumnName = "Category";

                    barChart.RawDataSource = ds.Tables[0];
                    barChart.DataBind();
                }
                else
                {
                    //Customize Axis-Y settings
                    barChart.AxisYMin = 0;
                    barChart.AxisYStep = 2;
                    barChart.AxisYMax = 24;

                    barChart.AxisXDescription = "Staff per 100 Students";
                    barChart.Type = StackedType.Normal;
                    barChart.DisplayColumnName =
                        ds._v_Staff.FTE_Staff_per_100_StudentsColumn.ColumnName;

                    //Set Column paramName
                    barChart.LabelColumnName = ColumnPicker.GetCompareToColumnName();
                    barChart.SeriesColumnName = "Category";
                    
                    barChart.RawDataSource = RemoveTotalPreGraph(ds).Tables[0];
                    barChart.DataBind();
                }
               
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        public string GetCompareToColumnName()
        {
            if (GlobalValues.CompareTo.Key == CompareToKeys.OrgLevel)
            {
                //The old site used DistState, but the new site use [District paramName]
                return "District Name";
            }
            else
            {
                return ColumnPicker.GetCompareToColumnName();
            }
        }

        //Notes For Graph
        private double GetMaxValueInResult(v_Staff ds)
        {
            double maxRateInResult = 0;

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                try
                {
                    if (Convert.ToDouble(
                            row[ds._v_Staff.Ratio_of_Students_to_FTE_StaffColumn.ColumnName].ToString())
                                        > maxRateInResult)
                    {
                        maxRateInResult = Convert.ToDouble
                                (row[ds._v_Staff.Ratio_of_Students_to_FTE_StaffColumn.ColumnName].ToString());
                    }
                }
                catch
                {
                    continue;
                }
            }
            return maxRateInResult;
        }

        //Notes For Graph
        private DataSet RemoveTotalPreGraph(DataSet ds)
        {
            DataSet dsReturn = ds.Clone();

            ds.Tables[0].Columns["Category"].ReadOnly = false;
            string condition = "[Category] not like '%Total%'";
            DataRow[] rows = ds.Tables[0].Select(condition);
            DataRow[] newRows = new DataRow[rows.Length];
            rows.CopyTo(newRows, 0);
            foreach (DataRow row in newRows)
            {
                //Remove the space in the Column
                //row["Category"] = row["Category"].ToString().Trim();
                dsReturn.Tables[0].ImportRow(row);
            }

            dsReturn.AcceptChanges();
            return dsReturn;
        }    

        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                (WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                (WI.displayed_obj.dataLinksPanel, true);
        }

        public override List<string> GetVisibleColumns(
            Group viewBy, OrgLevel orgLevel, 
            CompareTo compareTo, STYP schoolType)
        {
            List<string> retval = base.GetVisibleColumns
                (viewBy, orgLevel, compareTo, schoolType);

            retval.Add(dsStaff._v_Staff.CategoryColumn.ColumnName);
            retval.Add(dsStaff._v_Staff.Number_FTE_StaffColumn.ColumnName);

            //if ( compareTo == CompareToEnum.SELDISTRICTS )
            //{
            //    retval.Add(dsStaff._v_Staff.LinkedDistrictNameColumn.ColumnName);
            //}

            if (compareTo.Key == CompareToKeys.SelSchools && orgLevel.Key == OrgLevelKeys.District)
            {
                retval.Add(dsStaff._v_Staff.LinkedDistrictNameColumn.ColumnName);
            }
            //else if (compareTo == CompareToEnum.SELSCHOOLS)
            //{
            //    retval.Add(dsStaff._v_Staff.School_NameColumn.ColumnName);
            //}

            //if (compareTo == CompareToEnum.DISTSTATE)
            //{
            //    retval.Add(dsStaff._v_Staff.DistStateColumn.ColumnName);
            //}

            if (GlobalValues.StaffRatio.Key == StaffRatioKeys.Student)
            {
                retval.Add(dsStaff._v_Staff.Ratio_of_Students_to_FTE_StaffColumn.
                    ColumnName);
            }
            else
            {
                retval.Add(dsStaff._v_Staff.FTE_Staff_per_100_StudentsColumn.ColumnName);
            }
            return retval;
        }

        protected void SligoDataGrid2_RowDataBound(
            Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int colIndex = SligoDataGrid2.FindBoundColumnIndex(
                    dsStaff._v_Staff.LinkedDistrictNameColumn.ColumnName);
                e.Row.Cells[colIndex].Text = Server.HtmlDecode
                    (e.Row.Cells[colIndex].Text);
                
                //colIndex = SligoDataGrid2.FindBoundColumnIndex(
                //    dsStaff._v_Staff.LinkedNameColumn.ColumnName);
                //e.Row.Cells[colIndex].Text = Server.HtmlDecode
                //    (e.Row.Cells[colIndex].Text);

                //format the numerical values
                SligoDataGrid2.SetCellToFormattedDecimal(e.Row, 
                    dsStaff._v_Staff.Ratio_of_Students_to_FTE_StaffColumn.ColumnName, 
                    Constants.FORMAT_RATE_03);
            }
        }
    }
}
