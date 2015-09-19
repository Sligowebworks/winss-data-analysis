<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="StateTestsSimilarSchools.aspx.cs" Inherits="SligoCS.Web.WI.StateTestsSimilarSchools" %>

<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<table style="padding:0px 0px 0px 0px; width:400px;">
    <tr>
        <td>
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
            
            <sli:NavigationLinkRow ID="nlrWkceCombined" runat="server">
                    <RowLabel><!-- --></RowLabel>
                    <NavigationLinks>
                            <cc1:HyperLinkPlus ID="linkWOWWSASCombined" runat="server" ParamName="WOW" ParamValue="WSAS">WSAS: WKCE and WAA Combined</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkWOWWKCE" runat="server" ParamName="WOW" ParamValue="WKCE">WKCE Only</cc1:HyperLinkPlus>
                    </NavigationLinks>
            </sli:NavigationLinkRow>
            
            <sli:NavigationLinkRow ID="nlrLevel" runat="server">
                    <RowLabel>Sort By: </RowLabel>
                    <NavigationLinks>
                        <cc1:HyperLinkPlus ID="linkLevelAdvanced" runat="server" ParamName="Level" ParamValue="ADV">Advanced</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkLevelAdvancedProficient" runat="server" ParamName="Level" ParamValue="A-P">Advanced + Proficient</cc1:HyperLinkPlus>
                    </NavigationLinks>
                </sli:NavigationLinkRow>
            
          <sli:NavSimilarCriteria ID="nlrSimilar" runat="server" />      
                
            <sli:NavCompareTo ID="nlrCompareTo" runat="server" />
            
            <sli:NavigationLinkRow ID="nlrLocation" runat="server">
                    <RowLabel>Location:</RowLabel>
                    <NavigationLinks>
                        <cc1:HyperLinkPlus ID="linkLFState" runat="server" ParamName="LF" ParamValue="ST">Entire State</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkLFCESA" runat="server" ParamName="LF" ParamValue="CE">My CESA</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkLFCounty" runat="server" ParamName="LF" ParamValue="CT">My County</cc1:HyperLinkPlus>
                    </NavigationLinks>
                </sli:NavigationLinkRow>
        </td>
    </tr>
     <asp:Panel ID="GraphPanel" runat="server">
    <tr>
        <td>
            <Graph:GraphBarChart ID="barChart" runat="server"></Graph:GraphBarChart>
        </td>
    </tr>
    </asp:Panel>
    <tr>
        <td>
            <span class="text">
	<!--Major changes in WI student enrollment collections were implemented in
	2004-05. 2004-05 enrollment data may not be comprehensive so some 2004-05
	truancy rates should be interpreted with caution. <a href="javascript:popup('http://dpi.wi.gov/spr/tru_q&a.html#cautions')" onclick="setCookie(question, url)">[More]</a>
