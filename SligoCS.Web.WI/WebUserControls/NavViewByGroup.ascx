<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavViewByGroup.ascx.cs" Inherits="SligoCS.Web.WI.WebUserControls.NavViewByGroup" %>
<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>
<%@ Register TagName="NavigationLinkRow" TagPrefix="sli" Src="~/WebUserControls/NavigationLinkRow.ascx" %>

<sli:NavigationLinkRow ID="ViewByGroup_Links" runat="server">
    <RowLabel>View By:</RowLabel>
    <NavigationLinks>
        <cc1:HyperLinkPlus ID="linkGroupAll" runat="server" ParamName="Group" ParamValue="AllStudentsFAY">All Students</cc1:HyperLinkPlus>
        <cc1:HyperLinkPlus ID="linkGroupGender" runat="server" ParamName="Group" ParamValue="Gender">Gender</cc1:HyperLinkPlus>
        <cc1:HyperLinkPlus ID="linkGroupRace" runat="server" ParamName="Group" ParamValue="RaceEthnicity">Race/Ethnicity</cc1:HyperLinkPlus>
        <cc1:HyperLinkPlus ID="linkGroupRaceGender" runat="server" ParamName="Group" ParamValue="RceGndr">Race/Ethnicity & Gender</cc1:HyperLinkPlus>
        <cc1:HyperLinkPlus ID="linkGroupGrade" runat="server" ParamName="Group" ParamValue="Grade">Grade</cc1:HyperLinkPlus>
        <cc1:HyperLinkPlus ID="linkGroupDisability" runat="server" ParamName="Group" ParamValue="Disability">Disability</cc1:HyperLinkPlus>
        <cc1:HyperLinkPlus ID="linkGroupEconDisadv" runat="server" ParamName="Group" ParamValue="EconDisadv">Economic Status</cc1:HyperLinkPlus>
        <cc1:HyperLinkPlus ID="linkGroupEngLangProf" runat="server" ParamName="Group" ParamValue="ELP">English Proficiency</cc1:HyperLinkPlus>
        <cc1:HyperLinkPlus ID="linkGroupMigrant" runat="server" ParamName="Group" ParamValue="Mig">Migrant Status</cc1:HyperLinkPlus>
    </NavigationLinks>
</sli:NavigationLinkRow>