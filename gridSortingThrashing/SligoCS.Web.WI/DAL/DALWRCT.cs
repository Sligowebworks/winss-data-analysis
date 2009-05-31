using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.Web.WI.DAL.DataSets;

namespace SligoCS.DAL.WI
{
    public class DALWRCT : DALWIBase
    {
        public v_WRCT GetWRCT(string fullKey)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from v_WRCT where fullkey = '{0}'", fullKey);
            sql.AppendFormat("and (Enrolled <> '--' and [Advanced + Proficient Total] <> '--')");
            v_WRCT ds = new v_WRCT();
            base.GetDS(ds, sql.ToString(), ds._v_WRCT.TableName);
            return ds;
        }
    }
}
