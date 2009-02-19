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

namespace SligoCS.Web.WI.WebUserControls
{
    public partial class CESA_Map_1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            add_JS_distinct_caption();
        }

        private void add_JS_distinct_caption()
        {
            HtmlGenericControl myJs = new HtmlGenericControl();
            myJs.TagName = "script";
            myJs.Attributes.Add("type", "text/javascript");
            myJs.Attributes.Add("language", "javascript");
            myJs.Attributes.Add("src", ResolveUrl("/SligoWI/js/district_caption.js"));
            this.Page.Header.Controls.Add(myJs);
        }
    }
}