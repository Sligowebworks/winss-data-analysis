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
using SligoCS.DAL.WI.DataSets;
using SligoCS.BL.WI;
using SligoCS.Web.WI.PageBase.WI;
using SligoCS.Web.WI.HelperClasses;
using System.Text;
using SligoCS.Web.WI.WebSupportingClasses.WI;
using ChartFX.WebForms;

namespace SligoCS.Web.WI
{
    public partial class CoursesOffer : PageBaseWI
    {
        protected v_Course_Offerings _ds = new v_Course_Offerings();
        protected BLCourseOfferings _courseOfferings = new BLCourseOfferings();

        public CoursesOffer()
        {
            StickyParameter.CourseTypeID = 1;
            StickyParameter.WMASID1 = 4;
        }


        private string TableTitle = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Master.EnableViewState = false;

            base.PrepBLEntity(_courseOfferings);

            ((SligoCS.Web.WI.WI)Page.Master).SetPageHeading(
                "What advanced courses are offered?");

            _ds = _courseOfferings.getCourseOfferingsQuery(StickyParameter.CourseTypeID, StickyParameter.WMASID1);

            CheckSelectedSchoolOrDistrict(_courseOfferings);
            SetLinkChangeSelectedSchoolOrDistrict(_courseOfferings, ChangeSelectedSchoolOrDistrict);
            
            // no graph code needed

            // from Dropouts Page:
            //SetVisibleColumns2(SligoDataGrid2, _ds, _courseOfferings.ViewBy,  _courseOfferings.OrgLevel, _courseOfferings.CompareTo, _courseOfferings.SchoolType);
            // end from Dropouts page

            StickyParameter.SQL = _courseOfferings.SQL;

            this.SligoDataGrid2.DataSource = _ds;

            this.SligoDataGrid2.DataBind();
            
            switch (StickyParameter.CourseTypeID)
            {
                case 1:
                    TableTitle = "Advanced Placement Courses";
                    break;
                case 2:
                    TableTitle = "CAPP Courses";
                    break;
                case 3:  // going away soon... no data to show
                    TableTitle = "DPI Defined Course";
                    break;
                case 6:
                    TableTitle = "International Baccalaureate Courses";
                    break;
            }

            switch (StickyParameter.WMASID1)
            {
                case 4:
                    TableTitle += "<br />English Language Arts";
                    break;
                case 8:
                    TableTitle +=  "<br />Mathematics";
                    break;
                case 10:
                    TableTitle += "<br />Science";
                    break;
                case 11:
                    TableTitle += "<br />Social Studies";
                    break;
                case 2:
                    TableTitle += "<br />Art and Design";
                    break;
                case 6:
                    TableTitle += "<br />World Languages";
                    break;
                case 9:
                    TableTitle += "<br />Music";
                    break;
                case 13:
                    TableTitle += "<br />Other Subjects";
                    break;
            }

            TableTitle = "Advanced Course Offerings - " + TableTitle + "<br />";

            TableTitle = GetTitle(TableTitle,
                              _courseOfferings,
                              GetRegionString());
            this.SligoDataGrid2.AddSuperHeader(TableTitle);

           if (base.StickyParameter.DETAIL != null &&
                    base.StickyParameter.DETAIL.ToString() == "NO")
            {
                this.SligoDataGrid2.Visible = false;
            }
            //testing:
            this.SligoDataGrid2.Visible = true;

            GetVisibleColumns(_courseOfferings.ViewBy, _courseOfferings.OrgLevel, _courseOfferings.CompareTo, _courseOfferings.SchoolType);

            set_state();
            setBottomLink(_courseOfferings);
        }
        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(
                    WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(
                        WI.displayed_obj.dataLinksPanel, true);
        }
        private void setBottomLink(BLWIBase baseBL)
        {
            BottomLinkViewReport1.Year = baseBL.CurrentYear.ToString();
            BottomLinkViewReport1.DistrictCd = GetDistrictCdByFullKey();
            BottomLinkViewReport1.SchoolCd = GetSchoolCdByFullKey();
            BottomLinkViewProfile1.DistrictCd = GetDistrictCdByFullKey();
        }
        public override List<string> GetVisibleColumns(ViewByGroup viewBy, OrgLevel orgLevel, CompareTo compareTo,  SchoolType schoolType)
        {
            List<string> cols = base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);

            cols.Add("Course Content");

            foreach (string colName in cols)
            {
               SligoDataGrid2. SetBoundColumnVisible(colName, true);
            }

            return cols;
        }

        protected void SligoDataGrid2_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //decode the link to specific schools, so that it appears as a normal URL link.
                int colIndex = SligoDataGrid2.FindBoundColumnIndex(
                    _ds.v_COURSE_OFFERINGS.LinkedDistrictNameColumn.ColumnName);

                e.Row.Cells[colIndex].Text = Server.HtmlDecode(e.Row.Cells[colIndex].Text);

                SetOrgLevelRowLabels(_courseOfferings, SligoDataGrid2, e.Row);

            }
        }
        public override DataSet GetDataSet()
        {
            return _ds;
        }
    }
}
