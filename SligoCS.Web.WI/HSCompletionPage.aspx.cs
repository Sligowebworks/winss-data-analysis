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
using SligoCS.BL.WI;
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.Base.WebServerControls.WI;

using SligoCS.Web.WI.WebSupportingClasses.WI;
using SligoCS.Web.WI.WebSupportingClasses;
using SligoCS.Web.WI.WebUserControls;


namespace SligoCS.Web.WI
{
    public partial class HSCompletionPage : PageBaseWI
    {
        protected override DALWIBase InitDatabase()
        {
            return new DALHSCompletion();
        }
        protected override GridView InitDataGrid()
        {
            return TimeFrameDataGrid;
        }
        protected override ChartFX.WebForms.Chart InitGraph()
        {
            return barChart;
        }
        protected override NavViewByGroup InitNavRowGroups()
        {
            nlrViewByGroup.LinksEnabled = NavViewByGroup.EnableLinksVector.EconElp;
            return nlrViewByGroup;
        }
        protected override string SetPageHeading()
        {
            return "What are the high school completion rates?";
        }
        protected override void OnInitComplete(EventArgs e)
        {
            GlobalValues.CurrentYear = 2012;

            //STYP not supported (but don't loose school type at school level for titling purposes).
            if (GlobalValues.OrgLevel.Key != OrgLevelKeys.School )GlobalValues.OverrideSchoolTypeWhenOrgLevelIsSchool_Complete += PageBaseWI.DisableSchoolType;

            if (GlobalValues.TmFrm.Key == TmFrmKeys.All 
                || GlobalValues.TmFrm.Key == TmFrmKeys.FourYear)
            {
                GlobalValues.TrendStartYear = 2010;
            }
            else if (GlobalValues.TmFrm.Key == TmFrmKeys.SixYear)
            {
                GlobalValues.TrendStartYear = 2012;
            }
            else if (GlobalValues.HighSchoolCompletion.Key == HighSchoolCompletionKeys.All)
            {
                GlobalValues.TrendStartYear = 2004;
            }
            else if (GlobalValues.Group.Key == GroupKeys.Disability)
            {
                GlobalValues.TrendStartYear = 2003;
            }
            else if (GlobalValues.Group.Key == GroupKeys.EconDisadv
           || GlobalValues.Group.Key == GroupKeys.EngLangProf)
            {
                GlobalValues.TrendStartYear = 2008;
            }
            else
            {
                GlobalValues.TrendStartYear = 1997;
            }

            GlobalValues.Grade.Key = GradeKeys.Grade_12;

            if (ViewByGroupOverrideRule()
                || GlobalValues.Group.Key == GroupKeys.Grade)
                GlobalValues.Group.Value = GlobalValues.Group.Range[GroupKeys.All];

            nlrViewByGroup.LinkRow.LinkControlAdded += ViewByLinkControlAdded;
            
            if (GlobalValues.OrgLevel.Key != OrgLevelKeys.School)
                GlobalValues.OverrideSchoolTypeWhenOrgLevelIsSchool_Complete += PageBaseWI.DisableSchoolType;

            //temporary default since there are no prior-years of Timeframes other than Legacy.
            if (!GlobalValues.inQS.Contains(GlobalValues.CompareTo.Name)
                && GlobalValues.Year == 2010)
            {
                GlobalValues.CompareTo.Key =
                    (GlobalValues.OrgLevel.Key == OrgLevelKeys.State)
                        ? CompareToKeys.Current
                        : CompareToKeys.OrgLevel;
            }

            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.District) QueryMarshaller.RaceDisagCodes.Remove((int)QueryMarshaller.RaceCodes.Comb);

            base.OnInitComplete(e);

        }

