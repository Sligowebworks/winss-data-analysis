<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CESA_Map_Control.ascx.cs" Inherits="SligoCS.Web.WI.WebUserControl2" %>
<asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
&nbsp; 
<span class="smtext">
<a href="http://dpi.wi.gov/oea/cesainfo.html" target="help" onclick="popup('http://dpi.wi.gov/oea/cesainfo.html','help');">
What is a CESA?
</a>
</span>
<br /><br />
<span class="text">
<b>Search for <a href="<%= Request.ApplicationPath%>/questions.aspx<%Response.Write( GetQueryString(new String[] {"FULLKEY="+ SligoCS.BL.WI.FullKeyUtils.StateFullKey("")}));%>">STATE</a> Data</b>
</span>