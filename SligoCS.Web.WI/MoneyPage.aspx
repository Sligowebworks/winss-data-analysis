<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="MoneyPage.aspx.cs" Inherits="SligoCS.Web.WI.MoneyPage" Title="Money" %>

<%@ Register Src="WebUserControls/BottomLinks.ascx" TagName="BottomLinks" TagPrefix="uc1" %>
<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>
<%@ Register Src="~/WebUserControls/NavigationLinkRow.ascx"  TagName="NavigationLinkRow" TagPrefix="sli"%>
<%@ Register Src="~/WebUserControls/NavCompareTo.ascx" TagName="NavCompareTo" TagPrefix="sli" %>

<%@ Register Src="WebUserControls/BottomLinkMoreData.ascx" TagName="BottomLinkMoreData" TagPrefix="uc5" %>
<%@ Register Src="WebUserControls/BottomLinkViewReport.ascx" TagName="BottomLinkViewReport" TagPrefix="uc6" %>
<%@ Register Src="WebUserControls/BottomLinkViewProfile.ascx" TagName="BottomLinkViewProfile" TagPrefix="uc7" %>
<%@ Register Src="WebUserControls/BottomLinkDownload.ascx" TagName="BottomLinkDownload" TagPrefix="uc8" %>
<%@ Register Src="WebUserControls/BottomLinkWhatToConsider.ascx" TagName="BottomLinkWhatToConsider" TagPrefix="uc9" %>
<%@ Register Src="WebUserControls/BottomLinkEnrollmentCounts.ascx" TagName="BottomLinkEnrollmentCounts" TagPrefix="uc10" %>
<%@ Register Assembly="ChartFX.WebForms" Namespace="ChartFX.WebForms" TagPrefix="chartfx7" %>
<%@ Register Assembly="ChartFX.WebForms.Adornments" Namespace="ChartFX.WebForms.Adornments"
    TagPrefix="chartfxadornments" %>
<%@ Register Assembly="ChartFX.WebForms" Namespace="ChartFX.WebForms.Galleries" TagPrefix="chartfx7galleries" %>

<%@ Register tagPrefix="Graph" namespace="SligoCS.Web.WI.WebUserControls" Assembly="SligoCS.Web.WI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>

