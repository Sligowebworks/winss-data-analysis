using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;


namespace SligoCS.BL.WI
{
    public class BLAthleticConf : BLWIBase
    {
        public BLAthleticConf()
        {
        }

        public v_Athletic_Conf GetAthleticConfList(int year)
        {
            DALAthleticConf dal = new DALAthleticConf();
            v_Athletic_Conf ds = dal.GetAthleticConfList(year);
            this.sql = dal.SQL;
            return ds;
        }

        public v_Athletic_Conf GetAthleticConfList()
        {
            //Notes: will remove '+1' when 2008 data in place
            return GetAthleticConfList (base.CurrentYear + 1);
        }

        public string GetAthleticConfNameByID(int conferenceKey)
        {
            DALAthleticConf dal = new DALAthleticConf();
            //Notes: will remove '+1' when 2008 data in place
            return dal.GetAthleticConfNameByID(conferenceKey, CurrentYear + 1);
        }
    }
}
