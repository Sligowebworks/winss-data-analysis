using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using SligoCS.Web.WI;
using SligoCS.BL.WI;
using SligoCS.DAL.WI;
using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.Tests.WI.NUnitASPTesters
{
    [TestFixture]
    public class HSCompletionPageTester : BaseTester 
    {
        [Test]
        public void Bug617_GetVisibleColumns()
        {
            Group group = new Group();
            group.Value = group.Range[GroupKeys.All];

            HSCompletionPage HSCompletionPage = new HSCompletionPage();
            List<string> cols = HSCompletionPage.GetVisibleColumns(group, OrgLevel.District, CompareToEnum.PRIORYEARS, SchoolType.AllTypes);
            AssertColumns(cols, 8);
            AssertColumn(cols, "YearFormatted");
            AssertColumn(cols, "Total Enrollment Grade 12");
            AssertColumn(cols, "Total Expected to Complete High School");
            AssertColumn(cols, "Cohort_Dropouts_Count");
            AssertColumn(cols, "Students Who Reached the Maximum Age");
            AssertColumn(cols, "Certificates");
            AssertColumn(cols, "HSEDs");
            AssertColumn(cols, "Regular Diplomas");
        }


        [Test]
        public void Bug529_ExplanatoryText()
        {
            //RULE: HS Completion page should contain explanatory text.

            string url = "http://localhost:31489/SligoWI/HSCompletionPage.aspx?FULLKEY=013619040022&GraphFile=HIGHSCHOOLCOMPLETION&CompareTo=DISTSTATE&ORGLEVEL=District&Group=Grade&DN=Milwaukee&SN=Madison%20Hi&DETAIL=YES&NumSchools=4&ZBackTo=performance.aspx&FileName=c:\\sample.txt&SchoolWebaddress=http://mpsportal.milwaukee.k12.wi.us&DistrictWebaddress=http://www.milwaukee.k12.wi.us&STYP=3&COUNTY=40&YearLocal=2007&TrendStartYearLocal=1997&WhichSchool=4&ConferenceKey=27&DistrictID=3619&GradeBreakout=999&GradeBreakoutLAG=555&GradeBreakoutEDISA=212&TraceLevels=SQLStatement";
            Browser.GetPage(url);

            Assert.IsTrue(Browser.CurrentPageText.Contains("Graduation (regular diploma) "), "Expected explanatory text for HS Completion page.");

            //should see "Credential:", View By, and Compare To.
            AssertParamsLink("View By:");
            AssertParamsLink("Compare To:");
            AssertParamsLink("Credential:");

            //should see links at bottom of page.
            Assert.IsTrue(Browser.CurrentPageText.Contains("View Adequate Yearly Progress Report"), "Should see links at bottom of page.");

        }


    }
}
