using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.WI.WebSupportingClasses.WI;
using SligoCS.BL.WI;

namespace SligoCS.Web.WI.WebUserControls
{
    public partial class ChangeSelectedSchoolOrDistrictLink : System.Web.UI.UserControl
    {
        private GlobalValues GlobalValues;
        private GlobalValues UserValues;
        protected HyperLink theLink = new HyperLink();

        protected void Page_Load(object sender, EventArgs e)
        {
            GlobalValues = ((PageBaseWI)Page).GlobalValues;
            UserValues = ((PageBaseWI)Page).UserValues;
            SetChangeSelectedSchoolOrDistrict();
        }
        protected void SetChangeSelectedSchoolOrDistrict( )
         {
             theLink.Visible = false;

            if ((GlobalValues.CompareTo.Key == CompareToKeys.SelSchools || GlobalValues.CompareTo.Key == CompareToKeys.SelDistricts))
            {
                string qs = UserValues.GetBaseQueryString();

                if (GlobalValues.OrgLevel.Key == OrgLevelKeys.School)
                {
                    theLink.Text = "Change selected schools";
                    theLink.NavigateUrl = "~/selMultiSchools.aspx" + qs;
                    theLink.Visible = true;
                }

                if (GlobalValues.OrgLevel.Key == OrgLevelKeys.District)
                {
                    theLink.Text = "Change selected districts";
                    theLink.NavigateUrl = "~/selMultiDistricts.aspx" + qs;
                    theLink.Visible = true;
                }
            }
        }
    }
}