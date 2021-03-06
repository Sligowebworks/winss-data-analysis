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
using SligoCS.Web.WI.WebUserControls;

namespace SligoCS.Web.WI.WebUserControls
{
    public partial class Big_Header_Graphics : System.Web.UI.UserControl
    {
        protected GlobalValues globals = null;
        protected GlobalValues user = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            globals = ((PageBaseWI)Page).GlobalValues;
            set_link_item("WINSS", "~/questions.aspx", "Wins_Mortar_Guide", "wins_big", "mortar_big", "guide_big");
            data_big_gif.NavigateUrl = "~/questions.aspx" + globals.GetQueryString( string.Empty, string.Empty);
        }

        private void set_link_item(string link_text, string nav_url, string placeholder_name, string pic_name1, string pic_name2, string pic_name3)
        {
            string controlID = placeholder_name.ToString();
            globals = ((PageBaseWI)Page).GlobalValues;
            user = ((PageBaseWI)Page).UserValues;

            Image myImage1 = new Image();
            myImage1.AlternateText = link_text;
            myImage1.ImageUrl = "~/images/" + pic_name1 + ".gif";
            myImage1.Attributes.Add("name", pic_name1);

            Image myImage2 = new Image();
            myImage2.AlternateText = link_text;
            myImage2.ImageUrl = "~/images/" + pic_name2 + ".gif";
            myImage2.Attributes.Add("name", pic_name2);

            Image myImage3 = new Image();
            myImage3.AlternateText = link_text;
            myImage3.ImageUrl = "~/images/" + pic_name3 + ".gif";
            myImage3.Attributes.Add("name", pic_name3);

            HyperLink item_link = new HyperLink();
            item_link.ID = placeholder_name + "_link";
            item_link.Text = link_text;
            item_link.NavigateUrl = user.CreateNewURL(nav_url,
                            globals.GraphFile.Name,
                            globals.GraphFile.Range[GraphFileKeys.BLANK_REDIRECT_PAGE]);

            item_link.Attributes.Add("onMouseOver", "img_hot('" + pic_name2 + "')");
            item_link.Attributes.Add("onMouseOut", "img_cool('" + pic_name2 + "')");
            item_link.Controls.Add(myImage1);
            item_link.Controls.Add(myImage2);
            item_link.Controls.Add(myImage3);

            Control c = FindControl(controlID);
            if (c != null)
            {
                c.Controls.Add(item_link);
                c.Visible = true;
            }
        }
    }
}