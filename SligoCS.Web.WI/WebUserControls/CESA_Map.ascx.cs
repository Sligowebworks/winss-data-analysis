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
    public partial class CESA_Map : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void AddedControl(Control control, int index)
        {
            base.AddedControl(control, index);

            if (control is ImageMap)
                control.Load += SetNavigationURLs;
        }

        private void SetNavigationURLs(Object ctrl, EventArgs args)
        {
            ImageMap map = (ImageMap)ctrl;

            foreach (HotSpot spot in map.HotSpots)
            {
                String oldUrl = spot.NavigateUrl;
                
                Web.Base.PageBase.WI.PageBaseWI page = ((Web.Base.PageBase.WI.PageBaseWI)Page);
                
                // restore the array separator, with the exception of the "?"
                // add in the base querystring to keep our app state
                String newURL = oldUrl.Replace(".aspx?", ".aspx" + page.UserValues.GetBaseQueryString() + "&");

                spot.NavigateUrl = newURL;
            }
        }
    }
}