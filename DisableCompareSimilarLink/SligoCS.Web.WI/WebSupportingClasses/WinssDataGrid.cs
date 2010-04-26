
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using SligoCS.Web.Base.WebServerControls.WI;
using SligoCS.BL.WI;
using SligoCS.Web.Base.PageBase.WI;

namespace SligoCS.Web.WI.WebSupportingClasses
{
    public class WinssDataGrid : GridView
    {
        public WinssDataGrid()
            : base()
        {
            // no way to detect if already set in aspx...
            AutoGenerateColumns = false;
            if (Width.IsEmpty) Width = Unit.Pixel(585);
            UseAccessibleHeader = false; //true will cause th tags to be used instead of td's, which can mess with our styles
            SetDefaultStyleItems();

            RowDataBound += new GridViewRowEventHandler(DecodeHtmlEncodedLink);
        }
        private List<TableRow> headersCache = new List<TableRow>();
        private String orderby;
        private Boolean _selectedSortBySecondarySort = false;

        public event EventHandler ColumnLoaded;

        private void DecodeHtmlEncodedLink(Object sender, GridViewRowEventArgs rgs)
        {
            foreach (TableCell cell in rgs.Row.Cells)
            {// attacking all cells because hard to zero in on specific columns.
                cell.Text = System.Web.HttpUtility.HtmlDecode(cell.Text);
            }
        }
        /// <summary>
        /// routine to assign display-style-properties. Handles propagation to HeaderStyle, FooterStyle and RowStyle. Assignment in aspx will not be overwritten.
        /// </summary>
        protected virtual void SetDefaultStyleItems()
        {
            this.Style.Add("class", "text");
            // check that each property has not been set in the aspx before using the default).
            TableItemStyle myStyle = new TableItemStyle();

            //properties for the outermost control (gridview)
            BorderColor = System.Drawing.Color.Gray;
            BorderStyle = BorderStyle.Outset;
            BorderWidth = Unit.Pixel(4);

            //new ItemStyle obj
            myStyle.BorderColor = BorderColor;
            myStyle.BorderStyle = BorderStyle;
            myStyle.BorderWidth = BorderWidth;

            // use as basis for other default styles
            HeaderStyle.MergeWith(myStyle);
            RowStyle.MergeWith(myStyle);
            FooterStyle.MergeWith(myStyle);
        }
        /// <summary>
        /// defines table body Row Style. Uses this.RowStyle for defaults.
        /// </summary>
        /// <param name="ItemStyle"></param>
        public virtual void applyBodyStyle(Style ItemStyle)
        {
            if (ItemStyle.BorderWidth.IsEmpty) 
                ItemStyle.BorderWidth = Unit.Pixel((int)RowStyle.BorderWidth.Value - 1);

            if (ItemStyle is TableItemStyle)
            {
                if (((TableItemStyle)ItemStyle).HorizontalAlign == HorizontalAlign.NotSet)
                    ((TableItemStyle)ItemStyle).HorizontalAlign = HorizontalAlign.Center;

                ((TableItemStyle)ItemStyle).MergeWith(this.RowStyle);
            }
            else
            {
                ItemStyle.MergeWith(this.RowStyle);
            }
        }
        /// <summary>
        /// defines Header Row Style. Uses this.HeaderStyle for defaults.
        /// </summary>
        /// <param name="ItemStyle"></param>
        public virtual void applyHeaderStyle(Style ItemStyle)
        {
            if (ItemStyle is TableItemStyle)
            {
                if (((TableItemStyle)ItemStyle).HorizontalAlign == HorizontalAlign.NotSet)
                    ((TableItemStyle)ItemStyle).HorizontalAlign = HorizontalAlign.Center;

                ((TableItemStyle)ItemStyle).MergeWith(this.HeaderStyle);
            }
            else
            {
                ItemStyle.MergeWith(this.HeaderStyle);
            }
        }
        /// <summary>
        /// Adds additional Header Row above the automatic column header row.
        /// </summary>
        /// <param name="headerText"></param>
        public void AddSuperHeader(string headerText)
        {
            TableCell cell = new TableCell();
            TableRow row = new TableRow();

            headerText = headerText.Replace(TitleBuilder.newline, "<br />");
            
            cell.Text = headerText;
            cell.ColumnSpan = Columns.Count;
            cell.Visible = true;
            
            row.Cells.Add(cell);
            AddSuperHeader(row);
         }
        /// <summary>
        /// Adds additional Header Rows above the automatic column header row. 
        /// </summary>
        /// <param name="row"></param>
        public void AddSuperHeader(TableRow row)
        {
            //infer if we have DataBound the table yet
            if (TheTable().Rows.Count > 0)
            {
                AddSuperHeaderToTable(row);
            }
            else  // cache the header to add after data binding
            {
                headersCache.Add(row);
            }
        }
        private void AddSuperHeaderToTable(TableRow row)
        {
            int i = 0;
            // count the number of headers
            foreach (GridViewRow gvr in TheTable().Rows) { if (gvr.RowType.Equals(DataControlRowType.Header)) i = i + 1; }

            // if we have one or more headers, then add it at the second to last position (above the default header row)
            if (i > 0) i =  i-1;

            GridViewRow theRow = CreateRow(i, -1, DataControlRowType.Header, DataControlRowState.Normal);
            
            TableCell[] cellArr = new TableCell[row.Cells.Count];
            row.Cells.CopyTo(cellArr, 0);
            theRow.Cells.AddRange(cellArr);

            foreach (TableCell field in theRow.Cells)
            {
                applyHeaderStyle(field.ControlStyle);
            }
            TheTable().Rows.AddAt(i, (TableRow)theRow);
        }
        /// <summary>
        /// Adds Super Headers from the headerCache, called after DataBinding has occured
        /// </summary>
        private void DoAddSuperHeaders()
        {
            foreach (TableRow row in headersCache)
            {
                AddSuperHeaderToTable(row);
            }
        }
        /// <summary>
        /// merge label columns across rows
        /// </summary>
        private void DoRowSpanMerges()
        {
            if (ContainsEnabledMergeColumns())
            { // if field definition includes mergeable columns

                //hide the appropriate Table Cells.
                for (int i = Rows.Count - 1; i >= 0; i--)
                {
                    for (int j = 0; j < Columns.Count; j++)
                    {
                        if ((Columns[j] is MergeColumn)
                            && ((MergeColumn)Columns[j]).EnableMerge)
                        {
                            if (i > 0)    // never hide cell in the first row
                            {
                                TableCell prevCell = Rows[i - 1].Cells[j];
                                TableCell thisCell = Rows[i].Cells[j];

                                if (thisCell.Text == prevCell.Text) //merge it.
                                {
                                    thisCell.Visible = false;

                                    //Rowspan should default to 1 anyway.
                                    if (thisCell.RowSpan == 0) thisCell.RowSpan = 1;
                                    //MZD: is this logic valid?? why set thisCell.RowSpan if it is not visible? Shouldn't we incremement prevCell.RowSpan instead of = thisCell + 1. Does this work for rowspans > 2?
                                    prevCell.RowSpan = thisCell.RowSpan + 1;
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// confirms that the Columns Collection contains any fields that EnableMerge=true
        /// </summary>
        /// <returns></returns>
        private bool ContainsEnabledMergeColumns()
        {
            bool retval = false;
            foreach (DataControlField field in Columns)
            {
                if (field is MergeColumn)
                {
                    if (((MergeColumn)field).EnableMerge)
                    {
                        retval = true;
                        break;
                    }
                }
            }
            return retval;
        }
        /// <summary>
        /// Sets all columns named in the list to be visible.
        /// </summary>
        /// <param name="columnNames"></param>
        public void SetVisibleColumns(List<String> columnNames)
        {
            foreach (String colName in columnNames)
            {
                SetColumnVisibility(colName, true);
            }
        }
        /// <summary>
        /// Alias for SetColumnVisibility() for legacy support.
        /// </summary>
        /// <param name="DataFieldName"></param>
        /// <param name="visible"></param>
        public void SetBoundColumnVisible(string DataFieldName, bool visible)
        {
            SetColumnVisibility(DataFieldName, visible);
        }
        /// <summary>
        /// Sets Visible property of Column using search by fieldname of the Columns Collection.
        /// </summary>
        /// <param name="DataFieldName"></param>
        /// <param name="visible"></param>
        public void SetColumnVisibility(string DataFieldName, bool visible)
        {
            try
            {
                Columns[ColIndexByName(DataFieldName)].Visible = visible;
            }
            catch (Exception e)
            {
                throw new Exception("Field Name [" + DataFieldName + "] not found." + e.Message);
            }
        }
        /// <summary>
        /// use FormatString attribute to format value for display
        /// </summary>
        private void FormatCellValues()
        {
            foreach (GridViewRow row in Rows)
            {
                foreach (DataControlField field in Columns)
                {
                    if (field is WinssDataGridColumn)  // enables grids to have non-sligoDataGridColumns
                    {
                        string format = ((WinssDataGridColumn)field).FormatString;
                        string fieldName = ((BoundField)field).DataField.ToString();
                        SetCellToFormattedDecimal(row, fieldName, format);
                    }
                }
            }
        }
        /// <summary>
        /// Performs check to validate decimal and percents before applying string format, given.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="DataField"></param>
        /// <param name="format"></param>
        public void SetCellToFormattedDecimal(GridViewRow row, string DataFieldName, string format)
        {
            int colIndex = ColIndexByName(DataFieldName);

            string sVal = row.Cells[colIndex].Text;
            decimal val;
            if (decimal.TryParse(sVal, out val))
            {
                if (format.Contains("%"))
                {// library format for percents multiplies by 100, so...
                    val = val / 100;
                }
                row.Cells[colIndex].Text = val.ToString(format);
            }
        }
        public int ColIndexByName(String DataFieldName)
        {
            foreach (DataControlField col in Columns)
            {
                if (col is BoundField)
                {
                    if (((BoundField)col).DataField.ToUpper() == DataFieldName.ToUpper())
                    {
                        return Columns.IndexOf(col);
                    }
                }
            }
            return -1;
        }
        /// <summary>
        /// Missing GridView Property to get the default Table. Warning: returns a new Table object if one doesn't already exist.
        /// </summary>
        /// <returns></returns>
        protected Table TheTable()
        {
            Table tbl;
            try
            {
                tbl =  (Table)this.Controls[0];
            }
            catch 
            {
                tbl = new Table();
                this.Controls.AddAt(0, tbl);
            }
            return tbl;
        }
        public String OrderBy
        {
            get
            {
                return orderby;
            }
            set
            {
                orderby = value;
            }
        }
        public Boolean SelectedSortBySecondarySort
        {
            get { return _selectedSortBySecondarySort; }
            set { _selectedSortBySecondarySort = value; }
        }
	
        #region events
        /// <summary>
        /// Overrides Microsoft's Event Handler.  Recomended  in official documentation for manipulations of the display.
        /// </summary>
        protected override void PrepareControlHierarchy()
        {
            DoRowSpanMerges();
            FormatCellValues();

            base.PrepareControlHierarchy();
        }
        /// <summary>
        /// calls applyHeaderStyle() and applyBodyStyle() to ensure propagation of default style properties.
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="useDataSource"></param>
        /// <returns></returns>
        protected override System.Collections.ICollection CreateColumns(PagedDataSource dataSource, bool useDataSource)
        {
            // intercept base
            System.Collections.ArrayList cols =  (System.Collections.ArrayList)base.CreateColumns(dataSource, useDataSource);

            foreach (DataControlField field in cols)
            {
                if(ColumnLoaded != null) ColumnLoaded(field,  new EventArgs());
                applyHeaderStyle(field.HeaderStyle);
                applyBodyStyle(field.ItemStyle);
            }

            return cols;
        }
        public override object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                DataView view = (DataView)value;
                QueryMarshaller qm = ((PageBaseWI)Page).QueryMarshaller;

                if (String.IsNullOrEmpty(OrderBy))
                    OrderBy = 
                        SligoCS.DAL.WI.SQLHelper.ConvertToCSV(
                            qm.BuildOrderByList(view.Table.Columns), false
                            );

                OrderBy = OrderBy.Replace("year ASC", "year DESC");

                Boolean forceFloat = (ForceCurrentFloatToTopOrdering || qm.compareSelectedFullKeys);

                base.DataSource =
                    (SelectedSortBySecondarySort)
                    ? SligoCS.Web.WI.WebSupportingClasses.WI.DataTableSorter.SecondarySortCompareSelectedFloatToTop(
                        view.Table, qm, OrderBy)
                    :SligoCS.Web.WI.WebSupportingClasses.WI.DataTableSorter.SortAndCompareSelectedFloatToTop(
                        view.Table, qm, OrderBy, forceFloat)
                    ;

               // throw new Exception("SSBSS:" + SelectedSortBySecondarySort.ToString() + ":: " + OrderBy + ":: FF :: " + forceFloat.ToString());
            }
        }
        public Boolean ForceCurrentFloatToTopOrdering = false;

        protected override void OnDataBound(EventArgs e)
        {
            base.OnDataBound(e);
            DoAddSuperHeaders();
        }
        #endregion
    }
}