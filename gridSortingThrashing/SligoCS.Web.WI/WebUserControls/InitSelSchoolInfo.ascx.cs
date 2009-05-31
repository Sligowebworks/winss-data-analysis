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

using SligoCS.BL.WI;
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.WI.WebSupportingClasses.WI;
using SligoCS.Web.WI.WebUserControls;

namespace SligoCS.Web.WI.WebUserControls
{
    public partial class InitSelSchoolInfo : System.Web.UI.UserControl
    {
        protected GlobalValues globals = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            globals = ((PageBaseWI)Page).GlobalValues;
            string queryString = globals.GetQueryString(string.Empty, string.Empty);

            queryString = QueryStringUtils.ReplaceQueryString(queryString, StickyParameter.QStringVar.OrgLevel.ToString(), globals.OrgLevel.Range[OrgLevelKeys.State]);

            STATELevelData.NavigateUrl = "~/questions.aspx" + queryString;
        }
    }
}