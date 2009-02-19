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

using SligoCS.BL.WI;
using SligoCS.DAL.WI;
using SligoCS.DAL.WI.DataSets;
using SligoCS.Web.WI.PageBase.WI;
using SligoCS.Web.WI.HelperClasses;
using SligoCS.Web.WI.WebServerControls.WI;


namespace SligoCS.Web.WI
{
    /// <summary>
    /// This page was created during a working meeting on Oct 11 2007.
    /// It is also known as the Dropouts page.
    /// </summary>
    public partial class Demo : PageBaseWI
    {

        protected v_DropoutsWWoDisEconELPSchoolDistState _ds = new v_DropoutsWWoDisEconELPSchoolDistState();
        v_DropoutsWWoDisEconELPSchoolDistState.v_DropoutsWWoDisEconELPSchoolDistStateDataTable _myTable = null;    

        private BLDropouts _dropouts = new BLDropouts();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Master.EnableViewState = false;

            base.PrepBLEntity(_dropouts);

            //set the page heading
            ((SligoCS.Web.WI.WI)Page.Master).SetPageHeading("What is the drop out rate?");

            _ds = _dropouts.GetDropoutData2();
            _myTable = _ds._v_DropoutsWWoDisEconELPSchoolDistState;

            StickyParameter.SQL = _dropouts.SQL;                       
            SligoDataGrid2.DataSource = _ds;
            SligoDataGrid2.DataBind();
            SetVisibleColumns2(SligoDataGrid2, _ds, _dropouts.ViewBy, _dropouts.OrgLevel, _dropouts.CompareTo, _dropouts.SchoolType);
            SligoDataGrid2.AddSuperHeader(base.GetTitle("Dropout Rates", _dropouts));

            set_state();
        }

        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.dataLinksPanel, true);
        }


        public override List<string> GetVisibleColumns(ViewByGroup viewBy, OrgLevel orgLevel, CompareTo compareTo, SchoolType schoolType)
        {
            List<string> cols = base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);
            
            if ((compareTo == CompareTo.SELSCHOOLS) || (compareTo == CompareTo.CURRENTONLY))
            {
                //When user selects "Compare To = Selected Schools" the first column in the grid
                //should show the school name, but with no header.                
                cols.Add(_ds._v_DropoutsWWoDisEconELPSchoolDistState.NameColumn.ColumnName);
            }

            cols.Add(_ds._v_DropoutsWWoDisEconELPSchoolDistState.EnrollmentColumn.ColumnName);
            cols.Add(_ds._v_DropoutsWWoDisEconELPSchoolDistState.Students_expected_to_complete_the_school_termColumn.ColumnName);
            cols.Add(_ds._v_DropoutsWWoDisEconELPSchoolDistState.Students_who_completed_the_school_termColumn.ColumnName);
            cols.Add(_ds._v_DropoutsWWoDisEconELPSchoolDistState.Drop_OutsColumn.ColumnName);
            cols.Add(_ds._v_DropoutsWWoDisEconELPSchoolDistState.Drop_Out_RateColumn.ColumnName);

            return cols;
        }
        


        //protected override void SetVisibleColumns2(SligoDataGrid grid, DataSet ds, ViewByGroup viewBy, OrgLevel orgLevel, CompareTo compareTo, SchoolType schoolType)
        //{
        //    base.SetVisibleColumns2(grid, ds, viewBy, orgLevel, compareTo, schoolType);

        //    //grid.SetBoundColumnVisible(_ds._v_DropoutsWWoDisEconELPSchoolDistState.District_NameColumn.ColumnName.Replace("_", " "), (compareTo == CompareTo.DISTSTATE));


        //    if ((compareTo == CompareTo.SELSCHOOLS) || (compareTo == CompareTo.CURRENTONLY))
        //    {
        //        //When user selects "Compare To = Selected Schools" the first column in the grid
        //        //should show the school name, but with no header.                
        //        SligoDataGrid2.SetBoundColumnVisible(_ds._v_DropoutsWWoDisEconELPSchoolDistState.NameColumn.ColumnName, true);
        //    }
        //    else
        //    {                
        //        SligoDataGrid2.SetBoundColumnVisible(_ds._v_DropoutsWWoDisEconELPSchoolDistState.NameColumn.ColumnName, false);
        //    }


        //}

        protected void SligoDataGrid2_RowDataBound(Object sender, GridViewRowEventArgs e)
        {            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                v_DropoutsWWoDisEconELPSchoolDistState.v_DropoutsWWoDisEconELPSchoolDistStateDataTable _myTable 
                    = _ds._v_DropoutsWWoDisEconELPSchoolDistState;

                //decode the link to specific schools, so that it appears as a normal URL link.
                int colIndex = SligoDataGrid2.FindBoundColumnIndex(_myTable.LinkedNameColumn.ColumnName);
                e.Row.Cells[colIndex].Text = Server.HtmlDecode(e.Row.Cells[colIndex].Text);

                
                SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _myTable.EnrollmentColumn.ColumnName, Constants.FORMAT_RATE_03);
                SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _myTable.Students_expected_to_complete_the_school_termColumn.ColumnName, Constants.FORMAT_RATE_03);
                SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _myTable.Students_who_completed_the_school_termColumn.ColumnName, Constants.FORMAT_RATE_03);
                SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _myTable.Drop_OutsColumn.ColumnName, Constants.FORMAT_RATE_03);
                SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _myTable.Drop_Out_RateColumn.ColumnName, Constants.FORMAT_RATE_01_PERC);

                SetOrgLevelRowLabels(_dropouts, SligoDataGrid2, e.Row);

                FormatHelper formater = new FormatHelper();
                formater.SetRaceAbbr(SligoDataGrid2, e.Row, _myTable.RaceLabelColumn.ToString());
                                    
            }
        }


        public override DataSet GetDataSet()
        {
            return _ds;
        }
        
        
    }
}
