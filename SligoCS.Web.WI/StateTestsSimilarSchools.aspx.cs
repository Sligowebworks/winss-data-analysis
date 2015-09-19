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
using SligoCS.BL.WI;
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.Base.WebServerControls.WI;

using SligoCS.Web.WI.WebSupportingClasses.WI;
using SligoCS.Web.WI.WebUserControls;

namespace SligoCS.Web.WI
{
    public partial class StateTestsSimilarSchools : PageBaseWI
    {
        protected override DALWIBase InitDatabase()
        {
            return new DALWSASSimilarSchools();
        }
        protected override GridView InitDataGrid()
        {
            return SimilarDataGrid;
        }
        protected override ChartFX.WebForms.Chart InitGraph()
        {
            return barChart;
        }
        //protected override DataSet InitDataSet()
        //protected override NavViewByGroup InitNavRowGroups()
        protected override string SetPageHeading()
        {
            return "Are there similar** districts that might provide ideas to try?";
        }
        protected override void OnInit(EventArgs e)
        {
            OnCheckPrerequisites += new CheckPrerequisitesHandler(StateTestsSimilarSchools_OnCheckPrerequisites);
            base.OnInit(e);
        }

        void StateTestsSimilarSchools_OnCheckPrerequisites(PageBaseWI page, EventArgs args)
        {
            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.State) return;

            if (FullKeyUtils.GetOrgLevelFromFullKeyX(GlobalValues.FULLKEY).Key == OrgLevelKeys.State)
                OnRedirectUser += InitialAgencyRedirect;
        }
        protected override void OnInitComplete(EventArgs e)
        {
            GlobalValues.Year = 2010;

            //View By Group Unsupported.
            GlobalValues.Group.Key = GroupKeys.All;
            //SchoolType Unsupported.
            GlobalValues.OverrideSchoolTypeWhenOrgLevelIsSchool_Complete += PageBaseWI.DisableSchoolType;
            //StateLevel Unsupported.
            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.State) GlobalValues.OrgLevel.Key = OrgLevelKeys.District;

            GlobalValues.FAYCode.Key = FAYCodeKeys.FAY;

            //Subjects
            GlobalValues.OverrideByNavLinksNotPresent(GlobalValues.SubjectID, nlrSubject, SubjectIDKeys.Reading);
            if (GlobalValues.CompareTo.Key != CompareToKeys.Current || GlobalValues.Group.Key != GroupKeys.All)
            {
                nlrSubject.LinkControlAdded += new LinkControlAddedHandler(disableSubjects_LinkControlAdded);
                if (GlobalValues.SubjectID.Key == SubjectIDKeys.AllTested) GlobalValues.SubjectID.Key = SubjectIDKeys.Reading;
            }

            //Grades
            GlobalValues.OverrideByNavLinksNotPresent(GlobalValues.Grade, nlrGrade, GradeKeys.Combined_PreK_12);
            GlobalValues.GradeCodesActive = StateTestsScatterplot.getGradeCodeRange(this);
            if (!GlobalValues.GradeCodesActive.Contains(GlobalValues.Grade.Value)) GlobalValues.Grade.Value = GlobalValues.GradeCodesActive[0];  //default to minimum grade

            nlrGrade.LinkControlAdded += new LinkControlAddedHandler(StateTestsScatterplot.disableGradeLinks_LinkControlAdded);

            //Level (Sort By)
            GlobalValues.OverrideByNavLinksNotPresent(GlobalValues.Level, nlrLevel, LevelKeys.AdvancedProficient);
            if (GlobalValues.Level.Key == LevelKeys.All) GlobalValues.Level.Key = LevelKeys.AdvancedProficient;

            if (GlobalValues.WOW.Key == WOWKeys.WSASCombined
                && (GlobalValues.Level.Key == LevelKeys.WAA_ELL || GlobalValues.Level.Key == LevelKeys.WAA_SwD))
                GlobalValues.Level.Key = LevelKeys.NoWSAS;

            //When no Choice On, then override everything
            //also remove it from the QS
            //When no other options are chosen, then Set Globals to No Choice On

            if (StickyParameter.inQS.Contains(GlobalValues.NoChce.Name)
                && GlobalValues.NoChce.Key == NoChceKeys.On)
            {
                UserValues.SPEND.Key = GlobalValues.SPEND.Key = SPENDKeys.Off;
                UserValues.SIZE.Key = GlobalValues.SIZE.Key = SIZEKeys.Off;
                UserValues.ECON.Key = GlobalValues.ECON.Key = ECONKeys.Off;
                UserValues.DISABILITY.Key = GlobalValues.DISABILITY.Key = DISABILITYKeys.Off;
                UserValues.LEP.Key = GlobalValues.LEP.Key = LEPKeys.Off;

                StickyParameter.inQS.Remove(GlobalValues.NoChce.Name);
            }

