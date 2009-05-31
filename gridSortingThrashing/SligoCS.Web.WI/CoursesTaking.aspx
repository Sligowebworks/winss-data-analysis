<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="CoursesTaking.aspx.cs" Inherits="SligoCS.Web.WI.CoursesTaking" %>

<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>
<%@ Register Src="~/WebUserControls/NavigationLinkRow.ascx"  TagName="NavigationLinkRow" TagPrefix="sli"%>
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
    <sli:NavigationLinkRow ID="nlrGrade" runat="server">
        <RowLabel>Grades:</RowLabel>
        <NavigationLinks>
            <cc1:HyperLinkPlus ID="HyperLinkPlus4" runat="server" ParamName="Grade" ParamValue="AP">6</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="HyperLinkPlus5" runat="server" ParamName="Grade" ParamValue="AP">7</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="HyperLinkPlus6" runat="server" ParamName="Grade" ParamValue="AP">8</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="HyperLinkPlus7" runat="server" ParamName="Grade" ParamValue="AP">9</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="HyperLinkPlus8" runat="server" ParamName="Grade" ParamValue="AP">10</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="HyperLinkPlus9" runat="server" ParamName="Grade" ParamValue="AP">11</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="HyperLinkPlus10" runat="server" ParamName="Grade" ParamValue="AP">12</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="HyperLinkPlus11" runat="server" ParamName="Grade" ParamValue="AP">Summary 6-12</cc1:HyperLinkPlus>
        </NavigationLinks>
    </sli:NavigationLinkRow>
    <sli:NavigationLinkRow ID="nlrCourseTypeID" runat="server">
        <RowLabel>Show:</RowLabel>
        <NavigationLinks>
            <cc1:HyperLinkPlus ID="linkCourseTypeIDAP" runat="server" ParamName="CourseTypeID" ParamValue="1">Advanced Placement Program&reg;</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkCourseTypeIDCAPP" runat="server" ParamName="CourseTypeID" ParamValue="2">CAPP</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkCourseTypeIDOther" runat="server" ParamName="CourseTypeID" ParamValue="4">Other Courses</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkCourseTypeIDIB" runat="server" ParamName="CourseTypeID" ParamValue="6">International Baccalaureate</cc1:HyperLinkPlus>
        </NavigationLinks>
     </sli:NavigationLinkRow>
    <sli:NavigationLinkRow ID="nlrSubject" runat="server">
        <RowLabel>Subject:</RowLabel>
        <NavigationLinks>
            <cc1:HyperLinkPlus ID="HyperLinkPlus16" runat="server" ParamName="Subj" ParamValue="AP">English Language Arts</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="HyperLinkPlus17" runat="server" ParamName="Subj" ParamValue="AP">Mathematics</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="HyperLinkPlus18" runat="server" ParamName="Subj" ParamValue="AP">Science</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="HyperLinkPlus19" runat="server" ParamName="Subj" ParamValue="AP">Social Studies</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="HyperLinkPlus20" runat="server" ParamName="Subj" ParamValue="AP">Agricultural Education</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="HyperLinkPlus21" runat="server" ParamName="Subj" ParamValue="AP">Art &amp; Design</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="HyperLinkPlus22" runat="server" ParamName="Subj" ParamValue="AP">Business</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="HyperLinkPlus23" runat="server" ParamName="Subj" ParamValue="AP">Family and Consumer Ed</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="HyperLinkPlus24" runat="server" ParamName="Subj" ParamValue="AP">World Languages</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="HyperLinkPlus25" runat="server" ParamName="Subj" ParamValue="AP">Marketing Education</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="HyperLinkPlus26" runat="server" ParamName="Subj" ParamValue="AP">Music</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="HyperLinkPlus27" runat="server" ParamName="Subj" ParamValue="AP">Technology Education</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="HyperLinkPlus28" runat="server" ParamName="Subj" ParamValue="AP">Other</cc1:HyperLinkPlus>
        </NavigationLinks>
    </sli:NavigationLinkRow> 
    <sli:NavigationLinkRow ID="nlrVwBy" runat="server">
            <RowLabel>View By:</RowLabel>
            <NavigationLinks>
                <cc1:HyperLinkPlus ID="HyperLinkPlus1" runat="server" ParamName="VwBy" ParamValue="AP">All Students</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="HyperLinkPlus2" runat="server" ParamName="VwBy" ParamValue="AP">Gender</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="HyperLinkPlus3" runat="server" Enabled="false">Race/Ethnicity</cc1:HyperLinkPlus>
            </NavigationLinks>
     </sli:NavigationLinkRow>
    <sli:NavCompareTo ID="nlrCompareTo" runat="server" />
    </td></tr>
    <tr>
        <td>
            <asp:hyperlink ID="ChangeSelectedSchoolOrDistrict" Font-Size="Small" runat="server" />
        </td>
    </tr>
     <tr>
        <td>
            <slx:SligoDataGrid ID="SligoDataGrid2" runat="server" 
            AutoGenerateColumns="True"
                OnRowDataBound="SligoDataGrid2_RowDataBound">
                <Columns>
                <cc1:MergeColumn DataField="LinkedDistrictName">
                    <itemstyle horizontalalign="Left" VerticalAlign="Top" />
                </cc1:MergeColumn>
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
           
            <uc8:BottomLinkDownload id="BottomLinkDownload1" runat="server" Col="16"/>
            <SPAN class="text">
	            <p><a href="javascript:popup('http://dpi.wi.gov/spr/ret_use.html')" onClick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
            </p></SPAN>        
        </td>
    </tr>
    </table>
</asp:Content>