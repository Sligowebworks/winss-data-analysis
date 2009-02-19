<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="selMultiDistricts.aspx.cs" Inherits="SligoCS.Web.WI.selMultiDistricts" Title="Selecting Comparable districts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table style="padding:0px 0px 0px 0px; width: 390px;" cellpadding="0" cellspacing="0">
<tr valign="top">
<td align="left" class="title" style="padding:10px 0px 10px 5px; width: 100%; background-color:#cccccc; font-weight:bold;">
<asp:Label ID="Label1" runat="server" Text="Selecting Multiple Districts" />
</td></tr>

<tr><td>&nbsp;</td></tr>

<tr valign="top">
<td align="left" class="title" style="padding:0px 0px 0px 0px; width: 100%;">
    <table style="padding:0px 0px 0px 0px; width: 100%;" border="1">
    <tr valign="top">
    <td align="left" colspan="2" class="smtext" style="padding:5px 5px 5px 5px; width: 100%; background-color:#cccccc;">Step 1: choose one of two options</td>
    </tr>
    <tr>
    <td class="smtext" style="padding:0px 0px 0px 5px; width: 50%;">
        <asp:RadioButton ID="Radio4Districts" GroupName="FourOrAllDistricts"  Checked="true" runat="server" />Select up to four districts in</td>
    <td class="smtext"style="padding:0px 0px 0px 5px; width: 50%;">
        <asp:RadioButton ID="RadioAllDistricts" runat="server" GroupName="FourOrAllDistricts" />Select all districts in<br />(with this option pages may be slow to download)</td>
    </tr>
    </table>  
</td></tr>

<tr><td>&nbsp;</td></tr>

<tr valign="top">
<td align="left" class="title" style="padding:0px 0px 0px 0px; width: 100%;">
    <table style="padding:0px 0px 0px 0px; width: 100%;" border="1">
    <tr valign="top">
    <td align="left" colspan="3" class="smtext" style="padding:5px 5px 5px 5px; width: 100%; background-color:#cccccc;">Step 2: choose the location of interest</td>
    </tr>
    <tr valign="top">
    <td class="smtext" >County</td>
    <td valign="middle" align="left"><asp:DropDownList ID="CountyDropdownlist" EnableViewState="true" runat="server"> </asp:DropDownList></td>
    <td><asp:Button ID="CountyButton" OnClick="CountyButton_Click" runat="server" Text="GO" /></td>
    </tr>
    <tr valign="top">
    <td class="smtext" >Athletic Conference</td>
    <td valign="middle" align="left"><asp:DropDownList ID="AthleticConferenceDropdownlist" EnableViewState="true" runat="server"> </asp:DropDownList></td>
    <td><asp:Button ID="AthleticConferenceButton" OnClick="AthleticConferenceButton_Click" runat="server" Text="GO" /></td>
    </tr>
    <tr valign="top">
    <td class="smtext" >CESA</td>
    <td valign="middle" align="left"><asp:DropDownList ID="CESADropdownlist" EnableViewState="true" runat="server"> </asp:DropDownList></td>
    <td><asp:Button ID="CESAButton" OnClick="CESAButton_Click" runat="server" Text="GO"  /></td>
    </tr>
    <tr valign="top">
    <td class="smtext" >Statewide</td>
    <td class="smtext" >Show all districts statewide<br />(coming soon) </td>
    <td><asp:Button ID="StatewideButton"  runat="server" Text="GO" Enabled="false" /></td>
    </tr>            
    </table>  
</td></tr>
    
</table>

			

<asp:table ID="TableDistrictList" width="390" BorderWidth="2" BorderStyle="Double" runat="server" >
<asp:TableRow>
    <asp:TableCell BackColor="Gainsboro" cssClass="smtext" runat="server">
			Currently Selected Districts 
		</asp:TableCell>
		<asp:TableCell ID="TableCell1" BackColor="Gainsboro" cssClass="smtext" runat="server">
			<asp:Button ID="BackToGraph"  OnClick="BackToGraph_Click" runat="server" Text="-BackToGraph-" />
		</asp:TableCell>
</asp:TableRow>
		
</asp:table>
   
</asp:Content>