            if (!
                (GlobalValues.SPEND.Key == SPENDKeys.On
                || GlobalValues.SIZE.Key == SIZEKeys.On
                || GlobalValues.ECON.Key == ECONKeys.On
                || GlobalValues.DISABILITY.Key == DISABILITYKeys.On
                || GlobalValues.LEP.Key == LEPKeys.On)
                )
            {
                GlobalValues.NoChce.Key = NoChceKeys.On;
            }

            base.OnInitComplete(e);

            if (GlobalValues.Grade.Key == GradeKeys.Combined_PreK_12)
                DataGrid.Columns.FieldsChanged += new EventHandler(StateTestsScatterplot.renameEnrolledColumn_FieldsChanged);

            nlrCompareTo.ShowSimilarSchoolsLink = true;
            nlrCompareTo.LinkRow.LinkControlAdded += new LinkControlAddedHandler(SetURLFile_LinkControlAdded);

        }
        void disableSubjects_LinkControlAdded(HyperLinkPlus link)
        {
            if (link.ID == "linkSubjectIDAllTested") link.Enabled = false;
        }
        void SetURLFile_LinkControlAdded(HyperLinkPlus link)
        {
            if (link.ID != "linkCompareToSimilar") link.UrlFile = GraphFileKeys.StateTests;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSetTitle = GetTitleWithoutGroupForSchoolTypeUnsupported(
                    ((GlobalValues.WOW.Key == WOWKeys.WSASCombined) ?
                        WOWKeys.WSASCombined : WOWKeys.WKCE)
                        + " - " + (
                        (GlobalValues.Grade.Key == GradeKeys.Combined_PreK_12
                        || GlobalValues.Grade.Key == GradeKeys.AllDisAgg)
                        ? String.Empty
                        : "Grade "
                        ) + GlobalValues.Grade.Key
                        + " - " + GlobalValues.SubjectID.Key 
                    )
                ;

            DataSetTitle = DataSetTitle.Replace(
                    GlobalValues.GetOrgName(),
                    "Top Five " + ((GlobalValues.OrgLevel.Key == OrgLevelKeys.School) ? "Schools" : "Districts") 
                    + " in "
                    + ((GlobalValues.LF.Key == LFKeys.State) ? "Entire State" :
                        (GlobalValues.LF.Key == LFKeys.CESA) ? "CESA " + GlobalValues.Agency.CESA.Trim() :
                        GlobalValues.Agency.CountyName.Trim()
                        )
                    + " Similar to " + WebSupportingClasses.TitleBuilder.newline + GlobalValues.GetOrgName()
                    +WebSupportingClasses.TitleBuilder.newline 
                    + " Sorted by % " + GlobalValues.Level.Key
                    );

            if (GlobalValues.FAYCode.Key == FAYCodeKeys.FAY)
                DataSetTitle = DataSetTitle.Replace(GlobalValues.GetOrgName(), GlobalValues.GetOrgName().Trim() + "  FAY ");

            DataSetTitle =
                DataSetTitle.Replace(TitleBuilder.GetYearRangeInTitle(QueryMarshaller.years),
                     "November " + (GlobalValues.Year - 1).ToString() + " Data");

            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.School)
            {
                DataSetTitle = DataSetTitle.Replace(TitleBuilder.GetSchoolTypeInTitle(GlobalValues.STYP), String.Empty);
            }

            DataSetTitle = DataSetTitle.Replace(TitleBuilder.GetCompareToInTitle(GlobalValues.OrgLevel, GlobalValues.CompareTo, GlobalValues.STYP, GlobalValues.S4orALL, WebSupportingClasses.TitleBuilder.GetRegionString(GlobalValues)), String.Empty);

            SimilarDataGrid.AddSuperHeader(DataSetTitle);
            SimilarDataGrid.AddSuperHeader(GetSecondSuperHeader());

            SimilarDataGrid.OrderBy = String.Empty; //disable default order by and let SQL take over

            SetUpChart();
            barChart.OrderBy = String.Empty; //disable default order by and let SQL take over
            if (GlobalValues.Sim.Key != SimKeys.Default) barChart.Visible = false;

            set_state();
            setBottomLink();

            nlrLevel.LinkControlAdded += new LinkControlAddedHandler(renameLinks_LinkControlAdded);
        }

        void renameLinks_LinkControlAdded(HyperLinkPlus link)
        {
            link.Text = "% " + link.Text;
        }
        private void SetUpChart()
        {
            barChart.Title = DataSetTitle;
            barChart.AxisY.LabelsFormat.CustomFormat = "0\\%";

            barChart.Height = new Unit(barChart.Height.Value + 100);;

            barChart.Type = SligoCS.Web.WI.WebUserControls.GraphBarChart.StackedType.Stacked100;

            barChart.AxisXDescription = SimilarDefinitionDescription();

            barChart.AxisYDescription = "Percent of Students Enrolled FAY";

            barChart.SeriesColors = GraphHorizBarChart.GetWSASStackedBarSeriesColors();

           //barChart.LabelColumnName = WebSupportingClasses.ColumnPicker.GetViewByColumnName(GlobalValues);
            barChart.LabelColumnName = "Name";

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
                graphColumns.Add(v_WSAS.No_WSAS);
            }

            if (GlobalValues.WOW.Key == WOWKeys.WSASCombined)
            {
                barChart.OverrideSeriesLabels.Add("AdvancedWSAS", "Advanced WSAS");
                barChart.OverrideSeriesLabels.Add("ProficientWSAS", "Proficient WSAS");
                barChart.OverrideSeriesLabels.Add("BasicWSAS", "Basic WSAS");
                barChart.OverrideSeriesLabels.Add("MinPerfWSAS", "MinPerf WSAS");
                barChart.OverrideSeriesLabels.Add("NoWSAS", "No WSAS");
            }
            else
            {
                barChart.OverrideSeriesLabels.Add("Percent Advanced", "Advanced WKCE");
                barChart.OverrideSeriesLabels.Add("Percent Proficient", "Proficient WKCE");
                barChart.OverrideSeriesLabels.Add("Percent Basic", "Basic WKCE");
                barChart.OverrideSeriesLabels.Add("Percent Minimal", "MinPerf WKCE");
                barChart.OverrideSeriesLabels.Add("Percent Pre-Req Skill", "WAA-SwD");
                barChart.OverrideSeriesLabels.Add("Percent Pre-Req Eng", "WAA-ELL");
                barChart.OverrideSeriesLabels.Add("No WSAS Total", "No WSAS");
            }
             
        }
        public String SimilarDefinitionDescription()
        {
            System.Text.StringBuilder definition = new System.Text.StringBuilder( "Criteria Selected for Defining Similar**:");
             
            /*if (GlobalValues.SPEND.Key == SPENDKeys.On) definition.Append("\n" + RelKeys.DistrictSpending + " <= 1000000");
            if (GlobalValues.SIZE.Key == SIZEKeys.On) definition.Append("\n" + RelKeys.DistrictSize + " <= 1000000");
            if (GlobalValues.ECON.Key == ECONKeys.On) definition.Append("\n" + RelKeys.EconDisadvantaged + " <= 1600");
            if (GlobalValues.DISABILITY.Key == DISABILITYKeys.On) definition.Append("\n" + RelKeys.Disabilities + " <= 1600");
            if (GlobalValues.LEP.Key == LEPKeys.On) definition.Append("\n" + RelKeys.LEP + " <= 1600");
            */
            DALWSASSimilarSchools dal = (DALWSASSimilarSchools)Database;
            dal.Ranges = dal.InitSimilarRanges(QueryMarshaller);
            String gtFrmt = "\n {0} >= {1}%";
            String whlFrmt = "\n {0} >= {1}";

            if (dal.Ranges.Exists(GlobalValues.SPEND.Name))
            {
                SimilarRange range = dal.Ranges.Find(GlobalValues.SPEND.Name);
                String frmt =
                    (decimal.Parse(range.Max) > 11500)
                ? "\n {0} <= {1}"
                : "\n {0} <= Any";

                definition.Append(String.Format(frmt, new string[] { RelKeys.DistrictSpending, range.Max }));
            }

            if (dal.Ranges.Exists(GlobalValues.SIZE.Name))
                definition.Append(String.Format(whlFrmt, new string[] { RelKeys.DistrictSize, dal.Ranges.Find(GlobalValues.SIZE.Name).Min }));

            if (dal.Ranges.Exists(GlobalValues.ECON.Name))
                definition.Append(String.Format(gtFrmt, new string[] { RelKeys.EconDisadvantaged, dal.Ranges.Find(GlobalValues.ECON.Name).Min }
            ));

            if (dal.Ranges.Exists(GlobalValues.DISABILITY.Name))
                definition.Append(String.Format(gtFrmt, new string[] { RelKeys.Disabilities, dal.Ranges.Find(GlobalValues.DISABILITY.Name).Min }
            ));

            if (dal.Ranges.Exists(GlobalValues.LEP.Name))
                definition.Append(String.Format(gtFrmt, new string[] { RelKeys.LEP, dal.Ranges.Find(GlobalValues.LEP.Name).Min }
            ));


            if (GlobalValues.NoChce.Key == NoChceKeys.On) definition.Append("\n" + "No Criteria Chosen");

            return definition.ToString();
        }
        public override List<string> GetVisibleColumns(Group viewBy, OrgLevel orgLevel, CompareTo compareTo, STYP schoolType)
        {
           List<String> cols = base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);

           cols.Add(v_WSASDemographics.LinkedName);
            cols.Add(v_WSASDemographics.Enrolled);

            if (GlobalValues.SIZE.Key == SIZEKeys.On)
                cols.Add(v_WSASDemographics.District_Size);

            if (GlobalValues.SPEND.Key == SPENDKeys.On)
                cols.Add(v_WSASDemographics.Cost_Per_Member);

            if (GlobalValues.ECON.Key == ECONKeys.On)
                cols.Add(v_WSASDemographics.PctEcon);

            if (GlobalValues.LEP.Key == LEPKeys.On)
                cols.Add(v_WSASDemographics.PctLEP);

            if (GlobalValues.DISABILITY.Key == DISABILITYKeys.On)
                cols.Add(v_WSASDemographics.PctDisabled);

           if (GlobalValues.Level.Key == LevelKeys.Advanced)
            {
                if (GlobalValues.WOW.Key == WOWKeys.WSASCombined)
                    cols.Add(v_WSASDemographics.AdvancedWSAS);
                else
                    cols.Add(v_WSASDemographics.Percent_Advanced);
            }
            else if (GlobalValues.Level.Key == LevelKeys.AdvancedProficient)
            {
                if (GlobalValues.WOW.Key == WOWKeys.WSASCombined)
                    cols.Add(v_WSASDemographics.AdvancedPlusProficientTotalWSAS);
                else
                    cols.Add(v_WSASDemographics.PCTAdvPlusPCTPrf);
            }

            return cols;
        }
        private TableRow GetSecondSuperHeader()
        {
            TableRow tr = new TableRow();
            TQRelateTo relateTo = GlobalValues.TQRelateTo;

            AddTableCell(tr, "&nbsp;", 1); //first label column is always blank

            string wkcewsas = GlobalValues.WOW.Key;
            string grade = ((GlobalValues.Grade.Key != GradeKeys.Combined_PreK_12 && GlobalValues.Grade.Key != GradeKeys.AllDisAgg)
                ? "Grade " : String.Empty)
            + GlobalValues.Grade.Key;

            int fayColSpan = 2;
             //   (GlobalValues.Level.Key == LevelKeys.WAA_ELL || GlobalValues.Level.Key == LevelKeys.WAA_SwD)
              //  ? 6 : 2;

            if (GlobalValues.SIZE.Key == SIZEKeys.Off
                && GlobalValues.SPEND.Key == SPENDKeys.Off)
            {
                fayColSpan++;
            }

            if (GlobalValues.SIZE.Key == SIZEKeys.On) 
            {
                AddTableCell(tr, RelKeys.DistrictSize, 1);
            }
            if (GlobalValues.SPEND.Key == SPENDKeys.On)
            {
                AddTableCell(tr, RelKeys.DistrictSpending, 1);
            }

            if (GlobalValues.ECON.Key == ECONKeys.On)
                fayColSpan++;

            if (GlobalValues.LEP.Key == LEPKeys.On)
                fayColSpan++;

            if (GlobalValues.DISABILITY.Key == DISABILITYKeys.On)
                fayColSpan++;

            AddTableCell(tr, wkcewsas + " -- All Students Enrolled FAY -- " + grade, fayColSpan);

            foreach (TableCell cell in tr.Cells)
            {
                cell.BorderColor = System.Drawing.Color.Gray;
                cell.BorderWidth = 4;
            }

            return tr;
        }
        private void AddTableCell(TableRow tr, string text, int colSpan)
        {
            TableCell cell = new TableCell();
            cell.Text = text;
            cell.ColumnSpan = colSpan;
            cell.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(cell);
        }
        protected override List<string> GetDownloadRawVisibleColumns()
        {
            List<string> cols = base.GetDownloadRawVisibleColumns();
            int index;

            index = cols.IndexOf(v_WSASDemographics.Enrolled);
            cols.Insert(index, v_WSASDemographics.SubjectLabel);

            if (GlobalValues.Level.Key == LevelKeys.Advanced)
            {
                if (GlobalValues.WOW.Key == WOWKeys.WSASCombined)
                {
                    index = cols.IndexOf(v_WSASDemographics.AdvancedWSAS);
                    cols.Insert(index, v_WSASDemographics.Number_AdvancedWSAS);
                }
                else
                {
                    index = cols.IndexOf(v_WSASDemographics.Percent_Advanced);
                    cols.Insert(index, v_WSASDemographics.Number_Advanced);
                }

            }
            else if (GlobalValues.Level.Key == LevelKeys.AdvancedProficient)
            {
                if (GlobalValues.WOW.Key == WOWKeys.WSASCombined)
                {
                    index = cols.IndexOf(v_WSASDemographics.AdvancedPlusProficientTotalWSAS);
                    cols.Insert(index, v_WSASDemographics.Number_AdvancedPlusProficientTotalWSAS);
                }
                else
                {
                    index = cols.IndexOf(v_WSASDemographics.PCTAdvPlusPCTPrf);
                    cols.Insert(index, v_WSASDemographics.Number_AdvPlusPCTPrf);
                }
            }

            if (GlobalValues.ECON.Key == ECONKeys.On)
            {
                index = cols.IndexOf(v_WSASDemographics.PctEcon);
                cols.Insert(index, v_WSASDemographics.NumEcon);
            }
            if (GlobalValues.LEP.Key == LEPKeys.On)
            {
                index = cols.IndexOf(v_WSASDemographics.PctLEP);
                cols.Insert(index, v_WSASDemographics.NumLEP);
            }
            if (GlobalValues.DISABILITY.Key == DISABILITYKeys.On)
            {
                index = cols.IndexOf(v_WSASDemographics.PctDisabled);
                cols.Insert(index, v_WSASDemographics.NumDisabled);
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
            newLabels.Remove(v_WSASDemographics.Enrolled);
            newLabels.Add(v_WSASDemographics.Enrolled, "enrolled_tested_grades_combined");
            newLabels.Remove(v_WSASDemographics.GradeLabel);
            newLabels.Add(v_WSASDemographics.GradeLabel, "tested_grades_combined");
            newLabels.Add(v_WSASDemographics.District_Size, "district_size_total_fall_enrollment_prek-12");
            newLabels.Add(v_WSASDemographics.Cost_Per_Member, "district_ current_education_cost_per_member");
            newLabels.Add(v_WSASDemographics.AdvancedWSAS, "percent_advanced_wsas");
            newLabels.Add(v_WSASDemographics.Number_AdvancedWSAS, "number_advanced_wsas");
            newLabels.Add(v_WSASDemographics.Percent_Advanced, "percent_advanced_wkce");
            newLabels.Add(v_WSASDemographics.Number_Advanced, "number_advanced_wkce");
            newLabels.Add(v_WSASDemographics.AdvancedPlusProficientTotalWSAS, "percent_advanced_plus_proficient_wsas");
            newLabels.Add(v_WSASDemographics.Number_AdvancedPlusProficientTotalWSAS, "number_advanced_plus_proficient_wsas");
            newLabels.Add(v_WSASDemographics.PCTAdvPlusPCTPrf, "percent_advanced_plus_proficient_wkce");
            newLabels.Add(v_WSASDemographics.Number_AdvPlusPCTPrf, "number_advanced_plus_proficient_wkce");
            newLabels.Add(v_WSASDemographics.BasicPlusMinPerfPlusNoWSASTotalWSAS, "percent_basic_plus_minperf_plus_nowsas_wsas");
            newLabels.Add(v_WSASDemographics.Number_BasicPlusMinPerfPlusNoWSASTotalWSAS, "number_basic_plus_minperf_plus_nowsas_wsas");
            newLabels.Add(v_WSASDemographics.BasicPlusMinPerfPlusPreReqSkillsEngPlusNoWSASTotal, "percent_basic_plus_minperf_plus_waa_swd_ell_plus_no_wsas_wkce");
            newLabels.Add(v_WSASDemographics.Number_BasicPlusMinPerfPlusPreReqSkillsEngPlusNoWSASTotal, "number_basic_plus_minperf_plus_waa_swd_ell_plus_no_wsas_wkce");
            newLabels.Add(v_WSASDemographics.PctTotalWAALep, "percent_waa_ell_total_wkce");
            newLabels.Add(v_WSASDemographics.Number_TotalWAALep, "number_waa_ell_total_wkce");
            newLabels.Add(v_WSASDemographics.ExcusedByParent, "percent_excused_by_parent");
            newLabels.Add(v_WSASDemographics.Number_ExcusedByParent, "number_excused_by_parent");
            newLabels.Add(v_WSASDemographics.EligibleButNotTested, "percent_eligible_not_tested");
            newLabels.Add(v_WSASDemographics.Number_EligibleButNotTested, "number_eligible_not_tested");
            newLabels.Add(v_WSASDemographics.No_WSAS_Total, "percent_no_wsas_wkce");
            newLabels.Add(v_WSASDemographics.Number_No_WSAS_Total, "number_no_wsas_wkce");
            newLabels.Add(v_WSASDemographics.PctEcon, "percent_economically_disadvantaged");
            newLabels.Add(v_WSASDemographics.NumEcon, "number_economically_disadvantaged");
            newLabels.Add(v_WSASDemographics.PctLEP, "percent_limited_english_proficient");
            newLabels.Add(v_WSASDemographics.NumLEP, "number_limited_english_proficient");
            newLabels.Add(v_WSASDemographics.PctDisabled, "percent_students_with_disabilities");
            newLabels.Add(v_WSASDemographics.NumDisabled, "number_students_with_disabilities");
            newLabels.Add(v_WSASDemographics.PctAmInd, "percent_amerind");
            newLabels.Add(v_WSASDemographics.NumAmInd, "number_amerind");
            newLabels.Add(v_WSASDemographics.PctAsian, "percent_asian");
            newLabels.Add(v_WSASDemographics.NumAsian, "number_asian");
            newLabels.Add(v_WSASDemographics.PctBlack, "percent_black");
            newLabels.Add(v_WSASDemographics.NumBlack, "number_black");
            newLabels.Add(v_WSASDemographics.PctHisp, "percent_hispanic");
            newLabels.Add(v_WSASDemographics.NumHisp, "number_hispanic");
            newLabels.Add(v_WSASDemographics.PctWhite, "percent_white");
            newLabels.Add(v_WSASDemographics.NumWhite, "number_white");
            newLabels.Add(v_WSASDemographics.Percent_PreReq_Skill_Level_1, "percent_waa_swd_minimal");
            newLabels.Add(v_WSASDemographics.Number_PreReq_Skill_Level_1, "number_waa_swd_minimal");
            newLabels.Add(v_WSASDemographics.Percent_PreReq_Skill_Level_2, "percent_waa_swd_basic");
            newLabels.Add(v_WSASDemographics.Number_PreReq_Skill_Level_2, "number_waa_swd_basic");
            newLabels.Add(v_WSASDemographics.Percent_PreReq_Skill_Level_3, "percent_waa_swd_proficient");
            newLabels.Add(v_WSASDemographics.Number_PreReq_Skill_Level_3, "number_waa_swd_proficient");
            newLabels.Add(v_WSASDemographics.Percent_PreReq_Skill_Level_4, "percent_waa_swd_advanced");
            newLabels.Add(v_WSASDemographics.Number_PreReq_Skill_Level_4, "number_waa_swd_advanced");
            newLabels.Add(v_WSASDemographics.Percent_PreReq_Eng_Basic, "percent_waa_ell_basic");
            newLabels.Add(v_WSASDemographics.Percent_PreReq_Eng_Minimal, "percent_waa_ell_minimal");
            newLabels.Add(v_WSASDemographics.Percent_PreReq_Eng_Proficient, "percent_waa_ell_proficient");
            newLabels.Add(v_WSASDemographics.PctTotalWAADisabil, "percent_waa_swd_total_wkce");
            newLabels.Add(v_WSASDemographics.Number_TotalWAADisabil, "number_waa_swd_total_wkce");
            return newLabels;
        }
        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                (WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                (WI.displayed_obj.dataLinksPanel, true);
        }
        private void setBottomLink()
        {
            BottomLinkViewProfile.DistrictCd = GlobalValues.DistrictCode;
        }
    }
}
