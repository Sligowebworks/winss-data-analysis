<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WI_DPI_Disclaim.ascx.cs" Inherits="SligoCS.Web.WI.WebUserControls.WI_DPI_Disclaim" %>

<table cellspacing="0" cellpadding="0" border="0" style="padding:0px 0px 0px 0px; width:185px;" class="datatext">
<tr valign="top">
<td align="left">
Most of the data on these pages are provided by Wisconsin schools and districts to the Department of Public Instruction as part of state or federally mandated data collections.
<br />
<br />
<!--a href="<%= Request.ApplicationPath%>/Selschool.aspx" target="_top" class="lightblue">EDIT</a-->
<asp:HyperLink ID="EDITselection" runat="server" CssClass="lightblue" Text="EDIT" />
your selection
<br />
<a href="http://winss.dpi.wi.gov/winss_usertips" target="help" class="lightblue" target="_blank">Help</a>
</td></tr></table>
<br /><br />