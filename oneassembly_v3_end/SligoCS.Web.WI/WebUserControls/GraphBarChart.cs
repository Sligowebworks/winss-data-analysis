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
using SligoCS.BLL.WI;

namespace SligoCS.Web.WI.WebUserControls
{
   
    /// <summary>
    /// Summary description for GraphChartBar
    /// </summary>
    public partial class GraphBarChart : ChartFX.WebForms.Chart
    {
        private DataTable _rawDataSource;
        private BLWIBase _blBase;
        private string _title = string.Empty;

        private String _displayColumnName = String.Empty;
        private String _labelColumnName = String.Empty;
        private String _seriesColumnName = String.Empty;

        private ArrayList _friendlyAxisXName = new ArrayList();
        private ArrayList _friendlyAxisYName = new ArrayList();
        private ArrayList _friendlySeriesName = new ArrayList();

        private double _maxRateInResult = 0;

        private int _axisYStep = 1;
        private int _axisYMin = 0;
        private int _axisYMax = 10;
        private string _axisYDescription = string.Empty;
        private string _axisXDescription = string.Empty;

        private ArrayList _color = new ArrayList();

        #region Public Properties

        //Required
        //This dataset is queried from db, we need to pivot this dataset because this dataset's structure can't meet chart displaying
        public DataTable RawDataSource
        {
            get { return _rawDataSource; }
            set { 
                _rawDataSource = value; 

                //Convert the Raw DataSource to Chart DataSource
                if (String.Empty != _displayColumnName && 
                    String.Empty != _labelColumnName && 
                    String.Empty != _seriesColumnName)
                {
                    ChartDataSource = pivotDataSet(
                        _rawDataSource, _labelColumnName,
                        _seriesColumnName, _displayColumnName,
                        _friendlyAxisXName, _friendlySeriesName);
                }
            }
        }

        //This datset is used to display chart, usually this dataset is pivoted from raw dataset
        public DataTable ChartDataSource
        {
            get { return (DataTable)this.DataSourceSettings.DataSource; }
            set { this.DataSourceSettings.DataSource = value; }
        }

        //Required
        public String Title
        {
            get { return _title; }
            set {
                _title = value;
                this.Titles.Add(new TitleDockable(_title));
            }
        }

        //There are three types/style we used to display bar chart: Normal Bar Chart, Stacked Bar Chart, Stacked100 Bar Chart
        public StackedType Type
        {
            set 
            { 
                switch (value)
                {
                    case StackedType.No:
                        this.AllSeries.Stacked = Stacked.No;
                        break;
                    case StackedType.Normal:
                        this.AllSeries.Stacked = Stacked.Normal;
                        break;
                    case StackedType.Stacked100:
                        this.AllSeries.Stacked = Stacked.Stacked100;
                        break;
                }
            }
        }

        //Optional
        //This is the required parameter when we pivort raw dataset to chart dataset
        //We need to use View By clumn name and Compare TO column name for pivoting dataset
        //BLBase object can provide the override function to generate thse column names
        public BLWIBase BLBase
        {
            get { return _blBase; }
            set
            {
                _blBase = value;

                //Generate the View By and Compare To
                LabelColumnName = _blBase.GetViewByColumnName();
                SeriesColumnName = _blBase.GetCompareToColumnName();
                //FriendlySeriesName = getFriendlySeriesName(_blBase);
            }
        }

        //Required
        //You need to specify the column name you want to display
        public String DisplayColumnName
        {
            get { return _displayColumnName; }
            set { _displayColumnName = value; }
        }

        //Optional
        //Column name for display labels
        public String LabelColumnName
        {
            get { return _labelColumnName; }
            set { _labelColumnName = value; }
        }

        //Optional
        //Column name for displaying series
        public String SeriesColumnName
        {
            get { return _seriesColumnName; }
            set { _seriesColumnName = value; }
        }

        //Optional
        //Customize friendly labled name which displayed on X-Axis
        public ArrayList FriendlyAxisXName
        {
            get { return _friendlyAxisXName; }
            set { _friendlyAxisXName = value; }
        }

        //Optional
        //Customize friendly series name (series name are displayed in the legend box)
        public ArrayList FriendlySeriesName
        {
            get { return _friendlySeriesName; }
            set { _friendlySeriesName = value; }
        }


