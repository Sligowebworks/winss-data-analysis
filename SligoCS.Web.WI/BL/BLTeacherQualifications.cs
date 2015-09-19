using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.Web.WI.DAL.DataSets;

using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.BL.WI
{
    public class BLTeacherQualifications : BLWIBase
    {

        public BLTeacherQualifications()
        {
            _trendStartYear = 2003;
            _currentYear = 2008;
        }

        public v_TeacherQualifications GetTeacherQualifications(string subjectCode)
        {
            List<int> schoolTypes = GetSchoolTypesList(this.SchoolType);

            //create a bunch of Generics Lists, automatically 
            // populated with a 9 (the default value).
            List<int> sexCodes, raceCodes, disabilityCodes,
                gradeCodes, econDisadvCodes, ELPCodes;
            base.GetViewByList(ViewBy, OrgLevel, out sexCodes, out raceCodes, 
                out disabilityCodes, out gradeCodes, out econDisadvCodes, out ELPCodes);

            Years = base.GetYearList();

            //Special logic for which fullkeys to use.
            _fullKeys = MyGetFullKeysList(CompareTo, 
                OrgLevel, OrigFullKey, CompareToSchoolsOrDistrict);

            bool useFullkeys = true; //
            _clauseForCompareToSchoolsDistrict =
                GetClauseForCompareToSchoolsDistrict(
                    FullKeyUtils.FullKeyDecode(OrigFullKey), S4orALL,
                    CompareTo, OrgLevel,
                    SCounty, SAthleticConf, SCESA, SRegion,
                    out useFullkeys);

            DALTeacherQualifications teachers = new DALTeacherQualifications();
            v_TeacherQualifications ds = new v_TeacherQualifications();
            List<string> orderBy = GetOrderByList(ds._v_TeacherQualifications, 
                CompareTo, FullKeyUtils.FullKeyDecode(OrigFullKey));

            ds = teachers.GetTeacherQualifications(
                raceCodes, sexCodes, Years, 
                FullKeyUtils.FullKeyDecode(_fullKeys), schoolTypes, 
               subjectCode,
                _clauseForCompareToSchoolsDistrict,
                useFullkeys, 
                orderBy 
                );
            
            this.sql =  teachers.SQL;
            return ds;
        }

        private List<string> MyGetFullKeysList(
            CompareTo compareTo, OrgLevel orgLevel, string origFullKey, 
            List<string> compareToSchoolsOrDistrict)
        {
            List<string> retval = FullKeyUtils.GetFullKeysList(compareTo, 
                orgLevel, origFullKey, compareToSchoolsOrDistrict, S4orALL);

            if (this.OrgLevel.Key != OrgLevelKeys.School)
            {
                List<string> maskedVals = new List<string>();
                foreach (string fullkey in retval)
                {
                    string maskedVal = FullKeyUtils.GetMaskedFullkey(fullkey, this.OrgLevel);
                    maskedVals.Add(maskedVal);
                }
                retval = maskedVals;
            }

            return retval;
        }

    }
}
