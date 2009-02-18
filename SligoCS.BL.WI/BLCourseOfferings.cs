using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;


namespace SligoCS.BL.WI
{
    public class BLCourseOfferings:BLWIBase
    {
        public BLCourseOfferings()
            : base()
        {
           _trendStartYear = 1997;
           _currentYear = 2007;
        }
        public v_Course_Offerings getCourseOfferingsQuery(int CourseTypeID, int WMASID1)
        {
            Years = base.GetYearList();

            _fullKeys = GetFullKeysList(CompareTo, OrgLevel, OrigFullKey, CompareToSchoolsOrDistrict);

            bool useFullkeys;  

            _clauseForCompareToSchoolsDistrict = 
                GetClauseForCompareToSchoolsDistrict(
                    FullKeyDecode(OrigFullKey), S4orALL,
                    CompareTo, OrgLevel,
                    SCounty, SAthleticConf, SCESA, SRegion,
                    out useFullkeys);

            DALCourseOfferings dalCO = new DALCourseOfferings();
            v_Course_Offerings ds = new v_Course_Offerings();

            List<string> orderBy = GetOrderByList(ds.v_COURSE_OFFERINGS, CompareTo, FullKeyDecode(OrigFullKey));

            ds = dalCO.GetData(_fullKeys, Years, CourseTypeID, WMASID1, _clauseForCompareToSchoolsDistrict, useFullkeys, orderBy);

            // to get it to compile
            return ds;
        }
    }
}