        //Optional
        public double MaxRateInResult
        {
            get { return _maxRateInResult; }
            set
            {
                _maxRateInResult = value;

                AxisYMin = 0;

                if (_maxRateInResult < 10)
                {
                    AxisYMax = 10;
                    AxisYStep = 1;
                }
                else if (_maxRateInResult < 20)
                {
                    AxisYMax = 20;
                    AxisYStep = 2;
                }

                else if (_maxRateInResult < 30)
                {
                    AxisYMax = 30;
                    AxisYStep = 3;
                }

                else if (_maxRateInResult < 50)
                {
                    AxisYMax = 50;
                    AxisYStep = 5;
                }
                else if (_maxRateInResult < 80)
                {
                    AxisYMax = 80;
                    AxisYStep = 10;
                }
                else if (_maxRateInResult < 100)
                {
                    AxisYMax = 100;
                    AxisYStep = 10;
                }
                else
                {
                    AxisYMax = 120;
                    AxisYStep = 10;
                }

                ArrayList _axisYNames = new ArrayList();
                for (int i = AxisYMin; i <= AxisYMax; i = i + AxisYStep)
                {
                    _axisYNames.Add(i.ToString() + "%");
                }
                FriendlyAxisYName = _axisYNames;

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
                if (_friendlyAxisYName.Count > 0)
                {
                    this.AxisY.LabelValue = _axisYStep;
                }
            }
        }

        //Optional
        //Min value of Y-Axis
        public int AxisYMin
        {
            get { return _axisYMin; }
            set{
                _axisYMin = value;
                this.AxisY.Min = _axisYMin;
                }
        }

        //Optional
        //Max value of Y-Axis
        public int AxisYMax
        {
            get { return _axisYMax; }
            set {
                _axisYMax = value;
                this.AxisY.Max = _axisYMax;
            }
        }

        //Optional
        //Customize each step name on Y-Axis
        public ArrayList FriendlyAxisYName
        {
            get { return _friendlyAxisYName; }
            set { 
                _friendlyAxisYName = value;
                for (int i = 0; i < _friendlyAxisYName.Count; i++)
                {
                    this.AxisY.Labels[i] = _friendlyAxisYName[i].ToString();
                }
                this.AxisY.LabelValue = AxisYStep;
            }
        }

