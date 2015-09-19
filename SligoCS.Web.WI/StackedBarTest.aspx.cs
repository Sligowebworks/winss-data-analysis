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
using SligoCS.Web.WI.WebUserControls;


namespace SligoCS.Web.WI
{
    public partial class StackedBarTest :SligoCS.Web.Base.PageBase.WI.PageBaseWI
    {
        protected override SligoCS.DAL.WI.DALWIBase InitDatabase()
        {
            return new SligoCS.DAL.WI.DALHSCompletion();
        }
        protected override ChartFX.WebForms.Chart InitGraph()
        {
            return barChart;
        }
        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
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

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SetUpChart_Stacked(DataSet);
        }
        private void SetUpChart_Stacked(DataSet _ds)
        {

            //stacked bars
            //barChart.Data.Series = 3;
            //barChart.Data.Points = _ds.Tables[0].Rows.Count;
            barChart.AxisYMin = 0;
            barChart.AxisYMax = 100;
            barChart.AxisYStep = 10;
                
            barChart.Type = SligoCS.Web.WI.WebUserControls.GraphBarChart.StackedType.Stacked100;
            barChart.DataGrid.Visible = true;

            barChart.Gallery = ChartFX.WebForms.Gallery.Bar;
            

            
                //barChart.AllSeries.Stacked = ChartFX.WebForms.Stacked.Normal;
                //for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                //{
                //    barChart.Data[0, i] = Convert.ToDouble(_ds.Tables[0].Rows[i][v_HSCWWoDisSchoolDistStateEconELP.Regular_Diplomas].ToString());
                //    barChart.Data[1, i] = Convert.ToDouble(_ds.Tables[0].Rows[i][v_HSCWWoDisSchoolDistStateEconELP.HSEDs].ToString());
                //    barChart.Data[2, i] = Convert.ToDouble(_ds.Tables[0].Rows[i][v_HSCWWoDisSchoolDistStateEconELP.Certificates].ToString());
                //    barChart.AxisX.Labels[i] = _ds.Tables[0].Rows[i][v_HSCWWoDisSchoolDistStateEconELP.YearFormatted].ToString();
                //}

                //Double srs0, srs1, srs2;
                //srs0 = GraphBarChart.GetMaxRateInColumn(_ds.Tables[0], v_HSCWWoDisSchoolDistStateEconELP.Regular_Diplomas);
                //srs1 = GraphBarChart.GetMaxRateInColumn(_ds.Tables[0], v_HSCWWoDisSchoolDistStateEconELP.HSEDs);
                //srs2 = GraphBarChart.GetMaxRateInColumn(_ds.Tables[0], v_HSCWWoDisSchoolDistStateEconELP.Certificates);
                //barChart.MaxRateInResult =
                //    (srs2 > srs1) ?
                //        ((srs2 > srs0) ?
                //            srs2
                //            : srs0)
                //        : (srs1 > srs0) ?
                //             srs1
                //             : srs0
                //;

                //throw new Exception(barChart.MaxRateInResult.ToString());

                //barChart.Series[0].Text = "Regular Diploma";
                //barChart.Series[1].Text = "HSED";
                //barChart.Series[2].Text = "Certificates";
            
        }
        public override void  DataBindGraph(ChartFX.WebForms.Chart graph, DataSet ds)
        {
            DataTable tbl = ds.Tables[0];
                
            if (GlobalValues.HighSchoolCompletion.Key == HighSchoolCompletionKeys.All)
            {//Stacked Bar Chart
                List<String> myCols = new List<string>();
                myCols.Add(barChart.LabelColumnName);
                myCols.Add(v_HSCWWoDisSchoolDistStateEconELP.Regular_Diplomas);
                myCols.Add(v_HSCWWoDisSchoolDistStateEconELP.HSEDs);
                myCols.Add(v_HSCWWoDisSchoolDistStateEconELP.Certificates);
                tbl = GraphBarChart.RemoveUnusedColumns(tbl, myCols);

                //ds.Tables[0].Columns[v_HSCWWoDisSchoolDistStateEconELP.Regular_Diplomas].ColumnName = "Regular Diploma";
                //ds.Tables[0].Columns[v_HSCWWoDisSchoolDistStateEconELP.HSEDs].ColumnName = "HSED";
                //ds.Tables[0].Columns[v_HSCWWoDisSchoolDistStateEconELP.Certificates]
            }

            base.DataBindGraph(graph, tbl.DataSet);
        }
    }
}
