using System;
using System.Collections.Generic;
using System.Text;

namespace SligoCS.Web.Base.WebServerControls.WI
{
    public class MergeColumn: System.Web.UI.WebControls.BoundField
    {
        #region class level variables
        private bool mergeRows = false;
        #endregion

        #region public properties
        public bool EnableMerge { get { return mergeRows; } set { mergeRows = value; } }
        public bool MergeRows { get { return mergeRows; } set { mergeRows = value; } }
        #endregion
    
    }
}
