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
    /// This page is also known as the TeacherQualifications page.
    /// </summary>
    public partial class TeacherQualifications: PageBaseWI
    {
        protected override void OnInitComplete(EventArgs e)
        {
            GlobalValues.TrendStartYear = 2003;
            GlobalValues.CurrentYear = 2012;

            if (GlobalValues.TQShow.Key == TQShowKeys.ESEAQualified)
            {
                if (GlobalValues.TQSubjects.Key == TQSubjectsKeys.All)
                    GlobalValues.TQSubjects.Key = TQSubjectsKeys.Core;

                if (GlobalValues.TQSubjects.Key == TQSubjectsKeys.SpecEdSumm)
                    GlobalValues.TQSubjects.Key = TQSubjectsKeys.SpecEdCore;

                nlrSubject.LinkControlAdded += new LinkControlAddedHandler(nlrSubject_DisableSummaryForESEA);
            }

            if (GlobalValues.TQSubjects.Key == TQSubjectsKeys.All
                || GlobalValues.TQSubjects.Key == TQSubjectsKeys.SpecEdSumm)
            {
                if (GlobalValues.TQShow.Key == TQShowKeys.ESEAQualified)
                    GlobalValues.TQShow.Key = TQShowKeys.WisconsinLicenseStatus;

                nlrShow.LinkControlAdded += new LinkControlAddedHandler(nlrShow_LinkControlAdded_DisableESEAQualified);
            }

            //View By Group not supported:
            GlobalValues.Group.Value = GlobalValues.Group.Range[GroupKeys.All];

            //Force Compare-To Current for STYP-All:
            if (GlobalValues.STYP.Key == STYPKeys.AllTypes)
            {
                GlobalValues.CompareTo.Value = GlobalValues.CompareTo.Range[CompareToKeys.Current];
                nlrCompareTo.LinkRow.LinkControlAdded += new LinkControlAddedHandler(LinkRow_LinkControlAdded_DisableCompareToForSTYPALL);
            }

            base.OnInitComplete(e);
        }

        void nlrShow_LinkControlAdded_DisableESEAQualified(SligoCS.Web.Base.WebServerControls.WI.HyperLinkPlus link)
        {
            if (link.ID == "linkTQShowESEAQualified") link.Enabled = false;
        }
        void LinkRow_LinkControlAdded_DisableCompareToForSTYPALL(SligoCS.Web.Base.WebServerControls.WI.HyperLinkPlus link)
        {
            if (link.ID != "linkCompareToCurrent") link.Enabled = false;
        }
        void nlrSubject_DisableSummaryForESEA(SligoCS.Web.Base.WebServerControls.WI.HyperLinkPlus link)
        {
            if (link.ID == "linkTQSubjectsAll"
                || link.ID == "linkTQSubjectsSpecEdSumm") link.Enabled = false;
        }
        protected override DALWIBase InitDatabase()
        {
            return new DALTeacherQualifications();
        }
       protected override GridView InitDataGrid()
        {
            return TQDataGrid;
        }
        protected override ChartFX.WebForms.Chart InitGraph()
        {
            return barChart;
        }
        protected override string SetPageHeading()
        {
            return "What are the qualifications of teachers?";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSetTitle = GetTitle();

            TQDataGrid.AddSuperHeader(DataSetTitle);

            //Add a 2nd Super Header if Show = Wisconsin State License.
            TQDataGrid.AddSuperHeader(GetSecondSuperHeader());

            set_state();
            setBottomLink();

            if (GlobalValues.CompareTo.Key == CompareToKeys.Years)
            {
                List<String> order = new List<String>(QueryMarshaller.BuildOrderByList(DataSet.Tables[0].Columns).ToArray());
                order.Insert(0, v_TeacherQualifications.Year);
                barChart.OrderBy = String.Join(",", order.ToArray());
            }

            SetUpChart(DataSet);
        }

        private void SetUpChart(DataSet ds)
        {
            barChart.Title = DataSetTitle;

            barChart.AxisYMin = 0;
            barChart.AxisYMax = 100;
            barChart.AxisYStep = 10;
            barChart.AxisYDescription = "Percent of FTE Teachers";
            barChart.AxisY.LabelsFormat.CustomFormat = "0" + "\\%";

            //Bind Data Source & Display
            
            if (base.GlobalValues.TQShow.CompareToKey(TQShowKeys.WisconsinLicenseStatus))
            {
                //Stacked Bar Chart
                barChart.Type = SligoCS.Web.WI.WebUserControls.GraphBarChart.StackedType.Stacked100;

                barChart.LabelColumnName = ColumnPicker.GetCompareToColumnName(GlobalValues);

                if (GlobalValues.CompareTo.Key == CompareToKeys.SelSchools
                    || GlobalValues.CompareTo.Key == CompareToKeys.SelDistricts)
                {
                    barChart.Height = (int)(barChart.Height.Value * 1.25);
                    Hashtable hashXLabels = new Hashtable();
                    String col = barChart.LabelColumnName;
                    String oldLabel;
                    String newLabel;
                    String trace = String.Empty;

                    int start, end, space;
                    for (int n = 0; n < DataSet.Tables[0].Rows.Count; n++)
                    {
                        if (DataSet.Tables[0].Rows[n][col].ToString().Length > 30)
                        {
                            //Wrap Long Labels
                            oldLabel = DataSet.Tables[0].Rows[n][col].ToString();
                            if (!hashXLabels.Contains(oldLabel)
                                && oldLabel.Length > 25)
                            {
                                start = (int)(oldLabel.Length /4);
                                end = start * 3;
                                space = oldLabel.LastIndexOf(" ", end, end-start);
                                if (space < 0) space = oldLabel.LastIndexOf("-", end, end - start) + 1;
                                newLabel = oldLabel.Replace(oldLabel.Remove(space), oldLabel.Remove(space) +Convert.ToChar(10).ToString()).ToString();
                                hashXLabels.Add(DataSet.Tables[0].Rows[n][col].ToString(), newLabel);
                                trace += DataSet.Tables[0].Rows[n][col] + "::" + newLabel;
                            }
                        }
                    }
                    barChart.OverrideAxisXLabels = hashXLabels;
                    //throw new Exception(trace);
                    //WebUserControls.GraphBarChart.ReplaceColumnValues(DataSet.Tables[0], col, hashXLabels);
                }

                List<String> graphColumns = barChart.MeasureColumns;
                graphColumns.Add(v_TeacherQualifications.FTELicenseFull);
                graphColumns.Add(v_TeacherQualifications.LicenseEmerFTE);
                graphColumns.Add(v_TeacherQualifications.LicenseNoFTE);

                Hashtable seriesLabels = barChart.OverrideSeriesLabels;
                seriesLabels.Add(v_TeacherQualifications.FTELicenseFull, "Full License");
                seriesLabels.Add(v_TeacherQualifications.LicenseEmerFTE, "Emergency License");
                seriesLabels.Add(v_TeacherQualifications.LicenseNoFTE, "No License For Assignment");
            }
            else
            {
                //Normal Bar Chart
                barChart.Type = SligoCS.Web.WI.WebUserControls.GraphBarChart.StackedType.No;
                
               Dictionary<String, String> mapping = new Dictionary<string,string>();

                SligoCS.Web.WI.WebSupportingClasses.WI.TQShow TQ = GlobalValues.TQShow;

                mapping.Add(
                    TQShowKeys.DistrictExperience,
                   v_TeacherQualifications.LocalExperience5YearsOrMoreFTEPercentage
                    );
                mapping.Add(
                    TQShowKeys.TotalExperience,
                     v_TeacherQualifications.TotalExperience5YearsOrMoreFTEPercentage
                     );
                mapping.Add(
                    TQShowKeys.HighestDegree,
                    v_TeacherQualifications.DegreeMastersOrHigherFTEPercentage
                    );
                mapping.Add(
                    TQShowKeys.ESEAQualified,
                    (GlobalValues.TQSubjects.Key ==  TQSubjectsKeys.SoclStd) 
                        ? v_TeacherQualifications.ESEA_Core_HQYesFTEPercentage 
                        :  v_TeacherQualifications.EHQYesFTEPercentage
                    );

                barChart.DisplayColumnName = mapping[TQ.Key].ToString();
                barChart.FriendlyAxisXNames = new List<string>( new String[] {String.Empty });
            }
        }
       protected string GetTitle()
        {
            const string DELIM = WebSupportingClasses.TitleBuilder.newline;
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

            string title = typeOfData + DELIM 
                +( (GlobalValues.SuperDownload.Key!=SupDwnldKeys.True)
                 ? base.GlobalValues.TQSubjects.Key.Replace("_", " ")
                 :String.Empty);

           return base.GetTitleWithoutGroup(title);
        }
        private TableRow GetSecondSuperHeader()
        {
            TableRow tr = new TableRow();
            String show = base.GlobalValues.TQShow.Value;
            if (show == GlobalValues.TQShow.Range[TQShowKeys.WisconsinLicenseStatus])
            {

                WinssDataGrid.AddTableCell(tr, string.Empty, CountVisibleColumns() - 7);
                WinssDataGrid.AddTableCell(tr, "Total", 1);
                WinssDataGrid.AddTableCell(tr, "Full License", 2);
                WinssDataGrid.AddTableCell(tr, "Emergency License", 2);
                WinssDataGrid.AddTableCell(tr, "No License For Assignment", 2);

            }
            else if (show == GlobalValues.TQShow.Range[TQShowKeys.HighestDegree])
            {                 
                //tr = new TableRow();
                WinssDataGrid.AddTableCell(tr, string.Empty, 1);
                WinssDataGrid.AddTableCell(tr, "Total", 1);
                WinssDataGrid.AddTableCell(tr, "Masters or Higher", 2);
            }
            else if (show == GlobalValues.TQShow.Range[TQShowKeys.ESEAQualified])
            {
                // Total ESEA Qualified Not ESEA Qualified 
                //tr = new TableRow();
                WinssDataGrid.AddTableCell(tr, string.Empty, 1);
                WinssDataGrid.AddTableCell(tr, "Total", 1);
                WinssDataGrid.AddTableCell(tr, TQShowKeys.ESEAQualified, 2);
                WinssDataGrid.AddTableCell(tr, "Not ESEA Qualified", 2);
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
        private int CountVisibleColumns()
        {
            int retval = 0;
            foreach (DataControlField col in TQDataGrid.Columns)
            {
                if (col.Visible)
                    retval += 1;
            }
            return retval;
        }
        public override List<string> GetVisibleColumns()
        {
            TQShow show = GlobalValues.TQShow;

            List<string> retval = base.GetVisibleColumns();

            if (GlobalValues.TQShow.Key == TQShowKeys.ESEAQualified
                && GlobalValues.TQSubjects.Key == TQSubjectsKeys.SoclStd)
            {
                retval.Add(v_TeacherQualifications.ESEA_Core_FTE_Total);
            }
            else
            {
                retval.Add(v_TeacherQualifications.FTETotal);
            }

            bool statewide = (GlobalValues.SuperDownload.Key == SupDwnldKeys.True);

            if (statewide || show.Key == TQShowKeys.WisconsinLicenseStatus)
            {
                retval.Add(v_TeacherQualifications.FTELicenseFull);
                retval.Add(v_TeacherQualifications.LicenseFullFTEPercentage);
                retval.Add(v_TeacherQualifications.LicenseEmerFTE);
                retval.Add(v_TeacherQualifications.LicenseEmerFTEPercentage);
                retval.Add(v_TeacherQualifications.LicenseNoFTE);
                retval.Add(v_TeacherQualifications.LicenseNoFTEPercentage);
            }

            if (statewide || show.Key == TQShowKeys.DistrictExperience)
            {
                retval.Add(v_TeacherQualifications.LocalExperience5YearsOrLessFTE);
                retval.Add(v_TeacherQualifications.LocalExperience5YearsOrMoreFTE);
                retval.Add(v_TeacherQualifications.LocalExperience5YearsOrMoreFTEPercentage);
            }

            if (statewide || show.Key == TQShowKeys.TotalExperience)
            {
                retval.Add(v_TeacherQualifications.TotalExperience5YearsOrLessFTE);
                retval.Add(v_TeacherQualifications.TotalExperience5YearsOrMoreFTE);
                retval.Add(v_TeacherQualifications.TotalExperience5YearsOrMoreFTEPercentage);
            }

            if (statewide || show.Key == TQShowKeys.HighestDegree)
            {
                retval.Add(v_TeacherQualifications.DegreeMastersOrHigherFTE);
                retval.Add(v_TeacherQualifications.DegreeMastersOrHigherFTEPercentage);
            }

            if (statewide || show.Key == TQShowKeys.ESEAQualified)
            {
                if (GlobalValues.TQSubjects.Key == TQSubjectsKeys.SoclStd)
                {
                    retval.Add(v_TeacherQualifications.FTE_ESEACore_HQYes);
                    retval.Add(v_TeacherQualifications.ESEA_Core_HQYesFTEPercentage);
                    retval.Add(v_TeacherQualifications.FTE_ESEACore_HQNo);
                    retval.Add(v_TeacherQualifications.ESEA_Core_HQNoFTEPercentage);
                }
                else
                {
                    retval.Add(v_TeacherQualifications.FTE_ESEA_HQYes);
                    retval.Add(v_TeacherQualifications.EHQYesFTEPercentage);
                    retval.Add(v_TeacherQualifications.FTE_ESEA_HQNo);
                    retval.Add(v_TeacherQualifications.EHQNoFTEPercentage);
                }
            }
            return retval;
        }

        public override List<string> GetDownloadRawVisibleColumns()
        {
            List<String> cols = base.GetDownloadRawVisibleColumns();

            if (GlobalValues.SuperDownload.Key == SupDwnldKeys.True)
            {
                try
                {
                    cols.Insert(cols.IndexOf(v_TeacherQualifications.FTETotal),
                        v_TeacherQualifications.LinkSubjectLabel);
                }
                catch
                {
                    cols.Add(v_TeacherQualifications.LinkSubjectLabel);
                }
            }

            return cols;
        }

        protected override SortedList<string, string> GetDownloadRawColumnLabelMapping()
        {
            SortedList<string, string> newLabels = base.GetDownloadRawColumnLabelMapping();
            newLabels.Add(v_TeacherQualifications.FTETotal, "total_number_of_fte_teachers");
            newLabels.Add(v_TeacherQualifications.FTELicenseFull, "full_license_number_of_fte");
            newLabels.Add(v_TeacherQualifications.LicenseFullFTEPercentage, "full_license_percentage_of_total");
            newLabels.Add(v_TeacherQualifications.LicenseEmerFTE, "emergency_license_number_of_fte");
            newLabels.Add(v_TeacherQualifications.LicenseEmerFTEPercentage, "emergency_license_percentage_of_total");
            newLabels.Add(v_TeacherQualifications.LicenseNoFTE, "no_license_number_of_fte");
            newLabels.Add(v_TeacherQualifications.LicenseNoFTEPercentage, "no_license_percentage_of_total");

            newLabels.Add(v_TeacherQualifications.LocalExperience5YearsOrLessFTE, "number_fte_with_less_than_5_years_experience_in_this_district");
            newLabels.Add(v_TeacherQualifications.LocalExperience5YearsOrMoreFTE, 
"number_fte_with_at_least_5_years_experience_in_this_district");
            newLabels.Add(v_TeacherQualifications.LocalExperience5YearsOrMoreFTEPercentage, "percentage_with_at_least_5_years_experience_in_this_district");
            newLabels.Add(v_TeacherQualifications.TotalExperience5YearsOrLessFTE, "number_fte_with_less_than_5_years_total_experience");
            newLabels.Add(v_TeacherQualifications.TotalExperience5YearsOrMoreFTE, "number_fte_with_at_least_5_years_total_experience");
            newLabels.Add(v_TeacherQualifications.TotalExperience5YearsOrMoreFTEPercentage, "percentage_with_at_least_5_years_total_experience");
            newLabels.Add(v_TeacherQualifications.DegreeMastersOrHigherFTE, "masters_or_higher_number_fte");
            newLabels.Add(v_TeacherQualifications.DegreeMastersOrHigherFTEPercentage, "masters_or_higher_percentage_of_total");
            newLabels.Add(v_TeacherQualifications.FTE_ESEA_HQYes, "esea_qualified_number_fte");
            newLabels.Add(v_TeacherQualifications.EHQYesFTEPercentage, "esea_qualified_percentage_of_total");
            newLabels.Add(v_TeacherQualifications.FTE_ESEACore_HQYes, "esea_qualified_number_core _fte");
            newLabels.Add(v_TeacherQualifications.ESEA_Core_HQYesFTEPercentage, "esea_qualified_percentage_of_total_core");
            newLabels.Add(v_TeacherQualifications.FTE_ESEA_HQNo, "not_esea_qualified_number_fte");
            newLabels.Add(v_TeacherQualifications.FTE_ESEACore_HQNo, "not_esea_qualified_number_core_fte");
            newLabels.Add(v_TeacherQualifications.EHQNoFTEPercentage, "not_esea_qualified_percentage_of_total");
            newLabels.Add(v_TeacherQualifications.ESEA_Core_HQNoFTEPercentage, "not_esea_qualified_percentage_of_total_core");
            newLabels.Add(v_TeacherQualifications.LinkSubjectLabel, "subject_taught");
            return newLabels;
        }
    }
}
