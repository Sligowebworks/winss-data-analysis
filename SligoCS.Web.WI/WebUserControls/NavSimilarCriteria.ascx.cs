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

using SligoCS.Web.Base.PageBase.WI;//.PageBaseWI;
using SligoCS.Web.WI.WebSupportingClasses.WI;//.QueryStringUtils; .GlobalValues
using SligoCS.Web.Base.WebServerControls.WI;//.HyperLinkPlus
using SligoCS.BL.WI; //.OrgLevel

namespace SligoCS.Web.WI.WebUserControls
{
    public partial class NavSimilarCriteria : System.Web.UI.UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            
            NavSimilarCriteria_LinkRow.LinkControlAdded += HandleLinkControlAdded;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public NavigationLinkRow LinkRow
        {
            get { return NavSimilarCriteria_LinkRow; }
        }
        protected void HandleLinkControlAdded(HyperLinkPlus link)
        {
            GlobalValues GlobalValues = ((PageBaseWI)Page).GlobalValues;

            //when the first link is hidden, remove the bullet in front of the second link;
            if (link.ID == "linkSPENDOff") link.Prefix = String.Empty;

        }
    }
}