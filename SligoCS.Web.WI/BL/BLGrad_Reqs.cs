using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.Web.WI.DAL.DataSets;
using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.BL.WI
{
    public class BLGrad_Reqs : BLWIBase
    {
        public BLGrad_Reqs()
        {
        }

        public v_Grad_Reqs GetGradReqs()
        {

            List<int> schoolTypes = GetSchoolTypesList(this.SchoolType);

            //create a bunch of Generics Lists, automatically populated with a 9 (the default value).
            List<int> sexCodes, raceCodes, disabilityCodes, gradeCodes, econDisadvCodes, ELPCodes;
            base.GetViewByList(ViewBy, OrgLevel, out sexCodes, out raceCodes, out disabilityCodes, out gradeCodes, out econDisadvCodes, out ELPCodes);

            Years = base.GetYearList();

            //Special logic for which fullkeys to use.
            _fullKeys = MyGetFullKeysList(CompareTo, OrgLevel, OrigFullKey, CompareToSchoolsOrDistrict);

            //Subject IDs
            List<int> subjectIDs = GenericsListHelper.GetPopulatedList(1, 7);

            DALGrad_Reqs reqs = new DALGrad_Reqs();
            v_Grad_Reqs ds = new v_Grad_Reqs();
            List<string> orderBy = GetOrderByList(ds._v_Grad_Reqs, CompareTo, FullKeyUtils.FullKeyDecode(OrigFullKey));
            ds = reqs.GetGradReqs(raceCodes, sexCodes, disabilityCodes, Years,
                FullKeyUtils.FullKeyDecode(_fullKeys), gradeCodes, schoolTypes, 
                econDisadvCodes, ELPCodes, subjectIDs, orderBy);
            this.sql = reqs.SQL;
            return ds;
        }


        private List<string> MyGetFullKeysList(CompareTo compareTo, OrgLevel orgLevel,  string origFullKey, List<string> compareToSchoolsOrDistrict)
        {
            List<string> retval = new List<string>();
            if (OrgLevel.Key == OrgLevelKeys.School)
            {
                //For Grad Requirements page:  school level data are not available.  Use District instead.
                string maskedFullKey = FullKeyUtils.DistrictFullKey(origFullKey);
                retval.Add(maskedFullKey);
            }
            else
            {
                retval = FullKeyUtils.GetFullKeysList(compareTo, orgLevel, origFullKey, compareToSchoolsOrDistrict, S4orALL);
            }

            return retval;
        }

    }
}
