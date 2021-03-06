using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SligoCS.DAL.WI;
using SligoCS.BL.WI;
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.Base.WebServerControls.WI;

using System.Text;
using SligoCS.Web.WI.WebSupportingClasses.WI;
using SligoCS.Web.WI.WebSupportingClasses;
using ChartFX.WebForms;

namespace SligoCS.Web.WI
{
    public partial class CoursesOffer : PageBaseWI
    {
        public String GetQueryString(String[] Params)
        {
            if (Params != null)
                return UserValues.GetQueryString(Params);
            else
                return UserValues.GetBaseQueryString();
        }
        protected override DALWIBase InitDatabase()
        {
            return new DALCourseOfferings();
        }
        protected override GridView InitDataGrid()
        {
            return CourseOfferDataGrid;
        }
        protected override void OnInitComplete(EventArgs e)
        {
            GlobalValues.TrendStartYear = 1997;
            GlobalValues.CurrentYear = 2010;

            //Disable CourseType, Other
            if (GlobalValues.CourseTypeID.Key == CourseTypeIDKeys.Other)
                GlobalValues.CourseTypeID.Value = (String)GlobalValues.GetParamDefault(GlobalValues.CourseTypeID.Name);

            //Disable School Level
            if (UserValues.OrgLevel.Key == OrgLevelKeys.School)
            {
                GlobalValues.OrgLevel.Value = GlobalValues.OrgLevel.Range[OrgLevelKeys.District];
                pnlMessage.Visible = true;
            }

            //Disable Compare To State
            if (UserValues.CompareTo.Key == CompareToKeys.OrgLevel)
            {
                GlobalValues.CompareTo.Value = GlobalValues.CompareTo.Range[CompareToKeys.Current];
            }
            nlrCompareTo.LinkRow.LinkControlAdded += new LinkControlAddedHandler(LinkRow_LinkControlAdded);

            //STYP not supported
            GlobalValues.OverrideSchoolTypeWhenOrgLevelIsSchool_Complete += PageBaseWI.DisableSchoolType;

            //View By Group Not Supported
            GlobalValues.Group.Value = GlobalValues.Group.Range[GroupKeys.All];

            GlobalValues.OverrideByNavLinksNotPresent(GlobalValues.WMAS, nlrSubject, WMASKeys.Other);

            base.OnInitComplete(e);
        }

        void LinkRow_LinkControlAdded(HyperLinkPlus link)
        {
            if (link.ID == "linkCompareToOrgLevel") link.Enabled = false;
        }
        protected override string SetPageHeading()
        {
            return "What advanced courses are offered?";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            nlrCourseTypeID.LinkControlAdded += CourseTypeIDLinkAdded;

            // no graph code needed

            StringBuilder title = new StringBuilder();

            title.Append("Advanced Course Offerings - ");
            title.Append( GlobalValues.CourseTypeID.Key + " Courses");
            title.Append(TitleBuilder.newline + GlobalValues.WMAS.Key );
            DataSetTitle = GetTitleWithoutGroupForSchoolTypeUnsupported(title.ToString());
            CourseOfferDataGrid.AddSuperHeader(DataSetTitle);

            List<String> order = new List<string>(QueryMarshaller.BuildOrderByList(DataSet.Tables[0].Columns).ToArray());
            order.Add(v_COURSE_OFFERINGS.Course);

            CourseOfferDataGrid.OrderBy = String.Join(",", order.ToArray());

            set_state();
            setBottomLink();
        }

        private void CourseTypeIDLinkAdded(HyperLinkPlus link)
        {
            if (link.ID == "linkCourseTypeIDOther")
            {
                link.Visible = false;
            }
        }

        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(
                    WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(
                        WI.displayed_obj.dataLinksPanel, true);
        }
        private void setBottomLink()
        {
            BottomLinkViewProfile1.DistrictCd = GlobalValues.DistrictCode;
        }
        public override List<string> GetVisibleColumns()
        {
            List<string> cols = base.GetVisibleColumns();

            cols.Add(v_COURSE_OFFERINGS.Course);
            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.State)
                   cols.Add(v_COURSE_OFFERINGS.Districts_Offering_At_Least_One_Course);
            else
                cols.Add(v_COURSE_OFFERINGS.Offerings);

            if (cols.Contains(ColumnPicker.CommonNames.SexLabel.ToString()))
                cols.Remove(ColumnPicker.CommonNames.SexLabel.ToString());
            if (cols.Contains(ColumnPicker.CommonNames.RaceLabel.ToString()))
                cols.Remove(ColumnPicker.CommonNames.RaceLabel.ToString());
            if (cols.Contains(ColumnPicker.CommonNames.DisabilityLabel.ToString()))
                cols.Remove(ColumnPicker.CommonNames.DisabilityLabel.ToString());
            
            return cols;
        }
        public override List<string> GetDownloadRawVisibleColumns()
        {
            List<string> visibleColumns = base.GetDownloadRawVisibleColumns();

            if (visibleColumns.Contains(ColumnPicker.CommonNames.SexLabel.ToString()))
                visibleColumns.Remove(ColumnPicker.CommonNames.SexLabel.ToString());
            if (visibleColumns.Contains(ColumnPicker.CommonNames.RaceLabel.ToString()))
                visibleColumns.Remove(ColumnPicker.CommonNames.RaceLabel.ToString());
            if (visibleColumns.Contains(ColumnPicker.CommonNames.DisabilityLabel.ToString()))
                visibleColumns.Remove(ColumnPicker.CommonNames.DisabilityLabel.ToString());
            
            int idx = visibleColumns.IndexOf(v_COURSE_OFFERINGS.Course);
            visibleColumns.Insert(idx, v_COURSE_OFFERINGS.CourseType);
            visibleColumns.Insert(idx, v_COURSE_OFFERINGS.WMAS_Description1);

            if (!visibleColumns.Contains(v_COURSE_OFFERINGS.Districts_Offering_At_Least_One_Course))
                visibleColumns.Add(v_COURSE_OFFERINGS.Districts_Offering_At_Least_One_Course);
            if (!visibleColumns.Contains(v_COURSE_OFFERINGS.Offerings))
                visibleColumns.Add(v_COURSE_OFFERINGS.Offerings);

            return visibleColumns;
        }
        protected override SortedList<string, string> GetDownloadRawColumnLabelMapping()
        {
            SortedList<string, string> cols = base.GetDownloadRawColumnLabelMapping();
            cols.Add(v_COURSE_OFFERINGS.Course, "course_content");

            cols.Add(v_COURSE_OFFERINGS.CourseType, "course_type");
            cols.Add(v_COURSE_OFFERINGS.WMAS_Description1, "subject");

            cols.Add(v_COURSE_OFFERINGS.Districts_Offering_At_Least_One_Course, "number_of_districts_offering_at_least_one_course");
            cols.Add(v_COURSE_OFFERINGS.Offerings, "number_of_different_courses_offered");

            return cols;
        }
    }
}
