<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="StateTestsScatterplot.aspx.cs" Inherits="SligoCS.Web.WI.StateTestsScatterplot" Title="WKCE Scatterplot"%>

<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>
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
                
                <sli:NavigationLinkRow ID="nlrRelateTo" runat="server" >
                    <RowLabel>Relate To:</RowLabel>
                    <NavigationLinks>
                        <cc1:HyperLinkPlus ID="linkRelDistrictSpending" runat="server" ParamName="Rel" ParamValue="Spending">District Spending</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkRelDistrictSize" runat="server" ParamName="Rel" ParamValue="Size">District Size</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkRelEconDisadvantaged" runat="server" ParamName="Rel" ParamValue="EconomicStatus">% Economically Disadvantaged</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkRelLEP" runat="server" ParamName="Rel" ParamValue="EnglishProficiency">% Limited English Proficient</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkRelDisabilities" runat="server" ParamName="Rel" ParamValue="Disability">% Students with Disabilities</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkRelNative" runat="server" ParamName="Rel" ParamValue="Native">% Am Indian</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkRelAsian" runat="server" ParamName="Rel" ParamValue="Asian">% Asian</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkRelBlack" runat="server" ParamName="Rel" ParamValue="Black">% Black</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkRelHispanic" runat="server" ParamName="Rel" ParamValue="Hispanic">% Hispanic</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkRelWhite" runat="server" ParamName="Rel" ParamValue="White">% White</cc1:HyperLinkPlus>
                    </NavigationLinks>
                </sli:NavigationLinkRow>
                <sli:NavigationLinkRow ID="nlrShowDiff" runat="server">
                       <RowLabel>Show Differences In:</RowLabel>
                      <NavigationLinks>
                        <cc1:HyperLinkPlus ID="linkGroup2DistrictSpending" runat="server" ParamName="Group2" ParamValue="Spending">District Spending</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkGroup2DistrictSize" runat="server" ParamName="Group2" ParamValue="Size">District Size</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkGroup2EconDisadvantaged" runat="server" ParamName="Group2" ParamValue="EconomicStatus">% Economically Disadvantaged</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkGroup2LEP" runat="server" ParamName="Group2" ParamValue="EnglishProficiency">% Limited English Proficient</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkGroup2Disabilities" runat="server" ParamName="Group2" ParamValue="Disability">% Students with Disabilities</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkGroup2Native" runat="server" ParamName="Group2" ParamValue="Native">% Am Indian</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkGroup2Asian" runat="server" ParamName="Group2" ParamValue="Asian">% Asian</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkGroup2Black" runat="server" ParamName="Group2" ParamValue="Black">% Black</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkGroup2Hispanic" runat="server" ParamName="Group2" ParamValue="Hispanic">% Hispanic</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkGroup2White" runat="server" ParamName="Group2" ParamValue="White">% White</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkGroup2None" runat="server" ParamName="Group2" ParamValue="None">No Option Selected</cc1:HyperLinkPlus>
                      </NavigationLinks> 
                </sli:NavigationLinkRow>
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
    <asp:Panel ID="pnlMessage" runat="server" Visible="false">
    <tr>
        <td>The data you have requested are not available for the school you have selected. The cause may be that the data are not available for the grade range of your school. For example, if the grade range of your school is kindergarten through second grade, then you will not get any data under "Examining School Performance on Statewide Tests" because no statewide tests are given to students in these grades. Another possible cause is that you selected a newly opened school, and the data you requested are not yet available. 

You may click on the "Back" button of your browser to select another type of data, or click on "change school" to select a different school. If you feel you have received this page in error, please open a help ticket <a href=" http://wise.dpi.wi.gov/help-ticket" target="_blank"> http://wise.dpi.wi.gov/help-ticket</a>. </td>
    </tr>
    </asp:Panel>
    <tr>
    <td>
        <Graph:GraphScatterplot ID="scatterplot" runat="server"></Graph:GraphScatterplot>
        <span class="text">
            <br />Larger red plotting symbol represents current <%= (GlobalValues.OrgLevel.Key == SligoCS.Web.WI.WebSupportingClasses.WI.OrgLevelKeys.School)? "school." : "district." %>
		     <br /> Some subject area tests are given only at grades 4, 8, and 10. 
			<br />Due to 2010-11 race/ethnicity reporting changes, pre- and post-2010-11 data by race/ethnicity may not be comparable. <a href="javascript:popup('http://spr.dpi.wi.gov/spr_demog_q&a')" onClick="setCookie(question, url)">[More]</a>
			<br />FAY = full academic year. <a href="javascript:popup('http://oea.dpi.wi.gov/oea_kce_q&a')" onclick='setCookie(question, url)'>What are WSAS, WKCE, and WAA?</a><br />
	</span>
	</td>
    </tr>
    
     <tr>
        <td>
        </td>
    </tr>

     <tr>
        <td>
        <slx:WinssDataGrid runat="Server" ID="StateTestsScatterDataGrid" >
            <Columns>
            <slx:WinssDataGridColumn DataField="GroupNum" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="YearFormatted" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="LinkedName"  HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="District Name"  HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="OrgSchoolTypeLabel" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="OrgLevelLabel" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="SchoolTypeLabel" HeaderText="School Type" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="SexLabel" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="GradeLabel"  HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="GroupName" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="RaceLabel" HeaderText="&nbsp;" />
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
        </td>
    </tr>

     <tr>
        <td>
            
            <sli:BottomLinkViewAccountabilityReport id="BottomLinkViewAccountabilityReport" runat="server"/>
            <sli:BottomLinkViewReport id="BottomLinkViewReport" runat="server"/>
            <sli:BottomLinkViewProfile ID="BottomLinkViewProfile" runat="server" />
            <sli:BottomLinkViewDistrictReport ID="BottomLinkViewDistrictReport" runat="server" />
            <sli:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
            <sli:BottomLinkDownload id="BottomLinkDownload1" runat="server" />
            <div class="text">
	            <a href="javascript:popup('http://spr.dpi.wi.gov/spr_kce_use')" onclick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
            </div>
        </td>
    </tr>
    
 </table>        
</asp:Content>

