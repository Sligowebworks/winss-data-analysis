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
            <cc1:HyperLinkPlus ID="linkGradeGrade_6" runat="server" ParamName="Grade" ParamValue="40">6</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkGradeGrade_7" runat="server" ParamName="Grade" ParamValue="44">7</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkGradeGrade_8" runat="server" ParamName="Grade" ParamValue="48">8</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkGradeGrade_9" runat="server" ParamName="Grade" ParamValue="52">9</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkGradeGrade_10" runat="server" ParamName="Grade" ParamValue="56">10</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkGradeGrade_11" runat="server" ParamName="Grade" ParamValue="60">11</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkGradeGrade_12" runat="server" ParamName="Grade" ParamValue="64">12</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkGradeGrades_6_12_Combined" runat="server" ParamName="Grade" ParamValue="94">Grades 6-12</cc1:HyperLinkPlus>
        </NavigationLinks>
    </sli:NavigationLinkRow>
    <sli:NavigationLinkRow ID="nlrCourseTypeID" runat="server">
        <RowLabel>Show:</RowLabel>
        <NavigationLinks>
            <cc1:HyperLinkPlus ID="linkCourseTypeIDAP" runat="server" ParamName="CourseTypeID" ParamValue="1">Advanced Placement Program&reg;</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkCourseTypeIDCAPP" runat="server" ParamName="CourseTypeID" ParamValue="2">CAPP</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkCourseTypeIDIB" runat="server" ParamName="CourseTypeID" ParamValue="6">International Baccalaureate</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkCourseTypeIDOther" runat="server" ParamName="CourseTypeID" ParamValue="4">Other Courses</cc1:HyperLinkPlus>
        </NavigationLinks>
     </sli:NavigationLinkRow>
    <sli:NavigationLinkRow ID="nlrSubject" runat="server">
        <RowLabel>Subject:</RowLabel>
        <NavigationLinks>
            <cc1:HyperLinkPlus ID="linkWMASEnglish" runat="server" ParamName="WMAS" ParamValue="4">English Language Arts</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkWMASMathematics" runat="server" ParamName="WMAS" ParamValue="8">Mathematics</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkWMASScience" runat="server" ParamName="WMAS" ParamValue="10">Science</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkWMASSocialStudies" runat="server" ParamName="WMAS" ParamValue="11">Social Studies</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkWMASAgriculture" runat="server" ParamName="WMAS" ParamValue="1">Agricultural Education</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkWMASArt" runat="server" ParamName="WMAS" ParamValue="2">Art and Design</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkWMASBusiness" runat="server" ParamName="WMAS" ParamValue="3">Business</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkWMASFamily" runat="server" ParamName="WMAS" ParamValue="5">Family and Consumer Ed</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkWMASWorldLang" runat="server" ParamName="WMAS" ParamValue="6">World Languages</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkWMASMarketing" runat="server" ParamName="WMAS" ParamValue="7">Marketing Education</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkWMASMusic" runat="server" ParamName="WMAS" ParamValue="9">Music</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkWMASTechEd" runat="server" ParamName="WMAS" ParamValue="12">Technology Education</cc1:HyperLinkPlus>
            <cc1:HyperLinkPlus ID="linkWMASOther" runat="server" ParamName="WMAS" ParamValue="13">Other Subjects</cc1:HyperLinkPlus>
        </NavigationLinks>
    </sli:NavigationLinkRow> 
    <sli:NavViewByGroup ID="nlrVwByGroup" runat="server" />
    <sli:NavCompareTo ID="nlrCompareTo" runat="server" />
    </td></tr>
    <tr><td>
    <table width="100%"><tr>
        <td><sli:ChangeSelectedSchoolOrDistrictLink id="ChangeSelectedSchoolOrDistrictLink1" runat="server" /></td>
        <td align="right"><p>Go to: <a href="CoursesOffer.aspx<% Response.Write(GetQueryString(new String[1]{"Qquad=offerings.aspx"}));  %>">What advanced courses are offered?</a></p></td>
        </tr></table>
    </td></tr>
    <asp:Panel ID="pnlMessage" runat="server" Visible="false">
    <tr>
        <td>School level data are not available.</td>
    </tr>
    </asp:Panel>
     <asp:Panel ID="GraphPanel" runat="server">       
    <!--no graph-->
    </asp:Panel>    
      <tr>
        <td>
            <slx:WinssDataGrid ID="CoursesTakingDataGrid" runat="server" >
                <Columns>
                    <slx:WinssDatagridColumn MergeRows="true" DataField="YearFormatted" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="LinkedName"  HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="District Name"  HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="OrgSchoolTypeLabel" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="OrgLevelLabel" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="SchooltypeLabel" HeaderText="School Type" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="RaceLabel" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="GradeLabel"  HeaderText="&nbsp;" />
                    
                   <slx:WinssDataGridColumn DataField="enrollment" HeaderText="Total Fall Enrollment in <nobr>Grade(s)**</nobr>" FormatString="#,##0.###" MergeRows="true"/>
                    <slx:WinssDataGridColumn MergeRows="true" DataField="Course" HeaderText="Course Content" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="SexLabel" HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn DataField="# Who Took Course" HeaderText="Sum of Students Taking Courses" FormatString="#,##0.###"/>
                </Columns>
            </slx:WinssDataGrid>
        </td>
    </tr>

          <tr>
        <td>
            <sli:BottomLinkEnrollmentCounts id="BottomLinkEnrollmentCounts1" runat="server"/>
            <sli:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            <sli:BottomLinkViewDistrictReport id="BottomLinkViewDistrictReport1" runat="server"/>
            <sli:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>
            <sli:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
            <sli:BottomLinkDownload id="BottomLinkDownload1" runat="server" />
            <div class="text">
	            <a href="javascript:popup('http://dpi.wi.gov/spr/course_use.html')" onclick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
            </div>        
        </td>
    </tr>
    </table>
</asp:Content>