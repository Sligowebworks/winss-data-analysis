using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.Web.WI;
using SligoCS.DAL.WI;
using NUnit.Framework;
using NUnit.Extensions.Asp;

namespace SligoCS.Tests.WI.NUnitASPTesters
{
    [TestFixture]
    public class TeacherQualificationsPageTester : BaseTester
    {

        [Test]
        [Ignore]
        public void Bug841_TeacherQual_OrgLevel()
        {
            //Check left navigation links for school, district, state.
            string schoolLevelURL = "http://localhost:31489/SligoWI/TeacherQualifications.aspx?FULLKEY=013619040022&ORGLEVEL=School";
            string distLevelURL = "http://localhost:31489/SligoWI/TeacherQualifications.aspx?FullKey=01361903XXXX&ORGLEVEL=District";



            //RULE:  Teacher Qual Page: If district or state, also show School Type row in ParamsLinkBox.


            #region Teacher Qualifcations page at School Level
            Browser.GetPage(schoolLevelURL);            

            //check school level link contains no hyperlink
            Assert.IsTrue(Browser.CurrentPageText.Contains(@"<a id=""ctl00_linkSchool"" style=""color:White;"">Madison Hi</a>"), "Incorrect school level link.");

            //check district level link contains a hyperlink
            Assert.IsTrue(Browser.CurrentPageText.Contains(@"<a id=""ctl00_linkDistrict"" href="), "Expected hyperlink for district.");

            //check district level link contains correct district name.
            Assert.IsTrue(Browser.CurrentPageText.Contains(@">Milwaukee</a>"), "Incorrect district name.");

            //check school level does NOT contain 'All Types'.
            Assert.IsTrue(!Browser.CurrentPageText.Contains("All&nbsp;Types"), "Teacher Qual page should not contain 'All Types' for orglevel == school.");
            #endregion



            #region Teacher Qual page at District level                        
            Browser.GetPage(distLevelURL);
            Assert.IsTrue(Browser.CurrentPageText.Contains("All&nbsp;Types"), "Teacher Qual page must contain 'All Types' if orglevel = district.");
            #endregion


        }

        
        [Test]
        public void Bug841_TeacherQual_CheckColumns()
        {
            //RULE: if orglevel = district, school type = 'Elem', columns should be:
            // OrgLevelLabel
            //, SchooltypeLabel as 'School Type'
            //, [FTETotal] as '# of FTE teachers'
            //, [FTELicenseFull] as '# FTE'
            //, [LicenseFullFTEPercentage] as '% of Total'
            //, [LicenseEmerFTE] as '# FTE'
            //, [LicenseEmerFTEPercentage] as '% of Total'
            //, [LicenseNoFTE] as '# FTE'
            //, [LicenseNoFTEPercentage] as '% of Total'

            //string url = "http://localhost:31489/SligoWI/TeacherQualifications.aspx?TQShow=LICSTAT&TQSubjects=SUMALL&FULLKEY=01361903XXXX&GraphFile=HIGHSCHOOLCOMPLETION&CompareTo=PRIORYEARS&ORGLEVEL=SC&Group=AllStudentsFAY&DN=Milwaukee&SN=Madison%20Hi&DETAIL=YES&NumSchools=4&ZBackTo=performance.aspx&FileName=c:\\sample.txt&SchoolWebaddress=http://mpsportal.milwaukee.k12.wi.us&DistrictWebaddress=http://www.milwaukee.k12.wi.us&STYP=6&COUNTY=40&YearLocal=2007&TrendStartYearLocal=1997&WhichSchool=4&ConferenceKey=27&DistrictID=3619&GradeBreakout=999&GradeBreakoutLAG=555&GradeBreakoutEDISA=212&TraceLevels=None";

            TeacherQualifications tqPage = new TeacherQualifications();
            List<string> cols = tqPage.GetVisibleColumns(SligoCS.BL.WI.ViewByGroup.AllStudentsFAY, SligoCS.BL.WI.OrgLevel.District, SligoCS.BL.WI.CompareTo.CURRENTONLY, SligoCS.DAL.WI.SchoolType.Elem, SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.TQShowEnum.Wisconsin_License_Status);
            AssertColumns(cols, 9);
            AssertColumn(cols, "OrgLevelLabel");
            AssertColumn(cols, "SchooltypeLabel");
            AssertColumn(cols, "FTETotal");
            AssertColumn(cols, "FTELicenseFull");
            AssertColumn(cols, "LicenseFullFTEPercentage");
            AssertColumn(cols, "LicenseEmerFTE");
            AssertColumn(cols, "LicenseEmerFTEPercentage");
            AssertColumn(cols, "LicenseNoFTE");
            AssertColumn(cols, "LicenseNoFTEPercentage");


            //RULE:  if show= District Experience, columns should be:
            //select YearFormatted as ' '
            //, [FTETotal] as '# of FTE teachers'
            //, [LocalExperience5YearsOrLessFTE] as '# FTE with less than 5 years experience in this district'
            //, [LocalExperience5YearsOrMoreFTE] as '# FTE with at least 5 years experience in this district'
            //, [LocalExperience5YearsOrMoreFTEPercentage] as '% with at least 5 years experience in this district'            
            cols = tqPage.GetVisibleColumns(SligoCS.BL.WI.ViewByGroup.AllStudentsFAY, SligoCS.BL.WI.OrgLevel.School, SligoCS.BL.WI.CompareTo.PRIORYEARS, SligoCS.DAL.WI.SchoolType.AllTypes, SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.TQShowEnum.District_Experience);
            AssertColumns(cols, 5);
            AssertColumn(cols, "YearFormatted");
            AssertColumn(cols, "FTETotal");
            AssertColumn(cols, "LocalExperience5YearsOrLessFTE");
            AssertColumn(cols, "LocalExperience5YearsOrMoreFTE");
            AssertColumn(cols, "LocalExperience5YearsOrMoreFTEPercentage");

        }


