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
                   <cc1:HyperLinkPlus ID="HyperLinkPlus1" runat="server" ParamName="TQSubjectsSP" ParamValue="SPCORE">Special Education &#150; Core Subjects</cc1:HyperLinkPlus>
                   <cc1:HyperLinkPlus ID="HyperLinkPlus5" runat="server" ParamName="TQSubjectsSP" ParamValue="CORESUM">Core Subjects Summary</cc1:HyperLinkPlus>
                   <cc1:HyperLinkPlus ID="HyperLinkPlus6" runat="server" ParamName="TQSubjectsSP" ParamValue="SPSUM">Special Education Summary</cc1:HyperLinkPlus>
                   <cc1:HyperLinkPlus ID="HyperLinkPlus7" runat="server" ParamName="TQSubjectsSP" ParamValue="SUMALL">Summary &#150;  All Subjects</cc1:HyperLinkPlus>
                </NavigationLinks>
         </sli:NavigationLinkRow>
         <sli:NavigationLinkRow ID="NavigationLinkRow1" runat="server">
                <RowLabel>Teacher Variable:</RowLabel> 
                <NavigationLinks>
                   <cc1:HyperLinkPlus ID="HyperLinkPlus2" runat="server" ParamName="TQTeacherVariable" ParamValue="PFWL">&#37; Full Wisconsin License </cc1:HyperLinkPlus>
                   <cc1:HyperLinkPlus ID="HyperLinkPlus8" runat="server" ParamName="TQTeacherVariable" ParamValue="PEWL">&#37; Emergency Wisconsin License</cc1:HyperLinkPlus>
                   <cc1:HyperLinkPlus ID="HyperLinkPlus9" runat="server" ParamName="TQTeacherVariable" ParamValue="PNLFA">&#37; No License For Assignment </cc1:HyperLinkPlus>
                   <cc1:HyperLinkPlus ID="HyperLinkPlus10" runat="server" ParamName="TQTeacherVariable" ParamValue="P5MYDE">&#37; 5 or More Years District Experience</cc1:HyperLinkPlus>
                   <cc1:HyperLinkPlus ID="HyperLinkPlus11" runat="server" ParamName="TQTeacherVariable" ParamValue="P5MYTE">&#37; 5 or More Years Total Experience</cc1:HyperLinkPlus>
                   <cc1:HyperLinkPlus ID="HyperLinkPlus12" runat="server" ParamName="TQTeacherVariable" ParamValue="PMHD">&#37; Masters or Higher Degree</cc1:HyperLinkPlus>
                   <cc1:HyperLinkPlus ID="HyperLinkPlus13" runat="server" ParamName="TQTeacherVariable" ParamValue="PEQ">&#37; ESEA Qualified</cc1:HyperLinkPlus>
                </NavigationLinks>
         </sli:NavigationLinkRow>
         <sli:NavigationLinkRow ID="NavigationLinkRow2" runat="server">
                <RowLabel>Relate To:</RowLabel> 
                <NavigationLinks>
                   <cc1:HyperLinkPlus ID="HyperLinkPlus3" runat="server" ParamName="RelateToTQS" ParamValue="Spending">District Spending</cc1:HyperLinkPlus>
                   <cc1:HyperLinkPlus ID="HyperLinkPlus14" runat="server" ParamName="RelateToTQS" ParamValue="DistSize">District Size</cc1:HyperLinkPlus>
                   <cc1:HyperLinkPlus ID="HyperLinkPlus15" runat="server" Enabled="false" ParamName="RelateToTQS" ParamValue="SchoolSize">School Size</cc1:HyperLinkPlus>
                   <cc1:HyperLinkPlus ID="HyperLinkPlus16" runat="server" ParamName="RelateToTQS" ParamValue="EconomicStatus">&#37; Economically Disadvantaged</cc1:HyperLinkPlus>
                   <cc1:HyperLinkPlus ID="HyperLinkPlus17" runat="server" ParamName="RelateToTQS" ParamValue="EnglishProficiency">&#37; Limited English Proficient </cc1:HyperLinkPlus>
                   <cc1:HyperLinkPlus ID="HyperLinkPlus18" runat="server" ParamName="RelateToTQS" ParamValue="Disability">&#37; Students with Disabilities</cc1:HyperLinkPlus>
                   <cc1:HyperLinkPlus ID="HyperLinkPlus19" runat="server" ParamName="RelateToTQS" ParamValue="Native">&#37; Am Indian</cc1:HyperLinkPlus>
                   <cc1:HyperLinkPlus ID="HyperLinkPlus20" runat="server" ParamName="RelateToTQS" ParamValue="Asian">&#37; Asian</cc1:HyperLinkPlus>
                   <cc1:HyperLinkPlus ID="HyperLinkPlus21" runat="server" ParamName="RelateToTQS" ParamValue="Black">&#37; Black</cc1:HyperLinkPlus>
                   <cc1:HyperLinkPlus ID="HyperLinkPlus22" runat="server" ParamName="RelateToTQS" ParamValue="Hispanic">&#37; Hispanic</cc1:HyperLinkPlus>
                   <cc1:HyperLinkPlus ID="HyperLinkPlus23" runat="server" ParamName="RelateToTQS" ParamValue="White">&#37; White</cc1:HyperLinkPlus>
                </NavigationLinks>
         </sli:NavigationLinkRow>
         <sli:NavigationLinkRow ID="NavigationLinkRow3" runat="server">
                <RowLabel>Location:</RowLabel> 
                <NavigationLinks>
                   <cc1:HyperLinkPlus ID="HyperLinkPlus4" runat="server" ParamName="TQLOCATIONSCATTER" ParamValue="ST">Entire State</cc1:HyperLinkPlus>
                   <cc1:HyperLinkPlus ID="HyperLinkPlus24" runat="server" ParamName="TQLOCATIONSCATTER" ParamValue="CE">My CESA</cc1:HyperLinkPlus>
                   <cc1:HyperLinkPlus ID="HyperLinkPlus25" runat="server" ParamName="TQLOCATIONSCATTER" ParamValue="CT">My County</cc1:HyperLinkPlus>
                </NavigationLinks>
         </sli:NavigationLinkRow>   
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
            <asp:hyperlink ID="ChangeSelectedSchoolOrDistrict" Font-Size="Small" runat="server" />
        </td>
    </tr> 
    <tr>
        
        <td>&nbsp;<!--Chart goes here.--></td>
        
    </tr>
    <tr>
        <td>
        <SPAN class="text">FTE: Full-Time Equivalency.&nbsp;&nbsp;<a href="javascript:popup('http://dpi.wi.gov/spr/teach_q&a.html')" onClick='setCookie(question, url)'>What are core subjects?</a><br>Teaching assignments are as of September of each school year.  <font color=red>2002-03 teacher data were summarized in this way for the first time so may reflect previously unnoticed reporting errors.</font>  <a href="javascript:popup('http://dpi.wi.gov/spr/teach_q&a.html')" onClick='setCookie(question, url)'>[Cautions]</a><br></SPAN>
        </td>
    </tr>
    <tr>