<asp:Panel ID="DataPanel" runat="server">
    <tr>
        <td>
            <sli:NavSelectYear ID="nlrSelectYear" runat="server" /> 
             <sli:NavigationLinkRow ID="nlrShow" runat="server">
                <RowLabel>Show:</RowLabel>
                <NavigationLinks>
                    <cc1:HyperLinkPlus ID="linkRevExpRevenue" runat="server" ParamName="RevExp" ParamValue="2">Revenue Per Member</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkRevExpExpenditure" runat="server" ParamName="RevExp" ParamValue="4">Cost Per Member</cc1:HyperLinkPlus>
                  </NavigationLinks>
                </sli:NavigationLinkRow>
                <!-- not visible except when Show = Cost -->
                <sli:NavigationLinkRow ID="nlrType" runat="server">
                    <RowLabel>Type:</RowLabel>
                    <NavigationLinks> 
                        <cc1:HyperLinkPlus ID="linkCTCost" runat="server" ParamName="CT" ParamValue="TC">Total Cost</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkCTTotalEducation" runat="server" ParamName="CT" ParamValue="TE">Total Education Cost</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkCTCurrentEducation" runat="server" ParamName="CT" ParamValue="CE">Current Education Cost</cc1:HyperLinkPlus>

                    </NavigationLinks>
                </sli:NavigationLinkRow>
                
                <sli:NavCompareTo ID="nlrCompareTo" runat="server" />
                <sli:ChangeSelectedSchoolOrDistrictLink id="ChangeSelectedSchoolOrDistrictLink1" runat="server" />
                
        </td>
    </tr>
    <tr>
        <td>
            <asp:hyperlink ID="ChangeSelectedSchoolOrDistrict" Font-Size="Small" runat="server" />
        </td>
    </tr> 
    <asp:Panel ID="GraphPanel" runat="server">       
    <tr>
        <td><!--Chart goes here.-->
        <Graph:GraphBarChart ID="barChart" runat="server"></Graph:GraphBarChart>
        </td>
    </tr>
    </asp:Panel>    

    <asp:Panel ID="pnlMessage" runat="server" Visible="false">
    <tr>
        <td>School level data are not available.</td>
    </tr>
    </asp:Panel>

    <tr>
        <td>
            <SPAN class="text">		
	         <asp:Literal ID="DistrictDataProvided" runat="server" />
	        </SPAN>
        </td>
    </tr>
    <tr>
        <td>
            <slx:WinssDataGrid ID="RevenueDataGrid" runat="server"  >
                <Columns>
                    <slx:WinssDataGridColumn DataField="PriorYear" MergeRows="true" HeaderText="&nbsp;"/>
                    <slx:WinssDatagridColumn MergeRows="true" DataField="LinkedName"  HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="District Name"  HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="OrgSchoolTypeLabel" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="OrgSchoolTypeLabelWithMembers" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="OrgLevelLabel" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn DataField="Category" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn DataField="Revenue" HeaderText="Revenue" FormatString="$#,##0.##" />
                    <slx:WinssDataGridColumn DataField="Revenue Per Member" HeaderText="Revenue Per Member" FormatString="$#,##0.##" />
                    <slx:WinssDataGridColumn DataField="Percent of Total" HeaderText="Percent of Total" FormatString="#,##0.0%"/>
                </Columns>
            </slx:WinssDataGrid>
            <slx:WinssDataGrid ID="ExpenditureDataGrid" runat="server"  >
                <Columns>
                    <slx:WinssDataGridColumn DataField="PriorYear" MergeRows="true" HeaderText="&nbsp;"/>
                    <slx:WinssDatagridColumn MergeRows="true" DataField="LinkedName"  HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="District Name"  HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="OrgSchoolTypeLabel" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="OrgSchoolTypeLabelWithMembers" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="OrgLevelLabel" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn DataField="Category" HeaderText="&nbsp;"/>
                    <slx:WinssDataGridColumn DataField="Cost" HeaderText="Cost" FormatString="$#,##0.##" />
                    <slx:WinssDataGridColumn DataField="Cost Per Member" HeaderText="Cost Per Member" FormatString="$#,##0.##"/>
                    <slx:WinssDataGridColumn DataField="Percent of Total" HeaderText="Percent of Total" FormatString="#,##0.0%" />
                </Columns>
            </slx:WinssDataGrid>
        </td>
    </tr>
    </asp:Panel>
    
    <!--asp:Panel ID="NoDataForSchoolPanel" run at="server">
    <tr>
    <td>
The data you have requested are not available for the school you have selected. The cause may be that the data are not available for the grade range of your school. For example, if the grade range of your school is kindergarten through second grade, then you will not get any data under "Examining School Performance on Statewide Tests" because no statewide tests are given to students in these grades. Another possible cause is that you selected a newly opened school, and the data you requested are not yet available. 
You may click on the "Back" button of your browser to select another type of data, or click on "change school" to select a different school. If you feel you have received this page in error, please email  <A HREF="mailto:winss@dpi.wi.gov">winss@dpi.wi.gov</A>. 
    </td>
    </tr>
    </asp:Panel-->
    
    
    <tr>
        <td>            
        <br/>
            <!-- <sli:BottomLinkEnrollmentCounts id="BottomLinkEnrollmentCounts1" runat="server"/> -->
            <sli:BottomLinkViewAccountabilityReport id="BottomLinkViewAccountabilityReport" runat="server"/>
            <sli:BottomLinkViewReport id="BottomLinkViewReport2" runat="server"/>
            <sli:BottomLinkViewDistrictReport id="BottomLinkViewDistrictReport1" runat="server"/>
            <sli:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>
            <sli:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
            <sli:BottomLinkDownload id="BottomLinkDownload2" runat="server" Col="14"/>
            <SPAN class="text">
	            <p><a href="javascript:popup('http://spr.dpi.wi.gov/spr_money_use')" onClick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
            </SPAN>        
        </td>
    </tr>
</table>        
</asp:Content>
