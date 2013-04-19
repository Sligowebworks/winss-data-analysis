using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SligoCS.BL.WI;
using SligoCS.DAL.WI;
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.WI.WebUserControls;
using SligoCS.Web.WI.WebSupportingClasses;
using SligoCS.Web.WI.WebSupportingClasses.WI;
using SligoCS.Web.Base.WebServerControls.WI;

using ChartFX.WebForms;

namespace SligoCS.Web.WI
{
    public partial class StateTestsPerformance : PageBaseWI
    {
        private List<String> GradeCodesActive = new List<String>();
        private Dictionary<String, Boolean> disaggFlags = new Dictionary<string, bool>();

        protected override DALWIBase InitDatabase()
        {
            return new DALWSAS();
        }
        protected override GridView InitDataGrid()
        {
            return StateTestsDataGrid;
        }
        protected override Chart InitGraph()
        {
            return barChart;
        }
        protected override SligoCS.Web.WI.WebUserControls.NavViewByGroup InitNavRowGroups()
        {
            nlrVwByGroup.LinksEnabled = SligoCS.Web.WI.WebUserControls.NavViewByGroup.EnableLinksVector.Migrant;
            nlrVwByGroup.LinkRow.LinkControlAdded += new LinkControlAddedHandler(disableGroupGrade_LinkControlAdded);
            return nlrVwByGroup;
        }
        void disableGroupGrade_LinkControlAdded(HyperLinkPlus link)
        {
            if (link.ID == "linkGroupGrade") link.Enabled = false;
        }
        protected override string SetPageHeading()
        {
            return "How did students perform on state test at grades 3-8 and 10?";
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            OnCheckPrerequisites += new CheckPrerequisitesHandler(RedirectToSimilar_OnCheckPrerequisites);
        }
        void RedirectToSimilar_OnCheckPrerequisites(PageBaseWI page, EventArgs args)
        {
            if (GlobalValues.CompareTo.Key == CompareToKeys.SimSchools)
                OnRedirectUser += new RedirectUserHandler(RedirectToSimilar_OnRedirectUser);
        }
        void RedirectToSimilar_OnRedirectUser()
        {
            String qs = UserValues.GetBaseQueryString();
            string NavigateUrl = GlobalValues.CreateURL("~/" + GraphFileKeys.StateTestsSimilar, qs);
            Response.Redirect(NavigateUrl, true);
        }
        protected override void OnInitComplete(EventArgs e)
        {
            GlobalValues.TrendStartYear = (GlobalValues.WOW.Key == WOWKeys.WKCE) ? 1997 : 2003;
            GlobalValues.CurrentYear = 2013;
           
            QueryMarshaller.RaceDisagCodes.Add((int)QueryMarshaller.RaceCodes.RaceEth_NA);

            //Disable View By Grade:
            if (GlobalValues.Group.Key == GroupKeys.Grade) GlobalValues.Group.Key = GroupKeys.All;

            //SchoolType Unsupported.
            GlobalValues.OverrideSchoolTypeWhenOrgLevelIsSchool_Complete += PageBaseWI.DisableSchoolType;

            if (GlobalValues.OrgLevel.Key != OrgLevelKeys.State || GlobalValues.SuperDownload.Key == SupDwnldKeys.True)
                GlobalValues.FAYCode.Key = FAYCodeKeys.FAY;
            else
                GlobalValues.FAYCode.Key = FAYCodeKeys.NonFAY;

            // Overrides are ordered by precedence, be careful!!

            //If this is the first time into the page, and CompareTo hasn't been selected on a previous page, then override the default to Current:
            object objCompare = GlobalValues.CompareTo.GetParamFromUser(GlobalValues.CompareTo.Name);
            if (objCompare == null) GlobalValues.CompareTo.Key = CompareToKeys.Current;

            //*** Level
            //Override WAA Levels based on WKCE or WSAS:
            if (GlobalValues.WOW.Key == WOWKeys.WSASCombined
                && (GlobalValues.Level.Key == LevelKeys.WAA_ELL || GlobalValues.Level.Key == LevelKeys.WAA_SwD)
                )
                GlobalValues.Level.Key = LevelKeys.NoWSAS;

            nlrLevel.LinkControlAdded += new LinkControlAddedHandler(WKCEorWSASLevels_LinkControlAdded);
            GlobalValues.OverrideByNavLinksNotPresent(GlobalValues.Level, nlrLevel, LevelKeys.AdvancedProficient);

            //***Subjects
            GlobalValues.OverrideByNavLinksNotPresent(GlobalValues.SubjectID, nlrSubject, SubjectIDKeys.Reading);
            
            //***Grades
            QueryMarshaller.gradeCodes.ObeyForceDisAgg = true;
            GlobalValues.OverrideByNavLinksNotPresent(GlobalValues.Grade, nlrGrade, GlobalValues.Grade.Value);
            GradeCodesActive = getGradeCodeRange();
            if (GlobalValues.SuperDownload.Key == SupDwnldKeys.True)
            {//necessary when Combined grades is selected because gives wrong floor.
                GlobalValues.Grade.Key = GradeKeys.AllDisAgg;
            }
            else if (!GradeCodesActive.Contains(GlobalValues.Grade.Value))
            {
                if (GradeCodesActive[0] == GlobalValues.Grade.Range[GradeKeys.Grade_3]
                    && GradeCodesActive.Contains(GlobalValues.Grade.Range[GradeKeys.Grade_4]))
                {// Don't use Grade 3 for the default
                    GlobalValues.Grade.Key = GradeKeys.Grade_4;
                }
                else
                {
                    GlobalValues.Grade.Value = GradeCodesActive[0];  //default to minimum grade
                }
            }
            nlrGrade.LinkControlAdded += new LinkControlAddedHandler(disableGradeLinks_LinkControlAdded);

            QueryMarshaller.SexDisagCodes.Add((int)SexCodes.Missing);

            base.OnInitComplete(e);

            if (GlobalValues.Grade.Key == GradeKeys.AllDisAgg
                || GlobalValues.Grade.Key == GradeKeys.Combined_PreK_12)
            { QueryMarshaller.gradeCodes.ObeyForceDisAgg = true; }

            DataGrid.Columns.FieldsChanged += new EventHandler(Columns_FieldsChanged);

            nlrCompareTo.ShowSimilarSchoolsLink = true;

            nlrLevel.LinkControlAdded += new LinkControlAddedHandler(renameLevelLabels_LinkControlAdded);

            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.District)
                QueryMarshaller.RaceDisagCodes.Add((int)QueryMarshaller.RaceCodes.Comb);

