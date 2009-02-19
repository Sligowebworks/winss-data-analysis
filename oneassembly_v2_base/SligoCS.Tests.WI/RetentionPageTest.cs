using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using SligoCS.Web.WI;
using SligoCS.BL.WI;
using SligoCS.DAL.WI;

namespace SligoCS.Tests.WI
{
    [TestFixture]
    public class RetentionPageTest : BasePageTest
    {
        
        [Test]
        public override void Bug617_GetVisibleColumns()
        {
            //Bug 617:
            //  when Retention: District: All Types: All Students: Compare To: Prior Years
            GridPage retentionPage = new GridPage();
            //List<string> cols = retentionPage.GetVisibleColumns(ViewByGroup.AllStudentsFAY, OrgLevel.District, CompareTo.PRIORYEARS, SchoolType.AllTypes);
            //Assert.IsTrue(cols.Count == 6, "should have 6 columns, found " + cols.Count);
            //Assert.IsTrue(cols.Contains("YearFormatted"), "missing column on Retention page.");
            //Assert.IsTrue(cols.Contains("SchooltypeLabel"), "missing column on Retention page.");
            //Assert.IsTrue(cols.Contains("Total Enrollment (K-12)"), "missing column on Retention page.");
            //Assert.IsTrue(cols.Contains("Completed_School_Term"), "missing column on Retention page.");
            //Assert.IsTrue(cols.Contains("Number of Retentions"), "missing column on Retention page.");
            //Assert.IsTrue(cols.Contains("Retention Rate"), "missing column on Retention page.");

            
        }        


        [Test]
        public override void  Bug808_GraphTitles()
        {
            //"EconDisadv" should read "Economica Status"
            GridPage retentionPage = new GridPage();
            //string title = retentionPage.GetTitle (
            //    "Rate of Retention", ViewByGroup.EconDisadv, "Milwaukee", OrgLevel.District, new List<int>(), 
            //    CompareTo.PRIORYEARS, SchoolType.AllTypes);
            //Assert.IsTrue(!title.Contains("EconDisadv"), "Title error:  'EconDisadv' should read 'Economically Disadvantaged'");
            //Assert.IsTrue(title.Contains("Economically Disadvantaged"), "Title error:  'EconDisadv' should read 'Economically Disadvantaged'");


            ////'ELP' should read = 'English Language Proficiency'
            //title = retentionPage.GetTitle("Rate of Retention", ViewByGroup.ELP, "Milwaukee", OrgLevel.District, new List<int>(), CompareTo.PRIORYEARS, SchoolType.AllTypes);
            //Assert.IsTrue(!title.Contains("ELP"), "Title error:  'ELP' should read = 'English Language Proficiency'");
            //Assert.IsTrue(title.Contains("English Language Proficiency"), "Title error:  'ELP' should read = 'English Language Proficiency'");
        }


    }
}
