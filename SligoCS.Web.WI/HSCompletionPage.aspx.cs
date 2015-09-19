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
using SligoCS.Web.Base.WebServerControls.WI;

using SligoCS.Web.WI.WebSupportingClasses.WI;
using SligoCS.Web.WI.WebSupportingClasses;
using SligoCS.Web.WI.WebUserControls;


namespace SligoCS.Web.WI
{
    public partial class HSCompletionPage : PageBaseWI
    {
        protected override DALWIBase InitDatabase()
        {
            return new DALHSCompletion();
        }
        protected override GridView InitDataGrid()
        {
            return this.DataGrid;
        }
        protected override ChartFX.WebForms.Chart InitGraph()
        {
            return barChart;
        }
        protected override NavViewByGroup InitNavRowGroups()
        {
            nlrViewByGroup.LinksEnabled = NavViewByGroup.EnableLinksVector.EconElp;
            return nlrViewByGroup;
        }
        protected override string SetPageHeading()
        {
            return "What are the high school completion rates?";
        }
        protected override void OnInitComplete(EventArgs e)
        {
            GlobalValues.Year = 2009;

            //STYP not supported (but don't loose school type at school level for titling purposes).
            if (GlobalValues.OrgLevel.Key != OrgLevelKeys.School )GlobalValues.OverrideSchoolTypeWhenOrgLevelIsSchool_Complete += PageBaseWI.DisableSchoolType;
           
            if (GlobalValues.HighSchoolCompletion.Key == HighSchoolCompletionKeys.All)
            {
                GlobalValues.TrendStartYear = 2004;
            }// Group Options only available when HighSchoolCompletion (Credential) <> All:
            else if (GlobalValues.Group.Key == GroupKeys.Disability)
            {
                GlobalValues.TrendStartYear = 2003;
            }
            else if (GlobalValues.Group.Key == GroupKeys.EconDisadv
                || GlobalValues.Group.Key == GroupKeys.EngLangProf)
            {
                GlobalValues.TrendStartYear = 2008;
            }

            GlobalValues.Grade.Key = GradeKeys.Grade_12;

            if (ViewByGroupOverrideRule()
                || GlobalValues.Group.Key == GroupKeys.Grade)
                GlobalValues.Group.Value = GlobalValues.Group.Range[GroupKeys.All];

            nlrViewByGroup.LinkRow.LinkControlAdded += ViewByLinkControlAdded;
            
            if (GlobalValues.OrgLevel.Key != OrgLevelKeys.School)
                GlobalValues.OverrideSchoolTypeWhenOrgLevelIsSchool_Complete += PageBaseWI.DisableSchoolType;

            base.OnInitComplete(e);
        }
       protected void Page_Load(object sender, EventArgs e)
        {
            String dataType = GlobalValues.HighSchoolCompletion.Key;
            if (GlobalValues.HighSchoolCompletion.Key == HighSchoolCompletionKeys.All)
            {
                dataType = "All Credential Types";
            }

            String prefix = "High School Completion Rate - " + dataType;

            this.DataGrid.AddSuperHeader(GetTitle(prefix));

            if (base.GlobalValues.DETAIL != null &&
                    base.GlobalValues.DETAIL.ToString() == "NO")
            {
                this.DataGrid.Visible = false;
            }
            set_state();
            
            //Notes:  For graph 
            DataSetTitle = GetTitle(prefix); //("High School Completion Rate");
            
            SetUpChart(DataSet);
            
            //  For graph 
        }
        public override void DataBindGraph(ChartFX.WebForms.Chart graph, DataSet ds)
        {
            DataTable tbl = ds.Tables[0];
            //don't use the default dataset
            if (GlobalValues.HighSchoolCompletion.Key == HighSchoolCompletionKeys.All)
            {//Stacked Bar Chart
                DataTable tempDs;
                List<String> myCols = new List<string>();
                myCols.Add(v_HSCWWoDisSchoolDistStateEconELP.Regular_Diplomas);
                myCols.Add(v_HSCWWoDisSchoolDistStateEconELP.HSEDs);
                myCols.Add(v_HSCWWoDisSchoolDistStateEconELP.Certificates);
                tempDs = GraphBarChart.GetMeasureColumnsAndCastAsDouble(ds.Tables[0], myCols);
                myCols.Clear(); myCols.Add(barChart.LabelColumnName);
                tbl = GraphBarChart.TransferColumnsBetweenDS(tbl, tempDs, myCols);
               
            }
           else
            {
                base.DataBindGraph(graph, ds);
            }
        }
        
