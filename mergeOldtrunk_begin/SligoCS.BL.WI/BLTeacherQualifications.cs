using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;

namespace SligoCS.BL.WI
{
    public class BLTeacherQualifications : BLWIBase
    {

        public BLTeacherQualifications()
        {
            _trendStartYear = 2003;
            _currentYear = 2008;
        }

        public override string GetViewByColumnName()
        {
            if ((OrgLevel != OrgLevel.School) && (SchoolType == SchoolType.AllTypes))
            {
                return "SchooltypeLabel2";
            }
            else
            {
                return base.GetViewByColumnName();
            }
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
            _fullKeys = GetFullKeysList(CompareTo, 
                OrgLevel, OrigFullKey, CompareToSchoolsOrDistrict);

            bool useFullkeys = true; //
            _clauseForCompareToSchoolsDistrict =
                GetClauseForCompareToSchoolsDistrict(
                    FullKeyDecode(OrigFullKey), S4orALL,
                    CompareTo, OrgLevel,
                    SCounty, SAthleticConf, SCESA, SRegion,
                    out useFullkeys);

            DALTeacherQualifications teachers = new DALTeacherQualifications();
            v_TeacherQualifications ds = new v_TeacherQualifications();
            List<string> orderBy = GetOrderByList(ds._v_TeacherQualifications, 
                CompareTo, FullKeyDecode(OrigFullKey));
            ds = teachers.GetTeacherQualifications(
                raceCodes, sexCodes, Years, 
                FullKeyDecode(_fullKeys), schoolTypes, 
               subjectCode,
                _clauseForCompareToSchoolsDistrict,
                useFullkeys, 
                orderBy 
                );
            
            this.sql =  teachers.SQL;
            return ds;
        }

        protected override List<string> GetFullKeysList(
            CompareTo compareTo, OrgLevel orgLevel, string origFullKey, 
            List<string> compareToSchoolsOrDistrict)
        {
            List<string> retval = base.GetFullKeysList(compareTo, 
                orgLevel, origFullKey, compareToSchoolsOrDistrict);

            if (this.OrgLevel != OrgLevel.School)
            {
                List<string> maskedVals = new List<string>();
                foreach (string fullkey in retval)
                {
                    string maskedVal = GetMaskedFullkey(fullkey, this.OrgLevel);
                    maskedVals.Add(maskedVal);
                }
                retval = maskedVals;
            }

            return retval;
        }

    }
}
