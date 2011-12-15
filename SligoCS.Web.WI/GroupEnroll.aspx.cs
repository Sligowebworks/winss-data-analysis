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
using ChartFX.WebForms;


namespace SligoCS.Web.WI
{
    public partial class GroupEnroll : PageBaseWI
    {
        protected override void OnInitComplete(EventArgs e)
        {
            GlobalValues.Grade.Key = GradeKeys.Combined_PreK_12;
            GlobalValues.CurrentYear = 2011;

            QueryMarshaller.RaceDisagCodes.Remove((int)QueryMarshaller.RaceCodes.Comb);

            if (GlobalValues.Group.Key == GroupKeys.Disability)
            {
                GlobalValues.TrendStartYear = 2003;
            }
            else if (GlobalValues.Group.Key == GroupKeys.EconDisadv)
            {
                GlobalValues.TrendStartYear = 2001;
            }
            else if (GlobalValues.Group.Key == GroupKeys.EngLangProf)
            {
                GlobalValues.TrendStartYear = 1999;
            }
            else
            {
                GlobalValues.TrendStartYear = 1996;
            }
            base.OnInitComplete(e);
        }

        protected override NavViewByGroup InitNavRowGroups()
        {
            nlrVwByGroup.LinksEnabled = NavViewByGroup.EnableLinksVector.EconElp;
            return nlrVwByGroup;
        }

        protected override ChartFX.WebForms.Chart InitGraph()
        {
            barChart.Visible = (GlobalValues.Group.Key == GroupKeys.All);
            hrzBarChart.Visible = !barChart.Visible;

            return (GlobalValues.Group.Key == GroupKeys.All) ? (Chart)barChart : (Chart)hrzBarChart;
        }
        protected override DALWIBase InitDatabase()
        {
            return new DALEnrollment();
        }
        protected override GridView InitDataGrid()
        {
            return EnrollmentDataGrid;
        }
        protected override string SetPageHeading()
        {
            return "What is the enrollment by student group?";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            nlrCompareTo.LinkRow.LinkControlAdded += CompareToLinkHandler;

            DataSetTitle = GetTitle("Enrollment");

            EnrollmentDataGrid.AddSuperHeader(DataSetTitle);

            if (GlobalValues.CompareTo.Key != CompareToKeys.Years)
            {
                DefPanel.Visible = true;
            }

            set_state();
            setBottomLink();

            if (!((GlobalValues.CompareTo.Key == CompareToKeys.SelDistricts || GlobalValues.CompareTo.Key == CompareToKeys.SelSchools)
                && GlobalValues.S4orALL.Key == S4orALLKeys.AllSchoolsOrDistrictsIn))
            {
                if (GlobalValues.Group.Key == GroupKeys.All) SetUpChart();
                else SetUpHorizChart();
            }

            if ((GlobalValues.CompareTo.Key == CompareToKeys.SelDistricts || GlobalValues.CompareTo.Key == CompareToKeys.SelSchools)
                && GlobalValues.S4orALL.Key == S4orALLKeys.AllSchoolsOrDistrictsIn)
                barChart.Visible = hrzBarChart.Visible = false;
        }

