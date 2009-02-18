using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.Web.WI;
using SligoCS.BL.WI;
using SligoCS.DAL.WI;

using NUnit.Framework;
using NUnit.Extensions.Asp;

namespace SligoCS.Tests.WI.NUnitASPTesters
{
    [TestFixture]
    public class PostGradPageTester : BaseTester
    {

        [Test]
        public void Bug844_PostGrad_Columns()
        {

            PostGradIntentPage postGrad = new PostGradIntentPage();

            //RULE:  Show=all, ViewBy=All Students, CompareTo=PriorYears:
            //select PriorYear as ' '
            //, [Number of Graduates]
            //, [% 4-Year College]
            //, [% Voc/Tech College]
            //, [% Employment] as [% Emp.]
            //, [% Military]
            //, [% Job Training]
            //, [% Miscellaneous] as [% Misc.] 
            List<string> cols = postGrad.GetVisibleColumns(SligoCS.BL.WI.ViewByGroup.AllStudentsFAY, SligoCS.BL.WI.OrgLevel.School, SligoCS.BL.WI.CompareTo.PRIORYEARS, SchoolType.AllTypes, SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.PostGradPlans.All);
            AssertColumns(cols, 8);
            AssertColumn(cols, "PriorYear");
            AssertColumn(cols, "Number of Graduates");
            AssertColumn(cols, "% 4-Year College");
            AssertColumn(cols, "% Voc/Tech College");
            AssertColumn(cols, "% Employment");
            AssertColumn(cols, "% Military");
            AssertColumn(cols, "% Job Training");
            AssertColumn(cols, "% Miscellaneous");


            //RULE: Show=4-year college, ViewBy=AllStudents, CompareTo=PriorYears:
            //select PriorYear as ' '
            //, [Number of Graduates]
            //, [Number 4-Year College] 
            //, [% 4-Year College] 

            cols = postGrad.GetVisibleColumns(SligoCS.BL.WI.ViewByGroup.AllStudentsFAY, SligoCS.BL.WI.OrgLevel.School, SligoCS.BL.WI.CompareTo.PRIORYEARS, SchoolType.AllTypes, SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.PostGradPlans.FourYear);
            AssertColumns(cols, 4);
            AssertColumn(cols, "PriorYear");
            AssertColumn(cols, "Number of Graduates");
            AssertColumn(cols, "Number 4-Year College");
            AssertColumn(cols, "% 4-Year College");
            
        }


        [Test]
        public void Bug898_PostGradPlans_GenderNotWorking()
        {
            string url = "http://localhost:31489/SligoWI/PostGradIntentPage.aspx?PLAN=All&FULLKEY=013619040022&GraphFile=AP&CompareTo=PRIORYEARS&ORGLEVEL=School&GROUP=Gender&DN=Milwaukee&SN=Madison%20Hi&DETAIL=YES&NumSchools=4&ZBackTo=performance.aspx&FileName=c:\\sample.txt&SchoolWebaddress=http://mpsportal.milwaukee.k12.wi.us&DistrictWebaddress=http://www.milwaukee.k12.wi.us&STYP=3&COUNTY=40&YearLocal=2007&TrendStartYearLocal=1997&WhichSchool=4&ConferenceKey=27&DistrictID=3619&GradeBreakout=999&GradeBreakoutLAG=555&GradeBreakoutEDISA=212&TraceLevels=None";
            Browser.GetPage(url);

            Assert.IsTrue(Browser.CurrentPageText.Contains("Female"), "Expected to see 'Female'");
            Assert.IsTrue(Browser.CurrentPageText.Contains("Male"), "Expected to see 'Male'");
        }


        [Test]
        public void Bug898B_RaceEthinicity()
        {
            //make sure race/ethnicity is showing correctly.
            string url = "http://localhost:31489/SligoWI/PostGradIntentPage.aspx?PLAN=All&FULLKEY=013619040022&GraphFile=AP&CompareTo=PRIORYEARS&ORGLEVEL=School&GROUP=RaceEthnicity&DN=Milwaukee&SN=Madison%20Hi&DETAIL=YES&NumSchools=4&ZBackTo=performance.aspx&FileName=c:\\sample.txt&SchoolWebaddress=http://mpsportal.milwaukee.k12.wi.us&DistrictWebaddress=http://www.milwaukee.k12.wi.us&STYP=3&COUNTY=40&YearLocal=2007&TrendStartYearLocal=1997&WhichSchool=4&ConferenceKey=27&DistrictID=3619&GradeBreakout=999&GradeBreakoutLAG=555&GradeBreakoutEDISA=212&TraceLevels=SQLStatement";
            Browser.GetPage(url);

            Assert.IsTrue(Browser.CurrentPageText.Contains("Asian"), "Expected 'Asian'");
            Assert.IsTrue(Browser.CurrentPageText.Contains("Black"), "Expected 'Black'");
            Assert.IsTrue(Browser.CurrentPageText.Contains("White"), "Expected 'White'");            
        }


