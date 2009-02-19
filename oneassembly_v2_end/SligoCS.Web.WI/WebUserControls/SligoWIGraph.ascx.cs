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
using ChartFX.WebForms;

using SligoCS.BL.WI;
using SligoCS.Web.WI.PageBase.WI;

namespace SligoCS.Web.WI.WebUserControls
{


    /////6/1/08 HT just copied and pasted below the old code for chartfx from SligoWIGraph.ascx in LISA
    public partial class SligoWIGraph : WIUserControlBase
    {
        private DataTable m_datatable;
        public int m_numpoints, m_numseries;

        private string zYear;
        private double m_axisYMin;
        private double m_axisYMax;

        protected global::SligoCS.Web.WI.WebServerControls.WI.ChartMain graph1;
  
        protected void Page_Load(object sender, EventArgs e)
        {
              
            BLRetention retention = new BLRetention();
            ((PageBaseWI)this.Page).PrepBLEntity(retention);
            this.MakeGraph(retention);
        }
        public DataTable DataTable
        {
            get { return m_datatable; }
            set { m_datatable = value; }
        }

        public void MakeGraph(BLRetention retention)
        {
            //DataSet ds = null;
            if (this.Page is PageBaseWI)
            {
             // ds = ((PageBaseWI)this.Page).DataSet;
            }
          //  graph1.DataSourceSettings.Fields.Add(new FieldMap(ds.v_RetentionWWoDisEconELPSchoolDistState.Retention_RateColumn.ColumnName, FieldUsage.Value));
           // graph1.DataSourceSettings.Fields.Add(new FieldMap(ds.v_RetentionWWoDisEconELPSchoolDistState.yearColumn.ColumnName, FieldUsage.Label));
          //  graph1.DataSourceSettings.DataSource = ds.v_RetentionWWoDisEconELPSchoolDistState;
            //test above lines
            graph1.Gallery = ChartFX.WebForms.Gallery.Bar;
            graph1.AxisY.DataFormat.Decimals = 0;
            graph1.Width = 460;
            graph1.Height = 370;
            graph1.AxisY.Title.Text = "Retention Rate"; // a function 2b done here
            graph1.LegendBox.Dock = ChartFX.WebForms.DockArea.Bottom;
            m_numseries = numseries(retention);
            m_numpoints = numpoints(retention);
            graph1.Data.Points = m_numpoints;
            graph1.Data.Series = m_numseries; /* series for school years */
            graphPointsLabel(retention);
            if (retention.ViewBy == ViewByGroup.AllStudentsFAY)
            {
                databind_AllStudentsFAY(retention);
            }
            if (retention.ViewBy == ViewByGroup.Gender)
            {
                databind_Gender(retention);
            }

            if (retention.ViewBy == ViewByGroup.Disability)
            {
                databind_Disability(retention);
            }

            if (retention.ViewBy == ViewByGroup.RaceEthnicity)
            {
                //databind_Ethnicity(retention);
            }

            if (retention.ViewBy == ViewByGroup.Grade)
            {
                //databind_Grade(retention);
            }
            seriescolors();
            findAxisYMin();
            findAxisYMax();
            graph1.AxisY.Min = m_axisYMin;
            graph1.AxisY.Max = m_axisYMax;
            graph1.AxisY.LabelsFormat.Format = AxisFormat.Percentage;
            graph1.LegendBox.Border = 0;
            graph1.LegendBox.AutoSize = false;
            graph1.LegendBox.Height = 60;
            graph1.LegendBox.Width = 600;
            graph1.LegendBox.MarginX = 38;
            graph1.LegendBox.ContentLayout = ContentLayout.Near;
            graph1.LegendBox.PlotAreaOnly = false;
            TitleDockable graph1Title;
            graph1Title = new TitleDockable();
            /*To do a function or class for title */
            graph1Title.Text = "Retention Rate" + "\n" + " DePeres " + "\n" + zYear + " Compared To Prior Years";
            graph1.Titles.Add(graph1Title);
            graph1Title.Font.Bold.Equals(true);
            graph1Title.Dock = DockArea.Top;
            graph1.Visible = true;
        }
        private void findAxisYMin()
        {
            m_axisYMin = 0.00;  // to add logic here   
        }

