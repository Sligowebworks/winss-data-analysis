<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="ACTPage.aspx.cs" Inherits="SligoCS.Web.WI.ACTPage" Title="ACT" %>

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

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>
    <tr>
        <td>
        <sli:NavSelectYear ID="nlrSelectYear" runat="server" /> 
        <sli:NavigationLinkRow ID="nlrShow" runat="server">
            <RowLabel>Show:</RowLabel>
            <NavigationLinks>
                <cc1:HyperLinkPlus ID="ACTlink" runat="server" Selected="true">ACT</cc1:HyperLinkPlus>
                  <cc1:HyperLinkPlus ID="APlink" runat="server" Selected="false" UrlFile="APTestsPage.aspx">Advanced Placement Program &reg; Exams</cc1:HyperLinkPlus>
            </NavigationLinks>
        </sli:NavigationLinkRow>
        
        <sli:NavigationLinkRow ID="nlrSubject" runat="server">
            <RowLabel>Subject: </RowLabel>
            <NavigationLinks>
                <cc1:HyperLinkPlus ID="linkACTSubjReading" runat="server" ParamName="ACTSubj" ParamValue="1RE">Reading</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkACTSubjEnglish" runat="server" ParamName="ACTSubj" ParamValue="2LA">English</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkACTSubjMath" runat="server" ParamName="ACTSubj" ParamValue="3MA">Math</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkACTSubjScience" runat="server" ParamName="ACTSubj" ParamValue="4SC">Science</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkACTSubjSummary" runat="server" ParamName="ACTSubj" ParamValue="0AS">Composite</cc1:HyperLinkPlus>
               </NavigationLinks>
        </sli:NavigationLinkRow>
        <sli:NavViewByGroup ID="nlrVwByGroup" runat="server" />        
        <sli:NavCompareTo ID="nlrCompareTo" runat="server" />
        <sli:ChangeSelectedSchoolOrDistrictLink ID="ChangeSelectedSchoolOrDistrict" Font-Size="Small" runat="server" />
        </td>
    </tr>
    <tr>
        <td><!--Chart goes here.-->
        <Graph:GraphBarChart ID="barChart" runat="server"></Graph:GraphBarChart>
        </td>
    </tr>
    
    <tr>
        <td>
            <SPAN class="text">	
            ACT results are for public school students in grade 12 who took the ACT as juniors or seniors. 
            Major changes in WI student enrollment collections were implemented in 2004-05. 2004-05 enrollment data 
            may not be comprehensive so 2004-05 test participation rates should be interpreted with caution. 
            <a href="javascript:popup('http://spr.dpi.wi.gov/spr_colleg_q&a')" onClick="setCookie(question, url)">[More]</a>
            </SPAN>
        </td>
    </tr>
    <tr>
        <td>
            <slx:WinssDataGrid ID="ACTDataGrid" runat="server" >
                <Columns>
                <slx:WinssDataGridColumn MergeRows="true" DataField="YearFormatted" HeaderText=" " /> 
                <slx:WinssDataGridColumn MergeRows="true" DataField="LinkedName"  HeaderText=" " />
                <slx:WinssDataGridColumn MergeRows="true" DataField="District Name"  HeaderText=" " />
                <slx:WinssDataGridColumn MergeRows="true" DataField="OrgLevelLabel"  HeaderText=" " />
                <slx:WinssDataGridColumn MergeRows="true" DataField="OrgSchoolTypeLabel"  HeaderText=" " />
                <slx:WinssDataGridColumn DataField="RaceLabel" HeaderText="Race"/>
                <slx:WinssDataGridColumn DataField="SexLabel" HeaderText="Gender" />
                <slx:WinssDataGridColumn DataField="GradeLabel"  HeaderText="Grade" />
                <slx:WinssDataGridColumn DataField="enrollment" HeaderText="Total Fall Enrollment Grade 12" FormatString="#,##0.###" />
                <slx:WinssDataGridColumn DataField="Pupilcount" HeaderText="Number Tested" FormatString="#,##0.###" />
                <slx:WinssDataGridColumn DataField="Perc Tested" HeaderText="% Tested" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="Reading" HeaderText="Average Score - Reading" FormatString="#,##0.0" />
                <slx:WinssDataGridColumn DataField="English" HeaderText="Average Score - English" FormatString="#,##0.0" />
                <slx:WinssDataGridColumn DataField="Math" HeaderText="Average Score - Math" FormatString="#,##0.0" />
                <slx:WinssDataGridColumn DataField="Science" HeaderText="Average Score - Science" FormatString="#,##0.0" />
                <slx:WinssDataGridColumn DataField="Composite" HeaderText="Average Score - Composite" FormatString="#,##0.0" />
                </Columns>
            </slx:WinssDataGrid>
        </td>
    </tr>
    <tr>
        <td>
                        
            <asp:Panel ID="TextForRaceInBottomLink" runat="server">
                <p>
                </p>
                <span class='text'>If the number of test takers in the "Race/Eth Code Missing" category
                    is large, then data by racial/ethnic group should be interpreted with caution. </span>
            </asp:Panel>  
            <br/>

            <sli:BottomLinkViewAccountabilityReport id="BottomLinkViewAccountabilityReport" runat="server"/>
            <sli:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            <sli:BottomLinkViewDistrictReport id="BottomLinkViewDistrictReport1" runat="server"/>
            <sli:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>
            <sli:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
            <sli:BottomLinkDownload id="BottomLinkDownload1" runat="server" Col="16"/>
                      
<%--             <uc9:BottomLinkWhatToConsider id="BottomLinkWhatToConsider1" runat="server"/>
--%>   
<SPAN class="text">
	<p><a href="javascript:popup('http://spr.dpi.wi.gov/spr_colleg_use')" onClick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
</SPAN>   
        </td>
    </tr>
</table>        
</asp:Content>
