using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;

namespace SligoCS.BL.WI
{

    /// <summary>
    /// This entity was created for use with the GridPage.aspx (aka Retention page).
    /// </summary>
    [Obsolete ("Use class BLRention instead.")]
    public class SampleEntity : EntityWIBase
    {
        /// <summary>
        /// The 1st tier, the client layer, calls only methods in the business layer (2nd tier).
        /// In this case, the business layer is a simple passthrough, but it will frequently 
        /// contain business rule logic.    
        /// </summary>
        /// <returns></returns>
        [Obsolete("Use BLRetention.GetRetentionData() instead.")]
        public v_RetentionWWoDisSchoolDistState GetSampleData(int STYP)
        {            
            SampleDAL dal = new SampleDAL();
            v_RetentionWWoDisSchoolDistState ds = dal.GetSampleData2(STYP);
            this.sql = dal.SQL;
            return ds;
        }
    }
}
