<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="HSCompletionPage.aspx.cs" Inherits="SligoCS.Web.WI.HSCompletionPage" Title="High School Completion" %>

<%@ Register Assembly="ChartFX.WebForms" Namespace="ChartFX.WebForms" TagPrefix="chartfx7" %>
<%@ Register Assembly="ChartFX.WebForms.Adornments" Namespace="ChartFX.WebForms.Adornments"
    TagPrefix="chartfxadornments" %>
<%@ Register Assembly="ChartFX.WebForms" Namespace="ChartFX.WebForms.Galleries" TagPrefix="chartfx7galleries" %>

<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"  TagPrefix="cc1" %>

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
        <sli:NavSelectYear ID="nlrSelectYear" runat="server" /> 
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

        <sli:NavigationLinkRow ID="nlrTimeFrame" runat="server">
            <RowLabel>Timeframe:</RowLabel>
            <NavigationLinks>
                <cc1:HyperLinkPlus ID="linkTmFrmAll" runat="server" ParamName="TmFrm" ParamValue="A">All Timeframes</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkTmFrmFourYear" runat="server" ParamName="TmFrm" ParamValue="4">Four-Year Rate</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkTmFrmSixYear" runat="server" ParamName="TmFrm" ParamValue="6">Six-Year Rate</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkTmFrmLegacy" runat="server" ParamName="TmFrm" ParamValue="L">Legacy Rate (By Age 21)</cc1:HyperLinkPlus>
            </NavigationLinks>
        </sli:NavigationLinkRow>
        <sli:NavViewByGroup ID="nlrViewByGroup" runat="server" />        
        <sli:NavCompareTo ID="nlrCompareTo" runat="server" />
        <sli:ChangeSelectedSchoolOrDistrictLink ID="ChangeSelectedSchoolOrDistrict" Font-Size="Small" runat="server" />
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
	<span class="text">
	Graduation (regular diploma) and completion rate reporting changed in 2003-04 and 2009-10. 2003-04 was a year of transition to a new student data collection, and as a result 2003-04 legacy-rate data may not be comprehensive. 4-year rates were first available in 2009-10, 5-Year rates were first available in 2010-11, and 6-year rates were first available in 2011-12.  See formulas for calculating <a href="javascript:popup('http://spr.dpi.wi.gov/spr_grad_q&a#rate')" onclick="setCookie(question, url)">rates</a>.  Due to <a href="javascript:popup('http://spr.dpi.wi.gov/spr_demog_q&a#race')">2010-11 race/ethnicity reporting changes</a>, pre- and post-2010-11 completion rates by race/ethnicity may not be comparable. Also note that automated processes to <a href="javascript:popup('http://lbstat.dpi.wi.gov/lbstat_priv_more')" onclick="setCookie(question, url)">protect student privacy</a> were modified in spring 2012. <a href="javascript:popup('http://spr.dpi.wi.gov/spr_grad_q&a')" onclick="setCookie(question, url)">[More]</a>  
	</span>
        </td>
    </tr>
    <tr>
        <td>
            <slx:WinssDataGrid ID="TimeFrameDataGrid" runat="server" >
            <Columns>
            <slx:WinssDataGridColumn MergeRows="true" DataField="YearFormatted" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="LinkedName" HeaderText="&nbsp;" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="OrgSchoolTypeLabel" HeaderText="&nbsp;"/>
                    <slx:WinssDataGridColumn MergeRows="true" DataField="SchooltypeLabel" HeaderText="School Type " />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="TimeFrameLabel" HeaderText="Timeframe"/>
                    <slx:WinssDataGridColumn DataField="RaceLabel" HeaderText="Race" />
                    <slx:WinssDataGridColumn DataField="SexLabel" HeaderText="Gender" />
                    <slx:WinssDataGridColumn DataField="GradeLabel" HeaderText="Grade" />
                    <slx:WinssDataGridColumn DataField="DisabilityLabel" HeaderText="Student Group" />
                    <slx:WinssDataGridColumn DataField="EconDisadvLabel" HeaderText="Student Group" />
                    <slx:WinssDataGridColumn DataField="ELPLabel" HeaderText="Student Group" />
                            
                    <slx:WinssDataGridColumn DataField="Total Enrollment Grade 12" HeaderText="Total Fall Enrollment Grade 12" FormatString="#,##0.###"/>
                    <slx:WinssDataGridColumn DataField="Total_Expected_to_Complete_High_School_Count" HeaderText="Total Expected to Complete High School**" FormatString="#,##0.###"/>
                    
                    <slx:WinssDataGridColumn DataField="Not Continuing Percent" FormatString="#,##0.0%" HeaderText="% Not Known to be Continuing"/>
                    <slx:WinssDataGridColumn DataField="Students Who Reached the Maximum Age Percent" FormatString="#,##0.0%" HeaderText="% Reached Maximum Age"/>
                    <slx:WinssDataGridColumn DataField="Continuing Percent" FormatString="#,##0.0%" HeaderText="% Known to be Continuing"/>
                    <slx:WinssDataGridColumn DataField="Certificates Percent" FormatString="#,##0.0%" HeaderText="% Certificates" />
                    <slx:WinssDataGridColumn DataField="HSEDs Percent" FormatString="#,##0.0%" HeaderText="% HSEDs"/>
                    <slx:WinssDataGridColumn DataField="Regular Diplomas Percent" FormatString="#,##0.0%" HeaderText="% Regular Diplomas (Graduates)"/>

            </Columns>
            </slx:WinssDataGrid>
        </td>
    </tr>
    <tr>
        <td>
	
<br /><p><span class="text">**<a href="javascript:popup('http://winss.dpi.wi.gov/winss_perfacademic_glossary#expected_completer')" onclick="setCookie(question, url)">Total Expected to Complete High School</a> is the denominator used to calculate graduation and completion rates.   For the four-year rates, this total is the count of students in the <a href="javascript:popup('http://winss.dpi.wi.gov/winss_perfacademic_glossary#adjusted_cohort')" onclick="setCookie(question, url)">adjusted 4-year cohort</a> for the graduating class. For the legacy rates, this total is the sum of actual high school completers, <a href="javascript:popup('http://winss.dpi.wi.gov/winss_perfacademic_glossary#cohort_dropout')" onclick="setCookie(question, url)">cohort dropouts</a>, plus noncompleters who reached the <a href="javascript:popup('http://winss.dpi.wi.gov/winss_perfacademic_glossary#maximum_age')" onclick="setCookie(question, url)">maximum age</a> associated with the right to a free public education.   
</span></p>
            <sli:BottomLinkViewAccountabilityReport id="BottomLinkViewAccountabilityReport" runat="server"/>
            <sli:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            <sli:BottomLinkViewDistrictReport id="BottomLinkViewDistrictReport1" runat="server"/>
            <sli:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>
            <sli:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
            <sli:BottomLinkDownload id="BottomLinkDownload1" runat="server" Col="14"/>
            
	            <p><span class="text"><a href="javascript:popup('http://spr.dpi.wi.gov/spr_grad_use')" onclick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a></span></p>
        </td>
    </tr>
</table>
</asp:Content>
