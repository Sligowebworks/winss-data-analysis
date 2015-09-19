/*using System;
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
*/

using System;
using System.Data;
using System.Collections.Generic;
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
using SligoCS.Web.Base.PageBase.WI;

using SligoCS.Web.WI.WebSupportingClasses;
using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.Web.WI.WebUserControls
{
    /// <summary>
    /// Summary description for GraphScatterplot
    /// </summary>
    public class GraphScatterplot : ChartFX.WebForms.Chart
    {
        private string _title = string.Empty;

        public double[] SeriesByStrata= null;

        private String _xValueColumnName = String.Empty;
        private String _yValueColumnName = String.Empty;
        private String seriesColName = String.Empty;

        private int _axisXMin = 0;
        private int _axisXStep = 10;
        private int _axisXMax = 100;
        private int _axisYMin = 0;
        private int _axisYStep = 10;
        private int _axisYMax = 100;
        private ArrayList _friendlySeriesName = new ArrayList();
        private Hashtable customSeriesLabelsMap;

        private string _axisXDescription = string.Empty;
        private string _axisYDescription = string.Empty;

        private ArrayList _color = new ArrayList();

        #region Public Properties
        /// <summary>
        /// Supply a Name-Value collection to Override the Series labels.
        /// Name is the original label, and Value will replace it.
        /// </summary>
        public Hashtable OverrideSeriesLabels
        {
            get { return customSeriesLabelsMap; }
            set { customSeriesLabelsMap = value; }
        }

        public override object DataSource
        {
            get { return base.DataSource; }
            set
            {  //This dataset is queried from db, we need to pivot this dataset because this dataset's structure can't meet chart displaying

                DataTable dt =
                    (value is DataTable) ? (DataTable)value
                    : (value is DataSet) ? ((DataSet)value).Tables[0]
                    : (value is DataView) ? ((DataView)value).Table
                    : null;
                if (dt == null) throw new Exception("DataSource is not valid");

                QueryMarshaller qm = ((PageBaseWI)Page).QueryMarshaller;

                dt = SligoCS.Web.WI.WebSupportingClasses.WI.DataTableSorter.SortAndCompareSelectedFloatToTop(
                    dt, qm,
                    SligoCS.DAL.WI.SQLHelper.ConvertToCSV(qm.BuildOrderByList(dt.Columns), false),
                    ForceCurrentFloatToTopOrdering
                    );

                int pointIndex = 0;
                int seriesNumber = 1;
                int s = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double xValue, yValue, zValue;
                    xValue = yValue = zValue = 0;
                    Boolean discardX = false, discardY = false;

                    try { xValue = Convert.ToDouble(dt.Rows[i][_xValueColumnName]); }
                    catch { discardX = true; }
                    try { yValue = Convert.ToDouble(dt.Rows[i][_yValueColumnName]); }
                    catch {discardY = true     ;}
                    try { if (!String.IsNullOrEmpty(seriesColName)) zValue = Convert.ToDouble(dt.Rows[i][seriesColName]); }
                    catch {}
                    
                    
                    this.Points[pointIndex].MarkerSize = 2;
                    
                    if (SeriesByStrata == null || seriesColName == null) seriesNumber = 1;
                    else 
                    {// strata[0] should == 0
                        for (s = 1; s < SeriesByStrata.Length; s++)
                        {
                            if (
                                    (  zValue > SeriesByStrata[s - 1] || (zValue == 0 && s == 1)    )
                                    && zValue <= SeriesByStrata[s] 
                                )    
                                    break;
                            }// last strata is open ended, so if above condition is never true, then last strata is used.
                        seriesNumber = s;
                    }

                    if (i == 0)
                    {
                        s = seriesNumber;
                        seriesNumber = 0;
                        this.Points[pointIndex].MarkerSize = 5;
                        //set the marker of the first item
                        if (SeriesByStrata != null)
                            FirstMarker = (ChartFX.WebForms.MarkerShape)Markers[s];
                    }

                    if (!discardX)
                        this.Data.X[seriesNumber, pointIndex] = xValue;
                    if (!discardY)
                        this.Data.Y[seriesNumber, pointIndex] = yValue;

                    pointIndex++;
                }
                if (_friendlySeriesName != null)
                {
                    if (SeriesByStrata != null)
                    {  // ensure that all custom series labels are displayed, even if there is no data in the series.
                        while (Series.Count <= SeriesByStrata.Length)
                        {
                            Data.X[Series.Count, pointIndex++] = -5;   //add a hidden data point (off the grid)
                        }
                    }

                    for (int i = 0; i < Series.Count && i < _friendlySeriesName.Count ; i++)
                    {
                        this.Series[i].Text = _friendlySeriesName[i].ToString();
                    }
                }
                if (!Data.Y.HasData) this.Visible = false;
                //if (!Data.X.HasData) throw new Exception("No X");
                //if (!Data.Y.HasData) throw new Exception("No Y");

                GraphBarChart.OverrideSeriesColumnNames(dt, customSeriesLabelsMap);
                SetMarkersAndColors();
            }
        }
            public Boolean ForceCurrentFloatToTopOrdering = true;
        //Required
        public String Title
        {
            get { return _title; }
            set
            {
                _title = System.Web.HttpUtility.HtmlDecode(value);
                TitleDockable chartTitle = new TitleDockable(_title);
                chartTitle.Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Bold);
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

        public String SeriesColumnName
        {
            get { return seriesColName; }
            set { seriesColName = value; }
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
            set { _friendlySeriesName = value; }
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
                    _color = GetDefaultScatterPlotSeriesColors();
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
            this.Width = 580;
            this.Height = 370;

            System.Drawing.Font myFont = new System.Drawing.Font("Arial", 9);
            this.Font = myFont;

            this.MainPane.AxisY.LabelTrimming = System.Drawing.StringTrimming.EllipsisCharacter;
       
            this.LegendBox.Dock = ChartFX.WebForms.DockArea.Bottom;
            this.LegendBox.PlotAreaOnly = false;
            this.LegendBox.Border = 0;
            this.LegendBox.ContentLayout = ContentLayout.Spread;
            this.LegendBox.Height = 60;
            this.LegendBox.Width = 460;
            this.LegendBox.MarginX = 0;
            this.LegendBox.MarginY = 5;
            this.LegendBox.AutoSize = false;
            System.Drawing.Font legendFont = new System.Drawing.Font("Arial", 8);
            this.LegendBox.Font = legendFont;
            this.AxisX.Grids.Major.Visible = false;
            /* 
             //ht: commented out but do not delete 
             //since this code might be useful later in some other cases.
            CustomLegendItem custLegItem = new CustomLegendItem();
            custLegItem.Attributes.MarkerShape = MarkerShape.Diamond;
            custLegItem.Attributes.Color = System.Drawing.Color.Gray;
            custLegItem.Text = "Other Schools";
            LegendBox.CustomItems.Add(custLegItem);

            CustomLegendItem custLegItem1 = new CustomLegendItem();
            custLegItem1.Text = "Current School";
            custLegItem1.Attributes.Color = System.Drawing.Color.Red;
            custLegItem1.Attributes.MarkerShape = MarkerShape.Diamond;
            LegendBox.CustomItems.Add(custLegItem1);
            */
           // RollOverSeriesSeqInLengendBox();
           
            ChartFX.WebForms.Adornments.SolidBackground solidBackground =
                new ChartFX.WebForms.Adornments.SolidBackground();
            solidBackground.Color = System.Drawing.Color.White;
            ChartFX.WebForms.Adornments.SimpleBorder simpleBorder =
                new ChartFX.WebForms.Adornments.SimpleBorder();
            simpleBorder.Color = System.Drawing.Color.Black;

            this.Background = solidBackground;
            this.Border = simpleBorder;
        }

        void SetMarkersAndColors()
        {
            for (int i = 0; i < (this.Series.Count < Color.Count ? this.Series.Count : Color.Count); i++)
            {
                this.Series[i].Color = (System.Drawing.Color)Color[i];
                if (i < Markers.Count) Series[i].MarkerShape = (MarkerShape)Markers[i];
            }
        }
        private Hashtable markers;
        public Hashtable Markers
        {
            get
            {
                if (markers != null) 
                    return markers;
                else
                return GetAutoMarkers();
            }
            set { markers = value;}
        }
        public MarkerShape FirstMarker = MarkerShape.Marble;
        public Hashtable GetAutoMarkers()
        {
            Hashtable markers = new Hashtable();
            if (SeriesByStrata != null)
            {
                markers.Add(0, FirstMarker);
                markers.Add(1, MarkerShape.InvertedTriangle);
                markers.Add(2, MarkerShape.Diamond);
                markers.Add(3, MarkerShape.Rect);
                markers.Add(4, MarkerShape.Triangle);
                markers.Add(5, MarkerShape.Cross);
            }
            else
            {
                markers.Add(0, FirstMarker);
                markers.Add(1, MarkerShape.Marble);
            }
            return markers;
        }
        public static ArrayList GetDefaultScatterPlotSeriesColors()
        {
            ArrayList colors = new ArrayList();
            //colors.Add(System.Drawing.Color.FromArgb(248, 252, 70));

            // red
            colors.Add(System.Drawing.Color.FromArgb(255, 0, 0));
            //grey
            colors.Add(System.Drawing.Color.FromArgb(155, 155, 155));
            // light blue
            colors.Add(System.Drawing.Color.FromArgb(0, 255, 255));
            // orange
            colors.Add(System.Drawing.Color.FromArgb(255, 128, 0));
            // pink
            colors.Add(System.Drawing.Color.FromArgb(255, 0, 255));
            // primary blue
            colors.Add(System.Drawing.Color.FromArgb(0, 0, 255));
            // pale yellow
            colors.Add(System.Drawing.Color.FromArgb(255, 255, 0));
            // bright green
            colors.Add(System.Drawing.Color.FromArgb(0, 255, 0));
            // deep blue
            colors.Add(System.Drawing.Color.FromArgb(64, 0, 128));
            // purple
            colors.Add(System.Drawing.Color.FromArgb(128, 0, 64));
            // light green
            colors.Add(System.Drawing.Color.FromArgb(128, 255, 192));
            // light purple 
            colors.Add(System.Drawing.Color.FromArgb(192, 128, 255));
            // forest green
            colors.Add(System.Drawing.Color.FromArgb(3, 122, 100));
            // salmon
            colors.Add(System.Drawing.Color.FromArgb(255, 128, 192));
            // light orange
            colors.Add(System.Drawing.Color.FromArgb(255, 192, 128));
            // lime
            colors.Add(System.Drawing.Color.FromArgb(192, 255, 128));
            // lighter blue 
            colors.Add(System.Drawing.Color.FromArgb(128, 192, 255));
            // brown 
            colors.Add(System.Drawing.Color.FromArgb(128, 64, 0));

            return colors;
        }
    }
}
