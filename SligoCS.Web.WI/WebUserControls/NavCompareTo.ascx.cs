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
        public Boolean ShowSimilarSchoolsLink = false;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public NavigationLinkRow LinkRow
        {
            get{  return CompareTo_Links;}
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            CompareTo_Links.LinkControlAdded += HandleLinkControlAdded;
        }
        protected void HandleLinkControlAdded(HyperLinkPlus link)
        {
            OrgLevel ol = ((PageBaseWI)Page).GlobalValues.OrgLevel;

            if (link.ID == "linkCompareToSimSchools")
                link.Visible = ShowSimilarSchoolsLink;

            if (link.ID == "linkCompareToSelDistricts")
                link.Visible = false; // hide by default, shown below...

            if (ol.Key == OrgLevelKeys.State)
            {
                if (link.ID == "linkCompareToCurrent" || link.ID == "linkCompareToSimilar") link.Text = link.Text.Replace("School", "State");

                if (//link.ID == "linkCompareToCurrent"
                     link.ID == "linkCompareToSelSchools"
                    | link.ID == "linkCompareToSelDistricts"
                    | link.ID == "linkCompareToOrgLevel")
                {
                    link.Enabled = false;
                }

            }
            else if (ol.Key == OrgLevelKeys.District)
            {
                //Note from Bug 379:
                //- Selected schools link - should say Selected Districts at district level 
                if (link.ID == "linkCompareToSelSchools") //Bug 597:  Current District Data
                {
                    link.Visible = false;
                }
                else if (link.ID == "linkCompareToSelDistricts")
                {
                    link.Visible = true;
                }

                if (link.ID == "linkCompareToCurrent") 
                {
                    link.Text = link.Text.Replace("School", "District");
                }

                if (link.ID == "linkCompareToOrgLevel")
                {
                    link.Text = link.Text.Replace("District/", "");
                }
                if (link.ID == "linkCompareToSimSchools")
                {
                    link.Text = link.Text.Replace("Schools", "Districts");
                }
            }
            else // OrgLevel = school - show SelSchools link, hide SelDistricts link
            {
                if (link.ID == "linkCompareToSelSchools") //Bug 597:  Current District Data
                {
                    link.Visible = true;
                }
                else if (link.ID == "linkCompareToSelDistricts")
                {
                    link.Visible = false;
                }
            }
        }
    }
}