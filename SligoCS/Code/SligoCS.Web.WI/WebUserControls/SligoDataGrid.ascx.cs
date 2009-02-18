using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SligoCS.Web.WI.WebUserControls
{
    /// <summary>
    /// This class binds against a data table to display the results in a grid.
    /// This class has functions to show/hide columns from that data grid,
    /// or to merge repeated values into a single cell.
    /// </summary>
    public partial class SligoDataGrid : WIUserControlBase
    {
        private DataTable m_table;
        private string m_dataGridTitle;

        private const int ROWINDEX_TITLE = -2;
        private const int ROWINDEX_SUBTITLE = -1;


        protected void Page_Load(object sender, EventArgs e)
        {
            PopulateHTMLTable();
        }

        public DataTable DataTable
        {
            get { return m_table; }
            set { m_table = value; }
        }

        public string DataGridTitle
        {
            get { return m_dataGridTitle; }
            set { m_dataGridTitle = value; }
        }



        /// <summary>
        /// Create the <tr></tr> and <td></td> tags that will populate the table.
        /// </summary>
        private void PopulateHTMLTable()
        {
            if (m_table == null)
                return;

            int visibleColumnCount = 0;
            string cellID = null;

            for (int i = ROWINDEX_SUBTITLE; i < m_table.Rows.Count; i++)
            {
                DataRow row = null;
                if (i >= 0)
                    row = m_table.Rows[i];
                TableRow tr = new TableRow();
                tr.ID = i.ToString();  //name the HTML data row column with a unique ID.
                tbl.Controls.Add(tr);

                foreach (DataColumn col in m_table.Columns)
                {

                    if (ColumnIsVisible(col))
                    {
                        visibleColumnCount++;

                        string cellvalue = string.Empty;

                        if (i == ROWINDEX_SUBTITLE)
                        {
                            //The current data row is on the SubTitle row.
                            //display the DisplayName, if it exists.
                            //otherwise, display the column name as it comes from the database.
                            if (col.ExtendedProperties[Constants.DISPLAYNAME] != null)
                                cellvalue = col.ExtendedProperties[Constants.DISPLAYNAME].ToString();
                            else
                                cellvalue = col.ColumnName;
                        }
                        else if ((row[col.ColumnName] != null) && (row[col.ColumnName] != DBNull.Value))
                        {
                            cellvalue = row[col.ColumnName].ToString();
                        }


                        if (CanMergeCells(i, col, cellvalue))
                        {
                            MergeCells(i, col);
                        }
                        else
                        {
                            //display data in its own <td> cell.
                            cellID = GetUniqueCellID(tr, col);
                            AddTableCell(tr, cellID, tbl.BorderWidth, 1, cellvalue);
                        }
                    }
                }
            }



            //Add the title to the data grid, if it exists.
            //Note:  I add the title at the bottom of this function, 
            //  because you must already know the number of visible columns.
            //  However, add the title to the TOP of the HTML table using table.Controls.AddAt()
            if (m_dataGridTitle != null)
            {
                TableRow tr = new TableRow();
                tr.ID = ROWINDEX_TITLE.ToString();  //name the HTML data row column with a unique ID.  
                tbl.Controls.AddAt(0, tr);

                cellID = GetUniqueCellID(tr, "DATAGRIDTITLE");
                AddTableCell(tr, cellID, tbl.BorderWidth, visibleColumnCount, m_dataGridTitle);
            }
        }



        /// <summary>
        /// Overload.  Applies a naming convention to uniquely identify table:tr:TD cells
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private string GetUniqueCellID(TableRow row, DataColumn col)
        {
            string cellID = GetUniqueCellID(row, col.ColumnName);
            return cellID;
        }

        /// <summary>
        /// Implementor.  Applies a naming convention to uniquely identify table:tr:TD cells
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private string GetUniqueCellID(TableRow row, string colName)
        {
            string cellID = string.Format("{0}_{1}", row.ID, colName);
            return cellID;
        }



        /// <summary>
        /// Adds a table cell to the given table row.
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="cellID"></param>
        /// <param name="borderWidth"></param>
        /// <param name="cellValue"></param>
        private void AddTableCell(TableRow tr, string cellID, Unit borderWidth, int colSpan, string cellValue)
        {
            TableCell td = new TableCell();
            td.ID = cellID;
            td.BorderWidth = borderWidth;
            td.ColumnSpan = colSpan;
            td.Text = cellValue;
            td.HorizontalAlign = HorizontalAlign.Center;
            tr.Controls.Add(td);
        }


        /// <summary>
        /// Returns TRUE if col.ExtendedProperties["VisibleColumn"] == TRUE; returns FALSE by default
        /// 
        /// I changed this function to default to FALSE to be secure-by-default --djw 10/17/07
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        private bool ColumnIsVisible(DataColumn col)
        {
            bool retval = false;
            if ((col.ExtendedProperties[Constants.VISIBLECOLUMN] != null)
                       && (col.ExtendedProperties[Constants.VISIBLECOLUMN].ToString() == true.ToString()))
            {
                retval = true;
            }
            return retval;

        }


        /// <summary>
        /// Returns TRUE if given column is marked as a Merge Column And cell value in current cell matches cell above it.
        /// </summary>
        /// <param name="iCurrentRowIndex"></param>
        /// <param name="col"></param>
        /// <param name="cellValue"></param>
        /// <returns></returns>
        private bool CanMergeCells(int iCurrentRowIndex, DataColumn col, string cellValue)
        {
            bool retval = false;

            if ((iCurrentRowIndex > 0)
                && (col.ExtendedProperties.ContainsKey(Constants.MERGECOLUMN))
                && (col.ExtendedProperties[Constants.MERGECOLUMN].ToString() == true.ToString()))
            {
                if (m_table.Rows[iCurrentRowIndex][col].ToString() == m_table.Rows[iCurrentRowIndex - 1][col].ToString())
                {
                    retval = true;
                }
            }

            return retval;
        }



        private void MergeCells(int iCurrentRowIndex, DataColumn col)
        {
            if (iCurrentRowIndex <= 0)
                throw new Exception("CurrentRowIndex must be >= 1.");

            //start at the previous row and work back up to the top of the table
            //  until you find the appropriate table cell.
            for (int i = iCurrentRowIndex - 1; i >= 0; i--)
            {

                TableRow tr = (TableRow)tbl.FindControl(i.ToString());

                if (tr != null)
                {
                    string tdCellName = string.Format("{0}_{1}", i, col.ColumnName);
                    TableCell td = (TableCell)tr.FindControl(tdCellName);
                    if (td != null)
                    {
                        if (td.RowSpan == 0)
                            td.RowSpan = 2;
                        else
                            td.RowSpan += 1;
                        break;
                    }
                }
            }
        }
    }
}