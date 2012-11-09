<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="ActivitiesParticipate.aspx.cs" Inherits="SligoCS.Web.WI.ActivitiesParticipate"  Title="Activities Participation" %>

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

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table style="padding:0px 0px 0px 0px; width:400px;">
    <tr><td>
        <sli:NavSelectYear ID="nlrSelectYear" runat="server" /> 
        <sli:NavSchoolType ID="nlrSchoolType" runat="server" />
        
        <sli:NavigationLinkRow ID="nlrShow" runat="server">
            <RowLabel>Show:</RowLabel>
             <NavigationLinks>
                <cc1:HyperLinkPlus ID="linkShowExtracurricular" runat="server" ParamName="Show" ParamValue="EXTRA">Extra/Co-curricular Activites</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkShowCommunity" runat="server" ParamName="Show" ParamValue="COMM">Community Activities</cc1:HyperLinkPlus>
                </NavigationLinks>
        </sli:NavigationLinkRow>
        <sli:NavCompareTo ID="nlrCompareTo" runat="server" />
    </td></tr>
    <tr>
        <td>
            
            <table width="100%"><tr>
        <td><sli:ChangeSelectedSchoolOrDistrictLink id="ChangeSelectedSchoolOrDistrict" runat="server" /></td>
        <td align="right"><p>Go to: <a href="ActivitiesOffer.aspx<% Response.Write(GetQueryString(new String[1]{"Qquad=offerings.aspx"})); %>">What school-supported activities are offered?</a></p></td>
              </tr></table>
        </td>
    </tr>
    <asp:Panel ID="GraphPanel" runat="server">    
    <tr>
        <td>
           <Graph:GraphBarChart ID="barChart" runat="server"></Graph:GraphBarChart>
        </td>
    </tr>    
    </asp:Panel>
    <tr>
        <td>
            <slx:WinssDataGrid ID="ActivitiesDataGrid" runat="server" >
                <Columns> 
                    <slx:WinssDatagridColumn MergeRows="true" DataField="YearFormatted" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="LinkedName"  HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="District Name"  HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="OrgSchoolTypeLabel" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="OrgLevelLabel" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="SchooltypeLabel" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn DataField="RaceLabel" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn DataField="SexLabel" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn DataField="GradeLabel"  HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="ActivityLabel" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn DataField="Enrollment Grades 6-12*" 
                            HeaderText="Total Fall Enrollment <br />Grades 6-12**"  FormatString="#,##0" />
                    <slx:WinssDatagridColumn DataField="Enrollment Grades 9-12*" 
                            HeaderText="Total Fall Enrollment <br />Grades 9-12**" FormatString="#,##0"  />
                    <slx:WinssDatagridColumn DataField="Pupils Participating"  FormatString="#,##0" />
                    <slx:WinssDatagridColumn DataField="Participation Rate"  FormatString="#,##0.0%" />
                </Columns>
                </slx:WinssDataGrid>
                
        </td>
    </tr>
    <tr>
        <td>
            <uc10:BottomLinkEnrollmentCounts id="BottomLinkEnrollmentCounts1" runat="server"/>
            <uc6:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            <sli:BottomLinkViewDistrictReport id="BottomLinkViewDistrictReport1" runat="server"/>
            <uc7:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>
            <uc11:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
            <uc8:BottomLinkDownload id="BottomLinkDownload1" runat="server" Col="16"/>
            <SPAN class="text">
	            <p><a href="javascript:popup('http://spr.dpi.wi.gov/spr_activi_use')" onClick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a></p>
                
            </SPAN> 
        </td>
    </tr>
 </table>
</asp:Content>
