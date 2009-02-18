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
using SligoCS.Web.Base.WebSupportingClasses.WI;


namespace SligoCS.Web.WI.WebUserControls
{
    public partial class BottomLinkViewReport : System.Web.UI.UserControl
    {
        private string districtCd = string.Empty;
        private string year = string.Empty;
        private string schoolCd = string.Empty;

        protected StickyParameter StickyParameter = null;


        public string Year
        {
            get
            {
                return year;
            }
            set
            {
                year = value;
            }
        }
        
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

        public string SchoolCd
        {
            get
            {
                return schoolCd;
            }
            set
            {
                schoolCd = value;
            }
        }

        // ?year=2007&districtcd=3619&schoolcd=0022
        public string QueryStringInBottomLink
        {
            get 
            {
                string result = string.Empty;
                if (year != string.Empty)
                { result = "?year=" + year; }

                if (districtCd != string.Empty)
                { result = result + "&districtcd=" + districtCd; }

                if (schoolCd != string.Empty)
                { result = result + "&schoolcd=" + schoolCd; }

                return result;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Page is PageBaseWI)
            {
                StickyParameter = ((PageBaseWI)Page).StickyParameter;
            }

            if (StickyParameter.ORGLEVEL == OrgLevel.State.ToString())
            {
                LinkPanel.Visible = false;
            }

        }
    }
}