<td>
<slx:SligoDataGrid ID="SligoDataGrid2" runat="server" 
AutoGenerateColumns="False"
OnRowDataBound="SligoDataGrid2_RowDataBound" 
UseAccessibleHeader="False" 
BorderColor="#888888" 
BorderWidth="4px" 
BorderStyle="Double" Width="460" >
<Columns>
<cc1:MergeColumn DataField="YearFormatted" 
    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
    HeaderStyle-BorderStyle="Double"
    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
    ItemStyle-BorderStyle="Double">
    <itemstyle horizontalalign="Center" />
</cc1:MergeColumn>

<cc1:MergeColumn DataField="LinkedName"
    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
    HeaderStyle-BorderStyle="Double"
    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
    ItemStyle-BorderStyle="Double">
    <itemstyle horizontalalign="Left" VerticalAlign="Top" />
</cc1:MergeColumn>
<cc1:MergeColumn DataField="District Name"
    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
    HeaderStyle-BorderStyle="Double"
    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
    ItemStyle-BorderStyle="Double">
    <itemstyle horizontalalign="Center" />
</cc1:MergeColumn>
<%--
<cc1:MergeColumn DataField="SchooltypeLabel" HeaderText="School Type" 
    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
    HeaderStyle-BorderStyle="Double"
    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
    ItemStyle-BorderStyle="Double">
    <itemstyle horizontalalign="Center" />