        [Test]
        public void Bug898C_ViewByGroupLimitedSelection()
        {
            //Rule:  Post Graduation Plans page should only show view by: All Students • Gender • Race/Ethnicity 
            string url = "http://localhost:31489/SligoWI/PostGradIntentPage.aspx?PLAN=All&FULLKEY=013619040022&GraphFile=AP&CompareTo=PRIORYEARS&ORGLEVEL=School&GROUP=RaceEthnicity&DN=Milwaukee&SN=Madison%20Hi&DETAIL=YES&NumSchools=4&ZBackTo=performance.aspx&FileName=c:\\sample.txt&SchoolWebaddress=http://mpsportal.milwaukee.k12.wi.us&DistrictWebaddress=http://www.milwaukee.k12.wi.us&STYP=3&COUNTY=40&YearLocal=2007&TrendStartYearLocal=1997&WhichSchool=4&ConferenceKey=27&DistrictID=3619&GradeBreakout=999&GradeBreakoutLAG=555&GradeBreakoutEDISA=212&TraceLevels=SQLStatement";
            Browser.GetPage(url);

            Assert.IsTrue(Browser.CurrentPageText.Contains("All&nbsp;Students"), "All students");
            Assert.IsTrue(Browser.CurrentPageText.Contains("Gender"), "Gender");
            Assert.IsTrue(Browser.CurrentPageText.Contains("Race/Ethnicity"), "Race/Ethnicity");

            //should NOT contain Grade • Disability • Economically Disadvantaged • English Language Proficiency 
            Assert.IsTrue(!Browser.CurrentPageText.Contains("ParamValue=\"Grade\""), "Grade");
            Assert.IsTrue(!Browser.CurrentPageText.Contains("Disability"), "Disability");

        }


        [Test]
        public void Bug898D_CompareToDistState()
        {
            //Rule:  Post Graduation Plans page should only show view by: All Students • Gender • Race/Ethnicity 
            string url = "http://localhost:31489/SligoWI/PostGradIntentPage.aspx?PLAN=All&FULLKEY=013619040022&GraphFile=AP&CompareTo=DISTSTATE&ORGLEVEL=District&Group=RaceEthnicity&DN=Milwaukee&SN=Madison%20Hi&DETAIL=YES&NumSchools=4&ZBackTo=performance.aspx&FileName=c:\\sample.txt&SchoolWebaddress=http://mpsportal.milwaukee.k12.wi.us&DistrictWebaddress=http://www.milwaukee.k12.wi.us&STYP=3&COUNTY=40&YearLocal=2007&TrendStartYearLocal=1997&WhichSchool=4&ConferenceKey=27&DistrictID=3619&GradeBreakout=999&GradeBreakoutLAG=555&GradeBreakoutEDISA=212&TraceLevels=SQLStatement";
            Browser.GetPage(url);

            //should see Milwaukee and WI Public Schools.
            Assert.IsTrue(Browser.CurrentPageText.Contains(@"<td align=""center"">Milwaukee"), "Expected district level data.");
            Assert.IsTrue(Browser.CurrentPageText.Contains(@"WI Public Schools"), "Expected state level data.");

            //should not see school type anything on Post Grad Plans page.
            Assert.IsTrue(!Browser.CurrentPageText.Contains("SchoolType in"), "Should not see 'SchoolType' on Post Grad Plans page.");
            
        }


        [Test]
        public void Bug898E_OrgLevelDistrict()
        {
            string url = "http://localhost:31489/SligoWI/PostGradIntentPage.aspx?PLAN=All&FULLKEY=013619040022&GraphFile=AP&CompareTo=DISTSTATE&ORGLEVEL=District&Group=RaceEthnicity&DN=Milwaukee&SN=Madison%20Hi&DETAIL=YES&NumSchools=4&ZBackTo=performance.aspx&FileName=c:\\sample.txt&SchoolWebaddress=http://mpsportal.milwaukee.k12.wi.us&DistrictWebaddress=http://www.milwaukee.k12.wi.us&STYP=3&COUNTY=40&YearLocal=2007&TrendStartYearLocal=1997&WhichSchool=4&ConferenceKey=27&DistrictID=3619&GradeBreakout=999&GradeBreakoutLAG=555&GradeBreakoutEDISA=212&TraceLevels=SQLStatement";
            Browser.GetPage(url);

            //If org level = district, should not see fullkey 013619040022 in sql statement.
            Assert.IsTrue(!Browser.CurrentPageText.Contains("'013619040022'"), "should not see fullkey for school level when orglevel = district.");



            //select rtrim(racedesc) as '  ' 
            //, v_POST_GRAD_INTENT.diststate as ' ' 
            //, [Number of Graduates]
            //, [% 4-Year College]
            //, [% Voc/Tech College]
            //, [% Employment] as [% Emp.]
            //, [% Military]
            //, [% Job Training]
            //, [% Miscellaneous] as [% Misc.] 
            PostGradIntentPage pgp = new PostGradIntentPage();
            List<string> cols = pgp.GetVisibleColumns(SligoCS.BL.WI.ViewByGroup.RaceEthnicity, SligoCS.BL.WI.OrgLevel.District, SligoCS.BL.WI.CompareTo.DISTSTATE, SchoolType.AllTypes, SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.PostGradPlans.All);
            AssertColumns(cols, 9);
            AssertColumn(cols, "racedesc");
            AssertColumn(cols, "diststate");
            AssertColumn(cols, "Number of Graduates");
            AssertColumn(cols, "% 4-Year College");
            AssertColumn(cols, "% Voc/Tech College");
            AssertColumn(cols, "% Employment");
            AssertColumn(cols, "% Military");
            AssertColumn(cols, "% Job Training");
            AssertColumn(cols, "% Miscellaneous");

        }
    }
}
