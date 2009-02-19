<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="GradReqsPage.aspx.cs" Inherits="SligoCS.Web.WI.GradReqsPage" Title="Requirements for High School Graduation" %>

<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.WI.WebServerControls.WI"
    TagPrefix="cc1" %>
<%@ Register Src="WebUserControls/BottomLinks.ascx" TagName="BottomLinks" TagPrefix="uc1" %>
<%@ Register Src="WebUserControls/ParamsLinkBox.ascx" TagName="ParamsLinkBox" TagPrefix="uc2" %>
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
        <td><uc2:ParamsLinkBox ID="ParamsLinkBox1" runat="server" ShowTR_GradReqs="true"  ShowTR_CompareTo="true"/></td>
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
            <cc1:SligoDataGrid ID="SligoDataGrid2" runat="server" AutoGenerateColumns="true"
                ShowSuperHeader="False"  OnRowDataBound="SligoDataGrid2_RowDataBound" UseAccessibleHeader="False" 
                BorderColor="#888888" BorderWidth="4px" BorderStyle="Double" Width="460" >
               
            </cc1:SligoDataGrid>
        </td>
    </tr>
    <tr>
        <td>
            <uc1:BottomLinks ID="BottomLinks1" runat="server" />
        </td>
    </tr>
</table>        
</asp:Content>
