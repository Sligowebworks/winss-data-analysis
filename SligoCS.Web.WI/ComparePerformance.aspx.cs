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
using SligoCS.Web.WI.WebSupportingClasses;

using System.Text;
using SligoCS.Web.WI.WebSupportingClasses.WI;
using ChartFX.WebForms;

namespace SligoCS.Web.WI
{
    public partial class ComparePerformance : PageBaseWI
    {
        private List<String> GradeCodesActive = new List<String>();

        protected override DALWIBase InitDatabase()
        {
            return new DALWSAS();
        }
        protected override GridView InitDataGrid()
        {
            return CompareContinuingDataGrid;
        }
        protected override DataSet InitDataSet()
        {
            return base.InitDataSet();
        }
        protected override Chart InitGraph()
        {
            return barChart;
        }
        protected override SligoCS.Web.WI.WebUserControls.NavViewByGroup InitNavRowGroups()
        {
            return base.InitNavRowGroups();
        }
         protected override string SetPageHeading()
        {
            return "How did performance of all students in my school compare to performance of continuing students only?";
        }
        protected override void OnInitComplete(EventArgs e)
        {
            GlobalValues.CurrentYear = 2013;
            GlobalValues.TrendStartYear = 2006;

            GlobalValues.OverrideByNavLinksNotPresent(GlobalValues.Grade, nlrGrade, GradeKeys.Combined_PreK_12);
            GlobalValues.OverrideByNavLinksNotPresent(GlobalValues.SubjectID, nlrSubject, SubjectIDKeys.Reading);

            GradeCodesActive = getGradeCodeRange();
            if (!GradeCodesActive.Contains(GlobalValues.Grade.Value)) GlobalValues.Grade.Key = GradeKeys.Combined_PreK_12;

            nlrGrade.LinkControlAdded += new LinkControlAddedHandler(disableGradeLinks_LinkControlAdded);

            GlobalValues.OverrideSchoolTypeWhenOrgLevelIsSchool_Complete += PageBaseWI.DisableSchoolType;
            GlobalValues.CompareTo.Key = CompareToKeys.Current;
            GlobalValues.Group.Key = GroupKeys.All;

            QueryMarshaller.gradeCodes.ObeyForceDisAgg = true;
            QueryMarshaller.WsasSubjectCodes.ObeyForceDisAgg = true;

            base.OnInitComplete(e);
        }

        void disableGradeLinks_LinkControlAdded(HyperLinkPlus link)
        {
            if (!GradeCodesActive.Contains(link.ParamValue)) link.Enabled = false;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSetTitle = GetTitleWithoutGroupForSchoolTypeUnsupported(
                    ((GlobalValues.WOW.Key == WOWKeys.WSASCombined) ?
                        WOWKeys.WSASCombined : WOWKeys.WKCE) + " - " +
                    ((GlobalValues.Grade.Key == GradeKeys.Combined_PreK_12) ? 
                    (GlobalValues.Grade.Key.ToString()) : ("Grade " + GlobalValues.Grade.Key.ToString())) 
                        + " - " + GlobalValues.SubjectID.Key
                ).Replace(TitleBuilder.GetYearRangeInTitle(QueryMarshaller.years),
                    "November " + ((GlobalValues.Year)-1).ToString() + "* Data" ).Replace("Summary - All School Types Combined", "Summary")
                ;

            CompareContinuingDataGrid.AddSuperHeader(DataSetTitle);

            SetUpChart();
            set_state();
            setBottomLink();
        }
         private void SetUpChart()
         {
             barChart.Title = DataSetTitle;
             barChart.Type = SligoCS.Web.WI.WebUserControls.GraphBarChart.StackedType.Stacked100;
             barChart.SeriesColors = WebUserControls.GraphHorizBarChart.GetWSASStackedBarSeriesColors();
                 //barChart.MaxRateInResult = WebUserControls.GraphBarChart.GetMaxRateInColumn(DataSet.Tables[0], v_WSAS.AdvancedWSAS);
             barChart.AxisYDescription = "Percent of Students";

             barChart.LabelColumnName = v_WSAS.FAYShortLabel;

             List<String> graphColumns = barChart.MeasureColumns;
             if (GlobalValues.WOW.Key == WOWKeys.WKCE)
             {
                 graphColumns.Add(v_WSAS.Percent_Advanced);
                 graphColumns.Add(v_WSAS.Percent_Proficient);
                 graphColumns.Add(v_WSAS.Percent_Basic);
                 graphColumns.Add(v_WSAS.Percent_Minimal);
                 graphColumns.Add(v_WSAS.Percent_PreReq_Eng);
                 graphColumns.Add(v_WSAS.Percent_PreReq_Skill);
                 graphColumns.Add(v_WSAS.No_WSAS_Total);
             }
             else// if (GlobalValues.WOW.Key == WOWKeys.WSASCombined)
             {
                 graphColumns.Add(v_WSAS.AdvancedWSAS);
                 graphColumns.Add(v_WSAS.ProficientWSAS);
                 graphColumns.Add(v_WSAS.BasicWSAS);
                 graphColumns.Add(v_WSAS.MinPerfWSAS);
                 graphColumns.Add(v_WSAS.No_WSAS_Total);
             }
             
             //barChart.SeriesColumnName = ;
             barChart.OverrideSeriesLabels = new Hashtable(6);
             barChart.OverrideSeriesLabels.Add(v_WSAS.AdvancedWSAS, "Advanced WSAS");
             barChart.OverrideSeriesLabels.Add(v_WSAS.BasicWSAS, "Basic WSAS");
             barChart.OverrideSeriesLabels.Add(v_WSAS.ProficientWSAS, "Proficient WSAS");
             barChart.OverrideSeriesLabels.Add(v_WSAS.MinPerfWSAS, "Min Perf WSAS");
             barChart.OverrideSeriesLabels.Add(v_WSAS.Percent_Advanced, "Advanced WKCE");
             barChart.OverrideSeriesLabels.Add(v_WSAS.Percent_Proficient, "Proficient WKCE");
             barChart.OverrideSeriesLabels.Add(v_WSAS.Percent_Basic, "Basic WKCE");
             barChart.OverrideSeriesLabels.Add(v_WSAS.Percent_Minimal, "Minimal WKCE");
             barChart.OverrideSeriesLabels.Add(v_WSAS.Percent_PreReq_Eng, "WAA-ELL");
             barChart.OverrideSeriesLabels.Add(v_WSAS.Percent_PreReq_Skill, "WAA-SwD");

         }
        public override List<string> GetVisibleColumns()
        {
            List<string> cols = base.GetVisibleColumns();

            if (cols.Contains(v_WSAS.LinkedName)) cols.Remove(v_WSAS.LinkedName);
            cols.Add(v_WSAS.FAYLabel);

            cols.Add(v_WSAS.Enrolled);
            cols.Add(v_WSAS.No_WSAS_Total);
            if (GlobalValues.WOW.Key == WOWKeys.WKCE)
            {
                cols.Add(v_WSAS.Percent_PreReq_Skill);
                cols.Add(v_WSAS.Percent_PreReq_Eng);
                cols.Add(v_WSAS.Percent_Minimal);
                cols.Add(v_WSAS.Percent_Basic);
                cols.Add(v_WSAS.Percent_Proficient);
                cols.Add(v_WSAS.Percent_Advanced);
            }
            else
            {
                cols.Add(v_WSAS.MinPerfWSAS);
                cols.Add(v_WSAS.BasicWSAS);
                cols.Add(v_WSAS.ProficientWSAS);
                cols.Add(v_WSAS.AdvancedWSAS);
            }

            return cols;
        }
        public List<String> getGradeCodeRange()
        {
            List<String> codes = new List<String>();
            QueryMarshaller qm = new QueryMarshaller(GlobalValues);
            GlobalValues.SQL = DALWSAS.WSASGrades(qm);
            qm.AssignQuery(new DALWSAS(), GlobalValues.SQL);
            DataSet result = qm.Database.DataSet.Copy();
            if (result == null || result.Tables[0].Rows.Count == 0) return codes;
            //throw new Exception(tbl.GetLength(0).ToString() + " || " + tbl[0][0].ToString());
            foreach (DataRow row in result.Tables[0].Rows)
            {
                codes.Add(row[0].ToString());
            }
            return codes;
        }
        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(
                   WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(
                       WI.displayed_obj.dataLinksPanel, true);
        }
        private void setBottomLink()
        {
            BottomLinkViewProfile1.DistrictCd = GlobalValues.DistrictCode;
        }


