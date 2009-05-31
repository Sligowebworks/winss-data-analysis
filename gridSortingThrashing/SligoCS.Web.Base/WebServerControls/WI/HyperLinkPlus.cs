using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Drawing;

namespace SligoCS.Web.Base.WebServerControls.WI
{
    /// <summary>
    /// This class will act just like a regular ASP:HyperLink, only it can be bound to a name-value pair.  
    /// The link is active by default, disabled when selected.
    /// </summary>
    public class HyperLinkPlus : System.Web.UI.WebControls.HyperLink
    {
        #region class level variables
        private string paramName = String.Empty;
        private string paramValue = String.Empty;
        private string urlFile = null;
        private bool selected = false;
        private string prefix = " •&#32;";  //a string to prefix before the hyperlink appears.
        private Color selectedColor = Color.Black;
        private Color disabledColor = Color.Gray;
        #endregion

        #region public properties
        //The developer can set these properties at design time        
        public string ParamName { get { return paramName; } set { paramName = value; } }    //e.g. "STYP"
        public string ParamValue { get { return paramValue; } set { paramValue = value; } } //e.g. "9"
        /// <summary>
        /// File name component of the url. Must be relative to the Application Root Path.
        /// </summary>
        public string UrlFile {  get { return urlFile; } set { urlFile = value;}}
        public string Prefix { get { return prefix; } set { prefix = value; } }
        public Color SelectedColor { get { return selectedColor; } set { selectedColor = value; } }
        public Color DisabledColor { get { return disabledColor; } set { disabledColor = value; } }

        public bool Selected { 
            get { return selected; }
            set
            {
                selected = value;
                if (selected)
                {
                    //See bug #597
                    this.NavigateUrl = string.Empty;
                    this.ForeColor = selectedColor;
                }
            }
        }
        
        #endregion

        #region event handlers
        public override void RenderBeginTag(System.Web.UI.HtmlTextWriter writer)
        {
            if (Prefix != null)
                writer.Write(Prefix);
            base.RenderBeginTag(writer);
        }
        #endregion        

        protected override void OnPreRender(EventArgs e)
        {
            //See bug #597
            if (!this.Enabled)
            {
                this.ForeColor = disabledColor;
            }
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            base.Render(writer);
        }
    }
}
