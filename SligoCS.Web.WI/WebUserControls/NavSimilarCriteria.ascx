<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavSimilarCriteria.ascx.cs" Inherits="SligoCS.Web.WI.WebUserControls.NavSimilarCriteria" %>

<%@ Register TagName="NavigationLinkRow" TagPrefix="sli" Src="~/WebUserControls/NavigationLinkRow.ascx" %>

<sli:NavigationLinkRow ID="NavSimilarCriteria_LinkRow" runat="server">
    <RowLabel>Similar** Criteria:</RowLabel>
    <NavigationLinks>
        <slx:HyperLinkToggle ID="linkSPENDOn" runat="server" ParamName="SPEND" ParamValue="Y">District  Spending</slx:HyperLinkToggle>
        <slx:HyperLinkToggle ID="linkSPENDOff" runat="server" ParamName="SPEND" ParamValue="N">District Spending</slx:HyperLinkToggle>
        <slx:HyperLinkToggle ID="linkSIZEOn" runat="server" ParamName="SIZE" ParamValue="Y">District  Size</slx:HyperLinkToggle>
        <slx:HyperLinkToggle ID="linkSIZEOff" runat="server" ParamName="SIZE" ParamValue="N">District Size</slx:HyperLinkToggle>
        <slx:HyperLinkToggle ID="linkECONOn" runat="server" ParamName="ECON" ParamValue="Y">%  Economically Disadvantaged</slx:HyperLinkToggle>
        <slx:HyperLinkToggle ID="linkECONOff" runat="server" ParamName="ECON" ParamValue="N">% Economically Disadvantaged</slx:HyperLinkToggle>
        <slx:HyperLinkToggle ID="linkDISABILITYOn" runat="server" ParamName="DISABILITY" ParamValue="Y">%  Students w/ Disabilites</slx:HyperLinkToggle>
        <slx:HyperLinkToggle ID="linkDISABILITYOff" runat="server" ParamName="DISABILITY" ParamValue="N">% Students w/ Disabilites</slx:HyperLinkToggle>
        <slx:HyperLinkToggle ID="linkLEPOn" runat="server" ParamName="LEP" ParamValue="Y">%  Limited English Proficient</slx:HyperLinkToggle>
        <slx:HyperLinkToggle ID="linkLEPOff" runat="server" ParamName="LEP" ParamValue="N">% Limited English Proficient</slx:HyperLinkToggle>
        <slx:HyperLinkToggle ID="linkNoChceOn" runat="server" ParamName="NoChce" ParamValue="Y">No Criteria  Chosen</slx:HyperLinkToggle>
        <slx:HyperLinkToggle ID="linkNoChceOff" runat="server" ParamName="NoChce" ParamValue="N">No Criteria Chosen</slx:HyperLinkToggle>
    </NavigationLinks>
</sli:NavigationLinkRow>