using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.Web.WI;
using SligoCS.BL.WI;
using SligoCS.DAL.WI;

using NUnit.Extensions.Asp;
using NUnit.Framework;

namespace SligoCS.Tests.WI.NUnitASPTesters
{
    [TestFixture]
    public class AttendancePageTester : BaseTester
    {
        private AttendancePage attendancePage;


        [TestFixtureSetUp]
        public void Init()
        {
            attendancePage = new AttendancePage();
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            attendancePage = null;
        }
        
        [Test]
        public void InheritsPageBaseWI()
        {
            Boolean myBln = false;
            
            if ((Object)attendancePage is SligoCS.Web.Base.PageBase.WI.PageBaseWI) myBln = true;
          
            Assert.IsTrue(myBln, "Does not inherit from PageBaseWI");
        }

        [Test]
        public void Bug806B_IncorrectFullKeyinSQL()
        {
            //RULE: attendance page: orglevel = district, school type = high, view by= grade, compare to dist/state
            //should NOT see the school-level fullkey in the sql statement.

            string url = "http://localhost:31489/SligoWI/AttendancePage.aspx?FULLKEY=013619040022&GraphFile=HIGHSCHOOLCOMPLETION&CompareTo=DISTSTATE&ORGLEVEL=District&Group=Grade&DN=Milwaukee&SN=Madison%20Hi&DETAIL=YES&NumSchools=4&ZBackTo=performance.aspx&FileName=c:\\sample.txt&SchoolWebaddress=http://mpsportal.milwaukee.k12.wi.us&DistrictWebaddress=http://www.milwaukee.k12.wi.us&STYP=3&COUNTY=40&YearLocal=2007&TrendStartYearLocal=1997&WhichSchool=4&ConferenceKey=27&DistrictID=3619&GradeBreakout=999&GradeBreakoutLAG=555&GradeBreakoutEDISA=212&TraceLevels=4";
            Browser.GetPage(url);

            Assert.IsTrue(!Browser.CurrentPageText.Contains("FullKey in ('XXXXXXXXXXXX', '013619040022',"), "Should not contain school-level fullkey in where clause.");
        }

        [Test]
        public void Bug617_GetVisibleColumns()
        {
            //Bug 617:
            //  when Attendance: District: All Types: All Students: Compare To: Prior Years

            AttendancePage attendancePage = new AttendancePage();
            List<string> cols = attendancePage.GetVisibleColumns(ViewByGroup.AllStudentsFAY, OrgLevel.District, CompareTo.PRIORYEARS, SchoolType.AllTypes);
            Assert.IsTrue(cols.Count == 6, "should have 6 columns, found " + cols.Count);
            Assert.IsTrue(cols.Contains("YearFormatted"), "missing column on Retention page.");
            Assert.IsTrue(cols.Contains("SchooltypeLabel"), "missing column on Retention page.");
            Assert.IsTrue(cols.Contains("Enrollment PreK-12"), "missing column on Retention page.");
            Assert.IsTrue(cols.Contains("Actual Days Of Attendance"), "missing column on Retention page.");
            Assert.IsTrue(cols.Contains("Possible Days Of Attendance"), "missing column on Retention page.");
            Assert.IsTrue(cols.Contains("Attendance Rate"), "missing column on Retention page.");

        }

        [Test]
        public void Bug806_GetVisibleColumns()
        {
            //Bug 617:
            //  when Attendance: District: High : Grades: Dist/state

            AttendancePage attendancePage = new AttendancePage();
            List<string> cols = attendancePage.GetVisibleColumns(ViewByGroup.Grade, OrgLevel.District, CompareTo.DISTSTATE, SchoolType.Hi);
            Assert.IsTrue(cols.Count == 6, "should have 6 columns, found " + cols.Count);
            Assert.IsTrue(cols.Contains("District Name"), "missing column on Retention page.");
            Assert.IsTrue(cols.Contains("GradeLabel"), "missing column on Retention page.");
            Assert.IsTrue(cols.Contains("Enrollment PreK-12"), "missing column on Retention page.");
            Assert.IsTrue(cols.Contains("Actual Days Of Attendance"), "missing column on Retention page.");
            Assert.IsTrue(cols.Contains("Possible Days Of Attendance"), "missing column on Retention page.");
            Assert.IsTrue(cols.Contains("Attendance Rate"), "missing column on Retention page.");

        }


        [Test]
        public void Bug807_SQL()
        {
            //AttendancePage attendancePage = new AttendancePage();
            //BLAttendance att = new BLAttendance();
            //attendancePage.ParamsHelper.LoadFromContext( StickyParameter);
            //attendancePage.PrepBLEntity(att);
            //att.GetAttendanceData();

            ////should be '((GradeCode >= 99) AND (GradeCode <=99))' not '((GradeCode >= 98) AND (GradeCode <=98))'
            //Assert.IsTrue(!att.SQL.Contains("((GradeCode >= 98) AND (GradeCode <= 98))"), "Error in SQL: should be '((GradeCode >= 99) AND (GradeCode <=99))' not '((GradeCode >= 98) AND (GradeCode <=98))'");
            //Assert.IsTrue(att.SQL.Contains("((GradeCode >= 99) AND (GradeCode <= 99))"), "Error in SQL: should be '((GradeCode >= 99) AND (GradeCode <=99))' not '((GradeCode >= 98) AND (GradeCode <=98))'");

        }
        
    }
}
