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
    public class DropoutsPageTest : BasePageTest
    {
        [Test]
        public override void Bug617_GetVisibleColumns()
        {
            //Bug 617:
            //  when Dropouts: District: All Types: All Students: Compare To: Prior Years

             //Total Enrollment Grades 7-12** Students expected to complete the school term Students who completed the school term Drop Outs Drop Out Rate 
            Demo dropoutsPage = new Demo();
            //List<string> cols = dropoutsPage.GetVisibleColumns(ViewByGroup.AllStudentsFAY, OrgLevel.District, CompareToEnum.PRIORYEARS, SchoolType.AllTypes);
            //Assert.IsTrue(cols.Count == 7, "should have 7 columns, found " + cols.Count);
            //Assert.IsTrue(cols.Contains("YearFormatted"), "missing column on Retention page.");
            //Assert.IsTrue(cols.Contains("SchooltypeLabel"), "missing column on Retention page.");
            //Assert.IsTrue(cols.Contains("Enrollment"), "missing column on Retention page.");
            //Assert.IsTrue(cols.Contains("Students expected to complete the school term"), "missing column on Retention page.");
            //Assert.IsTrue(cols.Contains("Students who completed the school term"), "missing column on Retention page.");
            //Assert.IsTrue(cols.Contains("Drop Outs"), "missing column on Retention page.");
            //Assert.IsTrue(cols.Contains("Drop Out Rate"), "missing column on Retention page.");

        }
    }
}
