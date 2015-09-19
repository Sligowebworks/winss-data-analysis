using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.Web.WI;
using SligoCS.BL.WI;
using SligoCS.DAL.WI;
using SligoCS.Web.WI.WebSupportingClasses.WI;

using NUnit.Framework;
using NUnit.Extensions.Asp;

namespace SligoCS.Tests.WI.NUnitASPTesters
{
    [TestFixture]
    public class APTestsPageTester : BaseTester
    {

        [Test]
        [Ignore]
        public void Bug842_APTests_Columns()
        {

            //AP Tests: ViewBy All Students: Compare To Prior Years:  display these columns:
            // v_AP_TESTS.PriorYear as ' '
            //, v_AP_TESTS.Enrollment as 'Enrollment Grades 9-12'
            //, v_AP_TESTS.[# Taking Exams]
            //, v_AP_TESTS.[% Taking Exams]
            //, v_AP_TESTS.[# Exams Taken]
            //, v_AP_TESTS.[# Exams Passed]
            //, v_AP_TESTS.[% of Exams Passed] as '% of Exams Passed (Score of 3 or Above)' 

            Group group = new Group();
            group.Value = group.Range[GroupKeys.All];

            APTestsPage ap = new APTestsPage();
            List<string> cols = ap.GetVisibleColumns(group, SligoCS.BL.WI.OrgLevel.District, SligoCS.BL.WI.CompareToEnum.PRIORYEARS, SchoolType.AllTypes);
            Assert.IsTrue(cols.Count == 7, "Expected 7 columns on AP Exams page.");
            AssertColumn(cols, "PriorYear");
            AssertColumn(cols, "Enrollment");
            AssertColumn(cols, "# Taking Exams");
            AssertColumn(cols, "% Taking Exams");
            AssertColumn(cols, "# Exams Taken");
            AssertColumn(cols, "# Exams Passed");
            AssertColumn(cols, "% of Exams Passed");






            //AP Tests: ViewBy All Students: Compare To DIST/STATE:  display these columns:
            // v_AP_TESTS.DistState 
            //, v_AP_TESTS.Enrollment as 'Enrollment Grades 9-12'
            //, v_AP_TESTS.[# Taking Exams]
            //, v_AP_TESTS.[% Taking Exams]
            //, v_AP_TESTS.[# Exams Taken]
            //, v_AP_TESTS.[# Exams Passed]
            //, v_AP_TESTS.[% of Exams Passed] as '% of Exams Passed (Score of 3 or Above)' 

            cols = ap.GetVisibleColumns(group, SligoCS.BL.WI.OrgLevel.District, SligoCS.BL.WI.CompareToEnum.DISTSTATE, SchoolType.AllTypes);
            Assert.IsTrue(cols.Count == 7, "Expected 7 columns on AP Exams page.");
            AssertColumn(cols, "DistState");
            AssertColumn(cols, "Enrollment");
            AssertColumn(cols, "# Taking Exams");
            AssertColumn(cols, "% Taking Exams");
            AssertColumn(cols, "# Exams Taken");
            AssertColumn(cols, "# Exams Passed");
            AssertColumn(cols, "% of Exams Passed");



            //AP Tests: ViewBy Race/Ethnicity: Compare To DIST/STATE:  display these columns:
            // v_AP_TESTS.DistState 
            //, v_AP_TESTS.RaceLabel
            //, v_AP_TESTS.Enrollment as 'Enrollment Grades 9-12'
            //, v_AP_TESTS.[# Taking Exams]
            //, v_AP_TESTS.[% Taking Exams]
            //, v_AP_TESTS.[# Exams Taken]
            //, v_AP_TESTS.[# Exams Passed]
            //, v_AP_TESTS.[% of Exams Passed] as '% of Exams Passed (Score of 3 or Above)' 

            group.Value = group.Range[GroupKeys.Race];

            cols = ap.GetVisibleColumns(group, SligoCS.BL.WI.OrgLevel.District, SligoCS.BL.WI.CompareToEnum.DISTSTATE, SchoolType.AllTypes);
            Assert.IsTrue(cols.Count == 8, "Expected 8 columns on AP Exams page.");
            AssertColumn(cols, "DistState");
            AssertColumn(cols, "RaceLabel");
            AssertColumn(cols, "Enrollment");
            AssertColumn(cols, "# Taking Exams");
            AssertColumn(cols, "% Taking Exams");
            AssertColumn(cols, "# Exams Taken");
            AssertColumn(cols, "# Exams Passed");
            AssertColumn(cols, "% of Exams Passed");




        }


