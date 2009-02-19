<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="APTestsPage.aspx.cs" Inherits="SligoCS.Web.WI.APTestsPage" Title="Advanced Placement Program&reg; Exams" %>

<%--<%@ Register Src="WebUserControls/BottomLinks.ascx" TagName="BottomLinks" TagPrefix="uc1" %>--%>
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
        <td><uc2:ParamsLinkBox ID="ParamsLinkBox1" runat="server" ShowTR_CompareTo="true" ShowTR_Group="true" ShowTR_STYP="true" ShowTR_ShowGraphFile="true" /></td>
    </tr>
    <tr>
        <td><!--Chart goes here.--><Graph:GraphBarChart ID="barChart" runat="server"></Graph:GraphBarChart>
        </td>
    </tr>
    <tr>
        <td>
            <asp:hyperlink ID="ChangeSelectedSchoolOrDistrict" Font-Size="Small" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
        <SPAN class='text'>School level data are not available.</SPAN><br/>
	    <SPAN class="text">	
	     Major changes in WI student enrollment collections were implemented in 2004-05. 2004-05 enrollment data 
	    may not be comprehensive so 2004-05 test participation rates should be interpreted with caution. 
	    <a href="javascript:popup('http://www.dpi.wi.gov/spr/colleg_q&a.html')" onClick="setCookie(question, url)">[More]</a>
	    </SPAN>
        </td>
    </tr>
    <tr>
        <td>
            <cc1:SligoDataGrid ID="SligoDataGrid2" runat="server" 
            AutoGenerateColumns="False"
                ShowSuperHeader="False" SuperHeaderText="" 
                OnRowDataBound="SligoDataGrid2_RowDataBound" 
                UseAccessibleHeader="False" 
                BorderColor="#888888" BorderWidth="4px" 
                BorderStyle="Double" Width="460" >

                <Columns>
                <cc1:MergeColumn DataField="YearFormatted" 
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center"/>
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="LinkedDistrictName"
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Left" VerticalAlign="Top"/>
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="District Name"
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>

                <asp:BoundField DataField="RaceLabel" HeaderText="Race"
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SexLabel" HeaderText="Gender"
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="GradeLabel"  HeaderText="Grade" 
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" /> 
                </asp:BoundField>
                
                <cc1:MergeColumn DataField="enrollment" HeaderText="Total Fall Enrollment Grades 9-12**" 
                EnableMerge="false" 
                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="# Taking Exams" HeaderText="# Taking Exams" 
                EnableMerge="false" 
                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="% Taking Exams" HeaderText="% Taking Exams" 
                EnableMerge="false" 
                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="# Exams Taken" HeaderText="# Exams Taken" 
                EnableMerge="false" 
                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="# Exams Passed" HeaderText="# Exams Passed" 
                EnableMerge="false" 
                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="% of Exams Passed" 
                HeaderText="% of Exams Passed (Score of 3 or Above)" EnableMerge="false" 
                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                                
                </Columns>
            </cc1:SligoDataGrid>
        </td>
    </tr>
    <tr>
        <td>
            <br/>

            <uc10:BottomLinkEnrollmentCounts id="BottomLinkE2" runat="server"/>

            <uc6:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            
            <uc7:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>
            <uc11:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />

            <uc8:BottomLinkDownload id="BottomLinkDownload1" runat="server" Col="17"/>
                      
<%--             <uc9:BottomLinkWhatToConsider id="BottomLinkWhatToConsider1" runat="server"/>
--%>   
<SPAN class="text">
	<p><a href="javascript:popup('http://dpi.wi.gov/spr/colleg_use.html')" onClick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
</SPAN>         
        </td>
    </tr>
</table>        
</asp:Content>
