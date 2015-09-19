using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SligoCS.Web.WI.WebUserControls
{
    public class CESAMapBase : System.Web.UI.UserControl
    {
        public String GetQueryString(String[] NameValuePairs)
        {
            return ((SligoCS.Web.Base.PageBase.WI.PageBaseWI)Page).UserValues.GetQueryString(NameValuePairs);
        }
    }
}
