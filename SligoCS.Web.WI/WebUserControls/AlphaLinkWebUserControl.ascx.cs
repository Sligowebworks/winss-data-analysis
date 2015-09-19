using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SligoCS.BL.WI;
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.WI.WebSupportingClasses.WI;
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

        protected GlobalValues user = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            char loopEnd;
            loopEnd = 'Z';

            user = ((PageBaseWI)Page).UserValues;

            for (char myChar = 'A'; myChar <= loopEnd; myChar++)
            {
                LiteralControl mySpacer = new LiteralControl();
                mySpacer.Text = " ";

                HyperLink myLink = new HyperLink();
                myLink.ID = "myLink" + myChar;
                myLink.Text = myChar.ToString();        

                String qString = user.GetBaseQueryString();
                qString = QueryStringUtils.ReplaceQueryString(qString, "SEARCHTYPE",  SDC.ToString() );
                qString = QueryStringUtils.ReplaceQueryString(qString, "L", myChar.ToString());
                myLink.NavigateUrl = user.CreateURL("~/SchoolScript.aspx", qString.ToString());

                this.PlaceHolder1.Controls.Add(mySpacer);
                this.PlaceHolder1.Controls.Add(myLink);
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