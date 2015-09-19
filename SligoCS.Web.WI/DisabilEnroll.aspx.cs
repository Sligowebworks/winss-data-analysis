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

using System.Text;
using SligoCS.Web.WI.WebSupportingClasses;
using SligoCS.Web.WI.WebSupportingClasses.WI;
using ChartFX.WebForms;

namespace SligoCS.Web.WI
{
    public partial class DisabilEnroll : PageBaseWI
    {
        protected override DALWIBase InitDatabase()
        {
            return new DALDisabilities();
        }
        protected override Chart InitGraph()
        {
            barChart.Visible = (GlobalValues.PrDis.Key != PrDisKeys.AllDisabilities);
            hrzBarChart.Visible = !barChart.Visible;

            return (GlobalValues.PrDis.Key != PrDisKeys.AllDisabilities)? (Chart)barChart : (Chart) hrzBarChart;
        }
        protected override GridView InitDataGrid()
        {
            return DisabilitiesDataGrid;
        }
        protected override SligoCS.Web.WI.WebUserControls.NavViewByGroup InitNavRowGroups()
        {
            nlrVwByGroup.AddRaceGender = true;

            if (GlobalValues.OrgLevel.Key != OrgLevelKeys.State) 
            {
                nlrVwByGroup.LinksEnabled = SligoCS.Web.WI.WebUserControls.NavViewByGroup.EnableLinksVector.AllStudents;
            }
            else
            {
                nlrVwByGroup.LinksEnabled = SligoCS.Web.WI.WebUserControls.NavViewByGroup.EnableLinksVector.RaceGender;
                nlrVwByGroup.LinkRow.LinkControlAdded += new LinkControlAddedHandler(LinkRow_EnableStateLevelGroupLinks);
            }

            return nlrVwByGroup;
        }

        void LinkRow_EnableStateLevelGroupLinks(SligoCS.Web.Base.WebServerControls.WI.HyperLinkPlus link)
        {
            if (link.ID == "linkGroupAll"
                || link.ID == "linkGroupGender"
                || link.ID == "linkGroupRace"
                || link.ID == "linkGroupRaceGender")
                link.Enabled = true;
        }
        protected override void OnInitComplete(EventArgs e)
        {
            GlobalValues.TrendStartYear = 2003;
            GlobalValues.CurrentYear = 2012;

            DisabilitiesDataGrid.ColumnLoaded += new EventHandler(DisabilitiesDataGrid_ColumnLoaded);

            nlrSTYP.LinkRow.LinkControlAdded += new LinkControlAddedHandler(LinkRow_GrayOutSummaryStyp);
            if (GlobalValues.STYP.Key == STYPKeys.StateSummary && GlobalValues.Group.Key == GroupKeys.All)
                    GlobalValues.STYP.Value = GlobalValues.STYP.Range[STYPKeys.AllTypes];

            QueryMarshaller.RaceDisagCodes.Remove((int)QueryMarshaller.RaceCodes.Comb);

            base.OnInitComplete(e);

            //putting after base because overriding global GROUP override by STYP
            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.State)
            {
                GlobalValues.Group.Value = UserValues.Group.Value;
                GlobalValues.OverrideGroupByLinksShown(NavRowGroups.LinksEnabled);
            }

        }

        void LinkRow_GrayOutSummaryStyp(SligoCS.Web.Base.WebServerControls.WI.HyperLinkPlus link)
        {
            if (link.ID == "linkSTYP_StateSummary")
            {
                if (GlobalValues.Group.Key == GroupKeys.All)
                    link.Enabled = false;
            }
        }
        
