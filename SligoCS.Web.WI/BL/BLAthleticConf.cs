using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.Web.WI.DAL.DataSets;


namespace SligoCS.BL.WI
{
    public class BLAthleticConf : BLWIBase
    {
        public BLAthleticConf()
        {
        }

        public v_Athletic_Conf GetAthleticConfList()
        {
            DALAthleticConf dal = new DALAthleticConf();
            v_Athletic_Conf ds = dal.GetAthleticConfList();
            this.sql = dal.SQL;
            return ds;
        }

        public string GetAthleticConfNameByID(int conferenceKey)
        {
            DALAthleticConf dal = new DALAthleticConf();
            return dal.GetAthleticConfNameByID(conferenceKey);
        }
    }
}
