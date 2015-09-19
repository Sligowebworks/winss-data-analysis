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

using SligoCS.BL.WI;
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.WI.WebSupportingClasses.WI;

namespace SligoCS.Web.WI.WebUserControls
{
    public partial class BottomLinkViewProfile : System.Web.UI.UserControl
    {
        private string districtCd = string.Empty;
        protected GlobalValues globals = null;

        public string DistrictCd
        {
            get
            {
                return districtCd;
            }
            set
            {
                districtCd = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Page is PageBaseWI)
            {
                globals = ((PageBaseWI)Page).GlobalValues;
            }

            if (globals.OrgLevel.Key != OrgLevelKeys.District)
            {
                LinkPanel.Visible = false;
            }
            DistrictCd = globals.DistrictCode;
        }
    }
}