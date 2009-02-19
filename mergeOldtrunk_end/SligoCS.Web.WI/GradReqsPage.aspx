<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="GradReqsPage.aspx.cs" Inherits="SligoCS.Web.WI.GradReqsPage" Title="Requirements for High School Graduation" %>

<%@ Register Src="WebUserControls/BottomLinks.ascx" TagName="BottomLinks" TagPrefix="uc1" %>
<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>
<%@ Register Src="WebUserControls/ParamsLinkBox.ascx" TagName="ParamsLinkBox" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>
    <tr>
        <td><uc2:ParamsLinkBox ID="ParamsLinkBox1" runat="server" ShowTR_GradReqs="true"  ShowTR_CompareTo="true"/></td>
    </tr>
    <tr>
        <td>
            <asp:hyperlink ID="ChangeSelectedSchoolOrDistrict" Font-Size="Small" runat="server" />
        </td>
    </tr>    
    <tr>
        <td><br />School level data are not available.</td>
    </tr>
    <tr>
        <td>
            <cc1:SligoDataGrid ID="SligoDataGrid2" runat="server" AutoGenerateColumns="False"
                ShowSuperHeader="False" SuperHeaderText="" OnRowDataBound="SligoDataGrid2_RowDataBound" UseAccessibleHeader="False" 
                BorderColor="#888888" BorderWidth="4px" BorderStyle="Double" Width="460" >
                <Columns>
                <cc1:MergeColumn DataField="PriorYear" HeaderText="">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn> 
                <cc1:MergeColumn DataField="Subject" HeaderText="Subject">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn> 
                <cc1:MergeColumn DataField="Credits Required by District" HeaderText="Credits Required by District" EnableMerge="false">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn> 
                <cc1:MergeColumn DataField="Credits Required by State" HeaderText="Credits Required by State Law**" EnableMerge="false">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn> 
                <cc1:MergeColumn DataField="District Requirements Meet or Exceed Law" HeaderText="District Requirements Meet or Exceed Law" EnableMerge="false">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn> 
                <cc1:MergeColumn DataField="% of Districts Where Credit Requirements Exceed State Law" HeaderText="% of Districts Where Credit Requirements Exceed State Law" EnableMerge="false">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn> 
                </Columns>
            </cc1:SligoDataGrid>
        </td>
    </tr>
    <tr>
        <td>
            <uc1:BottomLinks ID="BottomLinks1" runat="server" />
        </td>
    </tr>
</table>        
</asp:Content>
