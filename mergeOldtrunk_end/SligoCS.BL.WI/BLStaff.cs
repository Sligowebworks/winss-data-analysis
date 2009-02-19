using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;

namespace SligoCS.BL.WI
{


    public class BLStaff : BLWIBase 
    {

        public override string GetCompareToColumnName()
        {
            if (CompareTo.DISTSTATE == CompareTo)
            {
                //The old site used DistState, but the new site use [District Name]
                return "District Name";
            }
            else
            {
                return base.GetCompareToColumnName();
            }
        }

        public v_Staff GetStaffData()
        {

            //List<int> schoolTypes = GetSchoolTypesList(this.SchoolType);

            ////create a bunch of Generics Lists, automatically populated with a 9 (the default value).
            //List<int> sexCodes, raceCodes, disabilityCodes, gradeCodes, econDisadvCodes, ELPCodes;
            //base.GetViewByList(ViewBy, OrgLevel, out sexCodes, out raceCodes, 
            //    out disabilityCodes, out gradeCodes, out econDisadvCodes, out ELPCodes);

            Years = base.GetYearList();
            
            //Special logic for which fullkeys to use.
            _fullKeys = GetFullKeysList(CompareTo, OrgLevel, OrigFullKey, CompareToSchoolsOrDistrict);


            bool useFullkeys = true; //
            _clauseForCompareToSchoolsDistrict =
                GetClauseForCompareToSchoolsDistrict(
                    FullKeyDecode(OrigFullKey), S4orALL,
                    CompareTo, OrgLevel,
                    SCounty, SAthleticConf, SCESA, SRegion,
                    out useFullkeys);

            DALStaff staff = new DALStaff();
            v_Staff ds = new v_Staff ();
            List<string> orderBy = GetOrderByList(ds._v_Staff, CompareTo, FullKeyDecode(OrigFullKey));
            //List<string> orderBy = new List<string>();
            //orderBy.Add("PRIORYEAR");

            if (CompareTo == CompareTo.SELSCHOOLS)
            {
                orderBy.Add(ds._v_Staff.LinkedDistrictNameColumn.ColumnName);
            }

            orderBy.Add("Category");

            ds = staff.GetStaffData(
                Years, FullKeyDecode(_fullKeys),
                _clauseForCompareToSchoolsDistrict,
                useFullkeys,
                orderBy);
            this.sql = staff.SQL;
            return ds;
        }


        

        


        

    }
}
