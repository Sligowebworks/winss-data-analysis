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
    public partial class NavSelectYear : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GlobalValues globals = ((PageBaseWI)Page).GlobalValues;
            if (globals.CompareTo.Key != CompareToKeys.Years)
                createLinks(globals);
            else
                LinkRow.Visible = false;
        }

        protected void createLinks(GlobalValues globals)
        {
            int start = globals.TrendStartYear;
            int end = globals.LatestYear;
            HyperLinkPlus link;
            int linkYear;

            for (linkYear = end; linkYear >= start; linkYear--)
            {
                link = new HyperLinkPlus();
                link.ParamName = "Year";
                link.ParamValue = linkYear.ToString();
                link.Text = (SingleYearLabel || SingleYearPrevLabel)
                    ? LinkPrefix + ((SingleYearPrevLabel) ? linkYear - 1 : linkYear)
                    : LinkPrefix + String.Format("{0}-{1}", (linkYear - 1), linkYear.ToString().Substring(2))
                    ;

                SelectYear_Row.NavigationLinks.Add(link);
            }
        }

        string linkPrefix;
        public String LinkPrefix { get { return linkPrefix; } set { linkPrefix = value; } }

        Boolean singleYearLabel;
        public Boolean SingleYearLabel { get { return singleYearLabel; } set { singleYearLabel = value; } }
        Boolean singleYearPrevLabel;
        public Boolean SingleYearPrevLabel { get { return singleYearPrevLabel; } set { singleYearPrevLabel = value; } }

        public NavigationLinkRow LinkRow
        {
            get { return SelectYear_Row; }
            set { SelectYear_Row = value; }
        }
    }
}