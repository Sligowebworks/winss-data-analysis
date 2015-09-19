<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BottomLinkViewDistrictReport.ascx.cs" Inherits="SligoCS.Web.WI.WebUserControls.BottomLinkViewDistrictReport" %>
<asp:Panel ID="LinkPanel" runat="server">
<p>
<span class="text">	
<a href="javascript:popup('https://apps2.dpi.wi.gov/sdpr/district-report.action?district=<%= DistrictCd %>')"  >

View School District Performance Report</a>
</span>
</p>
</asp:Panel>