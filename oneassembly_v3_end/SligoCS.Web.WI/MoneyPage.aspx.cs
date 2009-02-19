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
using SligoCS.BLL.WI;
using SligoCS.Web.Base.PageBase.WI;
using SligoCS.Web.WI.HelperClasses;
using SligoCS.Web.Base.WebSupportingClasses.WI;
using StickyParamsEnum = 
        SligoCS.Web.Base.WebSupportingClasses.WI.StickyParameter.QStringVar;

namespace SligoCS.Web.WI
{
    public partial class MoneyPage : PageBaseWI
    {
        protected v_Revenues_2 _ds = new v_Revenues_2();
        protected v_Expend_2 _dsExp = new v_Expend_2();
        private StickyParameter stickyParameter = null;
        public MoneyPage()
        {
            StickyParameter = new StickyParameter();
            stickyParameter = StickyParameter;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Master.EnableViewState = false;

            //set the page heading
            ((SligoCS.Web.WI.WI)Page.Master).SetPageHeading(
                "How much money is received and spent in this district?");

            set_state(); 

            if (BLWIBase.GetOrgLevel(stickyParameter.ORGLEVEL)
                == OrgLevel.School )
            {
                //DataPanel.Visible = false;
                //NoDataForSchoolPanel.Visible = true;
                string qString =
                  ParamsHelper.GetQueryString ( stickyParameter,
                   StickyParamsEnum.ORGLEVEL.ToString(),
                   "District");
                qString = ParamsHelper.ReplaceQueryString ( qString,
                    StickyParamsEnum.FULLKEY.ToString(), 
                    BLWIBase.GetMaskedFullkey(StickyParameter.FULLKEY, OrgLevel.District));
                qString = ParamsHelper.ReplaceQueryString(qString,
                    "MR", "Yes");  //"MR" is for money page re-driected
                Response.Redirect("~/MoneyPage.aspx" + qString, true);
            }
            else
            {
                //NoDataForSchoolPanel.Visible = false;

                if (Request.QueryString["MR"] == "Yes") //"MR" is for money page re-driected
                {
                    this.DistrictDataProvided.Text = "District level data are provided. ";
                }
                else
                { 
                    this.DistrictDataProvided.Text = string.Empty;
                }

                //Notes for graph
           GraphPanel.Visible = false;
                barChart.AxisYMin = 0;
                barChart.AxisYMax = 20000;
                barChart.AxisYStep = 2000;
                ArrayList axisYName = new ArrayList();
                axisYName.Add("$0");
                for (int i = 1; i < 11; i++)
                {
                    axisYName.Add("$" + Convert.ToString(i * 2) + ",000");
                }
                barChart.FriendlyAxisYName = axisYName;
                barChart.Type = StackedType.Normal;

                    if (stickyParameter.RATIO == StickyParameter.RATIO_REVENUE)
                    {
                        SligoCS.BLL.WI.BLRevenue bl = new SligoCS.BLL.WI.BLRevenue();
                        ParamsLinkBox1.ShowTR_TypeCost = false;
                        base.PrepBLEntity(bl);

                        _ds = bl.GetRevenueData();

                        CheckSelectedSchoolOrDistrict(bl);
                        SetLinkChangeSelectedSchoolOrDistrict(bl, ChangeSelectedSchoolOrDistrict);

                        SetVisibleColumns2(SligoDataGrid2, _ds, bl.ViewBy, bl.OrgLevel,
                            bl.CompareTo, bl.SchoolType);
                        StickyParameter.SQL = bl.SQL;
                        this.SligoDataGrid2.DataSource = _ds;


                        string title = "Revenue Per Member <br/>" +
                            base.GetOrgName(bl.OrgLevel) + " (All School Types) " + "<br/>" +
                            base.GetYearRangeInTitle(bl.Years) +
                            base.GetCompareToInTitle(
                                bl.OrgLevel, bl.CompareTo,
                                bl.SchoolType, bl.S4orALL, GetRegionString());

                        this.SligoDataGrid2.AddSuperHeader(title);
                        setBottomLink(bl);

                        //Notes for graph
                        if (_ds.Tables[0].Rows.Count > 0)
                        {
                            GraphPanel.Visible = true;
                            barChart.Title = title.Replace("<br/>", "\n");
                            barChart.BLBase = bl;
                            barChart.AxisYDescription = "Revenue per Member";
                            barChart.AxisXDescription = "Revenue per Member";
                            barChart.DisplayColumnName = _ds._v_Revenues_2.Revenue_Per_MemberColumn.ColumnName;
                            barChart.RawDataSource = RemoveTotalPreGraph(_ds).Tables[0];
                            barChart.DataBind();
                        }

                    }
                    else if (stickyParameter.RATIO == StickyParameter.RATIO_EXPENDITURE)
                    {
                        SligoCS.BLL.WI.BLExpenditure bl = new SligoCS.BLL.WI.BLExpenditure();
                        base.PrepBLEntity(bl);
                        bl.Cost = StickyParameter.CT;
                        _dsExp = bl.GetExpenditureData();

                        CheckSelectedSchoolOrDistrict(bl);
                        SetLinkChangeSelectedSchoolOrDistrict(bl, ChangeSelectedSchoolOrDistrict);

                        SetVisibleColumns2(SligoDataGrid2, _dsExp, bl.ViewBy, bl.OrgLevel,
                             bl.CompareTo, bl.SchoolType);
                        StickyParameter.SQL = bl.SQL;
                        this.SligoDataGrid2.DataSource = _dsExp.Tables[0];

                        this.SligoDataGrid2.AddSuperHeader(getCostTitle(bl));
                        setBottomLink(bl);

                        //Notes for graph
                        if (_dsExp.Tables[0].Rows.Count > 0)
                        {
                            GraphPanel.Visible = true;
                            barChart.Title = getCostTitle(bl).Replace("<br/>", "\n");
                            barChart.BLBase = bl;
                            barChart.AxisYDescription = "Cost per Member";
                            barChart.AxisXDescription = "Cost per Member";
                            barChart.DisplayColumnName = _dsExp._v_Expend_2.Cost_Per_MemberColumn.ColumnName;
                            barChart.RawDataSource = RemoveTotalPreGraph(_dsExp).Tables[0];
                            barChart.DataBind();
                        }
                    }

                this.SligoDataGrid2.DataBind();
                this.SligoDataGrid2.ShowSuperHeader = true;
            }
        }