        void DisabilitiesDataGrid_ColumnLoaded(object sender, EventArgs e)
        {
            if (!(sender is WebSupportingClasses.WinssDataGridColumn)) return;

            WebSupportingClasses.WinssDataGridColumn col = (WebSupportingClasses.WinssDataGridColumn)sender;

            if (GlobalValues.PrDis.Key != PrDisKeys.AllDisabilities)
            {
                if (col.DataField.Contains("Count")) col.HeaderText = "Number";
                if (col.DataField.Contains("Percent")) col.HeaderText = "Percent";
            }
            else
            {
                if (col.DataField.Contains("Percent")) col.HeaderText = col.DataField.Replace("Percent", String.Empty);
                if (col.DataField.Contains("OtherPercent")) col.HeaderText = "Other Primary Disability";
                if (col.DataField.Contains("StudentsWODisPercent")) col.HeaderText = "Students w/o Disability";
            }
        }
        protected override string SetPageHeading()
        {
            return "What are the primary disabilities of students receiving special education services?";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSetTitle = GetTitle("Enrollment by Primary Disability");

            DisabilitiesDataGrid.AddSuperHeader(DataSetTitle);
            if (GlobalValues.PrDis.Key != PrDisKeys.AllDisabilities) DisabilitiesDataGrid.AddSuperHeader(BuildSecondaryTableTitleRow());
            
            set_state();
            setBottomLink();

            int dimcount =0 ;
            if (GlobalValues.STYP.Key == STYPKeys.AllTypes) dimcount++;
            if (GlobalValues.PrDis.Key == PrDisKeys.AllDisabilities) dimcount++;
            if (GlobalValues.CompareTo.Key != CompareToKeys.Current) dimcount++;
            if (GlobalValues.Group.Key != GroupKeys.All) dimcount++;

            if (dimcount < 3)
            {
                if (GlobalValues.PrDis.Key != PrDisKeys.AllDisabilities) SetUpChart(DataSet);
                else SetUpHorizChart(DataSet);
            }
            else
            {
                barChart.Visible = hrzBarChart.Visible = false;
            }


            /*
            if (GlobalValues.STYP.Key == STYPKeys.AllTypes
                && GlobalValues.PrDis.Key == PrDisKeys.AllDisabilities
                && GlobalValues.CompareTo.Key == CompareToKeys.Years)
                barChart.Visible = hrzBarChart.Visible = false;*/
        }