        //Optional
        //Additional description for Y-Axis
        public string AxisYDescription
        {
            get { return _axisYDescription; }
            set {
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
            get {
                if (_color.Count == 0)
                {
                    //Default color
                    _color.Add(System.Drawing.Color.FromArgb(0, 146, 255));
                    _color.Add(System.Drawing.Color.FromArgb(255, 146, 255));
                    _color.Add(System.Drawing.Color.FromArgb(219, 36, 85));
                    _color.Add(System.Drawing.Color.FromArgb(146, 255, 170));
                    _color.Add(System.Drawing.Color.FromArgb(255, 146, 0));
                    _color.Add(System.Drawing.Color.FromArgb(0, 255, 170));
                    _color.Add(System.Drawing.Color.FromArgb(146, 255, 255));
                    _color.Add(System.Drawing.Color.FromArgb(255, 0, 170));
                    _color.Add(System.Drawing.Color.FromArgb(146, 255, 0));
                    _color.Add(System.Drawing.Color.FromArgb(146, 255, 255));
                    _color.Add(System.Drawing.Color.FromArgb(255, 255, 170));
                }
                return _color;
            }
            set { _color = value; }

        }

        #endregion

        public GraphBarChart()
            : base()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.Gallery = Gallery.Bar;
            this.Width = 460;
            this.Height = 370;
            
            this.LegendBox.Dock = ChartFX.WebForms.DockArea.Bottom;
            this.LegendBox.PlotAreaOnly = false;
            this.LegendBox.Border = 0;
            this.LegendBox.ContentLayout = ContentLayout.Near;
            this.LegendBox.Height = 60;
            this.LegendBox.Width = 460;
            this.LegendBox.MarginX = 0;
            this.LegendBox.MarginY = 5;
            this.LegendBox.AutoSize = false;

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

        public override void DataBind()
        {
            for (int i = 0; i < (this.Series.Count < Color.Count? this.Series.Count: Color.Count); i++)
            {
                this.Series[i].Color = (System.Drawing.Color)Color[i];
            }
            base.DataBind();
        }

        private bool Validate()
        {
            return true;
        }

        /// <summary>
        /// Convert raw dataset to chart dataset
        /// </summary>
        /// <param name="rawDS">Raw dataset</param>
        /// <param name="viewBy">View by column name, i.e. label names of axis X</param>
        /// <param name="compareTo">Compare to column name, i.e. series names </param>
        /// <param name="valueColumn">Column name used to displayed value</param>
        /// <param name="chartLabels">Friendly label names of axis X), you have to 
        /// leave it empty if you don't want to custmize the x-axis label name</param>
        /// <param name="chartSeries">Friendly series name (displayed in legend box), you have to  
        /// leave it empty if you don't want to custmize the series name</param>
        /// <returns>Chart dataset</returns>
        /// <remarks>
        /// Create chart dataset structure first before populate data.
        /// </remarks>
        private DataTable pivotDataSet(DataTable rawDS, 
            string viewBy, 
            string compareTo, 
            string valueColumn, 
            ArrayList chartLabels, 
            ArrayList chartSeries)
        {
            if (null == rawDS || String.Empty == viewBy || 
                String.Empty == compareTo || String.Empty == valueColumn)
            {
                return null;
            }
            DataTable chartTable = new DataTable();

            //Create Chart Table (Chart Dataset) Structure, the first column is for 
            // the label names of axis X, i.e. View by column names.
            DataColumn newColumn = new DataColumn(
                    viewBy, System.Type.GetType("System.String"));
            chartTable.Columns.Add(newColumn);

            //Hashtable is just used to help justify the unique value. 
            //The value for chart is in the ArrayList.
            //Unique values in label names of axis X, i.e. Compare To column are required for renderring graph, 
            //Also Unique values in Series name, i.e. View By column is required for renderring graph.
            Hashtable hashCompareTo = new Hashtable();
            ArrayList listCompareTo = new ArrayList();
            Hashtable hashViewBy = new Hashtable();
            ArrayList listViewBy = new ArrayList();

            foreach (DataRow row in rawDS.Rows)
            {
                if (!hashCompareTo.Contains(row[compareTo].ToString()))
                {
                    hashCompareTo.Add(row[compareTo].ToString(), 1);
                    listCompareTo.Add(row[compareTo].ToString());
                }


                if (!hashViewBy.Contains(row[viewBy].ToString()))
                {
                    hashViewBy.Add(row[viewBy].ToString(), 1);
                    listViewBy.Add(row[viewBy].ToString());
                }
            }

            bool useFriendlyLabel = false;
            bool useFriendSeries = false;
            if (listViewBy.Count == chartLabels.Count)
            {
                //Use user difined labels to replace the default list.
                useFriendlyLabel = true;
            }
            if (listCompareTo.Count == chartSeries.Count)
            {
                useFriendSeries = true;
            }

            //Create label names of axis X, i.e. Compare To columns
            if (useFriendSeries)
            {
                foreach (string compareColumnName in chartSeries)
                {
                    newColumn = new DataColumn(
                        compareColumnName, System.Type.GetType("System.Double"));
                    chartTable.Columns.Add(newColumn);
                }
            }
            else
            {
                foreach (string compareColumnName in listCompareTo)
                {
                    newColumn = new DataColumn(
                        compareColumnName, System.Type.GetType("System.Double"));
                    chartTable.Columns.Add(newColumn);
                }
            }

            //Fetch Data From The Raw DataSet
            for (int i = 0; i < listViewBy.Count; i++)
            {
                DataRow newRow = chartTable.NewRow();
                for (int j = 0; j < listCompareTo.Count + 1; j++)
                {
                    //The first column in each row in chart dataset is the Series name, i.e. View By value.
                    if (0 == j)
                    {
                        if (useFriendlyLabel)
                        {
                            newRow[j] = chartLabels[i];
                        }
                        else
                        {
                            newRow[j] = listViewBy[i];
                        }
                    }
                    else
                    {
                        string condition = string.Empty;        
   
                        // A simple quick query from raw dataset
                        condition = "[" + viewBy + "] = '" + listViewBy[i] +
                            "' and [" + compareTo + "] = '" + listCompareTo[j - 1] + "'";

                        DataRow[] rawRow = rawDS.Select(condition);

                        if (rawRow.Length > 0)
                        {
                            try
                            {
                                newRow[j] = Double.Parse(rawRow[0][valueColumn].ToString());
                            }
                            catch
                            {
                                //No value for displaying
                                newRow[j] = 0;
                            }
                        }

                    }
                }
                chartTable.Rows.Add(newRow);
            }
            chartTable.AcceptChanges();

            return chartTable;
        }

    }
}


