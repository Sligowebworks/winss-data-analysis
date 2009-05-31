using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.Web.WI.DAL.DataSets;
using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.BL.WI
{


    public class BLStaff : BLWIBase 
    {

        public v_Staff GetStaffData()
        {

            //List<int> schoolTypes = GetSchoolTypesList(this.STYP);

            ////create a bunch of Generics Lists, automatically populated with a 9 (the default value).
            //List<int> sexCodes, raceCodes, disabilityCodes, gradeCodes, econDisadvCodes, ELPCodes;
            //base.GetViewByList(ViewBy, OrgLevel, out sexCodes, out raceCodes, 
            //    out disabilityCodes, out gradeCodes, out econDisadvCodes, out ELPCodes);

            Years = base.GetYearList();
            
            //Special logic for which fullkeys to use.
            _fullKeys = FullKeyUtils.GetFullKeysList(CompareTo, OrgLevel, OrigFullKey, CompareToSchoolsOrDistrict, S4orALL);


            bool useFullkeys = true; //
            _clauseForCompareToSchoolsDistrict =
                GetClauseForCompareToSchoolsDistrict(
                    FullKeyUtils.FullKeyDecode(OrigFullKey), S4orALL,
                    CompareTo, OrgLevel,
                    SCounty, SAthleticConf, SCESA, SRegion,
                    out useFullkeys);

            DALStaff staff = new DALStaff();
            v_Staff ds = new v_Staff ();
            List<string> orderBy = GetOrderByList(ds._v_Staff, CompareTo, FullKeyUtils.FullKeyDecode(OrigFullKey));
            //List<string> orderBy = new List<string>();
            //orderBy.Add("PRIORYEAR");

            if (CompareTo.Key == CompareToKeys.SelSchools ||
                    CompareTo.Key == CompareToKeys.SelDistricts)
            {
                orderBy.Add(ds._v_Staff.LinkedDistrictNameColumn.ColumnName);
            }

            orderBy.Add("Category");

            ds = staff.GetStaffData(
                Years, FullKeyUtils.FullKeyDecode(_fullKeys),
                _clauseForCompareToSchoolsDistrict,
                useFullkeys,
                orderBy);
            this.sql = staff.SQL;
            return ds;
        }


        

        


        

    }
}
