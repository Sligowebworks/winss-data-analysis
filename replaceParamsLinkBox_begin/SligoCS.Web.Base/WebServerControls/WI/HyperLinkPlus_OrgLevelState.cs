using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace SligoCS.Web.Base.WebServerControls.WI
{
    /// <summary>
    /// The org level hyperlinks have special rules that are applied.
    /// This class implements the special rules for the OrgLevel = State link.
    /// </summary>
    public class HyperLinkPlus_OrgLevelState : HyperLinkPlus
    {

        protected override void OnLoad(EventArgs e)
        {
            //See Bug #597, Comment #11
            //  When at State level, "Current School Data" link gets grayed out and links
            //  default to "Prior Years" selection - but the data showing in the table are
            //  "Current Data" - sql query and entire page state should default to "Prior
            //  Years" selection, not just link state
            base.OnLoad(e);         
            if ((HttpContext.Current != null) && (!this.Selected))
            {
                //this.NavigateUrl = ParamsHelper.GetURL(StickyParameter, HttpContext.Current.Request.Url.LocalPath, StickyParameter.QStringVar.CompareTo.ToString(), CompareTo.PRIORYEARS.ToString());
                this.NavigateUrl = ParamsHelper.GetURL(StickyParameter, HttpContext.Current.Request.Url.LocalPath, this.ParamName, this.ParamValue);
            }
        }
    }
}
