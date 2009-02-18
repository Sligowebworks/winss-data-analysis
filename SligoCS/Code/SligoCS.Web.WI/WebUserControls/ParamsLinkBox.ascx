<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ParamsLinkBox.ascx.cs" Inherits="SligoCS.Web.WI.WebUserControls.ParamsLinkBox" %>
<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>
<table border="1">
    <tr id="trSTYP" runat="server">
        <td>School Type</td>
        <td>
            <asp:Panel ID="pnlSchoolType" runat="server">
            <cc1:HyperLinkPlus ID="linkSTYP_ALLTypes" runat="server" ParamName="STYP" ParamValue="1">All Types</cc1:HyperLinkPlus>            
            •
            <cc1:HyperLinkPlus ID="linkSTYP_Elem" runat="server" ParamName="STYP" ParamValue="6">Elem</cc1:HyperLinkPlus>            
            •
            <cc1:HyperLinkPlus ID="linkSTYP_Mid" runat="server" ParamName="STYP" ParamValue="5">Mid/Jr Hi</cc1:HyperLinkPlus>            
            •
            <cc1:HyperLinkPlus ID="linkSTYP_Hi" runat="server" ParamName="STYP" ParamValue="3">High</cc1:HyperLinkPlus>            
            •
            <cc1:HyperLinkPlus ID="linkSTYP_ElSec" runat="server" ParamName="STYP" ParamValue="7">El/Sec</cc1:HyperLinkPlus>            
            •
            <cc1:HyperLinkPlus ID="linkSTYP_StateSummary" runat="server" ParamName="STYP" ParamValue="9">State Summary</cc1:HyperLinkPlus>            
            </asp:Panel>
        </td>
    </tr>
    <tr id="trGroup" runat="server">
        <td>View By</td>
        <td>
        <cc1:HyperLinkPlus ID="linkGroup_AllStudents" runat="server" ParamName="GROUP" ParamValue="AllStudentsFAY">All Students</cc1:HyperLinkPlus>
        • 
        <cc1:HyperLinkPlus ID="linkGroup_Gender" runat="server" ParamName="GROUP" ParamValue="Gender">Gender</cc1:HyperLinkPlus>        
        • 
        <cc1:HyperLinkPlus ID="linkGroup_RaceEthnicity" runat="server" ParamName="GROUP" ParamValue="RaceEthnicity">Race/Ethnicity</cc1:HyperLinkPlus>
        • 
        <cc1:HyperLinkPlus ID="linkGroup_Grade" runat="server" ParamName="GROUP" ParamValue="Grade">Grade</cc1:HyperLinkPlus>        
        • 
        <cc1:HyperLinkPlus ID="linkDisability" runat="server" ParamName="GROUP" ParamValue="Disability">Disability</cc1:HyperLinkPlus>        
        </td>
    </tr>
    <tr id="trCred" runat="server">
        <td>Show</td>
        <td>
        <cc1:HyperLinkPlus ID="linkCred_Required" runat="server" ParamName="cred" ParamValue="R">Subjects Required by State Law</cc1:HyperLinkPlus>
        • 
        <cc1:HyperLinkPlus ID="linkCred_Additional" runat="server" ParamName="cred" ParamValue="A">Additional Subjects</cc1:HyperLinkPlus>
        </td>
    </tr>
    <tr id="trWMAS" runat="server">
        <td>Subject</td>
        <td>        
            <cc1:HyperLinkPlus ID="linkWMAS_English" runat="server" ParamName="STYP" ParamValue="4">English Lanugage Arts</cc1:HyperLinkPlus>            
            •
            <cc1:HyperLinkPlus ID="linkWMAS_Math" runat="server" ParamName="STYP" ParamValue="8">Mathematics</cc1:HyperLinkPlus>            
            •
            <cc1:HyperLinkPlus ID="linkWMAS_Science" runat="server" ParamName="STYP" ParamValue="10">Science</cc1:HyperLinkPlus>            
            •
            <cc1:HyperLinkPlus ID="linkWMAS_SocialStudies" runat="server" ParamName="STYP" ParamValue="11">Social Studies</cc1:HyperLinkPlus>            
            •
            <cc1:HyperLinkPlus ID="linkWMAS_Art" runat="server" ParamName="STYP" ParamValue="2">Art and Design</cc1:HyperLinkPlus>            
            •
            <cc1:HyperLinkPlus ID="linkWMAS_Languages" runat="server" ParamName="STYP" ParamValue="6">Foreign Languages</cc1:HyperLinkPlus>            
            •
            <cc1:HyperLinkPlus ID="linkWMAS_Music" runat="server" ParamName="STYP" ParamValue="9">Music</cc1:HyperLinkPlus>            
            •
            <cc1:HyperLinkPlus ID="linkWMAS_Other" runat="server" ParamName="STYP" ParamValue="13">Other</cc1:HyperLinkPlus>            
        </td>
    </tr>
    <tr id="trCompareTo" runat="server">
        <td>Compare To</td>
        <td>
        <cc1:HyperLinkPlus ID="linkCompareTo_PriorYears" runat="server" ParamName="CompareTo" ParamValue="PRIORYEARS">Prior Years</cc1:HyperLinkPlus>        
        • 
        <cc1:HyperLinkPlus ID="linkCompareTo_DistState" runat="server" ParamName="CompareTo" ParamValue="DISTSTATE">District/State</cc1:HyperLinkPlus>        
        • 
        <cc1:HyperLinkPlus ID="linkCompareTo_SelSchools" runat="server" ParamName="CompareTo" ParamValue="SELSCHOOLS">Selected Schools</cc1:HyperLinkPlus>        
        • 
        <cc1:HyperLinkPlus ID="linkCompareTo_CurrentOnly" runat="server" ParamName="CompareTo" ParamValue="CURRENTONLY">Current School Data</cc1:HyperLinkPlus>        
        </td>
    </tr>
</table>






