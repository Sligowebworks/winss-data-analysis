<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="questions.aspx.cs" Inherits="SligoCS.Web.WI.questions" Title="Data Analysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<table border="0" cellspacing="0" cellpadding="0">
<tr valign="top">
<td >
    <table border="0" cellspacing="0" cellpadding="0">
    <tr valign="top">
    <td align="left">
        <asp:PlaceHolder ID="performance" runat="server"></asp:PlaceHolder>
    </td>
    </tr>
   
	<tr><td align="left">&nbsp;</td></tr>
	
	<tr>
	<td align="left">
        <asp:PlaceHolder ID="attendance" runat="server"></asp:PlaceHolder>
	</td>
	</tr>

	</table>
</td>
<td >
    <table border="0" cellspacing="0" cellpadding="0">
	<tr>
	<td colspan="2" align="right">
	    <asp:PlaceHolder ID="offerings" runat="server"></asp:PlaceHolder>
	</td>
	</tr>

	<tr><td align="left">&nbsp;</td></tr>
		
	<tr valign="bottom">
	<td align="right">
        <asp:PlaceHolder ID="demographics" runat="server"></asp:PlaceHolder>
	</td>
	</tr>
	
	</table>
</td>
</tr>
</table>

</asp:Content>
