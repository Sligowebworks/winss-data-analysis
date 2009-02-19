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
using SligoCS.Web.WI.PageBase.WI;
using SligoCS.Web.WI.HelperClasses;
//using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.Web.WI
{

    /// <summary>
    /// This page is also known as the TeacherQualifications page.
    /// </summary>
    public partial class TeacherQualifications: PageBaseWI
    {
        private string graphTitle = string.Empty;
        protected v_TeacherQualifications teacherQualDataSet = 
            new v_TeacherQualifications ();
        BLTeacherQualifications teachersBL = new BLTeacherQualifications();
        
        protected void Page_Load(object sender, EventArgs e)
        {

            Page.Master.EnableViewState = false;

            base.PrepBLEntity(teachersBL);

            CheckSelectedSchoolOrDistrict(teachersBL);
            SetLinkChangeSelectedSchoolOrDistrict(
                teachersBL, ChangeSelectedSchoolOrDistrict);

            //set the page heading
            ((SligoCS.Web.WI.WI)Page.Master).SetPageHeading
                ("What are the qualifications of teachers?");

            teacherQualDataSet = 
                teachersBL.GetTeacherQualifications( 
                        base.StickyParameter.TQSubjects );
            
            SetVisibleColumns2(SligoDataGrid2, 
                teacherQualDataSet, teachersBL.ViewBy, 
                teachersBL.OrgLevel, teachersBL.CompareTo, 
                teachersBL.SchoolType);
            
            StickyParameter.SQL = teachersBL.SQL;

            this.SligoDataGrid2.DataSource = teacherQualDataSet;

            graphTitle = GetTitle(
                teachersBL,
                GetRegionString());

            this.SligoDataGrid2.DataBind();
            SligoDataGrid2.AddSuperHeader (graphTitle);

            //Add a 2nd Super Header if Show = Wisconsin State License.
            SligoDataGrid2.AddSuperHeader(GetSecondSuperHeader());

            set_state();
            setBottomLink(teachersBL);

            ////Notes:  graph 
            GraphPanel.Visible = false;
            if (CheckIfGraphPanelVisible(teachersBL, GraphPanel) == true)
            {
                SetUpChart(teacherQualDataSet);
            }
        }

        /// <summary>
        ///  Set up graph 
        /// </summary>
        /// <param name="ds"></param>
        private void SetUpChart(v_TeacherQualifications ds)
        {
            try
            {
                graphTitle = graphTitle.Replace("<br/>", "\n");
                barChart.Title = graphTitle;

                barChart.AxisYMin = 0;
                barChart.AxisYMax = 100;
                barChart.AxisYStep = 10;
                barChart.AxisYDescription = "Percent of FTE Teachers";

                //Bind Data Source & Display
                barChart.BLBase = teachersBL;

                if (base.StickyParameter.GetTQShow() ==
                    SligoCS.Web.WI.WebSupportingClasses.WI.StickyParameter.TQShowEnum.Wisconsin_License_Status)
                {
                    //Stacked Bar Chart
                    barChart.Type = StackedType.Stacked100;
                    barChart.ChartDataSource = RemoveExtraColumnsPreGraph(ds).Tables[0];
                }
                else
                {
                    //Normal Bar Chart
                    barChart.Type = StackedType.No;
                    
                    string displayColumn = string.Empty;
                    ArrayList friendlyAxisXName = new ArrayList();
                    switch (base.StickyParameter.GetTQShow())
                    {
                        case SligoCS.Web.WI.WebSupportingClasses.WI.StickyParameter.TQShowEnum.District_Experience:
                            displayColumn = ds._v_TeacherQualifications.LocalExperience5YearsOrMoreFTEPercentageColumn.ColumnName;
                            friendlyAxisXName.Add("District Experience");
                            break;
                        case SligoCS.Web.WI.WebSupportingClasses.WI.StickyParameter.TQShowEnum.Total_Experience:
                            displayColumn = ds._v_TeacherQualifications.TotalExperience5YearsOrMoreFTEPercentageColumn.ColumnName;
                            friendlyAxisXName.Add("Total Experience");
                            break;
                        case SligoCS.Web.WI.WebSupportingClasses.WI.StickyParameter.TQShowEnum.Highest_Degree:
                            displayColumn = ds._v_TeacherQualifications.DegreeMastersOrHigherFTEPercentageColumn.ColumnName;
                            friendlyAxisXName.Add("Highest Degree");
                            break;
                        case SligoCS.Web.WI.WebSupportingClasses.WI.StickyParameter.TQShowEnum.ESEA_Qualified:
                            displayColumn = ds._v_TeacherQualifications.EHQYesFTEPercentageColumn.ColumnName;
                            friendlyAxisXName.Add("ESEA Qualified");
                            break;
                    }
                    barChart.DisplayColumnName = displayColumn;
                    barChart.FriendlyAxisXName = friendlyAxisXName;
                    barChart.RawDataSource = ds.Tables[0];
                }

                barChart.DataBind();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        //Notes For Graph
        private DataSet RemoveExtraColumnsPreGraph(v_TeacherQualifications ds)
        {
            DataSet dsGraph = ds.Copy();
            string labelColumnName = string.Empty;
            if (teachersBL.SchoolType == SchoolType.AllTypes)
            {
                labelColumnName = teachersBL.GetViewByColumnName();
            }
            else
            {
                labelColumnName = teachersBL.GetCompareToColumnName();
            }
            String[] graphColumns = new string[] { 
                labelColumnName,
                ds._v_TeacherQualifications.FTELicenseFullColumn.ColumnName, 
                ds._v_TeacherQualifications.LicenseEmerFTEColumn.ColumnName, 
                ds._v_TeacherQualifications.LicenseNoFTEColumn.ColumnName };

            ArrayList removedColumns = new ArrayList();
            foreach(DataColumn col in dsGraph.Tables[0].Columns)
            {
                removedColumns.Add(col.ColumnName);
            }
            removedColumns.Remove(graphColumns[0]);
            removedColumns.Remove(graphColumns[1]);
            removedColumns.Remove(graphColumns[2]);
            removedColumns.Remove(graphColumns[3]);

            for(int i = 0; i < removedColumns.Count; i++)
            {
                dsGraph.Tables[0].Columns.Remove(removedColumns[i].ToString());
            }

            //Rename the column name for displaying friendly series name
            dsGraph.Tables[0].Columns[ds._v_TeacherQualifications.FTELicenseFullColumn.ColumnName].ColumnName = "Full License";
            dsGraph.Tables[0].Columns[ds._v_TeacherQualifications.LicenseEmerFTEColumn.ColumnName].ColumnName = "Emergency License";
            dsGraph.Tables[0].Columns[ds._v_TeacherQualifications.LicenseNoFTEColumn.ColumnName].ColumnName = "No License For Assignment";
            
            return dsGraph;
        }


        private string GetTitle(
            BLWIBase entity, string regionString)
        {
            const string DELIM = @"<br/>";
            StringBuilder tmpHeader = new StringBuilder();

            string typeOfData = String.Empty;

            SligoCS.Web.WI.WebSupportingClasses.
                WI.StickyParameter.TQShowEnum tqShow =
                base.StickyParameter.GetTQShow();
            if (tqShow == SligoCS.Web.WI.WebSupportingClasses.
                   WI.StickyParameter.TQShowEnum.Wisconsin_License_Status)
            {
                typeOfData = "Wisconsin Teacher License Status";
            }
            else if (tqShow == SligoCS.Web.WI.WebSupportingClasses.
                        WI.StickyParameter.TQShowEnum.District_Experience)
            {
                typeOfData = "FTE Teachers with 5 or More Years of District Experience";
            }
            else if (tqShow == SligoCS.Web.WI.WebSupportingClasses.
                   WI.StickyParameter.TQShowEnum.Total_Experience)
            {
                typeOfData = "FTE Teachers with 5 or More Years of Total Experience";
            }
            else if (tqShow == SligoCS.Web.WI.WebSupportingClasses.
                   WI.StickyParameter.TQShowEnum.Highest_Degree)
            {
                typeOfData = "Percent of FTE Teachers with Masters Degree or Higher";
            }
            else if (tqShow == SligoCS.Web.WI.WebSupportingClasses.
                   WI.StickyParameter.TQShowEnum.ESEA_Qualified)
            {
                typeOfData = "ESEA Qualified Teachers";
            }

            string title = typeOfData + DELIM +
                base.StickyParameter.
                    GetTQSubjectsTaught().ToString().Replace("_", " ") + DELIM +
                base.GetOrgName(entity.OrgLevel) + ":" + 
                GetSchoolTypeInTitle((BLWIBase)entity).Replace(@"<br/> ", " ") + DELIM +
                base.GetYearRangeInTitle(entity.Years) +
                base.GetCompareToInTitle(
                    entity.OrgLevel, entity.CompareTo,
                    entity.SchoolType, entity.S4orALL, 
                    GetRegionString());

            return title.Replace("Summary All Subjects", "Summary - All Subjects");
        }

        private TableRow GetSecondSuperHeader()
        {
            TableRow tr = new TableRow();
            SligoCS.Web.WI.WebSupportingClasses.
                WI.StickyParameter.TQShowEnum show = base.StickyParameter.GetTQShow();
            if (show == SligoCS.Web.WI.WebSupportingClasses.
                WI.StickyParameter.TQShowEnum.Wisconsin_License_Status)
            {
                
                AddTableCell(tr, string.Empty, GetVisibleColumns() - 7);                
                AddTableCell(tr, "Total", 1);
                AddTableCell(tr, "Full License", 2);
                AddTableCell(tr, "Emergency License", 2);
                AddTableCell(tr, "No License For Assignment", 2);

            }
            else if (show == SligoCS.Web.WI.WebSupportingClasses.
                WI.StickyParameter.TQShowEnum.Highest_Degree)
            {                 
                //tr = new TableRow();
                AddTableCell(tr, string.Empty, 1);
                AddTableCell(tr, "Total", 1);
                AddTableCell(tr, "Masters or Higher", 2);
            }
            else if (show == SligoCS.Web.WI.WebSupportingClasses.
           WI.StickyParameter.TQShowEnum.ESEA_Qualified)
            {
                // Total ESEA Qualified Not ESEA Qualified 
                //tr = new TableRow();
                AddTableCell(tr, string.Empty, 1);
                AddTableCell(tr, "Total", 1);
                AddTableCell(tr, "ESEA Qualified", 2);
                AddTableCell(tr, "Not ESEA Qualified", 2);
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
                compareTo, schoolType, base.StickyParameter.GetTQShow());
        }

        public List<string> GetVisibleColumns
            (ViewByGroup viewBy, OrgLevel orgLevel, 
            CompareTo compareTo, SchoolType schoolType,
            SligoCS.Web.WI.WebSupportingClasses.
                WI.StickyParameter.TQShowEnum show)
        {
            List<string> retval =  
                base.GetVisibleColumns(viewBy, orgLevel, 
                compareTo, schoolType);

            if (compareTo == CompareTo.PRIORYEARS)
                retval.Add(teacherQualDataSet._v_TeacherQualifications.
                    YearFormattedColumn.ColumnName);           
            else if (compareTo != CompareTo.PRIORYEARS)
            {
                retval.Add(teacherQualDataSet._v_TeacherQualifications.
                    OrgLevelLabelColumn.ColumnName);
            }

            if ((schoolType != SchoolType.AllTypes) && 
                (orgLevel != OrgLevel.School))
            {
                retval.Add(teacherQualDataSet._v_TeacherQualifications.
                    SchooltypeLabelColumn.ColumnName);
            }

            retval.Add(
                teacherQualDataSet._v_TeacherQualifications.
                        FTETotalColumn.ColumnName);


            if (show == SligoCS.Web.WI.WebSupportingClasses.
                WI.StickyParameter.TQShowEnum.Wisconsin_License_Status)
            {
                retval.Add(teacherQualDataSet.
                    _v_TeacherQualifications.FTELicenseFullColumn.ColumnName);
                retval.Add(teacherQualDataSet.
                    _v_TeacherQualifications.LicenseFullFTEPercentageColumn.ColumnName);
                retval.Add(teacherQualDataSet.
                    _v_TeacherQualifications.LicenseEmerFTEColumn.ColumnName);
                retval.Add(teacherQualDataSet.
                    _v_TeacherQualifications.LicenseEmerFTEPercentageColumn.ColumnName);
                retval.Add(teacherQualDataSet.
                    _v_TeacherQualifications.LicenseNoFTEColumn.ColumnName);
                retval.Add(teacherQualDataSet.
                    _v_TeacherQualifications.LicenseNoFTEPercentageColumn.ColumnName);
            }
            else if (show == SligoCS.Web.WI.WebSupportingClasses.
                WI.StickyParameter.TQShowEnum.District_Experience)
            {
                retval.Add(teacherQualDataSet._v_TeacherQualifications.
                    LocalExperience5YearsOrLessFTEColumn.ColumnName);
                retval.Add(teacherQualDataSet._v_TeacherQualifications.
                    LocalExperience5YearsOrMoreFTEColumn.ColumnName);
                retval.Add(teacherQualDataSet._v_TeacherQualifications.
                    LocalExperience5YearsOrMoreFTEPercentageColumn.ColumnName);  
            }
            else if (show == SligoCS.Web.WI.WebSupportingClasses.
           WI.StickyParameter.TQShowEnum.Total_Experience)
            {
                retval.Add(teacherQualDataSet._v_TeacherQualifications.
                    TotalExperience5YearsOrLessFTEColumn.ColumnName);
                retval.Add(teacherQualDataSet._v_TeacherQualifications.
                    TotalExperience5YearsOrMoreFTEColumn.ColumnName);
                retval.Add(teacherQualDataSet._v_TeacherQualifications.
                    TotalExperience5YearsOrMoreFTEPercentageColumn.ColumnName);  
            }
            else if (show == SligoCS.Web.WI.WebSupportingClasses.
           WI.StickyParameter.TQShowEnum.Highest_Degree)
            {
                retval.Add(teacherQualDataSet._v_TeacherQualifications.
                    DegreeMastersOrHigherFTEColumn.ColumnName);
                retval.Add(teacherQualDataSet._v_TeacherQualifications.
                    DegreeMastersOrHigherFTEPercentageColumn.ColumnName);
            }
            else if (show == SligoCS.Web.WI.WebSupportingClasses.
           WI.StickyParameter.TQShowEnum.ESEA_Qualified)
            {
                retval.Add(teacherQualDataSet._v_TeacherQualifications.
                    EHQYesFTEColumn.ColumnName);
                retval.Add(teacherQualDataSet._v_TeacherQualifications.
                    EHQYesFTEPercentageColumn.ColumnName);
                retval.Add(teacherQualDataSet._v_TeacherQualifications.
                    EHQNoFTEColumn.ColumnName);
                retval.Add(teacherQualDataSet._v_TeacherQualifications.
                    EHQNoFTEPercentageColumn.ColumnName);               
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
                     (teacherQualDataSet._v_TeacherQualifications.LinkedNameColumn.ColumnName);
                e.Row.Cells[colIndex].Text = Server.HtmlDecode(e.Row.Cells[colIndex].Text);

                ////format the numerical values                
                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row, 
                    teacherQualDataSet._v_TeacherQualifications.
                    FTETotalColumn.ColumnName, Constants.FORMAT_RATE_01);
                
                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row, teacherQualDataSet._v_TeacherQualifications.
                    FTELicenseFullColumn.ColumnName, Constants.FORMAT_RATE_01);
                
                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row, teacherQualDataSet._v_TeacherQualifications.
                    LicenseFullFTEPercentageColumn.ColumnName, Constants.FORMAT_RATE_01_PERC);
                
                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row, teacherQualDataSet._v_TeacherQualifications.
                    LicenseEmerFTEColumn.ColumnName, Constants.FORMAT_RATE_01);
                
                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row, teacherQualDataSet._v_TeacherQualifications.
                    LicenseEmerFTEPercentageColumn.ColumnName, Constants.FORMAT_RATE_01_PERC);
                
                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row, teacherQualDataSet._v_TeacherQualifications.
                    LicenseNoFTEColumn.ColumnName, Constants.FORMAT_RATE_01);
                
                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row, teacherQualDataSet._v_TeacherQualifications.
                    LicenseNoFTEPercentageColumn.ColumnName, Constants.FORMAT_RATE_01_PERC);
                
                SligoDataGrid2.SetCellToFormattedDecimal(
     e.Row,
     teacherQualDataSet._v_TeacherQualifications.
     LocalExperience5YearsOrLessFTEColumn.ColumnName, Constants.FORMAT_RATE_01);

                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row,
                    teacherQualDataSet._v_TeacherQualifications.
                    LocalExperience5YearsOrMoreFTEColumn.ColumnName, Constants.FORMAT_RATE_01);

                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row,
                    teacherQualDataSet._v_TeacherQualifications.
                    LocalExperience5YearsOrMoreFTEPercentageColumn.ColumnName, 
                    Constants.FORMAT_RATE_01_PERC);

                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row,
                    teacherQualDataSet._v_TeacherQualifications.
                    TotalExperience5YearsOrLessFTEColumn.ColumnName, Constants.FORMAT_RATE_01);

                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row,
                    teacherQualDataSet._v_TeacherQualifications.
                    TotalExperience5YearsOrMoreFTEColumn.ColumnName, Constants.FORMAT_RATE_01);

                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row,
                    teacherQualDataSet._v_TeacherQualifications.
                    TotalExperience5YearsOrMoreFTEPercentageColumn.ColumnName, 
                    Constants.FORMAT_RATE_01_PERC);
                
                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row,
                    teacherQualDataSet._v_TeacherQualifications.
                    DegreeMastersOrHigherFTEColumn.ColumnName, Constants.FORMAT_RATE_01);
                
                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row,
                    teacherQualDataSet._v_TeacherQualifications.
                    DegreeMastersOrHigherFTEPercentageColumn.ColumnName, 
                    Constants.FORMAT_RATE_01_PERC);
                
                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row,
                    teacherQualDataSet._v_TeacherQualifications.
                    EHQYesFTEColumn.ColumnName, Constants.FORMAT_RATE_01);
                
                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row,
                    teacherQualDataSet._v_TeacherQualifications.
                    EHQYesFTEPercentageColumn.ColumnName, Constants.FORMAT_RATE_01_PERC);
                
                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row,
                    teacherQualDataSet._v_TeacherQualifications.
                    EHQNoFTEColumn.ColumnName, Constants.FORMAT_RATE_01);
                
                SligoDataGrid2.SetCellToFormattedDecimal(
                    e.Row,
                    teacherQualDataSet._v_TeacherQualifications.
                    EHQNoFTEPercentageColumn.ColumnName, Constants.FORMAT_RATE_01_PERC);

                SetOrgLevelRowLabels(teachersBL, SligoDataGrid2, e.Row);

                //FormatHelper formater = new FormatHelper();
                //formater.SetRaceAbbr(SligoDataGrid2, e.Row, 
                //  teacherQualDataSet._v_TeacherQualifications.RaceLabelColumn.ToString());

            }
        }

        public override DataSet GetDataSet()
        {
            return teacherQualDataSet;
        }

    }
}