        //Notes For Graph
        private DataSet RemoveTotalPreGraph(DataSet ds)
        {
            DataSet dsReturn = ds.Clone();

            ds.Tables[0].Columns["Category"].ReadOnly = false;
            string condition = "";
            if (stickyParameter.RATIO == StickyParameter.RATIO_EXPENDITURE &&
                 StickyParameter.CT == "CE")
            {
                condition = "[Category] not like '%Current Education Cost%'";
            }
            else
            {
                condition = "[Category] not like '%Total%'";
            }
            DataRow[] rows = ds.Tables[0].Select(condition);
            DataRow[] newRows = new DataRow[rows.Length];
            rows.CopyTo(newRows, 0);
            foreach (DataRow row in newRows)
            {
                //Remove the space in the Column
                row["Category"] = row["Category"].ToString().Trim();
                dsReturn.Tables[0].ImportRow(row);
            }

            dsReturn.AcceptChanges();
            return dsReturn;
        }
        
        private void setBottomLink(BLWIBase baseBL)
        {
            BottomLinkViewReport1.Year = baseBL.CurrentYear.ToString();
            BottomLinkViewReport1.DistrictCd = GetDistrictCdByFullKey();
            BottomLinkViewReport1.SchoolCd = GetSchoolCdByFullKey();
            BottomLinkViewProfile1.DistrictCd = GetDistrictCdByFullKey();
        }

