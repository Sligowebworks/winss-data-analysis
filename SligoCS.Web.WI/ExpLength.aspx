<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="ExpLength.aspx.cs" Inherits="SligoCS.Web.WI.ExpLength" Title="Post Expulsion" %>

<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>
<%@ Register Src="~/WebUserControls/NavigationLinkRow.ascx"  TagName="NavigationLinkRow" TagPrefix="sli"%>
<%@ Register Src="~/WebUserControls/NavCompareTo.ascx" TagName="NavCompareTo" TagPrefix="sli" %>

<%@ Register Src="WebUserControls/BottomLinkMoreData.ascx" TagName="BottomLinkMoreData" TagPrefix="uc5" %>
<%@ Register Src="WebUserControls/BottomLinkViewReport.ascx" TagName="BottomLinkViewReport" TagPrefix="uc6" %>
<%@ Register Src="~/WebUserControls/BottomLinkViewDistrictReport.ascx" TagName="BottomlinkViewDistrictReport" TagPrefix="uc"%>
<%@ Register Src="WebUserControls/BottomLinkViewProfile.ascx" TagName="BottomLinkViewProfile" TagPrefix="uc7" %>
<%@ Register Src="WebUserControls/BottomLinkDownload.ascx" TagName="BottomLinkDownload" TagPrefix="uc8" %>
<%@ Register Src="WebUserControls/BottomLinkWhatToConsider.ascx" TagName="BottomLinkWhatToConsider" TagPrefix="uc9" %>
<%@ Register Src="WebUserControls/BottomLinkEnrollmentCounts.ascx" TagName="BottomLinkEnrollmentCounts" TagPrefix="uc10" %>
<%@ Register Src="WebUserControls/BottomLinkWhyNotReported.ascx" TagName="BottomLinkWhyNotReported" TagPrefix="uc11" %>
<%@ Register tagPrefix="Graph" namespace="SligoCS.Web.WI.WebUserControls" Assembly="SligoCS.Web.WI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table style="padding:0px 0px 0px 0px; width:400px;">
 <tr><td>
        <sli:NavSelectYear ID="nlrSelectYear" runat="server" /> 
        <sli:NavigationLinkRow ID="nlrShow" runat="server">
                <RowLabel>Show:</RowLabel> 
                <NavigationLinks>
                   <cc1:HyperLinkPlus ID="HyperLinkPlus6" runat="server" UrlFile="ExpLength.aspx" Selected="true">Length of Expulsion</cc1:HyperLinkPlus>
                   <cc1:HyperLinkPlus ID="HyperLinkPlus1" runat="server" UrlFile="ExpServices.aspx" Selected="false">Post Expulsion Services</cc1:HyperLinkPlus>
                   <cc1:HyperLinkPlus ID="HyperLinkPlus2" runat="server" UrlFile="ExpReturns.aspx" Selected="false">Returns to School</cc1:HyperLinkPlus>
                </NavigationLinks>
         </sli:NavigationLinkRow>
        
        <sli:NavCompareTo ID="nlrCompareTo" runat="server" />
        <sli:ChangeSelectedSchoolOrDistrictLink id="ChangeSelectedSchoolOrDistrict" runat="server" />
         </td></tr>
        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
        <tr>
            <td>School level data are not available.</td>
        </tr>
        </asp:Panel>
        <asp:Panel ID="GraphPanel" runat="server">    
        <tr>
            <td>
               <Graph:GraphHorizBarChart ID="barChart" runat="server"></Graph:GraphHorizBarChart>
            </td>
        </tr>    
        </asp:Panel>
        <tr>
        <td>
        &nbsp;
            <slx:WinssDataGrid ID="PostExpulsionDataGrid" runat="server">
                <Columns>
                <slx:WinssDataGridColumn MergeRows="true" DataField="YearFormatted" HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="OrgLevelLabel" HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="OrgSchoolTypeLabel" HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="LinkedName" HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn DataField="Expulsion Type" HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn DataField="Total # Expelled" HeaderText="Total # Expelled" />
                <slx:WinssDataGridColumn DataField="# One Year or Less" />
                <slx:WinssDataGridColumn DataField="# More Than One Year" />
                <slx:WinssDataGridColumn DataField="# Permanent" />
                </Columns>
            </slx:WinssDataGrid>
        </td>
    </tr>

          <tr>
        <td>
        <br/>
            <uc6:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            <uc:BottomlinkViewDistrictReport ID="BottomLinkViewDistrictReport" runat="server" />
            <uc7:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>
            <uc11:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
            <uc8:BottomLinkDownload id="BottomLinkDownload1" runat="server" col="16"/>
            
	            <p><span class="text"><a href="javascript:popup('http://dpi.wi.gov/spr/discip_use.html')" onclick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
            </span> </p>       
        </td>
    </tr>
    </table>
</asp:Content>
