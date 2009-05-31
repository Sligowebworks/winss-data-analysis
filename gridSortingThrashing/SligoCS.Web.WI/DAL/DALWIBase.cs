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
                if (dataSet == null) throw new Exception("DataSet not set");
                return dataSet;
            }
        }
        #endregion



        #region overrides
        public DataSet Query()
        {
            GetDS(dataSet, SQL, "");

            return dataSet;
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

        public string GetScalarStringByID(System.Data.DataSet ds, string columnName)
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

        public string FullKeyXsFromZs(string fullkeyIn)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(fullkeyIn) == false)
            {
                result = fullkeyIn.ToLower().Replace("zzzz", "xxxx");
            }
            return result;
        }
        #endregion


    }
}
