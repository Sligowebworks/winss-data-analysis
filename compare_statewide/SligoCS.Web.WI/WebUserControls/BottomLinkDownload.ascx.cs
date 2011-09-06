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

namespace SligoCS.Web.WI.WebUserControls
{
    public partial class BottomLinkDownload : System.Web.UI.UserControl
    {
        private string url;
        private string ignoreSelectionsUrl;

        public string Url
        {
            get
            {
                return url;
            }
            set
            {
                url = value;
            }
        }

        public string IgnoreSelectionsUrl
        {
            get
            {
                return ignoreSelectionsUrl;
            }
            set
            {
                ignoreSelectionsUrl = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (bool.Parse(ConfigurationManager.AppSettings["GenericCsvName"]))
            {
                Url = ResolveUrl("~/serveRawDataCsv.aspx");
            }
            else
            {
                Url = ResolveUrl("~/" + Session["RawCsvName"]);
            }

            PageBaseWI page = ((PageBaseWI)Page);
            SupDwnld supDwnld = page.GlobalValues.SuperDownload;

            IgnoreSelectionsUrl = Request.Url.LocalPath + page.UserValues.GetQueryString(supDwnld.Name, supDwnld.Range[SupDwnldKeys.True]);
        }
    }
}