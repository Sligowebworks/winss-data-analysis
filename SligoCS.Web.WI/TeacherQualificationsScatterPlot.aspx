<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="TeacherQualificationsScatterPlot.aspx.cs" Inherits="SligoCS.Web.WI.TeacherQualificationsScatterPlot" Title="Teacher Qualifications Scatter Plot" %>

<%@ Register Src="WebUserControls/BottomLinks.ascx" TagName="BottomLinks" TagPrefix="uc1" %>
<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>
<%@ Register Src="~/WebUserControls/NavigationLinkRow.ascx"  TagName="NavigationLinkRow" TagPrefix="sli"%>
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
        <sli:NavSchoolType ID="nlrSTYP" runat="server" />
        <sli:NavigationLinkRow ID="nlrShow" runat="server">
                <RowLabel>Subject Taught:</RowLabel> 
                <NavigationLinks>
                   <cc1:HyperLinkPlus ID="linkTQSubjectsSPSpecEdCore" runat="server" ParamName="TQSubjectsSP" ParamValue="SPCORE">Special Education Core Subjects</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkTQSubjectsSPCore" runat="server" ParamName="TQSubjectsSP" ParamValue="CORESUM">Core Subjects Summary</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkTQSubjectsSPSpecEdSumm" runat="server" ParamName="TQSubjectsSP" ParamValue="SPSUM">Special Education Summary</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="linkTQSubjectsSPAll" runat="server" ParamName="TQSubjectsSP" ParamValue="SUMALL">Summary All Subjects</cc1:HyperLinkPlus>
                </NavigationLinks>
         </sli:NavigationLinkRow>
         <sli:NavigationLinkRow ID="NavigationLinkRow1" runat="server">
                <RowLabel>Teacher Variable:</RowLabel> 
                <NavigationLinks>
                   <cc1:HyperLinkPlus ID="linkTQTeacherVariableWiscLicense" runat="server" ParamName="TQTeacherVariable" ParamValue="LICFULL">&#37; Full Wisconsin License</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkTQTeacherVariableEmergencyLic" runat="server" ParamName="TQTeacherVariable" ParamValue="LICEMER">&#37; Emergency Wisconsin License</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkTQTeacherVariableNoLicense" runat="server" ParamName="TQTeacherVariable" ParamValue="LICNO">&#37; No License for Assignment</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkTQTeacherVariableDistrictExp" runat="server" ParamName="TQTeacherVariable" ParamValue="DISTEXP">&#37; 5 or More Years District Experience</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkTQTeacherVariableTotalExp" runat="server" ParamName="TQTeacherVariable" ParamValue="TOTEXP">&#37; 5 or More Years Total Experience</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkTQTeacherVariableDegree" runat="server" ParamName="TQTeacherVariable" ParamValue="DEGR">&#37; Masters or Higher Degree</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkTQTeacherVariableESEA" runat="server" ParamName="TQTeacherVariable" ParamValue="ESEAHIQ">&#37; ESEA Qualified</cc1:HyperLinkPlus>
                </NavigationLinks>
         </sli:NavigationLinkRow>
         <sli:NavigationLinkRow ID="nlrRelateTo" runat="server">
                <RowLabel>Relate To:</RowLabel> 
                <NavigationLinks>
                    <cc1:HyperLinkPlus ID="linkTQRelateToSpending" runat="server" ParamName="TQRelateTo" ParamValue="SPND">District Spending</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkTQRelateToDistrictSize" runat="server" ParamName="TQRelateTo" ParamValue="DSZE">District Size</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkTQRelateToSchoolSize" runat="server" ParamName="TQRelateTo" ParamValue="SSZE">School Size</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkTQRelateToEconomicStatus" runat="server" ParamName="TQRelateTo" ParamValue="Econ">&#37; Economically Disadvantaged</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkTQRelateToEnglishProficiency" runat="server" ParamName="TQRelateTo" ParamValue="LEP">&#37; Limited English Proficient</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkTQRelateToDisability" runat="server" ParamName="TQRelateTo" ParamValue="DISAB">&#37; Students with Disabilities</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkTQRelateToNativeAm" runat="server" ParamName="TQRelateTo" ParamValue="Ntv">&#37; Amer Indian</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkTQRelateToAsian" runat="server" ParamName="TQRelateTo" ParamValue="Asn">&#37; Asian</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkTQRelateToBlack" runat="server" ParamName="TQRelateTo" ParamValue="Blck">&#37; Black</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkTQRelateToHispanic" runat="server" ParamName="TQRelateTo" ParamValue="Hsp">&#37; Hispanic</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkTQRelateToPacific" runat="server" ParamName="TQRelateTo" ParamValue="Pac">&#37; Pacific Isle</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkTQRelateToWhite" runat="server" ParamName="TQRelateTo" ParamValue="Wht">&#37; White</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkTQRelateToTwoPlusRaces" runat="server" ParamName="TQRelateTo" ParamValue="2OrMore">&#37; Two or More Races</cc1:HyperLinkPlus>
                </NavigationLinks>
         </sli:NavigationLinkRow>
         
         <sli:NavigationLinkRow ID="NavigationLinkRow3" runat="server">
                <RowLabel>Location:</RowLabel> 
                <NavigationLinks>
                   <cc1:HyperLinkPlus ID="linkTQLocationState" runat="server" ParamName="TQLocation" ParamValue="ST">Entire State</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkTQLocationCESA" runat="server" ParamName="TQLocation" ParamValue="CE">My CESA</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkTQLocationCounty" runat="server" ParamName="TQLocation" ParamValue="CT">My County</cc1:HyperLinkPlus>
                </NavigationLinks>
         </sli:NavigationLinkRow>   
        <sli:ChangeSelectedSchoolOrDistrictLink id="ChangeSelectedSchoolOrDistrict" runat="server" /> 
        </td>
    </tr>
        <asp:Panel ID="GraphPanel" runat="server">    
    <tr>
        <td>

       <Graph:GraphScatterplot ID="scatterplot" runat="server"></Graph:GraphScatterplot>

        </td>
    </tr>    
    </asp:Panel>
       
    <tr>
        <td>
        <SPAN class="text">FTE: Full-Time Equivalency.&nbsp;&nbsp;<a href="javascript:popup('http://dpi.wi.gov/spr/teach_q&a.html')" onClick='setCookie(question, url)'>What are core subjects?</a><br>Teaching assignments are as of September of each school year.  <font color=red>2002-03 teacher data were summarized in this way for the first time so may reflect previously unnoticed reporting errors.</font>  <a href="javascript:popup('http://dpi.wi.gov/spr/teach_q&a.html')" onClick='setCookie(question, url)'>[Cautions]</a><br></SPAN>
        </td>
    </tr>
    <tr>
