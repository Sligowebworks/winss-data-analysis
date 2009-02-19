using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;

namespace SligoCS.BLL.WI
{


    public class BLHSCompletion : BLWIBase
    {
        public BLHSCompletion()
        {
            _gradeCode = 64;
        }

        public v_HSCWWoDisSchoolDistState GetHSCompletionData(int startYear)
        {

            List<int> schoolTypes = GetSchoolTypesList(this.SchoolType);

            //create a bunch of Generics Lists, automatically populated with a 9 (the default value).
            List<int> sexCodes, raceCodes, disabilityCodes, gradeCodes, econDisadvCodes, ELPCodes;
            base.GetViewByList(ViewBy, OrgLevel, out sexCodes, out raceCodes, out disabilityCodes, out gradeCodes, out econDisadvCodes, out ELPCodes);

            Years = GetYearList(startYear);

            //Special logic for which fullkeys to use.
            _fullKeys = GetFullKeysList(CompareTo, OrgLevel, OrigFullKey, CompareToSchoolsOrDistrict);

            bool useFullkeys = true; //
            _clauseForCompareToSchoolsDistrict =
                GetClauseForCompareToSchoolsDistrict(
                    FullKeyDecode(OrigFullKey), S4orALL,
                    CompareTo, OrgLevel,
                    SCounty, SAthleticConf, SCESA, SRegion,
                    out useFullkeys);

            DALHSCompletion HSCompletion = new DALHSCompletion();
            v_HSCWWoDisSchoolDistState ds = new v_HSCWWoDisSchoolDistState();
            List<string> orderBy = GetOrderByList(ds._v_HSCWWoDisSchoolDistState, CompareTo, FullKeyDecode(OrigFullKey));
            ds = HSCompletion.GetHSCompletionData(raceCodes, sexCodes, disabilityCodes, Years,
                FullKeyDecode(_fullKeys), gradeCodes, schoolTypes, 
                econDisadvCodes, ELPCodes,
                _clauseForCompareToSchoolsDistrict,
                useFullkeys,
                orderBy);
            this.sql = HSCompletion.SQL;
            return ds;
        }









    }
}
