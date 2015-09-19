using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.Web.WI.DAL.DataSets;

namespace SligoCS.DAL.WI
{
    public class DALSchools : DALWIBase
    {
        public v_Schools GetSchool(string fullKey, int year)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from v_Schools where fullkey like '{0}'", fullKey);
            sql.AppendFormat(" AND year = {0}", year);
            v_Schools ds = new v_Schools();
            base.GetDS(ds, sql.ToString(), ds._v_Schools.TableName);
            return ds;
        }
    }
}
