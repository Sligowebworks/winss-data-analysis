using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI.DataSets;

namespace SligoCS.DAL.WI
{
    /// <summary>
    /// This DAL class was created for the use with Demo.aspx (aka Dropouts page).
    /// </summary>
    [Obsolete("This class has been replaced with DALDropouts")]
    public class DemoDAL : SligoCS.DAL.WI.DALWIBase 
    {

        /// <summary>
        /// This function returns a strongly typed dataset.
        /// </summary>
        /// <returns></returns>
        [Obsolete("This function has been replaced with DALDropouts.GetDropoutData")]
        public v_dropoutsWWoDisSchoolDistState GetDemoData()
        {
            //TODO:  Finish something here.
            string mySql = "select top 100 * from v_dropoutsWWoDisSchoolDistState";

            v_dropoutsWWoDisSchoolDistState ds = new v_dropoutsWWoDisSchoolDistState();

            base.GetDS(ds, mySql, ds.v_DropoutsWWoDisSchoolDistState.TableName);

            return ds;
        }
    }
}