        [Test]
        public void Bug892_APTestPageCrashes()
        {
            //should not crash!
            Browser.GetPage("http://localhost:31489/SligoWI/APTestsPage.aspx?FULLKEY=013619040022&GraphFile=AP&CompareTo=DISTSTATE&ORGLEVEL=School&Group=AllStudentsFAY&DN=Milwaukee&SN=Madison%20Hi&DETAIL=YES&NumSchools=4&ZBackTo=performance.aspx&FileName=c:\\sample.txt&SchoolWebaddress=http://mpsportal.milwaukee.k12.wi.us&DistrictWebaddress=http://www.milwaukee.k12.wi.us&STYP=3&COUNTY=40&YearLocal=2007&TrendStartYearLocal=1997&WhichSchool=4&ConferenceKey=27&DistrictID=3619&GradeBreakout=999&GradeBreakoutLAG=555&GradeBreakoutEDISA=212&TraceLevels=SQLStatement");
            Browser.GetPage("http://localhost:31489/SligoWI/APTestsPage.aspx?FULLKEY=013619040022&GraphFile=AP&CompareTo=SELSCHOOLS&ORGLEVEL=School&Group=AllStudentsFAY&DN=Milwaukee&SN=Madison%20Hi&DETAIL=YES&NumSchools=4&ZBackTo=performance.aspx&FileName=c:\\sample.txt&SchoolWebaddress=http://mpsportal.milwaukee.k12.wi.us&DistrictWebaddress=http://www.milwaukee.k12.wi.us&STYP=3&COUNTY=40&YearLocal=2007&TrendStartYearLocal=1997&WhichSchool=4&ConferenceKey=27&DistrictID=3619&GradeBreakout=999&GradeBreakoutLAG=555&GradeBreakoutEDISA=212&TraceLevels=SQLStatement");
            Browser.GetPage("http://localhost:31489/SligoWI/APTestsPage.aspx?FULLKEY=013619040022&GraphFile=AP&CompareTo=CURRENTONLY&ORGLEVEL=School&Group=AllStudentsFAY&DN=Milwaukee&SN=Madison%20Hi&DETAIL=YES&NumSchools=4&ZBackTo=performance.aspx&FileName=c:\\sample.txt&SchoolWebaddress=http://mpsportal.milwaukee.k12.wi.us&DistrictWebaddress=http://www.milwaukee.k12.wi.us&STYP=3&COUNTY=40&YearLocal=2007&TrendStartYearLocal=1997&WhichSchool=4&ConferenceKey=27&DistrictID=3619&GradeBreakout=999&GradeBreakoutLAG=555&GradeBreakoutEDISA=212&TraceLevels=SQLStatement");
        }


        [Test]
        [Ignore]
        public void Bug892B_APTestPageNotShowingStateLevel()
        {
            //Rule:  when compareto = dist/state, should show District AND state in grid.
            string url = "http://localhost:31489/SligoWI/APTestsPage.aspx?FULLKEY=013619040022&GraphFile=AP&CompareTo=DISTSTATE&ORGLEVEL=School&GROUP=AllStudentsFAY&DN=Milwaukee&SN=Madison%20Hi&DETAIL=YES&NumSchools=4&ZBackTo=performance.aspx&FileName=c:\\sample.txt&SchoolWebaddress=http://mpsportal.milwaukee.k12.wi.us&DistrictWebaddress=http://www.milwaukee.k12.wi.us&STYP=3&COUNTY=40&YearLocal=2007&TrendStartYearLocal=1997&WhichSchool=4&ConferenceKey=27&DistrictID=3619&GradeBreakout=999&GradeBreakoutLAG=555&GradeBreakoutEDISA=212&TraceLevels=SQLStatement";
            Browser.GetPage(url);

            Assert.IsTrue(Browser.CurrentPageText.Contains(@"<td align=""center"">Milwaukee"), "Expected district level data.");
            Assert.IsTrue(Browser.CurrentPageText.Contains(@"WI Public Schools"), "Expected state level data.");

            //Even though this URL is at school level, 
            //we should never see Compare To:  "Selected Schools", only "Selected Districts"
            Assert.IsTrue(!Browser.CurrentPageText.Contains("Selected Schools"), "Should never see 'Selected Schools' on AP Exams page.");
            Assert.IsTrue(!Browser.CurrentPageText.Contains("Selected&nbsp;Schools"), "Should never see 'Selected Schools' on AP Exams page.");

            Assert.IsTrue(Browser.CurrentPageText.Contains("Selected&nbsp;Districts"), "Should see 'Selected Districts' on AP Exams page.");


            //when compareto = selected districts, make sure Milwaukee appears.
            url = "http://localhost:31489/SligoWI/APTestsPage.aspx?TQShow=LICSTAT&TQSubjects=SUMALL&FULLKEY=013619040022&GraphFile=AP&CompareTo=SELSCHOOLS&ORGLEVEL=School&Group=AllStudentsFAY&DN=Milwaukee&SN=Madison%20Hi&DETAIL=YES&NumSchools=4&ZBackTo=performance.aspx&FileName=c:\\sample.txt&SchoolWebaddress=http://mpsportal.milwaukee.k12.wi.us&DistrictWebaddress=http://www.milwaukee.k12.wi.us&STYP=3&COUNTY=40&YearLocal=2007&TrendStartYearLocal=1997&WhichSchool=4&ConferenceKey=27&DistrictID=3619&GradeBreakout=999&GradeBreakoutLAG=555&GradeBreakoutEDISA=212&TraceLevels=4";
            Browser.GetPage(url);            
            Assert.IsTrue(Browser.CurrentPageText.Contains(@"<td align=""center"">Milwaukee"), "Expected district level data.");


            //when org level = state, must see data.
            url = "http://localhost:31489/SligoWI/APTestsPage.aspx?FULLKEY=013619040022&GraphFile=AP&CompareTo=SELSCHOOLS&ORGLEVEL=State&Group=AllStudentsFAY&DN=Milwaukee&SN=Madison%20Hi&DETAIL=YES&NumSchools=4&ZBackTo=performance.aspx&FileName=c:\\sample.txt&SchoolWebaddress=http://mpsportal.milwaukee.k12.wi.us&DistrictWebaddress=http://www.milwaukee.k12.wi.us&STYP=3&COUNTY=40&YearLocal=2007&TrendStartYearLocal=1997&WhichSchool=4&ConferenceKey=27&DistrictID=3619&GradeBreakout=999&GradeBreakoutLAG=555&GradeBreakoutEDISA=212&TraceLevels=SQLStatement";
            Browser.GetPage(url);
            Assert.IsTrue(Browser.CurrentPageText.Contains(@"WI Public Schools"), "Expected state level data.");
        }


