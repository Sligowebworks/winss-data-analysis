<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="Demo.aspx.cs" Inherits="SligoCS.Web.WI.Demo" Title="Dropouts Demo Page" %>

<%@ Register Src="WebUserControls/ParamsLinkBox.ascx" TagName="ParamsLinkBox" TagPrefix="uc2" %>

<%@ Register Src="WebUserControls/SligoDataGrid.ascx" TagName="SligoDataGrid" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>
    <tr>
        <td>
            <uc2:ParamsLinkBox ID="ParamsLinkBox1" runat="server" ShowTR_CompareTo="true" ShowTR_Group="true"
                ShowTR_STYP="true" />
        </td>
    </tr>
    <tr>
        <td><uc1:SligoDataGrid ID="SligoDataGrid1" runat="server" /></td>
    </tr>
</table>    
</asp:Content>
