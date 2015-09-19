using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;

namespace SligoCS.BL.WI
{
    /// <summary>
    /// This entity was created for use with the Demo.aspx page (aka Dropouts page),
    /// created during a working meeting on October 11 2007.  
    /// </summary>
    /// <returns></returns>
    [Obsolete("This class has been replaced by the BLDropouts class.")]
    public class DemoEntity : SligoCS.BL.WI.EntityWIBase 
    {

        /// <summary>
        /// This function returns a strongly-typed dataset.
        /// </summary>
        /// <returns></returns>
        [Obsolete("This function has been replaced with BLDropouts.GetDropoutData()")]
        public v_dropoutsWWoDisSchoolDistState GetDemoData()
        {
            DemoDAL dal = new DemoDAL();
            v_dropoutsWWoDisSchoolDistState ds = dal.GetDemoData();
            this.sql = dal.SQL;

            return ds;

        }
    }
}
