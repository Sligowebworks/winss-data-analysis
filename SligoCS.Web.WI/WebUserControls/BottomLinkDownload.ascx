<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BottomLinkDownload.ascx.cs" Inherits="SligoCS.Web.WI.WebUserControls.BottomLinkDownload" %>
   
<p>
<span class="text">
<a href="<% =Url %>" target="_blank">
Download Raw Data From This Page</a>
</span>
</p>
<asp:Panel ID="pnlStatewideDownload" runat="server"><p>
<span class="text">
<a href="<% =  StatewideDownloadUrl %>" <% = DisableAttr %>>Download All School and District Data Statewide</a>&nbsp;&nbsp;&nbsp;<a href="http://winss.dpi.wi.gov/winss_data_download#statewide" target="_blank">How to use this feature.</a>
</span>
</p></asp:Panel>
