using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.Web.WI.DAL.DataSets;

namespace SligoCS.BL.WI
{
    public class BLExpulsionServices :BLWIBase
    {
        public BLExpulsionServices()
            : base()
        {

        }

        public v_ExpulsionServices GetExpulsionServicesData()
        {
            DALExpulsionServices dal = new DALExpulsionServices();
            v_ExpulsionServices ds = new v_ExpulsionServices();

            Years = base.GetYearList();

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

            List<string> orderBy = GetOrderByList(
                ds.vExpulsionServices, CompareTo, FullKeyUtils.FullKeyDecode(OrigFullKey));

            ds = dal.GetExpulsionServicesData (
               Years,
                FullKeyUtils.FullKeyDecode(_fullKeys),
                 _clauseForCompareToSchoolsDistrict,
                useFullkeys,
                orderBy);

            //for sql trace
            this.sql = dal.SQL;

            return ds;
        }

        public vExpulsionServicesAndReturns GetExpulsionAndReturnsData()
        {
            DALExpulsionServices dal = new DALExpulsionServices();
            vExpulsionServicesAndReturns ds = new vExpulsionServicesAndReturns();

            Years = base.GetYearList();

            _fullKeys = FullKeyUtils.GetFullKeysList(
                CompareTo, OrgLevel, OrigFullKey, 
                        CompareToSchoolsOrDistrict, S4orALL);

            bool useFullkeys = true; 

            _clauseForCompareToSchoolsDistrict =
                GetClauseForCompareToSchoolsDistrict(
                        FullKeyUtils.FullKeyDecode(OrigFullKey), S4orALL,
                        CompareTo, OrgLevel,
                        SCounty, SAthleticConf, SCESA, SRegion,
                        out useFullkeys);

            List<string> orderBy = GetOrderByList(
                ds._vExpulsionServicesAndReturns, CompareTo, FullKeyUtils.FullKeyDecode(OrigFullKey));

            ds = dal.GetExpulsionAndReturnsData(
               Years,
                FullKeyUtils.FullKeyDecode(_fullKeys),
                 _clauseForCompareToSchoolsDistrict,
                useFullkeys,
                orderBy);

            //for sql trace
            this.sql = dal.SQL;

            return ds;
        }
    }
}
