<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavigationLinkRow.ascx.cs" Inherits="SligoCS.Web.WI.WebUserControls.NavigationLinkRow" %>
<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="sli" %>
    <table border="0" class="text">
    <tr>
        <td><asp:PlaceHolder runat="server" ID="label" /></td>
        <td><asp:PlaceHolder ID="links" runat="server" /></td>
    </tr>
    </table>
