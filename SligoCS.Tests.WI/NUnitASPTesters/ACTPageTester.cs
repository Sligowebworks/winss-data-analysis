using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.Web.WI;
using SligoCS.Web.WI.WebSupportingClasses.WI;
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
            //RULE:  ACT Page: Subject=Reading / ViewBy=All Students / CompareToEnum=Prior Years
           //select vACT.PriorYears 
           // , vact.enrollment as 'Enrollment Grade 12'
           // , vact.Pupilcount as 'Number Tested'
           // , [Perc Tested] as '% Tested'
           // , vACT.Reading as 'Average Score - Reading ' 

            StickyParameter sp = new StickyParameter();
            Group group = new Group();
            group.Value = group.Range[GroupKeys.All];

            ACTPage act = new ACTPage();
            List<string> cols = act.GetVisibleColumns(group, SligoCS.BL.WI.OrgLevel.District, SligoCS.BL.WI.CompareToEnum.PRIORYEARS, SchoolType.AllTypes);
            AssertColumns(cols, 5);
            AssertColumn(cols, "PriorYears");
            AssertColumn(cols, "enrollment");
            AssertColumn(cols, "Pupilcount");
            AssertColumn(cols, "Perc Tested");
            AssertColumn(cols, "Reading");


            //RULE:  ACT Page: Subject=English / ViewBy=All Students / CompareToEnum=Prior Years
            //select vACT.PriorYears 
            // , vact.enrollment as 'Enrollment Grade 12'
            // , vact.Pupilcount as 'Number Tested'
            // , [Perc Tested] as '% Tested'
            // , vACT.English as 'Average Score - English ' 
            sp.ACTSubj.Value = sp.ACTSubj.Range[ACTSubjKeys.English];

            cols = act.GetVisibleColumns(group, SligoCS.BL.WI.OrgLevel.District, SligoCS.BL.WI.CompareToEnum.PRIORYEARS, SchoolType.AllTypes, sp.ACTSubj);
            AssertColumns(cols, 5);
            AssertColumn(cols, "PriorYears");
            AssertColumn(cols, "enrollment");
            AssertColumn(cols, "Pupilcount");
            AssertColumn(cols, "Perc Tested");
            AssertColumn(cols, "English");




            //RULE:  ACT Page: Subject=Reading / ViewBy=All Students / CompareToEnum=Prior Years
            //select vACT.PriorYears 
            // , RaceShortLabel
            // , vact.enrollment as 'Enrollment Grade 12'
            // , vact.Pupilcount as 'Number Tested'
            // , [Perc Tested] as '% Tested'
            // , vACT.Reading as 'Average Score - Reading ' 

            group.Value = group.Range[GroupKeys.Race];

            cols = act.GetVisibleColumns(group, SligoCS.BL.WI.OrgLevel.District, SligoCS.BL.WI.CompareToEnum.PRIORYEARS, SchoolType.AllTypes);
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
