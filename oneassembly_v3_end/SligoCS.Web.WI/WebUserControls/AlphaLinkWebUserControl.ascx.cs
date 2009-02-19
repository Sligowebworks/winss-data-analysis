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
using SligoCS.Web.WI.WebUserControls;

namespace SligoCS.Web.WI
{
    public partial class WebUserControl1 : System.Web.UI.UserControl
    {
        public enum sdc_type
        {
            SC,
            DI,
            CO
        }
        private sdc_type sdc;

        protected StickyParameter stickyParameter = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            char loopEnd;
            loopEnd = 'Z';

            switch (SDC.ToString())
            {
                case "CO":
                    loopEnd = 'W';
                    break;
            }

            stickyParameter = ((PageBaseWI)Page).StickyParameter;

            for (char myChar = 'A'; myChar <= loopEnd; myChar++)
            {
                if ((myChar != 'Q') && (myChar != 'X'))
                {
                    String nav_url;

                    LiteralControl mySpacer = new LiteralControl();
                    mySpacer.Text = " ";

                    HyperLink myLink = new HyperLink();
                    myLink.ID = "myLink" + myChar;
                    myLink.Text = myChar.ToString();        

                    nav_url = ParamsHelper.GetQueryString(stickyParameter, string.Empty, string.Empty);

                    myLink.NavigateUrl = "~/SchoolScript.aspx" + nav_url + "&SEARCHTYPE=" + SDC.ToString() + "&L=" + myChar.ToString();

                    this.PlaceHolder1.Controls.Add(mySpacer);
                    this.PlaceHolder1.Controls.Add(myLink);
                }
            }
        }

        public sdc_type SDC
        {
            get
            {
                return sdc;
            }
            set
            {
                sdc = value;
            }
        }

        protected void SchoolRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void Repeater_OnItemDataBound(object source, RepeaterCommandEventArgs e)
        {

        }

    }
}