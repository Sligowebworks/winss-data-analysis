<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="Offerings.aspx.cs" Inherits="SligoCS.Web.WI.Offerings" Title="Data Analysis"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div align="right"><img src="/SligoWI/images/data_title3.gif" width="373" height="42" hspace="0" vspace="0" border="0" alt="What programs, staff, and money are available?" /></div>
<br />

<table cellpadding="0" cellspacing="0" border="0" width="390">
<tr valign="top">
<td width="390" align="left">

<ul class="text">
    <asp:Label ID="Label1" runat="server" Text="Reviewing School Programs and Processes" Font-Bold="true" />
<li>
    <asp:PlaceHolder ID="activities" runat="server" />
</li>

<li>
    <asp:PlaceHolder ID="requirements" runat="server" />
</li>

<li>
    <asp:PlaceHolder ID="advanced" runat="server" />
</li>
</ul>


<ul class="text">	
    <asp:Label ID="Label2" runat="server" Text="Examining Staffing Patterns" Font-Bold="true" />
<li>
    <asp:PlaceHolder ID="staff" runat="server" />
</li>

<li>
    <asp:PlaceHolder ID="qualifications" runat="server" />
</li>
</ul>


<ul class="text">	
    <asp:Label ID="Label3" runat="server" Text="Examining Spending Patterns" Font-Bold="true" />
<li>
    <asp:PlaceHolder ID="money" runat="server" />
</li>

<li><a href="javascript:popup('http://www2.dpi.state.wi.us/sfsdw/')" onclick='setCookie(question, url)'>Where can I find more detailed school finance data?</a></li>
</ul>
<br />&nbsp;
</td></tr></table>
</asp:Content>
