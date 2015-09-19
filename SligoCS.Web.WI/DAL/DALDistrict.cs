using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.Web.WI.DAL.DataSets;

namespace SligoCS.DAL.WI
{
    public class DALDistrict : DALWIBase 
    {
        public v_Districts GetDistrict(string fullKey, int year)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from v_districts where fullkey like '{0}'", fullKey);
            sql.AppendFormat(" AND year = {0}", year);
            v_Districts ds = new v_Districts();
            base.GetDS(ds, sql.ToString(), ds._v_Districts.TableName);
            return ds;
        }
    }
}
