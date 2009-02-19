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
    public partial class AfterExpell : PageBaseWI
    {
        protected v_ExpulsionServices _dsExpSrvc =
            new v_ExpulsionServices();
        protected BLExpulsionServices _blExpulsionServices = new BLExpulsionServices();

        private string graphTitle = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Master.EnableViewState = false;

            base.PrepBLEntity(_blExpulsionServices);
            ((SligoCS.Web.WI.WI)Page.Master).SetPageHeading(
                "What happens after students are expelled?");

            _dsExpSrvc = _blExpulsionServices.GetExpulsionServicesData();

            CheckSelectedSchoolOrDistrict(_blExpulsionServices);
            SetLinkChangeSelectedSchoolOrDistrict(
                _blExpulsionServices, ChangeSelectedSchoolOrDistrict);

            SetVisibleColumns2(SligoDataGrid2, _dsExpSrvc, _blExpulsionServices.ViewBy,
               _blExpulsionServices.OrgLevel, _blExpulsionServices.CompareTo, _blExpulsionServices.SchoolType);

            StickyParameter.SQL = _blExpulsionServices.SQL;

            this.SligoDataGrid2.DataSource = _dsExpSrvc;

            this.SligoDataGrid2.DataBind();


            graphTitle = GetTitle("Dropout Rate",
                              _blExpulsionServices,
                              GetRegionString());

            this.SligoDataGrid2.AddSuperHeader(graphTitle);

            this.SligoDataGrid2.ShowSuperHeader = true;

            if (base.StickyParameter.DETAIL != null &&
                    base.StickyParameter.DETAIL.ToString() == "NO")
            {
                this.SligoDataGrid2.Visible = false;
            }

            set_state();
            setBottomLink(_blExpulsionServices);

            //    GraphPanel.Visible = false;
            //    if ( CheckIfGraphPanelVisible(_Dropout, GraphPanel) == true)
            //    {
            //        SetUpChart(_ds);
            //    }
            }

            //private void SetUpChart(v_DropoutsWWoDisEconELPSchoolDistState ds)
            //{
            //    try
            //    {
            //        graphTitle = graphTitle.Replace("<br/>", "\n");
            //        barChart.Title = graphTitle;
            //        barChart.MaxRateInResult = GetMaxRateInResult(ds);

            //        if (_Dropout.ViewBy == ViewByGroup.AllStudentsFAY)
            //        {
            //            ArrayList friendlyAxisXName = new ArrayList();
            //            friendlyAxisXName.Add("All Students");
            //            barChart.FriendlyAxisXName = friendlyAxisXName;
            //        }

            //        barChart.AxisYDescription = "Dropout Rate";
            //        barChart.BLBase = _Dropout;
            //        barChart.DisplayColumnName = 
            //            ds._v_DropoutsWWoDisEconELPSchoolDistState.Drop_Out_RateColumn.ColumnName;
            //        barChart.RawDataSource = ds.Tables[0];
            //        barChart.DataBind();

            //    }
            //    catch (Exception ex)
            //    {
            //        System.Diagnostics.Debug.WriteLine(ex.Message);
            //    }
            //}


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
            public override List<string> GetVisibleColumns(ViewByGroup viewBy, OrgLevel orgLevel, CompareTo compareTo, SchoolType schoolType)
            {
                List<string> cols = base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);

                return cols;
            }
            protected void SligoDataGrid2_RowDataBound(Object sender, GridViewRowEventArgs e)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //decode the link to specific schools, so that it appears as a normal URL link.
                    int colIndex = SligoDataGrid2.FindBoundColumnIndex(
                        _dsExpSrvc.vExpulsionServices.LinkedDistrictNameColumn.ColumnName);
                    e.Row.Cells[colIndex].Text = Server.HtmlDecode(e.Row.Cells[colIndex].Text);

                   SetOrgLevelRowLabels(_blExpulsionServices, SligoDataGrid2, e.Row);
}
        }

        public override DataSet GetDataSet()
        {
            return _dsExpSrvc;
        }
    }
}
