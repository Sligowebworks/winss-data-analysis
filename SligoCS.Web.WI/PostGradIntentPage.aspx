<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="PostGradIntentPage.aspx.cs" Inherits="SligoCS.Web.WI.PostGradIntentPage" Title="Graduation Plan" %>

<%@ Register Src="WebUserControls/BottomLinks.ascx" TagName="BottomLinks" TagPrefix="uc1" %>
<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>
<%@ Register Src="WebUserControls/ParamsLinkBox.ascx" TagName="ParamsLinkBox" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>
    <tr>
        <td><uc2:ParamsLinkBox ID="ParamsLinkBox1" runat="server" ShowTR_CompareTo="true" ShowTR_Group="true" ShowTR_PostGradShow="true" /></td>
    </tr>
    <tr>
        <td>
            <asp:hyperlink ID="ChangeSelectedSchoolOrDistrict" Font-Size="Small" runat="server" />
        </td>
    </tr> 
    <tr>
        <td>
            <cc1:SligoDataGrid ID="SligoDataGrid2" runat="server" AutoGenerateColumns="False"
                ShowSuperHeader="False" SuperHeaderText="" OnRowDataBound="SligoDataGrid2_RowDataBound" UseAccessibleHeader="False" 
                BorderColor="#888888" BorderWidth="4px" BorderStyle="Double" Width="460" >
                <Columns>
                <cc1:MergeColumn DataField="SexDesc">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="RaceDesc">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="DistState">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="PriorYear">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="Number of Graduates" HeaderText="Number of Graduates" EnableMerge="false">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="Number 4-Year College" HeaderText="Number 4-Year College" EnableMerge="false">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="% 4-Year College" HeaderText="% 4-Year College" EnableMerge="false">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="% Voc/Tech College" HeaderText="% Voc/Tech College" EnableMerge="false">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="% Employment" HeaderText="% Emp." EnableMerge="false">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="% Military" HeaderText="% Military" EnableMerge="false">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="% Job Training" HeaderText="% Job Training" EnableMerge="false">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="% Miscellaneous" HeaderText="% Misc." EnableMerge="false">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                </Columns>
            </cc1:SligoDataGrid>
        </td>
    </tr>
    <tr>
        <td>
            <uc1:BottomLinks id="BottomLinks1" runat="server">
            </uc1:BottomLinks></td>
    </tr>
</table>        
</asp:Content>
