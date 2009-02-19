using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;

namespace SligoCS.BL.WI
{

    public class BLExpulsionsDaysLost : BLWIBase
    {

        public v_Explulsions GetExpulsionsDaysLostData()
        {

            List<int> schoolTypes = GetSchoolTypesList(this.SchoolType);

            //create a bunch of Generics Lists, automatically 
            //    populated with a 9 (the default value).
            List<int> sexCodes, raceCodes, disabilityCodes,
                gradeCodes, econDisadvCodes, ELPCodes;
            base.GetViewByList(ViewBy, OrgLevel, out sexCodes,
                out raceCodes, out disabilityCodes, out 
                gradeCodes, out econDisadvCodes, out ELPCodes);

            // jdj: must override default grade when ViewBY <> Grade
            if (ViewBy != ViewByGroup.Grade)
            {
                gradeCodes = GenericsListHelper.GetPopulatedList(99);
            }

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

            DALExpulsionsDaysLost ExpulsionsDaysLost = new DALExpulsionsDaysLost();
            v_Explulsions ds = new v_Explulsions();
            List<string> orderBy = GetOrderByList(ds.v_Expulsions,
                CompareTo, FullKeyDecode(OrigFullKey));
            ds = ExpulsionsDaysLost.GetExpulsionsDaysLostData(
                raceCodes, sexCodes, disabilityCodes, Years,
                FullKeyDecode(_fullKeys), gradeCodes,
                schoolTypes, econDisadvCodes, ELPCodes,
                _clauseForCompareToSchoolsDistrict,
                useFullkeys,
                orderBy);

            this.sql = ExpulsionsDaysLost.SQL;
            return ds;

        }

    }
}
