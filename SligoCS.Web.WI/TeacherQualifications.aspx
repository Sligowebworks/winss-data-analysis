<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="TeacherQualifications.aspx.cs" Inherits="SligoCS.Web.WI.TeacherQualifications" Title="Teacher Qualifications" %>

<%@ Register Src="WebUserControls/BottomLinks.ascx" TagName="BottomLinks" TagPrefix="uc1" %>
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
<table>
    <tr>
        <td>
        <sli:NavSelectYear ID="nlrSelectYear" runat="server" /> 
        <sli:NavSchoolType ID="nlrSchoolType" runat="server" />
        
        <sli:NavigationLinkRow ID="nlrShow" runat="server">
            <RowLabel>Show:</RowLabel>
            <NavigationLinks>
                <cc1:HyperLinkPlus ID="linkTQShowWisconsinLicenseStatus" runat="server" ParamName="TQShow" ParamValue="LICSTAT">Wisconsin License Status</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkTQShowDistrictExperience" runat="server" ParamName="TQShow" ParamValue="DISTEXP">District Experience</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkTQShowTotalExperience" runat="server" ParamName="TQShow" ParamValue="TOTEXP">Total Experience</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkTQShowHighestDegree" runat="server" ParamName="TQShow" ParamValue="DEGR">Highest Degree</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkTQShowESEAQualified" runat="server" ParamName="TQShow" ParamValue="ESEAHIQ">ESEA Qualified</cc1:HyperLinkPlus>
            </NavigationLinks>
        </sli:NavigationLinkRow>
        
        <sli:NavigationLinkRow ID="nlrSubject" runat="server">
            <RowLabel>Subject Taught:</RowLabel>
            <NavigationLinks>
                <cc1:HyperLinkPlus ID="linkTQSubjectsEngLangArt" runat="server" ParamName="TQSubjects" ParamValue="ELA">English Language Arts</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkTQSubjectsMathematics" runat="server" ParamName="TQSubjects" ParamValue="MATH">Mathematics</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkTQSubjectsScience" runat="server" ParamName="TQSubjects" ParamValue="SCI">Science</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkTQSubjectsSoclStd" runat="server" ParamName="TQSubjects" ParamValue="SOC">Social Studies</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkTQSubjectsFrgnLang" runat="server" ParamName="TQSubjects" ParamValue="FLANG">Foreign Languages</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkTQSubjectsArts" runat="server" ParamName="TQSubjects" ParamValue="ARTS">The Arts: Art &amp; Design Dance Music Theatre</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkTQSubjectsElem" runat="server" ParamName="TQSubjects" ParamValue="ELSUBJ">Elementary - All Subjects</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkTQSubjectsSpecEdCore" runat="server" ParamName="TQSubjects" ParamValue="SPCORE">Special Education - Core Subjects</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkTQSubjectsSpecEdSumm" runat="server" ParamName="TQSubjects" ParamValue="SPSUM">Special Education Summary</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkTQSubjectsCore" runat="server" ParamName="TQSubjects" ParamValue="CORESUM">Core Subjects Summary</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkTQSubjectsAll" runat="server" ParamName="TQSubjects" ParamValue="SUMALL">Summary - All Subjects</cc1:HyperLinkPlus>

            </NavigationLinks>
        </sli:NavigationLinkRow>
                
        <sli:NavCompareTo ID="nlrCompareTo" runat="server" />
        <sli:ChangeSelectedSchoolOrDistrictLink id="ChangeSelectedSchoolOrDistrict" runat="server" />
        </td>
    </tr>
        <asp:Panel ID="GraphPanel" runat="server">    
    <tr>
        <td>
       <Graph:GraphBarChart ID="barChart" runat="server" />
        </td>
    </tr>    
    </asp:Panel>
    <tr>
        <td>
        <SPAN class="text">FTE: Full-Time Equivalency.&nbsp;&nbsp;Teaching assignments are as of September of each school year.&nbsp;&nbsp;"ESEA Qualified" status applies to core subjects only.&nbsp;&nbsp;<a href="javascript:popup('http://dpi.wi.gov/spr/teach_q&a.html#core_subjects')" onClick='setCookie(question, url)'>What are core subjects?</a> <a href="javascript:popup('http://dpi.wi.gov/spr/teach_q&a.html')" onClick='setCookie(question, url)'>[Cautions]</a><br></SPAN>
        </td>
    </tr>
    <tr>
        <td>
            <slx:WinssDataGrid ID="TQDataGrid" runat="server">
                <Columns>
                <slx:WinssDataGridColumn MergeRows="true" HeaderText="&nbsp;" DataField="YearFormatted"/>
                <slx:WinssDataGridColumn MergeRows="true" HeaderText="&nbsp;" DataField="LinkedName" />
                <slx:WinssDataGridColumn MergeRows="true" HeaderText="&nbsp;" DataField="District Name" />
                <slx:WinssDataGridColumn MergeRows="true" HeaderText="&nbsp;" DataField="SchooltypeLabel" />
                <slx:WinssDataGridColumn MergeRows="true" HeaderText="&nbsp;" DataField="OrgSchoolTypeLabel" />
                <slx:WinssDataGridColumn MergeRows="true" HeaderText="&nbsp;" DataField="OrgLevelLabel" />
                <slx:WinssDataGridColumn DataField="FTETotal" HeaderText="# of FTE teachers"  FormatString="#,##0.0" />
                <slx:WinssDataGridColumn DataField="ESEA_Core_FTE_Total" HeaderText="# of FTE teachers"  FormatString="#,##0.0" />
                <slx:WinssDataGridColumn DataField="FTELicenseFull" HeaderText="# FTE" FormatString="#,##0.0" />
                <slx:WinssDataGridColumn DataField="LicenseFullFTEPercentage" HeaderText="% of Total" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="LicenseEmerFTE" HeaderText="# FTE" FormatString="#,##0.0" />
                <slx:WinssDataGridColumn DataField="LicenseEmerFTEPercentage" HeaderText="% of Total" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="LicenseNoFTE" HeaderText="# FTE" FormatString="#,##0.0" />
                <slx:WinssDataGridColumn DataField="LicenseNoFTEPercentage" HeaderText="% of Total" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="LocalExperience5YearsOrLessFTE" HeaderText="# FTE with less than 5 years experience in this district" FormatString="#,##0.0" />
                <slx:WinssDataGridColumn DataField="LocalExperience5YearsOrMoreFTE" HeaderText="# FTE with at least 5 years experience in this district" FormatString="#,##0.0" />
                <slx:WinssDataGridColumn DataField="LocalExperience5YearsOrMoreFTEPercentage" HeaderText="% with at least 5 years experience in this district" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="TotalExperience5YearsOrLessFTE" HeaderText="# FTE with less than 5 years total experience" FormatString="#,##0.0" />
                <slx:WinssDataGridColumn DataField="TotalExperience5YearsOrMoreFTE" HeaderText="# FTE with at least 5 years total experience" FormatString="#,##0.0" />
                <slx:WinssDataGridColumn DataField="TotalExperience5YearsOrMoreFTEPercentage" HeaderText="% with at least 5 years total experience" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="DegreeMastersOrHigherFTE" HeaderText="# FTE" FormatString="#,##0.0" />
                <slx:WinssDataGridColumn DataField="DegreeMastersOrHigherFTEPercentage" HeaderText="% of Total" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="FTE_ESEA_HQYes" HeaderText="# FTE" FormatString="#,##0.0" />
                <slx:WinssDataGridColumn DataField="FTE_ESEACore_HQYes" HeaderText="# FTE" FormatString="#,##0.0" />
                <slx:WinssDataGridColumn DataField="EHQYesFTEPercentage" HeaderText="% of Total" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="ESEA_Core_HQYesFTEPercentage" HeaderText="% of Total" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="FTE_ESEA_HQNo" HeaderText="# FTE" FormatString="#,##0.0" />
                <slx:WinssDataGridColumn DataField="FTE_ESEACore_HQNo" HeaderText="# FTE" FormatString="#,##0.0" />
                <slx:WinssDataGridColumn DataField="EHQNoFTEPercentage" HeaderText="% of Total" FormatString="#,##0.0%" />
                <slx:WinssDataGridColumn DataField="ESEA_Core_HQNoFTEPercentage" HeaderText="% of Total" FormatString="#,##0.0%" />
                </Columns>
            </slx:WinssDataGrid>
        </td>
    </tr>
    <tr>
       <td>
       <br/>
            <!-- <sli:BottomLinkEnrollmentCounts id="BottomLinkEnrollmentCounts1" runat="server"/> -->
            <sli:BottomLinkViewReport id="BottomLinkViewReport2" runat="server"/>
            <sli:BottomLinkViewDistrictReport id="BottomLinkViewDistrictReport1" runat="server"/>
            <sli:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>
            <sli:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
            <sli:BottomLinkDownload id="BottomLinkDownload2" runat="server" Col="14"/>
            <SPAN class="text">
	            <p><a href="javascript:popup('http://dpi.wi.gov/spr/teach_use.html')" onClick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
            </SPAN>        
        </td>
    </tr>
</table>        
</asp:Content>
