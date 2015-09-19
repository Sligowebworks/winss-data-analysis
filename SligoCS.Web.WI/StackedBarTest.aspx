<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="StackedBarTest.aspx.cs" Inherits="SligoCS.Web.WI.StackedBarTest" Title="Untitled Page" %>
<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"  TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table style="padding:0px 0px 0px 0px; width:400px;">
    <tr>
        <td>
        <sli:NavigationLinkRow ID="nlrHighSchoolCompletion" runat="server">
            <RowLabel>Credential: </RowLabel>
            <NavigationLinks>
                <cc1:HyperLinkPlus ID="linkHighSchoolCompletionAll" runat="server" ParamName="HighSchoolCompletion" ParamValue="All">All Types</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkHighSchoolCompletionCertificate" runat="server" ParamName="HighSchoolCompletion" ParamValue="CERT">Certificate</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkHighSchoolCompletionHSED" runat="server" ParamName="HighSchoolCompletion" ParamValue="HSED">HSED</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkHighSchoolCompletionRegular" runat="server" ParamName="HighSchoolCompletion" ParamValue="REG">Regular Diploma</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkHighSchoolCompletionSummary" runat="server" ParamName="HighSchoolCompletion" ParamValue="COMB">Combined</cc1:HyperLinkPlus>
            </NavigationLinks>
        </sli:NavigationLinkRow>

        <sli:NavViewByGroup ID="nlrVwByGroup" runat="server" />
        <sli:NavCompareTo ID="nlrCompareTo" runat="server" />
        <sli:ChangeSelectedSchoolOrDistrictLink id="ChangeSelectedSchoolOrDistrict" runat="server" />
        </td>
    </tr>
    
    <asp:Panel ID="GraphPanel" runat="server">    
    <tr>
        <td>
<%--            <chartfx7:Chart ID="ChartOnPage" run at="server">
            </chartfx7:Chart>--%>
           <Graph:GraphBarChart ID="barChart" runat="server"></Graph:GraphBarChart>
        </td>
    </tr>    
    </asp:Panel>
    </table>
</asp:Content>
