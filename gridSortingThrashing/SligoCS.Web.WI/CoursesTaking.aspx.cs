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
using SligoCS.Web.WI.HelperClasses;
using System.Text;
using SligoCS.Web.WI.WebSupportingClasses.WI;
using ChartFX.WebForms;

namespace SligoCS.Web.WI
{
    public partial class CoursesTaking : PageBaseWI
    {
        protected v_Coursework _ds = new v_Coursework();
        protected BLCoursesTaken _courses;
        
        public CoursesTaking()
        {
            GlobalValues.Grade = 94;
        }

        private string TableTitle = string.Empty;

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
            _courses = new BLCoursesTaken();
            PrepBLEntity(_courses);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Master.EnableViewState = false;

            ((SligoCS.Web.WI.WI)Page.Master).SetPageHeading(
                "What courses are students taking?");

            _ds = _courses.getCourseworkQuery(GlobalValues.Grade, GlobalValues.CourseTypeID, GlobalValues.WMAS);
            
            CheckSelectedSchoolOrDistrict(_courses);
            SetLinkChangeSelectedSchoolOrDistrict(ChangeSelectedSchoolOrDistrict);
            
            SetVisibleColumns2(SligoDataGrid2, _ds, _courses.ViewBy,
               _courses.OrgLevel, _courses.CompareTo, GlobalValues.STYP);

            GlobalValues.SQL = _courses.SQL;

            this.SligoDataGrid2.DataSource = _ds;

            this.SligoDataGrid2.DataBind();


            TableTitle = GetTitle("Dropout Rate", _courses);

            this.SligoDataGrid2.AddSuperHeader(TableTitle);

            this.SligoDataGrid2.ShowSuperHeader = true;

            if (base.GlobalValues.DETAIL != null &&
                    base.GlobalValues.DETAIL.ToString() == "NO")
            {
                this.SligoDataGrid2.Visible = false;
            }

            set_state();
            setBottomLink(_courses);
        }


            private double GetMaxRateInResult(v_Coursework ds)
            {
                double maxRateInResult = 0;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    try
                    {
                        if (Convert.ToDouble(
                            // maybe need _column__Who_Took_Course
                                row[ds.v_COURSEWORK.___Who_Took_CourseColumn.ToString()]) > maxRateInResult)
                        {
                            maxRateInResult = Convert.ToDouble
                                    (row[ds.v_COURSEWORK.___Who_Took_CourseColumn.ColumnName].ToString());
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
                return maxRateInResult;
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
                        _ds.v_COURSEWORK.LinkedDistrictNameColumn.ColumnName);
                    e.Row.Cells[colIndex].Text = Server.HtmlDecode(e.Row.Cells[colIndex].Text);

                    SligoDataGrid2.SetOrgLevelRowLabels(GlobalValues, e.Row);
                }
        }
    }
}
