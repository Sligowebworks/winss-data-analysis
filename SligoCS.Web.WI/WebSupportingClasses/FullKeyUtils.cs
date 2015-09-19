using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;

using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.BL.WI
{
    public class FullKeyUtils
    {
        public static OrgLevel GetOrgLevelFromFullKeyX(string origFullKey)
        {
            OrgLevel orgLevel = new OrgLevel();
            if (!(origFullKey.Length == 12  || String.IsNullOrEmpty(origFullKey)))
                throw new Exception("FullKey must be 12 characters long.");
            else if (origFullKey.StartsWith("X") || String.IsNullOrEmpty(origFullKey))
                orgLevel.Value = orgLevel.Range[OrgLevelKeys.State];
            else if (origFullKey.Contains("X"))
                orgLevel.Value = orgLevel.Range[OrgLevelKeys.District];
            else
                orgLevel.Value = orgLevel.Range[OrgLevelKeys.School];

            return orgLevel;
        }

        public static String StateFullKey(String FullKey)
        {
            return GetMaskedFullkey(FullKey, OrgLevelKeys.State);
        }
        public static String DistrictFullKey(String FullKey)
        {
            return GetMaskedFullkey(FullKey, OrgLevelKeys.District);
        }
        public static String SchoolFullKey(String FullKey)
        {
            return GetMaskedFullkey(FullKey, OrgLevelKeys.School);
        }
        /// <summary>
        /// DEPRECATED; waiting to upgrade dependendant classes to remove
        /// </summary>
        /// <param name="compareTo"></param>
        /// <param name="orgLevel"></param>
        /// <param name="origFullKey"></param>
        /// <param name="compareToSchoolsOrDistrict"></param>
        /// <param name="s4orAll"></param>
        /// <returns></returns>
        public static List<string> GetFullKeysList(CompareTo compareTo, 
            OrgLevel orgLevel, string origFullKey, List<string> compareToSchoolsOrDistrict, S4orALL s4orAll)
        {
            return null;
        }
        public static String GetMaskedFullkey(String fullkey, OrgLevel orgLevel)
        {
            return GetMaskedFullkey(fullkey, orgLevel.Key);
        }
        public static String GetMaskedFullkey(string origFullKey, String desiredOrgLevelKey)
        {
            string retval = origFullKey;
            OrgLevel origOrgLevel = FullKeyUtils.GetOrgLevelFromFullKeyX(origFullKey);

            if (desiredOrgLevelKey == OrgLevelKeys.School)
            {
                if ((origOrgLevel.Key == OrgLevelKeys.District) || (origOrgLevel.Key == OrgLevelKeys.State))
                {
                    //cannot create a school's fullkey from a District or State fullky.
                    retval = string.Empty;
                }

                //otherwise, continue using default value
            }
            else if (desiredOrgLevelKey == OrgLevelKeys.District)
            {
                if (origOrgLevel.Key == OrgLevelKeys.State)
                {
                    //cannot create s District fullkey given a State fullkey.
                    retval = string.Empty;
                }
                else
                {
                    //otherwise, replace the last 4 digits of the fullkey
                    //retval = origFullKey.Substring(0, 7) + "3XXXX"; //BR: shoulb be "3ZZZZ" for out going URL
                    //to fix Charter school issues
                    retval = origFullKey.Substring(0, 6) + "03XXXX"; //BR: shoulb be "03ZZZZ" for out going URL
                }
            }
            else
            {
                //orglevel == State
                retval = "XXXXXXXXXXXX"; //BR: shoulb be "ZZZZZZZZZZZZ" for out going URL 6/08
            }

            return retval;
        }
        public static List<string> ParseFullKeyString(string input)
        {
            List<string> result = new List<string>();

            if (input != null && input.Length > 11)
            {
                result.Add(input.Substring(0, 12));
                if (input.Length > 23)
                {
                    result.Add(input.Substring(12, 12));
                    if (input.Length > 35)
                    {
                        result.Add(input.Substring(24, 12));
                        if (input.Length > 47)
                        {
                            result.Add(input.Substring(36, 12));
                        }
                    }

                }
            }
            return result;
        }
    }
}
