using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;

namespace SligoCS.BL.WI
{

    public class BLSuspExpIncidents : BLWIBase
    {

        public v_SuspExpIncidents GetSuspExpIncidentsData()
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

            DALSuspExpIncidents SuspExpIncidents = new DALSuspExpIncidents();
            v_SuspExpIncidents ds = new v_SuspExpIncidents();
            List<string> orderBy = GetOrderByList(ds._v_SuspExpIncidents,
                CompareTo, FullKeyDecode(OrigFullKey));
            ds = SuspExpIncidents.GetSuspExpIncidentsData(
                raceCodes, sexCodes, disabilityCodes, Years,
                FullKeyDecode(_fullKeys), gradeCodes,
                schoolTypes, econDisadvCodes, ELPCodes,
                _clauseForCompareToSchoolsDistrict,
                useFullkeys,
                orderBy);

            this.sql = SuspExpIncidents.SQL;
            return ds;

        }

    }
}
