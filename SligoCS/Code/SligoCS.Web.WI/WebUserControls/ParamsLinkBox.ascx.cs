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
using SligoCS.Web.Base.WebSupportingClasses.WI;


namespace SligoCS.Web.WI.WebUserControls
{
    /// <summary>
    /// This web user control (ASCX) displays a set of rows, such as School Type, View By, etc.
    /// The developer can choose at design time which rows of links to display 
    /// (see troubleshooting tips, below).
    /// </summary>
    public partial class ParamsLinkBox : WIUserControlBase
    {

        protected enum tableRows
        {
            trSTYP,
            trGroup,
            trCred,
            trWMAS,
            trCompareTo
        }

        private bool showTR_STYP = false;
        private bool showTR_Group = false;
        private bool showTR_Cred = false;
        private bool showTR_WMAS = false;
        private bool showTR_CompareTo = false;


        #region public properties
        [System.ComponentModel.Browsable(true)]
        public bool ShowTR_STYP{get{return showTR_STYP;} set{showTR_STYP = value;}}
        public bool ShowTR_Group{get{return showTR_Group;} set{showTR_Group = value;}}
        public bool ShowTR_Cred { get { return showTR_Cred; } set { showTR_Cred = value; } }
        public bool ShowTR_WMAS { get { return showTR_WMAS; } set { showTR_WMAS = value; } }
        public bool ShowTR_CompareTo{get{return showTR_CompareTo;} set{showTR_CompareTo = value;}}
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            /*TROUBLESHOOTING TIPS
             * 
             * 1.  If you've included a ParamsLinkBox on your ASPX page, (or ASCX control, for that matter),
             * and if ALL LINKS ARE DISABLED, you should change the page to inherit from PageBaseWI.
             * --djw 10/17/07
             * 
             * 2.  You can set which rows in the table appear on the web page at design time:
             * a) Create a new ASPX page & make sure it inherits from PageBaseWI.
             * b) In Solutions Explorer (usually upper right hand side of Visual Studio environment)
             *      right click on your new ASPX page, and choose View Designer.
             * c) drag a ParamsLinkBox onto the new ASPX page.
             * d) right click on the ParamsLinkBox, and choose Properties.
             * e) in the Properties window, you'll see "ShowTR_CompareTo, ShowTR_Cred", etc.
             * f) choose whichever row(s) you want to appear, and set them to true.
             * 
             */

            

            HideTableRows();

            //EnableSchoolTypePanel();
        }


        /// <summary>
        /// Enables/ disables School Type panel based on current Org Level.
        /// </summary>
        private void EnableSchoolTypePanel()
        {

            //default to enabled.
            pnlSchoolType.Enabled = true;

            //we don't need to load all of the 25 or so querystring variables at this time.
            //we just need the FullKey.
            ParamsHelper helper = new ParamsHelper();
            object oFullKey = helper.GetParamFromContext(ParamsHelper.QStringVar.FULLKEY.ToString());
            if (oFullKey != null)
            {
                OrgLevel orgLevel = EntityWIBase.GetOrgLevelFromFullKey(oFullKey.ToString());
                if (orgLevel == OrgLevel.School)
                    pnlSchoolType.Enabled = false;                    
            }
        }


        /// <summary>
        /// Shows/hides all of the table rows in the table, based on each row's property setting.
        /// </summary>
        private void HideTableRows()
        {
            //HideTableRow(tableRows.trSTYP, ShowTR_STYP);
            HideTableRow(tableRows.trGroup, ShowTR_Group);
            HideTableRow(tableRows.trCred, ShowTR_Cred);
            HideTableRow(tableRows.trWMAS, ShowTR_WMAS);
            HideTableRow(tableRows.trCompareTo, ShowTR_CompareTo);

            //School Type, if Org Level == School, hide School Type.  Otherwise, show it.

            ParamsHelper helper = new ParamsHelper();
            object oFullKey = helper.GetParamFromContext(ParamsHelper.QStringVar.FULLKEY.ToString());
            if (oFullKey != null)
            {
                OrgLevel orgLevel = EntityWIBase.GetOrgLevelFromFullKey(oFullKey.ToString());
                if (orgLevel == OrgLevel.School)
                    HideTableRow(tableRows.trSTYP, false);
                else
                    HideTableRow(tableRows.trSTYP, true);
                    //pnlSchoolType.Enabled = false;
            }

            
        }

        /// <summary>
        /// Private function.  Shows/ hides a single table row.
        /// </summary>
        /// <param name="trName"></param>
        /// <param name="visible"></param>
        private void HideTableRow(tableRows trName, bool visible)
        {
            Control tr = this.FindControl(trName.ToString());
            if (tr != null)
            {
                tr.Visible = visible;
            }
        }



    }
}