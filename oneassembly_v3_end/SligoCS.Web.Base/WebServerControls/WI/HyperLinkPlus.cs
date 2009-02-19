using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SligoCS.Web.Base.WebServerControls.WI
{
    /// <summary>
    /// This class will act just like a regular ASP:HyperLink, only it will be bound to a particular 
    /// value in a StickyParameter class.  If that value is currently set from the Querystring, Session
    /// or config file, the link will become disabled.        
    /// 
    /// Web Server controls (as opposed to Web User Controls) can be added directly to 
    /// a Web Project or Web Application, but they must be included in the App_Code 
    /// subfolder to work in that website.  However, because web server controls are 100% compiled
    /// with no HTML page married to it, web server controls can be placed in a DLL that also resides
    /// logically in the display layer with the web site.  --djw 9/22/07
    /// </summary>
    public class HyperLinkPlus : System.Web.UI.WebControls.HyperLink
    {
        #region class level variables
        private string paramName = String.Empty;
        private string paramValue = String.Empty;
        private StickyParameter stickyParameter = null;
        private bool selected = false;
        private string prefix = " •&#32;";  //a string to prefix before the hyperlink appears.
        #endregion

        #region public properties
        //The developer can set these two properties at design time        
        public string ParamName { get { return paramName; } set { paramName = value; } }    //e.g. "STYP"
        public string ParamValue { get { return paramValue; } set { paramValue = value; } } //e.g. "9"
        public string Prefix { get { return prefix; } set { prefix = value; } }

        //this property is read-only, based on current URL
        public bool Selected { get { return selected; } }
  

        //The StickyParameter object will only be available at runtime.
        [Browsable(false)]
        public StickyParameter StickyParameter
        {
            get
            {
                if ((stickyParameter == null) && (this.Page is PageBaseWI))
                {
                    //since we know the web page is a Wisconsin Site web page, 
                    //we know it'll have a StickyParameter object (see definition of PageBaseWI).
                    stickyParameter = ((PageBaseWI)Page).StickyParameter;
                }
                return stickyParameter;
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


        //public void LoadFromContext()
        //{
        //    if (StickyParameter != null)
        //    {
        //        object val = ParamsHelper.GetParamFromContext(this.paramName, false);
        //        this.paramValue = val.ToString();
        //        if (this.Text.Trim() == string.Empty)
        //        {
        //            this.Text = val.ToString();
        //        }
        //    }
        //}

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (StickyParameter != null)
            {
                //We want the same URL as the current page, except change the value for my stickyParameter.
                string qstring = ParamsHelper.GetQueryString(StickyParameter, this.paramName, this.paramValue);
                string url = string.Format("{0}{1}", Page.Request.Url.LocalPath, qstring);
                this.NavigateUrl = url;

                //If the current user's context is already set to use this param/value, 
                //disable this LinkButtonPlus.
                //E.g. if this LinkButtonPlus is set for STYP=1, and the current
                //  user context is set to STYP=1, then disable this. 
                System.Reflection.PropertyInfo property = StickyParameter.GetType().
                    GetProperty(this.paramName,
                                System.Reflection.BindingFlags.GetProperty
                                | System.Reflection.BindingFlags.Public
                                | System.Reflection.BindingFlags.Instance
                                | System.Reflection.BindingFlags.IgnoreCase);
                if (property != null && property.GetValue(StickyParameter, null) != null )
                {
                    selected = (this.paramValue == property.GetValue(StickyParameter, null).ToString());
                }
                if (selected)
                {
                    //See bug #597
                    this.NavigateUrl = string.Empty;
                    this.ForeColor = System.Drawing.Color.Black;
                }
            }

        }
         

        protected override void OnPreRender(EventArgs e)
        {
            //See bug #597
            if (!this.Enabled)
            {
                this.ForeColor = System.Drawing.Color.Gray;
            }

        }
    }
}
