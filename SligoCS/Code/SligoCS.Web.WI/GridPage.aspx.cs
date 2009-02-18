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

using SligoCS.DAL.WI.DataSets;
using SligoCS.BL.WI;
using SligoCS.Web.Base.PageBase.WI;
using ChartFX;
 
namespace SligoCS.Web.WI
{

    /// <summary>
    /// This page was the initial demonstration of the SligoDataGrid.
    /// This page is also known as the Retention page.
    /// </summary>
    public partial class GridPage : PageBaseWI
    {
        protected void Page_Load(object sender, EventArgs e)
        {                        
            BLRetention retention = new BLRetention();

            base.PrepBLEntity(retention);


            
            v_RetentionWWoDisSchoolDistState ds = retention.GetRetentionData();
            SetVisibleColumns(ds, retention.ViewBy, retention.OrgLevel, retention.CompareTo, retention.SchoolType);
            SetColumnHeaders(ds);
            SetMergeColumns(ds);
            SetColumnOrder(ds, retention.CompareTo);
            SetColumnFormats(ds);

            ParamsHelper.SQL = retention.SQL;
            this.SligoDataGrid1.DataTable = ds._v_RetentionWWoDisSchoolDistState;
            this.SligoDataGrid1.DataGridTitle = "My Test Data";
            this.SligoWIGraph1.TestString = "graph control is here";
            /* ht to do for graph:
             To call some function that fills graph with data here
             Then databinding
             If Not IsPostBack Then
                    Page.DataBind()
                End If
             * Then display graph
            
            
            If this.m_strNodata = "False" Or this.m_strNodata = "" Then
                this.SligoWIGraph1.Visible = True   
                this.SligoWIGraph1.AppAdmin = MyBase.AppAdmin_MDK12
                this.SligoWIGraph1.DV2Display = this.dv
                this.SligoWIGraph1.m_strNoDataLabel= ""
           Else
                this.m_strNoDataLabel = this.GetStrNoDataLabel()
           End If   
             */

        }


        private void SetColumnOrder(v_RetentionWWoDisSchoolDistState ds, CompareTo compareTo)
        {
            if (compareTo == CompareTo.DISTSTATE)
                ds._v_RetentionWWoDisSchoolDistState.OrgLevelLabelColumn.SetOrdinal(0);
            else if ((compareTo == CompareTo.SELSCHOOLS) || (compareTo == CompareTo.CURRENTONLY))
                ds._v_RetentionWWoDisSchoolDistState.LinkedNameColumn.SetOrdinal(0);            
        }

        private void SetMergeColumns(v_RetentionWWoDisSchoolDistState ds)
        {
            ds._v_RetentionWWoDisSchoolDistState.YearFormattedColumn.ExtendedProperties[Constants.MERGECOLUMN] = true;
            ds._v_RetentionWWoDisSchoolDistState.RaceLabelColumn.ExtendedProperties[Constants.MERGECOLUMN] = true;
            ds._v_RetentionWWoDisSchoolDistState.SexLabelColumn.ExtendedProperties[Constants.MERGECOLUMN] = true;
            ds._v_RetentionWWoDisSchoolDistState.GradeLabelColumn.ExtendedProperties[Constants.MERGECOLUMN] = true;
            ds._v_RetentionWWoDisSchoolDistState.DisabilityLabelColumn.ExtendedProperties[Constants.MERGECOLUMN] = true;
            ds._v_RetentionWWoDisSchoolDistState.SchooltypeLabelColumn.ExtendedProperties[Constants.MERGECOLUMN] = true;
            ds._v_RetentionWWoDisSchoolDistState.OrgLevelLabelColumn.ExtendedProperties[Constants.MERGECOLUMN] = true;
            ds._v_RetentionWWoDisSchoolDistState.LinkedNameColumn.ExtendedProperties[Constants.MERGECOLUMN] = true;

        }


        private void SetColumnHeaders(v_RetentionWWoDisSchoolDistState ds)
        {
            //These columns should have NO header.
            ds._v_RetentionWWoDisSchoolDistState.YearFormattedColumn.ExtendedProperties[Constants.DISPLAYNAME] = string.Empty;
            ds._v_RetentionWWoDisSchoolDistState.RaceLabelColumn.ExtendedProperties[Constants.DISPLAYNAME] = string.Empty;
            ds._v_RetentionWWoDisSchoolDistState.SexLabelColumn.ExtendedProperties[Constants.DISPLAYNAME] = string.Empty;
            ds._v_RetentionWWoDisSchoolDistState.GradeLabelColumn.ExtendedProperties[Constants.DISPLAYNAME] = string.Empty;
            ds._v_RetentionWWoDisSchoolDistState.DisabilityLabelColumn.ExtendedProperties[Constants.DISPLAYNAME] = string.Empty;

            
            ds._v_RetentionWWoDisSchoolDistState.Completed_School_TermColumn.ExtendedProperties[Constants.DISPLAYNAME] = "Students who completed the school term";
            ds._v_RetentionWWoDisSchoolDistState.Number_of_RetentionsColumn.ExtendedProperties[Constants.DISPLAYNAME] = "Number of Retentions";
            ds._v_RetentionWWoDisSchoolDistState.Retention_RateColumn.ExtendedProperties[Constants.DISPLAYNAME] = "Retention Rate";
            ds._v_RetentionWWoDisSchoolDistState.SchooltypeLabelColumn.ExtendedProperties[Constants.DISPLAYNAME] = "School Type";

            ds._v_RetentionWWoDisSchoolDistState.OrgLevelLabelColumn.ExtendedProperties[Constants.DISPLAYNAME] = string.Empty;
            ds._v_RetentionWWoDisSchoolDistState.LinkedNameColumn.ExtendedProperties[Constants.DISPLAYNAME] = string.Empty;

        }


