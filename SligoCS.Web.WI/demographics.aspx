<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="demographics.aspx.cs" Inherits="SligoCS.Web.WI.demographics" Title="Data Analysis" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<img src="<%= Request.ApplicationPath %>/images/data_title1.gif" width="370" height="21" hspace="0" vspace="0" border="0" alt="What are student demographics?" />

<table width="100%" cellpadding="0" cellspacing="0">
<tr><td>&nbsp;</td></tr>
<tr><td align="left">
<ul class="text">    
<li>
    <asp:PlaceHolder ID="groups" runat="server" />
</li>
<li>
    <asp:PlaceHolder ID="disabilities" runat="server" />
</li>
<li>
What are the characteristics of limited English proficient students at this school?
</li>
</ul>

</td></tr></table>

</asp:Content>
