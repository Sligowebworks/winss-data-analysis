<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="DisabilEnroll.aspx.cs" Inherits="SligoCS.Web.WI.DisabilEnroll" Title="Enrollment By Primary Disability" %>

<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>
<%@ Register Src="~/WebUserControls/NavigationLinkRow.ascx"  TagName="NavigationLinkRow" TagPrefix="sli"%>
<%@ Register Src="~/WebUserControls/NavCompareTo.ascx" TagName="NavCompareTo" TagPrefix="sli" %>
<%@ Register Src="~/WebUserControls/NavSchoolType.ascx" TagName="NavSchoolType" TagPrefix="sli"%>

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
    <sli:NavigationLinkRow ID="nlrDisabil" runat="server">
        <RowLabel>Primary Disability:</RowLabel>
        <NavigationLinks>
            <cc1:HyperLinkPlus ID="linkPrDisAllDisabilities" runat="server" ParamName="PrDis" ParamValue="apd">All Primary Disabilities</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkPrDisCognitive" runat="server" ParamName="PrDis" ParamValue="cd">Cognitive Disability (CD)</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkPrDisEmotional" runat="server" ParamName="PrDis" ParamValue="ebd">Emotional Behavioral Disability (EBD)</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkPrDisLearning" runat="server" ParamName="PrDis" ParamValue="ld">Specific Learning Disability (LD)</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkPrDisSpeachLanguage" runat="server" ParamName="PrDis" ParamValue="sl">Speech or Language Impairment (SL)</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkPrDisAutism" runat="server" ParamName="PrDis" ParamValue="a">Autism (A)</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkPrDisDeafBlind" runat="server" ParamName="PrDis" ParamValue="db">Deaf-Blind (DB)</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkPrDisHearing" runat="server" ParamName="PrDis" ParamValue="hi">Hearing Impairment (HI)</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkPrDisOtherHealth" runat="server" ParamName="PrDis" ParamValue="ohi">Other Health Impairment (OHI)</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkPrDisOrthopedic" runat="server" ParamName="PrDis" ParamValue="oi">Orthopedic Impairment (OI)</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkPrDisDevelopmental" runat="server" ParamName="PrDis" ParamValue="sdd">Significant Developmental Delay (SDD)</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkPrDisTraumaticBrain" runat="server" ParamName="PrDis" ParamValue="tbi">Traumatic Brain Injury (TBI)</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkPrDisVisual" runat="server" ParamName="PrDis" ParamValue="vi">Visual Impairment (VI)</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkPrDisCombined" runat="server" ParamName="PrDis" ParamValue="swd">Combined (SWD)</cc1:HyperLinkPlus>
        </NavigationLinks>
    </sli:NavigationLinkRow>
    <sli:NavViewByGroup ID="nlrVwByGroup" runat="server" />
    <sli:NavCompareTo ID="nlrCompareTo" runat="server" />
    </td></tr>
    <tr>
        <td>
            <sli:ChangeSelectedSchoolOrDistrictLink id="ChangeSelectedSchoolOrDistrict" runat="server" />
        </td>
    </tr>
         <asp:Panel ID="GraphPanel" runat="server">    
    <tr>
        <td>
            <Graph:GraphBarChart ID="barChart" runat="server"></Graph:GraphBarChart>
            <Graph:GraphHorizBarChart ID="hrzBarChart" runat="server" />
            	<SPAN class="text"><br>
Major changes in WI data collection systems were implemented in 2004-05. 2004-05 enrollment data were included in this transition year collection and are not comprehensive so should be interpreted with caution. Also note that, due to 2010-11 race/ethnicity reporting changes, pre- and post-2010-11 data by race/ethncity may not be comparable. <a href="javascript:popup('http://www.dpi.wi.gov/spr/demog_q&a.html')" onClick="setCookie(question, url)">[More]</a>
	</SPAN>
        </td>
    </tr>    
    </asp:Panel>

      <tr>
        <td>
            <slx:WinssDataGrid ID="DisabilitiesDataGrid" runat="server">
            <Columns>
                <slx:WinssDataGridColumn MergeRows="true" DataField="YearFormatted" HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="LinkedName" HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="District Name" HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="OrgLevelLabel" HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="OrgSchoolTypeLabel" HeaderText="&nbsp;" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="SchooltypeLabel" HeaderText="School Type" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="SexLabel" HeaderText="Gender" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="RaceLabel" HeaderText="Race" />
                <slx:WinssDataGridColumn MergeRows="true" DataField="GradeLabel" HeaderText="Student Group" />
                 <slx:WinssDataGridColumn DataField="Enrollment PK-12*" HeaderText="Total Fall Enrollment (PK-12)**" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="CDCount" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="CDPercent" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="NonCDCount" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="NonCDPercent" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="ACount" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="APercent" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="NonACount" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="NonAPercent" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="DBCount" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="DBPercent" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="NonDBCount" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="NonDBPercent" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="EBDCount" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="EBDPercent" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="NonEBDCount" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="NonEBDPercent" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="HCount" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="HPercent" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="NonHCount" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="NonHPercent" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="LDCount" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="LDPercent" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="NonLDCount" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="NonLDPercent" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="OHICount" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="OHIPercent" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="NonOHICount" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="NonOHIPercent" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="OICount" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="OIPercent" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="NonOICount" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="NonOIPercent" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="SDDCount" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="SDDPercent" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="NonSDDCount" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="NonSDDPercent" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="SLCount" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="SLPercent" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="NonSLCount" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="NonSLPercent" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="SWDCount" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="SWDPercent" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="NonSWDCount" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="NonSWDPercent" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="TBICount" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="TBIPercent" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="NonTBICount" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="NonTBIPercent" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="VICount" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="VIPercent" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="NonVICount" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="NonVIPercent" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="OtherCount" FormatString="#,##0.###"/>
                <slx:WinssDataGridColumn DataField="OtherPercent" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="StudentsWODisCount"  FormatString="#,##0.###" />
                <slx:WinssDataGridColumn DataField="StudentsWODisPercent" FormatString="#,##0.0%" />
            </Columns>
            </slx:WinssDataGrid>
        </td>
    </tr>

          <tr>
        <td>

            <uc10:BottomLinkEnrollmentCounts id="BottomLinkEnrollmentCounts1" runat="server"/>
            <uc6:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            <sli:BottomlinkViewDistrictReport ID="BottomLinkViewDistrictReport" runat="server" />
            <uc7:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>
            <uc11:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
            <uc8:BottomLinkDownload id="BottomLinkDownload1" runat="server" Col="16"/>
            <SPAN class="text">
	            <p><a href="javascript:popup('http://dpi.wi.gov/spr/demog_use.html')" onClick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
            </p></SPAN>        
        </td>
    </tr>
</table>        
</asp:Content>