            // Give other overrides a chance to have effect before overriding Year
            if (GlobalValues.CompareTo.Key == CompareToKeys.Years)
            { //due to changes is cut scores, years are not comparable.
                if (GlobalValues.Year > 2012)
                {
                    GlobalValues.CurrentYear = 2012;
                }
            }
        }

        void renameLevelLabels_LinkControlAdded(HyperLinkPlus link)
        {
            if (link.ID != "linkLevelBasicMinSkillEng") return;

            if (GlobalValues.WOW.Key == WOWKeys.WSASCombined)
                link.Text = "Basic + Min Perf +No WSAS";
        }

        void Columns_FieldsChanged(object sender, EventArgs e)
        {
            DataControlFieldCollection columns = (DataControlFieldCollection)sender;

            foreach (DataControlField field in columns)
            {
                if (GlobalValues.Grade.Key == GradeKeys.Combined_PreK_12
                    && field.HeaderText == "Enrolled at Test Time")
                    field.HeaderText = "Enrolled in Tested Grade(s) at Test Time";
            }
        }
        void WKCEorWSASLevels_LinkControlAdded(HyperLinkPlus link)
        {
            if (GlobalValues.WOW.Key == WOWKeys.WSASCombined
                && (link.ID == "linkLevelWAA_SwD" || link.ID == "linkLevelWAA_ELL")
                ) link.Enabled = false;
        }
        void disableGradeLinks_LinkControlAdded(HyperLinkPlus link)
        {
            if (!GradeCodesActive.Contains(link.ParamValue)) link.Enabled = false;
        }
        public List<String> getGradeCodeRange()
        {
            List<String> codes = new List<String>();
            QueryMarshaller qm = new QueryMarshaller(GlobalValues);

            //if (GlobalValues.CompareTo.Key == CompareToKeys.Current
            //    && GlobalValues.Group.Key == GroupKeys.All
            //    && GlobalValues.SubjectID.Key != SubjectIDKeys.AllTested)
                codes.Add(GlobalValues.Grade.Range[GradeKeys.AllDisAgg]);

            GlobalValues.SQL = DALWSAS.WSASGrades(qm);
            qm.AssignQuery(new DALWSAS(), GlobalValues.SQL);
            DataSet result = qm.Database.DataSet.Copy();

            if (result == null || result.Tables[0].Rows.Count == 0) return codes;
            
            foreach (DataRow row in result.Tables[0].Rows)
            {
                codes.Add(row[0].ToString());
            }

            //unsupported grade by Subject:
            string[] unsupported = 
            {
                GlobalValues.Grade.Range[GradeKeys.Grade_3], 
                GlobalValues.Grade.Range[GradeKeys.Grade_5], 
                GlobalValues.Grade.Range[GradeKeys.Grade_6],
                GlobalValues.Grade.Range[GradeKeys.Grade_7]
            };
            if (GlobalValues.SubjectID.Key == SubjectIDKeys.Language
                || GlobalValues.SubjectID.Key == SubjectIDKeys.Science
                || GlobalValues.SubjectID.Key == SubjectIDKeys.SocialStudies)
            {
                foreach (String gr in unsupported)
                {
                    if (codes.Contains(gr)) codes.Remove(gr);
                }
            }
            //throw new Exception(String.Join(",", codes.ToArray()));
            return codes;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (GlobalValues.CompareTo.Key == CompareToKeys.Years)
            {
                    pnlNotComparable.Visible = true;
            }
            

            DataSetTitle = GetTitleForSchoolTypeUnsupported(
                    ((GlobalValues.WOW.Key == WOWKeys.WSASCombined) ?
                        "WSAS" : "WKCE") + " - " +
                        ((GlobalValues.Grade.Key != GradeKeys.AllDisAgg 
                        && GlobalValues.Grade.Key != GradeKeys.Combined_PreK_12) ?
                        "Grade " : String.Empty) + GlobalValues.Grade.Key +
                        " - " + GlobalValues.SubjectID.Key + WebSupportingClasses.TitleBuilder.newline +
                        GlobalValues.Level.Key+ WebSupportingClasses.TitleBuilder.newline +
                        ((GlobalValues.WOW.Key == WOWKeys.WSASCombined) ?
                        WOWKeys.WSASCombined.Replace("WSAS: ", String.Empty) : WOWKeys.WKCE)
                        ) 
                ;

            if (GlobalValues.FAYCode.Key == FAYCodeKeys.FAY)
                DataSetTitle = DataSetTitle.Replace(GlobalValues.GetOrgName(), GlobalValues.GetOrgName() + " FAY");

            if (GlobalValues.WOW.Key == WOWKeys.WSASCombined
                && GlobalValues.Level.Key == LevelKeys.BasicMinSkillEng)
                DataSetTitle = DataSetTitle.Replace("+ WAA SwD/ELL ", String.Empty);
            
            //if (GlobalValues.CompareTo.Key != CompareToKeys.Years)
                DataSetTitle = 
                    DataSetTitle.Replace(TitleBuilder.GetYearRangeInTitle(QueryMarshaller.years),
                         "November " + (GlobalValues.Year -1).ToString() + " Data")
                ;

            StateTestsDataGrid.AddSuperHeader(DataSetTitle);

            List<String> order = new List<string>();
            StateTestsDataGrid.OrderBy = barChart.OrderBy = GetCustomOrderBy(order, DataSet.Tables[0].Columns);

            ///*GRAPH*:

            disaggFlags = SetIsDisaggFlags(GlobalValues); //used by CountDimensions() and SetGraphSeriesAndAxisLabelColumns_()
            
            if (CountDimensions(disaggFlags) > 2)
               HideGraphForDisAggOverflow();
            else
                SetUpGraph();

            set_state();
            setBottomLink();
        }
        public override void DataBindGraph(Chart graph, DataSet ds)
        {
            base.DataBindGraph(graph, ds);

            ///Create Patterned Bars to delimit different data provider before 2003
            if (GlobalValues.CompareTo.Key == CompareToKeys.Years
                            && GlobalValues.WOW.Key == WOWKeys.WKCE)
            {
                //get years up to the limit
                DataRow[] rows = ds.Tables[0].Select("year < 2003");
                int c = ds.Tables[0].Columns.IndexOf("year");
                List<String> years = new List<string>();
                for (int r = 0; r < rows.Length; r++)
                {
                    //get distinct years
                    if (r == 0
                        || rows[r][c].ToString() != rows[r - 1][c].ToString())
                        years.Add(rows[r][c].ToString());
                }
                years.Sort();
                //for each unique year, turn on patterned bars

                for (int i = 0; i < years.Count && i < barChart.Series.Count; i++)
                {
                    SeriesAttributes series = barChart.Series[i];
                    if (true)
                    {
                        series.Pattern = System.Drawing.Drawing2D.HatchStyle.Weave;
                        series.FillMode = FillMode.Pattern;
                        series.AlternateColor = System.Drawing.Color.White;
                    }
                }
            }
        }
        void SetUpGraph()
        {
            if (disaggFlags[GlobalValues.Level.Name])
                SetupStackedBarChart();
            else
                SetUpChart();
        }
        private void SetUpChart()
        {
            barChart.Height = (int)barChart.Height.Value + 100;
            //pin at 100%
            barChart.AxisY.Max = 100;
            barChart.Title = DataSetTitle;
            barChart.AxisY.LabelsFormat.CustomFormat = "0\\%";

            SetGraphSeriesAndAxisLabelColumns_SingleBar();

            barChart.AxisYDescription = "Percent of Students Enrolled" + ((GlobalValues.OrgLevel.Key != OrgLevelKeys.State)?" FAY":String.Empty);

            if (GlobalValues.Level.Key == LevelKeys.Advanced)
            {
                barChart.DisplayColumnName =
                    (GlobalValues.WOW.Key == WOWKeys.WSASCombined)
                    ? v_WSAS.AdvancedWSAS : v_WSAS.Percent_Advanced;
            }
            else if (GlobalValues.Level.Key == LevelKeys.AdvancedProficient)
            {
                barChart.DisplayColumnName =
                    (GlobalValues.WOW.Key == WOWKeys.WSASCombined)
                    ? v_WSAS.AdvancedPlusProficientTotalWSAS : v_WSAS.PCTAdvPlusPCTPrf;
            }
            else if (GlobalValues.Level.Key == LevelKeys.BasicMinSkillEng)
            {
                barChart.DisplayColumnName =
                    (GlobalValues.WOW.Key == WOWKeys.WSASCombined)
                    ? v_WSAS.BasicPlusMinPerfPlusNoWSASTotalWSAS : v_WSAS.BasicPlusMinPerfPlusPreReqSkillsEngPlusNoWSASTotal;
            }
            else if (GlobalValues.Level.Key == LevelKeys.WAA_SwD)
                barChart.DisplayColumnName = v_WSAS.PctTotalWAADisabil;
            else if (GlobalValues.Level.Key == LevelKeys.WAA_ELL)
                barChart.DisplayColumnName = v_WSAS.PctTotalWAALep;
            else if (GlobalValues.Level.Key == LevelKeys.NoWSAS)
                barChart.DisplayColumnName = v_WSAS.No_WSAS_Total;

        }
        private void SetupStackedBarChart()
        {
            barChart.Title = DataSetTitle;
            barChart.AxisY.LabelsFormat.CustomFormat = "0\\%";

            barChart.Type = SligoCS.Web.WI.WebUserControls.GraphBarChart.StackedType.Stacked100;

            if (GlobalValues.Group.Key == GroupKeys.All)
            {
                barChart.OverrideAxisXLabels = new Hashtable();
                barChart.OverrideAxisXLabels.Add("Both Groups Combined", "All Students");
            }

            SetGraphSeriesAndAxisLabelColumns_StackedBar();

            barChart.AxisYDescription = "Percent of Students";

            barChart.SeriesColors = GraphHorizBarChart.GetWSASStackedBarSeriesColors();

            if (GlobalValues.Level.Key == LevelKeys.All) SetLevelSeriesLabelsOverrides(barChart);

            //Decide what columns to chart
            if (GlobalValues.Level.Key == LevelKeys.All) barChart.MeasureColumns = SetMeasuresForLevels(barChart);
            else barChart.DisplayColumnName = ChooseLevelColumn();

        }
        private void SetGraphSeriesAndAxisLabelColumns_StackedBar()
        {
           barChart.SeriesColumnName = v_WSAS.Level;

           if (disaggFlags[GlobalValues.Group.Name] || disaggFlags[GlobalValues.SubjectID.Name])
            {
                // if (groupIsDisagg && subjectIsDisagg) never happens in stacked bar

                if (disaggFlags[GlobalValues.Group.Name])
                {
                    barChart.LabelColumnName = ColumnPicker.GetViewByColumnName(GlobalValues);
                }
                else
                {
                    barChart.LabelColumnName = v_WSAS.SubjectLabel;
                }
            }
            else if (disaggFlags[GlobalValues.CompareTo.Name])
            {
                if (disaggFlags[GlobalValues.Level.Name])
                {
                    barChart.LabelColumnName = ColumnPicker.GetCompareToColumnName(GlobalValues);
                }
                else
                {
                    barChart.LabelColumnName = v_WSAS.GradeShortLabel;
                }
            }
            else
            {
                if (disaggFlags[GlobalValues.Grade.Name])
                {
                    barChart.LabelColumnName = v_WSAS.GradeShortLabel;
                }
                else
                {
                    barChart.FriendlyAxisXNames = new List<String>(new string[] { "All Students" });
                }
            } 
   
        }
        private void SetGraphSeriesAndAxisLabelColumns_SingleBar()
        {
            barChart.LegendBox.Visible = (CountDimensions(disaggFlags) > 1);

            if (disaggFlags[GlobalValues.CompareTo.Name] == false)
            {
                if (disaggFlags[GlobalValues.Grade.Name])
                {
                    if (!disaggFlags[GlobalValues.Group.Name] && !disaggFlags[GlobalValues.SubjectID.Name])
                        barChart.LabelColumnName = v_WSAS.GradeShortLabel;
                    else if (disaggFlags[GlobalValues.Group.Name]) // and if both IsDisagg
                        barChart.LabelColumnName = ColumnPicker.GetViewByColumnName(GlobalValues);
                    else // if only subject IsDisagg
                        barChart.LabelColumnName = v_WSAS.SubjectLabel;
                }
                else //if (gradeIsDisagg == false)
                {
                    if (disaggFlags[GlobalValues.Group.Name])
                    {
                        barChart.LabelColumnName = ColumnPicker.GetViewByColumnName(GlobalValues);
                    }
                    else if (disaggFlags[GlobalValues.SubjectID.Name])
                    {
                        barChart.LabelColumnName = v_WSAS.SubjectLabel;
                    }
                    else
                    {
                        barChart.FriendlyAxisXNames = new List<String>(new string[] { "All Students" });
                    }
                }
            }
            else //if (compareToIsDisagg)
            {
                if (!disaggFlags[GlobalValues.Group.Name] && !disaggFlags[GlobalValues.SubjectID.Name])
                {
                    if (!disaggFlags[GlobalValues.Group.Name] && !disaggFlags[GlobalValues.Grade.Name])
                    {
                        barChart.FriendlyAxisXNames = new List<String>(new string[] { "All Students" });
                    }
                    else if (!disaggFlags[GlobalValues.Grade.Name])
                    {
                        barChart.LabelColumnName = ColumnPicker.GetCompareToColumnName(GlobalValues);
                    }
                    else
                        barChart.LabelColumnName = v_WSAS.GradeShortLabel;
                }
                else if (disaggFlags[GlobalValues.Group.Name])
                {
                    barChart.LabelColumnName = ColumnPicker.GetViewByColumnName(GlobalValues);
                }
                else // if (subjectIsDisagg)
                {
                    barChart.LabelColumnName = v_WSAS.SubjectLabel;
                }
            }

            if (CountDimensions(disaggFlags) > 1)
            {
                if (disaggFlags[GlobalValues.CompareTo.Name])
                    barChart.SeriesColumnName = ColumnPicker.GetCompareToColumnName(GlobalValues);
                else
                {
                    if (disaggFlags[GlobalValues.Grade.Name])
                        barChart.SeriesColumnName = v_WSAS.GradeShortLabel;
                    else
                        barChart.SeriesColumnName = v_WSAS.SubjectLabel;
                }
            }
            else if (!disaggFlags[GlobalValues.Group.Name]) // All Students
            {
                barChart.SeriesColumnName = ColumnPicker.GetCompareToColumnName(GlobalValues);
                barChart.LegendBox.Visible = true; // not sure why this is necessary!! but not visible without it
            }

            if (barChart.SeriesColumnName == barChart.LabelColumnName)
                //Assumption is that Series column is defaulting to CompareTo and should not be used
            {
                barChart.SeriesColumnName = ColumnPicker.GetViewByColumnName(GlobalValues);
            }
        }
        String GetCustomOrderBy(List<String> order, DataColumnCollection columns)
        {
            QueryMarshaller.BuildCompareToOrderBy(order, columns);

            if (GlobalValues.Group.Key != GroupKeys.All
                &&
                (GlobalValues.CompareTo.Key == CompareToKeys.SelSchools)
                )
            {               
                order.Insert(0, v_WSAS.District_Name);//district, school
            }

            if (GlobalValues.Grade.Key == GradeKeys.AllDisAgg)
                order.Add( v_WSAS.GradeCode + " ASC");

            if (GlobalValues.SubjectID.Key == SubjectIDKeys.AllTested)
                order.Add(v_WSAS.SubjectID);

            if (GlobalValues.Group.Key != GroupKeys.All)
                QueryMarshaller.BuildOrderByViewByGroup(order, columns);

            return String.Join(",", order.ToArray()); 
        }
        public override List<string> GetVisibleColumns()
        {
            List<String> cols = base.GetVisibleColumns();

            if (GlobalValues.SuperDownload.Key == SupDwnldKeys.True)
                return cols; // return only generic (base) columns

            //debug GroupNum, faycode
            if ((GlobalValues.TraceLevels & TraceStateUtils.TraceLevels.sql) == TraceStateUtils.TraceLevels.sql)
            {
                cols.Add(v_WSAS.GroupNum);
                cols.Add(v_WSAS.FAYCode);
            }

            if (GlobalValues.Grade.Key == GradeKeys.AllDisAgg)
                cols.Add(v_WSAS.GradeLabel);

            if (GlobalValues.SubjectID.Key == SubjectIDKeys.AllTested)
                cols.Add(v_WSAS.SubjectLabel);

            cols.Add(v_WSAS.Enrolled);

            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.District
                && GlobalValues.Group.Key != GroupKeys.All
                || GlobalValues.CompareTo.Key == CompareToKeys.OrgLevel)
            {
                cols.Add(v_WSAS.Included);
            }