       protected void Page_Load(object sender, EventArgs e)
        {

            String dataType = GlobalValues.HighSchoolCompletion.Key;
            if (GlobalValues.HighSchoolCompletion.Key == HighSchoolCompletionKeys.All)
            {
                dataType = "All Credential Types";
            }

            String prefix = "High School Completion Rate - " + dataType + TitleBuilder.newline + GlobalValues.TmFrm.Key;
            DataSetTitle = GetTitle(prefix);
            String sTypeTitle = TitleBuilder.GetSchoolTypeInTitle(GlobalValues.STYP);
            if (TitleBuilder.GetSchoolTypeInTitle(GlobalValues.STYP) != String.Empty) DataSetTitle = DataSetTitle.Replace(sTypeTitle, String.Empty);

           ((WinssDataGrid)DataGrid).AddSuperHeader(DataSetTitle);
           ((WinssDataGrid)DataGrid).AddSuperHeader(GetSecondSuperHeader());

           List<String> dgOrder = QueryMarshaller.BuildOrderByList(DataSet.Tables[0].Columns);
           //timeframe should be the second sort factor
           dgOrder.Insert(1, "TimeFrameSort");

           barChart.OrderBy =  TimeFrameDataGrid.OrderBy = String.Join(",", dgOrder.ToArray());

            set_state();

            if (CountDimensions(SetIsDisaggFlags(GlobalValues)) > 2)
                HideGraphForDisAggOverflow();
            else
                SetUpChart(DataSet);
            
        }

