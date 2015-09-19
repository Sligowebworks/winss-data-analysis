using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ChartFX.WebForms;
using ChartFX.WebForms.Adornments;
using SligoCS.BL.WI;
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.WI.WebSupportingClasses;
using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.Web.WI.WebUserControls
{
    public class GraphHorizBarChart : ChartFX.WebForms.Chart
    {
        private List<String> _measureColumns = new List<String>();
        private List<String> _labelColumns = new List<String>();
        private Boolean _selectedSortBySecondarySort = true;
        private String _title;
        private String orderBy;
        private Hashtable customSeriesLabelsMap;
        private String _yaxis_suffix;
  
        #region subs
        public static void SetSeriesColors(SeriesAttributesCollection series, ArrayList argColors)
        {
            ArrayList colors;
            colors =  (argColors == null) ? GetDefaultWinssSeriesColors() : argColors;
            int max = (series.Count < colors.Count) ? series.Count : colors.Count;
            for (int i = 0; i < max ; i++)
            {
                series[i].Color = (System.Drawing.Color)colors[i];
            }
        }
        public static void InitializeHorizontalStackedBar(GraphHorizBarChart chart)
        {
            chart.StackedType = Stacked.Normal;
            chart.Gallery = Gallery.Gantt;

            chart.Font = new System.Drawing.Font("Arial", 9);

            chart.Width = 580;
            chart.Height = 370;

            LegendItemAttributes lia = new LegendItemAttributes();
            lia.Inverted = true;
            chart.LegendBox.ItemAttributes[chart.Series] = lia;
            chart.LegendBox.Dock = ChartFX.WebForms.DockArea.Bottom;
            chart.LegendBox.Border = 0;
            chart.LegendBox.ContentLayout = ChartFX.WebForms.ContentLayout.Center;
            chart.LegendBox.AutoSize = false;
            chart.LegendBox.Height = 90;
            chart.LegendBox.Width = (int)chart.Width.Value - 30; 
            chart.LegendBox.MarginX = 0;
            chart.LegendBox.MarginY = 0;
            // plotareaonly = false means that the legend box will not flow outside of the chart area
            // however this doesn't mean that the legend itself will not flow outside of the legendbox
            // and therefore have hidden legend items.
            chart.LegendBox.PlotAreaOnly = false;

            chart.AxisX.Grids.Major.Visible = false;
            chart.AxisX.Inverted = true;

            chart.Background = new SolidBackground();
            ((SolidBackground)chart.Background).Color = System.Drawing.Color.White;
            chart.Border = new ChartFX.WebForms.Adornments.SimpleBorder();
            chart.Border.Color = System.Drawing.Color.Black;
        }
        public static ArrayList GetDefaultWinssSeriesColors()
        {
            ArrayList colors = new ArrayList();
            //colors.Add(System.Drawing.Color.FromArgb(248, 252, 70));

            // primary blue
            colors.Add(System.Drawing.Color.FromArgb(0, 0, 255));
            // pale yellow
            colors.Add(System.Drawing.Color.FromArgb(255, 255, 0));
            // red
            colors.Add(System.Drawing.Color.FromArgb(255, 0, 0));
            // bright green
            colors.Add(System.Drawing.Color.FromArgb(0, 255, 0));
            // pink
            colors.Add(System.Drawing.Color.FromArgb(255, 0, 255));
            // deep blue
            colors.Add(System.Drawing.Color.FromArgb(64, 0, 128));
            // orange
            colors.Add(System.Drawing.Color.FromArgb(255, 128, 0));
            // light blue
            colors.Add(System.Drawing.Color.FromArgb(0, 255, 255));
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

            /*
            // test 
            colors.Add(System.Drawing.Color.FromArgb(0, 64, 128));
            colors.Add(System.Drawing.Color.FromArgb(64, 0, 128));
            colors.Add(System.Drawing.Color.FromArgb(64, 128, 0));
            colors.Add(System.Drawing.Color.FromArgb(128, 64, 0));
            colors.Add(System.Drawing.Color.FromArgb(64, 128, 0));
            colors.Add(System.Drawing.Color.FromArgb(128, 0, 64));
            colors.Add(System.Drawing.Color.FromArgb(64, 0, 128));
            colors.Add(System.Drawing.Color.FromArgb(64, 128, 0));
            colors.Add(System.Drawing.Color.FromArgb(0, 0, 0));
            
            // good light strawberry?
            colors.Add(System.Drawing.Color.FromArgb(255, 64, 128));
            // possibly better purple - lighter 
            colors.Add(System.Drawing.Color.FromArgb(168, 4, 99));

            // almost a good darker brick red
            colors.Add(System.Drawing.Color.FromArgb(219, 36, 85));
            // black 
            colors.Add(System.Drawing.Color.FromArgb(0, 0, 0));

            colors.Add(System.Drawing.Color.FromArgb(255, 146, 255));
            colors.Add(System.Drawing.Color.FromArgb(146, 255, 170));
            colors.Add(System.Drawing.Color.FromArgb(0, 255, 170));
            colors.Add(System.Drawing.Color.FromArgb(255, 0, 170));
            colors.Add(System.Drawing.Color.FromArgb(146, 255, 0));
            colors.Add(System.Drawing.Color.FromArgb(146, 255, 255));
            colors.Add(System.Drawing.Color.FromArgb(255, 255, 170));
            colors.Add(System.Drawing.Color.FromArgb(248, 252, 70));
            colors.Add(System.Drawing.Color.FromArgb(119, 247, 39));
            colors.Add(System.Drawing.Color.FromArgb(38, 8, 250));

            // purple, too similar 
            colors.Add(System.Drawing.Color.FromArgb(128, 4, 136));

            // too similar to good 
            colors.Add(System.Drawing.Color.FromArgb(128, 255, 0));
            colors.Add(System.Drawing.Color.FromArgb(0, 255, 128));
            colors.Add(System.Drawing.Color.FromArgb(0, 128, 255));
            colors.Add(System.Drawing.Color.FromArgb(255, 0, 128));
            colors.Add(System.Drawing.Color.FromArgb(128, 0, 255));
            colors.Add(System.Drawing.Color.FromArgb(255, 128, 255));
            colors.Add(System.Drawing.Color.FromArgb(255, 128, 255));
            colors.Add(System.Drawing.Color.FromArgb(128, 255, 255));
            colors.Add(System.Drawing.Color.FromArgb(255, 128, 255));
            colors.Add(System.Drawing.Color.FromArgb(241, 39, 5));

            // close dupes
            colors.Add(System.Drawing.Color.FromArgb(241, 235, 5));
            colors.Add(System.Drawing.Color.FromArgb(25, 241, 5));
            colors.Add(System.Drawing.Color.FromArgb(8, 5, 241));
            colors.Add(System.Drawing.Color.FromArgb(238, 4, 241));
            colors.Add(System.Drawing.Color.FromArgb(4, 241, 241));
            colors.Add(System.Drawing.Color.FromArgb(0, 0, 0));
            // similar to red
            colors.Add(System.Drawing.Color.FromArgb(241, 39, 5));
            // too dark
            colors.Add(System.Drawing.Color.FromArgb(0, 64, 128));
            colors.Add(System.Drawing.Color.FromArgb(64, 0, 128));
            colors.Add(System.Drawing.Color.FromArgb(64, 128, 0));
            
            */

            return colors;
        }

        public static ArrayList GetWSASStackedBarSeriesColors()
        {
            ArrayList WSAScolors = new ArrayList();
            // primary blue - Advanced
            WSAScolors.Add(System.Drawing.Color.FromArgb(0, 0, 255));
            // bright green - Proficient
            WSAScolors.Add(System.Drawing.Color.FromArgb(0, 255, 0));
            // pale yellow - Basic
            WSAScolors.Add(System.Drawing.Color.FromArgb(255, 255, 0));
            // red - Minimal Performance
            WSAScolors.Add(System.Drawing.Color.FromArgb(255, 0, 0));
            // medium gray - pre-req eng
            WSAScolors.Add(System.Drawing.Color.FromArgb(96, 96, 96));
            // dark gray - pre-req skill 
            WSAScolors.Add(System.Drawing.Color.FromArgb(64, 64, 64));
            // light gray - No WSAS
            WSAScolors.Add(System.Drawing.Color.FromArgb(160, 160, 160));
            // gray - No WSAS
            //WSAScolors.Add(System.Drawing.Color.FromArgb(128, 128, 128));
            // pink
            WSAScolors.Add(System.Drawing.Color.FromArgb(255, 0, 255));
            // deep blue
            WSAScolors.Add(System.Drawing.Color.FromArgb(64, 0, 128));
            // orange
            WSAScolors.Add(System.Drawing.Color.FromArgb(255, 128, 0));
            // light blue
            WSAScolors.Add(System.Drawing.Color.FromArgb(0, 255, 255));
            // purple
            WSAScolors.Add(System.Drawing.Color.FromArgb(128, 0, 64));
            // light green
            WSAScolors.Add(System.Drawing.Color.FromArgb(128, 255, 192));
            // light purple 
            WSAScolors.Add(System.Drawing.Color.FromArgb(192, 128, 255));
            // forest green
            WSAScolors.Add(System.Drawing.Color.FromArgb(3, 122, 100));
            // salmon
            WSAScolors.Add(System.Drawing.Color.FromArgb(255, 128, 192));
            // light orange
            WSAScolors.Add(System.Drawing.Color.FromArgb(255, 192, 128));
            // lime
            WSAScolors.Add(System.Drawing.Color.FromArgb(192, 255, 128));
            // lighter blue 
            WSAScolors.Add(System.Drawing.Color.FromArgb(128, 192, 255));
            // brown 
            WSAScolors.Add(System.Drawing.Color.FromArgb(128, 64, 0));

            return WSAScolors;

        }

        private DataTable TransformDataSource(DataTable table)
        {
            if (String.IsNullOrEmpty(OrderBy))
                OrderBy = String.Join(", ", LabelColumns.ToArray());

            OrderBy = OrderBy.Replace("YearFormatted ASC,", "YearFormatted,");
            OrderBy = OrderBy.Replace("YearFormatted,", "YearFormatted DESC,");
            OrderBy = OrderBy.Replace("year ASC,", "year,");
            OrderBy = OrderBy.Replace("year,", "year DESC,");
             
            //throw new Exception(OrderBy);

            if (SelectedSortBySecondarySort)
            {
                if (LabelColumns.Count < 2)throw new Exception("Only one Label Column given with SelectedSortBySecondarySort is True.");

                table = DataTableSorter.SecondarySortCompareSelectedFloatToTop(
                    table,
                    ((PageBaseWI)Page).QueryMarshaller, OrderBy
                );
            }
            else
            {
                table = DataTableSorter.SortAndCompareSelectedFloatToTop(
                    table,
                    ((PageBaseWI)Page).QueryMarshaller, OrderBy
                    );
            }

            DataTable final = GraphBarChart.GetMeasureColumnsAndCastAsDouble(table, MeasureColumns);

            if (LabelColumns.Count > 1)
            {
                final = AddMergeRowStyleColumn(table, final, LabelColumns);
            }
            else
            {
                final = GraphBarChart.TransferColumnsBetweenDS(table, final, LabelColumns);
                //final = AddMergeRowStyleColumn(table, final, new List<String>(new String[]{LabelColumns[0], LabelColumns[0]}));
            }

            return final;
        }
        /// <summary>
        /// Adds a single new column to the Dst DataSet
        /// Uses to two columns from Src, specified in Names.
        /// Resulting column is named for the second item in Names.
        /// Names[0] is included only once per unique value across rows
        /// </summary>
        /// <param name="Src">Must Contain Columns Specified in names</param>
        /// <param name="Dst">Will have it's columns copied to the result</param>
        /// <param name="Names">Columns to concatenate, in RowSpan style.</param>
        /// <param name="reverse">Will merge the rows from the bottom up when true</param>
        /// <returns></returns>
        public static DataTable AddMergeRowStyleColumn(DataTable Src, DataTable Dst, List<String> Names)
        {
            return AddMergeRowStyleColumn(Src, Dst, Names, false);
        }
                   /// Adds a single new column to the Dst DataSet
        /// Uses to two columns from Src, specified in Names.
        /// Resulting column is named for the second item in Names.
        /// Names[0] is included only once per unique value across rows
        /// </summary>
        /// <param name="Src">Must Contain Columns Specified in names</param>
        /// <param name="Dst">Will have it's columns copied to the result</param>
        /// <param name="Names">Columns to concatenate, in RowSpan style.</param>
        /// <param name="reverse">Will merge the rows from the bottom up when true</param>
        /// <param name="useDest">"right" column of merge-label is in the Dst, not Src DataTable</param>
        /// <returns></returns>
        private static DataTable AddMergeRowStyleColumn(DataTable Src, DataTable Dst, List<String> Names, Boolean useDest)
        {
            //System.Diagnostics.Debug.WriteIf((Names.Count > 2), "Warning: AddMergeRowStyleColumn: More than 2 names provided.\n");

            String left = Names[0];
            String right = Names[1];
            int nameCount = Names.Count;

            if (nameCount > 2)
            {
                int i;
                for (i = nameCount-1; i > 0; i--)
                {
                    if (i < nameCount - 1 && ! useDest) useDest = true;
                    Dst = AddMergeRowStyleColumn(Src, Dst, new List<String>(new String[] { Names[i - 1], Names[nameCount-1] }), useDest);
                }
                return Dst;
                //right = Names[nameCount-1];
            }

            DataTable newTable = new DataTable();

            //get the old columns
            foreach (DataColumn col in Dst.Columns)
            {
                if (!newTable.Columns.Contains(col.ColumnName))
                newTable.Columns.Add(col.ColumnName, col.DataType);
            }
            //use the right column for the new column name
            String mergeColName = right;

            if (!newTable.Columns.Contains(mergeColName)) newTable.Columns.Add(mergeColName, Src.Columns[mergeColName].DataType);


            DataRow rowNew;
            DataRow dest, source, prev;

            int n, count;
            count = Dst.Rows.Count;
            n = 0;
            while (n >= 0 && n < count)
            {
                rowNew = newTable.NewRow();
                dest = Dst.Rows[n];
                source = Src.Rows[n];
                prev = source;

                if (n != 0)
                {
                    prev = Src.Rows[n - 1];
                }

                // Copy the existing columns from the target
                foreach (DataColumn col in dest.Table.Columns)
                {
                    rowNew[col.ColumnName] = dest[col.ColumnName];
                }
                
                // Do Row-Span Style copy to Single Column
                if (n == 0 || prev[left].ToString() != source[left].ToString())
                {
                    rowNew[mergeColName] =
                        source[left].ToString().Trim() 
                        + getSmartSpacer( 
                        ((useDest)?
                        dest[right].ToString().Trim():
                        source[right].ToString().Trim()).Length
                        )
                        +
                        ((useDest)?
                        dest[right].ToString().Trim():
                        source[right].ToString().Trim());
                }
                else
                {
                    rowNew[mergeColName] 
                        =  ((useDest)?
                        dest[right].ToString().Trim(): 
                        source[right].ToString());
                }

                newTable.Rows.Add(rowNew);
                
                n = n +1;
            }

            return newTable;
        }
        private static string getSmartSpacer(int length)
        {
            int howmany;
            System.Text.StringBuilder str = new System.Text.StringBuilder();

            howmany = 15 -length;
            if (howmany < 0) howmany = 6;

            for (int i = 0; i <= howmany; i+=6)
            {
                str.Append("         ");
            }
            return str.ToString();
        }

        private void SetYAxisSuffix(String suff)
        {
            AxisY.LabelAngle = 0;
            AxisY.LabelsFormat.CustomFormat = "0" + suff;
        }

        #endregion //subs
        #region overrides
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            InitializeHorizontalStackedBar(this);
            SetSeriesColors(Series, null);
        }
        public override void DataBind()
        {
            DataTable tbl = GraphBarChart.getTableFromObject(DataSource);

            //Normalize RaceLabelNames: Replace Old Race Labels with New labels:
            GraphBarChart.ReplaceOldRaceLabelsWithNew(tbl);

            tbl = TransformDataSource(tbl);

            GraphBarChart.OverrideSeriesColumnNames(tbl, customSeriesLabelsMap);

            DataSource = (object)tbl;
            Height = (int)(Height.Value + tbl.Rows.Count * ((tbl.Rows.Count* AutoHeightIncreaseFactor > 35000) ? 1 : AutoHeightIncreaseFactor)); //multiply rowcount by 20, unless it will exceed the Graph's Height Property Maximum (~35,000);
            base.DataBind();

            if (!String.IsNullOrEmpty(YAxisSuffix)) SetYAxisSuffix(YAxisSuffix);
        }
        #endregion //overrides
        #region properties
        public String OrderBy
        {
            get { return orderBy; }
            set { orderBy = value; }
        }
        /// <summary>
        /// pixel size by which the chart height is increased by for each row of data. Defaults to 20 px.
        /// </summary>
        public int AutoHeightIncreaseFactor = 25;
        public List<String> MeasureColumns
        {
            get { return _measureColumns; }
            set { _measureColumns = value; }
        }
        public List<String> LabelColumns
        {
            get { return _labelColumns; }
            set { _labelColumns = value; }
        }
        public Boolean SelectedSortBySecondarySort
        {
            get { return _selectedSortBySecondarySort; }
		    set { _selectedSortBySecondarySort = value; }
    	}
        public String Title
        {
            get { return _title; }
            set
            {
                _title = HttpUtility.HtmlDecode(value);
                TitleDockable title = new TitleDockable(_title);
                title.PlotAreaOnly = false;
                this.Titles.Add(title);
            }
        }
        public Stacked StackedType
	    {
		    get { return AllSeries.Stacked;}
		    set { AllSeries.Stacked = value;}
	    }
        public String AxisYDescription
        {
            get { return AxisY.Title.Text; }
            set {AxisY.Title.Text = value; }
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
        /// <summary>
        /// Caution, if using a % use an escape such as \% or \\%, or else the labels will be multiplied by 100
        /// </summary>
        public String YAxisSuffix
        {
            get { return _yaxis_suffix; }
            set { _yaxis_suffix = value; }
        }
# endregion //properties
    }
}
