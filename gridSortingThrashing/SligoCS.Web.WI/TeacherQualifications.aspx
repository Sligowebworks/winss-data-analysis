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
                <cc1:HyperLinkPlus ID="HyperLinkPlus6" runat="server" ParamName="TQSubjects" ParamValue="ELA"> English Lanuage Arts</cc1:HyperLinkPlus>
                 <cc1:HyperLinkPlus ID="HyperLinkPlus7" runat="server" ParamName="TQSubjects" ParamValue="MATH"> Mathematics</cc1:HyperLinkPlus>
                 <cc1:HyperLinkPlus ID="HyperLinkPlus8" runat="server" ParamName="TQSubjects" ParamValue="SCI"> Science</cc1:HyperLinkPlus>
                 <cc1:HyperLinkPlus ID="HyperLinkPlus9" runat="server" ParamName="TQSubjects" ParamValue="SOC"> Social Studies</cc1:HyperLinkPlus>
                 <cc1:HyperLinkPlus ID="HyperLinkPlus10" runat="server" ParamName="TQSubjects" ParamValue="FLANG"> Foreign Languages</cc1:HyperLinkPlus>
                 <cc1:HyperLinkPlus ID="HyperLinkPlus11" runat="server" ParamName="TQSubjects" ParamValue="ARTS"> The Arts: Art &amp; Design, Dance, Music, Theatre</cc1:HyperLinkPlus>
                 <cc1:HyperLinkPlus ID="HyperLinkPlus12" runat="server" ParamName="TQSubjects" ParamValue="ELSUBJ"> Elementary &#150; All Subjects</cc1:HyperLinkPlus>
                 <cc1:HyperLinkPlus ID="HyperLinkPlus13" runat="server" ParamName="TQSubjects" ParamValue="SPCORE"> Special Education &#150; Core Subjects</cc1:HyperLinkPlus>
                 <cc1:HyperLinkPlus ID="HyperLinkPlus14" runat="server" ParamName="TQSubjects" ParamValue="CORESUM"> Core Subjects Summary</cc1:HyperLinkPlus>
                 <cc1:HyperLinkPlus ID="HyperLinkPlus15" runat="server" ParamName="TQSubjects" ParamValue="SPSUM"> Special Education Summary</cc1:HyperLinkPlus>
                 <cc1:HyperLinkPlus ID="HyperLinkPlus16" runat="server" ParamName="TQSubjects" ParamValue="SUMALL"> Summary &#150;  All Subjects</cc1:HyperLinkPlus>
            </NavigationLinks>
        </sli:NavigationLinkRow>
                
        <sli:NavCompareTo ID="nlrCompareTo" runat="server" />
        </td>
    </tr>
        <asp:Panel ID="GraphPanel" runat="server">    
    <tr>
        <td>

       <Graph:GraphBarChart ID="barChart" runat="server"></Graph:GraphBarChart>

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
                <cc1:MergeColumn DataField="OrgLevelLabel" HeaderText="" 
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>--%>
                
                <cc1:MergeColumn DataField="SchooltypeLabel" HeaderText="School Type" 
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                
                <cc1:MergeColumn DataField="FTETotal" HeaderText="# of FTE teachers"  
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double" 
                EnableMerge="false">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="FTELicenseFull" HeaderText="# FTE" 
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double" 
                    EnableMerge="false">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="LicenseFullFTEPercentage" HeaderText="% of Total" 
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double"
                     EnableMerge="false">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="LicenseEmerFTE" HeaderText="# FTE" 
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double" 
                    EnableMerge="false">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="LicenseEmerFTEPercentage" HeaderText="% of Total" 
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double" 
                    EnableMerge="false">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <asp:BoundField DataField="LicenseNoFTE" HeaderText="# FTE" 
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="LicenseNoFTEPercentage" HeaderText="% of Total" 
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField>
                
                <asp:BoundField DataField="LocalExperience5YearsOrLessFTE" HeaderText="# FTE with less than 5 years experience in this district" 
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="LocalExperience5YearsOrMoreFTE" HeaderText="# FTE with at least 5 years experience in this district" 
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="LocalExperience5YearsOrMoreFTEPercentage" HeaderText="% with at least 5 years experience in this district" 
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField>
                
                
                
                <asp:BoundField DataField="TotalExperience5YearsOrLessFTE" HeaderText="# FTE with less than 5 years total experience" 
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="TotalExperience5YearsOrMoreFTE" HeaderText="# FTE with at least 5 years total experience" 
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="TotalExperience5YearsOrMoreFTEPercentage" HeaderText="% with at least 5 years total experience" 
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField>                                              
                
                
                <asp:BoundField DataField="DegreeMastersOrHigherFTE" HeaderText="# FTE" 
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="DegreeMastersOrHigherFTEPercentage" HeaderText="% of Total" 
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField>
                
                
                <asp:BoundField DataField="EHQYesFTE" HeaderText="# FTE" 
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="EHQYesFTEPercentage" HeaderText="% of Total" 
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField>
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
                </asp:BoundField>
                                                                               
                </Columns>
            </slx:SligoDataGrid>
        </td>
    </tr>
    <tr>
       <td>

            <uc10:BottomLinkEnrollmentCounts id="BottomLinkEnrollmentCounts1" runat="server"/>
            <uc6:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            <uc7:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>

<%--            <uc11:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
--%>           
            <uc8:BottomLinkDownload id="BottomLinkDownload1" runat="server" Col="16"/>
            <p><span class="text">
	            <a href="javascript:popup('http://dpi.wi.gov/spr/ret_use.html')" onclick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
            </span></p>
        </td>
    </tr>
</table>        
</asp:Content>