-->
        </span>
        </td>
    </tr>
    <tr>
        <td>
        </td>
    </tr>
    <tr>
        <td>
        <% if (GlobalValues.Sim.Key != SligoCS.Web.WI.WebSupportingClasses.WI.SimKeys.Default)
           {%>
            <cc1:HyperLinkPlus ID="linkSimDefault" runat="server" ParamName="Sim" ParamValue="D">Back to Top 5  Similar Graph</cc1:HyperLinkPlus>
            <%
           } %>
        <slx:WinssDataGrid ID="SimilarDataGrid" runat="server">
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
                
                <slx:WinssDataGridColumn MergeRows="true" DataField="SubjectLabel" HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="EconDisadvLabel" HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="DisabilityLabel" HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="ELPLabel" HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="Level" HeaderText="&nbsp;" />
                
                <slx:WinssDataGridColumn DataField="District Size" HeaderText="Total # Enrolled" FormatString="#,##0.###" />
                <slx:WinssDatagridColumn DataField="Cost Per Member"  HeaderText="Current Education Cost per Member" FormatString="$#,##0" />
                
                <slx:WinssDataGridColumn DataField="Enrolled" HeaderText="Enrolled in Tested Grade(s)" FormatString="#,##0.###" />                    
                <slx:WinssDataGridColumn DataField="Excused-By-Parent" HeaderText="% No WSAS - Excused By Parent"  FormatString="#,##0.###"  />
                <slx:WinssDataGridColumn DataField="Eligible-But-Not-Tested" HeaderText="% No WSAS - Reasons Unknown"  FormatString="#,##0.###"  />
                <slx:WinssDataGridColumn DataField="No WSAS Total" HeaderText="% No WSAS"  FormatString="#,##0.###"  />
   
                <slx:WinssDataGridColumn DataField="PctEcon" HeaderText="% Econ. Disadv." FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="PctLEP"  HeaderText="% Limited English Proficient" FormatString="#,##0.0%"/>
                <slx:WinssDataGridColumn DataField="PctDisabled" HeaderText="% Students with Disabilities"  FormatString="#,##0.0%"/>
                <slx:WinssDataGridColumn DataField="PctAsian" HeaderText= "% Asian" FormatString="#,##0.0%"/>
                <slx:WinssDataGridColumn DataField="PctAmInd" HeaderText="% Am. Indian/Alaskan Native" FormatString="#,##0.0%"/>
                <slx:WinssDataGridColumn DataField="PctBlack" HeaderText="% Black" FormatString="#,##0.0%"/>
                <slx:WinssDataGridColumn DataField="PctHisp"  HeaderText="% Hispanic" FormatString="#,##0.0%"/>
                <slx:WinssDataGridColumn DataField="PctWhite" HeaderText="% White" FormatString="#,##0.0%"/>
                
                <slx:WinssDatagridColumn DataField="Percent Pre-Req Skill"  HeaderText="WAA SwD" FormatString="#,##0.0%" />
                <slx:WinssDatagridColumn DataField="Percent Pre-Req Eng"  HeaderText="WAA ELL" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="MinPerfWSAS" HeaderText="% Min Perf" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="BasicWSAS" HeaderText="% Basic" FormatString="#,##0.0%"  />
                <slx:WinssDataGridColumn DataField="ProficientWSAS" HeaderText="% Proficient" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="AdvancedWSAS" HeaderText="% Advanced" FormatString="#,##0.0%" />
                <slx:WinssDatagridColumn DataField="Percent Minimal"  HeaderText="% Min Perf" FormatString="#,##0.0%" />
                <slx:WinssDatagridColumn DataField="Percent Basic"  HeaderText="% Basic" FormatString="#,##0.0%" />
                <slx:WinssDatagridColumn DataField="Percent Proficient"  HeaderText="% Proficient" FormatString="#,##0.0%" />
                <slx:WinssDatagridColumn DataField="Percent Advanced"  HeaderText="% Advanced" FormatString="#,##0.0%" />
                <slx:WinssDatagridColumn DataField="BasicPlusMinPerfPlusNoWSASTotalWSAS"  
                        HeaderText="% Basic + Min Perf + No WSAS Total" FormatString="#,##0.0%" /> 
                <slx:WinssDatagridColumn DataField="BasicPlusMinPerfPlusPre-ReqSkillsEngPlusNoWSASTotal"  
                        HeaderText="% Basic + Min Perf + WAA Swd/ELL + No WSAS Total" FormatString="#,##0.0%" /> 
                <slx:WinssDatagridColumn DataField="AdvancedPlusProficientTotalWSAS"  HeaderText="% Advanced + Proficient" FormatString="#,##0.0%" />
                <slx:WinssDatagridColumn DataField="PCTAdvPlusPCTPrf"  HeaderText="% Advanced + Proficient" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="Percent Pre-Req Skill Level 1" HeaderText="WAA SwD Min Perf" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="Percent Pre-Req Skill Level 2" HeaderText="WAA SwD Basic" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="Percent Pre-Req Skill Level 3" HeaderText="WAA SwD Proficient" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="Percent Pre-Req Skill Level 4" HeaderText="WAA SwD Advanced" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="Percent Pre-Req Eng" FormatString="#,##0.0%" HeaderText="WAA ELL Total"/>
                <slx:WinssDataGridColumn DataField="Percent Pre-Req Eng Minimal" FormatString="#,##0.0%" HeaderText="WAA ELL Min Perf"/>
                <slx:WinssDataGridColumn DataField="Percent Pre-Req Eng Basic" FormatString="#,##0.0%" HeaderText="WAA ELL Basic"/>
                <slx:WinssDataGridColumn DataField="Percent Pre-Req Eng Proficient" FormatString="#,##0.0%" HeaderText="WAA ELL Proficient"/>
                <slx:WinssDataGridColumn DataField="Percent Pre-Req Eng Advanced" FormatString="#,##0.0%" HeaderText="WAA ELL Advanced"/>
                
                <slx:WinssDataGridColumn DataField="PctTotalWAALEP" HeaderText="WAA ELL Total" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="PctTotalWAADisabil" HeaderText="WAA SwD Total" FormatString="#,##0.0%" />    
            </Columns>
        </slx:WinssDataGrid>
        </td>
    </tr>
    <tr>
            <td>
                <asp:Panel ID="DefPanel" runat="server">
                <span class="text">**You define “similar” by selecting one or more criteria on the “Similar Criteria” row above the graph.   Note that all data used in defining 'similar' are the most current data available. Districts serving similar or more disadvantaged populations with similar or fewer resources are included in lists of districts that outperformed your district to provide more sources of ideas for improvement.
[<a href="javascript:popup('http://dpi.wi.gov/winss/similar.html')" onclick="setCookie(question, url)">More</a>] 
                </span>
                </asp:Panel>
                
              <cc1:HyperLinkPlus ID="linkSimOutperform" runat="server" ParamName="Sim" ParamValue="O">View list of all 'similar' districts that outperformed my school.</cc1:HyperLinkPlus><br />
              This list includes districts with more disadvantaged polulations or fewer resources <br />
              <cc1:HyperLinkPlus ID="linkSimAllSimilar" runat="server" ParamName="Sim" ParamValue="A">View list of all 'similar districts.</cc1:HyperLinkPlus><br />
               This is a sorted list that includes all 'similar' districts regardless of performance.

                
                <sli:BottomLinkViewReport id="BottomLinkViewReport" runat="server"/>
                <sli:BottomLinkViewProfile ID="BottomLinkViewProfile" runat="server" />
                <sli:BottomLinkViewDistrictReport ID="BottomLinkViewDistrictReport" runat="server" />
                <sli:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported" runat="server" />
                <sli:BottomLinkDownload id="BottomLinkDownload" runat="server" />
                <div class="text">
                    <a href="javascript:popup('http://dpi.wi.gov/spr/kce_use.html')" onclick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
                </div>
            </td>
    </tr>
</table>
</asp:Content>
