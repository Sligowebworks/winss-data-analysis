<%@ Page Language="C#"   MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="ComparePerformance.aspx.cs" Inherits="SligoCS.Web.WI.ComparePerformance" Title="Proficiency Levels" %>
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
<%@ Register Src="WebUserControls/BottomLinkAMO.ascx" TagName="BottomLinkAMO" TagPrefix="uc12" %>
<%@ Register tagPrefix="Graph" namespace="SligoCS.Web.WI.WebUserControls" Assembly="SligoCS.Web.WI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>
    <tr>
        <td>
        <sli:NavSelectYear ID="nlrSelectYear" runat="server" /> 

        <sli:NavigationLinkRow ID="nlrGrade" runat="server">
            <RowLabel>Grade:</RowLabel>
            <NavigationLinks>
                <cc1:HyperLinkPlus ID="linkGradeGrade_3" runat="server" ParamName="Grade" ParamValue="28">3</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkGradeGrade_4" runat="server" ParamName="Grade" ParamValue="32">4</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkGradeGrade_5" runat="server" ParamName="Grade" ParamValue="36">5</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkGradeGrade_6" runat="server" ParamName="Grade" ParamValue="40">6</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkGradeGrade_7" runat="server" ParamName="Grade" ParamValue="44">7</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkGradeGrade_8" runat="server" ParamName="Grade" ParamValue="48">8</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkGradeGrade_10" runat="server" ParamName="Grade" ParamValue="56">10</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkGradeCombined_PreK_12" runat="server" ParamName="Grade" ParamValue="99">Combined Grades</cc1:HyperLinkPlus>
            </NavigationLinks>
        </sli:NavigationLinkRow>
        <sli:NavigationLinkRow ID="nlrSubject" runat="server">
            <RowLabel>Subject: </RowLabel>
            <NavigationLinks>
                <cc1:HyperLinkPlus ID="linkSubjectIDReading" runat="server" ParamName="SubjectID" ParamValue="1RE">Reading</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkSubjectIDLanguage" runat="server" ParamName="SubjectID" ParamValue="2LA">Language Arts</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkSubjectIDMath" runat="server" ParamName="SubjectID" ParamValue="3MA">Mathematics</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkSubjectIDScience" runat="server" ParamName="SubjectID" ParamValue="4SC">Science</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkSubjectIDSocialStudies" runat="server" ParamName="SubjectID" ParamValue="5SS">Social Studies</cc1:HyperLinkPlus>
            </NavigationLinks>
        </sli:NavigationLinkRow>
        
        <sli:NavigationLinkRow ID="nlrWkceCombined" runat="server">
            <RowLabel><!-- --></RowLabel>
            <NavigationLinks>
                <cc1:HyperLinkPlus ID="linkWOWWSASCombined" runat="server" ParamName="WOW" ParamValue="WSAS">WSAS: WKCE and WAA Combined</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkWOWWKCE" runat="server" ParamName="WOW" ParamValue="WKCE">WKCE Only</cc1:HyperLinkPlus>
            </NavigationLinks>
        </sli:NavigationLinkRow>
        
    </td></tr>

     <asp:Panel ID="GraphPanel" runat="server">    
    <tr>
        <td>
            <Graph:GraphBarChart ID="barChart" runat="server"></Graph:GraphBarChart>
            	<div class="text">
            	  	        
				<br />* WINSS proficiency data for Nov 2002 through Nov 2011 are not comparable to data for earlier years, and reading and math proficiency data are not comparable to later years, due to cut score adjustments in 2002 and <a href="http://dpi.wi.gov/oea_wkce-crtcuts" target="_blank">2012</a>.   Some subject area tests are given only at grades 4, 8, and 10.  
			
			<br /> Due to <a href="javascript:popup('http://spr.dpi.wi.gov/spr_demog_q&a#race')" onClick="setCookie(question, url)">2010-11 race/ethnicity reporting</a> changes, pre- and post-2010-11 data by race/ethnicity may not be comparable. 
		
		<br />FAY = full academic year. <a href="javascript:popup('http://oea.dpi.wi.gov/oea_kce_q&a')" onclick='setCookie(question, url)'>What are WSAS, WKCE, and WAA? [More]</a>
	            </div>
        </td>
    </tr>    
    </asp:Panel>
      <tr>
        <td>
            <slx:WinssDataGrid ID="CompareContinuingDataGrid" runat="server">
                <Columns>
                    <slx:WinssDatagridColumn MergeRows="true" DataField="YearFormatted" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="LinkedName"  HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="District Name"  HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="OrgSchoolTypeLabel" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="OrgLevelLabel" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="SchoolTypeLabel" HeaderText="School Type" />
                    <slx:WinssDatagridColumn DataField="RaceLabel" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn DataField="SexLabel" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn DataField="GradeLabel"  HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn DataField="FayLabel"  HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn DataField="Enrolled"  HeaderText="" FormatString="#,##0.###" />
                    <slx:WinssDatagridColumn DataField="No WSAS"  HeaderText="No WSAS" FormatString="#,##0.0%" />
                    <slx:WinssDatagridColumn DataField="No WSAS Total"  HeaderText="No WSAS" FormatString="#,##0.0%" />
                    <slx:WinssDatagridColumn DataField="Percent Pre-Req Skill"  HeaderText="WAA-SwD" FormatString="#,##0.0%" />
                    <slx:WinssDatagridColumn DataField="Percent Pre-Req Eng"  HeaderText="WAA-ELL" FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="MinPerfWSAS" HeaderText="Min Perf" FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="BasicWSAS" HeaderText="Basic" FormatString="#,##0.0%"  />
                    <slx:WinssDataGridColumn DataField="ProficientWSAS" HeaderText="Proficient" FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="AdvancedWSAS" HeaderText="Advanced" FormatString="#,##0.0%" />
                    <slx:WinssDatagridColumn DataField="Percent Minimal"  HeaderText="Min Perf" FormatString="#,##0.0%" />
                    <slx:WinssDatagridColumn DataField="Percent Basic"  HeaderText="Basic" FormatString="#,##0.0%" />
                    <slx:WinssDatagridColumn DataField="Percent Proficient"  HeaderText="Proficient" FormatString="#,##0.0%" />
                    <slx:WinssDatagridColumn DataField="Percent Advanced"  HeaderText="Advanced" FormatString="#,##0.0%" />
                </Columns>
            </slx:WinssDataGrid>
        </td>
    </tr>

          <tr>
        <td>
         <uc12:BottomLinkAMO id="BottomLinkAMO" runat="server" />
            <sli:BottomLinkViewAccountabilityReport id="BottomLinkViewAccountabilityReport" runat="server"/>
            <sli:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            <uc7:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>
            <uc11:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
            <uc8:BottomLinkDownload id="BottomLinkDownload1" runat="server"/>
            <div class="text">
	            <a href="javascript:popup('http://spr.dpi.wi.gov/spr_ret_use')" onclick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
            </div>        
        </td>
    </tr>

</table>        
</asp:Content>
