using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;


namespace SligoCS.BL.WI
{
    public class BLActivities : BLWIBase
    {
        public BLActivities()
            : base()
        {
            _trendStartYear = 1997;
            _currentYear = 2007;
        }
        public v_ActivitiesSchoolDistState getActivitiesQuery(string SHOW)
        {
            Years = base.GetYearList();

            _fullKeys = GetFullKeysList(CompareTo, OrgLevel, OrigFullKey, CompareToSchoolsOrDistrict);

            List<int> schoolTypes = GetSchoolTypesList(this.SchoolType);

            bool useFullkeys;

            _clauseForCompareToSchoolsDistrict =
                GetClauseForCompareToSchoolsDistrict(
                    FullKeyDecode(OrigFullKey), S4orALL,
                    CompareTo, OrgLevel,
                    SCounty, SAthleticConf, SCESA, SRegion,
                    out useFullkeys);

            DALActivities dalAO = new DALActivities();

            v_ActivitiesSchoolDistState ds = new v_ActivitiesSchoolDistState();

            List<string> orderBy = GetOrderByList(ds._v_ActivitiesSchoolDistState, CompareTo, FullKeyDecode(OrigFullKey));

            ds = dalAO.GetActivitiesSchoolDistStateData(Years, _fullKeys, schoolTypes, SHOW, _clauseForCompareToSchoolsDistrict, useFullkeys, orderBy);

            // to get it to compile
            return ds;
        }
    }
}
