using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
    public class DataTableSorter
    {
        public static void ImportRows(DataTable table, DataRow[] rows)
        {
            foreach (DataRow row in rows)
            {
                table.ImportRow(row);
            }
        }
        public static String RemoveAscDescModifier(String column)
        {
            System.Text.RegularExpressions.Regex expr = new System.Text.RegularExpressions.Regex(" ASC$| DESC$| asc$| desc$| Asc$| Desc$");
            column = column.Trim();
            
            return expr.Replace(column, String.Empty);
        }
        public static DataTable SecondarySortCompareSelectedFloatToTop(DataTable sourceTable, SligoCS.BL.WI.QueryMarshaller qm, String orderBy)
        {
            DataTable temp = sourceTable.Clone();
            DataTable result = sourceTable.Clone();
            DataRow[] rows;
            String primary = RemoveAscDescModifier(orderBy.Split(',')[0]);
            String secondary = RemoveAscDescModifier(orderBy.Split(',')[1]);
            String[] order = orderBy.Split(',');
            String[] secondOrder = new String[order.Length -1];
            Array.ConstrainedCopy(order, 1, secondOrder, 0, order.Length - 1);

            String distinct = String.Empty;

            foreach (DataRow row in sourceTable.Select(String.Empty , primary))
            {
                if (distinct != row[primary].ToString())
                {
                    temp.Clear();
                    distinct = row[primary].ToString();
                    //throw new Exception(primary + "=" + distinct);
                    rows = sourceTable.Select("[" +primary+ "]" + "='" + distinct + "'");
                    foreach(DataRow subrow in rows) 
                    {
                        temp.ImportRow(subrow);
                    }

                    temp = SortAndCompareSelectedFloatToTop(temp, qm, String.Join(", ", secondOrder));

                    result.Merge(temp);
                }
            }
            return result;
        }
        public static DataTable SortAndCompareSelectedFloatToTop(DataTable sourceTable, SligoCS.BL.WI.QueryMarshaller qm)
        {
            return SortAndCompareSelectedFloatToTop(sourceTable, qm,
                SligoCS.DAL.WI.SQLHelper.ConvertToCSV(qm.BuildOrderByList(sourceTable.Columns), false)
            );
        }
        public static DataTable SortAndCompareSelectedFloatToTop(DataTable sourceTable, SligoCS.BL.WI.QueryMarshaller qm, String orderBy)
        {
            return SortAndCompareSelectedFloatToTop(sourceTable, qm, orderBy, qm.compareSelectedFullKeys);
        }
        public static DataTable SortAndCompareSelectedFloatToTop(DataTable sourceTable, SligoCS.BL.WI.QueryMarshaller qm, String orderBy, Boolean ForceFloatToTop)
        {
            DataTable table = sourceTable.Clone();
            String fullkey = SligoCS.BL.WI.FullKeyUtils.GetMaskedFullkey(qm.GlobalValues.FULLKEY, qm.GlobalValues.OrgLevel);

            if (ForceFloatToTop)
            {
                DataRow[] current = sourceTable.Select("fullkey = '" + fullkey + "'", orderBy);
                DataRow[] rest = sourceTable.Select("fullkey <> '" + fullkey + "'", orderBy);
                ImportRows(table, current);
                ImportRows(table, rest);
            }
            else
            {
                DataRow[] sortedRows = sourceTable.Select(String.Empty, orderBy);
                ImportRows(table, sortedRows);
            }
            return table;
        }
        public static DataTable SortTable(DataTable theTable, String sortClause)
        {
            DataTable sortedTable = theTable.Clone();
            DataRow[] sortedRows = theTable.Select(String.Empty, sortClause);
            ImportRows(sortedTable, sortedRows);
            return sortedTable;
        }
    }
}
