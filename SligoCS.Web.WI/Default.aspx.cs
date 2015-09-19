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
using SligoCS.BL.WI;

namespace SligoCS.Web.WI
{
    /// <summary>
    /// This web page will act as the home page for the Wisconsin web site.
    /// </summary>
    public partial class Default : PageBaseWI
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Master.EnableViewState = false;

            set_state();
        }

        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.Mixed_Header_Graphics1, true);
        }
    }
}