        private string getCostTitle( BLExpenditure bl )
        {

            string dataType = string.Empty;

            if (StickyParameter.CT == StickyParameter.COST_TOTAL_COST)
            {
                dataType = "Total Cost";
            }
            else if (StickyParameter.CT == StickyParameter.COST_TOTAL_EDUCATION_COST)
            {
                dataType = "Total Education Cost";
            }
            else if (StickyParameter.CT == StickyParameter.COST_CURRENT_EDUCATION_COST)
            {
                dataType = "Current Education Cost";
            }

            return dataType + " Per Member<br/>" +
                base.GetOrgName(bl.OrgLevel) + " (All School Types) " + "<br/>" +
                        base.GetYearRangeInTitle(bl.Years) +
                        base.GetCompareToInTitle(
                            bl.OrgLevel, bl.CompareTo,
                            bl.SchoolType, bl.S4orALL, GetRegionString());
        }

        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.dataLinksPanel, true);
        }

        public override List<string> GetVisibleColumns(ViewByGroup viewBy, OrgLevel orgLevel, CompareTo compareTo, SchoolType schoolType)
        {
            List<string> retval = new List<string>();//base.GetVisibleColumns(viewBy, orgLevel, compareTo, schoolType);

            if (stickyParameter.RATIO == StickyParameter.RATIO_REVENUE)
            {            
                if (compareTo == CompareTo.PRIORYEARS)
                {
                  retval.Add(_ds._v_Revenues_2.PriorYearColumn.ColumnName);
                }
                else
                {
                    retval.Add(_ds._v_Revenues_2.DistStateColumn.ColumnName);
                }  
                retval.Add(_ds._v_Revenues_2.CategoryColumn.ColumnName);
                retval.Add(_ds._v_Revenues_2.RevenueColumn.ColumnName);

                retval.Add(_ds._v_Revenues_2.Revenue_Per_MemberColumn.ColumnName);
                retval.Add(_ds._v_Revenues_2.Percent_of_TotalColumn.ColumnName);
            }
            else if (stickyParameter.RATIO == StickyParameter.RATIO_EXPENDITURE)
            {
                if (compareTo == CompareTo.PRIORYEARS)
                {
                    retval.Add(_dsExp._v_Expend_2.PriorYearColumn.ColumnName);
                }
                else
                {
                    retval.Add(_dsExp._v_Expend_2.DistStateColumn.ColumnName);
                }  
                
                retval.Add(_dsExp._v_Expend_2.CategoryColumn.ColumnName);
                retval.Add(_dsExp._v_Expend_2.CostColumn.ColumnName);

                retval.Add(_dsExp._v_Expend_2.Cost_Per_MemberColumn.ColumnName);
                retval.Add(_dsExp._v_Expend_2.Percent_of_TotalColumn.ColumnName);
            }
            return retval;
        }
        
        protected void SligoDataGrid2_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (stickyParameter.RATIO == StickyParameter.RATIO_REVENUE)
                {
                    int colIndex = SligoDataGrid2.FindBoundColumnIndex(
                        _ds._v_Revenues_2.LinkedNameColumn.ColumnName);
                    if (colIndex > -1)
                    {
                        e.Row.Cells[colIndex].Text = 
                            Server.HtmlDecode(e.Row.Cells[colIndex].Text);
                    }
                    SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _ds._v_Revenues_2.RevenueColumn.ColumnName, Constants.FORMAT_RATE_DOLLAR_0_NO_DOT);
                    SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _ds._v_Revenues_2.Revenue_Per_MemberColumn.ColumnName, Constants.FORMAT_RATE_DOLLAR_0_NO_DOT);
                    SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _ds._v_Revenues_2.Percent_of_TotalColumn.ColumnName, Constants.FORMAT_RATE_01_PERC);
                }
                else if (stickyParameter.RATIO == StickyParameter.RATIO_EXPENDITURE)
                {
                    int colIndex = SligoDataGrid2.FindBoundColumnIndex(
                        _dsExp._v_Expend_2.LinkedNameColumn.ColumnName);
                    if (colIndex > -1)
                    {
                        e.Row.Cells[colIndex].Text = Server.HtmlDecode
                            (e.Row.Cells[colIndex].Text);
                    }
                    SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _dsExp._v_Expend_2.CostColumn.ColumnName, Constants.FORMAT_RATE_DOLLAR_0_NO_DOT);
                    SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _dsExp._v_Expend_2.Cost_Per_MemberColumn.ColumnName, Constants.FORMAT_RATE_DOLLAR_0_NO_DOT);
                    SligoDataGrid2.SetCellToFormattedDecimal(e.Row, _dsExp._v_Expend_2.Percent_of_TotalColumn.ColumnName, Constants.FORMAT_RATE_01_PERC);

                }
            }
        }

        public override DataSet GetDataSet()
        {
            return _ds;
        }
    }
}
