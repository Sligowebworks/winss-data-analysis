using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI.DataSets;

namespace SligoCS.DAL.WI
{
    public class DALDropouts : DALWIBase 
    {
        /// <summary>
        /// Returns a strongly typed dataset.
        /// </summary>
        /// <returns></returns>
        public v_dropoutsWWoDisSchoolDistState GetDropoutData(List<string> fullKey, 
            List<int> schoolType,
            List<string> orderBy)
        {
            
            StringBuilder sb = new StringBuilder();
            SQLHelper sql = new SQLHelper();


            //TODO:  remove top 100
            sb.Append("select top 100 * from v_dropoutsWWoDisSchoolDistState WHERE ");

            //School Types
            sb.Append(sql.WhereClauseCSV(SQLHelper.WhereClauseJoiner.NONE, "SchoolType", schoolType));
            
            //fullkey
            sb.Append(sql.WhereClauseCSV(SQLHelper.WhereClauseJoiner.AND, "FullKey", fullKey));


            //order by clause
            sb.AppendFormat(" ORDER BY {0}", sql.ConvertToCSV(orderBy, false));

            v_dropoutsWWoDisSchoolDistState ds = new v_dropoutsWWoDisSchoolDistState();

            base.GetDS(ds, sb.ToString(), ds.v_DropoutsWWoDisSchoolDistState.TableName);

            return ds;
        }
    }
}
