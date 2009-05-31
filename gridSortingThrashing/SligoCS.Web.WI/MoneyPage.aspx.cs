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
using SligoCS.Web.WI.WebSupportingClasses.WI;
using StickyParamsEnum = 
        SligoCS.Web.WI.WebSupportingClasses.WI.StickyParameter.QStringVar;

namespace SligoCS.Web.WI
{
    public partial class MoneyPage : PageBaseWI
    {
        protected v_Revenues_2 _ds = new v_Revenues_2();
        protected v_Expend_2 _dsExp = new v_Expend_2();
        protected BLRevenue _revenue;
        protected BLExpenditure _expenditure;

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
            _revenue = new BLRevenue();
            PrepBLEntity(_revenue);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Master.EnableViewState = false;

            //set the page heading
            ((SligoCS.Web.WI.WI)Page.Master).SetPageHeading(
                "How much money is received and spent in this district?");

            set_state();

            if (GlobalValues.OrgLevel == OrgLevelKeys.School)
            {
                OrgLevel distLevel = new OrgLevel();
                distLevel.Value = distLevel.Range[OrgLevelKeys.District];

                string qString =
                  GlobalValues.GetQueryString(StickyParamsEnum.OrgLevel.ToString(),
                   "District");
                qString = QueryStringUtils.ReplaceQueryString ( qString,
                    StickyParamsEnum.FULLKEY.ToString(),
                    FullKeyUtils.GetMaskedFullkey(GlobalValues.FULLKEY, distLevel));
                qString = QueryStringUtils.ReplaceQueryString(qString,
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

                    if (GlobalValues.Ratio.Key == RatioKeys.Revenue)
                    {
                        _ds = (v_Revenues_2)Database.DataSet;

                        CheckSelectedSchoolOrDistrict(_revenue);
                        SetLinkChangeSelectedSchoolOrDistrict(ChangeSelectedSchoolOrDistrict);

                        SetVisibleColumns2(SligoDataGrid2, _ds, _revenue.ViewBy, _revenue.OrgLevel,
                            _revenue.CompareTo, GlobalValues.STYP);
                        GlobalValues.SQL = _revenue.SQL;
                        this.SligoDataGrid2.DataSource = _ds;


                        string title = "Revenue Per Member <br/>" +
                            GlobalValues.GetOrgName() + " (All School Types) " + "<br/>" +
                            TitleBuilder.GetYearRangeInTitle(_revenue.Years) +
                            TitleBuilder.GetCompareToInTitle(
                                _revenue.OrgLevel, _revenue.CompareTo,
                                GlobalValues.STYP, _revenue.S4orALL, GetRegionString());

                        this.SligoDataGrid2.AddSuperHeader(title);
                        setBottomLink(_revenue);

                        //Notes for graph
                        if (_ds.Tables[0].Rows.Count > 0)
                        {
                            GraphPanel.Visible = true;
                            barChart.Title = title.Replace("<br/>", "\n");
                            barChart.AxisYDescription = "Revenue per Member";
                            barChart.AxisXDescription = "Revenue per Member";
                            barChart.DisplayColumnName = _ds._v_Revenues_2.Revenue_Per_MemberColumn.ColumnName;
                            barChart.RawDataSource = RemoveTotalPreGraph(_ds).Tables[0];
                            barChart.DataBind();
                        }

                    }
                    else if (GlobalValues.Ratio.Key == RatioKeys.Expenditure)
                    {
                        _expenditure = new SligoCS.BL.WI.BLExpenditure();
                        base.PrepBLEntity(_expenditure);
                        _expenditure.Cost = GlobalValues.CT;
                        _dsExp = _expenditure.GetExpenditureData();

                        CheckSelectedSchoolOrDistrict(_expenditure);
                        SetLinkChangeSelectedSchoolOrDistrict(ChangeSelectedSchoolOrDistrict);

                        SetVisibleColumns2(SligoDataGrid2, _dsExp, _expenditure.ViewBy, _expenditure.OrgLevel,
                             _expenditure.CompareTo, GlobalValues.STYP);
                        GlobalValues.SQL = _expenditure.SQL;
                        this.SligoDataGrid2.DataSource = _dsExp.Tables[0];

                        this.SligoDataGrid2.AddSuperHeader(getCostTitle(_expenditure));
                        setBottomLink(_expenditure);

                        //Notes for graph
                        if (_dsExp.Tables[0].Rows.Count > 0)
                        {
                            GraphPanel.Visible = true;
                            barChart.Title = getCostTitle(_expenditure).Replace("<br/>", "\n");
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
            if (GlobalValues.Ratio.Key == RatioKeys.Expenditure  &&
                 GlobalValues.CT.Key == CTKeys.CurrentEducation)
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
            //get parameter long name:
            string dataType = GlobalValues.CT.Key;

            return dataType + " Per Member<br/>" +
                GlobalValues.GetOrgName() + " (All School Types) " + "<br/>" +
                        TitleBuilder.GetYearRangeInTitle(bl.Years) +
                        TitleBuilder.GetCompareToInTitle(
                            bl.OrgLevel, bl.CompareTo,
                            GlobalValues.STYP, bl.S4orALL, GetRegionString());
        }

        private void set_state()
        {
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.Mixed_Header_Graphics1, true);
            ((SligoCS.Web.WI.WI)Page.Master).set_visible_state(WI.displayed_obj.dataLinksPanel, true);
        }

        public override List<string> GetVisibleColumns(Group viewBy, OrgLevel orgLevel, CompareTo compareTo, STYP schoolType)
        {
            List<string> retval = new List<string>();//base.GetVisibleColumns(viewBy, orgLevelKey, compareTo, schoolType);

            if (GlobalValues.Ratio.Key == RatioKeys.Revenue)
            {            
                if (compareTo.Key == CompareToKeys.Years)
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
            else if (GlobalValues.Ratio.Key == RatioKeys.Expenditure)
            {
                if (compareTo.Key == CompareToKeys.Years)
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
                if (GlobalValues.Ratio.Key == RatioKeys.Revenue)
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
                else if (GlobalValues.Ratio.Key == RatioKeys.Expenditure)
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
    }
}
