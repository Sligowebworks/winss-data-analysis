using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;

namespace SligoCS.BLL.WI
{   
    public class BLRetention : BLWIBase 
    {
        public BLRetention(): base()
        {
        }
        
        protected override List<int> GetYearList()
        {
            List<int> years = base.GetYearList();
            ////see bug 974. 974 cancelled by Jim
            //if (CompareTo == CompareTo.PRIORYEARS && ViewBy == ViewByGroup.Disability)
            //{
            //   years = GenericsListHelper.GetPopulatedList(2005, _currentYear);
            //}
            return years;
        }

        public v_RetentionWWoDisEconELPSchoolDistState GetRetentionData2()
        {
            List<int> schoolTypes = GetSchoolTypesList(this.SchoolType);

            //create a bunch of Generics Lists, automatically populated with a 9 (the default value).
            List<int> sexCodes, raceCodes, disabilityCodes, gradeCodes, econDisadvCodes, ELPCodes;
            
            base.GetViewByList(ViewBy, OrgLevel, out sexCodes, out raceCodes, 
                out disabilityCodes, out gradeCodes, out econDisadvCodes, out ELPCodes);

            Years = GetYearList(); // base.GetYearList();

            _fullKeys = GetFullKeysList(CompareTo, OrgLevel, 
                OrigFullKey, CompareToSchoolsOrDistrict);

            bool useFullkeys = true; //
            _clauseForCompareToSchoolsDistrict =
                GetClauseForCompareToSchoolsDistrict(
                    FullKeyDecode(OrigFullKey), S4orALL, 
                    CompareTo, OrgLevel,
                    SCounty, SAthleticConf, SCESA, SRegion, 
                    out useFullkeys);

            DALRetention retention = new DALRetention();
            v_RetentionWWoDisEconELPSchoolDistState ds = new v_RetentionWWoDisEconELPSchoolDistState();
            List<string> orderBy = GetOrderByList(ds._v_RetentionWWoDisEconELPSchoolDistState,
                CompareTo, FullKeyDecode(OrigFullKey));
            
            ds = retention.GetRetentionData2(
                raceCodes, sexCodes, disabilityCodes, Years, 
                FullKeyDecode(_fullKeys), gradeCodes, 
                schoolTypes, econDisadvCodes, ELPCodes,
                _clauseForCompareToSchoolsDistrict,
                useFullkeys,
                orderBy);
            
            this.sql = retention.SQL;
            return ds;
        }


        

        


        

    }
}
