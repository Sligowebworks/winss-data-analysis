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
using SligoCS.DAL.WI.DataSets;
using SligoCS.Web.Base.PageBase.WI;

namespace SligoCS.Web.WI
{
    /// <summary>
    /// This page was created during a working meeting on Oct 11 2007.
    /// It is also known as the Dropouts page.
    /// </summary>
    public partial class Demo : PageBaseWI
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            BLDropouts dropouts = new BLDropouts();
            base.PrepBLEntity(dropouts);

            v_dropoutsWWoDisSchoolDistState ds = dropouts.GetDropoutData();

            SetVisibleColumns(ds, dropouts.OrgLevel, dropouts.CompareTo, dropouts.SchoolType);
            SetColumnHeaders(ds);
            SetMergeColumns(ds);
            SligoDataGrid1.DataTable = ds.v_DropoutsWWoDisSchoolDistState;
            SligoDataGrid1.DataGridTitle = "Dropouts";
            ParamsHelper.SQL = dropouts.SQL;

        }


        private void SetColumnHeaders(v_dropoutsWWoDisSchoolDistState ds)
        {
            ds.v_DropoutsWWoDisSchoolDistState.SchoolTypeLabelColumn.ExtendedProperties[Constants.DISPLAYNAME] = "School Type";
        }

        private void SetMergeColumns(v_dropoutsWWoDisSchoolDistState ds)
        {
            ds.v_DropoutsWWoDisSchoolDistState.SchoolTypeLabelColumn.ExtendedProperties[Constants.MERGECOLUMN] = true;
            ds.v_DropoutsWWoDisSchoolDistState.School_NameColumn.ExtendedProperties[Constants.MERGECOLUMN] = true;
        }

        /// <summary>
        /// SligoDataGrid now defaults to all columns hidden.  Set some  columns to visible.
        /// </summary>
        /// <param name="ds"></param>
        private void SetVisibleColumns(v_dropoutsWWoDisSchoolDistState ds, 
            OrgLevel orgLevel,
            CompareTo compareTo,
            SchoolType schoolType)
        {            

            ds.v_DropoutsWWoDisSchoolDistState.fullkeyColumn.ExtendedProperties[Constants.VISIBLECOLUMN] = true;
            ds.v_DropoutsWWoDisSchoolDistState.EnrollmentColumn.ExtendedProperties[Constants.VISIBLECOLUMN] = true;
            ds.v_DropoutsWWoDisSchoolDistState.Drop_Out_RateColumn.ExtendedProperties[Constants.VISIBLECOLUMN] = true;
            ds.v_DropoutsWWoDisSchoolDistState.School_NameColumn.ExtendedProperties[Constants.VISIBLECOLUMN] = true;

            //by default, set this column to FALSE
            ds.v_DropoutsWWoDisSchoolDistState.SchoolTypeLabelColumn.ExtendedProperties[Constants.VISIBLECOLUMN] = false;

            //Note that when "OrgLevel <> SC" and "Schooltype = 1 (all types)", we add
            //another column, SchooltypeLabel, to label the schooltype itemization...
            if ((orgLevel != OrgLevel.School) && (schoolType == SchoolType.AllTypes))
            {
                ds.v_DropoutsWWoDisSchoolDistState.SchoolTypeLabelColumn.ExtendedProperties[Constants.VISIBLECOLUMN] = true;
            }

        }
    }
}
