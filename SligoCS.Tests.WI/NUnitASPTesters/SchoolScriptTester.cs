using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.Web.WI.WebSupportingClasses.WI;

using NUnit.Framework;
using NUnit.Extensions.Asp;

namespace SligoCS.Tests.WI.NUnitASPTesters
{
    [TestFixture]
    public class SchoolScriptTester : BaseTester 
    {

        [Test]
        public void Bug874_SchoolScriptShouldPassParams()
        {
            ////RULE:  School Script page should pass 4 stickyParameters:
            ////  FULLKEY, LOWGRADE, HIGHGRADE, and ORGLEVEL

            //string url = "http://localhost:31489/SligoWI/SchoolScript.aspx?SEARCHTYPE=SC&L=A";
            //Browser.GetPage(url);

            ////Check the hyperlink for Abbotford Elementary.
            //string searchFor = @"ctl00_ContentPlaceHolder1_SligoDataGrid1_ctl03_DistrictDetails1"" href=""questions.aspx?FULLKEY=100007040020&amp;DN=Abbotsford&amp;TYPECODE=6&amp;LOWGRADE=9&amp;HIGHGRADE=36&amp;ORGLEVEL=DI";
            //Assert.IsTrue(Browser.CurrentPageText.Contains(searchFor), "Expected to see link for Abbotsford El, with FULLKEY, LOWGRADE, HIGHGRADE, and ORGLEVEL");

            ////check LOWGRADE and HIGHGRADE exist in GlobalValues and have Default values.
            //GlobalValues helper = new GlobalValues();
            //helper.LoadFromContext(globalValues);
            //Assert.IsTrue (helper.LOWGRADE != null, "Expected a default value for LOWGRADE");
            //Assert.IsTrue (helper.HIGHGRADE != null, "Expected a default value for HIGHGRADE");


            

        }
    }
}
