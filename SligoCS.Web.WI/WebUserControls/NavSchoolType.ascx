<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavSchoolType.ascx.cs" Inherits="SligoCS.Web.WI.WebUserControls.NavSchoolType" %>
<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>

<%@ Register TagName="NavigationLinkRow" TagPrefix="sli" Src="~/WebUserControls/NavigationLinkRow.ascx" %>

<sli:NavigationLinkRow id="Links" runat="server" >
    <RowLabel>School Type:</RowLabel>
    <NavigationLinks>
            <cc1:HyperLinkPlus ID="linkSTYP_ALLTypes" runat="server" ParamName="STYP" ParamValue="1" Prefix="">All&nbsp;Types</cc1:HyperLinkPlus>            
            <cc1:HyperLinkPlus ID="linkSTYP_Elem" runat="server" ParamName="STYP" ParamValue="6">Elem</cc1:HyperLinkPlus>            
            <cc1:HyperLinkPlus ID="linkSTYP_Mid" runat="server" ParamName="STYP" ParamValue="5">Mid/Jr&nbsp;Hi</cc1:HyperLinkPlus>            
            <cc1:HyperLinkPlus ID="linkSTYP_Hi" runat="server" ParamName="STYP" ParamValue="3">High</cc1:HyperLinkPlus>            
            <cc1:HyperLinkPlus ID="linkSTYP_ElSec" runat="server" ParamName="STYP" ParamValue="7">El/Sec</cc1:HyperLinkPlus>            
            <cc1:HyperLinkPlus ID="linkSTYP_StateSummary" runat="server" ParamName="STYP" ParamValue="9">School&nbsp;Summary</cc1:HyperLinkPlus>
    </NavigationLinks>
</sli:NavigationLinkRow>
