using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.Web.Base.PageBase.WI;

namespace SligoCS.Web.WI.WebSupportingClasses
{
    public class  HyperLinkToggle : SligoCS.Web.Base.WebServerControls.WI.HyperLinkPlus
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            WI.GlobalValues GV =  ((PageBaseWI)Page).GlobalValues;

            System.Reflection.PropertyInfo propertyInfo = GV.GetType().
                GetProperty(this.ParamName,
                            System.Reflection.BindingFlags.GetProperty
                            | System.Reflection.BindingFlags.Public
                            | System.Reflection.BindingFlags.Instance
                            | System.Reflection.BindingFlags.IgnoreCase);

            ((PageBaseWI)Page).SetNavigationLinkSelectedStatus(propertyInfo, this);

            if (Selected) Visible = false;
        }
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            if (!Selected && ParamValue == "N")
            {  
                this.ForeColor = selectedColor;
            }
            
            base.Render(writer);
        }
    }
}
