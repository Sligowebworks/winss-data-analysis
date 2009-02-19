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
using SligoCS.DAL.WI.DataSets;
using SligoCS.BL.WI;
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.WI.HelperClasses;
//using SligoCS.Web.Base.WebSupportingClasses.WI;

namespace SligoCS.Web.WI
{

    /// <summary>
    /// This page is also known as the TeacherQualificationsScatterPlot page.
    /// </summary>
    public partial class TeacherQualificationsScatterPlot : PageBaseWI
    {
        private string graphTitle = string.Empty;
        string relateToLabel = string.Empty;
        string relateToDenominatorLabel = string.Empty;
        protected v_TeacherQualificationsScatterPlot 
            tqScatterPlotDataSet = new v_TeacherQualificationsScatterPlot ();
        BLTeacherQualificationsScatterPlot tqScatterPlotBL = 
                new BLTeacherQualificationsScatterPlot();
        
        protected void Page_Load(object sender, EventArgs e)
        {

            Page.Master.EnableViewState = false;

            base.PrepBLEntity(tqScatterPlotBL);

            //CheckSelectedSchoolOrDistrict(tqScatterPlotBL);
            //SetLinkChangeSelectedSchoolOrDistrict(
            //    tqScatterPlotBL, ChangeSelectedSchoolOrDistrict);

            //set the page heading
            ((SligoCS.Web.WI.WI)Page.Master).SetPageHeading
                ("What are the qualifications of teachers?");
            

            tqScatterPlotDataSet = 
                tqScatterPlotBL.GetTeacherQualificationsScatterPlot( 
                        base.StickyParameter.TQSubjects,
                        base.StickyParameter.TQTeacherVariable,
                        base.StickyParameter.TQRelatedTo, 
                        base.StickyParameter.TQLocation,
                        base.StickyParameter.COUNTY,
                        getCESA(StickyParameter.FULLKEY)   
                        );
            
            SetVisibleColumns2(SligoDataGrid2, 
                tqScatterPlotDataSet, tqScatterPlotBL.ViewBy, 
                tqScatterPlotBL.OrgLevel, tqScatterPlotBL.CompareTo, 
                tqScatterPlotBL.SchoolType);
            
            StickyParameter.SQL = tqScatterPlotBL.SQL;

            this.SligoDataGrid2.DataSource = tqScatterPlotDataSet;

            graphTitle = getTitle(
                tqScatterPlotBL,
                GetRegionString());

            getRelateToHeaderText(tqScatterPlotDataSet);
            SligoDataGrid2.AddSuperHeader (graphTitle);

            //Add a 2nd Super Header if Show = Wisconsin State License.
            SligoDataGrid2.AddSuperHeader(
                GetSecondSuperHeader());
            
            this.SligoDataGrid2.DataBind();

            set_state();
            setBottomLink(tqScatterPlotBL);

            ////Notes:  graph 
            GraphPanel.Visible = false;
            if (CheckIfGraphPanelVisible(tqScatterPlotBL, GraphPanel) == true)
            {
                SetUpChart(tqScatterPlotDataSet);
            }
        }

