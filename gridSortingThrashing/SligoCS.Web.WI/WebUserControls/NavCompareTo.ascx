<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavCompareTo.ascx.cs" Inherits="SligoCS.Web.WI.WebUserControls.NavCompareTo" %>
<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="sli" %>
<%@ Register TagName="NavigationLinkRow" TagPrefix="sli" Src="~/WebUserControls/NavigationLinkRow.ascx" %>

<sli:NavigationLinkRow id="CompareTo_Links" runat="server" >
    <RowLabel>Compare To:</RowLabel>
    <NavigationLinks>
            <sli:HyperLinkPlus ID="linkCompareToYears" runat="server" ParamName="CompareTo" ParamValue="PRIORYEARS">Prior Years</sli:HyperLinkPlus>
            <sli:HyperLinkPlus ID="linkCompareToOrgLevel" runat="server" ParamName="CompareTo" ParamValue="DISTSTATE">District/State</sli:HyperLinkPlus>
            <sli:HyperLinkPlus ID="linkCompareToSelSchools" runat="server" ParamName="CompareTo" ParamValue="SELSCHOOLS">Selected&nbsp;Schools</sli:HyperLinkPlus>
            <sli:HyperLinkPlus ID="linkCompareToCurrent" runat="server" ParamName="CompareTo" ParamValue="CURRENTONLY">Current&nbsp;School&nbsp;Data</sli:HyperLinkPlus>
           
    </NavigationLinks>
</sli:NavigationLinkRow>
  