using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SligoCS.BL.WI;
using SligoCS.DAL.WI;
using SligoCS.Web.WI.DAL.DataSets;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
    /// <summary>
    /// Scope for globally utilized variables. Serves as additional encapsulation for User Parameters. That is, any programatic overrides should be on fields in this class rather than the raw StickyParamers Object. All tests of user input should be through this object.
    /// </summary>
    public class GlobalValues : QueryStringUtils
    {
        public GlobalValues() : base()
        {
            this.STYP = InitSchoolType(this.FULLKEY);
        }

        public List<String> CompareSelectedFullKeys
        {
            get { return FullKeyUtils.ParseFullKeyString(SSchoolFullKeys); }
        }

        /// <summary>
        /// If STYP is set in the querystring, use that. 
        /// Otherwise, if the Fullkey is provided and matches a single school, do a
        ///     db lookup to determine the STYP,
        /// Otherwise, use the GlobalValues default STYP (3).
        /// </summary>
        /// <param name="fullKey"></param>
        /// <returns></returns>
        private STYP InitSchoolType(string fullKey)
        {
            string strParam = String.Empty;
            object objParam;

            objParam = GetParamFromUser(StickyParameter.QStringVar.STYP.ToString());

            strParam = (objParam == null)? String.Empty : objParam.ToString();
            
            //MZD: not sure what req this code is for. Doesn't seem reachable...
            if ( strParam == String.Empty)
            {
                BLSchool school = new BLSchool();
                
                v_Schools ds = school.GetSchool(fullKey, Year);
                if (ds._v_Schools.Count == 1)
                {
                    strParam = ds._v_Schools[0].schooltype;
                }
            }

            STYP retval = new STYP();
            if (strParam != String.Empty) retval.Value = strParam;
            
            return retval;
        }

        /*public OrgLevel OrgLevel
        {
            get
            {
                //keep for backwards compatibility with old site.
                if (oRGLEVEL.ToLower() == "sc")
                    oRGLEVEL = OrgLevel.School.ToString();
                else if (oRGLEVEL.ToLower() == "di")
                    oRGLEVEL = OrgLevel.District.ToString();
                else if (oRGLEVEL.ToLower() == "st")
                    oRGLEVEL = OrgLevel.State.ToString();

                OrgLevel orglevel = (OrgLevel)Enum.Parse(typeof(OrgLevel), oRGLEVEL);
                return orglevel;
            }
            set { oRGLEVEL = ((OrgLevel)value).ToString(); }
        }*/

        public String GetOrgName()
        {
            return GetOrgName( OrgLevel, FULLKEY); 
        }
        public static String GetOrgName(OrgLevel level, String fullkey)
        {
            return GetOrgName(level.Key, fullkey);
        }
        public static String GetOrgName(String orgLevelKey, String fullKey)
        {
            string retval = string.Empty;
            if (orgLevelKey == OrgLevelKeys.School)
            {
                BLSchool school = new BLSchool();
                retval = school.GetSchoolName(fullKey, school.CurrentYear);
            }
            else if (orgLevelKey == OrgLevelKeys.District)
            {
                BLDistrict dist = new BLDistrict();
                retval = dist.GetDistrictName(fullKey, dist.CurrentYear);
            }
            else
            {
                //(OrgLevel == OrgLevel.State)
                retval = "Entire State";
            }
            return retval;
        }

    }
}
