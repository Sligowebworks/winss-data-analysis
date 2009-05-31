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
    public partial class NavSchoolType : System.Web.UI.UserControl
    {
        public NavigationLinkRow LinkRow
        {
            get { return Links; }
        }

        protected override void EnsureChildControls()
        {
            base.EnsureChildControls();

            Links.LinkControlAdded += HandleLinkControlAdded;
        }

        protected void HandleLinkControlAdded(HyperLinkPlus link)
        {
            if (OrgLevel.Key == OrgLevelKeys.State)
                    if (link.ID == "linkSTYP_StateSummary")
                        link.Text = link.Text.Replace("School", "State");
            
            if (OrgLevel.Key == OrgLevelKeys.District)
                if (link.ID == "linkSTYP_StateSummary")
                        link.Text = link.Text.Replace("School", "District");

                    if (OrgLevel.Key == OrgLevelKeys.School)
                 LinkRow.Visible = false;
        }

        protected OrgLevel OrgLevel
        {
            get
            {
                return ((PageBaseWI)Page).GlobalValues.OrgLevel;
            }
        }

    }
}