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
using SligoCS.Web.Base.WebSupportingClasses.WI;
using SligoCS.Web.WI.WebUserControls;

namespace SligoCS.Web.WI.WebUserControls
{
    public partial class InitSelSchoolInfo : System.Web.UI.UserControl
    {
        protected StickyParameter stickyParameter = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            stickyParameter = ((PageBaseWI)Page).StickyParameter;
            string queryString = ParamsHelper.GetQueryString(stickyParameter, string.Empty, string.Empty);

            queryString = ParamsHelper.ReplaceQueryString(queryString, StickyParameter.QStringVar.ORGLEVEL.ToString(), "State");

            STATELevelData.NavigateUrl = "~/questions.aspx" + queryString;
        }
    }
}