        private void SetUpChart()
        {
            try
            {
                barChart.Title = DataSetTitle;
                if (GlobalValues.Group.Key == GroupKeys.All)
                {
                    List<String> friendlyAxisXName = new List<String>(new String[] { String.Empty });
                    barChart.FriendlyAxisXNames = friendlyAxisXName;
                }
                barChart.AxisYDescription = "Count of All Students";
                barChart.AxisY.LabelsFormat.Format = AxisFormat.Number;

                barChart.DisplayColumnName = v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Total_Enrollment_PreK12; 
                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void SetUpHorizChart()
        {
            try
            {
                
                hrzBarChart.SelectedSortBySecondarySort = false;

                hrzBarChart.Title = DataSetTitle;
                if (GlobalValues.CompareTo.Key == CompareToKeys.Years)
                {
                    hrzBarChart.LabelColumns = new List<String>(new String[]
                        {
                            v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.YearFormatted
                        });
                }
                if (GlobalValues.CompareTo.Key == CompareToKeys.OrgLevel)
                {
                    hrzBarChart.LabelColumns = new List<String>(new String[]
                        {
                            v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.OrgLevelLabel
                        });
                }
                if (GlobalValues.CompareTo.Key == CompareToKeys.SelDistricts
                    || GlobalValues.CompareTo.Key == CompareToKeys.SelSchools)
                {
                    hrzBarChart.LabelColumns = new List<String>(new String[]
                        {
                            v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Name
                        });
                }
                if (GlobalValues.CompareTo.Key == CompareToKeys.Current)
                {
                    if (GlobalValues.STYP.Key == STYPKeys.AllTypes)
                    {
                        hrzBarChart.LabelColumns = new List<string>(new String[]
                        {
                            v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.SchooltypeLabel
                        });
                    }
                    else
                    {
                        hrzBarChart.LabelColumns = new List<string>(new String[]
                        {
                            v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.OrgLevelLabel
                        });
                    }
                }

                if (GlobalValues.Group.Key == GroupKeys.Gender)
                {
                    hrzBarChart.MeasureColumns.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.FemalePercent);
                    hrzBarChart.MeasureColumns.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.MalePercent);
                    hrzBarChart.OverrideSeriesLabels = new Hashtable(2);
                    hrzBarChart.OverrideSeriesLabels.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.FemalePercent, "% Female");
                    hrzBarChart.OverrideSeriesLabels.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.MalePercent, "% Male");

                }
                if (GlobalValues.Group.Key == GroupKeys.Race)
                {
                    hrzBarChart.MeasureColumns.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.AmIndPercent);
                    hrzBarChart.MeasureColumns.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.AsianPercent);
                    hrzBarChart.MeasureColumns.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.BlackPercent);
                    hrzBarChart.MeasureColumns.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.HispPercent);
                    hrzBarChart.MeasureColumns.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.PacIPercent);
                    hrzBarChart.MeasureColumns.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.WhitePercent);
                    hrzBarChart.MeasureColumns.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.TwoPercent);
                    hrzBarChart.OverrideSeriesLabels = new Hashtable(5);
                    hrzBarChart.OverrideSeriesLabels.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.AmIndPercent, "% Amer Indian");
                    hrzBarChart.OverrideSeriesLabels.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.AsianPercent, "% Asian");
                    hrzBarChart.OverrideSeriesLabels.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.BlackPercent, "% Black");
                    hrzBarChart.OverrideSeriesLabels.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.HispPercent, "% Hispanic");
                    hrzBarChart.OverrideSeriesLabels.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.PacIPercent, "% Pacific Isle");
                    hrzBarChart.OverrideSeriesLabels.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.WhitePercent, "% White");
                    hrzBarChart.OverrideSeriesLabels.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.TwoPercent, "% Two or More");
                
                }

                if (GlobalValues.Group.Key == GroupKeys.Grade)
                {
                    hrzBarChart.MeasureColumns.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.PreKPercent);
                    hrzBarChart.MeasureColumns.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.KinderPercent);
                    hrzBarChart.MeasureColumns.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade1Percent);
                    hrzBarChart.MeasureColumns.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade2Percent);
                    hrzBarChart.MeasureColumns.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade3Percent);
                    hrzBarChart.MeasureColumns.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade4Percent);
                    hrzBarChart.MeasureColumns.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade5Percent);
                    hrzBarChart.MeasureColumns.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade6Percent);
                    hrzBarChart.MeasureColumns.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade7Percent);
                    hrzBarChart.MeasureColumns.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade8Percent);
                    hrzBarChart.MeasureColumns.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade9Percent);
                    hrzBarChart.MeasureColumns.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade10Percent);
                    hrzBarChart.MeasureColumns.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade11Percent);
                    hrzBarChart.MeasureColumns.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade12Percent);
                    hrzBarChart.OverrideSeriesLabels = new Hashtable(14);
                    hrzBarChart.OverrideSeriesLabels.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.PreKPercent, "% Pre-K");
                    hrzBarChart.OverrideSeriesLabels.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.KinderPercent, "% Kinder.");
                    hrzBarChart.OverrideSeriesLabels.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade1Percent, "% Grade 1");
                    hrzBarChart.OverrideSeriesLabels.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade2Percent, "% Grade 2");
                    hrzBarChart.OverrideSeriesLabels.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade3Percent, "% Grade 3");
                    hrzBarChart.OverrideSeriesLabels.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade4Percent, "% Grade 4");
                    hrzBarChart.OverrideSeriesLabels.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade5Percent, "% Grade 5");
                    hrzBarChart.OverrideSeriesLabels.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade6Percent, "% Grade 6");
                    hrzBarChart.OverrideSeriesLabels.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade7Percent, "% Grade 7");
                    hrzBarChart.OverrideSeriesLabels.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade8Percent, "% Grade 8");
                    hrzBarChart.OverrideSeriesLabels.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade9Percent, "% Grade 9");
                    hrzBarChart.OverrideSeriesLabels.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade10Percent, "% Grade 10");
                    hrzBarChart.OverrideSeriesLabels.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade11Percent, "% Grade 11");
                    hrzBarChart.OverrideSeriesLabels.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade12Percent, "% Grade 12");
 

                }

                if (GlobalValues.Group.Key == GroupKeys.Disability)
                {
                    hrzBarChart.MeasureColumns.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.DisabledPercent);
                    hrzBarChart.MeasureColumns.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.NonDisabledPercent);
                    hrzBarChart.OverrideSeriesLabels = new Hashtable(2);
                    hrzBarChart.OverrideSeriesLabels.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.DisabledPercent, "% With Disabilities");
                    hrzBarChart.OverrideSeriesLabels.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.NonDisabledPercent, "% Without Disabilities");
                }

                if (GlobalValues.Group.Key == GroupKeys.EconDisadv)
                {
                    hrzBarChart.MeasureColumns.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.EconDisadvPercent);
                    hrzBarChart.MeasureColumns.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.NonEconDisadvPercent);
                    hrzBarChart.OverrideSeriesLabels = new Hashtable(2);
                    hrzBarChart.OverrideSeriesLabels.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.EconDisadvPercent, "% Econ Disadv");
                    hrzBarChart.OverrideSeriesLabels.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.NonEconDisadvPercent, "% Not Econ Disadv or No Data");
                }

                if (GlobalValues.Group.Key == GroupKeys.EngLangProf)
                {
                    hrzBarChart.MeasureColumns.Add(v_LEPSchoolDistState.Percent_LEP_Spanish);
                    hrzBarChart.MeasureColumns.Add(v_LEPSchoolDistState.Percent_LEP_Hmong);
                    hrzBarChart.MeasureColumns.Add(v_LEPSchoolDistState.Percent_LEP_Other);
                    hrzBarChart.MeasureColumns.Add(v_LEPSchoolDistState.Percent_English_Proficient);
                    hrzBarChart.OverrideSeriesLabels = new Hashtable(4);
                    hrzBarChart.OverrideSeriesLabels.Add(v_LEPSchoolDistState.Percent_LEP_Spanish, "% LEP Spanish");
                    hrzBarChart.OverrideSeriesLabels.Add(v_LEPSchoolDistState.Percent_LEP_Hmong, "% LEP Hmong");
                    hrzBarChart.OverrideSeriesLabels.Add(v_LEPSchoolDistState.Percent_LEP_Other, "% LEP Other");
                    hrzBarChart.OverrideSeriesLabels.Add(v_LEPSchoolDistState.Percent_English_Proficient, "% English Proficient");
                }
                
                hrzBarChart.AxisYDescription = "Percent of Students Enrolled";
                hrzBarChart.YAxisSuffix = "\\%";
            }
            catch (Exception ex)
            {
                if (GlobalValues.TraceLevels > 0) throw ex;
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private double GetMaxRateInResult(DataSet ds)
        {
            double maxRateInResult = 0;

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                try
                {
                    if (Convert.ToDouble(row[v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Total_Enrollment_PreK12].ToString())
                                        > maxRateInResult)
                    {
                        maxRateInResult = Convert.ToDouble(row[v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Total_Enrollment_PreK12].ToString());
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
        public override List<string> GetVisibleColumns(Group viewBy, OrgLevel orgLevel, CompareTo compareTo, STYP schoolType)
        {
            List<string> cols = base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);

            cols.Remove(WebSupportingClasses.ColumnPicker.CommonNames.SexLabel.ToString());
            cols.Remove(WebSupportingClasses.ColumnPicker.CommonNames.RaceLabel.ToString());
            cols.Remove(WebSupportingClasses.ColumnPicker.CommonNames.GradeLabel.ToString());
            cols.Remove(WebSupportingClasses.ColumnPicker.CommonNames.DisabilityLabel.ToString());
            cols.Remove(WebSupportingClasses.ColumnPicker.CommonNames.EconDisadvLabel.ToString());
            cols.Remove(WebSupportingClasses.ColumnPicker.CommonNames.ELPLabel.ToString());

            cols.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Total_Enrollment_PreK12);

            if (GlobalValues.Group.Key == GroupKeys.Gender)
            {
                cols.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.FemalePercent);
                cols.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.MalePercent);
            }
            if (GlobalValues.Group.Key == GroupKeys.Race)
            {
                cols.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.AmIndPercent);
                cols.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.AsianPercent);
                cols.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.BlackPercent);
                cols.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.HispPercent);
                cols.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.PacIPercent);
                cols.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.WhitePercent);
                cols.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.TwoPercent);
            }

            if (GlobalValues.Group.Key == GroupKeys.Grade)
            {
                cols.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.PreKPercent);
                cols.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.KinderPercent);
                cols.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade1Percent);
                cols.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade2Percent);
                cols.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade3Percent);
                cols.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade4Percent);
                cols.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade5Percent);
                cols.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade6Percent);
                cols.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade7Percent);
                cols.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade8Percent);
                cols.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade9Percent);
                cols.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade10Percent);
                cols.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade11Percent);
                cols.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade12Percent);
            }

            if (GlobalValues.Group.Key == GroupKeys.Disability)
            {
                cols.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.DisabledPercent);
                cols.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.NonDisabledPercent);
            }

            if (GlobalValues.Group.Key == GroupKeys.EconDisadv)
            {
                cols.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.EconDisadvPercent);
                cols.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.NonEconDisadvPercent);
            }

            if (GlobalValues.Group.Key == GroupKeys.EngLangProf)
            {
                cols.Add(v_LEPSchoolDistState.Percent_LEP_Spanish);
                cols.Add(v_LEPSchoolDistState.Percent_LEP_Hmong);
                cols.Add(v_LEPSchoolDistState.Percent_LEP_Other);
                cols.Add(v_LEPSchoolDistState.Percent_English_Proficient);
            }
                
            return cols;
        }
        
        protected void CompareToLinkHandler(HyperLinkPlus theLink)
        {
            //example event handler;
        }

        protected override List<string> GetDownloadRawVisibleColumns()
        {
            List<string> cols = base.GetDownloadRawVisibleColumns();

            // not sure why this column is showing up in Download Files - removing it 
            cols.Remove(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.SexLabel);

            int index; 

            if (GlobalValues.Group.Key == GroupKeys.Gender)
            {
                index = cols.IndexOf(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.FemalePercent);
                cols.Insert(index, v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.FemaleCount);
                index = cols.IndexOf(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.MalePercent);
                cols.Insert(index, v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.MaleCount);
            }
            if (GlobalValues.Group.Key == GroupKeys.Race)
            {
                index = cols.IndexOf(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.AmIndPercent);
                cols.Insert(index, v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.AmIndCount);
                index = cols.IndexOf(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.AsianPercent);
                cols.Insert(index, v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.AsianCount);
                index = cols.IndexOf(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.BlackPercent);
                cols.Insert(index, v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.BlackCount);
                index = cols.IndexOf(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.HispPercent);
                cols.Insert(index, v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.HispCount);
                index = cols.IndexOf(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.PacIPercent);
                cols.Insert(index, v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.PacICount);
                index = cols.IndexOf(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.WhitePercent);
                cols.Insert(index, v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.WhiteCount);
                index = cols.IndexOf(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.TwoPercent);
                cols.Insert(index, v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.TwoCount);
                
            }

            if (GlobalValues.Group.Key == GroupKeys.Grade)
            {
                index = cols.IndexOf(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.PreKPercent);
                cols.Insert(index, v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.PreKCount);
                index = cols.IndexOf(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.KinderPercent);
                cols.Insert(index, v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.KinderCount);
                index = cols.IndexOf(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade1Percent);
                cols.Insert(index, v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade1Count);
                index = cols.IndexOf(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade2Percent);
                cols.Insert(index, v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade2Count);
                index = cols.IndexOf(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade3Percent);
                cols.Insert(index, v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade3Count);
                index = cols.IndexOf(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade4Percent);
                cols.Insert(index, v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade4Count);
                index = cols.IndexOf(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade5Percent);
                cols.Insert(index, v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade5Count);
                index = cols.IndexOf(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade6Percent);
                cols.Insert(index, v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade6Count);
                index = cols.IndexOf(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade7Percent);
                cols.Insert(index, v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade7Count);
                index = cols.IndexOf(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade8Percent);
                cols.Insert(index, v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade8Count);
                index = cols.IndexOf(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade9Percent);
                cols.Insert(index, v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade9Count);
                index = cols.IndexOf(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade10Percent);
                cols.Insert(index, v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade10Count);
                index = cols.IndexOf(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade11Percent);
                cols.Insert(index, v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade11Count);
                index = cols.IndexOf(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade12Percent);
                cols.Insert(index, v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.Grade12Count);
            }

            if (GlobalValues.Group.Key == GroupKeys.Disability)
            {
                index = cols.IndexOf(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.DisabledPercent);
                cols.Insert(index, v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.DisabledCount);
                index = cols.IndexOf(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.NonDisabledPercent);
                cols.Insert(index, v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.NonDisabledCount);
            }

            if (GlobalValues.Group.Key == GroupKeys.EconDisadv)
            {
                index = cols.IndexOf(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.EconDisadvPercent);
                cols.Insert(index, v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.EconDisadvCount);
                index = cols.IndexOf(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.NonEconDisadvPercent);
                cols.Insert(index, v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.NonEconDisadvCount);
            }

            if (GlobalValues.Group.Key == GroupKeys.EngLangProf)
            {
                index = cols.IndexOf(v_LEPSchoolDistState.Percent_LEP_Spanish);
                cols.Insert(index, v_LEPSchoolDistState.Number_LEP_Spanish);
                index = cols.IndexOf(v_LEPSchoolDistState.Percent_LEP_Hmong);
                cols.Insert(index, v_LEPSchoolDistState.Number_LEP_Hmong);
                index = cols.IndexOf(v_LEPSchoolDistState.Percent_LEP_Other);
                cols.Insert(index, v_LEPSchoolDistState.Number_LEP_Other);
                index = cols.IndexOf(v_LEPSchoolDistState.Percent_English_Proficient);
                cols.Insert(index, v_LEPSchoolDistState.Number_English_Proficient);
            }

            return cols;
        }
        protected override SortedList<String, String> GetDownloadRawColumnLabelMapping()
        {
            SortedList<String, String> map =  base.GetDownloadRawColumnLabelMapping();

            if (GlobalValues.Group.Key == GroupKeys.Race)
            {
                map.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.AmIndPercent, "amer_indian_percent");
                map.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.AmIndCount, "amer_indian_count");
                map.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.AsianPercent, "asian_percent");
                map.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.AsianCount, "asian_count");
                map.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.BlackPercent, "black_percent");
                map.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.BlackCount, "black_count");
                map.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.HispPercent, "hisp_percent");
                map.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.HispCount, "hisp_count");
                map.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.PacIPercent, "pac_isle_percent");
                map.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.PacICount, "pac_isle_count");
                map.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.WhitePercent, "white_percent");
                map.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.WhiteCount, "white_count");
                map.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.TwoPercent, "two_or_more_percent");
                map.Add(v_Template_Keys_WWoDisEconELP_tblAgencyFull_Flat.TwoCount, "two_or_more_count");

            }
            return map;
        }
    }
}
