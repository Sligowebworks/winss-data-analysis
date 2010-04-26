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

using System.Text;
using SligoCS.Web.WI.WebSupportingClasses.WI;
using ChartFX.WebForms;

namespace SligoCS.Web.WI
{
    public partial class ActivitiesParticipate : PageBaseWI
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
            return new DALActivities();
        }
        protected override GridView InitDataGrid()
        {
            return ActivitiesDataGrid;
        }
        protected override Chart InitGraph()
        {
            return barChart;
        }
        protected override void OnInitComplete(EventArgs e)
        {
            //View By Group Not Supported
            GlobalValues.Group.Value = GlobalValues.Group.Range[GroupKeys.All];

            GlobalValues.TrendStartYear = 1997;
            GlobalValues.Year = 2009;

            if (GlobalValues.Show.Key == ShowKeys.Community)
            {
                GlobalValues.Grade.Key = GradeKeys.Grades_9_12_Combined;
            }
            else //ShowKeys.Extracurricular
            {
                GlobalValues.Grade.Key = GradeKeys.Grades_6_12_Combined;
            }
            //Disable SchoolType All Types:
            if (GlobalValues.CompareTo.Key != CompareToKeys.Current)
            {
                if (GlobalValues.STYP.Key == STYPKeys.AllTypes) GlobalValues.STYP.Value = GlobalValues.STYP.Range[STYPKeys.StateSummary];
                nlrSchoolType.LinkRow.LinkControlAdded += new LinkControlAddedHandler(LinkRow_LinkControlAdded);
            }

            base.OnInitComplete(e);
        }

        void LinkRow_LinkControlAdded(SligoCS.Web.Base.WebServerControls.WI.HyperLinkPlus link)
        {
            if (link.ID == "linkSTYP_ALLTypes") link.Enabled = false;
        }
        protected override string SetPageHeading()
        {
            return "Do students participate in school-supported activities?";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSetTitle = (GlobalValues.Show.Key == ShowKeys.Community)
           ? GetTitleWithoutGroup("School -Sponsored Community Activities")
            : GetTitleWithoutGroup("Extra-Co-Curricular Activities");

            ActivitiesDataGrid.AddSuperHeader(DataSetTitle);
            List<String> order = QueryMarshaller.BuildOrderByList(DataSet.Tables[0].Columns);
            if (GlobalValues.STYP.Key == STYPKeys.AllTypes && GlobalValues.CompareTo.Key == CompareToKeys.Current)
            {
                order.Remove(v_ActivitiesSchoolDistState.LinkedName);
                order.Insert(0, v_ActivitiesSchoolDistState.SchooltypeLabel);
            }
            order.Insert(1, v_ActivitiesSchoolDistState.ActivityLabel);
            ActivitiesDataGrid.OrderBy = String.Join(",", order.ToArray());

            SetUpChart(DataSetTitle);

            set_state();

            setBottomLink();
        }

        private void SetUpChart(String title)
        {
            barChart.Title = title.Replace("<br/>", "\n");

            barChart.AxisYDescription = "Participation Rate";
            barChart.DisplayColumnName = v_ActivitiesSchoolDistState.Participation_Rate;

            barChart.MaxRateInResult = WebUserControls.GraphBarChart.GetMaxRateInColumn(DataSet.Tables[0], barChart.DisplayColumnName);

            barChart.LabelColumnName = v_ActivitiesSchoolDistState.ActivityLabel;
        }

        //jdj: testing
        /*
      private void SetUpChart(v_DropoutsWWoDisEconELPSchoolDistState ds)
      {
          try
          {
              graphTitle = graphTitle.Replace("<br/>", "\n");
            
              barChart.Title = graphTitle;
                
              barChart.MaxRateInResult = GetMaxRateInResult(ds);

              if (_Dropout.ViewBy == ViewByGroup.AllStudentsFAY)
              {
                  ArrayList friendlyAxisXName = new ArrayList();
                
                  friendlyAxisXName.Add("All Students");
                    
                  barChart.FriendlyAxisXName = friendlyAxisXName;
              }

              barChart.AxisYDescription = "Offerings Per School (Average)";
                
              barChart.BLBase = _Dropout;
                
              barChart.DisplayColumnName =
                  ds._v_DropoutsWWoDisEconELPSchoolDistState.Drop_Out_RateColumn.ColumnName;
    
              barChart.RawDataSource = ds.Tables[0];
                
              barChart.DataBind();

          }
          catch (Exception ex)
          {
              System.Diagnostics.Debug.WriteLine(ex.Message);
          }
      }


      private double GetMaxRateInResult(v_DropoutsWWoDisEconELPSchoolDistState ds)
      {
          double maxRateInResult = 0;

          foreach (DataRow row in ds.Tables[0].Rows)
          {
              try
              {
                  if (Convert.ToDouble(
                          row[ds._v_DropoutsWWoDisEconELPSchoolDistState.
                              Drop_Out_RateColumn.ColumnName].ToString())
                                      > maxRateInResult)
                  {
                      maxRateInResult = Convert.ToDouble
                              (row[ds._v_DropoutsWWoDisEconELPSchoolDistState.
                                  Drop_Out_RateColumn.ColumnName].ToString());
                  }
              }
              catch
              {
                  continue;
              }
          }
          return maxRateInResult;
      }

      */

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

            cols.Add(v_ActivitiesSchoolDistState.ActivityLabel);

            if (GlobalValues.Show.Key == ShowKeys.Community)
                cols.Add(v_ActivitiesSchoolDistState.Enrollment_Grades_912);

            if (GlobalValues.Show.Key == ShowKeys.Extracurricular)
                cols.Add(v_ActivitiesSchoolDistState.Enrollment_Grades_612);

            cols.Add(v_ActivitiesSchoolDistState.Pupils_Participating);
            cols.Add(v_ActivitiesSchoolDistState.Participation_Rate);

            if (GlobalValues.STYP.Key == STYPKeys.AllTypes && GlobalValues.CompareTo.Key == CompareToKeys.Current) cols.Remove(v_ActivitiesSchoolDistState.LinkedName);

            return cols;
        }
        protected override SortedList<string, string> GetDownloadRawColumnLabelMapping()
        {
            SortedList<string, string> newLabels = base.GetDownloadRawColumnLabelMapping();
            newLabels.Add(v_ActivitiesSchoolDistState.ActivityLabel, "activity_type");
            return newLabels;
        }

    }
}