<td>
        <slx:WinssDataGrid runat="Server" ID="TQScatterDataGrid" >
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
            <slx:WinssDatagridColumn DataField="RelateToNumerator"  FormatString="#,##0.0" />
            <slx:WinssDatagridColumn DataField="RelateToDenominator" HeaderText="Total Fall Enrollment (PreK-12)**" FormatString="#,##0" />
            <slx:WinssDatagridColumn DataField="RelateToValue"  HeaderText="RelateToValue"/>
            <slx:WinssDatagridColumn DataField="FTETotal" HeaderText="Total # of FTE Teachers"  FormatString="#,##0.0" />
            <slx:WinssDatagridColumn DataField="FTELicenseFull" HeaderText="FTE w/ Full Wisconsin License" FormatString="#,##0.0" />
            <slx:WinssDatagridColumn DataField="LicenseFullFTEPercentage" HeaderText="% Full Wisconsin License" FormatString="#,##0.0%" />
            <slx:WinssDatagridColumn DataField="LicenseEmerFTE" HeaderText="FTE w/ Emergency Wisconsin License" FormatString="#,##0.0" />
            <slx:WinssDatagridColumn DataField="LicenseEmerFTEPercentage" HeaderText="% Emergency Wisconsin License" FormatString="#,##0.0%" />
            <slx:WinssDatagridColumn DataField="LicenseNoFTE" HeaderText="FTE w/ No License For Assignment" FormatString="#,##0.0##" />
            <slx:WinssDatagridColumn DataField="LicenseNoFTEPercentage" HeaderText="% No License For Assignment" FormatString="#,##0.0%" />
            <slx:WinssDatagridColumn DataField="LocalExperience5YearsOrMoreFTE" HeaderText="FTE w/ 5 or More Years District Experience" FormatString="#,##0.0" />
            <slx:WinssDatagridColumn DataField="LocalExperience5YearsOrMoreFTEPercentage" HeaderText="% 5 or More Years District Experience" FormatString="#,##0.0%" />
            <slx:WinssDatagridColumn DataField="TotalExperience5YearsOrMoreFTE" HeaderText="FTE w/ 5 or More Years Total Experience" FormatString="#,##0.0" />
            <slx:WinssDatagridColumn DataField="TotalExperience5YearsOrMoreFTEPercentage"     HeaderText="% 5 or More Years Total Experience" FormatString="#,##0.0%" />
            <slx:WinssDatagridColumn DataField="DegreeMastersOrHigherFTE" HeaderText="FTE w/ Masters or Higher Degree" FormatString="#,##0.0" />
            <slx:WinssDatagridColumn DataField="DegreeMastersOrHigherFTEPercentage" HeaderText="% Masters or Higher Degree" FormatString="#,##0.0%" />
            <slx:WinssDatagridColumn DataField="EHQYesFTE" HeaderText="FTE w/ ESEA Qualified" FormatString="#,##0.0" />
            <slx:WinssDatagridColumn DataField="EHQYesFTEPercentage" HeaderText="% ESEA Qualified" FormatString="#,##0.0%" />
            </Columns>
        </slx:WinssDataGrid>
</td>
</tr>
    <tr>
       <td>

            <uc10:BottomLinkEnrollmentCounts id="BottomLinkEnrollmentCounts1" runat="server"/>
            <uc6:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            <uc7:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>
            <sli:BottomLinkViewDistrictReport ID="BottomLinkViewDistrictReport" runat="server" />
            <uc11:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
         
            <uc8:BottomLinkDownload id="BottomLinkDownload1" runat="server" Col="17"/>
        <div class="text">
	            <a href="javascript:popup('http://dpi.wi.gov/spr/teach_use.html')" onclick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
            </div>
        </td>
    </tr>
</table>        
</asp:Content>
