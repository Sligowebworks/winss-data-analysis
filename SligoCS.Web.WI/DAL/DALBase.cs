using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SligoCS.DAL.Base
{
    /// <summary>
    /// This class acts as the base class for all Sligo DAL classes.
    /// </summary>
    public class DALBase
    {
        /// <summary>
        /// This is the ubiquitous GetDS statement, which can be called by derived classes.
        /// </summary>
        /// <param name="ds">The dataset to be filled</param>
        /// <param name="sql">The SQL select statement</param>
        /// <param name="tableName">The name of the table in the dataset to fill.</param>
        protected virtual void GetDS(DataSet ds, string sql, string tableName)
        {
            ds.Clear();
            string connString = ConfigurationManager.ConnectionStrings["SligoCS.Web.WI.Properties.Settings.WI_2005_03_22ConnectionString"].ConnectionString;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, connString);
                da.SelectCommand.CommandTimeout = 120;  //give a full minute instead of std 30 secs.
                System.Diagnostics.Debug.WriteLine(sql);
                da.Fill(ds, tableName);
            }
            catch (Exception e)
            {
                StringBuilder msg = new StringBuilder();

                msg.Append(e.Message);
                msg.Append("[" + sql + "]");
                msg.Append(e.TargetSite);

                throw new Exception(msg.ToString());
            }
        } 
    }
}