        [Test]
        public void Bug890_APTestsLinkStillUnderlined()
        {
            //When AP Exams page is selected, link is black, but should NOT be underlined.
            string url = "http://localhost:31489/SligoWI/APTestsPage.aspx?FULLKEY=013619040022&GraphFile=AP&CompareTo=PRIORYEARS&ORGLEVEL=School&Group=AllStudentsFAY&DN=Milwaukee&SN=Madison%20Hi&DETAIL=YES&NumSchools=4&ZBackTo=performance.aspx&FileName=c:\\sample.txt&SchoolWebaddress=http://mpsportal.milwaukee.k12.wi.us&DistrictWebaddress=http://www.milwaukee.k12.wi.us&STYP=3&COUNTY=40&YearLocal=2007&TrendStartYearLocal=1997&WhichSchool=4&ConferenceKey=27&DistrictID=3619&GradeBreakout=999&GradeBreakoutLAG=555&GradeBreakoutEDISA=212&TraceLevels=None";
            Browser.GetPage(url);
            
            Assert.IsTrue(!Browser.CurrentPageText.Contains("id=\"ctl00_ContentPlaceHolder1_ParamsLinkBox1_linkShowGraphfile_AP\" href=\"/SligoWI/APTestsPage.aspx?")
                , "AP Exam page should not contain active links to AP Exam page.  Link should be disabled.");

            //AP page SHOULD contain active link to ACT page.
            Assert.IsTrue(Browser.CurrentPageText.Contains("id=\"ctl00_ContentPlaceHolder1_ParamsLinkBox1_linkShowGraphfile_ACT\" href=\"/SligoWI/ACTPage.aspx?"), "AP page should contain active links to ACT page");



            //ACT page should not contain active link to ACT page.
            url = "http://localhost:31489/SligoWI/ACTPage.aspx?FULLKEY=013619040022&GraphFile=ACT&CompareTo=PRIORYEARS&ORGLEVEL=School&Group=AllStudentsFAY&DN=Milwaukee&SN=Madison%20Hi&DETAIL=YES&NumSchools=4&ZBackTo=performance.aspx&FileName=c:\\sample.txt&SchoolWebaddress=http://mpsportal.milwaukee.k12.wi.us&DistrictWebaddress=http://www.milwaukee.k12.wi.us&STYP=3&COUNTY=40&YearLocal=2007&TrendStartYearLocal=1997&WhichSchool=4&ConferenceKey=27&DistrictID=3619&GradeBreakout=999&GradeBreakoutLAG=555&GradeBreakoutEDISA=212&TraceLevels=None";
            Browser.GetPage(url);
            Assert.IsTrue(!Browser.CurrentPageText.Contains("id=\"ctl00_ContentPlaceHolder1_ParamsLinkBox1_linkShowGraphfile_ACT\" href=\"/SligoWI/ACTPage.aspx?"), "ACT page should not contain active links to ACT page.  Link should be disabled.");
            //ACT page SHOULD contain active link to AP Exams page.
            Assert.IsTrue(Browser.CurrentPageText.Contains("id=\"ctl00_ContentPlaceHolder1_ParamsLinkBox1_linkShowGraphfile_AP\" href=\"/SligoWI/APTestsPage.aspx?")
                , "ACT page should contain active links to AP Exam page.");



        }

    }
}
