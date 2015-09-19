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

using SligoCS.DAL.WI;
using SligoCS.BL.WI;
using SligoCS.Web.Base.PageBase.WI;

using SligoCS.Web.WI.WebSupportingClasses.WI;
using SligoCS.Web.WI.WebSupportingClasses;

namespace SligoCS.Web.WI
{

    /// <summary>
    /// This page is also known as the TeacherQualificationsScatterPlot page.
    /// </summary>
    public partial class TeacherQualificationsScatterPlot : PageBaseWI
    {
        string relateToLabel = string.Empty;
        string relateToDenominatorLabel = string.Empty;

        protected override void OnInit(EventArgs e)
        {
            OnCheckPrerequisites += new CheckPrerequisitesHandler(TeacherQualificationsScatterPlot_OnCheckPrerequisites);
            base.OnInit(e);
        }

        void TeacherQualificationsScatterPlot_OnCheckPrerequisites(PageBaseWI page, EventArgs args)
        {
            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.State) return;
                
            if (FullKeyUtils.GetOrgLevelFromFullKeyX(GlobalValues.FULLKEY).Key == OrgLevelKeys.State)
                OnRedirectUser += InitialAgencyRedirect;
        }
        protected override DALWIBase InitDatabase()
        {
            return new DALTeacherQualificationsScatterPlot();
        }
       protected override ChartFX.WebForms.Chart InitGraph()
        {
            return scatterplot;
        }
        protected override GridView InitDataGrid()
        {
            return TQScatterDataGrid;
        }
        protected override string SetPageHeading()
        {
            return "What are the qualifications of teachers?";
        }
        protected override void OnInitComplete(EventArgs e)
        {
            GlobalValues.CompareTo.Key = CompareToKeys.Current;
            GlobalValues.Group.Key = GroupKeys.All;
            GlobalValues.LatestYear = 2010;

            //disable state level
            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.State) GlobalValues.OrgLevel.Key = OrgLevelKeys.District;

            base.OnInitComplete(e);
            TQScatterDataGrid.ColumnLoaded += RenameRelatedToColumn;
            TQScatterDataGrid.ColumnLoaded += new EventHandler(SetFormatString_ColumnLoaded);
            
            nlrRelateTo.LinkControlAdded += new LinkControlAddedHandler(DisableSchoolSize_LinkControlAdded);
            if (GlobalValues.OrgLevel.Key != OrgLevelKeys.School
                && GlobalValues.TQRelateTo.Key == TQRelateToKeys.SchoolSize)
                GlobalValues.TQRelateTo.Key = TQRelateToKeys.DistrictSize;

