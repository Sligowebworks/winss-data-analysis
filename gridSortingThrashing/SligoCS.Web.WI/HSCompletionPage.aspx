<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="HSCompletionPage.aspx.cs" Inherits="SligoCS.Web.WI.HSCompletionPage" Title="High School Completion" %>

<%@ Register Assembly="ChartFX.WebForms" Namespace="ChartFX.WebForms" TagPrefix="chartfx7" %>
<%@ Register Assembly="ChartFX.WebForms.Adornments" Namespace="ChartFX.WebForms.Adornments"
    TagPrefix="chartfxadornments" %>
<%@ Register Assembly="ChartFX.WebForms" Namespace="ChartFX.WebForms.Galleries" TagPrefix="chartfx7galleries" %>

<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"  TagPrefix="cc1" %>
<%@ Register Src="~/WebUserControls/NavigationLinkRow.ascx"  TagName="NavigationLinkRow" TagPrefix="sli"%>
<%@ Register Src="~/WebUserControls/NavCompareTo.ascx" TagName="NavCompareTo" TagPrefix="sli" %>

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
        <td>
        <sli:NavigationLinkRow ID="nlrHighSchoolCompletion" runat="server">
            <RowLabel>Credential: </RowLabel>
            <NavigationLinks>
                <cc1:HyperLinkPlus ID="linkHighSchoolCompletionAll" runat="server" ParamName="HighSchoolCompletion" ParamValue="All">All Types</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkHighSchoolCompletionCertificate" runat="server" ParamName="HighSchoolCompletion" ParamValue="CERT">Certificate</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkHighSchoolCompletionHSED" runat="server" ParamName="HighSchoolCompletion" ParamValue="HSED">HSED</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkHighSchoolCompletionRegular" runat="server" ParamName="HighSchoolCompletion" ParamValue="REG">Regular Diploma</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkHighSchoolCompletionSummary" runat="server" ParamName="HighSchoolCompletion" ParamValue="COMB">Combined</cc1:HyperLinkPlus>
            </NavigationLinks>
        </sli:NavigationLinkRow>
        
        <sli:NavigationLinkRow ID="nlrVwBy" runat="server">
            <RowLabel>View By:</RowLabel>
            <NavigationLinks>
                <cc1:HyperLinkPlus ID="linkGroupAll" runat="server" ParamName="Group" ParamValue="AllStudentsFAY">All Students</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkGroupGender" runat="server" ParamName="Group" ParamValue="Gender">Gender</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkGroupRace" runat="server" ParamName="Group" ParamValue="RaceEthnicity">Race/Ethnicity</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkGroupGrade" runat="server" ParamName="Group" ParamValue="Grade">Grade</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkGroupDisability" runat="server" ParamName="Group" ParamValue="Disability">Disability</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkGroupEconDisadv" runat="server" ParamName="Group" ParamValue="EconDisadv">Economic Status</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkGroupEngLangProf" runat="server" ParamName="Group" ParamValue="ELP">English Proficiency</cc1:HyperLinkPlus>
            </NavigationLinks>
        </sli:NavigationLinkRow>
        
        <sli:NavCompareTo ID="nlrCompareTo" runat="server" />
        </td>
    </tr>
    <asp:Panel ID="GraphPanel" runat="server">    
    <tr>
        <td>
           <Graph:GraphBarChart id="barChart" runat="server"></Graph:GraphBarChart>
        </td>
    </tr>    
    </asp:Panel>
    <tr>
        <td>
            <asp:hyperlink ID="ChangeSelectedSchoolOrDistrict" Font-Size="Small" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
	<SPAN class="text"><br/>
	Graduation (regular diploma) and completion rate reporting changed in 1998-99 and 2003-04.  2003-04 was a year of transition to a new student data collection, 
	and as a result 2003-04 high school completion data may not be comprehensive. 
	<a href="javascript:popup('http://www.dpi.wi.gov/spr/grad_q&a.html')" onClick="setCookie(question, url)">[More]</a> 
	See formula for calculating <a href="javascript:popup('http://www.dpi.wi.gov/spr/grad_q&a.html#rate')" onClick="setCookie(question, url)">rates</a>.
	</SPAN>
        </td>
    </tr>
    <tr>
        <td>
            <slx:WinssDataGrid ID="DataGrid" runat="server" OnRowDataBound="DataGrid_RowDataBound">
                <Columns>
                    <slx:WinssDataGridColumn MergeRows="true" DataField="YearFormatted" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="LinkedName" HeaderText="&nbsp;" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="OrgLevelLabel" HeaderText="&nbsp;"/>
                    <slx:WinssDataGridColumn MergeRows="true" DataField="SchooltypeLabel" HeaderText="School Type " />
                    <slx:WinssDataGridColumn DataField="RaceLabel" HeaderText="Race" />
                    <slx:WinssDataGridColumn DataField="SexLabel" HeaderText="Gender" />
                    <slx:WinssDataGridColumn DataField="GradeLabel" HeaderText="Grade" />
                    <slx:WinssDataGridColumn DataField="DisabilityLabel" HeaderText="Student Group" />
                    <slx:WinssDataGridColumn DataField="EconDisadvLabel" HeaderText="Student Group" />
                    <slx:WinssDataGridColumn DataField="ELPLabel" HeaderText="Student Group" />
                    <slx:WinssDataGridColumn DataField="Total Enrollment Grade 12" HeaderText="Total Fall Enrollment Grade 12" FormatString="#,##0.###"/>
                    <slx:WinssDataGridColumn DataField="Total Expected to Complete High School" HeaderText="Total Expected to Complete High School**" FormatString="#,##0.###"/>
                    <slx:WinssDataGridColumn DataField="Cohort Dropouts" FormatString="#,##0.0%"/>
                    <slx:WinssDataGridColumn DataField="Students Who Reached the Maximum Age" FormatString="#,##0.0%"/>
                    <slx:WinssDataGridColumn DataField="Certificates" FormatString="#,##0.0%"/>
                    <slx:WinssDataGridColumn DataField="HSEDs" FormatString="#,##0.0%"/>
                    <slx:WinssDataGridColumn DataField="Regular Diplomas" FormatString="#,##0.0%"/>
                </Columns>
            </slx:WinssDataGrid>
        </td>
    </tr>
    <tr>
        <td>
	<SPAN class="text"></SPAN>
	
<br><SPAN class="text"><p>**Total Expected to Complete High School is a count of students who were expected to complete high school in the year indicated whether or not the students actually did.  This total includes actual high school completers, cohort dropouts, and noncompleters who reached the maximum age associated with the constitutional right to a free public education. <a href="javascript:popup('http://dpi.wi.gov/winss/perfacademic_glossary.html#expected_completer')" onClick="setCookie(question, url)">[More]</a></SPAN><SPAN class="text">	
</SPAN>

            <uc6:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            <uc11:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported2" runat="server" />
            <!--uc7:BottomLinkViewProfile id="BottomLinkViewProfile1" run at="server"/-->
            
            <uc8:BottomLinkDownload id="BottomLinkDownload1" runat="server" Col="25"/>
                      
             <uc9:BottomLinkWhatToConsider id="BottomLinkWhatToConsider1" runat="server"/>
        
        </td>
    </tr>
</table>
</asp:Content>