            if (GlobalValues.Group.Key == GroupKeys.Migrant)
                cols.Add(v_WSAS.MigrantLabel);

            if (GlobalValues.Level.Key == LevelKeys.All)
            {
                if (GlobalValues.WOW.Key == WOWKeys.WSASCombined)
                {
                    cols.Add(v_WSAS.No_WSAS_Total);
                    cols.Add(v_WSAS.MinPerfWSAS);
                    cols.Add(v_WSAS.BasicWSAS);
                    cols.Add(v_WSAS.ProficientWSAS);
                    cols.Add(v_WSAS.AdvancedWSAS);
                }
                else
                {
                    cols.Add(v_WSAS.No_WSAS_Total);
                    cols.Add(v_WSAS.Percent_PreReq_Skill);
                    cols.Add(v_WSAS.Percent_PreReq_Eng);
                    cols.Add(v_WSAS.Percent_Minimal);
                    cols.Add(v_WSAS.Percent_Basic);
                    cols.Add(v_WSAS.Percent_Proficient);
                    cols.Add(v_WSAS.Percent_Advanced);
                }
                //                    cols.Add(v_WSAS.PctTotalWAADisabil);
            }
            else if (GlobalValues.Level.Key == LevelKeys.Advanced)
            {
                if (GlobalValues.WOW.Key == WOWKeys.WSASCombined)
                    cols.Add(v_WSAS.AdvancedWSAS);
                else
                    cols.Add(v_WSAS.Percent_Advanced);
            }
            else if (GlobalValues.Level.Key == LevelKeys.AdvancedProficient)
            {
                if (GlobalValues.WOW.Key == WOWKeys.WSASCombined)
                    cols.Add(v_WSAS.AdvancedPlusProficientTotalWSAS);
                else
                    cols.Add(v_WSAS.PCTAdvPlusPCTPrf);
            }
            else if (GlobalValues.Level.Key == LevelKeys.BasicMinSkillEng)
            {
                if (GlobalValues.WOW.Key == WOWKeys.WSASCombined)
                {
                    cols.Add(v_WSAS.BasicPlusMinPerfPlusNoWSASTotalWSAS); 
                }
                else
                    cols.Add(v_WSAS.BasicPlusMinPerfPlusPreReqSkillsEngPlusNoWSASTotal);
            }
            else if (GlobalValues.Level.Key == LevelKeys.WAA_SwD && GlobalValues.WOW.Key == WOWKeys.WKCE)
            {
                cols.Add(v_WSAS.Percent_PreReq_Skill_Level_1);
                cols.Add(v_WSAS.Percent_PreReq_Skill_Level_2);
                cols.Add(v_WSAS.Percent_PreReq_Skill_Level_3);
                cols.Add(v_WSAS.Percent_PreReq_Skill_Level_4);
                cols.Add(v_WSAS.PctTotalWAADisabil);
            }
            else if (GlobalValues.Level.Key == LevelKeys.WAA_ELL && GlobalValues.WOW.Key == WOWKeys.WKCE)
            {
                cols.Add(v_WSAS.Percent_PreReq_Eng_Minimal);
                 cols.Add(v_WSAS.Percent_PreReq_Eng_Basic);
                 cols.Add(v_WSAS.Percent_PreReq_Eng_Proficient);
                 cols.Add(v_WSAS.Percent_PreReq_Eng_Advanced);
                 cols.Add(v_WSAS.PctTotalWAALep);
            }
            else if (GlobalValues.Level.Key == LevelKeys.NoWSAS)
            {
               cols.Add(v_WSAS.ExcusedByParent);
               cols.Add(v_WSAS.EligibleButNotTested);
               cols.Add(v_WSAS.No_WSAS_Total); //and for graph
            }
            
