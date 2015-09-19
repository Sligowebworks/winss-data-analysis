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
using SligoCS.Web.WI.WebSupportingClasses;

using System.Text;
using SligoCS.Web.WI.WebSupportingClasses.WI;
using ChartFX.WebForms;

namespace SligoCS.Web.WI
{
    public partial class CoursesTaking : PageBaseWI
    {
        public String GetQueryString(String[] Params)
        {
            if (Params != null)
                return UserValues.GetQueryString(Params);
            else
                return UserValues.GetBaseQueryString();
        }

        private List<String> GradeCodesActive = new List<String>();
        protected override SligoCS.Web.WI.WebUserControls.NavViewByGroup InitNavRowGroups()
        {
            nlrVwByGroup.LinksEnabled = SligoCS.Web.WI.WebUserControls.NavViewByGroup.EnableLinksVector.Gender;
            return nlrVwByGroup;
        }
        protected override void OnInitComplete(EventArgs e)
        {
            setGradeCodeRange(GradeCodesActive);
            if (!GradeCodesActive.Contains(GlobalValues.Grade.Value) //invalid selection
                || !GlobalValues.inQS.Contains(GlobalValues.Grade.Name)) //user hasn't specified a grade
                GlobalValues.Grade.Key = GradeKeys.Grades_6_12_Combined;


            GlobalValues.TrendStartYear = 1997;
            GlobalValues.CurrentYear = 2010;

            //Disable School Level
            if (UserValues.OrgLevel.Key == OrgLevelKeys.School)
            {
                GlobalValues.OrgLevel.Value = GlobalValues.OrgLevel.Range[OrgLevelKeys.District];
                pnlMessage.Visible = true;
            }

            //STYP not supported
            GlobalValues.OverrideSchoolTypeWhenOrgLevelIsSchool_Complete += PageBaseWI.DisableSchoolType;

            base.OnInitComplete(e);
        }
        protected override DALWIBase InitDatabase()
        {
            return new DALCoursesTaken();
        }
        protected override GridView InitDataGrid()
        {
            return CoursesTakingDataGrid;
        }
        protected override Chart InitGraph()
        {
            return barGraph;
        }
        protected override string SetPageHeading()
        {
            return "What courses are students taking?";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            nlrGrade.LinkControlAdded += DisableGradeLinks;

            String[] title = new String[7];
            title[0] = "Enrollment in";
            title[1] = GlobalValues.CourseTypeID.Key;
            if (GlobalValues.CourseTypeID.Key != CourseTypeIDKeys.Other) 
                title[2] = "Courses";
            title[3] = TitleBuilder.newline;
            title[4] = GlobalValues.WMAS.Key;
            title[5] = TitleBuilder.GetGradeTitle();
            DataSetTitle = GetTitleForSchoolTypeUnsupported(String.Join(" ", title));
            CoursesTakingDataGrid.AddSuperHeader(DataSetTitle);

            List<String> order = new List<string>(QueryMarshaller.BuildOrderByList(DataSet.Tables[0].Columns).ToArray());
            order.Add(v_COURSEWORK.Course);

            CoursesTakingDataGrid.OrderBy = String.Join(",", order.ToArray());

            set_state();
            setBottomLink();
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
        public override List<string> GetVisibleColumns(Group viewBy, OrgLevel orgLevel, CompareTo compareTo, STYP schoolType)
        {
            List<string> cols = base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);

            cols.Add(v_COURSEWORK.enrollment);
            cols.Add(v_COURSEWORK.Course);
            cols.Add(v_COURSEWORK.NUM_Who_Took_Course);
           
            return cols;
        }
        protected override SortedList<string, string> GetDownloadRawColumnLabelMapping()
        {
            SortedList<string, string> cols = base.GetDownloadRawColumnLabelMapping();
            cols.Add(v_COURSEWORK.enrollment, "total_enrolled_in_grades");
            cols.Add(v_COURSEWORK.Course, "course_content");
            cols.Add(v_COURSEWORK.NUM_Who_Took_Course, "sum_of_students_taking_courses");
            return cols;
        }

        private void DisableGradeLinks(HyperLinkPlus link)
        {
            if (!GradeCodesActive.Contains(link.ParamValue))
                link.Enabled = false;
        }

        private void setGradeCodeRange(List<String> codes)
        {
            int n;
            // magic numbers come from DataBase
            for (n = 40; n <= 64; n=n+4)
            {
                if (n <= GlobalValues.HIGHGRADE 
                    && n >= GlobalValues.LOWGRADE)
                    codes.Add(n.ToString());
            }
            codes.Add("94"); //"Grade" 6-12
        }
    }
}