        [Test]
        [Ignore]
        public void Bug888_SchoolLevelLinkDisappears()
        {
            //school-level link disappears whenever District level link is clicked.
            
            //Start with school level link.
            Browser.GetPage("http://localhost:31489/SligoWI/TeacherQualifications.aspx?FULLKEY=013619040022&ORGLEVEL=School");
            Assert.IsTrue(Browser.CurrentPageText.Contains(@"<a id=""ctl00_linkSchool"" style=""color:White;"">Madison Hi</a>"), "Expected to see school-level link on left hand side.");
            Assert.IsTrue(Browser.CurrentPageText.Contains(@">Milwaukee</a>"), "Expected to see district level link.");
            Assert.IsTrue(Browser.CurrentPageText.Contains(@"><b>State</b></a>"), "Expected to see state level link.");

            //Move to District level link.
            Browser.GetPage("http://localhost:31489/SligoWI/TeacherQualifications.aspx?FULLKEY=013619040022&ORGLEVEL=District");
            Assert.IsTrue(Browser.CurrentPageText.Contains(@">Madison Hi</a>"), "Expected to see school level link");
            Assert.IsTrue(Browser.CurrentPageText.Contains(@"<a id=""ctl00_linkDistrict"" style=""color:White;"">Milwaukee</a>"), "Expected to see district level link");
            Assert.IsTrue(Browser.CurrentPageText.Contains(@"><b>State</b></a>"), "Expected to see state level link");

            //Move to State level link.
            Browser.GetPage("http://localhost:31489/SligoWI/TeacherQualifications.aspx?FULLKEY=013619040022&ORGLEVEL=State");
            Assert.IsTrue(Browser.CurrentPageText.Contains(@">Madison Hi</a>"), "Expected to see school level link");
            Assert.IsTrue(Browser.CurrentPageText.Contains(@">Milwaukee</a>"), "Expected to see district level link");
            Assert.IsTrue(Browser.CurrentPageText.Contains(@"<a id=""ctl00_linkState"" style=""color:White;""><b>State</b></a>"), "Expected to see state level link");

        }


