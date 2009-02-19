<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="Demo.aspx.cs" Inherits="SligoCS.Web.WI.Demo" Title="Drop Out" %>

<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.WI.WebServerControls.WI"
    TagPrefix="cc1" %>

<%@ Register Src="WebUserControls/ParamsLinkBox.ascx" TagName="ParamsLinkBox" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>
    <tr>
        <td style="height: 114px">
            <uc2:ParamsLinkBox ID="ParamsLinkBox1" runat="server" ShowTR_CompareTo="true" ShowTR_Group="true"
                ShowTR_STYP="true" />
        </td>
    </tr>
    <tr>
        <td>            
            <cc1:SligoDataGrid ID="SligoDataGrid2" runat="server" AutoGenerateColumns="False" OnRowDataBound="SligoDataGrid2_RowDataBound" 
            ShowSuperHeader="true" UseAccessibleHeader="False" 
            BorderColor="#888888" BorderWidth="4px" BorderStyle="Double" Width="460" CellPadding="2">            
                <Columns>                    
                    <cc1:MergeColumn DataField="YearFormatted" EnableMerge="True" >
                    </cc1:MergeColumn>
                    <cc1:MergeColumn DataField="LinkedName" EnableMerge="True">
                    </cc1:MergeColumn>                    
                    <cc1:MergeColumn DataField="District Name" EnableMerge="True" >
                    </cc1:MergeColumn>                    
                    <cc1:MergeColumn DataField="SchoolTypeLabel" EnableMerge="True" >
                    </cc1:MergeColumn>                    
                    <asp:BoundField DataField="SexLabel">
                    </asp:BoundField>
                    <asp:BoundField DataField="RaceLabel">
                    </asp:BoundField>
                    <asp:BoundField DataField="GradeLabel">
                    </asp:BoundField>
                    <asp:BoundField DataField="DisabilityLabel">                    
                    </asp:BoundField>
                    <asp:BoundField DataField="EconDisadvLabel">
                    </asp:BoundField>
                    <asp:BoundField DataField="ELPLabel">
                    </asp:BoundField>                    
                    <asp:BoundField DataField="Enrollment" HeaderText="Total Fall Enrollment Grades 7-12**">
                    </asp:BoundField>
                    <asp:BoundField DataField="Students expected to complete the school term" HeaderText="Students expected to complete the school term">
                    </asp:BoundField>
                    <asp:BoundField DataField="Students who completed the school term" HeaderText="Students who completed the school term">                        
                    </asp:BoundField>
                    <asp:BoundField DataField="Drop Outs" HeaderText="Drop Outs">
                    </asp:BoundField>
                    <asp:BoundField DataField="Drop Out Rate" HeaderText="Drop Out Rate">
                    </asp:BoundField>
                </Columns>
            </cc1:SligoDataGrid>
        </td>
    </tr>
</table>    
</asp:Content>