</cc1:MergeColumn>
    --%>



<asp:BoundField DataField="RelateToNumerator"  HeaderText="RelateToNumerator" 
    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Double" 
    HeaderStyle-BorderWidth="3px" 
    ItemStyle-BorderColor="Gray"
    ItemStyle-BorderStyle="Double" ItemStyle-BorderWidth="3px">
    <ItemStyle HorizontalAlign="Center" />
</asp:BoundField >

<asp:BoundField  DataField="RelateToDenominator" HeaderText="RelateToDenominator" 
    HeaderStyle-BorderColor="Gray" 
    HeaderStyle-BorderStyle="Double"
    HeaderStyle-BorderWidth="3px" 
    ItemStyle-BorderColor="Gray" ItemStyle-BorderStyle="Double" 
    ItemStyle-BorderWidth="3px">
    <ItemStyle HorizontalAlign="Center" />
</asp:BoundField >
    
<asp:BoundField  DataField="RelateToValue"  HeaderText="RelateToValue" 
    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderStyle="Double"
    HeaderStyle-BorderWidth="3px" 
    ItemStyle-BorderColor="Gray"
    ItemStyle-BorderStyle="Double" ItemStyle-BorderWidth="3px">
    <ItemStyle HorizontalAlign="Center" />
</asp:BoundField >


<cc1:MergeColumn DataField="FTETotal" HeaderText="Total # of FTE Teachers"  
    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
    HeaderStyle-BorderStyle="Double"
    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
    ItemStyle-BorderStyle="Double" 
EnableMerge="false">
    <itemstyle horizontalalign="Center" />
</cc1:MergeColumn>

<cc1:MergeColumn DataField="FTELicenseFull" HeaderText="FTE w/ Full Wisconsin License" 
    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
    HeaderStyle-BorderStyle="Double"
    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
    ItemStyle-BorderStyle="Double" 
    EnableMerge="false">
    <itemstyle horizontalalign="Center" />
</cc1:MergeColumn>
<cc1:MergeColumn DataField="LicenseFullFTEPercentage" HeaderText="% Full Wisconsin License" 
    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
    HeaderStyle-BorderStyle="Double"
    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
    ItemStyle-BorderStyle="Double"
     EnableMerge="false">
    <itemstyle horizontalalign="Center" />
</cc1:MergeColumn>
<cc1:MergeColumn DataField="LicenseEmerFTE" 
HeaderText="FTE w/ Emergency Wisconsin License" 
    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
    HeaderStyle-BorderStyle="Double"
    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
    ItemStyle-BorderStyle="Double" 
    EnableMerge="false">
    <itemstyle horizontalalign="Center" />
</cc1:MergeColumn>
<cc1:MergeColumn DataField="LicenseEmerFTEPercentage" 
HeaderText="% Emergency Wisconsin License" 
    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
    HeaderStyle-BorderStyle="Double"
    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
    ItemStyle-BorderStyle="Double" 
    EnableMerge="false">
    <itemstyle horizontalalign="Center" />
</cc1:MergeColumn>
<asp:BoundField DataField="LicenseNoFTE" 
HeaderText="FTE w/ No License For Assignment" 
    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
    HeaderStyle-BorderStyle="Double"
    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
    ItemStyle-BorderStyle="Double">
    <itemstyle horizontalalign="Center" />
</asp:BoundField>
<asp:BoundField DataField="LicenseNoFTEPercentage" 
HeaderText="% No License For Assignment" 
    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
    HeaderStyle-BorderStyle="Double"
    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
    ItemStyle-BorderStyle="Double">
    <itemstyle horizontalalign="Center" />
</asp:BoundField>

<%--<asp:BoundField DataField="LocalExperience5YearsOrLessFTE" 
HeaderText="FTE w/ 5 or More Years District Experience" 
    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
    HeaderStyle-BorderStyle="Double"
    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
    ItemStyle-BorderStyle="Double">
    <itemstyle horizontalalign="Center" />
