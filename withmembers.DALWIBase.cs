using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SligoCS.DAL.WI
{
    /// <summary>
    /// Acts as a base class for all DAL classes in the Wisconsin website.
    /// </summary>
    public class DALWIBase : SligoCS.DAL.Base.DALBase
    {
        #region class level variables
        private string sql = string.Empty;
        public string fullkey;
        public List<string> compareToFullkeys;
        public List<Int16> years;
        public List<Int16> grades;
        public List<SchoolType> schooltype;
        public Int16 gender;
        public Int16 race;
        public Int16 disability;
        public Int16 econDis;
        public Int16 langProf;
        //private 
        #endregion

        #region public property accessors

       /// <summary>
        /// Typically, SQL is considered private to the DAL.  However, one of the Wisconsin
        /// development specs was to allow for trace information, including the SQL statement, 
        /// to appear on the page under certain circumstances.  To protect the privacy of the SQL, 
        /// and to ensure encapsulation, the public SQL property is read-only.
        /// </summary>
        public string SQL { get { return sql; } }

        #endregion



        #region overrides
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
