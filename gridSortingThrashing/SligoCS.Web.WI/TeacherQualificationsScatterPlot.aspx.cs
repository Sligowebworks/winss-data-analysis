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
using SligoCS.Web.WI.DAL.DataSets;
using SligoCS.BL.WI;
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.WI.HelperClasses;
using SligoCS.Web.WI.WebSupportingClasses.WI;

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
        BLTeacherQualificationsScatterPlot tqScatterPlotBL;

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
                        base.GlobalValues.TQSubjects,
                        base.GlobalValues.TQTeacherVariable,
                        base.GlobalValues.TQRelateTo, 
                        base.GlobalValues.TQLocation,
                        base.GlobalValues.COUNTY,
                        getCESA(GlobalValues.FULLKEY)   
                        );
            
            SetVisibleColumns2(SligoDataGrid2, 
                tqScatterPlotDataSet, tqScatterPlotBL.ViewBy, 
                tqScatterPlotBL.OrgLevel, tqScatterPlotBL.CompareTo, 
                GlobalValues.STYP);
            
            GlobalValues.SQL = tqScatterPlotBL.SQL;

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
            if (CheckIfGraphPanelVisible(GraphPanel) == true)
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
                    xValueColumnName = tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.RelateToDenominatorColumn.ColumnName;
                }
                else
                {
                    xValueColumnName = tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.RelateToValueColumn.ColumnName;
                }

                if (relatedTo.CompareToKey(TQRelateToKeys.DistrictSize)||
                    relatedTo.CompareToKey(TQRelateToKeys.SchoolSize)||
                    relatedTo.CompareToKey(TQRelateToKeys.Spending))
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
               

                if  (relatedTo.CompareToKey(TQRelateToKeys.Spending))
                {
                        axisXDescription = "District Current Education Cost Per Member";
                        xMax = maxValueInResult + 1; 
                        xStep = 2000;
                }  else if (relatedTo.CompareToKey(TQRelateToKeys.DistrictSize))
                {
                    axisXDescription = "District Size -- Number of Students Enrolled";
                    xMax = maxValueInResult + 1;
                    xStep = 10000;
                }  else if (relatedTo.CompareToKey(TQRelateToKeys.SchoolSize))
                    {
                        axisXDescription = "School Size -- Number of Students Enrolled";
                        xMax = maxValueInResult + 1;
                        xStep = 200;
                       } else if (relatedTo.CompareToKey(TQRelateToKeys.EconomicStatus))
                { 
                        axisXDescription = "Percent of Students Who Are Economically Disadvantaged";
                    } else if (relatedTo.CompareToKey(TQRelateToKeys.EnglishProficiency))
                {
                        axisXDescription = "Percent of Students Who Are Limited English Proficient";
                    }else if (relatedTo.CompareToKey(TQRelateToKeys.Disability))
                { 
                        axisXDescription = "Percent of Students Who Are Students With Disabilities";
                     }else if (relatedTo.CompareToKey(TQRelateToKeys.NativeAm))
                {
                        axisXDescription = "Percent of Students Who Are Am Indian";
                     }else if (relatedTo.CompareToKey(TQRelateToKeys.Asian))
                {
                        axisXDescription = "Percent of Students Who Are Asian";
                      }else if (relatedTo.CompareToKey(TQRelateToKeys.Black))
                {
                        axisXDescription = "Percent of Students Who Are Black";
                    }else if (relatedTo.CompareToKey(TQRelateToKeys.Hispanic))
                {
                        axisXDescription = "Percent of Students Who Are Hispanic";
                    }else if (relatedTo.CompareToKey(TQRelateToKeys.White))
                {
                        axisXDescription = "Percent of Students Who Are White";
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
                    yValueColumnName = tqScatterPlotDataSet.
                        v_TeacherQualificationsScatterplot.LicenseFullFTEPercentageColumn.ColumnName;
                }
                else if (teacherVariable.CompareToKey(TQTeacherVariableKeys.EmergencyLic))
                {
                    yValueColumnName = tqScatterPlotDataSet.
                        v_TeacherQualificationsScatterplot.LicenseEmerFTEPercentageColumn.ColumnName;
                }

                else if (teacherVariable.CompareToKey(TQTeacherVariableKeys.NoLicense))
                {
                    yValueColumnName = tqScatterPlotDataSet.
                        v_TeacherQualificationsScatterplot.LicenseNoFTEPercentageColumn.ColumnName;
                }

                else if (teacherVariable.CompareToKey(TQTeacherVariableKeys.DistrictExp))
                {
                    yValueColumnName = tqScatterPlotDataSet.
                        v_TeacherQualificationsScatterplot.LocalExperience5YearsOrMoreFTEPercentageColumn.ColumnName;
                }

                else if (teacherVariable.CompareToKey(TQTeacherVariableKeys.TotalExp))
                {
                    yValueColumnName = tqScatterPlotDataSet.
                        v_TeacherQualificationsScatterplot.TotalExperience5YearsOrMoreFTEPercentageColumn.ColumnName;
                }

                else if (teacherVariable.CompareToKey(TQTeacherVariableKeys.Degree))
                {
                    yValueColumnName = tqScatterPlotDataSet.
                        v_TeacherQualificationsScatterplot.DegreeMastersOrHigherFTEPercentageColumn.ColumnName;
                }

                else if (teacherVariable.CompareToKey(TQTeacherVariableKeys.ESEA))
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

            TQLocation location = base.GlobalValues.TQLocation;
            
            string title = "Teacher Qualifications" + DELIM +
                base.GlobalValues.TQSubjectsSP.Key.Replace("_", " ") + DELIM +

                GlobalValues.GetOrgName() + ":" +
                TitleBuilder.GetSchoolTypeInTitle().Replace(@"<br/> ", " ") + DELIM +
                TitleBuilder.GetYearRangeInTitle(entity.Years) + " " +
                 GetLocationString()+
                 TitleBuilder.GetSchoolTypeInTitle();

            return title;
        }

        protected string GetLocationString()
        {
            string result = string.Empty;
            BLAgencyFull agencyBL = new BLAgencyFull();

            TQLocation location = base.GlobalValues.TQLocation;

            string org = (GlobalValues.OrgLevel.Key == OrgLevelKeys.School) ? "School" : "District";

            if (location.CompareToKey(TQLocationKeys.State))
            {
                if (GlobalValues.OrgLevel.Key == OrgLevelKeys.School)
                {
                    result = "Compared to All " + TitleBuilder.GetSchoolTypeInTitle() 
                        + " in Entire State";
                }
                else
                {
                    result = "Compared to All Districts in Entire State";
                }
            }
            else if (location.CompareToKey(TQLocationKeys.County))
            {
                string region = agencyBL.GetCountyNameByID
                    (GlobalValues.COUNTY.ToString()).Trim();

                if (GlobalValues.OrgLevel.Key == OrgLevelKeys.School)
                {
                    result = "Compared to All " + TitleBuilder.GetSchoolTypeInTitle() + 
                        " in " + region;
                }
                else
                {
                    result = "Compared to All Districts in " + region;;

                }
            }

            else if (location.CompareToKey(TQLocationKeys.CESA))
            {
                string region = GlobalValues.FULLKEY.Substring (0,2);

                if (GlobalValues.OrgLevel.Key == OrgLevelKeys.School)
                {
                    result = "Compared to All " + TitleBuilder.GetSchoolTypeInTitle() + " in CESA " + region;
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
            TQRelateTo relateTo = GlobalValues.TQRelateTo;

            string org = (GlobalValues.OrgLevel.Key == OrgLevelKeys.School) ? "School" : "District";

            string schoolType = TitleBuilder.GetSchoolTypeInTitle();

            if (relateTo.CompareToKey(TQRelateToKeys.Spending))
            {
                AddTableCell(tr, org, 1);
                AddTableCell(tr, "District Spending", 1);
                AddTableCell(tr, "Teacher Data: " + schoolType, 3);

                //relateToDenominatorLabel = "Current Education Cost Per Member"; 
                relateToLabel = "Current Education Cost Per Member"; 
           
            }
            else if (relateTo.CompareToKey(TQRelateToKeys.DistrictSize))
            {
                AddTableCell(tr, org, 1);
                AddTableCell(tr, "District Size", 1);
                AddTableCell(tr, "Teacher Data: " + schoolType, 3);

                relateToDenominatorLabel = "Total Number Enrolled";            
            }
            else if (relateTo.CompareToKey(TQRelateToKeys.SchoolSize))
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
            (Group viewBy, OrgLevel orgLevel, 
            CompareTo compareTo, STYP schoolType)
        {
            return GetVisibleColumns(viewBy, orgLevel, 
                compareTo, schoolType, 
                base.GlobalValues.TQTeacherVariable,
                base.GlobalValues.TQRelateTo);
        }

        public List<string> GetVisibleColumns
        (Group viewBy, OrgLevel orgLevel, 
            CompareTo compareTo, STYP schoolType,
            TQTeacherVariable teacherVariable,
           TQRelateTo relatedTo)
        {
            List<string> retval = new List<string>();
            //base.GetVisibleColumns(viewBy, orgLevelKey,
            //compareTo, schoolType);

            retval.Add(tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
                    LinkedNameColumn.ColumnName);

            if ((schoolType.Key != STYPKeys.AllTypes) &&
                (orgLevel.Key != OrgLevelKeys.School))
            {
                retval.Add(tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
                    SchooltypeLabelColumn.ColumnName);
            }

            // Always display this column
            retval.Add(
                tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
                        FTETotalColumn.ColumnName);


            // Teacher Variable column to display

            if (teacherVariable.CompareToKey(TQTeacherVariableKeys.WiscLicense))
            {
                retval.Add(tqScatterPlotDataSet.
                    v_TeacherQualificationsScatterplot.FTELicenseFullColumn.ColumnName);
                retval.Add(tqScatterPlotDataSet.
                    v_TeacherQualificationsScatterplot.LicenseFullFTEPercentageColumn.ColumnName);
             }
           else if (teacherVariable.CompareToKey(TQTeacherVariableKeys.EmergencyLic))
            {
                retval.Add(tqScatterPlotDataSet.
                    v_TeacherQualificationsScatterplot.LicenseEmerFTEColumn.ColumnName);
                retval.Add(tqScatterPlotDataSet.
                    v_TeacherQualificationsScatterplot.LicenseEmerFTEPercentageColumn.ColumnName);
            }

            else if (teacherVariable.CompareToKey(TQTeacherVariableKeys.NoLicense))
            {
                retval.Add(tqScatterPlotDataSet.
                    v_TeacherQualificationsScatterplot.LicenseNoFTEColumn.ColumnName);
                retval.Add(tqScatterPlotDataSet.
                    v_TeacherQualificationsScatterplot.LicenseNoFTEPercentageColumn.ColumnName);
            }

            else if (teacherVariable.CompareToKey(TQTeacherVariableKeys.DistrictExp))
            {
                retval.Add(tqScatterPlotDataSet.
                    v_TeacherQualificationsScatterplot.LocalExperience5YearsOrMoreFTEColumn.ColumnName);
                retval.Add(tqScatterPlotDataSet.
                    v_TeacherQualificationsScatterplot.LocalExperience5YearsOrMoreFTEPercentageColumn.ColumnName);
            }

            else if (teacherVariable.CompareToKey(TQTeacherVariableKeys.TotalExp))
            {
                retval.Add(tqScatterPlotDataSet.
                    v_TeacherQualificationsScatterplot.TotalExperience5YearsOrMoreFTEColumn.ColumnName);
                retval.Add(tqScatterPlotDataSet.
                    v_TeacherQualificationsScatterplot.TotalExperience5YearsOrMoreFTEPercentageColumn.ColumnName);
            }

            else if (teacherVariable.CompareToKey(TQTeacherVariableKeys.Degree))
            {
                retval.Add(tqScatterPlotDataSet.
                    v_TeacherQualificationsScatterplot.DegreeMastersOrHigherFTEColumn.ColumnName);
                retval.Add(tqScatterPlotDataSet.
                    v_TeacherQualificationsScatterplot.DegreeMastersOrHigherFTEPercentageColumn.ColumnName);
            }

            else if (teacherVariable.CompareToKey(TQTeacherVariableKeys.ESEA))
            {
                retval.Add(tqScatterPlotDataSet.
                    v_TeacherQualificationsScatterplot.EHQYesFTEColumn.ColumnName);
                retval.Add(tqScatterPlotDataSet.
                    v_TeacherQualificationsScatterplot.EHQYesFTEPercentageColumn.ColumnName);
            }

            if (relatedTo.CompareToKey(TQRelateToKeys.Spending))
            {
                retval.Add(tqScatterPlotDataSet.v_TeacherQualificationsScatterplot.
                   RelateToValueColumn.ColumnName);
            }

            else if (relatedTo.CompareToKey(TQRelateToKeys.DistrictSize) ||
                                    relatedTo.CompareToKey(TQRelateToKeys.SchoolSize))
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

                SligoDataGrid2.SetOrgLevelRowLabels(GlobalValues, e.Row);

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
