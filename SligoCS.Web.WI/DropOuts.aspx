<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="DropOuts.aspx.cs" Inherits="SligoCS.Web.WI.DropOuts" Title="Dropouts" %>
<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>

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
      <sli:NavSchoolType ID="nlrSTYP" runat="server" />
       <sli:NavViewByGroup ID="nlrVwByGroup" runat="server" />
        <sli:NavCompareTo ID="nlrCompareTo" runat="server" />
            <sli:ChangeSelectedSchoolOrDistrictLink ID="ChangeSelectedSchoolOrDistrict" Font-Size="Small" runat="server" />
        </td>
    </tr>
         <asp:Panel ID="GraphPanel" runat="server">    
    <tr>
        <td>
            <Graph:GraphBarChart ID="barChart" runat="server"></Graph:GraphBarChart>
            	<SPAN class="text"><br>
	The method of calculating dropout rates changed in 1998-99 and 2003-04. 2003-04 was a year of transition to a new dropout data collection, and as a result 2003-04 dropout data may not be comprehensive. <a href="javascript:popup('http://www.dpi.wi.gov/spr_drop_q%26amp%3Ba')" onClick="setCookie(question, url)">[More]</a>
	</SPAN>
        </td>
    </tr>    
    </asp:Panel>

      <tr>
        <td>
            <slx:WinssDataGrid ID="DropoutsDataGrid" runat="server">
                <Columns>
                <slx:WinssDataGridColumn MergeRows="true" DataField="YearFormatted" HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="LinkedName"  HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="District Name"  HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="OrgLevelLabel"  HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="OrgSchoolTypeLabel"  HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="SchooltypeLabel" HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="RaceLabel" HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="SexLabel" HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="GradeLabel"  HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="DisabilityLabel"  HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="EconDisadvLabel"  HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="ELPLabel"  HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn DataField="Enrollment" HeaderText="Total Fall Enrollment Grades 7&#8209;12**" FormatString="#,##0.###" />
                <slx:WinssDataGridColumn DataField="Students expected to complete the school term" HeaderText="Students Expected To Complete the School Term" FormatString="#,##0.###" />
                <slx:WinssDataGridColumn DataField="Students who completed the school term" HeaderText="Students Who Completed the School Term" FormatString="#,##0.###" />
                <slx:WinssDataGridColumn DataField="Drop Outs" HeaderText="Number of Dropouts" FormatString="#,##0.###" />
                <slx:WinssDataGridColumn DataField="Drop Out Rate" HeaderText="Dropout Rate" FormatString="#,##0.00%" />
                </Columns>
            </slx:WinssDataGrid>
        </td>
    </tr>

          <tr>
        <td>
        <asp:Panel ID="DefPanel" runat="server">
	        <SPAN class="text">* <A HREF=javascript:popup('http://spr.dpi.wi.gov/spr_drop_q%26amp%3Ba') onClick='setCookie(question, url)'>Definition changed in 1998-99</A></SPAN>
            <br/>
	        <SPAN class="text">
	        ** Enrollment counts in this column may cover a narrower grade range if the "view by: grade" option is selected or if counts are for a specific "school type" (e.g. High School). <a href="javascript:popup('http://spr.dpi.wi.gov/spr_demog_q%26amp%3Ba')" onClick="setCookie(question, url)">[More]</a>
	        </SPAN>
        </asp:Panel>

            <!-- <uc10:BottomLinkEnrollmentCounts id="BottomLinkEnrollmentCounts1" runat="server"/> -->
            <sli:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            <sli:BottomLinkViewDistrictReport id="BottomLinkViewDistrictReport1" runat="server"/>
            <sli:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>
            <sli:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
            <sli:BottomLinkDownload id="BottomLinkDownload1" runat="server" Col="16"/>
            <SPAN class="text">
	            <p><a href="javascript:popup('http://spr.dpi.wi.gov/spr_drop_use ')" onClick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
            </p></SPAN>        
        </td>
    </tr>
 </table>        
</asp:Content>