        private void SetUpChart(DataSet _ds)
        {
            //try
            //{
            barChart.Title = DataSetTitle;

              /*   barChart.AxisY.LabelsFormat.Decimals = 1;
                 barChart.AxisY.DataFormat.Decimals = 1;
                 barChart.AxisY.LabelsFormat.Format = ChartFX.WebForms.AxisFormat.Percentage;
                 barChart.AxisY.DataFormat.Format = ChartFX.WebForms.AxisFormat.Percentage;
                 barChart.AxisY.ScaleUnit = 100;
                 barChart.AxisY.Step = 1;
                 barChart.AxisYMin = 0;
                 barChart.AxisYMax = 100;
              */   
                if (GlobalValues.Group.Key == GroupKeys.All)
                {
                    List<String> xname =  new List<String>();
                    xname.Add("All Students");
                    barChart.FriendlyAxisXNames = xname;
                }

                barChart.AxisYDescription = "Completion Rate\n" + (
                    (GlobalValues.HighSchoolCompletion.Key == HighSchoolCompletionKeys.All)
                    ? "All Credential Types" 
                    : GlobalValues.HighSchoolCompletion.Key.ToString()
                    );
                
               if (GlobalValues.HighSchoolCompletion.Key == HighSchoolCompletionKeys.All)
                    SetUpChart_Stacked(_ds);
               else
                    SetUpChart_Bar(_ds);

            //}
            //catch (Exception ex)
            //{
            //    //System.Diagnostics.Debug.WriteLine(ex.Message);
            //}
        }
        private void SetUpChart_Stacked(DataSet _ds)
        {
            barChart.AxisY.LabelsFormat.Decimals = 1;
            barChart.AxisY.DataFormat.Decimals = 1;
            barChart.AxisY.LabelsFormat.Format = ChartFX.WebForms.AxisFormat.Percentage;
            barChart.AxisY.DataFormat.Format = ChartFX.WebForms.AxisFormat.Percentage;
            barChart.AxisY.ScaleUnit = 100;
            barChart.AxisY.Step = 1;
            barChart.AxisYMin = 0;
            barChart.AxisYMax = 100;
            barChart.Type = GraphBarChart.StackedType.Normal;
            // AxisX.Labels set below:
            barChart.LabelColumnName = v_HSCWWoDisSchoolDistStateEconELP.OrgLevelLabel;
            barChart.Data.Points = _ds.Tables[0].Rows.Count;

             
               for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
               {
                    barChart.Data[0, i] = ConvertToDouble(_ds.Tables[0].Rows[i], v_HSCWWoDisSchoolDistStateEconELP.Regular_Diplomas);
                    barChart.Data[1, i] = ConvertToDouble(_ds.Tables[0].Rows[i], v_HSCWWoDisSchoolDistStateEconELP.HSEDs);
                   barChart.Data[2, i] = ConvertToDouble(_ds.Tables[0].Rows[i], v_HSCWWoDisSchoolDistStateEconELP.Certificates);

                   if (GlobalValues.CompareTo.Key == CompareToKeys.Years)
                       barChart.AxisX.Labels[i] = _ds.Tables[0].Rows[i][v_HSCWWoDisSchoolDistStateEconELP.YearFormatted].ToString();
                   else if (GlobalValues.CompareTo.Key == CompareToKeys.SelDistricts || GlobalValues.CompareTo.Key == CompareToKeys.SelSchools)
                       barChart.AxisX.Labels[i] = _ds.Tables[0].Rows[i][v_HSCWWoDisSchoolDistStateEconELP.Name].ToString();
                   else if (GlobalValues.CompareTo.Key == CompareToKeys.Current)
                       barChart.AxisX.Labels[i] = _ds.Tables[0].Rows[i][GraphBarChart.GetLabelsColumnDefault(GlobalValues)].ToString();
                   else
                       barChart.AxisX.Labels[i] = _ds.Tables[0].Rows[i][v_HSCWWoDisSchoolDistStateEconELP.OrgLevelLabel].ToString();
                }
               // Double srs0, srs1, srs2;
               // srs0 = GraphBarChart.GetMaxRateInColumn(_ds.Tables[0], v_HSCWWoDisSchoolDistStateEconELP.Regular_Diplomas);
              //  srs1 = GraphBarChart.GetMaxRateInColumn(_ds.Tables[0], v_HSCWWoDisSchoolDistStateEconELP.HSEDs);
              //  srs2 = GraphBarChart.GetMaxRateInColumn(_ds.Tables[0], v_HSCWWoDisSchoolDistStateEconELP.Certificates);
                //barChart.MaxRateInResult =
                //    (srs2 > srs1)?
                //        ((srs2 > srs0)?
                //            srs2 
                //            :  srs0)
                //        : (srs1 > srs0)? 
                //             srs1
                //             : srs0
                //;
                //throw new Exception(barChart.MaxRateInResult.ToString());
               barChart.MaxRateInResult = 99;
                // HT: hardcode MaxRateInResult = 99 here because in mike's code, 
                //if MaxRateInResult <100 then display 0% to 100% along axis Y -scale label 
               barChart.Series[0].Text = "Regular Diploma";
               barChart.Series[1].Text = "HSED";
               barChart.Series[2].Text = "Certificates";
        }
        private Double ConvertToDouble(DataRow row, String colName)
        {
            try
            {
               return  Convert.ToDouble(row[colName].ToString());
            }
            catch
            {
                return 0;
            }
        }

