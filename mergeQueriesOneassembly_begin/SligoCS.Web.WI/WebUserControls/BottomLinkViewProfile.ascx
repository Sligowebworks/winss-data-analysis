<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BottomLinkViewProfile.ascx.cs" Inherits="SligoCS.Web.WI.WebUserControls.BottomLinkViewProfile" %>
 <asp:Panel ID="LinkPanel" runat="server">
<p>
<SPAN class="text">				
<!--a href="javascript:popup('https://www2.dpi.state.wi.us/DistrictProfile/Pages/DistrictProfile.aspx?year=2006&DistrictID=3276')" onClick="setCookie(question, url)"-->
<a href="javascript:popup('https://www2.dpi.state.wi.us/DistrictProfile/Pages/DistrictProfile.aspx?year=2006&DistrictID=<% = DistrictCd %>')" onClick="setCookie(question, url)">
View Special Education District Profile</a>
</SPAN>
</p>
</asp:Panel> 