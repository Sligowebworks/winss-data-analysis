<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="StateTestsPerformance.aspx.cs" Inherits="SligoCS.Web.WI.StateTestsPerformance" title="How did students perform on state test at grades 3-8 and 10?"%>
<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>
 <%@ Register Src="WebUserControls/BottomLinkMoreData.ascx" TagName="BottomLinkMoreData" TagPrefix="uc5" %>
<%@ Register Src="WebUserControls/BottomLinkViewReport.ascx" TagName="BottomLinkViewReport" TagPrefix="uc6" %>
<%@ Register Src="WebUserControls/BottomLinkViewProfile.ascx" TagName="BottomLinkViewProfile" TagPrefix="uc7" %>
<%@ Register Src="WebUserControls/BottomLinkDownload.ascx" TagName="BottomLinkDownload" TagPrefix="uc8" %>
<%@ Register Src="WebUserControls/BottomLinkWhatToConsider.ascx" TagName="BottomLinkWhatToConsider" TagPrefix="uc9" %>
<%@ Register Src="WebUserControls/BottomLinkEnrollmentCounts.ascx" TagName="BottomLinkEnrollmentCounts" TagPrefix="uc10" %>
<%@ Register Src="WebUserControls/BottomLinkWhyNotReported.ascx" TagName="BottomLinkWhyNotReported" TagPrefix="uc11" %>
<%@ Register tagPrefix="Graph" namespace="SligoCS.Web.WI.WebUserControls" Assembly="SligoCS.Web.WI" %>
<%@ Register TagPrefix="sli" namespace="SligoCS.Web.WI.WebUserControls" Assembly="SligoCS.Web.WI" %>
<%@ Register Src="~/WebUserControls/NavigationLinkRow.ascx"  TagName="NavigationLinkRow" TagPrefix="sli"%>
<%@ Register Src="~/WebUserControls/NavCompareTo.ascx" TagName="NavCompareTo" TagPrefix="sli" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table style="padding:0px 0px 0px 0px; width:400px;">
    <tr>
        <td>
                <sli:NavigationLinkRow ID="nlrGrade" runat="server">
                    <RowLabel>Grade: </RowLabel>
                    <NavigationLinks>
                        <cc1:HyperLinkPlus ID="gradeAllTested" runat="Server" Enabled="false">All Tested Grades</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="grade3" runat="server" ParamName="Grade" ParamValue="3">3</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="grade4" runat="server" ParamName="Grade" ParamValue="4">4</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="grade5" runat="server" ParamName="Grade" ParamValue="5">5</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="grade6" runat="server" ParamName="Grade" ParamValue="6">6</cc1:HyperLinkPlus>
                         <cc1:HyperLinkPlus ID="grade7" runat="server" ParamName="Grade" ParamValue="7">7</cc1:HyperLinkPlus>
                          <cc1:HyperLinkPlus ID="grade8" runat="server" ParamName="Grade" ParamValue="8">8</cc1:HyperLinkPlus>
                          <cc1:HyperLinkPlus ID="grade9" runat="server" ParamName="Grade" ParamValue="9">9</cc1:HyperLinkPlus>
                           <cc1:HyperLinkPlus ID="grade10" runat="server" ParamName="Grade10" ParamValue="10">10</cc1:HyperLinkPlus>
                            <cc1:HyperLinkPlus ID="gradeCombined" runat="server" Enabled="false">Combined Grades</cc1:HyperLinkPlus>
                    </NavigationLinks>
                </sli:NavigationLinkRow>
        
                <sli:NavigationLinkRow ID="nlrSubject" runat="server">
                    <RowLabel>Subject: </RowLabel>
                    <NavigationLinks>
                        <cc1:HyperLinkPlus ID="HyperLinkPlus1" runat="Server" ParamName="Subject" ParamValue="1">All Tested Subjects</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="HyperLinkPlus2" runat="server" ParamName="Subject" ParamValue="2">Reading</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="HyperLinkPlus3" runat="server" ParamName="Subject" ParamValue="3">Language Arts</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="HyperLinkPlus4" runat="server" ParamName="Subject" ParamValue="4">Mathematics</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="HyperLinkPlus5" runat="server" ParamName="Subject" ParamValue="5">Science</cc1:HyperLinkPlus>
                         <cc1:HyperLinkPlus ID="HyperLinkPlus6" runat="server" ParamName="Subject" ParamValue="6">Social Studies</cc1:HyperLinkPlus>
                    </NavigationLinks>
                </sli:NavigationLinkRow>

                <sli:NavigationLinkRow ID="nlrLevel" runat="server">
                    <RowLabel>Level: </RowLabel>
                    <NavigationLinks>
                        <cc1:HyperLinkPlus ID="HyperLinkPlus7" runat="Server" ParamName="Level" ParamValue="1">All Levels</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="HyperLinkPlus8" runat="server" ParamName="Level" ParamValue="2">Advanced</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="HyperLinkPlus9" runat="server" ParamName="Leve" ParamValue="3">Advanced + Proficient</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="HyperLinkPlus10" runat="server" ParamName="Leve" ParamValue="4">Basic + Min Perf + No WSAS</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="HyperLinkPlus11" runat="server" ParamName="Level" ParamValue="2">No WSAS</cc1:HyperLinkPlus>
                    </NavigationLinks>
                </sli:NavigationLinkRow>

            <sli:NavigationLinkRow ID="nlrWkceCombined" runat="server">
                    <RowLabel><!-- --></RowLabel>
                    <NavigationLinks>
                            <cc1:HyperLinkPlus ID="linkWkceWsasWsas" runat="server" ParamName="WkceWsas" ParamValue="ws">[WSAS: WKCE and WAA Combined</cc1:HyperLinkPlus>
                            <cc1:HyperLinkPlus ID="linkWkceWsasWkcs" runat="server" ParamName="WkceWsas" ParamValue="wk">WKCE Only]</cc1:HyperLinkPlus>
                    </NavigationLinks>
                </sli:NavigationLinkRow>
                
                <sli:NavigationLinkRow ID="nlrViewBy" runat="server">
                    <RowLabel>View By: </RowLabel>
                    <NavigationLinks>
                        <cc1:HyperLinkPlus ID="linkGroupAll" runat="server" ParamName="Group" ParamValue="AllStudentsFAY">All Students</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkGroupGender" runat="server" ParamName="Group" ParamValue="Gender">Gender</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkGroupRace" runat="server" ParamName="Group" ParamValue="RaceEthnicity">Race/Ethnicity</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkGroupGrade" runat="server" ParamName="Group" ParamValue="Grade">Grade</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkGroupDisability" runat="server" ParamName="Group" ParamValue="Disability">Disability</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkGroupEconDisadv" runat="server" ParamName="Group" ParamValue="EconDisadv">Economic Status</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkGroupEngLangProf" runat="server" ParamName="Group" ParamValue="ELP">English Proficiency</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkGroupMigrant" runat="server" ParamName="Group" ParamValue="Mig">Migrant Status</cc1:HyperLinkPlus>
                    </NavigationLinks>
                </sli:NavigationLinkRow>
                <sli:NavCompareTo ID="nlrCompareTo" runat="server" />
        </td>
    </tr><tr>
        <td>
            <asp:hyperlink ID="ChangeSelectedSchoolOrDistrict" Font-Size="Small" runat="server" />
        </td>
    </tr>
         <asp:Panel ID="GraphPanel" runat="server">    
    <tr>
        <td>
            <Graph:GraphBarChart ID="barChart" runat="server"></Graph:GraphBarChart>
            	<span class="text"><br />
		        <!-- direct cut n' paste from old asp, html comments too :: MZD Jan '09 -->
				<!-- * Cutscores for proficiency levels changed effective November, 2002. [<a href="javascript:popup('http://dpi.wi.gov/oea/profdesc.html')" onClick="setCookie(question, url)">more</A>]<br> -->
				<!-- *<a href="http://dpi.wi.gov/oea/profdesc.html" target="_offsite_wsas">Proficiency data for November 2002 and later are not comparable to earlier years.</a>  Some subject area tests are given only at grades 4, 8, and 10.  -->
				<br />* <a href="javascript:popup('http://dpi.wi.gov/oea/profdesc.html')" onClick='setCookie(question, url)'>Proficiency data for November 2002 and later are not comparable to earlier years.</a>  Some subject area tests are given only at grades 4, 8, and 10. 
			
				<!-- FAY = full (prior) academic year. <a href="javascript:popup('http://dpi.wi.gov/oea/kce_q&a.html')" onClick='setCookie(question, url)'>What are WSAS, WKCE, and WAA?</a><br> -->
				FAY = full academic year. <a href="javascript:popup('http://dpi.wi.gov/oea/kce_q&a.html')" onClick='setCookie(question, url)'>What are WSAS, WKCE, and WAA?</a><br />
	</span>
        </td>
    </tr>    
    </asp:Panel>

      <tr>
        <td>
            <slx:WinssDataGrid ID="StateTestsDataGrid" runat="server" OnRowDataBound="StateTestsDataGrid_RowDataBound">
                <Columns>
                    <slx:WinssDataGridColumn MergeRows="true" DataField="Year" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="Grade" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="GroupName" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="SubjectLabel" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="District Name" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="Level" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn DataField="Enrolled" FormatString="#,##0.###" />
                    <slx:WinssDataGridColumn DataField="No WSAS" FormatString="#,##0.###"  />
                    <slx:WinssDataGridColumn DataField="Percent Minimal" FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="MinPerfWSAS" FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="Percent Basic" HeaderText="Basic" FormatString="#,##0.0%"  />
                    <slx:WinssDataGridColumn DataField="BasicWSAS" FormatString="#,##0.0%"  />
                    <slx:WinssDataGridColumn DataField="Percent Proficient" FormatString="#,##0.0%"  />
                    <slx:WinssDataGridColumn DataField="ProficientWSAS" FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="Percent Advanced" FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="AdvancedWSAS" FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="Percent Pre-Req Skill" FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="Percent Pre-Req Skill Level 1" FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="Percent Pre-Req Skill Level 2" FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="Percent Pre-Req Skill Level 3" FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="Percent Pre-Req Skill Level 4" FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="Percent Pre-Req Eng" FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="Percent Pre-Req Eng Minimal" FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="Percent Pre-Req Eng Basic" FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="Percent Pre-Req Eng Proficient" FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="Percent Pre-Req Eng Advanced" FormatString="#,##0.0%" />
                </Columns>
            </slx:WinssDataGrid>
        </td>
    </tr>

          <tr>
        <td>
<!-- MZD:: ToDo: these bottom links copied from DropOuts page; needs review -->
            <uc10:BottomLinkEnrollmentCounts id="BottomLinkEnrollmentCounts1" runat="server"/>
            <uc6:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            <uc7:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>

            <uc11:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
           
            <uc8:BottomLinkDownload id="BottomLinkDownload1" runat="server" Col="16"/>
            <span class="text">
	            <br /><a href="javascript:popup('http://dpi.wi.gov/spr/ret_use.html')" onClick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
            </p></span>        
        </td>
    </tr>
 </table>        
</asp:Content>
