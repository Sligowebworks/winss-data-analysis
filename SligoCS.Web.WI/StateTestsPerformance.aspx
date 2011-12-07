<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="StateTestsPerformance.aspx.cs" Inherits="SligoCS.Web.WI.StateTestsPerformance" title="Proficiency Levels"%>
<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>
 <%@ Register TagPrefix="Graph" namespace="SligoCS.Web.WI.WebUserControls" Assembly="SligoCS.Web.WI" %>
<%@ Register TagPrefix="sli" namespace="SligoCS.Web.WI.WebUserControls" Assembly="SligoCS.Web.WI" %>
<%@ Register Src="~/WebUserControls/NavigationLinkRow.ascx"  TagName="NavigationLinkRow" TagPrefix="sli"%>
<%@ Register Src="~/WebUserControls/NavCompareTo.ascx" TagName="NavCompareTo" TagPrefix="sli" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table style="padding:0px 0px 0px 0px; width:400px;">
    <tr>
        <td>
                <sli:NavSelectYear ID="nlrSelectYear" runat="server" /> 
                <sli:NavigationLinkRow ID="nlrGrade" runat="server">
                    <RowLabel>Grade: </RowLabel>
                    <NavigationLinks>
                         <cc1:HyperLinkPlus ID="linkGradeAllDisAgg" runat="server" ParamName="Grade" ParamValue="0">All Tested Grades</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkGradeGrade_3" runat="server" ParamName="Grade" ParamValue="28">3</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkGradeGrade_4" runat="server" ParamName="Grade" ParamValue="32">4</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkGradeGrade_5" runat="server" ParamName="Grade" ParamValue="36">5</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkGradeGrade_6" runat="server" ParamName="Grade" ParamValue="40">6</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkGradeGrade_7" runat="server" ParamName="Grade" ParamValue="44">7</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkGradeGrade_8" runat="server" ParamName="Grade" ParamValue="48">8</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkGradeGrade_10" runat="server" ParamName="Grade" ParamValue="56">10</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkGradeCombined" runat="server" ParamName="Grade" ParamValue="99">Combined Grades</cc1:HyperLinkPlus>
                    </NavigationLinks>
                </sli:NavigationLinkRow>
        
                <sli:NavigationLinkRow ID="nlrSubject" runat="server">
                    <RowLabel>Subject: </RowLabel>
                    <NavigationLinks>
                        <cc1:HyperLinkPlus ID="linkSubjectIDAllTested" runat="server" ParamName="SubjectID" ParamValue="0AS">All Tested Subjects</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkSubjectIDReading" runat="server" ParamName="SubjectID" ParamValue="1RE">Reading</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkSubjectIDLanguage" runat="server" ParamName="SubjectID" ParamValue="2LA">Language Arts</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkSubjectIDMath" runat="server" ParamName="SubjectID" ParamValue="3MA">Mathematics</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkSubjectIDScience" runat="server" ParamName="SubjectID" ParamValue="4SC">Science</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkSubjectIDSocialStudies" runat="server" ParamName="SubjectID" ParamValue="5SS">Social Studies</cc1:HyperLinkPlus>
                    </NavigationLinks>
                </sli:NavigationLinkRow>

                <sli:NavigationLinkRow ID="nlrLevel" runat="server">
                    <RowLabel>Level: </RowLabel>
                    <NavigationLinks>
                        <cc1:HyperLinkPlus ID="linkLevelAll" runat="server" ParamName="Level" ParamValue="ALL">All Levels</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkLevelAdvanced" runat="server" ParamName="Level" ParamValue="ADV">Advanced</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkLevelAdvancedProficient" runat="server" ParamName="Level" ParamValue="A-P">Advanced + Proficient</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkLevelBasicMinSkillEng" runat="server" ParamName="Level" ParamValue="B-M-NT">Basic + Min Perf + WAA SwD/ELL + No WSAS</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkLevelWAA_SwD" runat="server" ParamName="Level" ParamValue="SWD">WAA SwD</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkLevelWAA_ELL" runat="server" ParamName="Level" ParamValue="ELL">WAA ELL</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkLevelNoWSAS" runat="server" ParamName="Level" ParamValue="No-W">No WSAS</cc1:HyperLinkPlus>
                    </NavigationLinks>
                </sli:NavigationLinkRow>

            <sli:NavigationLinkRow ID="nlrWkceCombined" runat="server">
                    <RowLabel><!-- --></RowLabel>
                    <NavigationLinks>
                            <cc1:HyperLinkPlus ID="linkWOWWSASCombined" runat="server" ParamName="WOW" ParamValue="WSAS">WSAS: WKCE and WAA Combined</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkWOWWKCE" runat="server" ParamName="WOW" ParamValue="WKCE">WKCE Only</cc1:HyperLinkPlus>
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
            <Graph:GraphBarChart ID="barChart" runat="server"></Graph:GraphBarChart>
            	<span class="text"><br />
		        <!-- direct cut n' paste from old asp, html comments too :: MZD Jan '09 -->
				<br />* <a href="javascript:popup('http://dpi.wi.gov/oea/profdesc.html')" onclick='setCookie(question, url)'>Proficiency data for November 2002 and later are not comparable to earlier years.</a>  Some subject area tests are given only at grades 4, 8, and 10. 
			    <br />Due to 2010-11 race/ethnicity reporting changes, pre- and post-2010-11 data by race/ethnicity may not be comparable. <a href="javascript:popup('http://www.dpi.wi.gov/spr/demog_q&a.html')" onClick="setCookie(question, url)">[More]</a>
			    <br />FAY = full academic year. <a href="javascript:popup('http://dpi.wi.gov/oea/kce_q&a.html')" onclick='setCookie(question, url)'>What are WSAS, WKCE, and WAA?</a><br />
	</span>
        </td>
    </tr>    
    </asp:Panel>

      <tr>
        <td>
            <slx:WinssDataGrid ID="StateTestsDataGrid" runat="server" >
                <Columns>
                    <slx:WinssDataGridColumn DataField="GroupNum" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="LinkedName"  HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="YearFormatted" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="District Name"  HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="OrgSchoolTypeLabel" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="OrgLevelLabel" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="SchoolTypeLabel" HeaderText="School Type" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="GradeLabel"  HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="SubjectLabel" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="SexLabel" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="GroupName" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="RaceLabel" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="EconDisadvLabel" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="DisabilityLabel" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="ELPLabel" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="MigrantLabel" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="Level" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn DataField="Enrolled"  HeaderText="Enrolled at Test Time" FormatString="#,##0.###" />
                    <slx:WinssDataGridColumn DataField="Included" HeaderText="Number Included in Percents" FormatString="#,##0.###" />
                    <slx:WinssDataGridColumn DataField="Excused-By-Parent" HeaderText="No WSAS - Excused By Parent"  FormatString="#,##0.0%"  />
                    <slx:WinssDataGridColumn DataField="Eligible-But-Not-Tested" HeaderText="No WSAS - Reasons Unknown"  FormatString="#,##0.0%"  />
                    <slx:WinssDataGridColumn DataField="No WSAS Total" FormatString="#,##0.0%"  />
                    <slx:WinssDataGridColumn DataField="MinPerfWSAS" HeaderText="Min Perf" FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="BasicWSAS" HeaderText="Basic" FormatString="#,##0.0%"  />
                    <slx:WinssDataGridColumn DataField="ProficientWSAS" HeaderText="Proficient" FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="AdvancedWSAS" HeaderText="Advanced" FormatString="#,##0.0%" />
                    <slx:WinssDatagridColumn DataField="Percent Minimal"  HeaderText="Min Perf" FormatString="#,##0.0%" />
                    <slx:WinssDatagridColumn DataField="Percent Basic"  HeaderText="Basic" FormatString="#,##0.0%" />
                    <slx:WinssDatagridColumn DataField="Percent Proficient"  HeaderText="Proficient" FormatString="#,##0.0%" />
                    <slx:WinssDatagridColumn DataField="Percent Advanced"  HeaderText="Advanced" FormatString="#,##0.0%" />
                    <slx:WinssDatagridColumn DataField="BasicPlusMinPerfPlusNoWSASTotalWSAS"  
                            HeaderText="Basic + Min Perf + No WSAS Total" FormatString="#,##0.0%" /> 
                    <slx:WinssDatagridColumn DataField="BasicPlusMinPerfPlusPre-ReqSkillsEngPlusNoWSASTotal"  
                            HeaderText="Basic + Min Perf + WAA SwD/ELL + No WSAS Total" FormatString="#,##0.0%" /> 
                    <slx:WinssDatagridColumn DataField="AdvancedPlusProficientTotalWSAS"  HeaderText="Advanced + Proficient Total" FormatString="#,##0.0%" />
                    <slx:WinssDatagridColumn DataField="PCTAdvPlusPCTPrf"  HeaderText="Advanced + Proficient Total" FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="Percent Pre-Req Skill Level 1" HeaderText="WAA SwD Min Perf" FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="Percent Pre-Req Skill Level 2" HeaderText="WAA SwD Basic" FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="Percent Pre-Req Skill Level 3" HeaderText="WAA SwD Proficient" FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="Percent Pre-Req Skill Level 4" HeaderText="WAA SwD Advanced" FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="PctTotalWAADisabil" HeaderText="WAA SwD Total" FormatString="#,##0.0%" />                     
                    <slx:WinssDatagridColumn DataField="Percent Pre-Req Skill"  HeaderText="WAA SwD Total" FormatString="#,##0.0%" />
                    
                    <slx:WinssDataGridColumn DataField="Percent Pre-Req Eng Minimal" HeaderText="WAA-ELL Min Perf" FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="Percent Pre-Req Eng Basic" HeaderText="WAA-ELL Basic" FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="Percent Pre-Req Eng Proficient" HeaderText="WAA-ELL Proficient" FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="Percent Pre-Req Eng Advanced" HeaderText="WAA-ELL Advanced"  FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="PctTotalWAALep" HeaderText="WAA-ELL Total" FormatString="#,##0.0%" /> 
                    <slx:WinssDatagridColumn DataField="Percent Pre-Req Eng"  HeaderText="WAA-ELL Total" FormatString="#,##0.0%" />
                    
                </Columns>
            </slx:WinssDataGrid>
        </td>
    </tr>

     <tr>
        <td>
        <div class="text"><A href=javascript:popup('http://dpi.wi.gov/oea/amo.html') onClick='setCookie(question, url)'>View comparison to annual measurable objectives</A></div>
        
            <sli:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            <sli:BottomLinkViewDistrictReport id="BottomLinkViewDistrictReport1" runat="server"/>
            <sli:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>
            <sli:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
            <sli:BottomLinkDownload id="BottomLinkDownload1" runat="server" />
            <div class="text">
	            <a href="javascript:popup('http://dpi.wi.gov/oea/naeprslt.html')" onclick="setCookie(question, url)">National Assessment of Educational Progress – Wisconsin Results</a></div>
            <div class="text"><br />
	            <a href="javascript:popup('http://dpi.wi.gov/spr/kce_use.html')" onclick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
            </div>        
        </td>
    </tr>
    
 </table>        
</asp:Content>
