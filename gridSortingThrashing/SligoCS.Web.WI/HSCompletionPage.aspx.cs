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
using SligoCS.Web.Base.WebServerControls.WI;
using SligoCS.Web.WI.HelperClasses;
using SligoCS.Web.WI.WebSupportingClasses.WI;
using SligoCS.Web.WI.WebUserControls;


namespace SligoCS.Web.WI
{
    public partial class HSCompletionPage : PageBaseWI
    {
        protected v_HSCWWoDisSchoolDistStateEconELP _ds = null;
        private string graphTitle = string.Empty;

        protected override DALWIBase InitDatabase()
        {
            return new DALHSCompletion();
        }
        protected override DataSet InitDataSet()
        {
            _ds = new v_HSCWWoDisSchoolDistStateEconELP();
            return _ds;
        }
        protected override GridView InitDataGrid()
        {
            return this.DataGrid;
        }
        protected override string SetPageHeading()
        {
            return "What are the high school completion rates?";
        }
        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
            if (GlobalValues.HighSchoolCompletion.Key == HighSchoolCompletionKeys.All)
            {
                GlobalValues.TrendStartYear = 2004;
            }// Group Options only available when HighSchoolCompletion (Credential) <> All:
            else if (GlobalValues.Group.Key == GroupKeys.Disability)
            {
                GlobalValues.TrendStartYear = 2003;
            }
            else if (GlobalValues.Group.Key == GroupKeys.EconDisadv
                || GlobalValues.Group.Key == GroupKeys.EngLangProf)
            {
                GlobalValues.TrendStartYear = 2008;
            }

            GlobalValues.Grade = 64;

            if (GlobalValues.HighSchoolCompletion.Key == HighSchoolCompletionKeys.All
                && GlobalValues.Group.Key != GroupKeys.All)
                GlobalValues.Group.Value = GlobalValues.Group.Range[GroupKeys.All];

            nlrVwBy.LinkControlAdded += LinkControlAdded;

