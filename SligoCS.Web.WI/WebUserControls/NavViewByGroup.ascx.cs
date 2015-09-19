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

using SligoCS.Web.Base.PageBase.WI;//.PageBaseWI;
using SligoCS.Web.WI.WebSupportingClasses.WI;//.QueryStringUtils; .GlobalValues
using SligoCS.Web.Base.WebServerControls.WI;//.HyperLinkPlus
using SligoCS.BL.WI; //.OrgLevel

namespace SligoCS.Web.WI.WebUserControls
{
    public partial class NavViewByGroup : System.Web.UI.UserControl
    {
        private EnableLinksVector linksEnabled;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            LinksEnabled = NavViewByGroup.EnableLinksVector.Grade;
            LinkRow.LinkControlAdded += HandleLinkControlAdded;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void HandleLinkControlAdded(HyperLinkPlus link)
        {
           
            GlobalValues GlobalValues = ((PageBaseWI)Page).GlobalValues;

            link.Enabled = EnableByLinksEnabledFlag(link.ID);

            if (link.ID == "linkGroupMigrant" || link.ID == "linkGroupRaceGender")
                link.Visible = link.Enabled;
           
            //override for SchoolType
            if (GlobalValues.STYP.Key == STYPKeys.AllTypes
                && GlobalValues.CompareTo.Key != CompareToKeys.Current)
            {
                if (link.ID != "linkGroupAll")
                    link.Enabled = false;
            }
        }
        private Boolean EnableByLinksEnabledFlag(String id)
        {
            return
            (id == "linkGroupAll")
            || (id == "linkGroupGender"
                && LinksEnabled >= EnableLinksVector.Gender)
            || (id == "linkGroupRace"
                && LinksEnabled >= EnableLinksVector.Race)
            || (id == "linkGroupRaceGender"
                && AddRaceGender && LinksEnabled >= EnableLinksVector.RaceGender)
            || (id == "linkGroupGrade"
                && LinksEnabled >= EnableLinksVector.Grade)
            || (id == "linkGroupDisability"
                && LinksEnabled >= EnableLinksVector.Disability)
            || (id == "linkGroupEconDisadv"
                && LinksEnabled >= EnableLinksVector.EconElp)
            || (id == "linkGroupEngLangProf"
            && LinksEnabled >= EnableLinksVector.EconElp)
            || (id == "linkGroupMigrant"
            && LinksEnabled >= EnableLinksVector.Migrant)
            ;
        }
        
        public NavigationLinkRow LinkRow
        {
            get { return ViewByGroup_Links; }
        }
        public EnableLinksVector LinksEnabled
        {
            get { return linksEnabled; }
            set { linksEnabled = value; }// (EnableLinksVector)Enum.Parse(typeof(EnableLinksVector), value); }
        }
        private Boolean addRaceGender;

        public Boolean AddRaceGender
        {
            get { return addRaceGender; }
            set { addRaceGender = value; }
        }
	
        /// <summary>
        /// Flag for which links to enabled. Creates a precedent where all links up to and including the flag will be enabled, the remaining to be disabled, but still visible.
        /// </summary>
        public enum EnableLinksVector
        {
            AllStudents,
            Gender,
            Race,
            RaceGender,
            Grade,
            Disability,
            EconElp,
            Migrant
        }
    }
}