            //overide until 2011 refresh and new races are added to data.
            nlrRelateTo.LinkControlAdded += new LinkControlAddedHandler(disableNewRaces_LinkControlAdded);
            
        }

        void disableNewRaces_LinkControlAdded(SligoCS.Web.Base.WebServerControls.WI.HyperLinkPlus link)
        {
            // this routine may safely be deleted once GlobalValues.Year is incremented to 2011
            if (GlobalValues.Year > 2010) return;

            if (link.ID == "linkTQRelateToPacific"
                || link.ID == "linkTQRelateToTwoPlusRaces")
                link.Enabled = false;
        }

        void DisableSchoolSize_LinkControlAdded(SligoCS.Web.Base.WebServerControls.WI.HyperLinkPlus link)
        {
            if (GlobalValues.OrgLevel.Key != OrgLevelKeys.School
                && link.ID == "linkTQRelateToSchoolSize") 
                    link.Enabled = false;
        }
        protected override void OnPreRender(EventArgs e)
        {
            Page.Master.FindControl("linkState").Visible = false;
            base.OnPreRender(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSetTitle = GetTitleWithoutGroup("Teacher Qualifications" + " - " +
                GlobalValues.TQSubjectsSP.Key.Replace("_", " ") + TitleBuilder.newline +
                GlobalValues.TQTeacherVariable.Key + " vs. " + GlobalValues.TQRelateTo.Key)
                .Replace(TitleBuilder.GetYearRangeInTitle(QueryMarshaller.years),
                    TitleBuilder.GetYearRangeInTitle(QueryMarshaller.years)
                    + " Compared to " +
                    ((GlobalValues.OrgLevel.Key == OrgLevelKeys.District) ?
                    "All Districts in " :
                    "Schools in ") + 
                    ((GlobalValues.TQLocation.Key == TQLocationKeys.CESA) ? "CESA " + GlobalValues.Agency.CESA : 
                    (GlobalValues.TQLocation.Key == TQLocationKeys.County) ? GlobalValues.Agency.CountyName :
                    GlobalValues.TQLocation.Key));

            TQScatterDataGrid.AddSuperHeader(DataSetTitle);

            TQScatterDataGrid.ForceCurrentFloatToTopOrdering = true;

            //Add a 2nd Super Header if Show = Wisconsin State License.
            TQScatterDataGrid.AddSuperHeader(GetSecondSuperHeader());
            
            set_state();
            setBottomLink();

            SetUpChart(DataSet);
        }

        /// <summary>
        ///  Set up graph 
        /// </summary>
        /// <param name="ds"></param>
        private void SetUpChart(DataSet ds)
        {
            try
            {
                scatterplot.Title = DataSetTitle;

                #region X-Axis settings

                TQRelateTo relatedTo = GlobalValues.TQRelateTo;
                string xValueColumnName = string.Empty;
                int xMin = 0;
                int xMax = 100;
                int xStep = 10;
                string axisXDescription = string.Empty;
                int maxValueInResult = 0;

                if (relatedTo.CompareToKey(TQRelateToKeys.DistrictSize) ||
                    relatedTo.CompareToKey(TQRelateToKeys.SchoolSize))
                {
                    xValueColumnName = v_TeacherQualificationsScatterplot.RelateToDenominator;
                }
                else
                {
                    xValueColumnName = v_TeacherQualificationsScatterplot.RelateToValue;
                }

                if (relatedTo.CompareToKey(TQRelateToKeys.DistrictSize)||
                    relatedTo.CompareToKey(TQRelateToKeys.SchoolSize)||
                    relatedTo.CompareToKey(TQRelateToKeys.Spending))
                {
                    foreach (DataRow row in DataSet.Tables[0].Rows)
                    {
                        try
                        {
                            if (maxValueInResult < (int)Convert.ToDouble(row[xValueColumnName].ToString().Trim()))
                            {
                                maxValueInResult = (int)Convert.ToDouble(row[xValueColumnName].ToString().Trim());
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
               

                if  (relatedTo.CompareToKey(TQRelateToKeys.Spending))
                {
                        axisXDescription = "District Current Education Cost Per Member";
                        xMax = maxValueInResult + 1; 
                        xStep = 2000;
                        scatterplot.AxisX.LabelsFormat.Format = ChartFX.WebForms.AxisFormat.Currency;
                }  
                else if (relatedTo.CompareToKey(TQRelateToKeys.DistrictSize))
                {
                        axisXDescription = "District Size -- Number of Students Enrolled";
                        xMax = maxValueInResult + 1;
                        xStep = 10000;
                        scatterplot.AxisX.LabelsFormat.Format = ChartFX.WebForms.AxisFormat.Number;
                }
                else if (relatedTo.CompareToKey(TQRelateToKeys.SchoolSize))
                {
                        axisXDescription = "School Size -- Number of Students Enrolled";
                        xMax = maxValueInResult + 1;
                        xStep = 200;
                        scatterplot.AxisX.LabelsFormat.Format = ChartFX.WebForms.AxisFormat.Number;
                } 
                else
                {
                    axisXDescription = relatedTo.Key.Replace("&#37;", "Percent of Students Who Are");
                    scatterplot.AxisX.LabelsFormat.CustomFormat = "0" + "\\%";
                }
                
                scatterplot.XValueColumnName = xValueColumnName;
                scatterplot.AxisXMin = xMin;
                scatterplot.AxisXMax = xMax;
                scatterplot.AxisXStep = xStep;
                scatterplot.AxisXDescription = axisXDescription;
                #endregion

                #region Y-Axis settings

                string yValueColumnName = string.Empty;
                TQTeacherVariable teacherVariable = base.GlobalValues.TQTeacherVariable;
                if (teacherVariable.CompareToKey( TQTeacherVariableKeys.WiscLicense ) )
                {
                    yValueColumnName = v_TeacherQualificationsScatterplot.LicenseFullFTEPercentage;
                }
                else if (teacherVariable.CompareToKey(TQTeacherVariableKeys.EmergencyLic))
                {
                    yValueColumnName = v_TeacherQualificationsScatterplot.LicenseEmerFTEPercentage;
                }

                else if (teacherVariable.CompareToKey(TQTeacherVariableKeys.NoLicense))
                {
                    yValueColumnName = v_TeacherQualificationsScatterplot.LicenseNoFTEPercentage;
                }

                else if (teacherVariable.CompareToKey(TQTeacherVariableKeys.DistrictExp))
                {
                    yValueColumnName = v_TeacherQualificationsScatterplot.LocalExperience5YearsOrMoreFTEPercentage;
                }

                else if (teacherVariable.CompareToKey(TQTeacherVariableKeys.TotalExp))
                {
                    yValueColumnName = v_TeacherQualificationsScatterplot.TotalExperience5YearsOrMoreFTEPercentage;
                }

                else if (teacherVariable.CompareToKey(TQTeacherVariableKeys.Degree))
                {
                    yValueColumnName = v_TeacherQualificationsScatterplot.DegreeMastersOrHigherFTEPercentage;
                }

                else if (teacherVariable.CompareToKey(TQTeacherVariableKeys.ESEA))
                {
                    yValueColumnName =v_TeacherQualificationsScatterplot.EHQYesFTEPercentage;
                }
                scatterplot.YValueColumnName = yValueColumnName;
                scatterplot.AxisYMin = 0;
                scatterplot.AxisYMax = 100;
                scatterplot.AxisYStep = 10;
                scatterplot.AxisYDescription = "Percent of FTE Teachers With \n" + GlobalValues.TQTeacherVariable.Key.Replace("&#37;", String.Empty);
                scatterplot.AxisY.LabelsFormat.CustomFormat = "0" + "\\%";

                #endregion

                ArrayList friendlySeriesName = new ArrayList();
                if (GlobalValues.OrgLevel.Key == OrgLevelKeys.School)
                {
                    friendlySeriesName.Add("Current School");
                    friendlySeriesName.Add("Other Schools");
                }
                else //HT: district level. There is no state level in live page. To check if we should show state level?
                {
                    friendlySeriesName.Add("Current District");

                    friendlySeriesName.Add("Other Districts");
                }
                scatterplot.FriendlySeriesName = friendlySeriesName;
                if ((GlobalValues.TQRelateTo.Key == TQRelateToKeys.DistrictSize) && ((SligoCS.Web.WI.WebUserControls.GraphBarChart.GetMaxRateInColumn(DataSet.Tables[0], v_TeacherQualificationsScatterplot.RelateToDenominator)) < 20000))
                {
                    scatterplot.AxisX.Step = 2000;
                }

                //scatterplot.DataSource = DataSet.Tables[0];
                //scatterplot.DataBind();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        private TableRow GetSecondSuperHeader()
        {
            TableRow tr = new TableRow();
            TQRelateTo relateTo = GlobalValues.TQRelateTo;

            string org = (GlobalValues.OrgLevel.Key == OrgLevelKeys.School) ? "School" : "District";

            string schoolType = TitleBuilder.GetSchoolTypeInTitle(GlobalValues.STYP);

            if (relateTo.CompareToKey(TQRelateToKeys.Spending))
            {
                WinssDataGrid.AddTableCell(tr, org, 1);
                WinssDataGrid.AddTableCell(tr, "District Spending", 1);
                WinssDataGrid.AddTableCell(tr, "Teacher Data: " + schoolType, 3);

                //relateToDenominatorLabel = "Current Education Cost Per Member"; 
                relateToLabel = "Current Education Cost Per Member"; 
           
            }
            else if (relateTo.CompareToKey(TQRelateToKeys.DistrictSize))
            {
                WinssDataGrid.AddTableCell(tr, org, 1);
                WinssDataGrid.AddTableCell(tr, "District Size", 1);
                WinssDataGrid.AddTableCell(tr, "Teacher Data: " + schoolType, 3);

                relateToDenominatorLabel = "Total Number Enrolled";            
            }
            else if (relateTo.CompareToKey(TQRelateToKeys.SchoolSize))
            {
                WinssDataGrid.AddTableCell(tr, org, 1);
                WinssDataGrid.AddTableCell(tr, "School Size: " + schoolType, 1);
                WinssDataGrid.AddTableCell(tr, "Teacher Data: " + schoolType, 3);

                relateToDenominatorLabel = "Total Number Enrolled";
            }
            else
            {
                WinssDataGrid.AddTableCell(tr, org, 1);
                WinssDataGrid.AddTableCell(tr, "Student Data:" + schoolType, 2);
                WinssDataGrid.AddTableCell(tr, "Teacher Data:" + schoolType, 3);

                relateToDenominatorLabel = "Total # Enrolled (PreK-12)**";
            }
            
            foreach(  TableCell cell in tr.Cells) 
            {
                cell.BorderColor = System.Drawing.Color.Gray;
                cell.BorderStyle = BorderStyle.Double;
                cell.BorderWidth = 4;
            }

            return tr;
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

        public override List<string>  GetVisibleColumns(Group viewBy, OrgLevel orgLevel, CompareTo compareTo, STYP schoolType)
        {
            List<string> cols = base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);

            TQRelateTo relatedTo = GlobalValues.TQRelateTo;
            TQTeacherVariable teacherVariable = GlobalValues.TQTeacherVariable;

            //doesn't follow other pages pattern
            cols.Clear();
            // Always display this column
            cols.Add(v_TeacherQualificationsScatterplot.LinkedName);

            if (relatedTo.CompareToKey(TQRelateToKeys.Spending))
            {
                cols.Add(v_TeacherQualificationsScatterplot.RelateToValue);
            }

            else if (relatedTo.CompareToKey(TQRelateToKeys.DistrictSize) ||
                                    relatedTo.CompareToKey(TQRelateToKeys.SchoolSize))
            {
                cols.Add(v_TeacherQualificationsScatterplot.RelateToDenominator);
            }
            else
            {
                cols.Add(v_TeacherQualificationsScatterplot.RelateToDenominator);
                cols.Add(v_TeacherQualificationsScatterplot.RelateToValue);
            }

            cols.Add(v_TeacherQualificationsScatterplot.FTETotal);

            // Teacher Variable column to display

            if (teacherVariable.CompareToKey(TQTeacherVariableKeys.WiscLicense))
            {
                cols.Add(
                    v_TeacherQualificationsScatterplot.FTELicenseFull);
                cols.Add(
                    v_TeacherQualificationsScatterplot.LicenseFullFTEPercentage);
            }
            else if (teacherVariable.CompareToKey(TQTeacherVariableKeys.EmergencyLic))
            {
                cols.Add(
                    v_TeacherQualificationsScatterplot.LicenseEmerFTE);
                cols.Add(
                    v_TeacherQualificationsScatterplot.LicenseEmerFTEPercentage);
            }

            else if (teacherVariable.CompareToKey(TQTeacherVariableKeys.NoLicense))
            {
                cols.Add(v_TeacherQualificationsScatterplot.LicenseNoFTE);
                cols.Add(v_TeacherQualificationsScatterplot.LicenseNoFTEPercentage);
            }

            else if (teacherVariable.CompareToKey(TQTeacherVariableKeys.DistrictExp))
            {
                cols.Add(v_TeacherQualificationsScatterplot.LocalExperience5YearsOrMoreFTE);
                cols.Add(v_TeacherQualificationsScatterplot.LocalExperience5YearsOrMoreFTEPercentage);
            }

            else if (teacherVariable.CompareToKey(TQTeacherVariableKeys.TotalExp))
            {
                cols.Add(v_TeacherQualificationsScatterplot.TotalExperience5YearsOrMoreFTE);
                cols.Add(v_TeacherQualificationsScatterplot.TotalExperience5YearsOrMoreFTEPercentage);
            }

            else if (teacherVariable.CompareToKey(TQTeacherVariableKeys.Degree))
            {
                cols.Add(v_TeacherQualificationsScatterplot.DegreeMastersOrHigherFTE);
                cols.Add(v_TeacherQualificationsScatterplot.DegreeMastersOrHigherFTEPercentage);
            }

            else if (teacherVariable.CompareToKey(TQTeacherVariableKeys.ESEA))
            {
                cols.Add(v_TeacherQualificationsScatterplot.EHQYesFTE);
                cols.Add(v_TeacherQualificationsScatterplot.EHQYesFTEPercentage);
            }

            return cols;
        }
        private void RenameRelatedToColumn(Object sender, EventArgs args)
        {
            DataControlField column = (DataControlField)sender;
            if (column.HeaderText == "RelateToValue") 
                if (GlobalValues.TQRelateTo.Key == TQRelateToKeys.Spending) 
                    column.HeaderText = "Current Education Cost Per Member";
                else column.HeaderText = GlobalValues.TQRelateTo.Key;
        }

        void SetFormatString_ColumnLoaded(object sender, EventArgs e)
        {
            WinssDataGridColumn column = (WinssDataGridColumn)sender;
            if (column.DataField == v_TeacherQualificationsScatterplot.RelateToValue) 
                if (GlobalValues.TQRelateTo.Key == TQRelateToKeys.Spending)
                    column.FormatString = "$#,##0";
                else if (GlobalValues.TQRelateTo.Key == TQRelateToKeys.DistrictSize || GlobalValues.TQRelateTo.Key == TQRelateToKeys.SchoolSize)
                    column.FormatString = "#,##0";
                else 
                    column.FormatString = "#,##0.0%";
        }

        protected override List<string> GetDownloadRawVisibleColumns()
        {
            List<string> cols = base.GetDownloadRawVisibleColumns();
            TQRelateTo relatedTo = GlobalValues.TQRelateTo;
            
            int index;
            
            if (!(relatedTo.CompareToKey(TQRelateToKeys.DistrictSize)
                || relatedTo.CompareToKey(TQRelateToKeys.SchoolSize) 
                || relatedTo.CompareToKey(TQRelateToKeys.Spending)))
            {
                index = cols.IndexOf(v_TeacherQualificationsScatterplot.RelateToValue);
                cols.Insert(index, v_TeacherQualificationsScatterplot.RelateToNumerator);
            }

            return cols;
        }

        protected override SortedList<string, string> GetDownloadRawColumnLabelMapping()
        {
            SortedList<string, string> newLabels = base.GetDownloadRawColumnLabelMapping();
            if (GlobalValues.TQRelateTo.Key == TQRelateToKeys.Spending)
                newLabels.Add(v_TeacherQualificationsScatterplot.RelateToValue, "district_spending_current_education_cost_per_member");
            else if (GlobalValues.TQRelateTo.Key == TQRelateToKeys.DistrictSize)
            {
                newLabels.Add(v_TeacherQualificationsScatterplot.RelateToDenominator, "district_size_total_fall_enrollment_prek-12");
                newLabels.Add(v_TeacherQualificationsScatterplot.RelateToValue, GlobalValues.TQRelateTo.Key.Replace("&#37; ", "percent_"));
                newLabels.Add(v_TeacherQualificationsScatterplot.RelateToNumerator, GlobalValues.TQRelateTo.Key.Replace("&#37; ", "number_"));
            }
            else
            {
                newLabels.Add(v_TeacherQualificationsScatterplot.RelateToDenominator, "total_fall_enrollment_prek-12");
                newLabels.Add(v_TeacherQualificationsScatterplot.RelateToValue, GlobalValues.TQRelateTo.Key.Replace("&#37; ", "percent_"));
                newLabels.Add(v_TeacherQualificationsScatterplot.RelateToNumerator, GlobalValues.TQRelateTo.Key.Replace("&#37; ", "number_"));
            }
newLabels.Add(v_TeacherQualificationsScatterplot.FTETotal, "total_number_of_fte_teachers");
                newLabels.Add(v_TeacherQualificationsScatterplot.FTELicenseFull, "fte_with_full_wisconsin_license");
                newLabels.Add(v_TeacherQualificationsScatterplot.LicenseFullFTEPercentage, "percent_full_wisconsin_license");
                newLabels.Add(v_TeacherQualificationsScatterplot.LicenseEmerFTE, "fte_with_emergency_wisconsin_license");
                newLabels.Add(v_TeacherQualificationsScatterplot.LicenseEmerFTEPercentage, "percent_emergency_wisconsin_license");
                newLabels.Add(v_TeacherQualificationsScatterplot.LicenseNoFTE, "fte_with_no_wisconsin_license");
                newLabels.Add(v_TeacherQualificationsScatterplot.LicenseNoFTEPercentage, "percent_no_wisconsin_license");
                newLabels.Add(v_TeacherQualificationsScatterplot.LocalExperience5YearsOrMoreFTE, "fte_with_5_or_more_years_district_experience");
                newLabels.Add(v_TeacherQualificationsScatterplot.LocalExperience5YearsOrMoreFTEPercentage, "percent_with_5_or_more_years_district_experience");
                newLabels.Add(v_TeacherQualificationsScatterplot.TotalExperience5YearsOrMoreFTE, "fte_with_5_or_more_years_total_experience");
                newLabels.Add(v_TeacherQualificationsScatterplot.TotalExperience5YearsOrMoreFTEPercentage, "percent_with_5_or_more_years_total_experience");
                newLabels.Add(v_TeacherQualificationsScatterplot.DegreeMastersOrHigherFTE, "fte_with_masters_or_higher_degree");
                newLabels.Add(v_TeacherQualificationsScatterplot.DegreeMastersOrHigherFTEPercentage, "percent_with_masters_or_higher_degree");
                newLabels.Add(v_TeacherQualificationsScatterplot.EHQYesFTE, "fte_with_esea_qualified");
                newLabels.Add(v_TeacherQualificationsScatterplot.EHQYesFTEPercentage, "percent_esea_qualified");
                return newLabels;
        }
    }
}