        public override List<string> GetDownloadRawVisibleColumns()
        {
            List<string> cols = base.GetDownloadRawVisibleColumns();
            int index;
            index = cols.IndexOf(v_WSAS.No_WSAS_Total);
            cols.Insert(index, v_WSAS.Number_No_WSAS_Total);
            cols.Insert(index, v_WSAS.SubjectLabel);
            if (GlobalValues.WOW.Key == WOWKeys.WKCE)
            {
                index = cols.IndexOf(v_WSAS.Percent_PreReq_Skill);
                cols.Insert(index, v_WSAS.Number_PreReq_Skill);
                index = cols.IndexOf(v_WSAS.Percent_PreReq_Eng);
                cols.Insert(index, v_WSAS.Number_PreReq_Eng);
                index = cols.IndexOf(v_WSAS.Percent_Minimal);
                cols.Insert(index, v_WSAS.Number_Minimal);
                index = cols.IndexOf(v_WSAS.Percent_Basic);
                cols.Insert(index, v_WSAS.Number_Basic);
                index = cols.IndexOf(v_WSAS.Percent_Proficient);
                cols.Insert(index, v_WSAS.Number_Proficient);
                index = cols.IndexOf(v_WSAS.Percent_Advanced);
                cols.Insert(index, v_WSAS.Number_Advanced);
            }
            else
            {
                index = cols.IndexOf(v_WSAS.MinPerfWSAS);
                cols.Insert(index, v_WSAS.Number_MinPerfWSAS);
                index = cols.IndexOf(v_WSAS.BasicWSAS);
                cols.Insert(index, v_WSAS.Number_BasicWSAS);
                index = cols.IndexOf(v_WSAS.ProficientWSAS);
                cols.Insert(index, v_WSAS.Number_ProficientWSAS);
                index = cols.IndexOf(v_WSAS.AdvancedWSAS);
                cols.Insert(index, v_WSAS.Number_AdvancedWSAS);
            }
            cols.Remove(v_WSASDemographics.RaceLabel);
            cols.Remove(v_WSASDemographics.SexLabel);
            cols.Remove(v_WSASDemographics.DisabilityLabel);
            cols.Remove(v_WSASDemographics.EconDisadvLabel);
            cols.Remove(v_WSASDemographics.ELPLabel);
            cols.Remove(v_WSASDemographics.MigrantLabel);
            return cols;
        }

        protected override SortedList<string, string> GetDownloadRawColumnLabelMapping()
        {
            SortedList<string, string> newLabels = base.GetDownloadRawColumnLabelMapping();
            return StateTestsPerformance.WsasColumnLabelMapping( newLabels);
        }
    }
}
