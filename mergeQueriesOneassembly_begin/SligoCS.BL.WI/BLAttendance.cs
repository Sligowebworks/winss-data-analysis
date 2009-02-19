using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;

namespace SligoCS.BL.WI
{
     public class BLAttendance : BLWIBase
    {
        public BLAttendance()
        {
            _gradeCode = 99;
        }

        public v_AttendanceWWoDisSchoolDistState GetAttendanceData()
        {

            List<int> schoolTypes = GetSchoolTypesList(this.SchoolType);

            //create a bunch of Generics Lists, automatically populated with a 9 (the default value).
            List<int> sexCodes, raceCodes, disabilityCodes, gradeCodes, econDisadvCodes, ELPCodes;
            base.GetViewByList(ViewBy, OrgLevel, out sexCodes, out raceCodes, out disabilityCodes, out gradeCodes, out econDisadvCodes, out ELPCodes);

            Years = base.GetYearList();

            //Special logic for which fullkeys to use.
            _fullKeys = GetFullKeysList(CompareTo, OrgLevel, OrigFullKey, CompareToSchoolsOrDistrict);

            DALAttendance Attendance = new DALAttendance();
            v_AttendanceWWoDisSchoolDistState ds = new v_AttendanceWWoDisSchoolDistState();
            List<string> orderBy = GetOrderByList(ds._v_AttendanceWWoDisSchoolDistState, CompareTo, FullKeyDecode(OrigFullKey));
            ds = Attendance.GetAttendanceData(raceCodes, sexCodes, disabilityCodes, Years,
                FullKeyDecode(_fullKeys), gradeCodes, schoolTypes, econDisadvCodes, 
                ELPCodes, orderBy);
            this.sql = Attendance.SQL;
            return ds;
        }
    }
}
