using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;


namespace SligoCS.BLL.WI
{
    public class BLAP_Tests: BLWIBase
    {
        private List<string> newCompareToSchools = new List<string>();
        private string newOrigFullKey = string.Empty;
        private OrgLevel newOrgLevel;

        public BLAP_Tests()
        {
            _gradeCode = 96;
            _currentYear = 2007;
        }

        //Notes for graph
        public override string GetViewByColumnName()
        {
            //There is no RaceShortLabel column in this page, so we need to use the Full Name
            if (ViewBy == ViewByGroup.RaceEthnicity)
            {
                return "RaceLabel";
            }
            else
            {
                return base.GetViewByColumnName();
            }
        }

        //Notes for graph
        public override string GetCompareToColumnName()
        {
            if (CompareTo == CompareTo.DISTSTATE)
            {
                return "District Name";
            }
            else
            {
                return base.GetCompareToColumnName();
            }
        }


        protected override List<string> GetFullKeysList(
            CompareTo compareTo, 
            OrgLevel orgLevel, string origFullKey, 
            List<string> compareToSchoolsOrDistrict)
        {
            List<string> retval = new List<string>();


            if (OrgLevel == OrgLevel.School)
            {
                newOrigFullKey = GetMaskedFullkey(origFullKey, OrgLevel.District);
                newOrgLevel = OrgLevel.District;
                //convert each school into a district fullkey
                foreach (string fullkey in compareToSchoolsOrDistrict)
                {
                    string maskedFullkey = GetMaskedFullkey(fullkey, OrgLevel.District);
                    newCompareToSchools.Add(maskedFullkey);
                }
            }

            if (orgLevel == OrgLevel.District)
            {
                newOrgLevel = orgLevel;
                newOrigFullKey = origFullKey;
                newCompareToSchools = compareToSchoolsOrDistrict;
            }

            else if (orgLevel == OrgLevel.State)
            {
                newOrgLevel = orgLevel;
                newOrigFullKey = GetMaskedFullkey(origFullKey, OrgLevel.State);
            }

            retval = base.GetFullKeysList(compareTo, newOrgLevel, newOrigFullKey, newCompareToSchools);

            return retval;
        }

        public v_AP_TESTS GetAPTestData()
        {

            List<int> schoolTypes = GetSchoolTypesList(this.SchoolType);

            //create a bunch of Generics Lists, automatically populated with a 9 (the default value).
            List<int> sexCodes, raceCodes, disabilityCodes, gradeCodes, econDisadvCodes, ELPCodes;
            base.GetViewByList(ViewBy, OrgLevel, out sexCodes, out raceCodes, out disabilityCodes, out gradeCodes, out econDisadvCodes, out ELPCodes);

            Years = base.GetYearList();

            if (ViewBy == ViewByGroup.RaceEthnicity)
            {
                raceCodes = GenericsListHelper.GetPopulatedList(1, 2, 3, 4, 5, 8);
            }

            //Special logic for which fullkeys to use.
            _fullKeys = GetFullKeysList(CompareTo, OrgLevel, 
                OrigFullKey, CompareToSchoolsOrDistrict);

            bool useFullkeys = true; //
            _clauseForCompareToSchoolsDistrict =
                GetClauseForCompareToSchoolsDistrict(
                    FullKeyDecode(newOrigFullKey), S4orALL,
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
                Years, FullKeyDecode(_fullKeys)
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
