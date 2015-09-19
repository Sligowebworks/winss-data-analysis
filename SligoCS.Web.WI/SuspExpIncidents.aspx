<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="SuspExpIncidents.aspx.cs" Inherits="SligoCS.Web.WI.SuspExpIncidents" Title="Types of Incidents Resulting in Suspensions and Expulsions" %>
<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>
<%@ Register Src="~/WebUserControls/NavigationLinkRow.ascx"  TagName="NavigationLinkRow" TagPrefix="sli"%>
<%@ Register Src="~/WebUserControls/NavCompareTo.ascx" TagName="NavCompareTo" TagPrefix="sli" %>
<%@ Register Src="~/WebUserControls/NavSchoolType.ascx" TagName="NavSchoolType" TagPrefix="sli"%>

<%@ Register Src="WebUserControls/BottomLinkMoreData.ascx" TagName="BottomLinkMoreData" TagPrefix="uc5" %>
<%@ Register Src="WebUserControls/BottomLinkViewReport.ascx" TagName="BottomLinkViewReport" TagPrefix="uc6" %>
<%@ Register Src="WebUserControls/BottomLinkViewProfile.ascx" TagName="BottomLinkViewProfile" TagPrefix="uc7" %>
<%@ Register Src="WebUserControls/BottomLinkDownload.ascx" TagName="BottomLinkDownload" TagPrefix="uc8" %>
<%@ Register Src="WebUserControls/BottomLinkWhatToConsider.ascx" TagName="BottomLinkWhatToConsider" TagPrefix="uc9" %>
<%@ Register Src="WebUserControls/BottomLinkEnrollmentCounts.ascx" TagName="BottomLinkEnrollmentCounts" TagPrefix="uc10" %>
<%@ Register Src="WebUserControls/BottomLinkWhyNotReported.ascx" TagName="BottomLinkWhyNotReported" TagPrefix="uc11" %>

<%@ Register tagPrefix="Graph" namespace="SligoCS.Web.WI.WebUserControls" Assembly="SligoCS.Web.WI" %>
<%@ Register Assembly="ChartFX.WebForms" Namespace="ChartFX.WebForms" TagPrefix="chartfx7" %>
<%@ Register Assembly="ChartFX.WebForms.Adornments" Namespace="ChartFX.WebForms.Adornments"
    TagPrefix="chartfxadornments" %>
<%@ Register Assembly="ChartFX.WebForms" Namespace="ChartFX.WebForms.Galleries" TagPrefix="chartfx7galleries" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table style="padding:0px 0px 0px 0px; width:400px;">
    <tr>
        <td> 
        <sli:NavSelectYear ID="nlrSelectYear" runat="server" /> 
        <sli:NavSchoolType ID="nlrSTYP" runat="server" />
         <sli:NavigationLinkRow ID="nlrShow" runat="server">
                <RowLabel>Show:</RowLabel> 
                <NavigationLinks>
                  <cc1:HyperLinkPlus ID="linkIncidentRate" runat="server" ParamName="Incident" ParamValue="RATE">Incident Rate</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkIncidentConsequences" runat="server" ParamName="Incident" ParamValue="CONS">Disciplinary Consequences</cc1:HyperLinkPlus>
                </NavigationLinks>
         </sli:NavigationLinkRow>
        <sli:NavigationLinkRow ID="NavigationLinkRow1" runat="server">
                <RowLabel>Type:</RowLabel> 
                <NavigationLinks>
                   <cc1:HyperLinkPlus ID="linkWeaponYes" runat="server" ParamName="Weapon" ParamValue="YES">Weapon/Drug Related</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkWeaponNo" runat="server" ParamName="Weapon" ParamValue="NO">Not Weapon/Drug Related</cc1:HyperLinkPlus>
                </NavigationLinks>
         </sli:NavigationLinkRow> 
        <sli:NavViewByGroup ID="nlrVwByGroup" runat="server" />
        <sli:NavCompareTo ID="nlrCompareTo" runat="server" />
        <sli:ChangeSelectedSchoolOrDistrictLink ID="ChangeSelectedSchoolOrDistrictLink1" Font-Size="Small" runat="server" />
        </td>
    </tr>
    <asp:Panel ID="GraphPanel" runat="server">    
    <tr>
        <td>
           <Graph:GraphBarChart ID="barChart" runat="server" Visible ="false"></Graph:GraphBarChart>
           <Graph:GraphHorizBarChart ID="hrzBarChart" runat="server" Visible ="false"></Graph:GraphHorizBarChart>
              <span class="text"><br><br>
