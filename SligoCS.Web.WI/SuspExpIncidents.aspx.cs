
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

using SligoCS.Web.WI.WebSupportingClasses.WI;
using SligoCS.Web.WI.WebSupportingClasses;
using SligoCS.Web.WI.WebUserControls;

namespace SligoCS.Web.WI
{
    public partial class SuspExpIncidents : PageBaseWI
    {
        protected override NavViewByGroup InitNavRowGroups()
        {
            nlrVwByGroup.LinksEnabled = NavViewByGroup.EnableLinksVector.Disability;
            return nlrVwByGroup;
        }
        protected override void OnInitComplete(EventArgs e)
        {
            GlobalValues.LatestYear = 2010;
            if (GlobalValues.Group.Key == GroupKeys.Disability)
            {
                GlobalValues.TrendStartYear = 2003;
            }
            else
            {
                GlobalValues.TrendStartYear = 1997;
            }
            GlobalValues.Grade.Key = GradeKeys.Combined_PreK_12;

            //Don't show combined groups at District Level, until support is added in the data import.
            if (GlobalValues.OrgLevel.Key == OrgLevelKeys.District
                && QueryMarshaller.RaceDisagCodes.Contains((int)QueryMarshaller.RaceCodes.Comb))
                QueryMarshaller.RaceDisagCodes.Remove((int)QueryMarshaller.RaceCodes.Comb);

           base.OnInitComplete(e);
            // must come after base to allow global overrides to have effect:
           if (GlobalValues.STYP.Key == STYPKeys.AllTypes)
           {
               GlobalValues.CompareTo.Key = CompareToKeys.Current;
               nlrCompareTo.LinkRow.LinkControlAdded += new LinkControlAddedHandler(disableAllExceptCompareToCurrent_LinkControlAdded);
           }
        }
        void disableAllExceptCompareToCurrent_LinkControlAdded(HyperLinkPlus link)
        {
            if (link.ID != "linkCompareToCurrent") link.Enabled = false;
        }
        protected override ChartFX.WebForms.Chart InitGraph()
        {
            barChart.Visible = (GlobalValues.Incident.Key == IncidentKeys.Rate);
            hrzBarChart.Visible = !barChart.Visible;

            return (GlobalValues.Incident.Key == IncidentKeys.Rate) ? (ChartFX.WebForms.Chart)barChart : (ChartFX.WebForms.Chart)hrzBarChart;
        }
        protected override DALWIBase InitDatabase()
        {
            return new DALSuspExpIncidents();
        }
        protected override GridView InitDataGrid()
        {
            return SuspExpIncDataGrid;
        }
        protected override string SetPageHeading()
        {
            return "What types of incidents resulted in suspensions or expulsions?";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            String titlePrefix;

            if (GlobalValues.Weapon.Key == WeaponKeys.Yes || GlobalValues.SuperDownload.Key == SupDwnldKeys.True)
                titlePrefix = "Weapon/Drug Related Incidents \n Resulting in Suspension/Expulsion";
            else
                titlePrefix = "Incidents Not Related To Weapon/Drug \n Resulting in Suspension/Expulsion";

            DataSetTitle = GetTitle(titlePrefix);

            SuspExpIncDataGrid.AddSuperHeader(DataSetTitle);
          
            if (GlobalValues.Incident.Key == IncidentKeys.Consequences)
            {
                SetUpHorizChart(DataSet);
            }
            else
            {
                SetUpChart(DataSet);
            }
            set_state();
            setBottomLink();
        }
        private void SetUpChart(DataSet ds)
        {
            try
            {
                barChart.Title = DataSetTitle;
                barChart.AxisY.Step = 10;
                barChart.AxisYMin = 0;
                barChart.AxisYMax = 100;
                barChart.YAxisSuffix = String.Empty;

             //  if (GlobalValues.Group.Key == GroupKeys.All)
                //{
                    List<String> friendlyAxisXName = new List<String>();
                    friendlyAxisXName.Add("All Students");
                    barChart.FriendlyAxisXNames = friendlyAxisXName;
               // }
                barChart.AxisYDescription = "Incidents per 1,000 Students";
                //Bind Data Source & Display

                barChart.DisplayColumnName = v_SuspExpIncidentsWWoDisSchoolDistState.WeaponDrugIncidentRate;
                barChart.MaxRateInResult = GraphBarChart.GetMaxRateInColumn(ds.Tables[0], barChart.DisplayColumnName);
                //jdj: need to add column for nonweapondrug
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        public static DataTable TransferColumnsBetweenDS(DataTable source, DataTable dest, List<String> ColumnNames)
        {
            DataTable dsNew = new DataTable();

            //get the new columns
            foreach (String name in ColumnNames)
            {
                dsNew.Columns.Add(name, source.Columns[name].DataType);
            }
            //get the old columns
            foreach (DataColumn col in dest.Columns)
            {
                dsNew.Columns.Add(col.ColumnName, col.DataType);
            }

           // GraphBarChart.SimpleColumnCopy(dsNew, source, dest, ColumnNames);

            return dsNew;
        }
        private void SetUpHorizChart(DataSet ds)
        {
            try
            {
                if (GlobalValues.Group.Key == GroupKeys.Grade)
                {
                    //override the default orderby since will use GradeLabel, instead of GradeCode
                    List<String> order = new List<String>(QueryMarshaller.BuildOrderByList(DataSet.Tables[0].Columns).ToArray());
                    hrzBarChart.OrderBy = String.Join(",", order.ToArray());
                }
                else if (GlobalValues.Group.Key == GroupKeys.Race)
                {
                    List<String> grOrder = new List<string>(QueryMarshaller.BuildOrderByList(DataSet.Tables[0].Columns).ToArray());
                    grOrder.Insert(1, v_POST_GRAD_INTENT.Race);

                    hrzBarChart.OrderBy = String.Join(",", grOrder.ToArray());
                }

                hrzBarChart.YAxisSuffix = String.Empty;
                hrzBarChart.Title = DataSetTitle;

                if (GlobalValues.Group.Key == GroupKeys.Grade)
                    hrzBarChart.AutoHeightIncreaseFactor = 25;

                if (GlobalValues.CompareTo.Key == CompareToKeys.Current)
                {
                    hrzBarChart.LabelColumns.Add(
                        (GlobalValues.OrgLevel.Key == OrgLevelKeys.School)?
                        v_SuspExpIncidentsWWoDisSchoolDistState.School_Name
                        : v_SuspExpIncidentsWWoDisSchoolDistState.District_Name
                        );
                }
                else
                {
                    hrzBarChart.LabelColumns.Add(ColumnPicker.GetCompareToColumnName(GlobalValues));
                }

                if (GlobalValues.Group.Key != GroupKeys.All)
                {
                    hrzBarChart.LabelColumns.Add(ColumnPicker.GetViewByColumnName(GlobalValues));
                }

                if (GlobalValues.CompareTo.Key != CompareToKeys.SelDistricts
                    && GlobalValues.CompareTo.Key != CompareToKeys.SelSchools)
                {
                    hrzBarChart.SelectedSortBySecondarySort = false;
                }

                hrzBarChart.OverrideSeriesLabels = new Hashtable(6);

                //MEASURE COLUMNS:
                if (GlobalValues.Weapon.Key == WeaponKeys.Yes)
                {
                    hrzBarChart.MeasureColumns = new List<String>(new String[]
                    {
                       v_SuspExpIncidentsWWoDisSchoolDistState.PRCWeaponDrugAllSusp,
                       v_SuspExpIncidentsWWoDisSchoolDistState.PRCWeaponDrugExp
                      
                    });
                    hrzBarChart.OverrideSeriesLabels.Add(v_SuspExpIncidentsWWoDisSchoolDistState.PRCWeaponDrugAllSusp, "% Suspended");
                    hrzBarChart.OverrideSeriesLabels.Add(v_SuspExpIncidentsWWoDisSchoolDistState.PRCWeaponDrugExp, "% Expelled");
                }
                else
                {
                    hrzBarChart.MeasureColumns = new List<String>(new String[]
                    {
                       v_SuspExpIncidentsWWoDisSchoolDistState.PRCNonWeaponDrugAllSusp,
                       v_SuspExpIncidentsWWoDisSchoolDistState.PRCNonWeaponDrugExp
                    });
                    hrzBarChart.OverrideSeriesLabels.Add(v_SuspExpIncidentsWWoDisSchoolDistState.PRCNonWeaponDrugAllSusp, "% Suspended");
                    hrzBarChart.OverrideSeriesLabels.Add(v_SuspExpIncidentsWWoDisSchoolDistState.PRCNonWeaponDrugExp, "% Expelled");
                }

                hrzBarChart.AxisYDescription = "Percent of Incidents";
            }
            catch (Exception ex)
            {
                if (GlobalValues.TraceLevels > 0) throw ex;
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
      
        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                (WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state
                (WI.displayed_obj.dataLinksPanel, true);
        }

        private void setBottomLink()
        {
            BottomLinkViewProfile1.DistrictCd = GlobalValues.DistrictCode;
        }

        public override System.Collections.Generic.List<string> GetVisibleColumns(Group viewBy, OrgLevel orgLevel, CompareTo compareTo, STYP schoolType)
        {
            List<string> retval = base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);

            if (GlobalValues.SuperDownload.Key == SupDwnldKeys.True)
            {
                retval.Add(v_SuspExpIncidentsWWoDisSchoolDistState.enrollment);
                retval.Add(v_SuspExpIncidentsWWoDisSchoolDistState.IncidentCountTotalWeaponDrug);
                retval.Add(v_SuspExpIncidentsWWoDisSchoolDistState.IncidentCountTotalNonWeaponDrug);
                retval.Add(v_SuspExpIncidentsWWoDisSchoolDistState.PRCWeaponDrugAllSusp);
                retval.Add(v_SuspExpIncidentsWWoDisSchoolDistState.PRCWeaponDrugExp);
                retval.Add(v_SuspExpIncidentsWWoDisSchoolDistState.WeaponDrugIncidentRate);
                retval.Add(v_SuspExpIncidentsWWoDisSchoolDistState.PRCNonWeaponDrugAllSusp);
                retval.Add(v_SuspExpIncidentsWWoDisSchoolDistState.PRCNonWeaponDrugExp);
                retval.Add(v_SuspExpIncidentsWWoDisSchoolDistState.NonWeaponDrugIncidentRate);
                return retval;
            }

            retval.Add(v_SuspExpIncidentsWWoDisSchoolDistState.enrollment);
            
            if (GlobalValues.Incident.Key == IncidentKeys.Consequences)
            {
                if (GlobalValues.Weapon.Key == WeaponKeys.Yes)
                {
                    retval.Add(v_SuspExpIncidentsWWoDisSchoolDistState.IncidentCountTotalWeaponDrug);
                    retval.Add(v_SuspExpIncidentsWWoDisSchoolDistState.PRCWeaponDrugAllSusp);
                    retval.Add(v_SuspExpIncidentsWWoDisSchoolDistState.PRCWeaponDrugExp);
                }
                else
                {
                    retval.Add(v_SuspExpIncidentsWWoDisSchoolDistState.IncidentCountTotalNonWeaponDrug);
                    retval.Add(v_SuspExpIncidentsWWoDisSchoolDistState.PRCNonWeaponDrugAllSusp);
                    retval.Add(v_SuspExpIncidentsWWoDisSchoolDistState.PRCNonWeaponDrugExp);
                }
            }
            else
            {
                if (GlobalValues.Weapon.Key == WeaponKeys.Yes)
                {
                    retval.Add(v_SuspExpIncidentsWWoDisSchoolDistState.IncidentCountTotalWeaponDrug);
                    retval.Add(v_SuspExpIncidentsWWoDisSchoolDistState.WeaponDrugIncidentRate);
                }
                else
                {
                    retval.Add(v_SuspExpIncidentsWWoDisSchoolDistState.IncidentCountTotalNonWeaponDrug);
                    retval.Add(v_SuspExpIncidentsWWoDisSchoolDistState.NonWeaponDrugIncidentRate);
                }
            }

            return retval;
        }

        protected override System.Collections.Generic.List<string> GetDownloadRawVisibleColumns()
        {
            List<string> cols = base.GetDownloadRawVisibleColumns();
            if (GlobalValues.Incident.Key == IncidentKeys.Consequences || GlobalValues.SuperDownload.Key == SupDwnldKeys.True)
            {
                int index;
                if (GlobalValues.Weapon.Key == WeaponKeys.Yes || GlobalValues.SuperDownload.Key == SupDwnldKeys.True)
                {
                    index = cols.IndexOf(v_SuspExpIncidentsWWoDisSchoolDistState.PRCWeaponDrugAllSusp);
                    cols.Insert(index, v_SuspExpIncidentsWWoDisSchoolDistState.IncidentCountSuspWeaponDrug); 
                    index = cols.IndexOf(v_SuspExpIncidentsWWoDisSchoolDistState.PRCWeaponDrugExp);
                    cols.Insert(index, v_SuspExpIncidentsWWoDisSchoolDistState.IncidentCountExpWeaponDrug); 
                }
                
                if (GlobalValues.Weapon.Key == WeaponKeys.No || GlobalValues.SuperDownload.Key == SupDwnldKeys.True)
                {
                    index = cols.IndexOf(v_SuspExpIncidentsWWoDisSchoolDistState.PRCNonWeaponDrugAllSusp);
                    cols.Insert(index, v_SuspExpIncidentsWWoDisSchoolDistState.IncidentCountSuspNonWeaponDrug); 
                    index = cols.IndexOf(v_SuspExpIncidentsWWoDisSchoolDistState.PRCNonWeaponDrugExp);
                    cols.Insert(index, v_SuspExpIncidentsWWoDisSchoolDistState.IncidentCountExpNonWeaponDrug); 
                }
            }
            
            return cols; 
        }


        protected override SortedList<string, string> GetDownloadRawColumnLabelMapping()
        {
            SortedList<string, string> cols = base.GetDownloadRawColumnLabelMapping();
            cols.Add(v_SuspExpIncidentsWWoDisSchoolDistState.enrollment, "total_enrollment_prek_12");
            cols.Add(v_SuspExpIncidentsWWoDisSchoolDistState.PRCWeaponDrugAllSusp, "percent_suspended_weapon_drug");
            cols.Add(v_SuspExpIncidentsWWoDisSchoolDistState.IncidentCountSuspWeaponDrug, "number_suspended_weapon_drug"); 
            cols.Add(v_SuspExpIncidentsWWoDisSchoolDistState.PRCWeaponDrugExp, "percent_expelled_weapon_drug");
            cols.Add(v_SuspExpIncidentsWWoDisSchoolDistState.IncidentCountExpWeaponDrug, "number_expelled_weapon_drug"); 
            cols.Add(v_SuspExpIncidentsWWoDisSchoolDistState.PRCNonWeaponDrugAllSusp, "percent_suspended_non_weapon_drug");
            cols.Add(v_SuspExpIncidentsWWoDisSchoolDistState.IncidentCountSuspNonWeaponDrug, "number_suspended_non_weapon_drug"); 
            cols.Add(v_SuspExpIncidentsWWoDisSchoolDistState.PRCNonWeaponDrugExp, "percent_expelled_non_weapon_drug");
            cols.Add(v_SuspExpIncidentsWWoDisSchoolDistState.IncidentCountExpNonWeaponDrug, "number_expelled_non_weapon_drug"); 
            cols.Add(v_SuspExpIncidentsWWoDisSchoolDistState.IncidentCountTotalWeaponDrug, "number_of_incidents_resulting_in_suspension_expulsion_weapon_drug");
            cols.Add(v_SuspExpIncidentsWWoDisSchoolDistState.IncidentCountTotalNonWeaponDrug, "number_of_incidents_resulting_in_suspension_expulsion_non_weapon_drug");
            cols.Add(v_SuspExpIncidentsWWoDisSchoolDistState.WeaponDrugIncidentRate, "incidents_per_1000_students_weapon_drug");
            cols.Add(v_SuspExpIncidentsWWoDisSchoolDistState.NonWeaponDrugIncidentRate, "incidents_per_1000_students_non_weapon_drug");
            return cols;
        }
    }
}
