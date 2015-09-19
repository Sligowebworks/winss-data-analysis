<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavigationLinkRow.ascx.cs" Inherits="SligoCS.Web.WI.WebUserControls.NavigationLinkRow" %>
<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="sli" %>
    <table border="0" class="text">
    <tr>
        <td style="width:60px; vertical-align:top;"><asp:PlaceHolder runat="server" ID="label" /></td>
        <td style="vertical-align:top;"><asp:PlaceHolder ID="links" runat="server" /></td>
        <td style="vertical-align:top;"><asp:PlaceHolder ID="extensions" runat="Server" /></td>
    </tr>
    </table>