        private void SetUpChart_Bar(DataSet _ds)
        {
            try
            {
                barChart.AxisY.LabelsFormat.Decimals = 1;
                barChart.AxisY.DataFormat.Decimals = 1;
                barChart.AxisY.LabelsFormat.Format = ChartFX.WebForms.AxisFormat.Percentage;
                barChart.AxisY.DataFormat.Format = ChartFX.WebForms.AxisFormat.Percentage;
                barChart.AxisY.ScaleUnit = 100;
                barChart.AxisY.Step = 1;
                barChart.AxisYMin = 0;
                if (GlobalValues.HighSchoolCompletion.Key == HighSchoolCompletionKeys.Certificate)
                    barChart.DisplayColumnName = v_HSCWWoDisSchoolDistStateEconELP.Certificates;
                else if (GlobalValues.HighSchoolCompletion.Key == HighSchoolCompletionKeys.HSED)
                    barChart.DisplayColumnName = v_HSCWWoDisSchoolDistStateEconELP.HSEDs;
                else if (GlobalValues.HighSchoolCompletion.Key == HighSchoolCompletionKeys.Regular)
                    barChart.DisplayColumnName = v_HSCWWoDisSchoolDistStateEconELP.Regular_Diplomas;
                else if (GlobalValues.HighSchoolCompletion.Key == HighSchoolCompletionKeys.Summary)
                    barChart.DisplayColumnName = v_HSCWWoDisSchoolDistStateEconELP.Combined;
               

                barChart.MaxRateInResult = GraphBarChart.GetMaxRateInColumn(_ds.Tables[0], barChart.DisplayColumnName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
       
        //// end graph 
        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.dataLinksPanel, true);
        }

        public override List<string> GetVisibleColumns(Group viewBy, OrgLevel orgLevel, CompareTo compareTo, STYP schoolType)
        {
            List<string> retval = base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);
            retval.Remove(WebSupportingClasses.ColumnPicker.CommonNames.SchooltypeLabel.ToString());
            retval.Add(v_HSCWWoDisSchoolDistStateEconELP.Total_Enrollment_Grade_12);
            retval.Add(v_HSCWWoDisSchoolDistStateEconELP.Total_Expected_to_Complete_High_School);
            retval.Add(v_HSCWWoDisSchoolDistStateEconELP.Cohort_Dropouts);
            retval.Add(v_HSCWWoDisSchoolDistStateEconELP.Students_Who_Reached_the_Maximum_Age);
            retval.Add(v_HSCWWoDisSchoolDistStateEconELP.Certificates);
            retval.Add(v_HSCWWoDisSchoolDistStateEconELP.HSEDs);
            retval.Add(v_HSCWWoDisSchoolDistStateEconELP.Regular_Diplomas);

            return retval;
        }