        private void findAxisYMax()
        {
            m_axisYMax = 0.50;  // to add logic here
        }

        private int numpoints(BLRetention retention)
        {
            int numpoints = 1;
            if (retention.ViewBy == ViewByGroup.RaceEthnicity)
            {
                numpoints = 5;
            }
            if (retention.ViewBy == ViewByGroup.Gender)
            {
                numpoints = 2;
            }
            if (retention.ViewBy == ViewByGroup.Grade)
            {
                numpoints = 13;
            }
            if (retention.ViewBy == ViewByGroup.Disability)
            {
                numpoints = 2;
            }
            if (retention.ViewBy == ViewByGroup.AllStudentsFAY)
            {
                numpoints = 1;
            }
            return numpoints;
        }

        private int numseries(BLRetention retention)
        {
            int numseries = 10;
            if (retention.ViewBy == ViewByGroup.RaceEthnicity)
            {
                numseries = 10;
            }
            if (retention.ViewBy == ViewByGroup.Gender)
            {
                numseries = 10;
            }
            if (retention.ViewBy == ViewByGroup.Grade)
            {
                numseries = 13;
            }
            if (retention.ViewBy == ViewByGroup.Disability)
            {
                numseries = 10;
            }
            if (retention.ViewBy == ViewByGroup.AllStudentsFAY)
            {
                numseries = 10;
            }
            return numseries;

        }

        private void graphPointsLabel(BLRetention retention)
        {

            if (retention.ViewBy == ViewByGroup.RaceEthnicity)
            {
                graph1.AxisX.Labels.Insert(0, "Am Ind");
                graph1.AxisX.Labels.Insert(1, "Asian");
                graph1.AxisX.Labels.Insert(2, "Black");
                graph1.AxisX.Labels.Insert(3, "Hisp");
                graph1.AxisX.Labels.Insert(4, "White");
            }

            if (retention.ViewBy == ViewByGroup.Gender)
            {
                graph1.AxisX.Labels.Insert(0, "Female");
                graph1.AxisX.Labels.Insert(1, "Male");
            }

            if (retention.ViewBy == ViewByGroup.Grade)
            {
                //graph1.AxisX.Font.Size = 8;
                //graph1.AxisX.LabelAngle = 50;
                graph1.Font = new System.Drawing.Font("Times News Roman", 8);
                graph1.AxisX.Labels.Insert(0, "Kindergarden");
                graph1.AxisX.Labels.Insert(1, "Grade 1");
                graph1.AxisX.Labels.Insert(2, "Grade 2");
                graph1.AxisX.Labels.Insert(3, "Grade 3");
                graph1.AxisX.Labels.Insert(4, "Grade 4");
                graph1.AxisX.Labels.Insert(5, "Grade 5");
                graph1.AxisX.Labels.Insert(6, "Grade 6");
                graph1.AxisX.Labels.Insert(7, "Grade 7");
                graph1.AxisX.Labels.Insert(8, "Grade 8");
                graph1.AxisX.Labels.Insert(9, "Grade 9");
                graph1.AxisX.Labels.Insert(10, "Grade 10");
                graph1.AxisX.Labels.Insert(11, "Grade 11");
                graph1.AxisX.Labels.Insert(12, "Grade 12");
            }

            if (retention.ViewBy == ViewByGroup.Disability)
            {
                graph1.AxisX.Labels.Insert(0, "Students with Disability");
                graph1.AxisX.Labels.Insert(1, "Students w/o Disability");
            }

            if (retention.ViewBy == ViewByGroup.AllStudentsFAY)
            {
                graph1.AxisX.Labels.Insert(0, "All Students");
            }

        }

