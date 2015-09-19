using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.Web.WI.DAL.DataSets;
using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.BL.WI
{
    public class BLPOST_GRAD_INTENT : BLWIBase
    {
        public BLPOST_GRAD_INTENT()
        {
            _gradeCode = 99;
        }

        public v_POST_GRAD_INTENT GetPostGradIntent()
        {

            List<int> schoolTypes = GetSchoolTypesList(this.SchoolType);

            //create a bunch of Generics Lists, automatically populated with a 9 (the default value).
            List<int> sexCodes, raceCodes, disabilityCodes, gradeCodes, econDisadvCodes, ELPCodes;
            base.GetViewByList(ViewBy, OrgLevel, out sexCodes, out raceCodes, out disabilityCodes, out gradeCodes, out econDisadvCodes, out ELPCodes);

            Years = base.GetYearList();


            //Special logic for which fullkeys to use.
            _fullKeys = MyGetFullKeysList(CompareTo, OrgLevel, OrigFullKey, CompareToSchoolsOrDistrict);


            DALPOST_GRAD_INTENT grad = new DALPOST_GRAD_INTENT();
            v_POST_GRAD_INTENT ds = new v_POST_GRAD_INTENT();
            List<string> orderBy = GetOrderByList(ds._v_POST_GRAD_INTENT, CompareTo, FullKeyUtils.FullKeyDecode(OrigFullKey));
            if (ViewBy.Key == GroupKeys.Gender)
                orderBy.Insert(0, ds._v_POST_GRAD_INTENT.SexDescColumn.ColumnName);
            else if (ViewBy.Key == GroupKeys.Race)
                orderBy.Insert(0, ds._v_POST_GRAD_INTENT.RaceDescColumn.ColumnName);

            ds = grad.GetPostGradIntent(raceCodes, sexCodes, disabilityCodes, Years,
                FullKeyUtils.FullKeyDecode(_fullKeys), gradeCodes, schoolTypes, econDisadvCodes, ELPCodes, orderBy);
            this.sql = grad.SQL;
            return ds;
        }

        private List<string> MyGetFullKeysList(CompareTo compareTo, OrgLevel orgLevel, string origFullKey, List<string> compareToSchoolsOrDistrict)
        {
            string myOrigFullkey = origFullKey;
            if (OrgLevel.Key != OrgLevelKeys.School)
            {
                //On Post Grad Intent page, if org level == district, school level fullkey should not appear.
                if (FullKeyUtils.GetOrgLevelFromFullKeyX(origFullKey).Key == OrgLevelKeys.School)
                {
                    myOrigFullkey = FullKeyUtils.DistrictFullKey(origFullKey);
                }
            }
            return FullKeyUtils.GetFullKeysList(compareTo, orgLevel, myOrigFullkey, compareToSchoolsOrDistrict, S4orALL);
        }
    }
}