Major changes in WI discipline data collection systems were implemented in 2006-07. WINSS data about suspensions and expulsions were included in this transition year collection and are not comprehensive so should be interpreted with caution. See other <a href="javascript:popup('http://spr.dpi.wi.gov/spr_discip_q&a')" onClick="setCookie(question, url)">cautions</a> and information about discipline data on WINSS.
</span>
        </td>
        </tr>
        </asp:Panel>
        <tr>
            <td>
                &nbsp;
               <slx:WinssDataGrid ID="SuspExpIncDataGrid" runat="server">
                <Columns>     
                    <slx:WinssDataGridColumn MergeRows="true" DataField="YearFormatted" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="LinkedName" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="District Name" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="SchooltypeLabel" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="OrgSchoolTypeLabel" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="RaceLabel" HeaderText="&nbsp;"  />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="SexLabel" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="GradeLabel"  HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="DisabilityLabel"  HeaderText="&nbsp;" />
                     <slx:WinssDataGridColumn DataField="Enrollment" HeaderText="Total Fall Enrollment Grades PreK&#8209;12**" FormatString="#,##0.###" />
                    <slx:WinssDataGridColumn DataField="IncidentCountTotalWeaponDrug" FormatString="#,##0.###" HeaderText="Number of Incidents" />
                    <slx:WinssDataGridColumn DataField="IncidentCountTotalNonWeaponDrug" FormatString="#,##0.###" HeaderText="Number of Incidents" />
                    <slx:WinssDataGridColumn DataField="WeaponDrugIncidentRate" HeaderText="Incidents per 1,000 Students" FormatString="#,##0.0" />
                    <slx:WinssDataGridColumn DataField="NonWeaponDrugIncidentRate" HeaderText="Incidents per 1,000 Students" FormatString="#,##0.0" />
                    <slx:WinssDataGridColumn DataField="%WeaponDrugAllSusp" FormatString="#,##0.0%" HeaderText="% Suspended" />
                    <slx:WinssDataGridColumn DataField="%WeaponDrugExp" HeaderText="% Expelled" FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="%NonWeaponDrugAllSusp" FormatString="#,##0.0%" HeaderText="% Suspended" />
                    <slx:WinssDataGridColumn DataField="%NonWeaponDrugExp" HeaderText="% Expelled" FormatString="#,##0.0%" />
                    </Columns>
                </slx:WinssDataGrid>
            </td>
        </tr>
            <tr>
        <td>
        <asp:Panel ID="DefPanel" runat="server">
	        <div class="text">
	        ** Enrollment counts in this column may cover a narrower grade range if the "view by: grade" option is selected or if counts are for a specific "school type" (e.g. High School). <a href="javascript:popup('http://spr.dpi.wi.gov/spr_demog_q&a')" onclick="setCookie(question, url)">[More]</a>
	        </div>
        </asp:Panel>
            <sli:BottomLinkViewAccountabilityReport id="BottomLinkViewAccountabilityReport" runat="server"/>
            <sli:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            <sli:BottomLinkViewDistrictReport id="BottomLinkViewDistrictReport1" runat="server"/>
            <sli:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>
            <sli:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
            <sli:BottomLinkDownload id="BottomLinkDownload1" runat="server" Col="16"/>
            <SPAN class="text">
	            <p><a href="javascript:popup('http://spr.dpi.wi.gov/spr_discip_use')" onClick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
            </SPAN>        
        </td>
    </tr>
    </table>
</asp:Content>