        private int CountDimensions(Dictionary<String, Boolean> disaggFlags)
        {
            int count =0 ;
            foreach (KeyValuePair<String, Boolean> dimension in disaggFlags)
            {
                if (dimension.Value)
                    count++;
            }
            return count;
        }
        private void HideGraphForDisAggOverflow()
        {
            Graph.Visible = false;
        }
        private TableRow GetSecondSuperHeader()
        {
            TableRow tr = new TableRow();
            int span = 0;

            span = CountDimensions(SetIsDisaggFlags(GlobalValues)) + 2;
            if (GlobalValues.HighSchoolCompletion.Key == HighSchoolCompletionKeys.All)
                span--;

            WinssDataGrid.AddTableCell(tr, "&nbsp;", span); //first label column is always blank

            WinssDataGrid.AddTableCell(tr, "Noncompleters", 3);
            WinssDataGrid.AddTableCell(tr, "Completers", 3);

            return tr;
        }
        public Dictionary<String,Boolean> SetIsDisaggFlags(GlobalValues globals)
        {
            Dictionary<String,Boolean> disaggFlags = new Dictionary<string,bool>();

            disaggFlags.Add(globals.HighSchoolCompletion.Name, (globals.HighSchoolCompletion.Key == HighSchoolCompletionKeys.All));
            disaggFlags.Add(globals.TmFrm.Name, (globals.TmFrm.Key == TmFrmKeys.All));
            disaggFlags.Add(globals.Group.Name, (globals.Group.Key != GroupKeys.All));
            disaggFlags.Add(globals.CompareTo.Name, (globals.CompareTo.Key != CompareToKeys.Current));

            return disaggFlags;
        }
        private void SetUpChart(DataSet _ds)
        {

            barChart.Title = DataSetTitle;
            barChart.MaxRateInResult =99;

            if (GlobalValues.Group.Key == GroupKeys.All)
            {
                barChart.FriendlyAxisXNames = new List<String>(new String[] { "All Students"});
            }

            barChart.AxisYDescription = "Completion Rate\n" + (
                (GlobalValues.HighSchoolCompletion.Key == HighSchoolCompletionKeys.All)
                ? "All Credential Types" 
                : GlobalValues.HighSchoolCompletion.Key.ToString()
                );

            if (GlobalValues.HighSchoolCompletion.Key == HighSchoolCompletionKeys.All)
                SetUpChart_Stacked(_ds);
            else
                SetUpChart_Bar(_ds);
        }
        private void SetUpChart_Stacked(DataSet _ds)
        {
            barChart.Type = GraphBarChart.StackedType.Normal;
            
            barChart.MeasureColumns.Add(v_HSCWWoDisSchoolDistStateEconELPXYearRate.Regular_Diplomas_Percent);
            barChart.MeasureColumns.Add(v_HSCWWoDisSchoolDistStateEconELPXYearRate.HSEDs_Percent);
            barChart.MeasureColumns.Add(v_HSCWWoDisSchoolDistStateEconELPXYearRate.Certificates_Percent);

            if (GlobalValues.CompareTo.Key != CompareToKeys.Current)
                barChart.LabelColumnName = ColumnPicker.GetCompareToColumnName(GlobalValues);
            else
            {
                if (GlobalValues.TmFrm.Key == TmFrmKeys.All && GlobalValues.Group.Key == GroupKeys.All)
                    barChart.LabelColumnName = v_HSCWWoDisSchoolDistStateEconELPXYearRate.TimeFrameLabel;
                else if (GlobalValues.TmFrm.Key != TmFrmKeys.All)
                    barChart.OverrideAxisXLabels.Add("Both Groups Combined", "All Students");
                else
                    barChart.LabelColumnName = ColumnPicker.GetViewByColumnName(GlobalValues);
            }

             barChart.OverrideSeriesLabels.Add(v_HSCWWoDisSchoolDistStateEconELP.HSEDs, "HSED");
        }
        private void SetUpChart_Bar(DataSet _ds)
        {
            try
            {
                if (GlobalValues.HighSchoolCompletion.Key == HighSchoolCompletionKeys.Certificate)
                    barChart.DisplayColumnName = v_HSCWWoDisSchoolDistStateEconELPXYearRate.Certificates_Percent;
                else if (GlobalValues.HighSchoolCompletion.Key == HighSchoolCompletionKeys.HSED)
                    barChart.DisplayColumnName = v_HSCWWoDisSchoolDistStateEconELPXYearRate.HSEDs_Percent;
                else if (GlobalValues.HighSchoolCompletion.Key == HighSchoolCompletionKeys.Regular)
                    barChart.DisplayColumnName = v_HSCWWoDisSchoolDistStateEconELPXYearRate.Regular_Diplomas_Percent;
                else if (GlobalValues.HighSchoolCompletion.Key == HighSchoolCompletionKeys.Summary)
                    barChart.DisplayColumnName = v_HSCWWoDisSchoolDistStateEconELPXYearRate.Combined_Percent;

                if (GlobalValues.CompareTo.Key == CompareToKeys.Current)
                {
                    if (GlobalValues.TmFrm.Key == TmFrmKeys.All)
                    {
                        barChart.SeriesColumnName = v_HSCWWoDisSchoolDistStateEconELPXYearRate.TimeFrameLabel;

                        barChart.LabelColumnName = ColumnPicker.GetViewByColumnName(GlobalValues);
                    }
                }
                else
                {
                    if (GlobalValues.TmFrm.Key == TmFrmKeys.All)
                    {
                        barChart.SeriesColumnName = ColumnPicker.GetCompareToColumnName(GlobalValues);

                        barChart.LabelColumnName = v_HSCWWoDisSchoolDistStateEconELPXYearRate.TimeFrameLabel;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.dataLinksPanel, true);
        }

        public override List<string> GetVisibleColumns()
        {
            List<string> retval = base.GetVisibleColumns();
            retval.Remove(WebSupportingClasses.ColumnPicker.CommonNames.SchooltypeLabel.ToString());

            if (GlobalValues.CompareTo.Key == CompareToKeys.Current
                && retval.Contains(ColumnPicker.CommonNames.LinkedName.ToString()))
                retval.Remove(ColumnPicker.CommonNames.LinkedName.ToString());

            if (GlobalValues.TmFrm.Key == TmFrmKeys.All)
                retval.Add(v_HSCWWoDisSchoolDistStateEconELPXYearRate.TimeFrameLabel);

            retval.Add(v_HSCWWoDisSchoolDistStateEconELP.Total_Enrollment_Grade_12);
            retval.Add(v_HSCWWoDisSchoolDistStateEconELPXYearRate.Total_Expected_to_Complete_High_School_Count);

            retval.Add(v_HSCWWoDisSchoolDistStateEconELPXYearRate.Not_Continuing_Percent);
            retval.Add(v_HSCWWoDisSchoolDistStateEconELPXYearRate.Students_Who_Reached_the_Maximum_Age_Percent);
            retval.Add(v_HSCWWoDisSchoolDistStateEconELPXYearRate.Continuing_Percent);
            
            retval.Add(v_HSCWWoDisSchoolDistStateEconELPXYearRate.Certificates_Percent);
            retval.Add(v_HSCWWoDisSchoolDistStateEconELPXYearRate.HSEDs_Percent);
            retval.Add(v_HSCWWoDisSchoolDistStateEconELPXYearRate.Regular_Diplomas_Percent);
            
            return retval;
        }

        protected void ViewByLinkControlAdded(HyperLinkPlus theLink)
        {
            if (theLink.ID != "linkGroupAll"
                && ViewByGroupOverrideRule())
                theLink.Enabled = false;

            if (theLink.ID == "linkGroupGrade") theLink.Enabled = false;
        }
        private bool ViewByGroupOverrideRule()
        {
            // Use UserValues so that previous calls don't result in different result
            return
                (GlobalValues.HighSchoolCompletion.Key == HighSchoolCompletionKeys.All
                && GlobalValues.CompareTo.Key != CompareToKeys.Current)
            ;
        }

        public override List<string> GetDownloadRawVisibleColumns()
        {
            List<String> cols = base.GetDownloadRawVisibleColumns();
           
            int index = 0;

            index = cols.IndexOf(v_HSCWWoDisSchoolDistStateEconELPXYearRate.Not_Continuing_Percent);
            cols.Insert(index, v_HSCWWoDisSchoolDistStateEconELPXYearRate.Not_Continuing_Count);
                
            index = cols.IndexOf(v_HSCWWoDisSchoolDistStateEconELPXYearRate.Students_Who_Reached_the_Maximum_Age_Percent);
            cols.Insert(index, v_HSCWWoDisSchoolDistStateEconELPXYearRate.Maximum_Aged_Count);

            if (!cols.Contains(v_HSCWWoDisSchoolDistStateEconELPXYearRate.Continuing_Percent))
                cols.Add(v_HSCWWoDisSchoolDistStateEconELPXYearRate.Continuing_Percent);

            index = cols.IndexOf(v_HSCWWoDisSchoolDistStateEconELPXYearRate.Continuing_Percent);
            cols.Insert(index, v_HSCWWoDisSchoolDistStateEconELPXYearRate.Continuing_Count);

            index = cols.IndexOf(v_HSCWWoDisSchoolDistStateEconELPXYearRate.Certificates_Percent);
            cols.Insert(index, v_HSCWWoDisSchoolDistStateEconELPXYearRate.Certificate_Count);

            index = cols.IndexOf(v_HSCWWoDisSchoolDistStateEconELPXYearRate.HSEDs_Percent);
            cols.Insert(index, v_HSCWWoDisSchoolDistStateEconELPXYearRate.HSED_Count);

            index = cols.IndexOf(v_HSCWWoDisSchoolDistStateEconELPXYearRate.Regular_Diplomas_Percent);
            cols.Insert(index, v_HSCWWoDisSchoolDistStateEconELPXYearRate.Regular_Count);

            cols.Add(v_HSCWWoDisSchoolDistStateEconELPXYearRate.Completers_Combined_Count);
            cols.Add(v_HSCWWoDisSchoolDistStateEconELPXYearRate.Combined_Percent);
            
            return cols;
        }

        protected override SortedList<string, string> GetDownloadRawColumnLabelMapping()
        {
            SortedList<string, string> newLabels = base.GetDownloadRawColumnLabelMapping();
            newLabels.Add(v_HSCWWoDisSchoolDistStateEconELP.Cohort_Dropouts, "cohort_dropouts_percent");
            newLabels.Add(v_HSCWWoDisSchoolDistStateEconELP.Students_Who_Reached_the_Maximum_Age, "students_who_reached_the_maximum_age_percent");
            newLabels.Add(v_HSCWWoDisSchoolDistStateEconELP.Certificates, "certificates_percent");
            newLabels.Add(v_HSCWWoDisSchoolDistStateEconELP.HSEDs, "hseds_percent");
            newLabels.Add(v_HSCWWoDisSchoolDistStateEconELP.Regular_Diplomas, "regular_diplomas_percent");
            newLabels.Add(v_HSCWWoDisSchoolDistStateEconELP.Combined, "combined_percent");
            return newLabels;
        }
    }
}
