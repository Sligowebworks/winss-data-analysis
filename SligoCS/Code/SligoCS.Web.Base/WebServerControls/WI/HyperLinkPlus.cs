using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.Base.WebSupportingClasses.WI;


namespace SligoCS.Web.Base.WebServerControls.WI
{
    /// <summary>
    /// This class will act just like a regular ASP:HyperLink, only it will be bound to a particular 
    /// value in a ParamsHelper class.  If that value is currently set from the Querystring, Session
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
        private ParamsHelper paramsHelper = null;
        #endregion

        #region public properties
        //The developer can set these two properties at design time
        public string ParamName { get { return paramName; } set { paramName = value; } }    //e.g. "STYP"
        public string ParamValue { get { return paramValue; } set { paramValue = value; } } //e.g. "9"

        //The ParamsHelper object will only be available at runtime.
        [Browsable(false)]
        public ParamsHelper ParamsHelper
        {
            get
            {
                if ((paramsHelper == null) && (this.Page is PageBaseWI))
                {
                    //since we know the web page is a Wisconsin Site web page, 
                    //we know it'll have a ParamsHelper object (see definition of PageBaseWI).
                    paramsHelper = ((PageBaseWI)Page).ParamsHelper;
                }
                return paramsHelper;
            }
        }
        #endregion

        #region event handlers
        #endregion        

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (ParamsHelper != null)
            {

                //We want the same URL as the current page, except change the value for my parameter.
                string qstring = ParamsHelper.GetQueryString(this.paramName, this.paramValue);
                string url = string.Format("{0}?{1}", Page.Request.Url.LocalPath, qstring);
                this.NavigateUrl = url;

                //If the current user's context is already set to use this param/value, 
                //disable this LinkButtonPlus.
                //E.g. if this LinkButtonPlus is set for STYP=1, and the current
                //  user context is set to STYP=1, then disable this.                
                bool bEnabled = (this.ParamValue != ParamsHelper[this.ParamName]);
                this.Enabled = bEnabled;
                if (!bEnabled) this.NavigateUrl = string.Empty;


            }

        }
    }
}
