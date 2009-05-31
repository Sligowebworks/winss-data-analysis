using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.Web.WI.DAL.DataSets;
using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.BL.WI
{
    public class BLTeacherQualificationsScatterPlot : BLWIBase
    {

        public BLTeacherQualificationsScatterPlot()
        {
            _trendStartYear = 2003;
            _currentYear = 2008;
        }

        /*public override string GetViewByColumnName()
        {
            if ((OrgLevel != OrgLevel.School) && (STYP == STYP.AllTypes))
            {
                return "SchooltypeLabel2";
            }
            else
            {
                return base.GetViewByColumnName();
            }
        }*/


        public v_TeacherQualificationsScatterPlot 
            GetTeacherQualificationsScatterPlot(
            string linkSubjectCode,
            string teacherVariableCode,
            string relatedToKey,
            string locationCode,
            int county,
            string cesa
            )
        {
            List<int> schoolTypes = GetSchoolTypesList(this.SchoolType);

            //create a bunch of Generics Lists, automatically 
            // populated with a 9 (the default value).
            List<int> sexCodes, raceCodes, disabilityCodes,
                gradeCodes, econDisadvCodes, ELPCodes;
            base.GetViewByList(ViewBy, OrgLevel, out sexCodes, out raceCodes, 
                out disabilityCodes, out gradeCodes, out econDisadvCodes, out ELPCodes);

            Years= new List<int>();
            Years.Add(_currentYear - 1);
            Years.Add(_currentYear);

            //Special logic for which fullkeys to use.
            _fullKeys = MyGetFullKeysList(CompareTo,
                OrgLevel, OrigFullKey, CompareToSchoolsOrDistrict);

            DALTeacherQualificationsScatterPlot teachers = 
                    new DALTeacherQualificationsScatterPlot();
            v_TeacherQualificationsScatterPlot ds = 
                    new v_TeacherQualificationsScatterPlot();
            List<string> orderBy = new List<string>();
                //  GetOrderByList(
                //      ds.v_TeacherQualificationsScatterplot, 
                //      CompareToEnum, FullKeyUtils.FullKeyDecode(OrigFullKey));

            string temp = string.Format("(case {0} when '{1}' then {2} else {3} end)",
                OrderByCols.fullkey.ToString(),
                FullKeyUtils.GetMaskedFullkey( OrigFullKey, this.OrgLevel ),
                "' '+" + OrderByCols.Name.ToString(),
                OrderByCols.Name.ToString());
            orderBy.Add(temp);

            ds = teachers.GetTeacherQualificationsScatterPlot(
                _currentYear.ToString(),
                FullKeyUtils.FullKeyDecode(_fullKeys), 
                schoolTypes, 
                linkSubjectCode,
                teacherVariableCode,
                relatedToKey,
                locationCode,
                county,
                cesa,
                base.OrgLevel.ToString(),
                orderBy 
                );
            
            this.sql =  teachers.SQL;
            return ds;
        }

        protected  List<string> MyGetFullKeysList(
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