        private TableRow BuildSecondaryTableTitleRow()
        {
            TableRow tr = new TableRow();
            String dis = GlobalValues.PrDis.Key;
            int spacer = base.GetVisibleColumns().Count + 1;

            WinssDataGrid.AddTableCell(tr, String.Empty, spacer);
            WinssDataGrid.AddTableCell(tr, GetMeasureLabel(), 2);
            if (dis == PrDisKeys.Combined)
                WinssDataGrid.AddTableCell(tr, "Students w/o Disabilities", 2);

            //Common Columns
            if (dis != PrDisKeys.AllDisabilities
                && dis != PrDisKeys.Combined)
            {
                WinssDataGrid.AddTableCell(tr, "Other Primary Disability", 2);
                WinssDataGrid.AddTableCell(tr, "Students w/o Disability", 2);
            }

            return tr;
        }
        private String GetMeasureLabel()
        {
            String dis = GlobalValues.PrDis.Key;

            if (dis == PrDisKeys.Combined)
            {
                return  "Combined";
            }
            else if (dis == PrDisKeys.Autism)
            {
                return  "Autism";
            }
            else if (dis == PrDisKeys.Cognitive)
            {
                return  "Cognitive Disability";
            }
            else if (dis == PrDisKeys.DeafBlind)
            {
                return  "Deaf-Blind";
            }
            else if (dis == PrDisKeys.Developmental)
            {
                return  "Significant Developmental Delay";
            }
            else if (dis == PrDisKeys.Emotional)
            {
                return  "Emotional Behavioral Disability";
            }
            else if (dis == PrDisKeys.Hearing)
            {
                return  "Hearing Impairment";
            }
            else if (dis == PrDisKeys.Learning)
            {
                return  "Specific Learning Disability";
            }
            else if (dis == PrDisKeys.Orthopedic)
            {
                return  "Orthopedic Impairment";
            }
            else if (dis == PrDisKeys.OtherHealth)
            {
                return  "Other Health Impairment";
            }
            else if (dis == PrDisKeys.SpeachLanguage)
            {
                return  "Speech or Language Impairment";
            }
            else if (dis == PrDisKeys.TraumaticBrain)
            {
                return  "Tramatic Brain Injury";
            }
            else if (dis == PrDisKeys.Visual)
            {
                return "Visual Impairment";
            }
            else
            {
                return String.Empty;
            }

        }
        private void SetUpHorizChart(DataSet ds)
        {
            try
            {
                hrzBarChart.SelectedSortBySecondarySort = false;
                hrzBarChart.Title = DataSetTitle;
                if (GlobalValues.CompareTo.Key == CompareToKeys.Years)
                {
                    hrzBarChart.LabelColumns = new List<String>(new String[]
                        {
                            v_Disabilities2.YearFormatted
                        });
                }
                if (GlobalValues.CompareTo.Key == CompareToKeys.OrgLevel)
                {
                    hrzBarChart.LabelColumns = new List<String>(new String[]
                        {
                            v_Disabilities2.OrgLevelLabel
                        });
                }
                if (GlobalValues.CompareTo.Key == CompareToKeys.SelDistricts
                    || GlobalValues.CompareTo.Key == CompareToKeys.SelSchools)
                {
                    hrzBarChart.LabelColumns = new List<String>(new String[]
                        {
                            v_Disabilities2.Name
                        });
                }
                if (GlobalValues.CompareTo.Key == CompareToKeys.Current)
                {
                    if (GlobalValues.STYP.Key == STYPKeys.AllTypes)
                    {
                        hrzBarChart.LabelColumns = new List<string>(new String[]
                        {
                            v_Disabilities2.SchooltypeLabel
                        });
                    }
                    else
                    {
                        hrzBarChart.LabelColumns = new List<string>(new String[]
                        {
                            v_Disabilities2.OrgLevelLabel
                        });
                    }
                }

                if (GlobalValues.Group.Key != GroupKeys.All) hrzBarChart.SelectedSortBySecondarySort = true;

                if (GlobalValues.Group.Key == GroupKeys.RaceGender)
                {
                    hrzBarChart.LabelColumns.Add(v_Disabilities2.SexLabel);
                    hrzBarChart.LabelColumns.Add(v_Disabilities2.RaceShortLabel);
                }
                if (GlobalValues.Group.Key == GroupKeys.Gender)
                {
                    hrzBarChart.LabelColumns.Add(v_Disabilities2.SexLabel);
                }
                if (GlobalValues.Group.Key == GroupKeys.Race)
                {
                    hrzBarChart.LabelColumns.Add(v_Disabilities2.RaceShortLabel);
                }

                hrzBarChart.MeasureColumns = new List<String>(new String[]
                    {
                        v_Disabilities2.CDPercent,
                        v_Disabilities2.EBDPercent,
                        v_Disabilities2.LDPercent,
                        v_Disabilities2.SLPercent,
                        v_Disabilities2.OtherPercent,
                        v_Disabilities2.StudentsWODisPercent
                    }
                );

                hrzBarChart.OverrideSeriesLabels = new Hashtable(6);
                hrzBarChart.OverrideSeriesLabels.Add(v_Disabilities2.CDPercent, "% " + PrDisKeys.Cognitive);
                hrzBarChart.OverrideSeriesLabels.Add(v_Disabilities2.LDPercent, "% " + PrDisKeys.Learning);
                hrzBarChart.OverrideSeriesLabels.Add(v_Disabilities2.OtherPercent, "% " + PrDisKeys.OtherHealth);
                hrzBarChart.OverrideSeriesLabels.Add(v_Disabilities2.EBDPercent, "% " + PrDisKeys.Emotional);
                hrzBarChart.OverrideSeriesLabels.Add(v_Disabilities2.SLPercent, "% " + PrDisKeys.SpeachLanguage);
                hrzBarChart.OverrideSeriesLabels.Add(v_Disabilities2.StudentsWODisPercent, "% w/o Disabilities");

                hrzBarChart.AxisYDescription = "Percent of Students Enrolled";
                hrzBarChart.YAxisSuffix = "\\%";
            }
            catch (Exception ex)
            {
                if (GlobalValues.TraceLevels > 0) throw ex;
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        private void SetUpChart(DataSet ds)
        {
            //try
            {
                barChart.Title = DataSetTitle;
                
                if (GlobalValues.Group.Key == GroupKeys.All)
                {
                    List<String> friendlyAxisXName = new List<String>();
                    friendlyAxisXName.Add(GlobalValues.PrDis.Key);
                    barChart.FriendlyAxisXNames = friendlyAxisXName;
                }

                String axisYlbl = GlobalValues.PrDis.Key;
                int openparen = axisYlbl.IndexOf("(");
                int closeparen = axisYlbl.IndexOf(")");
                axisYlbl = axisYlbl.Substring(openparen + 1, closeparen - openparen-1);
                barChart.AxisYDescription =axisYlbl + " as a Percent of Students Enrolled";
                barChart.DisplayColumnName = GetBarChartDisplayColumnName();
                barChart.MaxRateInResult = WebUserControls.GraphBarChart.GetMaxRateInColumn(ds.Tables[0], barChart.DisplayColumnName);

            }
            //catch (Exception ex)
            //{
            //    System.Diagnostics.Debug.WriteLine(ex.Message);
            //}
        }
        private double GetMaxRateInResult(DataSet ds)
        {
            double maxRateInResult = 0;

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                try
                {
                    if (Convert.ToDouble(
                            row[v_Disabilities2.Enrollment_PK12].ToString())
                                        > maxRateInResult)
                    {
                        maxRateInResult = Convert.ToDouble
                                (v_Disabilities2.Enrollment_PK12.ToString());
                    }
                }
                catch
                {
                    continue;
                }
            }
            return maxRateInResult;
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
        public override List<string> GetVisibleColumns()
        {
            List<string> cols = base.GetVisibleColumns();

            cols.Add(v_Disabilities2.Enrollment_PK12);

            bool statewide = (GlobalValues.SuperDownload.Key == SupDwnldKeys.True);

            if (GlobalValues.PrDis.Key == PrDisKeys.AllDisabilities && !statewide)
            {
                cols.Add(v_Disabilities2.CDPercent);
                cols.Add(v_Disabilities2.EBDPercent);
                cols.Add(v_Disabilities2.LDPercent);
                cols.Add(v_Disabilities2.SLPercent);
                cols.Add(v_Disabilities2.OtherPercent);
                cols.Add(v_Disabilities2.StudentsWODisPercent);
            }
            if (GlobalValues.PrDis.Key == PrDisKeys.Autism || statewide)
            {
                cols.Add(v_Disabilities2.ACount);
                cols.Add(v_Disabilities2.APercent);
                if (!statewide)
                {
                    cols.Add(v_Disabilities2.NonACount);
                    cols.Add(v_Disabilities2.NonAPercent);
                }
            }
            if (GlobalValues.PrDis.Key == PrDisKeys.Cognitive || statewide)
            {
                cols.Add(v_Disabilities2.CDCount);
                cols.Add(v_Disabilities2.CDPercent);
                if (!statewide)
                {
                    cols.Add(v_Disabilities2.NonCDCount);
                    cols.Add(v_Disabilities2.NonCDPercent);
                }
            }
            if (GlobalValues.PrDis.Key == PrDisKeys.DeafBlind || statewide)
            {
                cols.Add(v_Disabilities2.DBCount);
                cols.Add(v_Disabilities2.DBPercent);
                if (!statewide)
                {
                    cols.Add(v_Disabilities2.NonDBCount);
                    cols.Add(v_Disabilities2.NonDBPercent);
                }
            }
            if (GlobalValues.PrDis.Key == PrDisKeys.Developmental || statewide)
            {
                cols.Add(v_Disabilities2.SDDCount);
                cols.Add(v_Disabilities2.SDDPercent);
                if (!statewide)
                {
                    cols.Add(v_Disabilities2.NonSDDCount);
                    cols.Add(v_Disabilities2.NonSDDPercent);
                }
            }
            if (GlobalValues.PrDis.Key == PrDisKeys.Emotional || statewide)
            {
                cols.Add(v_Disabilities2.EBDCount);
                cols.Add(v_Disabilities2.EBDPercent);
                if (!statewide)
                {
                    cols.Add(v_Disabilities2.NonEBDCount);
                    cols.Add(v_Disabilities2.NonEBDPercent);
                }
            }
            if (GlobalValues.PrDis.Key == PrDisKeys.Hearing || statewide)
            {
                cols.Add(v_Disabilities2.HCount);
                cols.Add(v_Disabilities2.HPercent);
                if (!statewide)
                {
                    cols.Add(v_Disabilities2.NonHCount);
                    cols.Add(v_Disabilities2.NonHPercent);
                }
            }
            if (GlobalValues.PrDis.Key == PrDisKeys.Learning || statewide)
            {
                cols.Add(v_Disabilities2.LDCount);
                cols.Add(v_Disabilities2.LDPercent);
                if (!statewide)
                {
                    cols.Add(v_Disabilities2.NonLDCount);
                    cols.Add(v_Disabilities2.NonLDPercent);
                }
            }
            if (GlobalValues.PrDis.Key == PrDisKeys.Orthopedic || statewide)
            {
                cols.Add(v_Disabilities2.OICount);
                cols.Add(v_Disabilities2.OIPercent);
                if (!statewide)
                {
                    cols.Add(v_Disabilities2.NonOICount);
                    cols.Add(v_Disabilities2.NonOIPercent);
                }
            }
            if (GlobalValues.PrDis.Key == PrDisKeys.OtherHealth || statewide)
            {
                cols.Add(v_Disabilities2.OHICount);
                cols.Add(v_Disabilities2.OHIPercent);
                if (!statewide)
                {
                    cols.Add(v_Disabilities2.NonOHICount);
                    cols.Add(v_Disabilities2.NonOHIPercent);
                }
            }
            if (GlobalValues.PrDis.Key == PrDisKeys.SpeachLanguage || statewide)
            {
                cols.Add(v_Disabilities2.SLCount);
                cols.Add(v_Disabilities2.SLPercent);
                if (!statewide)
                {
                    cols.Add(v_Disabilities2.NonSLCount);
                    cols.Add(v_Disabilities2.NonSLPercent);
                }
            }
            if (GlobalValues.PrDis.Key == PrDisKeys.TraumaticBrain || statewide)
            {
                cols.Add(v_Disabilities2.TBICount);
                cols.Add(v_Disabilities2.TBIPercent);
                if (!statewide)
                {
                    cols.Add(v_Disabilities2.NonTBICount);
                    cols.Add(v_Disabilities2.NonTBIPercent);
                }
            }
            if (GlobalValues.PrDis.Key == PrDisKeys.Visual || statewide)
            {
                cols.Add(v_Disabilities2.VICount);
                cols.Add(v_Disabilities2.VIPercent);
                if (!statewide)
                {
                    cols.Add(v_Disabilities2.NonVICount);
                    cols.Add(v_Disabilities2.NonVIPercent);
                }
            }
            if (GlobalValues.PrDis.Key == PrDisKeys.Combined || statewide)
            {
                cols.Add(v_Disabilities2.SWDCount);
                cols.Add(v_Disabilities2.SWDPercent);
            }
            // Common Columns:
            if (GlobalValues.PrDis.Key != PrDisKeys.AllDisabilities || statewide)
            {
                cols.Add(v_Disabilities2.StudentsWODisCount);
                cols.Add(v_Disabilities2.StudentsWODisPercent);
            }
            
            return cols;
        }
        private String GetBarChartDisplayColumnName()
        {
            if (GlobalValues.PrDis.Key == PrDisKeys.AllDisabilities)
            {
                return String.Empty;
            }
            if (GlobalValues.PrDis.Key == PrDisKeys.Autism)
            {
                return v_Disabilities2.APercent;
            }
            else if (GlobalValues.PrDis.Key == PrDisKeys.Cognitive)
            {
                return v_Disabilities2.CDPercent;
            }
            else if (GlobalValues.PrDis.Key == PrDisKeys.Combined)
            {
                return v_Disabilities2.SWDPercent;
            }
            else if (GlobalValues.PrDis.Key == PrDisKeys.DeafBlind)
            {
                return v_Disabilities2.DBPercent;
            }
            else if (GlobalValues.PrDis.Key == PrDisKeys.Developmental)
            {
                return v_Disabilities2.SDDPercent;
            }
            else if (GlobalValues.PrDis.Key == PrDisKeys.Emotional)
            {
                return v_Disabilities2.EBDPercent;
            }
            else if (GlobalValues.PrDis.Key == PrDisKeys.Hearing)
            {
                return v_Disabilities2.HPercent;
            }
            else if (GlobalValues.PrDis.Key == PrDisKeys.Learning)
            {
                return v_Disabilities2.LDPercent;
            }
            else if (GlobalValues.PrDis.Key == PrDisKeys.Orthopedic)
            {
                return v_Disabilities2.OIPercent;
            }
            else if (GlobalValues.PrDis.Key == PrDisKeys.OtherHealth)
            {
                return v_Disabilities2.OHIPercent;
            }
            else if (GlobalValues.PrDis.Key == PrDisKeys.SpeachLanguage)
            {
                return v_Disabilities2.SLPercent;
            }
            else if (GlobalValues.PrDis.Key == PrDisKeys.TraumaticBrain)
            {
                return v_Disabilities2.TBIPercent;
            }
            else if (GlobalValues.PrDis.Key == PrDisKeys.Visual)
            {
                return v_Disabilities2.VIPercent;
            }
            return String.Empty;
        }

        public override List<string> GetDownloadRawVisibleColumns()
        {
            List <String> cols = base.GetDownloadRawVisibleColumns();

            if (GlobalValues.PrDis.Key == PrDisKeys.AllDisabilities
                && GlobalValues.SuperDownload.Key == SupDwnldKeys.False)
            {
                int index = cols.IndexOf(v_Disabilities2.CDPercent);
                cols.Insert(index, v_Disabilities2.CDCount);

                index = cols.IndexOf(v_Disabilities2.EBDPercent);
                cols.Insert(index, v_Disabilities2.EBDCount);

                index = cols.IndexOf(v_Disabilities2.LDPercent);
                cols.Insert(index, v_Disabilities2.LDCount);

                index = cols.IndexOf(v_Disabilities2.SLPercent);
                cols.Insert(index, v_Disabilities2.SLCount);

                index = cols.IndexOf(v_Disabilities2.OtherPercent);
                cols.Insert(index, v_Disabilities2.OtherCount);

                index = cols.IndexOf(v_Disabilities2.StudentsWODisPercent);
                cols.Insert(index, v_Disabilities2.StudentsWODisCount);
            }

            if (GlobalValues.SuperDownload.Key == SupDwnldKeys.True)
            {

            }
            
            return cols;
        }

        protected override SortedList<string, string> GetDownloadRawColumnLabelMapping()
        {
            SortedList<string, string> newLabels = base.GetDownloadRawColumnLabelMapping();

            if (GlobalValues.SuperDownload.Key == SupDwnldKeys.False)
            {
                newLabels.Add(v_Disabilities2.NonCDCount, "other_primary_disability_count");
                newLabels.Add(v_Disabilities2.NonCDPercent, "other_primary_disability_percent");

                newLabels.Add(v_Disabilities2.NonEBDCount, "other_primary_disability_count");
                newLabels.Add(v_Disabilities2.NonEBDPercent, "other_primary_disability_percent");

                newLabels.Add(v_Disabilities2.NonLDCount, "other_primary_disability_count");
                newLabels.Add(v_Disabilities2.NonLDPercent, "other_primary_disability_percent");

                newLabels.Add(v_Disabilities2.NonSLCount, "other_primary_disability_count");
                newLabels.Add(v_Disabilities2.NonSLPercent, "other_primary_disability_percent");

                newLabels.Add(v_Disabilities2.NonACount, "other_primary_disability_count");
                newLabels.Add(v_Disabilities2.NonAPercent, "other_primary_disability_percent");

                newLabels.Add(v_Disabilities2.NonHCount, "other_primary_disability_count");
                newLabels.Add(v_Disabilities2.NonHPercent, "other_primary_disability_percent");

                newLabels.Add(v_Disabilities2.NonOHICount, "other_primary_disability_count");
                newLabels.Add(v_Disabilities2.NonOHIPercent, "other_primary_disability_percent");

                newLabels.Add(v_Disabilities2.NonOICount, "other_primary_disability_count");
                newLabels.Add(v_Disabilities2.NonOIPercent, "other_primary_disability_percent");

                newLabels.Add(v_Disabilities2.NonSDDCount, "other_primary_disability_count");
                newLabels.Add(v_Disabilities2.NonSDDPercent, "other_primary_disability_percent");

                newLabels.Add(v_Disabilities2.NonTBICount, "other_primary_disability_count");
                newLabels.Add(v_Disabilities2.NonTBIPercent, "other_primary_disability_percent");

                newLabels.Add(v_Disabilities2.NonVICount, "other_primary_disability_count");
                newLabels.Add(v_Disabilities2.NonVIPercent, "other_primary_disability_percent");
            }
 
            newLabels.Add(v_Disabilities2.CDCount, "number_cognitively_disabled");
            newLabels.Add(v_Disabilities2.CDPercent, "percent_cognitively_disabled");

            newLabels.Add(v_Disabilities2.EBDCount, "number_emotional_behavioral_disability");
            newLabels.Add(v_Disabilities2.EBDPercent, "percent_emotional_behavioral_disability");

            newLabels.Add(v_Disabilities2.LDCount, "number_specific_learning_disabilities");
            newLabels.Add(v_Disabilities2.LDPercent, "percent_specific_learning_disabilities");

            newLabels.Add(v_Disabilities2.SLCount, "number_speech_or_language_impairment");
            newLabels.Add(v_Disabilities2.SLPercent, "percent_speech_or_language_impairment");

            newLabels.Add(v_Disabilities2.ACount, "number_autism");
            newLabels.Add(v_Disabilities2.APercent, "percent_autism");

            newLabels.Add(v_Disabilities2.HCount, "number_hearing_impairment");
            newLabels.Add(v_Disabilities2.HPercent, "percent_hearing_impairment");

            newLabels.Add(v_Disabilities2.OHICount, "number_other_health_impairment");
            newLabels.Add(v_Disabilities2.OHIPercent, "percent_other_health_impairment");

            newLabels.Add(v_Disabilities2.OICount, "number_orthopedic_impairment");
            newLabels.Add(v_Disabilities2.OIPercent, "percent_orthopedic_impairment");

            newLabels.Add(v_Disabilities2.SDDCount, "number_significant_development_delay");
            newLabels.Add(v_Disabilities2.SDDPercent, "percent_significant_development_delay");

            newLabels.Add(v_Disabilities2.TBICount, "number_traumatic_brain_injury");
            newLabels.Add(v_Disabilities2.TBIPercent, "percent_traumatic_brain_injury");

            newLabels.Add(v_Disabilities2.VICount, "number_visual_impairment");
            newLabels.Add(v_Disabilities2.VIPercent, "percent_visual_impairment");

            newLabels.Add(v_Disabilities2.SWDCount, "number_total_students_with_disability");
            newLabels.Add(v_Disabilities2.SWDPercent, "percent_total_students_with_disability");
            newLabels.Add(v_Disabilities2.StudentsWODisCount, "number_students_without_disability");
            newLabels.Add(v_Disabilities2.StudentsWODisPercent, "percent_students_without_disability");

            return newLabels;
        }
    }
}
