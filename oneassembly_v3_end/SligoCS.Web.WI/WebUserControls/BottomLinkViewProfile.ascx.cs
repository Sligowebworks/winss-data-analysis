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

using SligoCS.BLL.WI;
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.Base.WebSupportingClasses.WI;

namespace SligoCS.Web.WI.WebUserControls
{
    public partial class BottomLinkViewProfile : System.Web.UI.UserControl
    {
        private string districtCd;
        protected StickyParameter StickyParameter = null;

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
                StickyParameter = ((PageBaseWI)Page).StickyParameter;
            }

            if (StickyParameter.ORGLEVEL != OrgLevel.District.ToString())
            {
                LinkPanel.Visible = false;
            }

        }
    }
}