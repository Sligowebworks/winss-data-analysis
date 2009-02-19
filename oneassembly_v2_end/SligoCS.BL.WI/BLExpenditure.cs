using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;


namespace SligoCS.BL.WI
{
    public class BLExpenditure : BLWIBase
    {
        public BLExpenditure()
        {
        }
        public string Cost
        {
            get { return cost; }
            set { cost = value; }
        }
        private string cost;

        public override string GetViewByColumnName()
        {
            return base.GetCompareToColumnName();
        }

        public override string GetCompareToColumnName()
        {
            return "Category";
        }

        public v_Expend_2 GetExpenditureData()
        {

            Years = base.GetYearList();

            //before database view changed, hard code here
            //List <string> category = new List<string>();
            //category.Add("Food and Comm. Serv.");
            //category.Add("Transportation + Facilities");
            //category.Add("Current Education Cost");
            //category.Add("Total Cost");

            List <string> CT = new List<string>();
            CT.Add(cost);
            CT.Add(SligoCS.BL.WI.Constants.MONEY_PAGE_CURRENT_EDUCATION_COST);

            //Special logic for which fullkeys to use.
            _fullKeys = GetFullKeysList(CompareTo, OrgLevel, OrigFullKey, CompareToSchoolsOrDistrict);

            bool useFullkeys = true; //
            _clauseForCompareToSchoolsDistrict =
                GetClauseForCompareToSchoolsDistrict(
                    FullKeyDecode(OrigFullKey), S4orALL,
                    CompareTo, OrgLevel,
                    SCounty, SAthleticConf, SCESA, SRegion,
                    out useFullkeys);

            DALExpenditure dal = new DALExpenditure();
            v_Expend_2 ds = new v_Expend_2();
            
            // before database view changed, hard code here. the base orderBy logic does not work here
            //List<string> orderBy = GetOrderByList(ds._v_Revenues_2, CompareTo, FullKeyDecode(OrigFullKey));
            List<string> orderBy = new List<string>();
            orderBy.Add("Year desc");

            ds = dal.GetExpendData(CT, Years, 
                FullKeyDecode(_fullKeys),
                _clauseForCompareToSchoolsDistrict,
                useFullkeys, 
                orderBy);
            this.sql = dal.SQL;
            return ds;
        }
    }
}
