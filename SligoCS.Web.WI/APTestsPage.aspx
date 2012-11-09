<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="APTestsPage.aspx.cs" Inherits="SligoCS.Web.WI.APTestsPage" Title="Advanced Placement Program&reg; Exams" %>

<%--<%@ Register Src="WebUserControls/BottomLinks.ascx" TagName="BottomLinks" TagPrefix="uc1" %>--%>
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
                <cc1:HyperLinkPlus ID="ACTlink" runat="server" Selected="false" UrlFile="ACTPage.aspx">ACT</cc1:HyperLinkPlus>
                  <cc1:HyperLinkPlus ID="APlink" runat="server" Selected="true">Advanced Placement Program &reg; Exams</cc1:HyperLinkPlus>
            </NavigationLinks>
        </sli:NavigationLinkRow>
        <sli:NavViewByGroup ID="nlrVwByGroup" runat="server" />
        <sli:NavCompareTo ID="nlrCompareTo" runat="server" />
        <sli:ChangeSelectedSchoolOrDistrictLink ID="ChangeSelectedSchoolOrDistrict" Font-Size="Small" runat="server" />
        </td>
    </tr>
    <tr>
        <td><!--Chart goes here.--><Graph:GraphBarChart ID="barChart" runat="server"></Graph:GraphBarChart>
        </td>
    </tr>
    
    <tr>
        <td>
        <SPAN class='text'>School level data are not available.</SPAN><br/>
	    <SPAN class="text">	
	     Major changes in WI student enrollment collections were implemented in 2004-05. 2004-05 enrollment data 
	    may not be comprehensive so 2004-05 test participation rates should be interpreted with caution. 
	    <a href="javascript:popup('http://www.dpi.wi.gov/spr_colleg_q%26amp%3Ba')" onClick="setCookie(question, url)">[More]</a>
	    </SPAN>
        </td>
    </tr>
    <tr>
        <td>
            <slx:WinssDataGrid ID="APTestsDataGrid" runat="server">
                <Columns>
                    <slx:WinssDataGridColumn EnableMerge="true" DataField="YearFormatted" HeaderText="&nbsp;"/>
                    <slx:WinssDataGridColumn EnableMerge="true" DataField="LinkedDistrictName"  HeaderText="&nbsp;"/>
                    <slx:WinssDataGridColumn EnableMerge="true" DataField="LinkedName"  HeaderText="&nbsp;"/>
                    <slx:WinssDataGridColumn EnableMerge="true" DataField="District Name"  HeaderText="&nbsp;"/>
                    <slx:WinssDataGridColumn EnableMerge="true" DataField="OrgLevelLabel"  HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="OrgSchoolTypeLabel"  HeaderText=" " />
                    <slx:WinssDataGridColumn EnableMerge="true" DataField="RaceLabel" HeaderText="Race"/>
                    <slx:WinssDataGridColumn EnableMerge="true" DataField="SexLabel" HeaderText="Gender"/>
                    <slx:WinssDataGridColumn EnableMerge="true" DataField="GradeLabel"  HeaderText="Grade" />
                    <slx:WinssDataGridColumn DataField="enrollment" HeaderText="Total Fall Enrollment Grades 9-12**"  FormatString="#,##0" />
                    <slx:WinssDataGridColumn DataField="# Taking Exams" HeaderText="# Taking Exams" FormatString="#,##0" />
                    <slx:WinssDataGridColumn DataField="% Taking Exams" HeaderText="% Taking Exams" FormatString="#,##0.0%" />
                    <slx:WinssDataGridColumn DataField="# Exams Taken" HeaderText="# Exams Taken"  FormatString="#,##0"/>
                    <slx:WinssDataGridColumn DataField="# Exams Passed" FormatString="#,##0" HeaderText="# of Scores 3 or Above"/>
                    <slx:WinssDataGridColumn DataField="% of Exams Passed" FormatString="#,##0.0%" HeaderText="% of Scores 3 or Above"/>
                </Columns>
            </slx:WinssDataGrid>
        </td>
    </tr>
    <tr>
        <td>
        <asp:Panel ID="DefPanel" runat="server">
	        <SPAN class="text">
	        ** Enrollment counts in this column may cover a narrower grade range if the "view by: grade" option is selected or if counts are for a specific "school type" (e.g. High School). <a href="javascript:popup('http://spr.dpi.wi.gov/spr_demog_q%26amp%3Ba')" onClick="setCookie(question, url)">[More]</a>
	        </SPAN>
        </asp:Panel>
            <!-- <uc10:BottomLinkEnrollmentCounts id="BottomLinkE2" runat="server"/> -->
            <sli:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            <sli:BottomLinkViewDistrictReport id="BottomLinkViewDistrictReport1" runat="server"/>
            <sli:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>
            <sli:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
            <sli:BottomLinkDownload id="BottomLinkDownload1" runat="server" Col="17"/>
                      
<%--             <uc9:BottomLinkWhatToConsider id="BottomLinkWhatToConsider1" runat="server"/>
--%>   
<SPAN class="text">
	<p><a href="javascript:popup('http://spr.dpi.wi.gov/spr_colleg_use')" onClick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
</SPAN>         
        </td>
    </tr>
</table>        
</asp:Content>
