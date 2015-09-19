using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.Web.WI.DAL.DataSets;

namespace SligoCS.DAL.WI.DataSets
{
    public class DALSuspendExpel : DALWIBase
    {
        v_SuspensionsDis _ds = new v_SuspensionsDis();
        v_SuspensionsDisTableAdapters.v_SuspensionsDisTableAdapter _ta = new v_SuspensionsDisTableAdapters.v_SuspensionsDisTableAdapter();

        public v_SuspensionsDis getSuspensionDisData(
            List<int> races,
            List<int> sexes,
            List<int> disabilityCode,
            List<int> grades,
            List<int> schooltypes,
            List<int> years,
            List<string> fullKey,
            string clauseForCompareToSchoolsDistrict,
            bool useFullkeys,
            List<string> orderBy)
        {
            //SELECT * FROM v_SuspensionsDis
            return _ds; 
        }
    }
}