        [Test]
        [Ignore]
        public void Bug894_TQPage_WrongGraphTitleAndColumns()
        {
            //Subject == SUMALL
            Browser.GetPage("http://localhost:31489/SligoWI/TeacherQualifications.aspx?TQShow=LICSTAT&TQSubjects=SUMALL&FULLKEY=013619040022&GraphFile=AP&CompareTo=CURRENTONLY&ORGLEVEL=School&Group=AllStudentsFAY&DN=Milwaukee&SN=Madison%20Hi&DETAIL=YES&NumSchools=4&ZBackTo=performance.aspx&FileName=c:\\sample.txt&SchoolWebaddress=http://mpsportal.milwaukee.k12.wi.us&DistrictWebaddress=http://www.milwaukee.k12.wi.us&STYP=3&COUNTY=40&YearLocal=2007&TrendStartYearLocal=1997&WhichSchool=4&ConferenceKey=27&DistrictID=3619&GradeBreakout=999&GradeBreakoutLAG=555&GradeBreakoutEDISA=212&TraceLevels=SQLStatement");
            //check title of graph is correct.
            Assert.IsTrue(Browser.CurrentPageText.Contains("Wisconsin Teacher License Status <br/>Summary All Subjects<br/>Madison Hi<br/>2007-07"), "Graph header is incorrect.");

            //check SQL statement is correct.
            Assert.IsTrue(Browser.CurrentPageText.Contains("(LinkSubjectCode = 'SUMALL')"), "SQL statement is incorrent.");



            //Subject = English Language Arts
            Browser.GetPage("http://localhost:31489/SligoWI/TeacherQualifications.aspx?TQShow=LICSTAT&TQSubjects=ELA&FULLKEY=013619040022&GraphFile=AP&CompareTo=CURRENTONLY&ORGLEVEL=School&Group=AllStudentsFAY&DN=Milwaukee&SN=Madison%20Hi&DETAIL=YES&NumSchools=4&ZBackTo=performance.aspx&FileName=c:\\sample.txt&SchoolWebaddress=http://mpsportal.milwaukee.k12.wi.us&DistrictWebaddress=http://www.milwaukee.k12.wi.us&STYP=3&COUNTY=40&YearLocal=2007&TrendStartYearLocal=1997&WhichSchool=4&ConferenceKey=27&DistrictID=3619&GradeBreakout=999&GradeBreakoutLAG=555&GradeBreakoutEDISA=212&TraceLevels=SQLStatement");
            Assert.IsTrue(Browser.CurrentPageText.Contains("(LinkSubjectCode = 'ELA')"), "wrong SQL");




        }





        [Test]
        public void Bug895_TeacherQual_CheckColumns()
        {
            //RULE: if orglevel = school, show=Wis state licens, subject = ELA, compareTo= prioryears:

            //select YearFormatted as ' '
            //, [FTETotal] as '# of FTE teachers'
            //, [FTELicenseFull] as '# FTE'
            //, [LicenseFullFTEPercentage] as '% of Total'
            //, [LicenseEmerFTE] as '# FTE'
            //, [LicenseEmerFTEPercentage] as '% of Total'
            //, [LicenseNoFTE] as '# FTE'
            //, [LicenseNoFTEPercentage] as '% of Total'
            //, YearFormatted 

            TeacherQualifications tqPage = new TeacherQualifications();
            List<string> cols = tqPage.GetVisibleColumns(SligoCS.BL.WI.ViewByGroup.AllStudentsFAY, SligoCS.BL.WI.OrgLevel.School, SligoCS.BL.WI.CompareTo.PRIORYEARS, SligoCS.DAL.WI.SchoolType.AllTypes, SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.TQShowEnum.Wisconsin_License_Status);
            AssertColumns(cols, 8);

            AssertColumn(cols, "YearFormatted");
            AssertColumn(cols, "FTETotal");
            AssertColumn(cols, "FTELicenseFull");
            AssertColumn(cols, "LicenseFullFTEPercentage");
            AssertColumn(cols, "LicenseEmerFTE");
            AssertColumn(cols, "LicenseEmerFTEPercentage");
            AssertColumn(cols, "LicenseNoFTE");
            AssertColumn(cols, "LicenseNoFTEPercentage");


            string url = "http://localhost:31489/SligoWI/TeacherQualifications.aspx?TQShow=LICSTAT&TQSubjects=ELA&FULLKEY=013619040022&GraphFile=HIGHSCHOOLCOMPLETION&CompareTo=PRIORYEARS&ORGLEVEL=School&Group=AllStudentsFAY&DN=Milwaukee&SN=Madison%20Hi&DETAIL=YES&NumSchools=4&ZBackTo=performance.aspx&FileName=c:\\sample.txt&SchoolWebaddress=http://mpsportal.milwaukee.k12.wi.us&DistrictWebaddress=http://www.milwaukee.k12.wi.us&STYP=3&COUNTY=40&YearLocal=2007&TrendStartYearLocal=1997&WhichSchool=4&ConferenceKey=27&DistrictID=3619&GradeBreakout=999&GradeBreakoutLAG=555&GradeBreakoutEDISA=212&TraceLevels=None";
            Browser.GetPage(url);
            Assert.IsTrue(!Browser.CurrentPageText.Contains(">School Type<"), "Should not contain School Type column with these params.");

            

            

            
        }





