using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;

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
            _fullKeys = GetFullKeysList(CompareTo, OrgLevel, OrigFullKey, CompareToSchoolsOrDistrict);


            DALPOST_GRAD_INTENT grad = new DALPOST_GRAD_INTENT();
            v_POST_GRAD_INTENT ds = new v_POST_GRAD_INTENT();
            List<string> orderBy = GetOrderByList(ds._v_POST_GRAD_INTENT, CompareTo, FullKeyDecode(OrigFullKey));
            if (ViewBy == ViewByGroup.Gender)
                orderBy.Insert(0, ds._v_POST_GRAD_INTENT.SexDescColumn.ColumnName);
            else if (ViewBy == ViewByGroup.RaceEthnicity)
                orderBy.Insert(0, ds._v_POST_GRAD_INTENT.RaceDescColumn.ColumnName);

            ds = grad.GetPostGradIntent(raceCodes, sexCodes, disabilityCodes, Years,
                FullKeyDecode(_fullKeys), gradeCodes, schoolTypes, econDisadvCodes, ELPCodes, orderBy);
            this.sql = grad.SQL;
            return ds;
        }

        protected override List<string> GetFullKeysList(CompareTo compareTo, OrgLevel orgLevel, string origFullKey, List<string> compareToSchoolsOrDistrict)
        {
            string myOrigFullkey = origFullKey;
            if (OrgLevel != OrgLevel.School)
            {
                //On Post Grad Intent page, if org level == district, school level fullkey should not appear.
                if (GetOrgLevelFromFullKeyX(origFullKey) == OrgLevel.School)
                {
                    myOrigFullkey = GetMaskedFullkey(origFullKey, OrgLevel.District);
                }
            }
            return base.GetFullKeysList(compareTo, orgLevel, myOrigFullkey, compareToSchoolsOrDistrict);
        }
    }
}
