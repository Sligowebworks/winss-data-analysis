using System;
using System.Collections.Generic;
using System.Text;


using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;


namespace SligoCS.BL.WI
{
    public class BLDropouts : BLWIBase 
    {


        /// <summary>
        /// This function returns a strongly-typed dataset.
        /// </summary>
        /// <returns></returns>
        [Obsolete("Use GetDropoutData2()")]
        public v_dropoutsWWoDisSchoolDistState GetDropoutData()
        {
            DALDropouts dropouts = new DALDropouts();

            List<int> schoolTypes = GetSchoolTypesList(this.SchoolType);
            _fullKeys = GetFullKeysList(CompareTo, OrgLevel, OrigFullKey, CompareToSchoolsOrDistrict);

            List<int> gradeCodeRange = GetGradeCodes();
            Years = base.GetYearList();

            List<int> sexCodes, raceCodes, disabilityCodes, gradeCodes, econDisadvCodes, ELPCodes;
            GetViewByList(ViewBy, OrgLevel, out sexCodes, out raceCodes, out disabilityCodes, out gradeCodes, out econDisadvCodes, out ELPCodes);

            

            v_dropoutsWWoDisSchoolDistState ds = new v_dropoutsWWoDisSchoolDistState();
            List<string> orderBy = GetOrderByList(ds.v_DropoutsWWoDisSchoolDistState, CompareTo, FullKeyDecode(OrigFullKey));
            ds = dropouts.GetDropoutData(
               FullKeyDecode(this._fullKeys), schoolTypes, Years, sexCodes, 
               raceCodes, disabilityCodes, gradeCodes, orderBy);

            this.sql = dropouts.SQL;

            return ds;            

        }


        public v_DropoutsWWoDisEconELPSchoolDistState GetDropoutData2()
        {
            DALDropouts dropouts = new DALDropouts();

            List<int> schoolTypes = GetSchoolTypesList(this.SchoolType);
            _fullKeys = GetFullKeysList(CompareTo, OrgLevel, OrigFullKey, CompareToSchoolsOrDistrict);

            List<int> gradeCodeRange = GetGradeCodes();
            Years = base.GetYearList();

            List<int> sexCodes, raceCodes, disabilityCodes, gradeCodes, econDisadvCodes, ELPCodes;
            GetViewByList(ViewBy, OrgLevel, out sexCodes, out raceCodes, out disabilityCodes, out gradeCodes, out econDisadvCodes, out ELPCodes);

            v_DropoutsWWoDisEconELPSchoolDistState ds = new v_DropoutsWWoDisEconELPSchoolDistState();
            List<string> orderBy = GetOrderByList(ds._v_DropoutsWWoDisEconELPSchoolDistState, CompareTo, FullKeyDecode(OrigFullKey));
            ds = dropouts.GetDropoutData2(FullKeyDecode(this._fullKeys), schoolTypes, 
                Years, sexCodes, raceCodes, disabilityCodes, gradeCodes, econDisadvCodes, 
                ELPCodes, orderBy);

            this.sql = dropouts.SQL;

            return ds;

        }

        /// <summary>
        /// Overrides GetViewByList()
        /// </summary>
        /// <param name="viewBy"></param>
        /// <param name="orgLevel"></param>
        /// <param name="sexCodes"></param>
        /// <param name="raceCodes"></param>
        /// <param name="disabilityCodes"></param>
        /// <param name="gradeCodes"></param>
        protected override void GetViewByList(ViewByGroup viewBy, OrgLevel orgLevel, out List<int> sexCodes, out List<int> raceCodes, 
            out List<int> disabilityCodes, out List<int> gradeCodes, out List<int> econDisadvCodes, out List<int> ELPCodes)
        {
            //basically the same as the base GetViewByList(),
            //except the Grade Code List.  See Bug 386 for details.
            base.GetViewByList(viewBy, orgLevel, out sexCodes, out raceCodes, 
                out disabilityCodes, out gradeCodes, out econDisadvCodes, out ELPCodes);

            gradeCodes = GetGradeCodes();
        }

        private List<int> GetGradeCodes()
        {
            //From bug 386:
            //when View By <> Grade: WHERE parameter for Dropouts should be 
            //" AND gradecode = '95' "
            //when View By = Grade and OrgLevel = SC: WHERE parameter for Dropouts should be 
            //"AND ((GradeCode >= 52) AND (GradeCode <= 64)) "
            //when View By = Grade and OrgLevel <> SC: WHERE parameter for Dropouts should be 
            //"AND ((GradeCode >= 44) AND (GradeCode <= 64)) "

            List<int> gradeCodeRange = GenericsListHelper.GetPopulatedList(95);

            // jdj: I think below might be wrong - see how I handled grade override in BLSuspensions.cs et al
            if (ViewBy == ViewByGroup.Grade)
            {
                if (OrgLevel == OrgLevel.School)
                    gradeCodeRange = GenericsListHelper.GetPopulatedList(52, 64);
                else
                    gradeCodeRange = GenericsListHelper.GetPopulatedList(44, 64);
            }
            return gradeCodeRange;
        }
    }
}
