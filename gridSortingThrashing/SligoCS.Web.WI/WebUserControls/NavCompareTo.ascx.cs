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
    public partial class NavCompareTo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public NavigationLinkRow LinkRow
        {
            get{  return CompareTo_Links;}
        }

        protected override void EnsureChildControls()
        {
            base.EnsureChildControls();

            CompareTo_Links.LinkControlAdded += HandleLinkControlAdded;

        }

        protected void HandleLinkControlAdded(HyperLinkPlus link)
        {
            OrgLevel ol = ((PageBaseWI)Page).GlobalValues.OrgLevel;

            if (ol.Key == OrgLevelKeys.State)
            {
                if (link.ID == "linkCompareToCurrent") link.Text = link.Text.Replace("School", "State");

                if (link.ID == "linkCompareToCurrent"
                    | link.ID == "linkCompareToSelSchools"
                    | link.ID == "linkCompareToOrgLevel")
                {
                    link.Enabled = false;
                }

            }
            else if (ol.Key == OrgLevelKeys.District)
            {
                //Note from Bug 379:
                //- Selected schools link - should say Selected Districts at district level 
                if (link.ID == "linkCompareToSelSchools"
                    | link.ID == "linkCompareToCurrent") //Bug 597:  Current District Data
                {
                    link.Text = link.Text.Replace("School", "District");
                }

                if (link.ID == "linkCompareToOrgLevel")
                {
                    link.Text = link.Text.Replace("District/", "");
                }
            }
        }
    }
}