        [Test]
        public void Bug895B_TeacherQual_CheckColumns()
        {
            //RULE: if orglevel = school, show=Wis state licens, subject = ELA, compareTo= current school:
            //should see 8 cols, starting with OrgLevelLabel

            TeacherQualifications tqPage = new TeacherQualifications();
            List<string> cols = tqPage.GetVisibleColumns(SligoCS.BL.WI.ViewByGroup.AllStudentsFAY, SligoCS.BL.WI.OrgLevel.School, SligoCS.BL.WI.CompareTo.CURRENTONLY, SligoCS.DAL.WI.SchoolType.AllTypes, SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.TQShowEnum.Wisconsin_License_Status);
            AssertColumns(cols, 8);
            AssertColumn(cols, "OrgLevelLabel");
        }

        [Test]
        [Ignore]
        public void Bug910_TQPage_DistrictLevel()
        {
            //RULE, when orglevel=District, full key should be masked (e.g. '01361903XXXX')
            string url = "http://localhost:31489/SligoWI/TeacherQualifications.aspx?TQShow=LICSTAT&TQSubjects=SUMALL&FULLKEY=013619040022&GraphFile=HIGHSCHOOLCOMPLETION&CompareTo=PRIORYEARS&ORGLEVEL=District&Group=AllStudentsFAY&DN=Milwaukee&SN=Madison%20Hi&DETAIL=YES&NumSchools=4&ZBackTo=performance.aspx&FileName=c:\\sample.txt&SchoolWebaddress=http://mpsportal.milwaukee.k12.wi.us&DistrictWebaddress=http://www.milwaukee.k12.wi.us&STYP=6&COUNTY=40&YearLocal=2007&TrendStartYearLocal=1997&WhichSchool=4&ConferenceKey=27&DistrictID=3619&GradeBreakout=999&GradeBreakoutLAG=555&GradeBreakoutEDISA=212&TraceLevels=4";
            Browser.GetPage(url);

            AssertString("01361903XXXX", true, false);

            
            //also check the Graph title.
            AssertString("Wisconsin Teacher License Status", true, true);
            AssertString("Summary All Subjects", true, true);
            AssertString("Milwaukee: Elementary Schools", true, true);
            AssertString("2007-07 Compared To Prior Years", true, false);



            //select YearFormatted as ' '
            //, SchooltypeLabel as 'School Type'
            //, [FTETotal] as '# of FTE teachers'
            //, [FTELicenseFull] as '# FTE'
            //, [LicenseFullFTEPercentage] as '% of Total'
            //, [LicenseEmerFTE] as '# FTE'
            //, [LicenseEmerFTEPercentage] as '% of Total'
            //, [LicenseNoFTE] as '# FTE'
            //, [LicenseNoFTEPercentage] as '% of Total'


            TeacherQualifications tq = new TeacherQualifications();
            List<string> cols = tq.GetVisibleColumns(SligoCS.BL.WI.ViewByGroup.AllStudentsFAY, SligoCS.BL.WI.OrgLevel.District, SligoCS.BL.WI.CompareTo.PRIORYEARS, SligoCS.DAL.WI.SchoolType.Elem, SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.TQShowEnum.Wisconsin_License_Status);
            AssertColumns(cols, 9);
            AssertColumn(cols, "YearFormatted");
            AssertColumn(cols, "SchooltypeLabel");
            AssertColumn(cols, "FTETotal");
            AssertColumn(cols, "FTELicenseFull");
            AssertColumn(cols, "LicenseFullFTEPercentage");
            AssertColumn(cols, "LicenseEmerFTE");
            AssertColumn(cols, "LicenseEmerFTEPercentage");
            AssertColumn(cols, "LicenseNoFTE");
            AssertColumn(cols, "LicenseNoFTEPercentage");

        }

    }
}
