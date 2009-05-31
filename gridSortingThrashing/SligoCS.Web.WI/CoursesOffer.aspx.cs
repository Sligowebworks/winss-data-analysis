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
using SligoCS.Web.WI.DAL.DataSets;
using SligoCS.BL.WI;
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.Base.WebServerControls.WI;
using SligoCS.Web.WI.HelperClasses;
using System.Text;
using SligoCS.Web.WI.WebSupportingClasses.WI;
using ChartFX.WebForms;

namespace SligoCS.Web.WI
{
    public partial class CoursesOffer : PageBaseWI
    {
        protected v_Course_Offerings _ds = new v_Course_Offerings();
        protected BLCourseOfferings _courseOfferings;

        private string TableTitle = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            nlrCourseTypeID.LinkControlAdded += CourseTypeIDLinkAdded;

            if (GlobalValues.CourseTypeID.Key == CourseTypeIDKeys.Other)
                GlobalValues.CourseTypeID.Value = QueryStringUtils.GetParamDefault(typeof(CourseTypeID).Name).ToString();
            
            Page.Master.EnableViewState = false;

            base.PrepBLEntity(_courseOfferings);

            ((SligoCS.Web.WI.WI)Page.Master).SetPageHeading(
                "What advanced courses are offered?");

            _ds = _courseOfferings.getCourseOfferingsQuery(GlobalValues.CourseTypeID, GlobalValues.WMAS);

            CheckSelectedSchoolOrDistrict(_courseOfferings);
            SetLinkChangeSelectedSchoolOrDistrict( ChangeSelectedSchoolOrDistrict);
            
            // no graph code needed

            // from Dropouts Page:
            //SetVisibleColumns2(SligoDataGrid2, _ds, _courseOfferings.ViewBy,  _courseOfferings.OrgLevel, _courseOfferings.CompareToEnum, _courseOfferings.STYP);
            // end from Dropouts page

            GlobalValues.SQL = _courseOfferings.SQL;

            this.SligoDataGrid2.DataSource = _ds;

            this.SligoDataGrid2.DataBind();

            TableTitle = GlobalValues.CourseTypeID.Key + " Courses";
            TableTitle = TableTitle.Replace(" Program&reg;", "");
            TableTitle += "<br />" + GlobalValues.WMAS.Key;
            TableTitle = "Advanced Course Offerings - " + TableTitle + "<br />";
            TableTitle = GetTitle(TableTitle, _courseOfferings);
            
            this.SligoDataGrid2.AddSuperHeader(TableTitle);

           if (base.GlobalValues.DETAIL != null &&
                    base.GlobalValues.DETAIL.ToString() == "NO")
            {
                this.SligoDataGrid2.Visible = false;
            }
            //testing:
            this.SligoDataGrid2.Visible = true;

            GetVisibleColumns(_courseOfferings.ViewBy, _courseOfferings.OrgLevel, _courseOfferings.CompareTo, GlobalValues.STYP);

            set_state();
            setBottomLink(_courseOfferings);
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
        private void setBottomLink(BLWIBase baseBL)
        {
            BottomLinkViewReport1.Year = baseBL.CurrentYear.ToString();
            BottomLinkViewReport1.DistrictCd = GetDistrictCdByFullKey();
            BottomLinkViewReport1.SchoolCd = GetSchoolCdByFullKey();
            BottomLinkViewProfile1.DistrictCd = GetDistrictCdByFullKey();
        }
        public override List<string> GetVisibleColumns(Group viewBy, OrgLevel orgLevel, CompareTo compareTo, STYP schoolType)
        {
            List<string> cols = base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);

            cols.Add("Course Content");

            foreach (string colName in cols)
            {
               SligoDataGrid2.SetBoundColumnVisible(colName, true);
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

                SligoDataGrid2.SetOrgLevelRowLabels(GlobalValues, e.Row);

            }
        }
        public override DataSet GetDataSet()
        {
            return _ds;
        }
    }
}
