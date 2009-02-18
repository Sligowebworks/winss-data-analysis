using System;
using System.Collections.Generic;
using System.Text;


using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;


namespace SligoCS.BL.WI
{
    public class BLDropouts : EntityWIBase 
    {


        /// <summary>
        /// This function returns a strongly-typed dataset.
        /// </summary>
        /// <returns></returns>
        public v_dropoutsWWoDisSchoolDistState GetDropoutData()
        {
            DALDropouts dropouts = new DALDropouts();

            List<int> schoolTypes = GetSchoolTypesList(this.SchoolType);
            _fullKeys = GetFullKeysList(CompareTo, OrigFullKey, CompareToSchools);

            v_dropoutsWWoDisSchoolDistState ds = new v_dropoutsWWoDisSchoolDistState();
            List<string> orderBy = GetOrderByList(ds.v_DropoutsWWoDisSchoolDistState, CompareTo, OrigFullKey);
            ds = dropouts.GetDropoutData(this._fullKeys, schoolTypes, orderBy);

            this.sql = dropouts.SQL;

            return ds;            

        }
    }
}
