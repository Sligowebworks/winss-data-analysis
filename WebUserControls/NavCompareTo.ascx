<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavCompareTo.ascx.cs" Inherits="SligoCS.Web.WI.WebUserControls.NavCompareTo" %>
<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>
<%@ Register TagName="NavigationLinkRow" TagPrefix="sli" Src="~/WebUserControls/NavigationLinkRow.ascx" %>

<sli:NavigationLinkRow id="CompareTo_Links" runat="server" >
    <RowLabel>Compare To:</RowLabel>
    <NavigationLinks>
            <cc1:HyperLinkPlus ID="linkCompareToYears" runat="server" ParamName="CompareTo" ParamValue="PRIORYEARS">Prior Years</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkCompareToOrgLevel" runat="server" ParamName="CompareTo" ParamValue="DISTSTATE">District/State</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkCompareToSelSchools" runat="server" ParamName="CompareTo" ParamValue="SELSCHOOLS">Selected Schools</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkCompareToSelDistricts" runat="server" ParamName="CompareTo" ParamValue="SELDISTRICTS">Selected Districts</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkCompareToSimSchools" runat="server" ParamName="CompareTo" ParamValue="SIMSCHOOLS" UrlFile="StateTestsSimilarSchools.aspx">Similar Schools</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkCompareToCurrent" runat="server" ParamName="CompareTo" ParamValue="CURRENTONLY">Current School Data</cc1:HyperLinkPlus>
    </NavigationLinks>
    <Extensions><asp:DropDownList ID="ddlYearOverride" runat="server"> <asp:ListItem>2011</asp:ListItem></asp:DropDownList></Extensions>
</sli:NavigationLinkRow>
 
  