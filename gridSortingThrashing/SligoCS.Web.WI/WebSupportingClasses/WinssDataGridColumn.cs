using System;
using System.Collections.Generic;
using System.Text;

using SligoCS.Web.Base.WebServerControls.WI;

namespace SligoCS.Web.WI.WebSupportingClasses
{
    public class WinssDataGridColumn : MergeColumn 
    {
        private string formatString;

        public string FormatString { get { return formatString ;} set { formatString = value;}}

        public override bool Initialize(bool enableSort, System.Web.UI.Control ctrl)
        {
            
            if (this is System.Web.UI.WebControls.BoundField)
            {
                if (HeaderText == "" && DataField != "") HeaderText = DataField;
            }

            return base.Initialize(enableSort, ctrl);
        }

        public WinssDataGridColumn()
            : base()
        {
            this.Visible = false;
        }
    }
}
