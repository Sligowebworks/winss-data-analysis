using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;

namespace SligoCS.BL.WI
{
    public class BLStateTestsPerformance : BLWIBase
    {
        public BLStateTestsPerformance()
            : base()
        {
            _trendStartYear = 1997;
            _currentYear = 2007;
        }
        public v_WSAS GetData(int Grade, List<string> Subjects, List<int> Groups)
        {
            v_WSAS ds = new v_WSAS();
           
            Years = base.GetYearList();

            _fullKeys = GetFullKeysList(CompareTo, OrgLevel, OrigFullKey, CompareToSchoolsOrDistrict);

            bool useFullkeys;
            _clauseForCompareToSchoolsDistrict =
                GetClauseForCompareToSchoolsDistrict(
                    FullKeyDecode(OrigFullKey), S4orALL,
                    CompareTo, OrgLevel,
                    SCounty, SAthleticConf, SCESA, SRegion,
                    out useFullkeys);

           
            List<string> orderBy = GetOrderByList(ds._v_WSAS, CompareTo, FullKeyDecode(OrigFullKey));

            ds = new DALWSAS().GetData(Years, _fullKeys, Grade, Subjects, Groups, _clauseForCompareToSchoolsDistrict, useFullkeys, orderBy);
            

            return ds;
        }
    }
}
