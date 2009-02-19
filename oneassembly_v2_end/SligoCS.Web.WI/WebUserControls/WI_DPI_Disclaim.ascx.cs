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
using SligoCS.Web.WI.PageBase.WI;
using SligoCS.Web.WI.WebSupportingClasses.WI;
using SligoCS.Web.WI.WebUserControls;

namespace SligoCS.Web.WI.WebUserControls
{
    public partial class WI_DPI_Disclaim : System.Web.UI.UserControl
    {
        protected StickyParameter stickyParameter = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            stickyParameter = ((PageBaseWI)Page).StickyParameter;
            string qs = ParamsHelper.GetQueryString(stickyParameter, 
                StickyParameter.QStringVar.FULLKEY.ToString(), "ZZZZZZZZZZZZ");
            qs = ParamsHelper.ReplaceQueryString(qs,
                StickyParameter.QStringVar.ORGLEVEL.ToString(), "State");
            qs = ParamsHelper.ReplaceQueryString(qs,
                StickyParameter.QStringVar.DN.ToString(), "None Chosen");
            qs = ParamsHelper.ReplaceQueryString(qs,
                StickyParameter.QStringVar.SN.ToString(), "None Chosen");
            EDITselection.NavigateUrl = "~/SelSchool.aspx" + qs;
        }
    }
}