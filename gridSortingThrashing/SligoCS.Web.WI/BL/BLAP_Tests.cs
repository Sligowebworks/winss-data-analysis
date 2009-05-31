using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.Web.WI.DAL.DataSets;
using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.BL.WI
{
    public class BLAP_Tests: BLWIBase
    {
        private List<string> newCompareToSchools = new List<string>();
        private string newOrigFullKey = string.Empty;
        private OrgLevel newOrgLevel = new OrgLevel();

        public BLAP_Tests()
        {
            _gradeCode = 96;
            _currentYear = 2007;
        }

        //Notes for graph
       /* public override string GetViewByColumnName()
        {
            //There is no RaceShortLabel column in this page, so we need to use the Full paramName
            if (ViewBy.Key == GroupKeys.Race)
            {
                return "RaceLabel";
            }
            else
            {
                return base.GetViewByColumnName();
            }
        }*/

        //Notes for graph
       /* public override string GetCompareToColumnName()
        {
            if (CompareTo == CompareToEnum.DISTSTATE)
            {
                return "District paramName";
            }
            else
            {
                return base.GetCompareToColumnName();
            }
        }*/


        private  List<string> MyGetFullKeysList(
            CompareTo compareTo, 
            OrgLevel orgLevel, string origFullKey, 
            List<string> compareToSchoolsOrDistrict)
        {
            List<string> retval = new List<string>();


            if (OrgLevel.Key == OrgLevelKeys.School)
            {
                newOrigFullKey = FullKeyUtils.DistrictFullKey(origFullKey);
                newOrgLevel.Value = newOrgLevel.Range[OrgLevelKeys.District];
                //convert each school into a district fullkey
                foreach (string fullkey in compareToSchoolsOrDistrict)
                {
                    string maskedFullkey = FullKeyUtils.GetMaskedFullkey(fullkey, OrgLevelKeys.District);
                    newCompareToSchools.Add(maskedFullkey);
                }
            }

            if (orgLevel.Key == OrgLevelKeys.District)
            {
                newOrgLevel = orgLevel;
                newOrigFullKey = origFullKey;
                newCompareToSchools = compareToSchoolsOrDistrict;
            }

            else if (orgLevel.Key == OrgLevelKeys.State)
            {
                newOrgLevel = orgLevel;
                newOrigFullKey = FullKeyUtils.StateFullKey(origFullKey);
            }

            retval = FullKeyUtils.GetFullKeysList(compareTo, newOrgLevel, newOrigFullKey, newCompareToSchools, S4orALL);

            return retval;
        }

        public v_AP_TESTS GetAPTestData()
        {

            List<int> schoolTypes = GetSchoolTypesList(this.SchoolType);

            //create a bunch of Generics Lists, automatically populated with a 9 (the default value).
            List<int> sexCodes, raceCodes, disabilityCodes, gradeCodes, econDisadvCodes, ELPCodes;
            base.GetViewByList(ViewBy, OrgLevel, out sexCodes, out raceCodes, out disabilityCodes, out gradeCodes, out econDisadvCodes, out ELPCodes);

            Years = base.GetYearList();

            if (ViewBy.Key == GroupKeys.Race)
            {
                raceCodes = GenericsListHelper.GetPopulatedList(1, 2, 3, 4, 5, 8);
            }

            //Special logic for which fullkeys to use.
            _fullKeys = MyGetFullKeysList(CompareTo, OrgLevel, 
                OrigFullKey, CompareToSchoolsOrDistrict);

            bool useFullkeys = true; //
            _clauseForCompareToSchoolsDistrict =
                GetClauseForCompareToSchoolsDistrict(
                    FullKeyUtils.FullKeyDecode(newOrigFullKey), S4orALL,
                    CompareTo, newOrgLevel,
                    SCounty, SAthleticConf, SCESA, SRegion,
                    out useFullkeys);


            //Exam Codes
            List<int> examCodes = GenericsListHelper.GetPopulatedList(99);

            DALAP_TESTS tests = new DALAP_TESTS();
            v_AP_TESTS ds = new v_AP_TESTS();
            List<string> orderBy = GetOrderByList(ds._v_AP_TESTS, 
                CompareTo, newOrigFullKey);
            ds = tests.GetAPTestData(raceCodes, sexCodes, disabilityCodes,
                Years, FullKeyUtils.FullKeyDecode(_fullKeys)
                , gradeCodes, schoolTypes, econDisadvCodes, 
                ELPCodes, examCodes,
                _clauseForCompareToSchoolsDistrict,
                useFullkeys,
                orderBy);
            this.sql = tests.SQL;
            return ds;
        }

    }
}
