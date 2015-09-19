using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using SligoCS.BL.WI;

namespace SligoCS.DAL.WI
{
    /// <summary>
    /// Acts as a base class for all DAL classes in the Wisconsin website.
    /// </summary>
    public class DALWIBase : SligoCS.DAL.Base.DALBase
    {
        #region class level variables
        private string sql = string.Empty;
        private DataSet dataSet;
        //private 
        #endregion

        #region public property accessors
        public string SQL 
        {
            get
            {
                if (sql == null) throw new Exception("SQL String not set");
                return sql; 
            }
            set { sql = value; }
        }
        public DataSet DataSet
        {
            set { dataSet = value; }
            get
            {
                if (dataSet == null) dataSet = new DataSet();
                return dataSet;
            }
        }
        #endregion



        #region overrides
        public void Query()
        {
            string tableName =
                 (DataSet == null || DataSet.Tables.Count < 1) ? "Default"
                : DataSet.Tables[0].TableName;

            GetDS(DataSet, SQL, tableName);
            foreach (System.Data.DataColumn col in DataSet.Tables[tableName].Columns)
            {
                System.Diagnostics.Debug.Write("[" + col.ColumnName + "]");
            }
            foreach (System.Data.DataColumn col in DataSet.Tables[tableName].DefaultView.Table.Columns)
            {
                System.Diagnostics.Debug.Write("DV[" + col.ColumnName + "]");
            }
        }

        public virtual String BuildSQL(QueryMarshaller Marshaller)
        {
            return String.Empty;
        }

        /// <summary>
        /// This function overrides the base function GetDS.  The only change in behavior is 
        /// setting the class-level SQL statement, which, by development spec, must be available
        /// for trace information.
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="sql"></param>
        /// <param name="tableName"></param>
        protected override void GetDS(DataSet ds, string sql, string tableName)
        {
            this.sql = sql;
            base.GetDS(ds, sql, tableName);
        }

        public static string GetScalarStringByID(System.Data.DataSet ds, string columnName)
        {
            string result = string.Empty;
            if (ds.Tables[0].Rows.Count > 1)
            {
                throw new Exception("More than one record returned query by ID.");
            }
            foreach (System.Data.DataRow row in ds.Tables[0].Rows)
            {
                result = row[columnName].ToString();
            }
            return result;
        }
        #endregion

        public String GetStringColumn(String field)
        {
            return (Marshalled) ? Table.Rows[0][field].ToString() : String.Empty;
        }
        public System.Data.DataTable Table
        {
            get
            {
                return DataSet.Tables[0];
            }
        }
        public Boolean Marshalled
        {
            get
            {
                return (!DataSet.HasErrors && DataSet.Tables.Count > 0);
            }
        }
        public static String SQLSafeString(String str)
        {
            if (String.IsNullOrEmpty(str)) return str;

            return str.ToUpper()
                .Replace("'", "''")
                .Replace(";", String.Empty)
                .Replace("--", String.Empty)
                .Replace("SELECT ", String.Empty)
                .Replace("INSERT ", String.Empty)
                .Replace("UPDATE ", String.Empty)
                .Replace("DELETE ", String.Empty)
            ;
        }
    }
}
