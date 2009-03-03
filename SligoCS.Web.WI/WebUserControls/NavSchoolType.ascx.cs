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

using SligoCS.Web.Base.WebServerControls.WI;

namespace SligoCS.Web.WI.WebUserControls
{
    [ParseChildren(false)]
    [PersistChildren(true)]
    public partial class NavSchoolType : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Controls.Add(STYP_Links);
        }

        public NavSchoolType()
        {
            STYP_Links = new NavigationLinkRow();
            /*linkSTYP_ALLTypes = new HyperLinkPlus();
            linkSTYP_Elem = new HyperLinkPlus();
            linkSTYP_ElSec = new HyperLinkPlus();
            linkSTYP_Hi = new HyperLinkPlus();
            linkSTYP_Mid = new HyperLinkPlus();
            linkSTYP_StateSummary = new HyperLinkPlus();*/
        }
    }
}