        private void seriescolors()
        {
            for (int k = 0; k < m_numseries; k++)
            {
                graph1.Series[0].Color = System.Drawing.Color.FromArgb(0, 128, 255);
                if (k > 0)
                    graph1.Series[1].Color = System.Drawing.Color.FromArgb(233, 128, 233);
                if (k > 1)
                    graph1.Series[2].Color = System.Drawing.Color.FromArgb(223, 36, 73);
                if (k > 2)
                    graph1.Series[3].Color = System.Drawing.Color.FromArgb(128, 255, 128);
                if (k > 3)
                    graph1.Series[4].Color = System.Drawing.Color.FromArgb(255, 128, 0);
                if (k > 4)
                    graph1.Series[5].Color = System.Drawing.Color.FromArgb(0, 255, 128);
                if (k > 5)
                    graph1.Series[6].Color = System.Drawing.Color.FromArgb(128, 255, 255);
                if (k > 6)
                    graph1.Series[7].Color = System.Drawing.Color.FromArgb(255, 0, 128);
                if (k > 7)
                    graph1.Series[8].Color = System.Drawing.Color.FromArgb(128, 255, 0);
                if (k > 8)
                    graph1.Series[9].Color = System.Drawing.Color.FromArgb(128, 255, 233);
                if (k > 9)
                    graph1.Series[10].Color = System.Drawing.Color.FromArgb(128, 233, 255);
                if (k > 10)
                    graph1.Series[11].Color = System.Drawing.Color.FromArgb(128, 233, 0);
            }

        }

        private void databind_AllStudentsFAY(BLRetention retention)
        {
            double x;
            int count = m_datatable.Rows.Count;
            for (int k = 0; k < count; k++)
            {
                x = System.Convert.ToDouble(m_datatable.Rows[k]["Retention Rate"]) / 100;
                graph1.Data[k, 0] = x;
                graph1.Series[k].Text = m_datatable.Rows[k]["yearformatted"].ToString();
                int y = m_datatable.Rows.Count - 1;
                if (k == y)
                    zYear = m_datatable.Rows[k]["yearformatted"].ToString();
            }
        }

        private void databind_Gender(BLRetention retention)
        {
            double x;
            int count = m_datatable.Rows.Count;
            for (int p = 0; p < m_numpoints; p++) //p is ordinal index of points
            {
                for (int s = 0; s < m_numseries; s++)  // series ordinal position
                {
                    if (p == 0)
                    {
                        x = System.Convert.ToDouble(m_datatable.Rows[s * m_numpoints]["Retention Rate"]) / 100;
                    }
                    else
                    {
                        int rIndex;
                        rIndex = s * m_numpoints + 1;
                        x = System.Convert.ToDouble(m_datatable.Rows[rIndex]["Retention Rate"]) / 100;
                        graph1.Series[s].Text = m_datatable.Rows[s]["yearformatted"].ToString();
                    }
                    graph1.Data[s, p] = x;
                }
                zYear = m_datatable.Rows[count - 1]["yearformatted"].ToString();
            }
        }

        private void databind_Disability(BLRetention retention)
        {
            int count = m_datatable.Rows.Count;
            {
                for (int s = 0; s < m_numseries; s++)  // series ordinal position
                {
                    if (System.Convert.ToString(m_datatable.Rows[s * m_numpoints]["Retention Rate"]).Equals("*"))
                    {
                    }
                    else
                    {
                        graph1.Data[0, 0] = 0.0; //(System.Convert.ToDouble(m_datatable.Rows[0]["Retention Rate"].ToString()));
                        graph1.Data[0, 1] = 1.2; // (System.Convert.ToDouble(m_datatable.Rows[1]["Retention Rate"].ToString()));
                        graph1.Data[1, 0] = 2.4; // (System.Convert.ToDouble(m_datatable.Rows[2]["Retention Rate"].ToString()));    
                        graph1.Data[1, 1] = 0.3; // (System.Convert.ToDouble(m_datatable.Rows[3]["Retention Rate"].ToString()));
                        graph1.Series[0].Text = m_datatable.Rows[0]["yearformatted"].ToString();
                        graph1.Series[1].Text = m_datatable.Rows[1]["yearformatted"].ToString();
                        // graph1.Series[2].Text = m_datatable.Rows[2]["yearformatted"].ToString();
                        //graph1.Series[3].Text = m_datatable.Rows[3]["yearformatted"].ToString();
                        zYear = m_datatable.Rows[count - 1]["yearformatted"].ToString();
                    }
                } // closing } For loop
            }

        }
    }

}    
