<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="TeacherQualificationsScatterPlot.aspx.cs" Inherits="SligoCS.Web.WI.TeacherQualificationsScatterPlot" Title="Teacher Qualifications Scatter Plot" %>

<%@ Register Src="WebUserControls/BottomLinks.ascx" TagName="BottomLinks" TagPrefix="uc1" %>
<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>
<%@ Register Src="WebUserControls/ParamsLinkBox.ascx" TagName="ParamsLinkBox" TagPrefix="uc2" %>

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
        <td><uc2:ParamsLinkBox ID="ParamsLinkBox1" runat="server" 
                ShowTR_STYP="true" 
                ShowTR_TQSubjects="true" 
                ShowTR_TQTeacherVariable="true" 
                ShowTR_TQRelatedTo="true" 
                ShowTR_TQLocation="true"  
                />
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
<cc1:SligoDataGrid ID="SligoDataGrid2" runat="server" 
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
</cc1:SligoDataGrid>
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
