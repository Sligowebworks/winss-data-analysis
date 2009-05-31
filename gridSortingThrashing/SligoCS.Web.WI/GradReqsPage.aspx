<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="GradReqsPage.aspx.cs" Inherits="SligoCS.Web.WI.GradReqsPage" Title="Requirements for High School Graduation" %>

<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>
        <%@ Register Src="~/WebUserControls/NavigationLinkRow.ascx"  TagName="NavigationLinkRow" TagPrefix="sli"%>
<%@ Register Src="~/WebUserControls/NavCompareTo.ascx" TagName="NavCompareTo" TagPrefix="sli" %>
    
<%@ Register Src="WebUserControls/BottomLinks.ascx" TagName="BottomLinks" TagPrefix="uc1" %>
<%@ Register Src="WebUserControls/BottomLinkMoreData.ascx" TagName="BottomLinkMoreData" TagPrefix="uc5" %>
<%@ Register Src="WebUserControls/BottomLinkViewReport.ascx" TagName="BottomLinkViewReport" TagPrefix="uc6" %>
<%@ Register Src="WebUserControls/BottomLinkViewProfile.ascx" TagName="BottomLinkViewProfile" TagPrefix="uc7" %>
<%@ Register Src="WebUserControls/BottomLinkDownload.ascx" TagName="BottomLinkDownload" TagPrefix="uc8" %>
<%@ Register Src="WebUserControls/BottomLinkWhatToConsider.ascx" TagName="BottomLinkWhatToConsider" TagPrefix="uc9" %>
<%@ Register Src="WebUserControls/BottomLinkEnrollmentCounts.ascx" TagName="BottomLinkEnrollmentCounts" TagPrefix="uc10" %>
<%@ Register Src="WebUserControls/BottomLinkWhyNotReported.ascx" TagName="BottomLinkWhyNotReported" TagPrefix="uc11" %>
<%@ Register tagPrefix="Graph" namespace="SligoCS.Web.WI.WebUserControls" Assembly="SligoCS.Web.WI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>
    <tr>
        <td>
            <sli:NavigationLinkRow ID="nlrShow" runat="server">
                <RowLabel>Show:</RowLabel>
                 <NavigationLinks>
                    <cc1:HyperLinkPlus ID="HyperLinkPlus1" runat="server" ParamName="Show" ParamValue="1">Subjects Required By State Law</cc1:HyperLinkPlus> 
                     <cc1:HyperLinkPlus ID="HyperLinkPlus2" runat="server" ParamName="Show" ParamValue="1">Additional Subjects</cc1:HyperLinkPlus> 
                 </NavigationLinks>
            </sli:NavigationLinkRow>
            
            <sli:NavCompareTo ID="nlrCompareTo" runat="server" />
            
        </td>
    </tr>
    <tr>
        <td>
            <asp:hyperlink ID="ChangeSelectedSchoolOrDistrict" Font-Size="Small" runat="server" />
        </td>
    </tr>    
    <tr>
        <td><br />School level data are not available.</td>
    </tr>
    <tr>
        <td>
            <slx:SligoDataGrid ID="SligoDataGrid2" runat="server" AutoGenerateColumns="true"
                ShowSuperHeader="False"  OnRowDataBound="SligoDataGrid2_RowDataBound" UseAccessibleHeader="False" 
                BorderColor="#888888" BorderWidth="4px" BorderStyle="Double" Width="460" >
               
            </slx:SligoDataGrid>
        </td>
    </tr>
    <tr>
        <td>
            <uc1:BottomLinks ID="BottomLinks1" runat="server" />
        </td>
    </tr>
</table>        
</asp:Content>
