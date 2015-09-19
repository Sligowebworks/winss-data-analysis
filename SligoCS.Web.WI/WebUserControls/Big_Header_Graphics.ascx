<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Big_Header_Graphics.ascx.cs" Inherits="SligoCS.Web.WI.WebUserControls.Big_Header_Graphics" %>

<table cellpadding="0" cellspacing="0" border="0" width="600">
<tr valign="bottom"><td style="{white-space: nowrap}"><asp:PlaceHolder ID="Wins_Mortar_Guide" runat="server"></asp:PlaceHolder><br /></td>

<td width="342" align="right"><asp:HyperLink ID="data_big_gif" runat="server" ImageUrl="~/images/data_big.gif" /></td></tr>
<tr><td valign="top" colspan="2"><img src="<%= Request.ApplicationPath%>/images/assess_runner.gif" width="600" height="5" hspace="0" vspace="0" border="0" alt="" /></td></tr>
<tr><td valign="top" colspan="2"><img src="<%= Request.ApplicationPath%>/images/white.gif" width="1" height="10" border="0" alt="" /></td></tr>
</table>