using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.Web.WI.DAL.DataSets;
using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.BL.WI
{
    public class BLACT : BLWIBase
    {
        public BLACT()
        {
            _gradeCode = 99;
            _currentYear = 2008; 
        }

        public v_ACT GetACTData()
        {

            List<int> schoolTypes = GetSchoolTypesList(this.SchoolType);

            //create a bunch of Generics Lists, automatically populated with a 9 (the default value).
            List<int> sexCodes, raceCodes, disabilityCodes, gradeCodes, econDisadvCodes, ELPCodes;
            base.GetViewByList(ViewBy, OrgLevel, out sexCodes, out raceCodes, 
                out disabilityCodes, out gradeCodes, out econDisadvCodes, out ELPCodes);

            Years = base.GetYearList();

            if (ViewBy.Key == GroupKeys.Race)
            {
                raceCodes = GenericsListHelper.GetPopulatedList(1, 2, 3, 4, 5, 8);
            }

            //Special logic for which fullkeys to use.
            _fullKeys = FullKeyUtils.GetFullKeysList(
                CompareTo, OrgLevel, OrigFullKey, 
                        CompareToSchoolsOrDistrict, S4orALL);

            bool useFullkeys = true; //
            _clauseForCompareToSchoolsDistrict =
                GetClauseForCompareToSchoolsDistrict(
                    FullKeyUtils.FullKeyDecode(OrigFullKey), S4orALL,
                    CompareTo, OrgLevel,
                    SCounty, SAthleticConf, SCESA, SRegion,
                    out useFullkeys);

            DALACT act = new DALACT();
            v_ACT ds = new v_ACT();
            List<string> orderBy = GetOrderByList(
                ds._v_ACT, CompareTo, FullKeyUtils.FullKeyDecode(OrigFullKey));
            ds = act.GetACTData(
                raceCodes, sexCodes, disabilityCodes, Years,
                FullKeyUtils.FullKeyDecode(_fullKeys), gradeCodes, schoolTypes, 
                econDisadvCodes, ELPCodes,
                _clauseForCompareToSchoolsDistrict,
                useFullkeys,
                orderBy);

            this.sql = act.SQL;
            return ds;
        }








    }
}
