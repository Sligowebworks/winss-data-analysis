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
    public partial class BottomLinkDownload : System.Web.UI.UserControl
    {
        private string col;

        public string Col
        {
            get
            {
                return col;
            }
            set
            {
                col = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}