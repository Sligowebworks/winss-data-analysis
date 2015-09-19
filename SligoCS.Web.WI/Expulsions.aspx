<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="Expulsions.aspx.cs" Inherits="SligoCS.Web.WI.Expulsions" Title="Expulsions" %>

<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>
<%@ Register Src="~/WebUserControls/NavigationLinkRow.ascx"  TagName="NavigationLinkRow" TagPrefix="sli"%>
<%@ Register Src="~/WebUserControls/NavCompareTo.ascx" TagName="NavCompareTo" TagPrefix="sli" %>
<%@ Register Src="~/WebUserControls/NavSchoolType.ascx" TagName="NavSchoolType" TagPrefix="sli"%>

<%@ Register Src="WebUserControls/BottomLinkMoreData.ascx" TagName="BottomLinkMoreData" TagPrefix="uc5" %>
<%@ Register Src="WebUserControls/BottomLinkViewReport.ascx" TagName="BottomLinkViewReport" TagPrefix="uc6" %>
<%@ Register Src="WebUserControls/BottomLinkViewProfile.ascx" TagName="BottomLinkViewProfile" TagPrefix="uc7" %>
<%@ Register Src="WebUserControls/BottomLinkDownload.ascx" TagName="BottomLinkDownload" TagPrefix="uc8" %>
<%@ Register Src="WebUserControls/BottomLinkWhatToConsider.ascx" TagName="BottomLinkWhatToConsider" TagPrefix="uc9" %>
<%@ Register Src="WebUserControls/BottomLinkEnrollmentCounts.ascx" TagName="BottomLinkEnrollmentCounts" TagPrefix="uc10" %>
<%@ Register Src="WebUserControls/BottomLinkWhyNotReported.ascx" TagName="BottomLinkWhyNotReported" TagPrefix="uc11" %>

<%@ Register tagPrefix="Graph" namespace="SligoCS.Web.WI.WebUserControls" Assembly="SligoCS.Web.WI" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table style="padding:0px 0px 0px 0px; width:400px;">
    <tr>
        <td>
        <sli:NavSelectYear ID="nlrSelectYear" runat="server" /> 
        <sli:NavSchoolType ID="nlrSTYP" runat="server" />
        <sli:NavigationLinkRow ID="nlrShow" runat="server">
                <RowLabel>Show:</RowLabel> 
                <NavigationLinks>
                   <cc1:HyperLinkPlus ID="HyperLinkPlus6" runat="server" UrlFile="Suspensions.aspx" Selected="false">Suspensions</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="HyperLinkPlus7" runat="server" UrlFile="Expulsions.aspx" Selected="true">Expulsions</cc1:HyperLinkPlus>
                </NavigationLinks>
         </sli:NavigationLinkRow>
        <sli:NavViewByGroup ID="nlrVwByGroup" runat="server" />
        <sli:NavCompareTo ID="nlrCompareTo" runat="server" />
        <sli:ChangeSelectedSchoolOrDistrictLink id="ChangeSelectedSchoolOrDistrictLink1" runat="server" />

        </td>
    </tr>
    <asp:Panel ID="GraphPanel" runat="server">    
    <tr>
        <td>
<%--            <chartfx7:Chart ID="ChartOnPage" run at="server">
            </chartfx7:Chart>--%>
           <Graph:GraphBarChart ID="barChart" runat="server"></Graph:GraphBarChart>
        </td>
    </tr>    
    </asp:Panel>

    <tr>
        <td>
            <asp:hyperlink ID="ChangeSelectedSchoolOrDistrict" Font-Size="Small"  runat="server" />
        </td>
    </tr>
        <tr>
            <td style="width: 291px; height: 3px;" >
              <p align="justify" style ="width:550px">
              <span class="text">
	Major changes in WI discipline data collection systems were implemented in 2006-07. WINSS data about suspensions and expulsions were included in this transition year collection and are not comprehensive so should be interpreted with caution. See other <a href="javascript:popup('http://spr.dpi.wi.gov/spr_discip_q%26amp%3Ba')" onClick="setCookie(question, url)">cautions</a> and information about discipline data on WINSS.

</span></p> </td>
        </tr>
        <tr>
            <td>
                &nbsp;
                <slx:WinssDataGrid ID="ExpulsionsDataGrid" runat="server" >
                <Columns>                                        
                <slx:WinssDataGridColumn MergeRows="true" DataField="YearFormatted" HeaderText="&nbsp;"/>
                <slx:WinssDataGridColumn MergeRows="true" DataField="LinkedName"  HeaderText="&nbsp;"/>
                <slx:WinssDataGridColumn MergeRows="true" DataField="District Name"  HeaderText="&nbsp;"/>
                <slx:WinssDatagridColumn MergeRows="true" DataField="OrgSchoolTypeLabel" HeaderText="&nbsp;" />
                <slx:WinssDatagridColumn MergeRows="true" DataField="OrgLevelLabel" HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="SchooltypeLabel" HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="RaceLabel" HeaderText="&nbsp;"/>
                <slx:WinssDataGridColumn DataField="DisabilityLabel" MergeRows="true"  HeaderText="&nbsp;"/>
                <slx:WinssDataGridColumn MergeRows="true" DataField="SexLabel" HeaderText="&nbsp;"/>
                <slx:WinssDataGridColumn MergeRows="true" DataField="GradeLabel" HeaderText="&nbsp;"/>
                <slx:WinssDataGridColumn DataField="Total Enrollment PreK-12" HeaderText="Total Fall Enrollment PreK-12**"  FormatString="#,##0.###" />
                <slx:WinssDataGridColumn DataField="Number of Students Expelled" HeaderText="Number of Students Expelled"  FormatString="#,##0.###" />
                <slx:WinssDataGridColumn DataField="Expulsion Percent" HeaderText="Expulsion Percent" FormatString="#,##0.00%" />
                </Columns>
                </slx:WinssDataGrid>
                 
            </td>
        </tr>
            <tr>
        <td>
        <asp:Panel ID="DefPanel" runat="server">
	        <SPAN class="text">
	        ** Enrollment counts in this column may cover a narrower grade range if the "view by: grade" option is selected or if counts are for a specific "school type" (e.g. High School). <a href="javascript:popup('http://spr.dpi.wi.gov/spr_demog_q%26amp%3Ba')" onClick="setCookie(question, url)">[More]</a>
	        </SPAN>
        </asp:Panel>
            <sli:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            <sli:BottomLinkViewDistrictReport id="BottomLinkViewDistrictReport1" runat="server"/>
            <sli:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>
            <sli:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
            <sli:BottomLinkDownload id="BottomLinkDownload1" runat="server" Col="14"/>
            <SPAN class="text">
	            <p><a href="javascript:popup('http://spr.dpi.wi.gov/spr_discip_use')" onClick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
            </SPAN>        
        </td>
    </tr>
    </table>
</asp:Content>
