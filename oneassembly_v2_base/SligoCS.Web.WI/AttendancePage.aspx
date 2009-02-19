<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="AttendancePage.aspx.cs" Inherits="SligoCS.Web.WI.AttendancePage" Title="Attendance" %>
<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>
<%@ Register Src="WebUserControls/ParamsLinkBox.ascx" TagName="ParamsLinkBox" TagPrefix="uc2" %>
<%@ Register tagPrefix="Graph" namespace="SligoCS.Web.WI.WebUserControls" Assembly="SligoCS.Web.WI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>
    <tr>
        <td><uc2:ParamsLinkBox ID="ParamsLinkBox1" runat="server" ShowTR_CompareTo="true" ShowTR_Group="true" ShowTR_STYP="true" /></td>
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
            <cc1:SligoDataGrid ID="SligoDataGrid2" runat="server" AutoGenerateColumns="False"
                ShowSuperHeader="False" SuperHeaderText="" OnRowDataBound="SligoDataGrid2_RowDataBound" UseAccessibleHeader="False" 
                BorderColor="#888888" BorderWidth="4px" BorderStyle="Double" Width="460" >
                <Columns>
                <cc1:MergeColumn DataField="YearFormatted">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="LinkedName">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="District Name">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="OrgLevelLabel">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="SchooltypeLabel" HeaderText="School Type">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <asp:BoundField DataField="RaceLabel">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SexLabel">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="GradeLabel">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="DisabilityLabel">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Enrollment PreK-12" HeaderText="Enrollment (PreK-12)*">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Actual Days Of Attendance" HeaderText="Actual Days Of Attendance">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField>                                                                                                               
                <asp:BoundField DataField="Possible Days Of Attendance" HeaderText="Possible Days Of Attendance">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField>                                
                <asp:BoundField DataField="Attendance Rate" HeaderText="Attendance Rate ">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField>                                                                
                </Columns>
            </cc1:SligoDataGrid>
        </td>
    </tr>
</table>        
</asp:Content>
