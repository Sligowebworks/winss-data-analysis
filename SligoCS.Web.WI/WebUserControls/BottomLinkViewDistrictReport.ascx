<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BottomLinkViewDistrictReport.ascx.cs" Inherits="SligoCS.Web.WI.WebUserControls.BottomLinkViewDistrictReport" %>
<asp:Panel ID="LinkPanel" runat="server">
<p>
<span class="text">	
<a href="javascript:popup('https://wlds.dpi.wi.gov/spr/districtreport?district=<% = DistrictCd %>')"  onclick="setCookie(question, url)">
<!--<a href="javascript:popup('http://www2.dpi.state.wi.us/sifi/ayp_summary.asp<% = QueryStringInBottomLink %>')"  onClick="setCookie(question, url)"> -->
View School District Performance Report</a>
</span>
</p>
</asp:Panel>