            if (GlobalValues.OrgLevel.Key != OrgLevelKeys.School)
                GlobalValues.STYP.Value = GlobalValues.STYP.Range[STYPKeys.StateSummary];
        }
       protected void Page_Load(object sender, EventArgs e)
        {
            String dataType = GlobalValues.HighSchoolCompletion.Key;
            if (GlobalValues.HighSchoolCompletion.Key == HighSchoolCompletionKeys.All)
            {
                dataType = "All  Credential Types";
            }

            String prefix = "High School Completion Rate - " + dataType + "<br/>";

            this.DataGrid.AddSuperHeader(GetTitle(prefix));

            this.DataGrid.SetVisibleColumns(
                GetVisibleColumns(
                    GlobalValues.Group,
                    GlobalValues.OrgLevel,
                    GlobalValues.CompareTo,
                    GlobalValues.STYP)
            );

            if (base.GlobalValues.DETAIL != null &&
                    base.GlobalValues.DETAIL.ToString() == "NO")
            {
                this.DataGrid.Visible = false;
            }
            setBottomLink();
            set_state();

            ////Notes:  For graph 
            graphTitle = GetTitle(prefix); //("High School Completion Rate");
            GraphPanel.Visible = false;
            if (CheckIfGraphPanelVisible(GraphPanel) == true)
            {
                SetUpChart(_ds);
            }
            ////  For graph 
        }
        private void SetUpChart(v_HSCWWoDisSchoolDistStateEconELP _ds)
        {
            try
            {
                graphTitle = graphTitle.Replace("<br/>", "\n");
                graphTitle = graphTitle.Replace(" - All Students", "All Students");
                barChart.Title =  graphTitle ;
                //HT May 26, 09: Jim, Mike: the If statement below is for the display of 
                // All Types- Year
                if (GlobalValues.HighSchoolCompletion.Key == HighSchoolCompletionKeys.All)
                {
                    barChart.LegendBox.Height = 60;
                    barChart.LegendBox.Width = 600;
                    barChart.LegendBox.MarginX = 38;
                    barChart.LegendBox.ContentLayout = ChartFX.WebForms.ContentLayout.Near;
                    barChart.LegendBox.PlotAreaOnly = false;
                }
                 // HT May 26, 09: Mike,I added in the below code and commented the method  GetMaxRateInResult(_ds)
                // out so that the y-axis labels are displayed as decimal eg. 100.0%
                // instead of just 100%
                 barChart.AxisY.LabelsFormat.Decimals = 1;
                 barChart.AxisY.DataFormat.Decimals = 1;
                 barChart.AxisY.LabelsFormat.Format = ChartFX.WebForms.AxisFormat.Percentage;
                 barChart.AxisY.DataFormat.Format = ChartFX.WebForms.AxisFormat.Percentage;
                 barChart.AxisY.ScaleUnit = 100;
                 barChart.AxisY.Step = 10;
                //barChart.MaxRateInResult = GetMaxRateInResult(_ds);
                 //end HT May 26, 09 comment
                if (GlobalValues.Group.Key == GroupKeys.All)
                {
                    ArrayList friendlyAxisXName = new ArrayList();
                    friendlyAxisXName.Add("All Students");
                     barChart.FriendlyAxisXName = friendlyAxisXName;
                }
                if (GlobalValues.HighSchoolCompletion.Key == HighSchoolCompletionKeys.All)
                    barChart.AxisYDescription = "Completion Rate All Credential Types";
                else
                    barChart.AxisYDescription = "Completion Rate - " + GlobalValues.HighSchoolCompletion.Key.ToString();
                //Bind Data Source & Display
                 barChart.DisplayColumnName =_ds._v_HSCWWoDisSchoolDistStateEconELP.Regular_DiplomasColumn.ColumnName;
                 barChart.RawDataSource = _ds.Tables[0];
                 barChart.DataBind();
                 if (GlobalValues.HighSchoolCompletion.Key == HighSchoolCompletionKeys.All)
                 { //stacked bars
                     barChart.Data.Series = 3;
                     barChart.Data.Points = _ds.Tables[0].Rows.Count;
                     barChart.AllSeries.Stacked = ChartFX.WebForms.Stacked.Normal;
                     for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                     {
                         barChart.Data[0, i] = Convert.ToDouble(_ds.Tables[0].Rows[i]["Regular Diplomas"].ToString());
                         barChart.Data[1, i] = Convert.ToDouble(_ds.Tables[0].Rows[i]["HSEDs"].ToString());
                         barChart.Data[2, i] = Convert.ToDouble(_ds.Tables[0].Rows[i]["Certificates"].ToString());
                         barChart.AxisX.Labels[i] = _ds.Tables[0].Rows[i]["YearFormatted"].ToString();
                     }
                     barChart.Series[0].Text = "Regular Diploma";
                     barChart.Series[1].Text = "HSED";
                     barChart.Series[2].Text = "Certificates";
                 }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
       
        private double GetMaxRateInResult(v_HSCWWoDisSchoolDistStateEconELP _ds)
        {
            double maxRateInResult = 0;

            foreach (DataRow row in _ds.Tables[0].Rows)
            {
                try
                {
                    if (Convert.ToDouble(
                            row[_ds._v_HSCWWoDisSchoolDistStateEconELP.
                                Regular_DiplomasColumn.ColumnName].ToString())
                                        > maxRateInResult)
                    {
                        maxRateInResult = Convert.ToDouble
                                (row[_ds._v_HSCWWoDisSchoolDistStateEconELP.
                                    Regular_DiplomasColumn.ColumnName].ToString());
                    }
                }
                catch
                {
                    continue;
                }
            }
            return maxRateInResult;
        }
        //// end graph 
        private void setBottomLink()
        {
            BottomLinkViewReport1.Year = GlobalValues.Year.ToString();
            BottomLinkViewReport1.DistrictCd = GetDistrictCdByFullKey();
            BottomLinkViewReport1.SchoolCd = GetSchoolCdByFullKey();
            //BottomLinkViewProfile1.DistrictCd = GetDistrictCdByFullKey();
        }

        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.dataLinksPanel, true);
        }

        public override List<string> GetVisibleColumns(Group viewBy, OrgLevel orgLevel, CompareTo compareTo, STYP schoolType)
        {
            List<string> retval = base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);
            retval.Remove(CommonColumnNames.SchooltypeLabel.ToString());
            retval.Add("Total Enrollment Grade 12");
            retval.Add("Total Expected to Complete High School");
            retval.Add("Cohort Dropouts");
            retval.Add("Students Who Reached the Maximum Age");
            retval.Add("Certificates");
            retval.Add("HSEDs");
            retval.Add("Regular Diplomas");

            return retval;
        }

        protected void DataGrid_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //decode the link to specific schools, so that it appears as a normal URL link.
                int colIndex = DataGrid.FindBoundColumnIndex(
                    _ds._v_HSCWWoDisSchoolDistStateEconELP.
                    LinkedNameColumn.ColumnName);
                e.Row.Cells[colIndex].Text = Server.HtmlDecode(
                    e.Row.Cells[colIndex].Text);

                FormatHelper formater = new FormatHelper();
                formater.SetRaceAbbr(DataGrid, e.Row, 
                    _ds._v_HSCWWoDisSchoolDistStateEconELP.RaceLabelColumn.ToString());
            }
        }
        protected void LinkControlAdded(HyperLinkPlus theLink)
        {
            if (theLink.ID != "linkGroupAll"
                && GlobalValues.HighSchoolCompletion.Key == HighSchoolCompletionKeys.All)
                theLink.Enabled = false;
        }
    }
}
