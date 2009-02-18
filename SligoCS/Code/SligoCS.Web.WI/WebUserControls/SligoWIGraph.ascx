<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SligoWIGraph.ascx.cs" Inherits="SligoCS.Web.WI.WebUserControls.SligoWIGraph" %>
<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc3" %>
   
<asp:PlaceHolder ID="PlaceHolder1" runat="server">
<!--%=TestString%-->
<table cellspacing="0" cellpadding="0" width="498" border="0">
				<tr valign="top">
					<td >
					<cc3:ChartMain ID = "graph1" runat="server"></cc3:ChartMain>
					</td>
				</tr>
</table>
<br />
</asp:PlaceHolder>
