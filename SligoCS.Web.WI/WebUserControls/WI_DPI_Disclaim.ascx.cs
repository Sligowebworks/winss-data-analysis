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
    public partial class WI_DPI_Disclaim : System.Web.UI.UserControl
    {
        protected GlobalValues globals = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            globals = ((PageBaseWI)Page).GlobalValues;
            string qs = globals.GetQueryString( 
                "FULLKEY", FullKeyUtils.StateFullKey(globals.FULLKEY));
            qs = QueryStringUtils.ReplaceQueryString(qs,
                globals.OrgLevel.Name, globals.OrgLevel.Range[OrgLevelKeys.State]);
            qs = QueryStringUtils.ReplaceQueryString(qs,
                "DN", "None Chosen");
            qs = QueryStringUtils.ReplaceQueryString(qs,
                "SN", "None Chosen");
            EDITselection.NavigateUrl = "~/SelSchool.aspx" + qs;
        }
    }
}