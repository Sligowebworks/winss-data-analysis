using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;

namespace SligoCS.BL.WI
{   


    public class BLTruancy : BLWIBase 
    {
        public v_TruancySchoolDistState GetTruancyData()
        {

            List<int> schoolTypes = GetSchoolTypesList(this.SchoolType);

            //create a bunch of Generics Lists, automatically 
            //    populated with a 9 (the default value).
            List<int> sexCodes, raceCodes, disabilityCodes, 
                gradeCodes, econDisadvCodes, ELPCodes;
            base.GetViewByList(ViewBy, OrgLevel, out sexCodes, 
                out raceCodes, out disabilityCodes, out 
                gradeCodes, out econDisadvCodes, out ELPCodes);

            Years = base.GetYearList();


            //Special logic for which fullkeys to use.
            _fullKeys = GetFullKeysList(CompareTo, OrgLevel, 
                OrigFullKey, CompareToSchoolsOrDistrict);


            bool useFullkeys = true; //
            _clauseForCompareToSchoolsDistrict =
                GetClauseForCompareToSchoolsDistrict(
                    FullKeyDecode(OrigFullKey), S4orALL,
                    CompareTo, OrgLevel,
                    SCounty, SAthleticConf, SCESA, SRegion,
                    out useFullkeys);

            DALTruancy Truancy = new DALTruancy();
            v_TruancySchoolDistState ds = new v_TruancySchoolDistState();
            List<string> orderBy = GetOrderByList(ds._v_TruancySchoolDistState, 
                CompareTo, FullKeyDecode(OrigFullKey));
            ds = Truancy.GetTruancySchoolDistStateData(
                raceCodes, sexCodes, disabilityCodes, Years,
                FullKeyDecode(_fullKeys), gradeCodes, 
                schoolTypes, econDisadvCodes, ELPCodes,
                _clauseForCompareToSchoolsDistrict,
                useFullkeys, 
                orderBy);

            this.sql = Truancy.SQL;
            return ds;
            
        }

    }
}
