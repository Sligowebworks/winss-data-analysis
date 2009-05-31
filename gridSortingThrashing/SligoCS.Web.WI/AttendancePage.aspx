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
        <sli:NavigationLinkRow ID="nlrVwBy" runat="server">
            <RowLabel>View By:</RowLabel>
            <NavigationLinks>
                <cc1:HyperLinkPlus ID="linkGroupAll" runat="server" ParamName="Group" ParamValue="AllStudentsFAY">All Students</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkGroupGender" runat="server" ParamName="Group" ParamValue="Gender">Gender</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkGroupRace" runat="server" ParamName="Group" ParamValue="RaceEthnicity">Race/Ethnicity</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkGroupGrade" runat="server" ParamName="Group" ParamValue="Grade">Grade</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkGroupDisability" runat="server" ParamName="Group" ParamValue="Disability">Disability</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkGroupEconDisadv" runat="server" ParamName="Group" ParamValue="EconDisadv">Economic Status</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkGroupEngLangProf" runat="server" ParamName="Group" ParamValue="ELP">English Proficiency</cc1:HyperLinkPlus>
            </NavigationLinks>
        </sli:NavigationLinkRow>
        <sli:NavCompareTo ID="nlrCompareTo" runat="server" />
        </td>
    </tr>
    <asp:Panel ID="GraphPanel" runat="server">       
    <tr>
        <td><!--Chart goes here.-->
        <Graph:GraphBarChart ID="barChart" runat="server"></Graph:GraphBarChart>
        </td>
    </tr>
    </asp:Panel>    
    
    <tr>
        <td>
            <slx:WinssDataGrid ID="AttendanceDataGrid" runat="server" OnRowDataBound="AttendanceDataGrid_RowDataBound" AllowSorting="true">
                <Columns>
                    <slx:WinssDataGridColumn MergeRows="true" DataField="YearFormatted" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="LinkedName" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="District Name" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="OrgLevelLabel" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="SchooltypeLabel" HeaderText="School Type" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="RaceLabel" HeaderText="Race" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="SexLabel" HeaderText="Gender" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="GradeLabel" HeaderText="Student Group" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="DisabilityLabel" HeaderText="Student Group" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="EconDisadvLabel" HeaderText="Student Group" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="ELPLabel" HeaderText="Student Group"  FormatString="#,##0.###" />
                    <slx:WinssDataGridColumn DataField="Enrollment PreK-12" HeaderText="Enrollment (PreK-12)*"  FormatString="#,##0.###" />
                    <slx:WinssDataGridColumn DataField="Actual Days Of Attendance"  FormatString="#,##0.###" />
                    <slx:WinssDataGridColumn DataField="Possible Days Of Attendance"  FormatString="#,##0.###" />
                    <slx:WinssDataGridColumn DataField="Attendance Rate"  FormatString="#,##0.0%" />
                </Columns>
            </slx:WinssDataGrid>
        </td>
    </tr>
</table>        
</asp:Content>
