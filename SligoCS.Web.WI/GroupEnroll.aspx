<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="GroupEnroll.aspx.cs" Inherits="SligoCS.Web.WI.GroupEnroll" Title="Enrollment by Student Group" %>

<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>
    
<%@ Register Src="~/WebUserControls/NavigationLinkRow.ascx"  TagName="NavigationLinkRow" TagPrefix="sli"%>
<%@ Register Src="~/WebUserControls/NavSchoolType.ascx" TagName="NavSchoolType" TagPrefix="sli"%>
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
<table style="padding:0px 0px 0px 0px; width:400px;">
    <tr><td>
        <sli:NavSelectYear ID="nlrSelectYear" runat="server" /> 
        <sli:NavSchoolType ID="nlrSTYP" runat="server" />
        <sli:NavViewByGroup ID="nlrVwByGroup" runat="server" />
        <sli:NavCompareTo ID="nlrCompareTo" runat="server" />
        <sli:ChangeSelectedSchoolOrDistrictLink id="ChangeSelectedSchoolOrDistrictLink1" runat="server" />        
        </td></tr>
         <asp:Panel ID="GraphPanel" runat="server">    
    <tr>
        <td>
            <Graph:GraphBarChart ID="barChart" runat="server"></Graph:GraphBarChart>
            <Graph:GraphHorizBarChart ID="hrzBarChart" runat="server" />
            	<SPAN class="text"><br>
Major changes in WI data collection systems were implemented in 2004-05. 2004-05 enrollment data were included in this transition year collection and are not comprehensive so should be interpreted with caution. Also note that, due to 2010-11 race/ethnicity reporting changes, pre- and post-2010-11 data by race/ethncity may not be comparable. <a href="javascript:popup('http://spr.dpi.wi.gov/spr_demog_q&a')" onClick="setCookie(question, url)">[More]</a>
	</SPAN>
        </td>
    </tr>    
    </asp:Panel>

      <tr>
        <td>
            <slx:WinssDataGrid ID="EnrollmentDataGrid" runat="server" >
                <Columns>
                <slx:WinssDatagridColumn MergeRows="true" DataField="YearFormatted" HeaderText="&nbsp;" />  
                <slx:WinssDatagridColumn MergeRows="true" DataField="LinkedName" HeaderText="&nbsp;" />  
                <slx:WinssDatagridColumn MergeRows="true" DataField="OrgLevelLabel" HeaderText="&nbsp;" />  
                <slx:WinssDatagridColumn MergeRows="true" DataField="SchooltypeLabel" HeaderText="&nbsp;" />  
                <slx:WinssDatagridColumn MergeRows="true" DataField="OrgSchoolTypeLabel" HeaderText="&nbsp;" />  
                <slx:WinssDatagridColumn DataField="Total Enrollment PreK-12" HeaderText="Total Fall Enrollment PreK-12**" FormatString="#,##0.###" />  
                <slx:WinssDatagridColumn DataField="DisabledSuppressed" HeaderText="DisabledSuppressed" FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="FemalePercent" HeaderText="% Female" FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="MalePercent" HeaderText="% Male" FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="AmIndPercent" HeaderText="% Amer Indian" FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="AsianPercent" HeaderText="% Asian" FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="BlackPercent" HeaderText="% Black" FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="HispPercent" HeaderText="% Hispanic" FormatString="#,##0.0%" /> 
                <slx:WinssDatagridColumn DataField="PacIPercent" HeaderText="% Pacific Isle" FormatString="#,##0.0%" /> 
                <slx:WinssDatagridColumn DataField="WhitePercent" HeaderText="% White" FormatString="#,##0.0%" /> 
                <slx:WinssDatagridColumn DataField="TwoPercent" HeaderText="% Two or More" FormatString="#,##0.0%" />
                <slx:WinssDatagridColumn DataField="CombPercent" HeaderText="% Combined Small N" FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="NoRespPercent" HeaderText="% NoResp" FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="DisabledPercent" HeaderText="% With Disabilities" FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="Non-DisabledPercent" HeaderText="% Without Disabilities" FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="Non-DisabledSuppressed" HeaderText="Non-DisabledSuppressed" FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="EconDisadvPercent" HeaderText="% Economically Disadvantaged" FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="NonEconDisadvPercent" HeaderText="% Not Economically Disadvantaged or No Data" FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="Percent LEP Spanish" HeaderText="% LEP Spanish" FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="Percent LEP Hmong" HeaderText="% LEP Hmong" FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="Percent LEP Other" HeaderText="% LEP Other" FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="Percent English Proficient" HeaderText="% English Proficient" FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="Pre-KPercent" HeaderText="% Pre-K" FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="KinderPercent" HeaderText="% Kinder." FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="Grade1Percent" HeaderText="% Grade 1" FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="Grade2Percent" HeaderText="% Grade 2" FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="Grade3Percent" HeaderText="% Grade 3" FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="Grade4Percent" HeaderText="% Grade 4" FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="Grade5Percent" HeaderText="% Grade 5" FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="Grade6Percent" HeaderText="% Grade 6" FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="Grade7Percent" HeaderText="% Grade 7" FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="Grade8Percent" HeaderText="% Grade 8" FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="Grade9Percent" HeaderText="% Grade 9" FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="Grade10Percent" HeaderText="% Grade 10" FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="Grade11Percent" HeaderText="% Grade 11" FormatString="#,##0.0%" />  
                <slx:WinssDatagridColumn DataField="Grade12Percent" HeaderText="% Grade 12" FormatString="#,##0.0%" />  
                
                </Columns>
            </slx:WinssDataGrid>
        </td>
    </tr>

          <tr>
        <td>
        <asp:Panel ID="DefPanel" runat="server">
            <br/>
	        <SPAN class="text">
	        ** Enrollment counts in this column may cover a narrower grade range if the "view by: grade" option is selected or if counts are for a specific "school type" (e.g. High School). <a href="javascript:popup('http://spr.dpi.wi.gov/spr_demog_q&a')" onClick="setCookie(question, url)">[More]</a>
	        </SPAN>
        </asp:Panel>
            <!-- <sli:BottomLinkEnrollmentCounts id="BottomLinkEnrollmentCounts1" runat="server"/> -->
            <sli:BottomLinkViewAccountabilityReport id="BottomLinkViewAccountabilityReport" runat="server"/>
            <sli:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            <sli:BottomLinkViewDistrictReport id="BottomLinkViewDistrictReport1" runat="server"/>
            <sli:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>
            <sli:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
            <sli:BottomLinkDownload id="BottomLinkDownload1" runat="server" />
            <span class="text">
	            <p><a href="javascript:popup('http://spr.dpi.wi.gov/spr_demog_use')" onClick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
            </span>        
        </td>
    </tr>
 </table>        
</asp:Content>
