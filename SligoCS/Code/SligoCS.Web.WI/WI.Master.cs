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
using SligoCS.Web.Base.WebSupportingClasses.WI;

namespace SligoCS.Web.WI
{
    public partial class WI : System.Web.UI.MasterPage
    {
        private ParamsHelper helper = null;

        /// <summary>
        /// occurs before the Page_Load event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            if ((!Page.IsPostBack) && (Page is PageBaseWI))
            {
                //Load up the instance of ParamsHelper from
                //  the current context (QString, then Session, then Config).
                helper = ((PageBaseWI)Page).ParamsHelper;
                helper.LoadFromContext();
            }

        }


        /// <summary>
        /// The master page's Page_Load event occurs AFTER the aspx page's Page_Load event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //set the URLs for the left hand hyperlinks
            if (helper != null)
            {
                //TODO:  eventually replace these hyperlinks with input from user.
                //hard-coded values for School, District, and State.  See bug #346.
                //lnkSchool.NavigateUrl = helper.GetURL(ParamsHelper.QStringVar.FULLKEY.ToString(), "013619040022");
                linkSchool.ParamValue = "013619040022";
                linkDistrict.ParamValue = "01361903XXXX";
                linkState.ParamValue = "XXXXXXXXXXXX";

                lnkRetention.NavigateUrl = helper.GetURL("/SligoWI/GridPage.aspx", ParamsHelper.QStringVar.STYP.ToString(), "3");
                lnkDropouts.NavigateUrl = helper.GetURL("/SligoWI/Demo.aspx", string.Empty, string.Empty);                
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (helper != null)
            {
                string traceVals = helper.ToString();
                if (traceVals != string.Empty)
                {
                    traceVals = Server.HtmlEncode(traceVals);
                    Response.Write(traceVals);
                }
            }

        }
    }
}
