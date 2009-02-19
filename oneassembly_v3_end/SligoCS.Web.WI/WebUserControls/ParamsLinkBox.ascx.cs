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

using SligoCS.DAL.WI;
using SligoCS.BLL.WI;
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

        private enum tableRows
        {
            trSTYP,
            trShowMoney,
            trShowRatioOfStaff,
            trTypeCost,
            trGroup,
            trCred,
            trShowGraphFile,
            trShowGradReqs,
            trCredential,
            trPostGradShow,
            trTQShow,            
            trACTSubjects,
            trWMAS,
            trTQSubjects,
            trCompareTo,
            trShowActivitiesOffer
        }

        private bool showTR_STYP = false;
        private bool showTR_ShowMoney = false;
        private bool showTR_ShowRatioOfStaff = false;
        private bool showTR_TypeCost = false;
        private bool showTR_Group = false;
        private bool showTR_Cred = false;
        private bool showTR_ShowGraphFile = false;
        private bool showTR_GradReqs = false;
        private bool showTR_Credential = false;
        private bool showTR_PostGradShow = false;
        private bool showTR_TQShow = false;
        private bool showTR_WMAS = false;
        private bool showTR_TQSubjects = false;
        private bool showTR_CompareTo = false;
        private bool showTR_ACTSubjects = false;
        private bool showTR_ShowActivitiesOffer = false;

        private StickyParameter stickyParameter = null;


        #region public properties
        [System.ComponentModel.Browsable(true)]
        public bool ShowTR_STYP{get{return showTR_STYP;} set{showTR_STYP = value;}}
        public bool ShowTR_ShowMoney { get { return showTR_ShowMoney; } set { showTR_ShowMoney = value; } }
        public bool ShowTR_ShowRatioOfStaff { get { return showTR_ShowRatioOfStaff; } set { showTR_ShowRatioOfStaff = value; } }
        public bool ShowTR_TypeCost { get { return showTR_TypeCost; } set { showTR_TypeCost = value; } }
        public bool ShowTR_Group{get{return showTR_Group;} set{showTR_Group = value;}}
        public bool ShowTR_Cred { get { return showTR_Cred; } set { showTR_Cred = value; } }
        public bool ShowTR_ShowGraphFile { get { return showTR_ShowGraphFile; } set { showTR_ShowGraphFile = value; } }
        public bool ShowTR_GradReqs { get { return showTR_GradReqs; } set { showTR_GradReqs = value; } }
        public bool ShowTR_Credential { get { return showTR_Credential; } set { showTR_Credential = value; } }
        public bool ShowTR_PostGradShow { get { return showTR_PostGradShow; } set { showTR_PostGradShow = value; } }
        public bool ShowTR_TQShow { get { return showTR_TQShow; } set { showTR_TQShow = value; } }
        public bool ShowTR_WMAS { get { return showTR_WMAS; } set { showTR_WMAS = value; } }
        public bool ShowTR_TQSubjects { get { return showTR_TQSubjects; } set { showTR_TQSubjects = value; } }
        public bool ShowTR_CompareTo{get{return showTR_CompareTo;} set{showTR_CompareTo = value;}}
        public bool ShowTR_ACTSubjects {get{return showTR_ACTSubjects;} set{showTR_ACTSubjects = value;}}
        #endregion

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (stickyParameter != null)
            {
                OrgLevel orglevel = ParamsHelper.GetOrgLevel(stickyParameter);
                SetACTAndAPLinks();
                SetLinks(orglevel);
                DisableLinks(orglevel);
            }

        }


        /// <summary>
        /// Post Grad Intent page should not show viewby= Disability, Grade, etc.
        /// </summary>
        private void SetLinks(OrgLevel orgLevel)
        {
            if (Page is PostGradIntentPage)
            {
                linkGroup_Grade.Visible = false;
                linkDisability.Visible = false;
                linkGroup_EconDisadv.Visible = false;
                linkGroup_ELP.Visible = false;
            }
            else if (Page is HSCompletionPage)
            {
                linkGroup_EconDisadv.Enabled = false;
                linkGroup_ELP.Enabled = false;
                if ( stickyParameter.HSC ==  StickyParameter.HSC_ALL)
                {
                    linkGroup_Gender.Enabled = false;
                    linkGroup_RaceEthnicity.Enabled = false;
                    linkGroup_Grade.Enabled = false;
                    linkDisability.Enabled = false;
                    linkGroup_EconDisadv.Enabled = false;
                    linkGroup_ELP.Enabled = false;
                }
            }
            else if (Page is Truancy)
            {
                linkDisability.Enabled = false;
                linkGroup_EconDisadv.Visible = false;
                linkGroup_ELP.Visible = false;
                if (orgLevel == OrgLevel.State)
                {
                    linkSTYP_ALLTypes.Enabled = false;
                    linkSTYP_Elem.Enabled = false;
                    linkSTYP_Mid.Enabled = false;
                    linkSTYP_Hi.Enabled = false;
                    linkSTYP_ElSec.Enabled = false;
                }
            }
            else if (Page is ActivitiesOffer)
            {
                linkDisability.Enabled = false;
                linkGroup_EconDisadv.Visible = false;
                linkGroup_ELP.Visible = false;
                if (orgLevel == OrgLevel.School)
                {
                    linkSTYP_ALLTypes.Enabled = false;
                    linkSTYP_Elem.Enabled = false;
                    linkSTYP_Mid.Enabled = false;
                    linkSTYP_Hi.Enabled = false;
                    linkSTYP_ElSec.Enabled = false;
                }
            }

        }

        private void DisableLinks(OrgLevel orgLevel)
        {
            SchoolType schoolType =
                (SchoolType)Enum.Parse(typeof(SchoolType), stickyParameter.STYP.ToString());
            ViewByGroup viewBy =
                (ViewByGroup)Enum.Parse(typeof(ViewByGroup), stickyParameter.Group);
            //From Bug 597 Comment 2:
            //When at State level:
            //District/State • Selected Schools • Current School Data
            //should all get grayed out and page should default to "Prior Years" selection 

            if (orgLevel == OrgLevel.State)
            {
                //only explictly set the flags here when orglevel == state.
                //If org level != state, don't mess with them!
                linkCompareTo_DistState.Enabled = false;
                linkCompareTo_SelSchools.Enabled = false;
                linkCompareTo_CurrentOnly.Enabled = false;
                linkCompareTo_PriorYears.Enabled = true;
                linkCompareTo_PriorYears.NavigateUrl = string.Empty;
            }

            ////On Retention page, Disability is visible, but disabled.  (Different than currently selected).
            //  // Reenabled per #966  
            //    if (Page is GridPage)
            //    {
            //        linkDisability.Enabled = false;
            //    }
                
            if (Page is GridPage || Page is Truancy)
                {
                    if ((orgLevel == OrgLevel.State || orgLevel == OrgLevel.District) &&
                        schoolType == SchoolType.AllTypes)
                    {
                        linkGroup_Gender.Enabled = false;
                        linkGroup_RaceEthnicity.Enabled = false;
                        linkGroup_Grade.Enabled = false;
                        linkDisability.Enabled = false;
                        linkGroup_EconDisadv.Enabled = false;
                        linkGroup_ELP.Enabled = false;
                    }
                }
        }

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


            if (Page is PageBaseWI)
            {
                stickyParameter = ((PageBaseWI)Page).StickyParameter;
                OrgLevel orglevel = ParamsHelper.GetOrgLevel(stickyParameter);
                HideTableRows(orglevel);
                SetDisplayTextForLinks(orglevel);
            }

        }


        /// <summary>
        /// The ACT and AP Tests links are not just stickyParameter variables; they jump between 2 pages.
        /// </summary>
        private void SetACTAndAPLinks()
        {
            if (this.Page is PageBaseWI)
            {
                StickyParameter stickyParameter = ((PageBaseWI)Page).StickyParameter;
                linkShowGraphfile_AP.NavigateUrl = ParamsHelper.GetURL(stickyParameter, "/SligoWI/APTestsPage.aspx", StickyParameter.QStringVar.GraphFile.ToString(), "AP");
                linkShowGraphfile_ACT.NavigateUrl = ParamsHelper.GetURL(stickyParameter, "/SligoWI/ACTPage.aspx", StickyParameter.QStringVar.GraphFile.ToString(), "ACT");
            }

            if (Page is APTestsPage)
            {
                linkDisability.Enabled = false;
                linkGroup_Grade.Enabled = false;
                linkGroup_ELP.Enabled = false;
                linkGroup_EconDisadv.Enabled = false;
                linkShowGraphfile_AP.NavigateUrl = string.Empty;
            }
            if (Page is ACTPage)
            {
                linkDisability.Enabled = false;
                linkGroup_Grade.Enabled = false;
                linkGroup_ELP.Enabled = false;
                linkGroup_EconDisadv.Enabled = false;
                linkShowGraphfile_ACT.NavigateUrl = string.Empty;
            }

        }


        /// <summary>
        /// Sets the display on certain links, which changes based on current context.
        /// </summary>
        private void SetDisplayTextForLinks(OrgLevel orgLevel)
        {
            //we don't need to load all of the 25 or so querystring variables at this time.
            //we just need the FullKey.
                if (orgLevel == OrgLevel.District)
                {
                    //Note from Bug 379:
                    //- State Summary link - should say District Summary at District level
                    //- Selected schools link - should say Selected Districts at district level 
                    this.linkSTYP_StateSummary.Text = "District&nbsp;Summary";
                    this.linkCompareTo_SelSchools.Text = "Selected&nbsp;Districts";

                    //Bug 597:  Current District Data
                    this.linkCompareTo_CurrentOnly.Text = "Current&nbsp;District&nbsp;Data";
                }
                else
                {
                    this.linkSTYP_StateSummary.Text = "State&nbsp;Summary";
                    this.linkCompareTo_SelSchools.Text = "Selected&nbsp;Schools";
                    if (orgLevel == OrgLevel.School)
                        this.linkCompareTo_CurrentOnly.Text = "Current&nbsp;School&nbsp;Data";
                    else
                        //state level
                        this.linkCompareTo_CurrentOnly.Text = "Current&nbsp;State&nbsp;Data";
                }
            
        }


        /// <summary>
        /// Shows/hides all of the table rows in the table, based on each row's property setting.
        /// </summary>
        private void HideTableRows(OrgLevel orgLevel)
        {
            //HideTableRow(tableRows.trSTYP, ShowTR_STYP);
            HideTableRow(tableRows.trShowMoney, ShowTR_ShowMoney);
            HideTableRow(tableRows.trShowRatioOfStaff, ShowTR_ShowRatioOfStaff);
            HideTableRow(tableRows.trCredential, ShowTR_Credential);
            HideTableRow(tableRows.trTypeCost, ShowTR_TypeCost);
            HideTableRow(tableRows.trGroup, ShowTR_Group);
            HideTableRow(tableRows.trCred, ShowTR_Cred);
            HideTableRow(tableRows.trShowGraphFile, ShowTR_ShowGraphFile);
            HideTableRow(tableRows.trShowGradReqs, ShowTR_GradReqs);
            HideTableRow(tableRows.trTQShow, ShowTR_TQShow);
            HideTableRow(tableRows.trPostGradShow, ShowTR_PostGradShow);
            HideTableRow(tableRows.trWMAS, ShowTR_WMAS);
            HideTableRow(tableRows.trTQSubjects, ShowTR_TQSubjects);
            HideTableRow(tableRows.trCompareTo, ShowTR_CompareTo);
            HideTableRow(tableRows.trACTSubjects, ShowTR_ACTSubjects);

            {
                //School Type: if Org Level == School, hide School Type.  Otherwise, show it.
                if (Page is APTestsPage)
                    HideTableRow(tableRows.trSTYP, false);  //Never show School Types for AP Tests page.
                else if (orgLevel == OrgLevel.School)
                    HideTableRow(tableRows.trSTYP, false);
                else
                    HideTableRow(tableRows.trSTYP, ShowTR_STYP);
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