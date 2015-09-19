using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.Web.WI.DAL.DataSets;


namespace SligoCS.BL.WI
{
    public class BLRevenue : BLWIBase
    {
        /*public v_Revenues_2 GetRevenueData()
        {

            Years = base.GetYearList();

            //before database view changed, hard code here
            //List <string> category = new List<string>();
            //category.Add("Other Local");
            //category.Add("Local: Property Taxes");
            //category.Add("Federal");

            //Special logic for which fullkeys to use.
            _fullKeys = FullKeyUtils.GetFullKeysList(CompareTo, OrgLevel, OrigFullKey, CompareToSchoolsOrDistrict, S4orALL);

            bool useFullkeys = true; //
            _clauseForCompareToSchoolsDistrict =
                GetClauseForCompareToSchoolsDistrict(
                    FullKeyUtils.FullKeyDecode(OrigFullKey), S4orALL,
                    CompareTo, OrgLevel,
                    SCounty, SAthleticConf, SCESA, SRegion,
                    out useFullkeys);

            DALRevenue dal = new DALRevenue();
            v_Revenues_2 ds = new v_Revenues_2();
            
            // before database view changed, hard code here. the base orderBy logic does not work here
            List<string> orderBy = GetOrderByList(ds._v_Revenues_2, CompareTo, FullKeyUtils.FullKeyDecode(OrigFullKey));
            //List<string> orderBy = new List<string>();
            //orderBy.Add("PRIORYEARABBR");

            ds = dal.GetRevenueData(Years, 
                FullKeyUtils.FullKeyDecode(_fullKeys),
                _clauseForCompareToSchoolsDistrict,
                useFullkeys,
                orderBy);
            this.sql = dal.SQL;
            return ds;
        }*/
    }
}
