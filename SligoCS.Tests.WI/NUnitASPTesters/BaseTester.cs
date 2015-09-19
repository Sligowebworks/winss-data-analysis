using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using NUnit.Extensions.Asp;

namespace SligoCS.Tests.WI.NUnitASPTesters
{
    /// <summary>
    /// Provides a base class for Sligo web page testers.
    /// </summary>
    public class BaseTester : WebFormTestCase
    {
        /// <summary>
        /// Same idea as List<string>.Contains(), but not case sensitive.
        /// </summary>
        /// <param name="cols"></param>
        /// <param name="expectedColumnName"></param>
        protected void AssertColumn(List<string> cols, string expectedColumnName)
        {            
            bool found = false;
            for (int i = 0; i < cols.Count; i++)
            {               
                string colName = cols[i].ToLower();
                if (colName == expectedColumnName.ToLower())
                {
                    found = true;
                    break;
                }
                   
            }

            Assert.IsTrue(found, "Expected column name " + expectedColumnName);
        }

        protected void AssertColumns(List<string> cols, int expectedNumCols)
        {
            string msg = string.Format("Expected {0} columns, found {1}", expectedNumCols, cols.Count);
            Assert.IsTrue(cols.Count == expectedNumCols, msg);
        }

        protected void AssertString(string stringToFind, bool shouldBeVisible, bool caseSensitive)
        {
            string pageText = Browser.CurrentPageText;
            if (!caseSensitive)
            {
                pageText = pageText.ToLower();
                stringToFind = stringToFind.ToLower();
            }
            if (shouldBeVisible)                
                Assert.IsTrue(pageText.Contains(stringToFind), "Expected to see '" + stringToFind + "'");
            else
                Assert.IsTrue(!pageText.Contains(stringToFind), "Should not see '" + stringToFind + "'");
        }


        protected void AssertParamsLink(string label)
        {
            string searchFor = "<td>" + label + "</td>";
            Assert.IsTrue(Browser.CurrentPageText.Contains(searchFor), "Expected to see ParamsLink for " + label);
        }
    }
}