</asp:BoundField>--%>
<asp:BoundField DataField="LocalExperience5YearsOrMoreFTE" 
HeaderText="FTE w/ 5 or More Years District Experience" 
    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
    HeaderStyle-BorderStyle="Double"
    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
    ItemStyle-BorderStyle="Double">
    <itemstyle horizontalalign="Center" />
</asp:BoundField>
<asp:BoundField DataField="LocalExperience5YearsOrMoreFTEPercentage" 
HeaderText="% 5 or More Years District Experience" 
    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
    HeaderStyle-BorderStyle="Double"
    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
    ItemStyle-BorderStyle="Double">
    <itemstyle horizontalalign="Center" />
</asp:BoundField>

<%--
<asp:BoundField DataField="TotalExperience5YearsOrLessFTE" 
    HeaderText="# FTE with less than 5 years total experience" 
    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
    HeaderStyle-BorderStyle="Double"
    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
    ItemStyle-BorderStyle="Double">
    <itemstyle horizontalalign="Center" />
</asp:BoundField>--%>

<asp:BoundField DataField="TotalExperience5YearsOrMoreFTE" 
    HeaderText="FTE w/ 5 or More Years Total Experience" 
    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
    HeaderStyle-BorderStyle="Double"
    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
    ItemStyle-BorderStyle="Double">
    <itemstyle horizontalalign="Center" />
</asp:BoundField>
<asp:BoundField DataField="TotalExperience5YearsOrMoreFTEPercentage" 
    HeaderText="% 5 or More Years Total Experience" 
    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
    HeaderStyle-BorderStyle="Double"
    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
    ItemStyle-BorderStyle="Double">
    <itemstyle horizontalalign="Center" />
</asp:BoundField>                                              


<asp:BoundField DataField="DegreeMastersOrHigherFTE" 
    HeaderText="FTE w/ Masters or Higher Degree" 
    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
    HeaderStyle-BorderStyle="Double"
    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
    ItemStyle-BorderStyle="Double">
    <itemstyle horizontalalign="Center" />
</asp:BoundField>
<asp:BoundField DataField="DegreeMastersOrHigherFTEPercentage" 
    HeaderText="% Masters or Higher Degree" 
    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
    HeaderStyle-BorderStyle="Double"
    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
    ItemStyle-BorderStyle="Double">
    <itemstyle horizontalalign="Center" />
</asp:BoundField>


<asp:BoundField DataField="EHQYesFTE" 
HeaderText="FTE w/ ESEA Qualified" 
    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
    HeaderStyle-BorderStyle="Double"
    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
    ItemStyle-BorderStyle="Double">
    <itemstyle horizontalalign="Center" />
</asp:BoundField>
<asp:BoundField DataField="EHQYesFTEPercentage" 
HeaderText="% ESEA Qualified" 
    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
    HeaderStyle-BorderStyle="Double"
    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
    ItemStyle-BorderStyle="Double">
    <itemstyle horizontalalign="Center" />
</asp:BoundField>

<%--
<asp:BoundField DataField="EHQNoFTE" HeaderText="# FTE" 
    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
    HeaderStyle-BorderStyle="Double"
    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
    ItemStyle-BorderStyle="Double">
    <itemstyle horizontalalign="Center" />
</asp:BoundField>
<asp:BoundField DataField="EHQNoFTEPercentage" HeaderText="% of Total" 
    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
    HeaderStyle-BorderStyle="Double"
    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
    ItemStyle-BorderStyle="Double">
    <itemstyle horizontalalign="Center" />
</asp:BoundField>--%>
               
                                                           
</Columns>
</slx:SligoDataGrid>
</td>
</tr>
    <tr>
       <td>

            <uc10:BottomLinkEnrollmentCounts id="BottomLinkEnrollmentCounts1" runat="server"/>
            <uc6:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            <uc7:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>

            <uc11:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
         
            <uc8:BottomLinkDownload id="BottomLinkDownload1" runat="server" Col="17"/>
<%--            <p><span class="text">
	            <a href="javascript:popup('http://dpi.wi.gov/spr/ret_use.html')" onclick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
            </span></p>--%>
        </td>
    </tr>
</table>        
</asp:Content>
