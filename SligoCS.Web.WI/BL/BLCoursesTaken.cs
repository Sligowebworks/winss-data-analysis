using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.Web.WI.DAL.DataSets;
using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.BL.WI
{
    public class BLCoursesTaken:BLWIBase
    {
        public BLCoursesTaken()
            : base()
        {
           _trendStartYear = 1997;
           _currentYear = 2007;
        }
        public v_Coursework getCourseworkQuery(int Grade, CourseTypeID CourseTypeID, WMAS WMASID1)
        {

            List<int> sexCodes, raceCodes, disabilityCodes,
                gradeCodes, econDisadvCodes, ELPCodes;
            base.GetViewByList(ViewBy, OrgLevel, out sexCodes,
                out raceCodes, out disabilityCodes, out 
                gradeCodes, out econDisadvCodes, out ELPCodes);
            
            Years = base.GetYearList();

            _fullKeys = FullKeyUtils.GetFullKeysList(CompareTo, OrgLevel, OrigFullKey, CompareToSchoolsOrDistrict, S4orALL);

            bool useFullkeys;  

            _clauseForCompareToSchoolsDistrict = 
                GetClauseForCompareToSchoolsDistrict(
                    FullKeyUtils.FullKeyDecode(OrigFullKey), S4orALL,
                    CompareTo, OrgLevel,
                    SCounty, SAthleticConf, SCESA, SRegion,
                    out useFullkeys);

            DALCoursesTaken dalCT = new DALCoursesTaken();
            v_Coursework ds = new v_Coursework();

            List<string> orderBy = GetOrderByList(ds.v_COURSEWORK, CompareTo, FullKeyUtils.FullKeyDecode(OrigFullKey));

            ds = dalCT.GetData(_fullKeys, Years, sexCodes, Grade, CourseTypeID, WMASID1, _clauseForCompareToSchoolsDistrict, useFullkeys, orderBy);

            // to get it to compile
            return ds;
        }
    }
}
