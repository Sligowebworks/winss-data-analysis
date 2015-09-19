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
    public partial class GraphBarChart : ChartFX.WebForms.Chart
    {
        private string _title = string.Empty;

        private String _displayColumnName = String.Empty;
        private String _labelColumnName = String.Empty;
        private String _seriesColumnName = String.Empty;
        private List<String> _measureColumns = new List<String>();

        private List<String> _friendlyAxisXNames = new List<String>();
        private List<String> _friendlyAxisYNames = new List<String>();
        private List<String> _friendlySeriesNames = new List<String>();
        protected List<String> _originalAxisXNames = new List<String>();
        protected List<String> _originalSeriesNames = new List<String>();
        
        private Hashtable customSeriesLabelsMap = new Hashtable();
        private Hashtable customAsixXOverrideMap = new Hashtable();
        private ArrayList seriesPatterns = new ArrayList();
        
        private double _maxRateInResult = 0;

        private int _axisYStep = 1;
        private int _axisYMin = 0;
        private int _axisYMax = 10;
        private string _axisYDescription = string.Empty;
        private string _axisXDescription = string.Empty;
        public String YAxisSuffix = String.Empty;

        private ArrayList _color = new ArrayList();

        private String orderBy;

        #region Public Properties
        /// <summary>
        /// Overrides the default series colors, if set.
        /// </summary>
        public ArrayList SeriesColors = null;

        /// <summary>
        /// Used by Stacked Bar, specify series column names
        /// </summary>
        public List<String> MeasureColumns
        {
            get { return _measureColumns; }
            set { _measureColumns = value; }
        }
        /// <summary>
        /// Supply a Name-Value collection to Override the Series labels.
        /// Name is the original label, and Value will replace it.
        /// </summary>
        public Hashtable OverrideSeriesLabels
        {
            get { return customSeriesLabelsMap; }
            set { customSeriesLabelsMap = value; }
        }
        public Hashtable OverrideAxisXLabels
        {
            get { return customAsixXOverrideMap; }
            set { customAsixXOverrideMap = value; }
        }
        public ArrayList OverrideSeriesPatterns
        {
            get { return seriesPatterns; }
            set { seriesPatterns = value; }
        }
        public String OrderBy
        {
            get { return orderBy; }
            set { orderBy = value; }
        }
        private DataTable SortDataSource(DataTable source)
        {
            QueryMarshaller qm = ((PageBaseWI)Page).QueryMarshaller;
            DataTable table = null;

            //keep source and table sorted the same way before transformed below.
            if (String.IsNullOrEmpty(OrderBy))
            {
                table =
                    (OrderBy == String.Empty) //disable any sorting
                    ? source
                    : SligoCS.Web.WI.WebSupportingClasses.WI.DataTableSorter.SortAndCompareSelectedFloatToTop(
                        source, qm);
            }
            else
            {
                table =
                    SligoCS.Web.WI.WebSupportingClasses.WI.DataTableSorter.SortAndCompareSelectedFloatToTop(
                        source, qm, OrderBy);
            }

            return table;
        }
        private DataTable TransformDataSource(DataTable source)
        {
            DataTable table = null; //return

            if ((Type == StackedType.Stacked100|| Type == StackedType.Normal)
                && MeasureColumns.Count > 0)
            {//transform for stacked bars
                table = GetMeasureColumnsAndCastAsDouble(source, MeasureColumns);
                table = GraphBarChart.TransferColumnsBetweenDS(source, table, new List<String>(new string[] { LabelColumnName }));
            }
            else //Simple bar (usually, but can be for stacked too)
            {
                table =  pivotDataSet(source);
            }

            return table;
        }
        //Required
        public String Title
        {
            get { return _title; }
            set {
                _title = HttpUtility.HtmlDecode(value);
                this.Titles.Add(new TitleDockable(_title));
            }
        }

        //There are three types/style we used to display bar chart: Normal Bar Chart, Stacked Bar Chart, Stacked100 Bar Chart
        public StackedType Type
        {
            get { return (StackedType)Enum.Parse(typeof(StackedType), AllSeries.Stacked.ToString());}
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
                SetLegendInvertedStatus(value);
            }
        }

        private void SetLegendInvertedStatus(StackedType type)
        {
            LegendItemAttributes liatt = new LegendItemAttributes();
            this.LegendBox.ItemAttributes[Series] = liatt;

            switch (type)
            {
                case StackedType.No:
                    liatt.Inverted = false; 
                    break;
                case StackedType.Normal:
                case StackedType.Stacked100:
                    liatt.Inverted = true; 
                    break;
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
            get
            {
                return (String.IsNullOrEmpty(_labelColumnName)) ?
                GetLabelsColumnDefault(((PageBaseWI)Page).GlobalValues)
                :_labelColumnName; 
            }
            set { _labelColumnName = value; }
        }

        //Optional
        //Column name for displaying series
        public String SeriesColumnName
        {
            get
            {
                return (String.IsNullOrEmpty(_seriesColumnName)) ?
                    GetSeriesColumnDefault(((PageBaseWI)Page).GlobalValues)
                    :_seriesColumnName;
            }
            set { _seriesColumnName = value; }
        }

        /// <summary>
        /// DEPRECATED. Use OverrideAxisXLabels.
        /// Will not be invoked for Stacked Bars.
        /// </summary>
        public List<String> FriendlyAxisXNames
        {
            get { return _friendlyAxisXNames; }
            set { _friendlyAxisXNames = value; }
        }

        //Optional
        //Customize friendly series name (series name are displayed in the legend box)
        public List<String> FriendlySeriesNames
        {
            get { return _friendlySeriesNames; }
            set { _friendlySeriesNames = value; }
        }


        //Optional
        public double MaxRateInResult
        {
            get { return _maxRateInResult; }
            set
            {
                _maxRateInResult = value;

                AxisYMin = 0;

                if (_maxRateInResult < 2)
                {
                    AxisYMax = 2;
                    AxisYStep = 1;
                }
                else if (_maxRateInResult < 5)
                {
                    AxisYMax = 5;
                    AxisYStep = 1;
                }
                else if (_maxRateInResult < 10)
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
                    AxisYMax = 109;
                    AxisYStep = 10;
                }

                SetAutoFriendlyAxisYNames(AxisYMin, AxisYMax, AxisYStep);
            }
        }

        private void SetAutoFriendlyAxisYNames(int min, int max, int step)
        {
            List<String> _axisYNames = new List<String>();
            for (int i = min; i <= max; i = i + step)
            {
                _axisYNames.Add(i.ToString() + YAxisSuffix);
            }
            FriendlyAxisYNames = _axisYNames;
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
                if (_friendlyAxisYNames.Count > 0)
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
        public List<String> FriendlyAxisYNames
        {
            get { return _friendlyAxisYNames; }
            set { 
                _friendlyAxisYNames = value;
                for (int i = 0; i < _friendlyAxisYNames.Count; i++)
                {
                    this.AxisY.Labels[i] = _friendlyAxisYNames[i].ToString();
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            //default
            Type = StackedType.No;
            YAxisSuffix = "%"; 
            System.Drawing.Font myFont = new System.Drawing.Font("Arial", 9);
            this.Font = myFont;

            this.Gallery = Gallery.Bar;
            this.Width = 580;
            this.Height = 370;
            
            this.LegendBox.Dock = ChartFX.WebForms.DockArea.Bottom;
            this.LegendBox.Border = 0;
            this.LegendBox.ContentLayout = ChartFX.WebForms.ContentLayout.Center;
            this.LegendBox.AutoSize = true;
//            this.LegendBox.Height = 60;
//            this.LegendBox.Width = 530; 
            this.LegendBox.MarginX = 0;
            this.LegendBox.MarginY = 0;
            // plotareaonly = false means that the legend box will not flow outside of the chart area
            // however this doesn't mean that the legend itself will not flow outside of the legendbox
            // and therefore have hidden legend items.

            this.LegendBox.PlotAreaOnly = false;
            
           
           
           // this.AxisY.LabelsFormat.IsPercentage.Equals(true);
           // this.Font.Bold.Equals(true);
           // this.LegendBox.Border.Equals(1);
            //this.LegendBox.Height = 60;
            //this.LegendBox.Width = 600;
            //this.LegendBox.MarginX = 38;
            //this.LegendBox.ContentLayout = ChartFX.WebForms.ContentLayout.Near;
            //this.LegendBox.PlotAreaOnly = false;
            //end ht test
            this.AxisX.Grids.Major.Visible = false;
            
            ChartFX.WebForms.Adornments.SolidBackground solidBackground = 
                new ChartFX.WebForms.Adornments.SolidBackground();
            solidBackground.Color = System.Drawing.Color.White;

            ChartFX.WebForms.Adornments.SimpleBorder simpleBorder = 
                new ChartFX.WebForms.Adornments.SimpleBorder();
            simpleBorder.Color = System.Drawing.Color.Black;

            this.Background = solidBackground;
            this.Border = simpleBorder;

            GraphHorizBarChart.SetSeriesColors(this.Series, null);
        }

        public override void DataBind()
        {
            if (!Visible)   // don't databind if we aren't going to use it.
                return;
            DataTable table = getTableFromObject(DataSource);

            //backwards compatibility
            if (SeriesColumnName == ColumnPicker.CommonGraphNames.OrgSchoolTypeLabel.ToString()
                && !table.Columns.Contains(ColumnPicker.CommonGraphNames.OrgSchoolTypeLabel.ToString()))
                SeriesColumnName = "OrgLevelLabel";

            table = SortDataSource(table);
            SaveOriginalAxisAndSeriesLabels(table);
            table = TransformDataSource(table);
            GraphBarChart.OverrideSeriesColumnNames(table, customSeriesLabelsMap);
            GraphBarChart.ReplaceColumnValues(table, LabelColumnName, OverrideAxisXLabels);
            
            DataSource = table;
            
            base.DataBind();
            GraphHorizBarChart.SetSeriesColors(this.Series, SeriesColors);
            SetSeriesPatterns(this.Series, OverrideSeriesPatterns);

        }
        private void SaveOriginalAxisAndSeriesLabels(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                AddUniqueToListIfRowContains(_originalSeriesNames, row, SeriesColumnName);
                AddUniqueToListIfRowContains(_originalAxisXNames, row, LabelColumnName);
            }
        }
        public static void SetSeriesPatterns(SeriesAttributesCollection series, ArrayList patterns)
        {
            if (patterns == null || patterns.Count < 1) return;

            int max = series.Count;
            for (int i = 0; i < max; i++)
            {
                if (i < patterns.Count)
                {
                    series[i].Pattern = (System.Drawing.Drawing2D.HatchStyle)patterns[i];
                    series[i].FillMode = FillMode.Pattern;
                    series[i].AlternateColor = System.Drawing.Color.FromArgb(255, 255, 255);
                }
            }
        }
        public static String GetSeriesColumnDefault(GlobalValues globals)
        {
            return ColumnPicker.GetCompareToColumnName(globals);
        }
        public static String GetLabelsColumnDefault(GlobalValues globals)
        {
            if (globals.CompareTo.Key == CompareToKeys.Current
                || globals.STYP.Key != STYPKeys.AllTypes)
            {
                return ColumnPicker.GetViewByColumnName(globals);
            }
            else
            {
                return ColumnPicker.CommonGraphNames.SchooltypeLabel.ToString();
            }
        }
        /// <summary>
        /// Convert raw dataset to chart dataset
        /// </summary>
        /// <param name="rawDS">Raw dataset</param>
        /// <returns>Chart DataTable</returns>
        /// <remarks>
        /// Create chart dataset structure first before populate data.
        /// returns null if LabelColumnName, SeriesColumnName, or DisplayColumnName is empty.
        /// also uses private members FriendlyAxisXNames, FriendlySeriesNames
        /// </remarks>
        private DataTable pivotDataSet(DataTable rawDS)
        {
            DataTable chartTable = new DataTable();

            //Create Chart Table (Chart Dataset) Structure, the first column is for 
            // the label names of axis X  Label column names.
            chartTable.Columns.Add(
                new DataColumn(LabelColumnName, typeof(String))
            );

            bool useCustomLabels, useCustomSeries;
            useCustomLabels = (_originalAxisXNames.Count == FriendlyAxisXNames.Count);
            useCustomSeries = (_originalSeriesNames.Count == FriendlySeriesNames.Count);

            List<String> seriesLabels = (useCustomSeries) ? FriendlySeriesNames : _originalSeriesNames;
            List<String> axisLabels = (useCustomLabels) ? FriendlyAxisXNames : _originalAxisXNames;

            foreach (string seriesName in seriesLabels)
            {
                chartTable.Columns.Add(
                    new DataColumn(seriesName, typeof(Double))
                    );
            }
            
            //Fetch Data From The Raw DataSet
            for (int i = 0; i < axisLabels.Count; i++)
            {
                DataRow newRow = chartTable.NewRow();
                for (int n = 0; n < seriesLabels.Count + 1; n++)
                {
                    //The first column in each row in chart dataset is the Series name, i.e. View By value.
                    if (0 == n)
                    {
                            newRow[n] = axisLabels[i];
                    }
                    else
                    {
                        string condition = string.Empty;        

                        // A simple quick query from raw dataset
                        if (rawDS.Columns.Contains(SeriesColumnName))
                            condition = "[" + LabelColumnName + "] = '" + _originalAxisXNames[i].Replace("'", "''") + "'";

                        if (rawDS.Columns.Contains(LabelColumnName))
                        {
                            if (!condition.Equals(String.Empty)) condition +=" and ";

                            condition += "[" + SeriesColumnName + "] = '" + seriesLabels[n - 1].Replace("'", "''") + "'";
                        }

                        DataRow[] rawRow = rawDS.Select(condition);

                        if (rawRow.Length > 0)
                        {
                            try
                            {
                                newRow[n] = Double.Parse(rawRow[0][DisplayColumnName].ToString());
                            }
                            catch
                            {
                                //No value for displaying
                                newRow[n] = 0;
                            }
                        }
                    }
                }
                chartTable.Rows.Add(newRow);
            }
            chartTable.AcceptChanges();
            
            return chartTable;
        }
        private void AddUniqueToListIfRowContains(List<String> list, DataRow row, String field)
        {
            String value = String.Empty;

            if (row.Table.Columns.Contains(field))
                value = row[field].ToString();

            if (!list.Contains(value))
                list.Add(value);
        }
        public static double GetMaxRateInColumn(DataTable dt, String col)
        {
            double max = 0;
            double val;
            
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    val = Convert.ToDouble(row[col].ToString());
                }
                catch //(Exception e) //can't convert to Double
                {
                    //if (row[col].ToString() != "0.0%") throw e;
                    continue;
                }
                if ( max < val) max = val;
            }
            return max;
        }
       public static DataTable TransferColumnsBetweenDS(DataTable source, DataTable dest, List<String> ColumnNames)
        {
            DataTable dsNew = new DataTable();
            
            //get the new columns
            foreach (String name in ColumnNames)
            {
                dsNew.Columns.Add(name, source.Columns[name].DataType);
            }
             //get the old columns
            foreach (DataColumn col in dest.Columns)
            {
                dsNew.Columns.Add(col.ColumnName, col.DataType);
            }

            SimpleColumnCopy(dsNew, source, dest, ColumnNames);

            return dsNew;
        }

        /// <summary>
        /// Copy columns specified in names, from DataTable Src, to DataTable Trgt, in addition to columns in DataTable Dst
        /// </summary>
        /// <param name="Trgt">output</param>
        /// <param name="Src">DataTable containing columns in names List</param>
        /// <param name="Dst">DataTable which has its columns auto copied to Trgt</param>
        /// <param name="names">Columns to copy from Src</param>
        /// <returns></returns>
        private static void SimpleColumnCopy(DataTable Trgt, DataTable Src, DataTable Dst, List<String> names)
        {
            DataRow rowNew;
            DataRow dest;
            DataRow source;
            int n;

            for (n = 0; n < Dst.Rows.Count; n++)
            {
                rowNew = Trgt.NewRow();
                dest = Dst.Rows[n];
                source = Src.Rows[n];

                // Copy the existing columns from the target
                foreach (DataColumn col in dest.Table.Columns)
                {
                    rowNew[col.ColumnName] = dest[col.ColumnName];
                }
                // add in the new columns
                foreach (String name in names)
                {
                    rowNew[name] = source[name];
                }
                Trgt.Rows.Add(rowNew);
            }
        }
        public static DataTable GetMeasureColumnsAndCastAsDouble(DataTable ds, List<String> usedColumnNames)
        {
            DataTable newTable = new DataTable();
            DataRow rowNew;
            object[] rowVals = new object[usedColumnNames.Count];
            DataColumn newCol;
            
            //define the columns in the new DataSet
           foreach(String name in usedColumnNames)
           {
                newCol = new DataColumn();
                newCol.DataType = typeof(Decimal);
                newCol.ColumnName = name;
               
                newTable.Columns.Add(newCol);
           }
            int n;
            int i;
            Decimal val;
            //ensure that we have numeric data
            for (n = 0 ; n < ds.Rows.Count; n ++)
           {
               rowNew = newTable.NewRow();
                
                for (i = 0; i < newTable.Columns.Count; i++)
                {
                    if (!Decimal.TryParse(ds.Rows[n][newTable.Columns[i].ColumnName].ToString(), out val))
                        val = 0;

                    rowVals[i] = val;
                }
                rowNew.ItemArray = rowVals;
                newTable.Rows.Add(rowNew);
           }
           return newTable;
        }
        /// <summary>
        /// Changes the ColumnName property in the supplied table to the Value field of the Labels Map.
        /// </summary>
        /// <param name="tbl"></param>
        /// <param name="seriesLabelsMap"></param>
        public static void OverrideSeriesColumnNames(DataTable tbl, Hashtable seriesLabelsMap )
        {
            RenameColumns(tbl, seriesLabelsMap);
        }
        /// <summary>
        /// Changes the ColumnName property in the supplied table to the Value field of the Labels Map.
        /// </summary>
         public static void RenameColumns(DataTable tbl, Hashtable columnNames)
        {
            if (columnNames != null)
            {
                foreach (DictionaryEntry map in columnNames)
                {
                    try { tbl.Columns[map.Key.ToString()].ColumnName = map.Value.ToString(); }
                    catch { continue; }
                }
            }
        }
        public static void ReplaceColumnValues(DataTable tbl, String colName, Hashtable searchReplaceMap)
        {
            if (searchReplaceMap == null) return;
            if (!tbl.Columns.Contains(colName)) return;
            String val;

            for (int n = 0; n < tbl.Rows.Count; n++)
            {
                val = tbl.Rows[n][colName].ToString();
                if (searchReplaceMap.ContainsKey(val))
                {
                    tbl.Rows[n][colName] = searchReplaceMap[val].ToString();
                }
            }
        }
        /// <summary>
        /// Retrieve a DataTable objec  from a generic DataSource reference
        /// </summary>
        /// <param name="DataSource"></param>
        /// <returns></returns>
        public static DataTable getTableFromObject(Object source)
        {
            if (source.GetType() != typeof(DataTable)
               && source.GetType() != typeof(DataSet)
               && source.GetType() != typeof(DataView))
            {
                throw new Exception("Invalid DataSource Object Type." + source.GetType().ToString());
            }
            DataTable table;
            if (source.GetType() == typeof(DataSet))
                table = ((DataSet)source).Tables[0];
            else if (source.GetType() == typeof(DataView))
                table = ((DataView)source).Table;
            else
                table = (DataTable)source;

            return table;
        }
        public enum StackedType
        {
            No,
            Normal,
            Stacked100
        }
    }
}


