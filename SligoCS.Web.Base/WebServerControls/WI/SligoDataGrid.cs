using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SligoCS.Web.Base.WebServerControls.WI
{
    public class SligoDataGrid : GridView
    {

        #region class level variables
        private bool showSuperHeader = false;
        private List<TableRow> superHeaders = new List<TableRow>();
        #endregion

        #region public properties
        public bool ShowSuperHeader
        {
            get { return showSuperHeader; }
            set { showSuperHeader = value; }
        }

        public string SuperHeaderText
        { 
            get { return superHeaders[0].Cells[0].Text; }
        }
        #endregion


        /// <summary>
        /// Overrides Microsoft's function.  Copies the appropriate properties from the GridView down to its embedded controls.
        /// For example, if you specify the BorderWidth on the data grid, it needs to be passed to its embedded table cells BEFORE rendering.
        /// </summary>
        protected override void PrepareControlHierarchy()
        {

            SetDefaultStyleItems();
            //do whatever Microsoft does...
            base.PrepareControlHierarchy();
            PrepareSuperHeader();
            PrepareMergeColumns();
        }


        protected virtual void SetDefaultStyleItems()
        {

            this.Style.Add("class", "text");

            foreach (DataControlField col in Columns)
            {
                if (col.HeaderStyle.HorizontalAlign == HorizontalAlign.NotSet)
                    col.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                if (col.ItemStyle.HorizontalAlign == HorizontalAlign.NotSet)
                    col.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            }
        }


        private void PrepareSuperHeader()
        {
            if ((this.Controls.Count > 0) && (this.Controls[0] is Table))
            {
                for (int i = superHeaders.Count - 1; i >= 0; i--)
                {
                    //work from the lowest header up.

                    //TableCell cell = new TableCell();
                    //cell.VerticalAlign = VerticalAlign.Middle;
                    //cell.HorizontalAlign = HorizontalAlign.Center;
                    //cell.ColumnSpan = this.Columns.Count;
                    //cell.Text = SuperHeaderText;

                    //TableRow row = new TableRow();
                    //row.Cells.Add(cell);
                    TableRow superHeaderRow = superHeaders[i];

                    //BR: set double border
                    superHeaderRow.BorderColor = this.BorderColor;
                    superHeaderRow.BorderStyle = this.BorderStyle;
                    superHeaderRow.BorderWidth = this.BorderWidth;
                    
                    ((Table)this.Controls[0]).Rows.AddAt(0, superHeaderRow);
                }
            }
        }

        public void AddSuperHeader(string headerText)
        {
            TableCell cell = new TableCell();
            cell.VerticalAlign = VerticalAlign.Middle;
            cell.HorizontalAlign = HorizontalAlign.Center;
            cell.ColumnSpan = this.Columns.Count;
            cell.Text = headerText;
            
            ////BR: set double border
            cell.BorderColor = this.BorderColor;
            cell.BorderStyle = this.BorderStyle;
            cell.BorderWidth = this.BorderWidth;
            
            TableRow row = new TableRow();
            row.Cells.Add(cell);
            AddSuperHeader(row);
        }

        public void AddSuperHeader(TableRow headerRow)
        {
            if (headerRow != null)
            {
                superHeaders.Add(headerRow);
            }
        }

        private void PrepareMergeColumns()
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

                                if (thisCell.Text == prevCell.Text)
                                {
                                    //merge it.
                                    thisCell.Visible = false;

                                    //Rowspan should default to 1 anyway.
                                    if (thisCell.RowSpan == 0)
                                        thisCell.RowSpan = 1;

                                    prevCell.RowSpan = thisCell.RowSpan + 1;
                                }
                            }

                        }
                    }
                }
            }
        }


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

        public int FindBoundColumnIndex(string DataField)
        {
            int retval = -1;
            for (int i = 0; i < Columns.Count; i++)
            {
                DataControlField col = Columns[i];
                if (col is BoundField)
                {
                    //we don't want to be case sensitive here.
                    if (((BoundField)col).DataField.ToLower() == DataField.ToLower())
                    {
                        retval = i;
                        break;
                    }
                }
            }

            return retval;
        }

        public void SetAllBoundColumnsVisible(bool visible)
        {
            for (int i = 0; i < Columns.Count; i++)
            {
                DataControlField col = Columns[i];
                if (col is BoundField)
                {
                    col.Visible = visible;
                }
            }
        }

        public void SetBoundColumnVisible(string DataField, bool visible)
        {
            int colIndex = FindBoundColumnIndex(DataField);
            if (colIndex >= 0)
            {
                Columns[colIndex].Visible = visible;
            }
            else // huong added this line
            {// should have code for catching exception here?? like colIndex = -1 ect
                //Columns[0].Visible = visible;
            }

        }

        public void SetCellToFormattedDecimal(GridViewRow row, string DataField, string format)
        {                        
            int colIndex = FindBoundColumnIndex(DataField);
            string sVal = row.Cells[colIndex].Text;
            decimal val;
            if (decimal.TryParse(sVal, out val))
            {
                if (format.Contains("%"))
                {
                    val = val / 100;
                }
                row.Cells[colIndex].Text = val.ToString(format);
            }
         
        }

    }
}
