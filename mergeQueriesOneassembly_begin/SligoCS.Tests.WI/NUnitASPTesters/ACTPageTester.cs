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
    public class ACTPageTester : BaseTester 
    {

        [Test]
        [Ignore]
        public void Bug843_ACTPage_Columns()
        {
            //RULE:  ACT Page: Subject=Reading / ViewBy=All Students / CompareTo=Prior Years
           //select vACT.PriorYears 
           // , vact.enrollment as 'Enrollment Grade 12'
           // , vact.Pupilcount as 'Number Tested'
           // , [Perc Tested] as '% Tested'
           // , vACT.Reading as 'Average Score - Reading ' 

            ACTPage act = new ACTPage();
            List<string> cols = act.GetVisibleColumns(SligoCS.BL.WI.ViewByGroup.AllStudentsFAY, SligoCS.BL.WI.OrgLevel.District, SligoCS.BL.WI.CompareTo.PRIORYEARS, SchoolType.AllTypes);
            AssertColumns(cols, 5);
            AssertColumn(cols, "PriorYears");
            AssertColumn(cols, "enrollment");
            AssertColumn(cols, "Pupilcount");
            AssertColumn(cols, "Perc Tested");
            AssertColumn(cols, "Reading");



            //RULE:  ACT Page: Subject=English / ViewBy=All Students / CompareTo=Prior Years
            //select vACT.PriorYears 
            // , vact.enrollment as 'Enrollment Grade 12'
            // , vact.Pupilcount as 'Number Tested'
            // , [Perc Tested] as '% Tested'
            // , vACT.English as 'Average Score - English ' 
            
            cols = act.GetVisibleColumns(SligoCS.BL.WI.ViewByGroup.AllStudentsFAY, SligoCS.BL.WI.OrgLevel.District, SligoCS.BL.WI.CompareTo.PRIORYEARS, SchoolType.AllTypes, SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.ACTSubjects.English);
            AssertColumns(cols, 5);
            AssertColumn(cols, "PriorYears");
            AssertColumn(cols, "enrollment");
            AssertColumn(cols, "Pupilcount");
            AssertColumn(cols, "Perc Tested");
            AssertColumn(cols, "English");




            //RULE:  ACT Page: Subject=Reading / ViewBy=All Students / CompareTo=Prior Years
            //select vACT.PriorYears 
            // , RaceShortLabel
            // , vact.enrollment as 'Enrollment Grade 12'
            // , vact.Pupilcount as 'Number Tested'
            // , [Perc Tested] as '% Tested'
            // , vACT.Reading as 'Average Score - Reading ' 

            cols = act.GetVisibleColumns(SligoCS.BL.WI.ViewByGroup.RaceEthnicity, SligoCS.BL.WI.OrgLevel.District, SligoCS.BL.WI.CompareTo.PRIORYEARS, SchoolType.AllTypes);
            AssertColumns(cols, 6);
            AssertColumn(cols, "PriorYears");
            AssertColumn(cols, "RaceShortLabel");
            AssertColumn(cols, "enrollment");
            AssertColumn(cols, "Pupilcount");
            AssertColumn(cols, "Perc Tested");
            AssertColumn(cols, "Reading");

        }
    }
}
