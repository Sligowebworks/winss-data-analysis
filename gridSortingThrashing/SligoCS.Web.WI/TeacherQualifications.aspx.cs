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
    /// This page is also known as the TeacherQualifications page.
    /// </summary>
    public partial class TeacherQualifications: PageBaseWI
    {
        private string graphTitle = string.Empty;
        protected v_TeacherQualifications teacherQualDataSet = 
            new v_TeacherQualifications ();
        protected BLTeacherQualifications _qualifications;
        
        protected void Page_Load(object sender, EventArgs e)
        {

            Page.Master.EnableViewState = false;

            CheckSelectedSchoolOrDistrict(_qualifications);
            SetLinkChangeSelectedSchoolOrDistrict(ChangeSelectedSchoolOrDistrict);

            //set the page heading
            ((SligoCS.Web.WI.WI)Page.Master).SetPageHeading
                ("What are the qualifications of teachers?");

            teacherQualDataSet = 
                _qualifications.GetTeacherQualifications( 
                        base.GlobalValues.TQSubjects.Value );
            
            SetVisibleColumns2(SligoDataGrid2, 
                teacherQualDataSet, _qualifications.ViewBy, 
                _qualifications.OrgLevel, _qualifications.CompareTo,
                GlobalValues.STYP);
            
            GlobalValues.SQL = _qualifications.SQL;

            this.SligoDataGrid2.DataSource = teacherQualDataSet;

            graphTitle = GetTitle(
                _qualifications,
                GetRegionString());

            this.SligoDataGrid2.DataBind();
            SligoDataGrid2.AddSuperHeader (graphTitle);

            //Add a 2nd Super Header if Show = Wisconsin State License.
            SligoDataGrid2.AddSuperHeader(GetSecondSuperHeader());

            set_state();
            setBottomLink(_qualifications);

            ////Notes:  graph 
            GraphPanel.Visible = false;
            if (CheckIfGraphPanelVisible(GraphPanel) == true)
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
                
                if (base.GlobalValues.TQShow.CompareToKey(TQShowKeys.WisconsinLicenseStatus)
                    )
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
                    
                    Dictionary<String, String> mapping = new Dictionary<string,string>();

                    SligoCS.Web.WI.WebSupportingClasses.WI.TQShow TQ = GlobalValues.TQShow;

                    mapping.Add(
                        TQShowKeys.DistrictExperience,
                       ds._v_TeacherQualifications.LocalExperience5YearsOrMoreFTEPercentageColumn.ColumnName
                        );
                    mapping.Add(
                        TQShowKeys.TotalExperience,
                         ds._v_TeacherQualifications.TotalExperience5YearsOrMoreFTEPercentageColumn.ColumnName
                         );
                    mapping.Add(
                        TQShowKeys.HighestDegree,
                        ds._v_TeacherQualifications.DegreeMastersOrHigherFTEPercentageColumn.ColumnName
                        );
                    mapping.Add(
                        TQShowKeys.ESEAQualified,
                        ds._v_TeacherQualifications.EHQYesFTEPercentageColumn.ColumnName
                        );

                    displayColumn = TQ.Key;
                    friendlyAxisXName.Add(mapping[TQ.Key].ToString());
                     
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
            if (GlobalValues.STYP.Key == STYPKeys.AllTypes)
            {
                labelColumnName = ColumnPicker.GetViewByColumnName();
            }
            else
            {
                labelColumnName = ColumnPicker.GetCompareToColumnName();
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

        public string GetViewByColumnName()
        {
            if ((GlobalValues.OrgLevel.Key != OrgLevelKeys.School) && (GlobalValues.STYP == STYPKeys.AllTypes))
            {
                return "SchooltypeLabel2";
            }
            else
            {
                return ColumnPicker.GetViewByColumnName();
            }
        }

        private string GetTitle(
            BLWIBase entity, string regionString)
        {
            const string DELIM = @"<br/>";
            StringBuilder tmpHeader = new StringBuilder();

            string typeOfData = String.Empty;

            String tqShow = base.GlobalValues.TQShow.Value;
            if (tqShow == GlobalValues.TQShow.Range[TQShowKeys.WisconsinLicenseStatus])
            {
                typeOfData = "Wisconsin Teacher License Status";
            }
            else if (tqShow == GlobalValues.TQShow.Range[TQShowKeys.DistrictExperience])
            {
                typeOfData = "FTE Teachers with 5 or More Years of District Experience";
            }
            else if (tqShow == GlobalValues.TQShow.Range[TQShowKeys.TotalExperience])
            {
                typeOfData = "FTE Teachers with 5 or More Years of Total Experience";
            }
            else if (tqShow == GlobalValues.TQShow.Range[TQShowKeys.HighestDegree])
            {
                typeOfData = "Percent of FTE Teachers with Masters Degree or Higher";
            }
            else if (tqShow == GlobalValues.TQShow.Range[TQShowKeys.ESEAQualified])
            {
                typeOfData = "ESEA Qualified Teachers";
            }

            string title = typeOfData + DELIM +
                base.GlobalValues.TQSubjects.Key.Replace("_", " ") + DELIM +
                GlobalValues.GetOrgName() + ":" +
                TitleBuilder.GetSchoolTypeInTitle().Replace(@"<br/> ", " ") + DELIM +
                TitleBuilder.GetYearRangeInTitle(entity.Years) +
                TitleBuilder.GetCompareToInTitle(
                    entity.OrgLevel, entity.CompareTo,
                    GlobalValues.STYP, entity.S4orALL, 
                    GetRegionString());

            return title.Replace("Summary All Subjects", "Summary - All Subjects");
        }

        private TableRow GetSecondSuperHeader()
        {
            TableRow tr = new TableRow();
            String show = base.GlobalValues.TQShow.Value;
            if (show == GlobalValues.TQShow.Range[TQShowKeys.WisconsinLicenseStatus])
            {
                
                AddTableCell(tr, string.Empty, GetVisibleColumns() - 7);                
                AddTableCell(tr, "Total", 1);
                AddTableCell(tr, "Full License", 2);
                AddTableCell(tr, "Emergency License", 2);
                AddTableCell(tr, "No License For Assignment", 2);

            }
            else if (show == GlobalValues.TQShow.Range[TQShowKeys.HighestDegree])
            {                 
                //tr = new TableRow();
                AddTableCell(tr, string.Empty, 1);
                AddTableCell(tr, "Total", 1);
                AddTableCell(tr, "Masters or Higher", 2);
            }
            else if (show == GlobalValues.TQShow.Range[TQShowKeys.ESEAQualified])
            {
                // Total ESEA Qualified Not ESEA Qualified 
                //tr = new TableRow();
                AddTableCell(tr, string.Empty, 1);
                AddTableCell(tr, "Total", 1);
                AddTableCell(tr, TQShowKeys.ESEAQualified, 2);
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
            (Group viewBy, OrgLevel orgLevel, 
            CompareTo compareTo, STYP schoolType)
        {
            return GetVisibleColumns(viewBy, orgLevel, 
                compareTo, schoolType, base.GlobalValues.TQShow);
        }

        public List<string> GetVisibleColumns
            (Group viewBy, OrgLevel orgLevel, 
            CompareTo compareTo, STYP schoolType,
            TQShow show)
        {
            List<string> retval =  
                base.GetVisibleColumns(viewBy, orgLevel, 
                compareTo, schoolType);

            if (compareTo.Key == CompareToKeys.Years)
                retval.Add(teacherQualDataSet._v_TeacherQualifications.
                    YearFormattedColumn.ColumnName);           
            else if (compareTo.Key != CompareToKeys.Years)
            {
                retval.Add(teacherQualDataSet._v_TeacherQualifications.
                    OrgLevelLabelColumn.ColumnName);
            }

            if ((schoolType.Key != STYPKeys.AllTypes) &&
                (orgLevel.Key != OrgLevelKeys.School))
            {
                retval.Add(teacherQualDataSet._v_TeacherQualifications.
                    SchooltypeLabelColumn.ColumnName);
            }

            retval.Add(
                teacherQualDataSet._v_TeacherQualifications.
                        FTETotalColumn.ColumnName);


            if (show.Key == TQShowKeys.WisconsinLicenseStatus)
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
            else if (show.Key == TQShowKeys.DistrictExperience)
            {
                retval.Add(teacherQualDataSet._v_TeacherQualifications.
                    LocalExperience5YearsOrLessFTEColumn.ColumnName);
                retval.Add(teacherQualDataSet._v_TeacherQualifications.
                    LocalExperience5YearsOrMoreFTEColumn.ColumnName);
                retval.Add(teacherQualDataSet._v_TeacherQualifications.
                    LocalExperience5YearsOrMoreFTEPercentageColumn.ColumnName);  
            }
            else if (show.Key == TQShowKeys.TotalExperience)
            {
                retval.Add(teacherQualDataSet._v_TeacherQualifications.
                    TotalExperience5YearsOrLessFTEColumn.ColumnName);
                retval.Add(teacherQualDataSet._v_TeacherQualifications.
                    TotalExperience5YearsOrMoreFTEColumn.ColumnName);
                retval.Add(teacherQualDataSet._v_TeacherQualifications.
                    TotalExperience5YearsOrMoreFTEPercentageColumn.ColumnName);  
            }
            else if (show.Key == TQShowKeys.HighestDegree)
            {
                retval.Add(teacherQualDataSet._v_TeacherQualifications.
                    DegreeMastersOrHigherFTEColumn.ColumnName);
                retval.Add(teacherQualDataSet._v_TeacherQualifications.
                    DegreeMastersOrHigherFTEPercentageColumn.ColumnName);
            }
            else if (show.Key == TQShowKeys.ESEAQualified)
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

                SligoDataGrid2.SetOrgLevelRowLabels(GlobalValues, e.Row);

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
