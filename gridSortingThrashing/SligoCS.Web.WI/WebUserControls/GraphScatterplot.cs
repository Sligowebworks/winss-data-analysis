using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections;
using ChartFX.WebForms;
using SligoCS.BL.WI;

namespace SligoCS.Web.WI.WebUserControls
{
    /// <summary>
    /// Summary description for GraphScatterplot
    /// </summary>
    public class GraphScatterplot : ChartFX.WebForms.Chart
    {
        private DataTable _rawDataSource;
        private string _title = string.Empty;

        private String _xValueColumnName = String.Empty;
        private String _yValueColumnName = String.Empty;

        private int _axisXMin = 0;
        private int _axisXStep = 10;
        private int _axisXMax = 100;
        private int _axisYMin = 0;
        private int _axisYStep = 10;
        private int _axisYMax = 100;
        private ArrayList _friendlySeriesName = new ArrayList();

        private string _axisXDescription = string.Empty;
        private string _axisYDescription = string.Empty;

        private ArrayList _color = new ArrayList();

        #region Public Properties

        //Required
        //This dataset is queried from db, we need to pivot this dataset because this dataset's structure can't meet chart displaying
        public DataTable RawDataSource
        {
            get { return _rawDataSource; }
            set
            {
                _rawDataSource = value;
                int pointIndex = 0;
                int seriesNumber = 1;
                for (int i = 0; i < _rawDataSource.Rows.Count; i++)
                {
                    double xValue = 0;
                    double yValue = 0;
                    try
                    {
                       xValue = Convert.ToDouble(_rawDataSource.Rows[i][_xValueColumnName]);
                       yValue = Convert.ToDouble(_rawDataSource.Rows[i][_yValueColumnName]);
                    }
                    catch
                    {
                        //Convert failed, don't display this point
                        continue;
                    }
                    seriesNumber = 1;
                    this.Points[pointIndex].MarkerSize = 2;
                    if (i == 0)
                    {
                        seriesNumber = 0;
                        this.Points[pointIndex].MarkerSize = 5;
                    }
                    this.Data.X[seriesNumber, pointIndex] = xValue;
                    this.Data.Y[seriesNumber, pointIndex] = yValue;
                    pointIndex++;
                }
            }
        }

        //Required
        public String Title
        {
            get { return _title; }
            set
            {
                _title = value;
                TitleDockable chartTitle = new TitleDockable(_title);
                chartTitle.Font = new System.Drawing.Font(chartTitle.Font, System.Drawing.FontStyle.Bold);
                chartTitle.Separation = 10;
                this.Titles.Add(chartTitle);
            }
        }

        //Mandatory
        //Column name for X-Axis value
        public String XValueColumnName
        {
            get { return _xValueColumnName; }
            set { _xValueColumnName = value; }
        }

        //Mandatory
        //Column name for Y-Axis value
        public String YValueColumnName
        {
            get { return _yValueColumnName; }
            set { _yValueColumnName = value; }
        }

        //Optional
        //Min value of X-Axis
        public int AxisXMin
        {
            get { return _axisXMin; }
            set
            {
                _axisXMin = value;
                this.AxisX.Min = _axisXMin;
            }
        }

        //Optional
        //Step of X-Axis
        public int AxisXStep
        {
            get { return _axisXStep; }
            set
            {
                _axisXStep = value;
                this.AxisX.Step = _axisXStep;
            }
        }

        //Optional
        //Max value of X-Axis
        public int AxisXMax
        {
            get { return _axisXMax; }
            set
            {
                _axisXMax = value;
                this.AxisX.Max = _axisXMax;
            }
        }

        //Optional
        //Min value of Y-Axis
        public int AxisYMin
        {
            get { return _axisYMin; }
            set
            {
                _axisYMin = value;
                this.AxisY.Min = _axisYMin;
            }
        }

        //Optional
        //Step of Y-Axis
        public int AxisYStep
        {
            get { return _axisYStep; }
            set
            {
                _axisYStep = value;
                this.AxisY.Step = _axisYStep;
            }
        }

        //Optional
        //Max value of Y-Axis
        public int AxisYMax
        {
            get { return _axisYMax; }
            set
            {
                _axisYMax = value;
                this.AxisY.Max = _axisYMax;
            }
        }

        //Optional
        //Customize friendly series name (series name are displayed in the legend box)
        public ArrayList FriendlySeriesName
        {
            get { return _friendlySeriesName; }
            set
            {
                _friendlySeriesName = value;
                for (int i = 0; i < _friendlySeriesName.Count; i++)
                {
                    this.Series[i].Text = _friendlySeriesName[i].ToString();
                }
            }
        }

        //Optional
        //Additional description for Y-Axis
        public string AxisYDescription
        {
            get { return _axisYDescription; }
            set
            {
                _axisYDescription = value;
                this.AxisY.Title.Text = _axisYDescription;
            }
        }

        //Optional
        //Additional description for X-Axis
        public string AxisXDescription
        {
            get { return _axisXDescription; }
            set
            {
                _axisXDescription = value;
                this.AxisX.Title.Text = _axisXDescription;
            }
        }

        //Optional
        //We already provide the default color for displaying chart, but you still can customize your special color via this property  
        public ArrayList Color
        {
            get
            {
                if (_color.Count == 0)
                {
                    //Default color
                    _color.Add(System.Drawing.Color.FromArgb(255, 0, 0));
                    _color.Add(System.Drawing.Color.FromArgb(125, 125, 125));

                }
                return _color;
            }
            set { _color = value; }

        }

        #endregion


        public GraphScatterplot()
            : base()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.Gallery = Gallery.Scatter;
            this.Width = 460;
            this.Height = 370;

            this.LegendBox.Dock = ChartFX.WebForms.DockArea.Bottom;
            this.LegendBox.PlotAreaOnly = false;
            this.LegendBox.Border = 0;
            this.LegendBox.ContentLayout = ContentLayout.Spread;
            this.LegendBox.Height = 60;
            this.LegendBox.Width = 460;
            this.LegendBox.MarginX = 0;
            this.LegendBox.MarginY = 5;
            this.LegendBox.AutoSize = false;

            RollOverSeriesSeqInLengendBox();

            this.AxisX.Grids.Major.Visible = false;

            ChartFX.WebForms.Adornments.SolidBackground solidBackground =
                new ChartFX.WebForms.Adornments.SolidBackground();
            solidBackground.Color = System.Drawing.Color.White;

            ChartFX.WebForms.Adornments.SimpleBorder simpleBorder =
                new ChartFX.WebForms.Adornments.SimpleBorder();
            simpleBorder.Color = System.Drawing.Color.Black;

            this.Background = solidBackground;
            this.Border = simpleBorder;
        }

        private void RollOverSeriesSeqInLengendBox()
        {
            this.LegendBox.ItemAttributes[this.Series].Visible = false;
            for (int i = this.Series.Count - 1; i >= 0; i--)
            {
                CustomLegendItem ci = new CustomLegendItem();
                ci.Attributes = this.Series[i];
                this.LegendBox.CustomItems.Add(ci);
            }
        }

        public override void DataBind()
        {
            for (int i = 0; i < (this.Series.Count < Color.Count ? this.Series.Count : Color.Count); i++)
            {
                this.Series[i].Color = (System.Drawing.Color)Color[i];
            }
            base.DataBind();
        }
    }
}
