using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using SligoCS.DAL.WI.DataSets;

namespace SligoCS.DAL.WI
{
    /// <summary>
    /// This DAL class was created for use with GridPage.aspx (aka Retention page).
    /// </summary>
    [Obsolete("Use class DALRetention instead.")]
    public class SampleDAL : DALWIBase
    {


        /// <summary>
        /// This function returns a strongly typed dataset to the middle tier.
        /// </summary>
        /// <param name="STYP"></param>
        /// <returns></returns>
        [Obsolete("Do not use anymore.  Use DALRetention.GetRetentionData() instead")]
        public v_RetentionWWoDisSchoolDistState GetSampleData2(int STYP)
        {
            v_RetentionWWoDisSchoolDistState ds = new v_RetentionWWoDisSchoolDistState();

            StringBuilder sb = new StringBuilder();
            sb.Append("select top 100 * from v_RetentionWWoDisSchoolDistState");

            if (STYP != 0)
                sb.AppendFormat(" where SchoolType = {0}", STYP);

            base.GetDS(ds, sb.ToString(), ds._v_RetentionWWoDisSchoolDistState.TableName);
            return ds;
        }
        
    }
}
