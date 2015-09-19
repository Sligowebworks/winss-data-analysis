<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BottomLinkDownload.ascx.cs" Inherits="SligoCS.Web.WI.WebUserControls.BottomLinkDownload" %>
   
<p>
<span class="text">
<a href="<% =Url %>">
Download Raw Data From This Page</a>
</span>
</p>
<asp:Panel ID="pnlStatewideDownload" runat="server"><p>
<span class="text">
<a href="<% =  StatewideDownloadUrl %>">Download All School and District Data Statewide</a>&nbsp;&nbsp;&nbsp;<a href="http://dpi.wi.gov/sig/data/download.html#statewide" target="_blank">How to use this feature.</a>
</span>
</p></asp:Panel>