            return cols;
        }
        public List<string> StatewideDownloadVisibleColumns(List<string> cols)
        {
            int index;

            /*if (cols.Contains(v_Template_Keys_WWoDisEconELP_tblAgencyFull.School_Name))
                index = cols.IndexOf(v_Template_Keys_WWoDisEconELP_tblAgencyFull.School_Name)+1;
            else index = 0;
            cols.Insert(index, v_WSAS.GradeShortLabel);*/
            
            if (cols.Contains(v_WSAS.MigrantLabel)) index = cols.IndexOf(v_WSAS.MigrantLabel)+1;
            else index = 0;
            cols.Insert(index, v_WSAS.SubjectLabel);

            cols.Add(v_WSAS.Enrolled);
            cols.Add(v_WSAS.Included);
            cols.Add(v_WSAS.Number_ExcusedByParent);
            cols.Add(v_WSAS.ExcusedByParent);
            cols.Add(v_WSAS.Number_EligibleButNotTested);
            cols.Add(v_WSAS.EligibleButNotTested);
            cols.Add(v_WSAS.Number_No_WSAS_Total);
            cols.Add(v_WSAS.No_WSAS_Total);
            cols.Add(v_WSAS.Number_MinPerfWSAS);
            cols.Add(v_WSAS.MinPerfWSAS);
            cols.Add(v_WSAS.Number_BasicWSAS);
            cols.Add(v_WSAS.BasicWSAS);
            cols.Add(v_WSAS.Number_ProficientWSAS);
            cols.Add(v_WSAS.ProficientWSAS);
            cols.Add(v_WSAS.Number_AdvancedWSAS);
            cols.Add(v_WSAS.AdvancedWSAS);
            cols.Add(v_WSAS.Number_Minimal);
            cols.Add(v_WSAS.Percent_Minimal);
            cols.Add(v_WSAS.Number_Basic);
            cols.Add(v_WSAS.Percent_Basic);
            cols.Add(v_WSAS.Number_Proficient);
            cols.Add(v_WSAS.Percent_Proficient);
            cols.Add(v_WSAS.Number_Advanced);
            cols.Add(v_WSAS.Percent_Advanced);
            cols.Add(v_WSAS.Number_PreReq_Skill_Level_1);
            cols.Add(v_WSAS.Percent_PreReq_Skill_Level_1);
            cols.Add(v_WSAS.Number_PreReq_Skill_Level_2);
            cols.Add(v_WSAS.Percent_PreReq_Skill_Level_2);
            cols.Add(v_WSAS.Number_PreReq_Skill_Level_3);
            cols.Add(v_WSAS.Percent_PreReq_Skill_Level_3);
            cols.Add(v_WSAS.Number_PreReq_Skill_Level_4);
            cols.Add(v_WSAS.Percent_PreReq_Skill_Level_4);
            cols.Add(v_WSAS.Number_PreReq_Skill);
            cols.Add(v_WSAS.Percent_PreReq_Skill);

            cols.Add(v_WSAS.Number_PreReq_Eng_Minimal);
            cols.Add(v_WSAS.Percent_PreReq_Eng_Minimal);
            cols.Add(v_WSAS.Number_PreReq_Eng_Basic);
            cols.Add(v_WSAS.Percent_PreReq_Eng_Basic);
            cols.Add(v_WSAS.Number_PreReq_Eng_Proficient);
            cols.Add(v_WSAS.Percent_PreReq_Eng_Proficient);
            cols.Add(v_WSAS.Number_PreReq_Eng_Advanced);
            cols.Add(v_WSAS.Percent_PreReq_Eng_Advanced);
            cols.Add(v_WSAS.Number_PreReq_Eng);
            cols.Add(v_WSAS.Percent_PreReq_Eng);
            
            return cols;
        }
        public override List<string> GetDownloadRawVisibleColumns()
        {
            List<string> cols = base.GetDownloadRawVisibleColumns();
            int index = 0;

            if (GlobalValues.SuperDownload.Key == SupDwnldKeys.True)
                return StatewideDownloadVisibleColumns(cols);
            
            index = cols.IndexOf(v_WSAS.Enrolled);
            cols.Insert(index, v_WSAS.SubjectLabel);

            if (GlobalValues.Level.Key == LevelKeys.All)
            {
                if (GlobalValues.WOW.Key == WOWKeys.WSASCombined)
                {
                    index = cols.IndexOf(v_WSAS.MinPerfWSAS);
                    cols.Insert(index, v_WSAS.Number_MinPerfWSAS);
                    index = cols.IndexOf(v_WSAS.BasicWSAS);
                    cols.Insert(index, v_WSAS.Number_BasicWSAS);
                    index = cols.IndexOf(v_WSAS.ProficientWSAS);
                    cols.Insert(index, v_WSAS.Number_ProficientWSAS);
                    index = cols.IndexOf(v_WSAS.AdvancedWSAS);
                    cols.Insert(index, v_WSAS.Number_AdvancedWSAS);
                    index = cols.IndexOf(v_WSAS.No_WSAS_Total);
                    cols.Insert(index, v_WSAS.Number_No_WSAS_Total);
                }
                else
                {
                    index = cols.IndexOf(v_WSAS.No_WSAS_Total);
                    cols.Insert(index, v_WSAS.Number_No_WSAS_Total);
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
            }
            else if (GlobalValues.Level.Key == LevelKeys.Advanced)
            {
                if (GlobalValues.WOW.Key == WOWKeys.WSASCombined)
                {
                    index = cols.IndexOf(v_WSAS.AdvancedWSAS);
                    cols.Insert(index, v_WSAS.Number_AdvancedWSAS);
                }
                else
                {
                    index = cols.IndexOf(v_WSAS.Percent_Advanced);
                    cols.Insert(index, v_WSAS.Number_Advanced);
                }
            }
            else if (GlobalValues.Level.Key == LevelKeys.AdvancedProficient)
            {
                if (GlobalValues.WOW.Key == WOWKeys.WSASCombined)
                {
                    index = cols.IndexOf(v_WSAS.AdvancedPlusProficientTotalWSAS);
                    cols.Insert(index, v_WSAS.Number_AdvancedPlusProficientTotalWSAS);
                }
                else
                {
                    index = cols.IndexOf(v_WSAS.PCTAdvPlusPCTPrf);
                    cols.Insert(index, v_WSAS.Number_AdvPlusPCTPrf);
                }
            }
            else if (GlobalValues.Level.Key == LevelKeys.BasicMinSkillEng)
            {
                if (GlobalValues.WOW.Key == WOWKeys.WSASCombined)
                {
                    index = cols.IndexOf(v_WSAS.BasicPlusMinPerfPlusNoWSASTotalWSAS);
                    cols.Insert(index, v_WSAS.Number_BasicPlusMinPerfPlusNoWSASTotalWSAS);
                }
                else
                {
                    index = cols.IndexOf(v_WSAS.BasicPlusMinPerfPlusPreReqSkillsEngPlusNoWSASTotal);
                    cols.Insert(index, v_WSAS.Number_BasicPlusMinPerfPlusPreReqSkillsEngPlusNoWSASTotal);
                }
            }
            else if (GlobalValues.Level.Key == LevelKeys.WAA_SwD && GlobalValues.WOW.Key == WOWKeys.WKCE)
            {
                index = cols.IndexOf(v_WSAS.Percent_PreReq_Skill_Level_1);
                cols.Insert(index, v_WSAS.Number_PreReq_Skill_Level_1);
                index = cols.IndexOf(v_WSAS.Percent_PreReq_Skill_Level_2);
                cols.Insert(index, v_WSAS.Number_PreReq_Skill_Level_2);
                index = cols.IndexOf(v_WSAS.Percent_PreReq_Skill_Level_3);
                cols.Insert(index, v_WSAS.Number_PreReq_Skill_Level_3);
                index = cols.IndexOf(v_WSAS.Percent_PreReq_Skill_Level_4);
                cols.Insert(index, v_WSAS.Number_PreReq_Skill_Level_4);
                index = cols.IndexOf(v_WSAS.PctTotalWAADisabil);
                cols.Insert(index, v_WSAS.Number_TotalWAADisabil);

            }
            else if (GlobalValues.Level.Key == LevelKeys.WAA_ELL && GlobalValues.WOW.Key == WOWKeys.WKCE)
            {
                index = cols.IndexOf(v_WSAS.Percent_PreReq_Eng_Minimal);
                cols.Insert(index, v_WSAS.Number_PreReq_Eng_Minimal);
                index = cols.IndexOf(v_WSAS.Percent_PreReq_Eng_Basic);
                cols.Insert(index, v_WSAS.Number_PreReq_Eng_Basic);
                index = cols.IndexOf(v_WSAS.Percent_PreReq_Eng_Proficient);
                cols.Insert(index, v_WSAS.Number_PreReq_Eng_Proficient);
                index = cols.IndexOf(v_WSAS.Percent_PreReq_Eng_Advanced);
                cols.Insert(index, v_WSAS.Number_PreReq_Eng_Advanced);
                index = cols.IndexOf(v_WSAS.PctTotalWAALep);
                cols.Insert(index, v_WSAS.Number_TotalWAALep);
            }
            else if (GlobalValues.Level.Key == LevelKeys.NoWSAS)
            {
                index = cols.IndexOf(v_WSAS.ExcusedByParent);
                cols.Insert(index, v_WSAS.Number_ExcusedByParent);
                index = cols.IndexOf(v_WSAS.EligibleButNotTested);
                cols.Insert(index, v_WSAS.Number_EligibleButNotTested);
                index = cols.IndexOf(v_WSAS.No_WSAS_Total);
                cols.Insert(index, v_WSAS.Number_No_WSAS_Total);
            }

            return cols;
        }
        protected override SortedList<string, string> GetDownloadRawColumnLabelMapping()
        {
            SortedList<string, string> newLabels = base.GetDownloadRawColumnLabelMapping();
            return WsasColumnLabelMapping(newLabels);
        }
        public static SortedList<string, string> WsasColumnLabelMapping(SortedList<string, string> newLabels)
        {            
            newLabels.Remove(v_WSAS.GradeLabel);
            newLabels.Add(v_WSAS.GradeLabel, "tested_grade(s)");
            newLabels.Add(v_WSAS.Enrolled, "enrolled_at_test_time");
            newLabels.Add(v_WSAS.Included, "number_included_in_percents");
            newLabels.Add(v_WSAS.AdvancedWSAS, "percent_advanced_wsas");
            newLabels.Add(v_WSAS.Number_AdvancedWSAS, "number_advanced_wsas");
            newLabels.Add(v_WSAS.Percent_Advanced, "percent_advanced_wkce");
            newLabels.Add(v_WSAS.Number_Advanced, "number_advanced_wkce");
            newLabels.Add(v_WSAS.ExcusedByParent, "percent_no_wsas_excused_by_parent");
            newLabels.Add(v_WSAS.Number_ExcusedByParent, "number_no_wsas_excused_by_parent");
            newLabels.Add(v_WSAS.EligibleButNotTested, "percent_no_wsas_reason_unknown");
            newLabels.Add(v_WSAS.Number_EligibleButNotTested, "number_no_wsas_reason_unknown");
            newLabels.Add(v_WSAS.No_WSAS_Total, "percent_no_wsas_total");
            newLabels.Add(v_WSAS.Number_No_WSAS_Total, "number_no_wsas_total");
            newLabels.Add(v_WSAS.MinPerfWSAS, "percent_minperf_wsas");
            newLabels.Add(v_WSAS.Number_MinPerfWSAS, "number_minperf_wsas");
            newLabels.Add(v_WSAS.BasicWSAS, "percent_basic_wsas");
            newLabels.Add(v_WSAS.Number_BasicWSAS, "number_basic_wsas");
            newLabels.Add(v_WSAS.ProficientWSAS, "percent_proficient_wsas");
            newLabels.Add(v_WSAS.Number_ProficientWSAS, "number_proficient_wsas");
            newLabels.Add(v_WSAS.Percent_Minimal, "percent_minperf_wkce");
            newLabels.Add(v_WSAS.Number_Minimal, "number_minperf_wkce");
            newLabels.Add(v_WSAS.Percent_Basic, "percent_basic_wkce");
            newLabels.Add(v_WSAS.Number_Basic, "number_basic_wkce");
            newLabels.Add(v_WSAS.Percent_Proficient, "percent_proficient_wkce");
            newLabels.Add(v_WSAS.Number_Proficient, "number_proficient_wkce");
            newLabels.Add(v_WSAS.SubjectLabel, "subject");
            newLabels.Add(v_WSASDemographics.Number_PreReq_Skill, "number_waa_swd_total");
            newLabels.Add(v_WSASDemographics.Percent_PreReq_Skill, "percent_waa_swd_total");
            newLabels.Add(v_WSASDemographics.Percent_PreReq_Skill_Level_1, "percent_waa_swd_minperf");
            newLabels.Add(v_WSASDemographics.Number_PreReq_Skill_Level_1, "number_waa_swd_minperf");
            newLabels.Add(v_WSASDemographics.Percent_PreReq_Skill_Level_2, "percent_waa_swd_basic");
            newLabels.Add(v_WSASDemographics.Number_PreReq_Skill_Level_2, "number_waa_swd_basic");
            newLabels.Add(v_WSASDemographics.Percent_PreReq_Skill_Level_3, "percent_waa_swd_proficient");
            newLabels.Add(v_WSASDemographics.Number_PreReq_Skill_Level_3, "number_waa_swd_proficient");
            newLabels.Add(v_WSASDemographics.Percent_PreReq_Skill_Level_4, "percent_waa_swd_advanced");
            newLabels.Add(v_WSASDemographics.Number_PreReq_Skill_Level_4, "number_waa_swd_advanced");
            newLabels.Add(v_WSASDemographics.Percent_PreReq_Eng, "percent_waa_ell_total");
            newLabels.Add(v_WSASDemographics.Number_PreReq_Eng, "number_waa_ell_total");
            newLabels.Add(v_WSASDemographics.Percent_PreReq_Eng_Basic, "percent_waa_ell_basic");
            newLabels.Add(v_WSASDemographics.Number_PreReq_Eng_Basic, "number_waa_ell_basic");
            newLabels.Add(v_WSASDemographics.Percent_PreReq_Eng_Minimal, "percent_waa_ell_minperf");
            newLabels.Add(v_WSASDemographics.Number_PreReq_Eng_Minimal, "number_waa_ell_minperf");
            newLabels.Add(v_WSASDemographics.Percent_PreReq_Eng_Proficient, "percent_waa_ell_proficient");
            newLabels.Add(v_WSASDemographics.Number_PreReq_Eng_Proficient, "number_waa_ell_proficient");
            newLabels.Add(v_WSASDemographics.Percent_PreReq_Eng_Advanced, "percent_waa_ell_advanced");
            newLabels.Add(v_WSASDemographics.Number_PreReq_Eng_Advanced, "number_waa_ell_advanced");

            /** Following columns being removed from download, but saving mappings;*/
            newLabels.Add(v_WSAS.AdvancedPlusProficientTotalWSAS, "percent_advanced_plus_proficient_wsas");
            newLabels.Add(v_WSAS.Number_AdvancedPlusProficientTotalWSAS, "number_advanced_plus_proficient_wsas");
            newLabels.Add(v_WSAS.PCTAdvPlusPCTPrf, "percent_advanced_plus_proficient_wkce");
            newLabels.Add(v_WSAS.Number_AdvPlusPCTPrf, "number_advanced_plus_proficient_wkce");
            newLabels.Add(v_WSAS.BasicPlusMinPerfPlusNoWSASTotalWSAS, "percent_basic_plus_minperf_plus_nowsas_wsas");
            newLabels.Add(v_WSAS.Number_BasicPlusMinPerfPlusNoWSASTotalWSAS, "number_basic_plus_minperf_plus_nowsas_wsas");
            newLabels.Add(v_WSAS.BasicPlusMinPerfPlusPreReqSkillsEngPlusNoWSASTotal, "percent_basic_plus_minperf_plus_waa_swd_ell_plus_no_wsas_wkce");
            newLabels.Add(v_WSAS.Number_BasicPlusMinPerfPlusPreReqSkillsEngPlusNoWSASTotal, "number_basic_plus_minperf_plus_waa_swd_ell_plus_no_wsas_wkce");
            newLabels.Add(v_WSAS.PctTotalWAADisabil, "percent_waa_swd_total_wkce");
            newLabels.Add(v_WSAS.Number_TotalWAADisabil, "number_waa_swd_total_wkce");
            newLabels.Add(v_WSAS.PctTotalWAALep, "percent_waa_ell_total_wkce");
            newLabels.Add(v_WSAS.Number_TotalWAALep, "number_waa_ell_total_wkce");

            return newLabels;
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
        int CountDimensions(Dictionary<String, Boolean> disaggFlags)
        {
            int count = 0;
            foreach (KeyValuePair<String, Boolean> dimension in disaggFlags)
            {
                if (dimension.Value)
                    count++;
            }
            return count;
        }
        void HideGraphForDisAggOverflow()
        {
            Graph.Visible = false;
        }
        public Dictionary<String, Boolean> SetIsDisaggFlags(GlobalValues globals)
        {
            Dictionary<String, Boolean> disaggFlags = new Dictionary<string, bool>();

            disaggFlags.Add(globals.Grade.Name, (globals.Grade.Key == GradeKeys.AllDisAgg));
            disaggFlags.Add(globals.SubjectID.Name, (globals.SubjectID.Key == SubjectIDKeys.AllTested));
            disaggFlags.Add(globals.Level.Name,  (globals.Level.Key == LevelKeys.All));
            disaggFlags.Add(globals.Group.Name, (globals.Group.Key != GroupKeys.All));
            disaggFlags.Add(globals.CompareTo.Name, (globals.CompareTo.Key != CompareToKeys.Current));

            return disaggFlags;
        }
        private String ChooseLevelColumn()
        {
            if (GlobalValues.WOW.Key == WOWKeys.WKCE)
            {
                if (GlobalValues.Level.Key == LevelKeys.Advanced) return v_WSAS.Percent_Advanced;
                if (GlobalValues.Level.Key == LevelKeys.AdvancedProficient) return v_WSAS.PCTAdvPlusPCTPrf;
                if (GlobalValues.Level.Key == LevelKeys.BasicMinSkillEng) return v_WSAS.BasicPlusMinPerfPlusPreReqSkillsEngPlusNoWSASTotal;
                if (GlobalValues.Level.Key == LevelKeys.WAA_SwD) return v_WSAS.PctTotalWAADisabil;
                if (GlobalValues.Level.Key == LevelKeys.WAA_ELL) return v_WSAS.PctTotalWAALep;
                if (GlobalValues.Level.Key == LevelKeys.NoWSAS) return v_WSAS.No_WSAS_Total;
            }
            else// if (GlobalValues.WOW.Key == WOWKeys.WSASCombined)
            {
                if (GlobalValues.Level.Key == LevelKeys.Advanced) return v_WSAS.AdvancedWSAS;
                if (GlobalValues.Level.Key == LevelKeys.AdvancedProficient) return v_WSAS.AdvancedPlusProficientTotalWSAS;
                if (GlobalValues.Level.Key == LevelKeys.BasicMinSkillEng) return v_WSAS.BasicPlusMinPerfPlusNoWSASTotalWSAS;
                if (GlobalValues.Level.Key == LevelKeys.NoWSAS) return v_WSAS.No_WSAS_Total;
            }
            throw new Exception("No Column found for given Level");
        }
        private List<String> SetMeasuresForLevels(GraphBarChart barChart)
        {
            List<String> graphColumns = new List<String>();
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
            return graphColumns;
        }
        private void SetLevelSeriesLabelsOverrides(GraphBarChart barChart)
        {
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
    }
}
