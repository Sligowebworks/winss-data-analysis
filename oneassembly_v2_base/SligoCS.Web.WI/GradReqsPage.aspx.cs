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

using SligoCS.Web.Base.WebSupportingClasses.WI;
using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;
using SligoCS.BL.WI;
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.WI.HelperClasses;

namespace SligoCS.Web.WI
{
    public partial class GradReqsPage : PageBaseWI
    {
        protected v_Grad_Reqs _ds;
        protected BLGrad_Reqs _gradreqs = new BLGrad_Reqs();
        protected string graphTitle = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Master.EnableViewState = false;

            base.PrepBLEntity(_gradreqs);

            //set the page heading
            ((SligoCS.Web.WI.WI)Page.Master).SetPageHeading("What are the requirements for high school graduation?");

            _ds = _gradreqs.GetGradReqs();

            CheckSelectedSchoolOrDistrict(_gradreqs);
            SetLinkChangeSelectedSchoolOrDistrict(_gradreqs, ChangeSelectedSchoolOrDistrict);

            SetVisibleColumns2(SligoDataGrid2, _ds, _gradreqs.ViewBy, _gradreqs.OrgLevel, _gradreqs.CompareTo, _gradreqs.SchoolType);
            StickyParameter.SQL = _gradreqs.SQL;

            this.SligoDataGrid2.DataSource = _ds;
            
            this.SligoDataGrid2.DataBind();
            this.SligoDataGrid2.AddSuperHeader(GetTitle("Graduation Requirements", _gradreqs));
            this.SligoDataGrid2.ShowSuperHeader = true;

            set_state();
        }

        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.dataLinksPanel, true);
        }


        protected override string GetTitle(string typeOfData, BLWIBase entity, string regionString)
        {
            string retval = base.GetTitle(typeOfData, entity, regionString);
            if ((StickyParameter != null) )
            {
                StickyParameter.CREDVALS cred = ((StickyParameter)StickyParameter).GetCredVals();
                if (cred == StickyParameter.CREDVALS.A)
                    retval = retval.Replace("All Students", "Additional Subjects");
                else
                    retval = retval.Replace("All Students", "Required Subjects");
            }
            return retval;

        }

        public override List<string> GetVisibleColumns(ViewByGroup viewBy, OrgLevel orgLevel, CompareTo compareTo, SchoolType schoolType)
        {
            List<string> retval =  base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);
            retval.Add(_ds._v_Grad_Reqs.PriorYearColumn.ColumnName);
            retval.Add(_ds._v_Grad_Reqs.SubjectColumn.ColumnName);
            retval.Add(_ds._v_Grad_Reqs.Credits_Required_by_DistrictColumn.ColumnName);
            retval.Add(_ds._v_Grad_Reqs.Credits_Required_by_StateColumn.ColumnName);
            retval.Add(_ds._v_Grad_Reqs.District_Requirements_Meet_or_Exceed_LawColumn.ColumnName);
            //retval.Add(_ds._v_Grad_Reqs.___of_Districts_Where_Credit_Requirements_Exceed_State_LawColumn.ColumnName);
            retval.Add(_ds._v_Grad_Reqs.____of_Districts_Where_Credit_Requirements_Exceed_State_LawColumn.ColumnName);

            return retval;
        }


        protected void SligoDataGrid2_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //decode the link to specific schools, so that it appears as a normal URL link.
                //int colIndex = SligoDataGrid2.FindBoundColumnIndex(_ds._v_Grad_Reqs.LinkedNameColumn.ColumnName);
                //e.Row.Cells[colIndex].Text = Server.HtmlDecode(e.Row.Cells[colIndex].Text);


                ////format the numerical values
                //SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _ds._v_Grad_Reqs._Enrollment_PreK_12Column.ColumnName, Constants.FORMAT_RATE_03);
                //SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _ds._v_Grad_Reqs.Actual_Days_Of_AttendanceColumn.ColumnName, Constants.FORMAT_RATE_03);
                //SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _ds._v_Grad_Reqs.Possible_Days_Of_AttendanceColumn.ColumnName, Constants.FORMAT_RATE_03);
                //SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _ds._v_Grad_Reqs.Attendance_RateColumn.ColumnName, Constants.FORMAT_RATE_01_PERC);


                //FormatHelper formater = new FormatHelper();
                //formater.SetRaceAbbr(SligoDataGrid2, e.Row, _ds._v_Grad_Reqs.RaceLabelColumn.ToString());

            }
        }


        public override DataSet GetDataSet()
        {
            return _ds;
        }

    }
}