        private void SetColumnFormats(v_RetentionWWoDisSchoolDistState ds)
        {
            ds._v_RetentionWWoDisSchoolDistState._Total_Enrollment__K_12_Column.ExtendedProperties[Constants.FORMAT] = "0,000";
        }
        


        /// <summary>
        /// SligoDataGrid now defaults to hiding columns, so set some of them to visible here.
        /// </summary>
        private void SetVisibleColumns(v_RetentionWWoDisSchoolDistState ds, 
            ViewByGroup viewBy, 
            OrgLevel orgLevel, 
            CompareTo compareTo,
            SchoolType schoolType)
        {
            ds._v_RetentionWWoDisSchoolDistState._Total_Enrollment__K_12_Column.ExtendedProperties[Constants.VISIBLECOLUMN] = true;
            ds._v_RetentionWWoDisSchoolDistState.Completed_School_TermColumn.ExtendedProperties[Constants.VISIBLECOLUMN] = true;
            ds._v_RetentionWWoDisSchoolDistState.Number_of_RetentionsColumn.ExtendedProperties[Constants.VISIBLECOLUMN] = true;
            ds._v_RetentionWWoDisSchoolDistState.Retention_RateColumn.ExtendedProperties[Constants.VISIBLECOLUMN] = true;

            //by default, set these columns to NOT VISIBLE
            ds._v_RetentionWWoDisSchoolDistState.YearFormattedColumn.ExtendedProperties[Constants.VISIBLECOLUMN] = false;
            ds._v_RetentionWWoDisSchoolDistState.RaceLabelColumn.ExtendedProperties[Constants.VISIBLECOLUMN] = false;
            ds._v_RetentionWWoDisSchoolDistState.SexLabelColumn.ExtendedProperties[Constants.VISIBLECOLUMN] = false;
            ds._v_RetentionWWoDisSchoolDistState.GradeLabelColumn.ExtendedProperties[Constants.VISIBLECOLUMN] = false;
            ds._v_RetentionWWoDisSchoolDistState.DisabilityLabelColumn.ExtendedProperties[Constants.VISIBLECOLUMN] = false;
            ds._v_RetentionWWoDisSchoolDistState.SchooltypeLabelColumn.ExtendedProperties[Constants.VISIBLECOLUMN] = false;
            ds._v_RetentionWWoDisSchoolDistState.OrgLevelLabelColumn.ExtendedProperties[Constants.VISIBLECOLUMN] = false;
            ds._v_RetentionWWoDisSchoolDistState.LinkedNameColumn.ExtendedProperties[Constants.VISIBLECOLUMN] = false;

            //based on what View By the user chose, set one of the columns to Visible.
            if (viewBy == ViewByGroup.RaceEthnicity)
                ds._v_RetentionWWoDisSchoolDistState.RaceLabelColumn.ExtendedProperties[Constants.VISIBLECOLUMN] = true;
            else if (viewBy == ViewByGroup.Gender)
                ds._v_RetentionWWoDisSchoolDistState.SexLabelColumn.ExtendedProperties[Constants.VISIBLECOLUMN] = true;
            else if (viewBy == ViewByGroup.Grade)
                ds._v_RetentionWWoDisSchoolDistState.GradeLabelColumn.ExtendedProperties[Constants.VISIBLECOLUMN] = true;
            else if (viewBy == ViewByGroup.Disability)
                ds._v_RetentionWWoDisSchoolDistState.DisabilityLabelColumn.ExtendedProperties[Constants.VISIBLECOLUMN] = true;            

            //Note that when "OrgLevel <> SC" and "Schooltype = 1 (all types)", we add
            //another column, SchooltypeLabel, to label the schooltype itemization...
            if ((orgLevel != OrgLevel.School) && (schoolType == SchoolType.AllTypes))
            {                
                ds._v_RetentionWWoDisSchoolDistState.SchooltypeLabelColumn.ExtendedProperties[Constants.VISIBLECOLUMN] = true;
            }


            if (compareTo == CompareTo.DISTSTATE)
                ds._v_RetentionWWoDisSchoolDistState.OrgLevelLabelColumn.ExtendedProperties[Constants.VISIBLECOLUMN] = true;
            else if ((compareTo == CompareTo.SELSCHOOLS) || (compareTo == CompareTo.CURRENTONLY))
                ds._v_RetentionWWoDisSchoolDistState.LinkedNameColumn.ExtendedProperties[Constants.VISIBLECOLUMN] = true;


            //only show Year column when CompareTo == Prior Years
            if (compareTo == CompareTo.PRIORYEARS)
                ds._v_RetentionWWoDisSchoolDistState.YearFormattedColumn.ExtendedProperties[Constants.VISIBLECOLUMN] = true;


        }

    }
}
