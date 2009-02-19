using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.WI.WebServerControls.WI
{
    public class MergeColumn: System.Web.UI.WebControls.BoundField
    {
        #region class level variables
        private bool enableMerge = true;
        #endregion

        #region public properties
        public bool EnableMerge { get { return enableMerge; } set { enableMerge = value; } }
        #endregion
    
    }
}
