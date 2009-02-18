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

namespace SligoCS.Web.WI
{
    public partial class WebUserControl2 : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            for (int myInt = 1; myInt <= 12; myInt++)
            {
                string myChar = myInt.ToString();
                LiteralControl mySpacer = new LiteralControl();
                mySpacer.Text = " ";

                HyperLink myLink = new HyperLink();
                myLink.ID = "myLink" + myChar;
                myLink.Text = myChar;
                
                myLink.NavigateUrl = "~/SchoolScript.aspx?SEARCHTYPE=CE&L=" + myChar;
                this.PlaceHolder1.Controls.Add(mySpacer);
                this.PlaceHolder1.Controls.Add(myLink);
            }
        }
    }
}