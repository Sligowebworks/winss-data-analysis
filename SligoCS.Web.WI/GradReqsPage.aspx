<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="GradReqsPage.aspx.cs" Inherits="SligoCS.Web.WI.GradReqsPage" Title="Requirements for High School Graduation" %>

<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>
        <%@ Register Src="~/WebUserControls/NavigationLinkRow.ascx"  TagName="NavigationLinkRow" TagPrefix="sli"%>
<%@ Register Src="~/WebUserControls/NavCompareTo.ascx" TagName="NavCompareTo" TagPrefix="sli" %>
    
<%@ Register Src="WebUserControls/BottomLinks.ascx" TagName="BottomLinks" TagPrefix="uc1" %>
<%@ Register Src="WebUserControls/BottomLinkMoreData.ascx" TagName="BottomLinkMoreData" TagPrefix="uc5" %>
<%@ Register Src="WebUserControls/BottomLinkViewReport.ascx" TagName="BottomLinkViewReport" TagPrefix="uc6" %>
<%@ Register Src="WebUserControls/BottomLinkViewProfile.ascx" TagName="BottomLinkViewProfile" TagPrefix="uc7" %>
<%@ Register Src="WebUserControls/BottomLinkDownload.ascx" TagName="BottomLinkDownload" TagPrefix="uc8" %>
<%@ Register Src="WebUserControls/BottomLinkWhatToConsider.ascx" TagName="BottomLinkWhatToConsider" TagPrefix="uc9" %>
<%@ Register Src="WebUserControls/BottomLinkEnrollmentCounts.ascx" TagName="BottomLinkEnrollmentCounts" TagPrefix="uc10" %>
<%@ Register Src="WebUserControls/BottomLinkWhyNotReported.ascx" TagName="BottomLinkWhyNotReported" TagPrefix="uc11" %>
<%@ Register tagPrefix="Graph" namespace="SligoCS.Web.WI.WebUserControls" Assembly="SligoCS.Web.WI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>
    <tr>
        <td>
            <sli:NavSelectYear ID="nlrSelectYear" runat="server" /> 
            <sli:NavigationLinkRow ID="nlrShow" runat="server">
                <RowLabel>Show:</RowLabel>
                 <NavigationLinks>
                    <cc1:HyperLinkPlus ID="linkGRSbjStateLaw" runat="server" ParamName="GRSbj" ParamValue="2">Subjects Required by State Law</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkGRSbjAdditional" runat="server" ParamName="GRSbj" ParamValue="4">Additional Subjects</cc1:HyperLinkPlus>
                 </NavigationLinks>
            </sli:NavigationLinkRow>
            
            <sli:NavCompareTo ID="nlrCompareTo" runat="server" />
            <sli:ChangeSelectedSchoolOrDistrictLink ID="ChangeSelectedSchoolOrDistrict" runat="server" />
        </td>
    </tr>
    <asp:Panel ID="GraphPanel" runat="server">    
    <tr>
        <td>
           <Graph:GraphBarChart ID="barChart" runat="server"></Graph:GraphBarChart>
        </td>
    </tr>    
    </asp:Panel>
    <asp:Panel ID="pnlMessage" runat="server" Visible="false" >
    <tr><td>School level data are not available.</td></tr>
    </asp:Panel>
    <tr>
        <td>
            <slx:WinssDataGrid ID="GradReqsDataGrid" runat="server" >
                <Columns>
                <slx:WinssDataGridColumn MergeRows="true" DataField="YearFormatted" HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="OrgLevelLabel" HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="OrgSchoolTypeLabel" HeaderText="&nbsp;" />
                <slx:WinssDatagridColumn MergeRows="true" DataField="LinkedName"  HeaderText="&nbsp;" />
                
                <slx:WinssDataGridColumn DataField="Subject" />
                <slx:WinssDataGridColumn DataField="Credits Required by District" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="Credits Required by State" HeaderText="Credits Required by State Law**" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="# of Districts with Grade 12" FormatString="#,##0"/>
                <slx:WinssDataGridColumn DataField="Average Number of Credits Required by Districts" FormatString="#,##0.0" />
                <slx:WinssDataGridColumn DataField="District Requirements Meet or Exceed Law" HeaderText="District Requirements Meet or Exceed State Law" /> 
                <slx:WinssDataGridColumn DataField="# of Districts Where Credit Requirements Exceed State Law" FormatString="#,##0"/>
                <slx:WinssDataGridColumn DataField="% of Districts Where Credit Requirements Exceed State Law" FormatString="#,##0.0%"/>
                </Columns>
            </slx:WinssDataGrid>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Panel ID="LinkPanel" runat="server">
                <p>
                <span class="text">	** State law sets a minimum standard of 13 credits in specified required subjects. Districts are encouraged by law to require a minimum of 8.5 credits in unspecified additional subjects for a total of at least 21.5 credits. Districts meet or exceed this 21.5 credit minimum standard.  <a href="javascript:popup('http://spr.dpi.wi.gov/spr_gradrq_q%26amp%3Ba')" >[More]</a>
                </span>
                </p>
            </asp:Panel>
            <sli:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            <sli:BottomLinkViewDistrictReport id="BottomLinkViewDistrictReport1" runat="server"/>
            <sli:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>
            <sli:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
            <sli:BottomLinkDownload id="BottomLinkDownload1" runat="server" Col="14"/>
            <SPAN class="text">
	            <p><a href="javascript:popup('http://spr.dpi.wi.gov/spr_gradrq_use')" onClick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
            </SPAN>        
        </td>
    </tr>
</table>        
</asp:Content>
