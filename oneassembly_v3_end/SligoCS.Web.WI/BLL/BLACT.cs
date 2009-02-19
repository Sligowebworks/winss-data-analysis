using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;


namespace SligoCS.BLL.WI
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

            if (ViewBy == ViewByGroup.RaceEthnicity)
            {
                raceCodes = GenericsListHelper.GetPopulatedList(1, 2, 3, 4, 5, 8);
            }

            //Special logic for which fullkeys to use.
            _fullKeys = GetFullKeysList(
                CompareTo, OrgLevel, OrigFullKey, 
                        CompareToSchoolsOrDistrict);

            bool useFullkeys = true; //
            _clauseForCompareToSchoolsDistrict =
                GetClauseForCompareToSchoolsDistrict(
                    FullKeyDecode(OrigFullKey), S4orALL,
                    CompareTo, OrgLevel,
                    SCounty, SAthleticConf, SCESA, SRegion,
                    out useFullkeys);

            DALACT act = new DALACT();
            v_ACT ds = new v_ACT();
            List<string> orderBy = GetOrderByList(
                ds._v_ACT, CompareTo, FullKeyDecode(OrigFullKey));
            ds = act.GetACTData(
                raceCodes, sexCodes, disabilityCodes, Years,
                FullKeyDecode(_fullKeys), gradeCodes, schoolTypes, 
                econDisadvCodes, ELPCodes,
                _clauseForCompareToSchoolsDistrict,
                useFullkeys,
                orderBy);

            this.sql = act.SQL;
            return ds;
        }








    }
}
