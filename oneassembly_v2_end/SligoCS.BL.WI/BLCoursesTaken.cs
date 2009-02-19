using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;


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
        public v_Coursework getCourseworkQuery(int Grade, int CourseTypeID, int WMASID1)
        {

            List<int> sexCodes, raceCodes, disabilityCodes,
                gradeCodes, econDisadvCodes, ELPCodes;
            base.GetViewByList(ViewBy, OrgLevel, out sexCodes,
                out raceCodes, out disabilityCodes, out 
                gradeCodes, out econDisadvCodes, out ELPCodes);
            
            Years = base.GetYearList();

            _fullKeys = GetFullKeysList(CompareTo, OrgLevel, OrigFullKey, CompareToSchoolsOrDistrict);

            bool useFullkeys;  

            _clauseForCompareToSchoolsDistrict = 
                GetClauseForCompareToSchoolsDistrict(
                    FullKeyDecode(OrigFullKey), S4orALL,
                    CompareTo, OrgLevel,
                    SCounty, SAthleticConf, SCESA, SRegion,
                    out useFullkeys);

            DALCoursesTaken dalCT = new DALCoursesTaken();
            v_Coursework ds = new v_Coursework();

            List<string> orderBy = GetOrderByList(ds.v_COURSEWORK, CompareTo, FullKeyDecode(OrigFullKey));

            ds = dalCT.GetData(_fullKeys, Years, sexCodes, Grade, CourseTypeID, WMASID1, _clauseForCompareToSchoolsDistrict, useFullkeys, orderBy);

            // to get it to compile
            return ds;
        }
    }
}
