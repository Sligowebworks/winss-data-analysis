<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="PostGradIntentPage.aspx.cs" Inherits="SligoCS.Web.WI.PostGradIntentPage" Title="Graduation Plan" %>

<%@ Register Src="WebUserControls/BottomLinks.ascx" TagName="BottomLinks" TagPrefix="uc1" %>
<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI" TagPrefix="cc1" %>
<%@ Register Src="~/WebUserControls/NavigationLinkRow.ascx"  TagName="NavigationLinkRow" TagPrefix="sli"%>
<%@ Register Src="~/WebUserControls/NavCompareTo.ascx" TagName="NavCompareTo" TagPrefix="sli" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>
    <tr>
        <td>
            <sli:NavigationLinkRow ID="nlrShow" runat="server">
                <RowLabel>Show: </RowLabel>
                <NavigationLinks>
                    <cc1:HyperLinkPlus ID="HyperLinkPlus1" runat="server" ParamName="Show" ParamValue="1">All Options</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="HyperLinkPlus2" runat="server" ParamName="Show" ParamValue="1">4-Yr College/University</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="HyperLinkPlus3" runat="server" ParamName="Show" ParamValue="1">Voc/Tec College</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="HyperLinkPlus4" runat="server" ParamName="Show" ParamValue="1">Employment</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="HyperLinkPlus5" runat="server" ParamName="Show" ParamValue="1">Military</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="HyperLinkPlus6" runat="server" ParamName="Show" ParamValue="1">Job Training </cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="HyperLinkPlus7" runat="server" ParamName="Show" ParamValue="1">Seeking Employment</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="HyperLinkPlus8" runat="server" ParamName="Show" ParamValue="1">Other Plans</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="HyperLinkPlus9" runat="server" ParamName="Show" ParamValue="1">Undecided</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="HyperLinkPlus10" runat="server" ParamName="Show" ParamValue="1">No Response</cc1:HyperLinkPlus>
                </NavigationLinks>
            </sli:NavigationLinkRow>
            
            <sli:NavigationLinkRow ID="nlrVwBy" runat="server">
                <RowLabel>View By: </RowLabel>
                <NavigationLinks>
                    <cc1:HyperLinkPlus ID="HyperLinkPlus11" runat="server" ParamName="VwBy" ParamValue="1">All Students</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="HyperLinkPlus12" runat="server" ParamName="VwBy" ParamValue="1">Gender</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="HyperLinkPlus13" runat="server" ParamName="VwBy" ParamValue="1">Race/Ethnicity</cc1:HyperLinkPlus>
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
        <td>
            <slx:SligoDataGrid ID="SligoDataGrid2" runat="server" AutoGenerateColumns="False"
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
            </slx:SligoDataGrid>
        </td>
    </tr>
    <tr>
        <td>
            <uc1:BottomLinks id="BottomLinks1" runat="server">
            </uc1:BottomLinks></td>
    </tr>
</table>        
</asp:Content>
