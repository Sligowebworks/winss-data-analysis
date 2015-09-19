<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="PostGradIntentPage.aspx.cs" Inherits="SligoCS.Web.WI.PostGradIntentPage" Title="Graduation Plan" %>

<%@ Register Src="WebUserControls/BottomLinks.ascx" TagName="BottomLinks" TagPrefix="uc1" %>
<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI" TagPrefix="cc1" %>
<%@ Register Src="~/WebUserControls/NavigationLinkRow.ascx"  TagName="NavigationLinkRow" TagPrefix="sli"%>
<%@ Register Src="~/WebUserControls/NavCompareTo.ascx" TagName="NavCompareTo" TagPrefix="sli" %>

<%@ Register tagPrefix="Graph" namespace="SligoCS.Web.WI.WebUserControls" Assembly="SligoCS.Web.WI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>
    <tr>
        <td> 
            <sli:NavSelectYear ID="nlrSelectYear" runat="server" /> 
            <sli:NavigationLinkRow ID="nlrShow" runat="server">
                <RowLabel>Show: </RowLabel>
                <NavigationLinks>
                    <cc1:HyperLinkPlus ID="linkPostGradPlanAll" runat="server" ParamName="PostGradPlan" ParamValue="ALL">All Options</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkPostGradPlanFourYr" runat="server" ParamName="PostGradPlan" ParamValue="4-YR">4-Yr College/University</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkPostGradPlanVocTechCollege" runat="server" ParamName="PostGradPlan" ParamValue="VOC">Voc/Tec College</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkPostGradPlanEmployment" runat="server" ParamName="PostGradPlan" ParamValue="EMP">Employment</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkPostGradPlanMilitary" runat="server" ParamName="PostGradPlan" ParamValue="MILITARY">Military</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkPostGradPlanTraining" runat="server" ParamName="PostGradPlan" ParamValue="JOB">Job Training</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkPostGradPlranSeekEmploy" runat="server" ParamName="PostGradPlan" ParamValue="SEEKEMP">Seeking Employment</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkPostGradPlanOther" runat="server" ParamName="PostGradPlan" ParamValue="OTHER">Other Plans</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkPostGradPlanUndecided" runat="server" ParamName="PostGradPlan" ParamValue="UNDECIDED">Undecided</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkPostGradPlanNoResponse" runat="server" ParamName="PostGradPlan" ParamValue="NO-RESP">No Response</cc1:HyperLinkPlus>
                </NavigationLinks>
            </sli:NavigationLinkRow>
            
            <sli:NavViewByGroup ID="nlrVwBy" runat="server" />
            
            <sli:NavCompareTo ID="nlrCompareTo" runat="server" />
            <sli:ChangeSelectedSchoolOrDistrictLink id="ChangeSelectedSchoolOrDistrictLink1" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:hyperlink ID="ChangeSelectedSchoolOrDistrict" Font-Size="Small" runat="server" />
        </td>
    </tr> 
        <asp:Panel ID="GraphPanel" runat="server">       
    <tr>
        <td><!--Chart goes here.-->
        <asp:Panel ID="pnlBarChart" runat="server" >
            <Graph:GraphBarChart ID="barChart" runat="server" />
        </asp:Panel>
        <asp:Panel ID="pnlHorizChart" runat="server">
            <Graph:GraphHorizBarChart ID="horizChart" runat="server" />
        </asp:Panel>
        </td>
    </tr>
    </asp:Panel>    
    <tr>
        <td>
            <slx:WinssDataGrid ID="PostGradDataGrid" runat="server" >
                <Columns>
                    <slx:WinssDataGridColumn MergeRows="true" DataField="YearFormatted" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="LinkedName" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="District Name" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="OrgLevelLabel" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="SchooltypeLabel" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="OrgSchooltypeLabel" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="RaceLabel" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="SexLabel" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="GradeLabel" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="DisabilityLabel" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="EconDisadvLabel" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="ELPLabel" HeaderText="&nbsp;"  />
                    <slx:WinssDataGridColumn DataField="Number of Graduates" FormatString="#,##0" />
                    <slx:WinssDataGridColumn DataField="Number 4-Year College"  FormatString="#,##0"/>
                    <slx:WinssDataGridColumn DataField="% 4-Year College" FormatString="#,##0.0%"   />
                    <slx:WinssDataGridColumn DataField="Number Voc/Tech College"  FormatString="#,##0"/>
                    <slx:WinssDataGridColumn DataField="% Voc/Tech College" FormatString="#,##0.0%"  />
                    <slx:WinssDataGridColumn DataField="Number Employment"  FormatString="#,##0"/>
                    <slx:WinssDataGridColumn DataField="% Employment" HeaderText="% Emp." FormatString="#,##0.0%"  />
                    <slx:WinssDataGridColumn DataField="Number Military"  FormatString="#,##0"/>
                    <slx:WinssDataGridColumn DataField="% Military" FormatString="#,##0.0%"  />
                    <slx:WinssDataGridColumn DataField="Number Job Training"  FormatString="#,##0"/>
                    <slx:WinssDataGridColumn DataField="% Job Training" FormatString="#,##0.0%"  />
                    <slx:WinssDataGridColumn DataField="Number Miscellaneous"  FormatString="#,##0"/>
                    <slx:WinssDataGridColumn DataField="% Miscellaneous" HeaderText="% Misc." FormatString="#,##0.0%"  />
                    <slx:WinssDataGridColumn DataField="Number Seeking Employment"  FormatString="#,##0"/>
                    <slx:WinssDataGridColumn DataField="% Seeking Employment" FormatString="#,##0.0%"  />
                    <slx:WinssDataGridColumn DataField="Number Other Plans"  FormatString="#,##0"/>
                    <slx:WinssDataGridColumn DataField="% Other Plans" FormatString="#,##0.0%"  />
                    <slx:WinssDataGridColumn DataField="Number Undecided"  FormatString="#,##0"/>
                    <slx:WinssDataGridColumn DataField="% Undecided" FormatString="#,##0.0%"  />
                    <slx:WinssDataGridColumn DataField="Number No Response"  FormatString="#,##0"/>
                    <slx:WinssDataGridColumn DataField="% No Response" FormatString="#,##0.0%"  />
                </Columns>
            </slx:WinssDataGrid>
        </td>
    </tr>
    <tr>
        <td><BR />
            <sli:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            <sli:BottomLinkViewDistrictReport id="BottomLinkViewDistrictReport1" runat="server"/>
            <sli:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>
            <sli:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
            <sli:BottomLinkDownload id="BottomLinkDownload1" runat="server" Col="14"/>
	            <div class="text"><a href="javascript:popup('http://spr.dpi.wi.gov/spr_post_use')" onClick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
            </div>        
            </td>
    </tr>
</table>        
</asp:Content>
