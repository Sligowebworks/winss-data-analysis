<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="AttendancePage.aspx.cs" Inherits="SligoCS.Web.WI.AttendancePage" Title="Attendance" %>
<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>
<%@ Register Src="~/WebUserControls/NavigationLinkRow.ascx"  TagName="NavigationLinkRow" TagPrefix="sli"%>
<%@ Register Src="~/WebUserControls/NavCompareTo.ascx" TagName="NavCompareTo" TagPrefix="sli" %>
<%@ Register Src="~/WebUserControls/NavSchoolType.ascx" TagName="NavSchoolType" TagPrefix="sli"%>

<%@ Register tagPrefix="Graph" namespace="SligoCS.Web.WI.WebUserControls" Assembly="SligoCS.Web.WI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>
    <tr>
        <td>
        <sli:NavSchoolType ID="nlrSTYP" runat="server" />
        <sli:NavViewByGroup ID="nlrVwBy" runat="server" />
        <sli:NavCompareTo ID="nlrCompareTo" runat="server" />
        <sli:NavSelectYear ID="nlrSelectYear" runat="server" /> 
        </td>
    </tr>
    <asp:Panel ID="GraphPanel" runat="server">       
    <tr>
        <td>
            <sli:ChangeSelectedSchoolOrDistrictLink id="ChangeSelectedSchoolOrDistrict" runat="server" />
        </td>
    </tr>
     <tr>
        <td><!--Chart goes here.-->
        <Graph:GraphBarChart ID="barChart" runat="server"></Graph:GraphBarChart>
        </td>
    </tr>
    </asp:Panel>    

    <tr>
         <td style="width: 291px; height: 3px;" >
            <!-- <br/> -->
              <p align="left" style ="width:475px">
              <span class="text">
                    2004-05 was a year of transition to a new attendance data collection, and as a result 2004-05 attendance data may not be comprehensive. <a href="javascript:popup('http://dpi.wi.gov/spr/att_q&a.html')" onclick="setCookie(question, url)">[More]</a>
              </span>
              </p> 
         </td>
    </tr>
    <tr>
        <td>
            <slx:WinssDataGrid ID="AttendanceDataGrid" runat="server" >
                <Columns>
                    <slx:WinssDataGridColumn MergeRows="true" DataField="YearFormatted" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="LinkedName" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="District Name" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="OrgLevelLabel" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="OrgSchoolTypeLabel" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="SchooltypeLabel" HeaderText="School Type" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="RaceLabel" HeaderText="Race" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="SexLabel" HeaderText="Gender" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="GradeLabel" HeaderText="Student Group" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="DisabilityLabel" HeaderText="Student Group" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="EconDisadvLabel" HeaderText="Student Group" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="ELPLabel" HeaderText="Student Group"  />
                    <slx:WinssDataGridColumn DataField="Enrollment PreK-12" HeaderText="Total Fall Enrollment (PreK-12)**"  FormatString="#,##0.###" />
                    <slx:WinssDataGridColumn DataField="Actual Days Of Attendance"  FormatString="#,##0.0##" />
                    <slx:WinssDataGridColumn DataField="Possible Days Of Attendance"  FormatString="#,##0.0###" />
                    <slx:WinssDataGridColumn DataField="Attendance Rate"  FormatString="#,##0.0%" />
                </Columns>
            </slx:WinssDataGrid>
        </td>
    </tr>
    
    <tr>
        <td>
        <asp:Panel ID="DefPanel" runat="server">
	        <span class="text">
	        ** Enrollment counts in this column may cover a narrower grade range if the "view by: grade" option is selected or if counts are for a specific "school type" (e.g. High School). <a href="javascript:popup('http://dpi.wi.gov/spr/demog_q&a.html')" onclick="setCookie(question, url)">[More]</a>
	        </span>
        </asp:Panel>
            <sli:BottomLinkViewReport id="BottomLinkViewReport" runat="server"/>
            <sli:BottomLinkViewProfile id="BottomLinkViewProfile" runat="server"/>
            <sli:BottomLinkViewDistrictReport id="BottomLinkViewDistrictReport" runat="server"/>
            <sli:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported" runat="server" />
            <sli:BottomLinkDownload id="BottomLinkDownload" runat="server" Col="14"/>
            
            <span class="text">
	            <a href="javascript:popup('http://dpi.wi.gov/spr/att_use.html')" onclick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
            </span>        
        </td>
    </tr>
</table>        
</asp:Content>