        protected void ViewByLinkControlAdded(HyperLinkPlus theLink)
        {
            if (theLink.ID != "linkGroupAll"
                && ViewByGroupOverrideRule())
                theLink.Enabled = false;

            if (theLink.ID == "linkGroupGrade") theLink.Enabled = false;
        }
        private bool ViewByGroupOverrideRule()
        {
            // Use UserValues so that previous calls don't result in different result
            return
                (GlobalValues.HighSchoolCompletion.Key == HighSchoolCompletionKeys.All
                && GlobalValues.CompareTo.Key != CompareToKeys.Current)
            ;
        }

        protected override List<string> GetDownloadRawVisibleColumns()
        {
            List<String> cols = base.GetDownloadRawVisibleColumns();
            int index = cols.IndexOf(v_HSCWWoDisSchoolDistStateEconELP.Cohort_Dropouts);
            cols.Insert(index, v_HSCWWoDisSchoolDistStateEconELP.Cohort_Dropouts_Count);

            index = cols.IndexOf(v_HSCWWoDisSchoolDistStateEconELP.Students_Who_Reached_the_Maximum_Age);
            cols.Insert(index, v_HSCWWoDisSchoolDistStateEconELP.Maximum_Aged_Count);
            
            index = cols.IndexOf(v_HSCWWoDisSchoolDistStateEconELP.Certificates);
            cols.Insert(index, v_HSCWWoDisSchoolDistStateEconELP.Certificate_Count);
            
            index = cols.IndexOf(v_HSCWWoDisSchoolDistStateEconELP.HSEDs);
            cols.Insert(index, v_HSCWWoDisSchoolDistStateEconELP.HSED_Count);
            
            index = cols.IndexOf(v_HSCWWoDisSchoolDistStateEconELP.Regular_Diplomas);
            cols.Insert(index, v_HSCWWoDisSchoolDistStateEconELP.Regular_Count);

            cols.Add(v_HSCWWoDisSchoolDistStateEconELP.Combined_Count);
            cols.Add(v_HSCWWoDisSchoolDistStateEconELP.Combined);
            
            return cols;
        }

        protected override SortedList<string, string> GetDownloadRawColumnLabelMapping()
        {
            SortedList<string, string> newLabels = base.GetDownloadRawColumnLabelMapping();
            newLabels.Add(v_HSCWWoDisSchoolDistStateEconELP.Cohort_Dropouts, "cohort_dropouts_percent");
            newLabels.Add(v_HSCWWoDisSchoolDistStateEconELP.Students_Who_Reached_the_Maximum_Age, "students_who_reached_the_maximum_age_percent");
            newLabels.Add(v_HSCWWoDisSchoolDistStateEconELP.Certificates, "certificates_percent");
            newLabels.Add(v_HSCWWoDisSchoolDistStateEconELP.HSEDs, "hseds_percent");
            newLabels.Add(v_HSCWWoDisSchoolDistStateEconELP.Regular_Diplomas, "regular_diplomas_percent");
            newLabels.Add(v_HSCWWoDisSchoolDistStateEconELP.Combined, "combined_percent");
            return newLabels;
        }
    }
}
