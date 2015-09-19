<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BottomLinkViewReport.ascx.cs" Inherits="SligoCS.Web.WI.WebUserControls.BottomLinkViewReport" %>
<asp:Panel ID="LinkPanel" runat="server">
<p>
<span class="text">	
<!--<a href="javascript:popup('http://www2.dpi.state.wi.us/sifi/ayp_summary.asp?year=2009&districtcd=<% = DistrictCd %>')"  onClick="setCookie(question, url)"> -->
<!--<a href="javascript:popup('http://www2.dpi.state.wi.us/sifi/ayp_summary.asp<% = QueryStringInBottomLink %>')"  onClick="setCookie(question, url)"> -->
<a href="javascript:popup('http://www2.dpi.state.wi.us/sifi/ayp_summary.asp?year=2010<% = QueryStringInBottomLink %>')"  onClick="setCookie(question, url)">
View Adequate Yearly Progress Report</a>
</span>
</p>
</asp:Panel>