        /// <summary>
        ///  Set up graph 
        /// </summary>
        /// <param name="ds"></param>
        private void SetUpChart(v_TeacherQualificationsScatterPlot ds)
        {
            try
            {
                graphTitle = graphTitle.Replace("<br/>", "\n") + "\n";
                scatterplot.Title = graphTitle;

                #region X-Axis settings

                SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.TQRelatedToEnum 
                    relatedTo = base.StickyParameter.GetTQRelatedTo();
                string xValueColumnName = string.Empty;
                int xMin = 0;
                int xMax = 100;
                int xStep = 10;
                string axisXDescription = string.Empty;
                int maxValueInResult = 0;

                if (relatedTo == SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.TQRelatedToEnum.District_Size ||
                    relatedTo == SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.TQRelatedToEnum.School_Size)
                {
                    xValueColumnName = tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.RelateToDenominatorColumn.ColumnName;
                }
                else
                {
                    xValueColumnName = tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.RelateToValueColumn.ColumnName;
                }

                if (relatedTo == SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.TQRelatedToEnum.District_Size ||
                    relatedTo == SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.TQRelatedToEnum.School_Size ||
                    relatedTo == SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.TQRelatedToEnum.District_Spending)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
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
               

                switch (relatedTo)
                {
                    case SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.TQRelatedToEnum.District_Spending:
                        axisXDescription = "District Current Education Cost Per Member";
                        xMax = maxValueInResult + 1; 
                        xStep = 2000;
                        break;
                    case SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.TQRelatedToEnum.District_Size:
                        axisXDescription = "District Size -- Number of Students Enrolled";
                        xMax = maxValueInResult + 1;
                        xStep = 10000;
                        break;
                    case SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.TQRelatedToEnum.School_Size:
                        axisXDescription = "School Size -- Number of Students Enrolled";
                        xMax = maxValueInResult + 1;
                        xStep = 200;
                        break;
                    case SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.TQRelatedToEnum.Percent_Economically_Disadvantaged:
                        axisXDescription = "Percent of Students Who Are Economically Disadvantaged";
                        break;
                    case SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.TQRelatedToEnum.Percent_Limited_English_Proficient:
                        axisXDescription = "Percent of Students Who Are Limited English Proficient";
                        break;
                    case SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.TQRelatedToEnum.Percent_Students_with_Disabilities:
                        axisXDescription = "Percent of Students Who Are Students With Disabilities";
                        break;
                    case SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.TQRelatedToEnum.Percent_Am_Indian:
                        axisXDescription = "Percent of Students Who Are Am Indian";
                        break;
                    case SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.TQRelatedToEnum.Percent_Asian:
                        axisXDescription = "Percent of Students Who Are Asian";
                        break;
                    case SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.TQRelatedToEnum.Percent_Black:
                        axisXDescription = "Percent of Students Who Are Black";
                        break;
                    case SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.TQRelatedToEnum.Percent_Hispanic:
                        axisXDescription = "Percent of Students Who Are Hispanic";
                        break;
                    case SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.TQRelatedToEnum.Percent_White:
                        axisXDescription = "Percent of Students Who Are White";
                        break;
                }
                scatterplot.XValueColumnName = xValueColumnName;
                scatterplot.AxisXMin = xMin;
                scatterplot.AxisXMax = xMax;
                scatterplot.AxisXStep = xStep;
                scatterplot.AxisXDescription = axisXDescription;
                #endregion

                #region Y-Axis settings

                string yValueColumnName = string.Empty;
                SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.TQTeacherVariableEnum teacherVariable = base.StickyParameter.GetTQTeacherVariable();
                if (teacherVariable == SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.TQTeacherVariableEnum.Percent_Full_Wisconsin_License)
                {
                    yValueColumnName = tqScatterPlotDataSet.
                        v_TeacherQualificationsScatterplot.LicenseFullFTEPercentageColumn.ColumnName;
                }
                else if (teacherVariable == SligoCS.Web.Base.WebSupportingClasses.
                    WI.StickyParameter.TQTeacherVariableEnum.Percent_Emergency_Wisconsin_License)
                {
                    yValueColumnName = tqScatterPlotDataSet.
                        v_TeacherQualificationsScatterplot.LicenseEmerFTEPercentageColumn.ColumnName;
                }

                else if (teacherVariable == SligoCS.Web.Base.WebSupportingClasses.
                     WI.StickyParameter.TQTeacherVariableEnum.Percent_No_License_For_Assignment)
                {
                    yValueColumnName = tqScatterPlotDataSet.
                        v_TeacherQualificationsScatterplot.LicenseNoFTEPercentageColumn.ColumnName;
                }

                else if (teacherVariable == SligoCS.Web.Base.WebSupportingClasses.
                    WI.StickyParameter.TQTeacherVariableEnum.Percent_5_or_More_Years_District_Experience)
                {
                    yValueColumnName = tqScatterPlotDataSet.
                        v_TeacherQualificationsScatterplot.LocalExperience5YearsOrMoreFTEPercentageColumn.ColumnName;
                }

                else if (teacherVariable == SligoCS.Web.Base.WebSupportingClasses.
                    WI.StickyParameter.TQTeacherVariableEnum.Percent_5_or_More_Years_Total_Experience)
                {
                    yValueColumnName = tqScatterPlotDataSet.
                        v_TeacherQualificationsScatterplot.TotalExperience5YearsOrMoreFTEPercentageColumn.ColumnName;
                }

                else if (teacherVariable == SligoCS.Web.Base.WebSupportingClasses.
                    WI.StickyParameter.TQTeacherVariableEnum.Percent_Masters_or_Higher_Degree)
                {
                    yValueColumnName = tqScatterPlotDataSet.
                        v_TeacherQualificationsScatterplot.DegreeMastersOrHigherFTEPercentageColumn.ColumnName;
                }

                else if (teacherVariable == SligoCS.Web.Base.WebSupportingClasses.
                    WI.StickyParameter.TQTeacherVariableEnum.Percent_ESEA_Qualified)
                {
                    yValueColumnName = tqScatterPlotDataSet.
                        v_TeacherQualificationsScatterplot.EHQYesFTEPercentageColumn.ColumnName;
                }
                scatterplot.YValueColumnName = yValueColumnName;
                scatterplot.AxisYMin = 0;
                scatterplot.AxisYMax = 100;
                scatterplot.AxisYStep = 10;
                scatterplot.AxisYDescription = "Percent of FTE Teachers With Full \n Wisconsin License";
                #endregion

                ArrayList friendlySeriesName = new ArrayList();
                friendlySeriesName.Add("Current District");
                friendlySeriesName.Add("Other Districts");
                scatterplot.FriendlySeriesName = friendlySeriesName;

                scatterplot.RawDataSource = ds.Tables[0];
                scatterplot.DataBind();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        //getCESA( fullkey ); 
        private string getCESA(string fullkey)
        {
            return fullkey.Substring (0, 2);
        }

        private void getRelateToHeaderText(
            v_TeacherQualificationsScatterPlot tqScatterPlotDataSet)
        {
            // get relateToLabel
            DataRow row = tqScatterPlotDataSet.Tables[0].Rows[0];

            relateToLabel = row[tqScatterPlotDataSet.
                v_TeacherQualificationsScatterplot.
                RelateToLabelColumn.ColumnName].ToString().Trim();
            
            // get relateToDenominatorlabel 
        }

        private string getTitle(
            BLWIBase entity, 
            string locationCode)
        {
            const string DELIM = @"<br/>";
            StringBuilder tmpHeader = new StringBuilder();

            string compareTo = String.Empty;

            SligoCS.Web.Base.WebSupportingClasses.
                WI.StickyParameter.TQLocationEnum location =
                base.StickyParameter.GetTQLocation();


            string title = "Teacher Qualifications" + DELIM +
                base.StickyParameter.
                    GetTQSubjectsTaught().ToString().Replace("_", " ") + DELIM +

                base.GetOrgName(entity.OrgLevel) + ":" +
                GetSchoolTypeInTitle((BLWIBase)entity).Replace(@"<br/> ", " ") + DELIM +
                base.GetYearRangeInTitle(entity.Years)+ " " +
                 GetLocationString()+ 
                 base.GetSchoolTypeInTitle(tqScatterPlotBL);

            return title;
        }

        protected string GetLocationString()
        {
            string result = string.Empty;
            BLAgencyFull agencyBL = new BLAgencyFull();

            SligoCS.Web.Base.WebSupportingClasses.
                WI.StickyParameter.TQLocationEnum location =
                base.StickyParameter.GetTQLocation();

            string org = (tqScatterPlotBL.OrgLevel == OrgLevel.School) ? "School" : "District";

            if (location == SligoCS.Web.Base.WebSupportingClasses.
                WI.StickyParameter.TQLocationEnum.Entire_State)
            {
                if (tqScatterPlotBL.OrgLevel == OrgLevel.School)
                {
                    result = "Compared to All " + base.GetSchoolTypeInTitle(tqScatterPlotBL) 
                        + " in Entire State";
                }
                else
                {
                    result = "Compared to All Districts in Entire State";

                }
            }
            else if (location == SligoCS.Web.Base.WebSupportingClasses.
                WI.StickyParameter.TQLocationEnum.My_County)
            {
                string region = agencyBL.GetCountyNameByID
                    (StickyParameter.COUNTY.ToString()).Trim();
                
                if (tqScatterPlotBL.OrgLevel == OrgLevel.School)
                {
                    result = "Compared to All " + base.GetSchoolTypeInTitle(tqScatterPlotBL) + 
                        " in " + region;
                }
                else
                {
                    result = "Compared to All Districts in " + region;;

                }
            }

            else if (location == SligoCS.Web.Base.WebSupportingClasses.
                WI.StickyParameter.TQLocationEnum.My_CESA)
            {
                string region = 
                    StickyParameter.FULLKEY.Substring (0,2);
                
                if (tqScatterPlotBL.OrgLevel == OrgLevel.School)
                {
                    result = "Compared to All " + base.GetSchoolTypeInTitle(tqScatterPlotBL) + 
                        " in CESA " + region;
                }
                else
                {
                    result = "Compared to All Districts in CESA " + region;;

                }
            }

            return result;
        }

        private TableRow GetSecondSuperHeader()
        {
            TableRow tr = new TableRow();
            SligoCS.Web.Base.WebSupportingClasses.
                WI.StickyParameter.TQRelatedToEnum relateTo = 
                base.StickyParameter.GetTQRelatedTo();

            string org = (tqScatterPlotBL.OrgLevel == OrgLevel.School) ?  "School" : "District" ;

            string schoolType = GetSchoolTypeInTitle(tqScatterPlotBL);

            if (relateTo == SligoCS.Web.Base.WebSupportingClasses.
                WI.StickyParameter.TQRelatedToEnum.District_Spending)
            {
                AddTableCell(tr, org, 1);
                AddTableCell(tr, "District Spending", 1);
                AddTableCell(tr, "Teacher Data: " + schoolType, 3);

                //relateToDenominatorLabel = "Current Education Cost Per Member"; 
                relateToLabel = "Current Education Cost Per Member"; 
           
            }
            else if (relateTo == SligoCS.Web.Base.WebSupportingClasses.
                WI.StickyParameter.TQRelatedToEnum.District_Size)
            {
                AddTableCell(tr, org, 1);
                AddTableCell(tr, "District Size", 1);
                AddTableCell(tr, "Teacher Data: " + schoolType, 3);

                relateToDenominatorLabel = "Total Number Enrolled";            
            }
            else if (relateTo == SligoCS.Web.Base.WebSupportingClasses.
           WI.StickyParameter.TQRelatedToEnum.School_Size)
            {
                AddTableCell(tr, org, 1);
                AddTableCell(tr, "School Size: " + schoolType, 1);
                AddTableCell(tr, "Teacher Data: " + schoolType, 3);

                relateToDenominatorLabel = "Total Number Enrolled";
            }
            else
            {
                AddTableCell(tr, org, 1);
                AddTableCell(tr, "Student Data:" + schoolType, 2);
                AddTableCell(tr, "Teacher Data:" + schoolType, 3);

                relateToDenominatorLabel = "Total # Enrolled";
            }
            
            
            foreach(  TableCell cell in tr.Cells) 
            {
                cell.BorderColor = System.Drawing.Color.Gray;
                cell.BorderStyle = BorderStyle.Double;
                cell.BorderWidth = 3;
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

        private void setBottomLink(BLWIBase baseBL)
        {
            BottomLinkViewReport1.Year = baseBL.CurrentYear.ToString();
            BottomLinkViewReport1.DistrictCd = GetDistrictCdByFullKey();
            BottomLinkViewReport1.SchoolCd = GetSchoolCdByFullKey();
            BottomLinkViewProfile1.DistrictCd = GetDistrictCdByFullKey();
        }

        private void AddTableCell(TableRow tr, string text, int colSpan)
        {
            TableCell cell = new TableCell();
            cell.Text = text;
            cell.ColumnSpan = colSpan;
            cell.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(cell);
        }

        private int GetVisibleColumns()
        {
            int retval = 0;
            foreach (DataControlField col in SligoDataGrid2.Columns)
            {
                if (col.Visible)
                    retval += 1;
            }
            return retval;
        }

        public override List<string> GetVisibleColumns
            (ViewByGroup viewBy, OrgLevel orgLevel, 
            CompareTo compareTo, SchoolType schoolType)
        {
            return GetVisibleColumns(viewBy, orgLevel, 
                compareTo, schoolType, 
                base.StickyParameter.GetTQTeacherVariable(), 
                base.StickyParameter.GetTQRelatedTo());
        }

        public List<string> GetVisibleColumns
        (ViewByGroup viewBy, OrgLevel orgLevel, 
            CompareTo compareTo, SchoolType schoolType,
            SligoCS.Web.Base.WebSupportingClasses.
            WI.StickyParameter.TQTeacherVariableEnum teacherVariable,
            SligoCS.Web.Base.WebSupportingClasses.
            WI.StickyParameter.TQRelatedToEnum relatedTo)
        {
            List<string> retval = new List<string>();
            //base.GetVisibleColumns(viewBy, orgLevel,
            //compareTo, schoolType);

            retval.Add(tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
                    LinkedNameColumn.ColumnName);

            if ((schoolType != SchoolType.AllTypes) &&
                (orgLevel != OrgLevel.School))
            {
                retval.Add(tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
                    SchooltypeLabelColumn.ColumnName);
            }

            // Always display this column
            retval.Add(
                tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
                        FTETotalColumn.ColumnName);


            // Teacher Variable column to display

            if (teacherVariable == SligoCS.Web.Base.WebSupportingClasses.
                WI.StickyParameter.TQTeacherVariableEnum.Percent_Full_Wisconsin_License)
            {
                retval.Add(tqScatterPlotDataSet.
                    v_TeacherQualificationsScatterplot.FTELicenseFullColumn.ColumnName);
                retval.Add(tqScatterPlotDataSet.
                    v_TeacherQualificationsScatterplot.LicenseFullFTEPercentageColumn.ColumnName);
             }
           else if (teacherVariable == SligoCS.Web.Base.WebSupportingClasses.
                WI.StickyParameter.TQTeacherVariableEnum.Percent_Emergency_Wisconsin_License)
            {
                retval.Add(tqScatterPlotDataSet.
                    v_TeacherQualificationsScatterplot.LicenseEmerFTEColumn.ColumnName);
                retval.Add(tqScatterPlotDataSet.
                    v_TeacherQualificationsScatterplot.LicenseEmerFTEPercentageColumn.ColumnName);
            }

            else if (teacherVariable == SligoCS.Web.Base.WebSupportingClasses.
                WI.StickyParameter.TQTeacherVariableEnum.Percent_No_License_For_Assignment)
            {
                retval.Add(tqScatterPlotDataSet.
                    v_TeacherQualificationsScatterplot.LicenseNoFTEColumn.ColumnName);
                retval.Add(tqScatterPlotDataSet.
                    v_TeacherQualificationsScatterplot.LicenseNoFTEPercentageColumn.ColumnName);
            }

            else if (teacherVariable == SligoCS.Web.Base.WebSupportingClasses.
           WI.StickyParameter.TQTeacherVariableEnum.Percent_5_or_More_Years_District_Experience)
            {
                retval.Add(tqScatterPlotDataSet.
                    v_TeacherQualificationsScatterplot.LocalExperience5YearsOrMoreFTEColumn.ColumnName);
                retval.Add(tqScatterPlotDataSet.
                    v_TeacherQualificationsScatterplot.LocalExperience5YearsOrMoreFTEPercentageColumn.ColumnName);
            }

            else if (teacherVariable == SligoCS.Web.Base.WebSupportingClasses.
           WI.StickyParameter.TQTeacherVariableEnum.Percent_5_or_More_Years_Total_Experience)
            {
                retval.Add(tqScatterPlotDataSet.
                    v_TeacherQualificationsScatterplot.TotalExperience5YearsOrMoreFTEColumn.ColumnName);
                retval.Add(tqScatterPlotDataSet.
                    v_TeacherQualificationsScatterplot.TotalExperience5YearsOrMoreFTEPercentageColumn.ColumnName);
            }

            else if (teacherVariable == SligoCS.Web.Base.WebSupportingClasses.
           WI.StickyParameter.TQTeacherVariableEnum.Percent_Masters_or_Higher_Degree)
            {
                retval.Add(tqScatterPlotDataSet.
                    v_TeacherQualificationsScatterplot.DegreeMastersOrHigherFTEColumn.ColumnName);
                retval.Add(tqScatterPlotDataSet.
                    v_TeacherQualificationsScatterplot.DegreeMastersOrHigherFTEPercentageColumn.ColumnName);
            }

            else if (teacherVariable == SligoCS.Web.Base.WebSupportingClasses.
           WI.StickyParameter.TQTeacherVariableEnum.Percent_ESEA_Qualified)
            {
                retval.Add(tqScatterPlotDataSet.
                    v_TeacherQualificationsScatterplot.EHQYesFTEColumn.ColumnName);
                retval.Add(tqScatterPlotDataSet.
                    v_TeacherQualificationsScatterplot.EHQYesFTEPercentageColumn.ColumnName);
            }


            // Related to 


           // if (relateTo == SligoCS.Web.Base.WebSupportingClasses.
           //     WI.StickyParameter.TQRelatedToEnum.District_Spending)
           // {
           //     retval.Add(tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
           //        RelateToDenominatorColumn.ColumnName);
           // }
           // else if (relateTo == SligoCS.Web.Base.WebSupportingClasses.
           //     WI.StickyParameter.TQRelatedToEnum.District_Size)
           // {
           //     AddTableCell(tr, org, 1);
           //     AddTableCell(tr, "District Size", 1);
           //     AddTableCell(tr, "Teacher Data:" + schoolType, 3);

           //     relateToDenominatorLabel = "Total Number Enrolled";
           // }
           // else if (relateTo == SligoCS.Web.Base.WebSupportingClasses.
           //WI.StickyParameter.TQRelatedToEnum.School_Size)
           // {
           //     AddTableCell(tr, org, 1);
           //     AddTableCell(tr, "School Size" + schoolType, 1);
           //     AddTableCell(tr, "Teacher Data:" + schoolType, 3);

           //     relateToDenominatorLabel = "Total Number Enrolled";
           // }
           // else
           // {
           //     AddTableCell(tr, org, 1);
           //     AddTableCell(tr, "Student Data:" + schoolType, 1);
           //     AddTableCell(tr, "Teacher Data:" + schoolType, 3);

           //     relateToDenominatorLabel = "Total # Enrolled";
           // }

            if (relatedTo == SligoCS.Web.Base.WebSupportingClasses.
                    WI.StickyParameter.TQRelatedToEnum.District_Spending )
            {
                retval.Add(tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
                   RelateToValueColumn.ColumnName);
            }

            else if (relatedTo == SligoCS.Web.Base.WebSupportingClasses.
                        WI.StickyParameter.TQRelatedToEnum.District_Size ||
                                    relatedTo == SligoCS.Web.Base.WebSupportingClasses.
                        WI.StickyParameter.TQRelatedToEnum.School_Size)
            {
                retval.Add(tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
                   RelateToDenominatorColumn.ColumnName);
            }
            else 
            {
                retval.Add(tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
                   RelateToDenominatorColumn.ColumnName);
                retval.Add(tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
                   RelateToValueColumn.ColumnName);
            }

            return retval;
        }

        //protected void SligoDataGrid2_DataBound(
        //    Object sender, GridViewRowEventArgs e)
        //{    
        //}

        //protected void SligoDataGrid2_RowCreated(
        //    Object sender, GridViewRowEventArgs e)
        //{


        //}

        protected void SligoDataGrid2_RowDataBound(
            Object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //decode the link to specific schools, so that it appears as a normal URL link.
                int colIndex = SligoDataGrid2.FindBoundColumnIndex
                     (tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.LinkedNameColumn.ColumnName);
                e.Row.Cells[colIndex].Text = Server.HtmlDecode(e.Row.Cells[colIndex].Text);

                ////format the numerical values                
                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row,
                    tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
                    FTETotalColumn.ColumnName, 
                    Constants.FORMAT_RATE_01);

                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row, tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
                    FTELicenseFullColumn.ColumnName, 
                    Constants.FORMAT_RATE_01);

                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row, tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
                    LicenseFullFTEPercentageColumn.ColumnName, 
                    Constants.FORMAT_RATE_00_PERC);

                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row, tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
                    LicenseEmerFTEColumn.ColumnName, 
                    Constants.FORMAT_RATE_01);

                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row, tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
                    LicenseEmerFTEPercentageColumn.ColumnName, 
                    Constants.FORMAT_RATE_00_PERC);

                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row, tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
                    LicenseNoFTEColumn.ColumnName, 
                    Constants.FORMAT_RATE_01);

                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row, tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
                    LicenseNoFTEPercentageColumn.ColumnName, 
                    Constants.FORMAT_RATE_00_PERC);


                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row, tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
                    RelateToDenominatorColumn.ColumnName, 
                    Constants.FORMAT_RATE_0_NO_DOT);

                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row, tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
                    RelateToValueColumn.ColumnName,
                    Constants.FORMAT_RATE_0_NO_DOT);

                //SligoDataGrid2.SetCellToFormattedDecimal(
                //     e.Row,
                //     tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
                //     LocalExperience5YearsOrLessFTEColumn.ColumnName, 
                //     Constants.FORMAT_RATE_01);

                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row,
                    tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
                    LocalExperience5YearsOrMoreFTEColumn.ColumnName,
                    Constants.FORMAT_RATE_01);

                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row,
                    tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
                    LocalExperience5YearsOrMoreFTEPercentageColumn.ColumnName,
                    Constants.FORMAT_RATE_00_PERC);

 
                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row,
                    tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
                    TotalExperience5YearsOrMoreFTEColumn.ColumnName, 
                    Constants.FORMAT_RATE_01);

                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row,
                    tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
                    TotalExperience5YearsOrMoreFTEPercentageColumn.ColumnName,
                    Constants.FORMAT_RATE_00_PERC);

                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row,
                    tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
                    DegreeMastersOrHigherFTEColumn.ColumnName, 
                    Constants.FORMAT_RATE_01);

                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row,
                    tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
                    DegreeMastersOrHigherFTEPercentageColumn.ColumnName,
                    Constants.FORMAT_RATE_00_PERC);

                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row,
                    tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
                    EHQYesFTEColumn.ColumnName, 
                    Constants.FORMAT_RATE_01);

                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row,
                    tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
                    EHQYesFTEPercentageColumn.ColumnName, 
                    Constants.FORMAT_RATE_00_PERC);
                
            //    SligoDataGrid2.SetCellToFormattedDecimal(
            //        e.Row,
            //        tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
            //        EHQNoFTEColumn.ColumnName, Constants.FORMAT_RATE_01);
                
            //    SligoDataGrid2.SetCellToFormattedDecimal(
            //        e.Row,
            //        tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
            //        EHQNoFTEPercentageColumn.ColumnName, Constants.FORMAT_RATE_00_PERC);

                SetOrgLevelRowLabels(tqScatterPlotBL, SligoDataGrid2, e.Row);

                //FormatHelper formater = new FormatHelper();
                //formater.SetRaceAbbr(SligoDataGrid2, e.Row, 
                //  tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.RaceLabelColumn.ToString());

            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    if (cell.Text == "RelateToValue")
                    {
                        cell.Text = relateToLabel;
                    }
                    if (cell.Text == "RelateToDenominator")
                    {
                        cell.Text = relateToDenominatorLabel;
                    }
                }

            }
        }

        public override DataSet GetDataSet()
        {
            return tqScatterPlotDataSet;
        }

    }
}
