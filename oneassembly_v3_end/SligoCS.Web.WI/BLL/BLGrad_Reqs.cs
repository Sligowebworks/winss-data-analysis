using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;

namespace SligoCS.BLL.WI
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
            _fullKeys = GetFullKeysList(CompareTo, OrgLevel, OrigFullKey, CompareToSchoolsOrDistrict);

            //Subject IDs
            List<int> subjectIDs = GenericsListHelper.GetPopulatedList(1, 7);

            DALGrad_Reqs reqs = new DALGrad_Reqs();
            v_Grad_Reqs ds = new v_Grad_Reqs();
            List<string> orderBy = GetOrderByList(ds._v_Grad_Reqs, CompareTo, FullKeyDecode(OrigFullKey));
            ds = reqs.GetGradReqs(raceCodes, sexCodes, disabilityCodes, Years,
                FullKeyDecode(_fullKeys), gradeCodes, schoolTypes, 
                econDisadvCodes, ELPCodes, subjectIDs, orderBy);
            this.sql = reqs.SQL;
            return ds;
        }


        protected override List<string> GetFullKeysList(CompareTo compareTo, OrgLevel orgLevel,  string origFullKey, List<string> compareToSchoolsOrDistrict)
        {
            List<string> retval = new List<string>();
            if (OrgLevel == OrgLevel.School)
            {
                //For Grad Requirements page:  school level data are not available.  Use District instead.
                string maskedFullKey = GetMaskedFullkey(origFullKey, OrgLevel.District);
                retval.Add(maskedFullKey);
            }
            else
            {
                retval = base.GetFullKeysList(compareTo, orgLevel, origFullKey, compareToSchoolsOrDistrict);
            }

            return retval;
        }

    }
}
