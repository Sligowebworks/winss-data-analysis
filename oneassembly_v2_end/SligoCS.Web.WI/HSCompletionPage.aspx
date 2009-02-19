<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="HSCompletionPage.aspx.cs" Inherits="SligoCS.Web.WI.HSCompletionPage" Title="High School Completion" %>

<%@ Register Assembly="ChartFX.WebForms" Namespace="ChartFX.WebForms" TagPrefix="chartfx7" %>
<%@ Register Assembly="ChartFX.WebForms.Adornments" Namespace="ChartFX.WebForms.Adornments"
    TagPrefix="chartfxadornments" %>
<%@ Register Assembly="ChartFX.WebForms" Namespace="ChartFX.WebForms.Galleries" TagPrefix="chartfx7galleries" %>

<%@ Register Src="WebUserControls/ParamsLinkBox.ascx" TagName="ParamsLinkBox" TagPrefix="uc1" %>
<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.WI.WebServerControls.WI"
    TagPrefix="cc1" %>

<%@ Register Src="WebUserControls/BottomLinkMoreData.ascx" TagName="BottomLinkMoreData" TagPrefix="uc5" %>
<%@ Register Src="WebUserControls/BottomLinkViewReport.ascx" TagName="BottomLinkViewReport" TagPrefix="uc6" %>
<%@ Register Src="WebUserControls/BottomLinkViewProfile.ascx" TagName="BottomLinkViewProfile" TagPrefix="uc7" %>
<%@ Register Src="WebUserControls/BottomLinkDownload.ascx" TagName="BottomLinkDownload" TagPrefix="uc8" %>
<%@ Register Src="WebUserControls/BottomLinkWhatToConsider.ascx" TagName="BottomLinkWhatToConsider" TagPrefix="uc9" %>
<%@ Register Src="WebUserControls/BottomLinkEnrollmentCounts.ascx" TagName="BottomLinkEnrollmentCounts" TagPrefix="uc10" %>
<%@ Register Src="WebUserControls/BottomLinkWhyNotReported.ascx" 
TagName="BottomLinkWhyNotReported" TagPrefix="uc11" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>
    <tr>
        <td>
            <uc1:ParamsLinkBox ID="ParamsLinkBox1" runat="server" 
            ShowTR_Group="true" ShowTR_CompareTo="true" 
            ShowTR_Credential="true" />
        </td>
    </tr>
    <asp:Panel ID="GraphPanel" runat="server">    
    <tr>
        <td>
<%--            <chartfx7:Chart ID="ChartOnPage" runat="server">
            </chartfx7:Chart>--%>
        </td>
    </tr>    
    </asp:Panel>

    <tr>
        <td>
            <asp:hyperlink ID="ChangeSelectedSchoolOrDistrict" Font-Size="Small" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
	<SPAN class="text"><br/>
	Graduation (regular diploma) and completion rate reporting changed in 1998-99 and 2003-04.  2003-04 was a year of transition to a new student data collection, 
	and as a result 2003-04 high school completion data may not be comprehensive. 
	<a href="javascript:popup('http://www.dpi.wi.gov/spr/grad_q&a.html')" onClick="setCookie(question, url)">[More]</a> 
	See formula for calculating <a href="javascript:popup('http://www.dpi.wi.gov/spr/grad_q&a.html#rate')" onClick="setCookie(question, url)">rates</a>.
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
                <cc1:MergeColumn DataField="SchooltypeLabel" HeaderText="School Type"
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
                
                <asp:BoundField DataField="DisabilityLabel"
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField>
                
                <asp:BoundField DataField="EconDisadvLabel"
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ELPLabel"
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField> 
                
                
                <cc1:MergeColumn DataField="Total Enrollment Grade 12" 
                    HeaderText="Total Enrollment Grade 12" EnableMerge="false"                    
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="Total Expected to Complete High School" 
                HeaderText="Total Expected to Complete High School**" EnableMerge="false"                    
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="Cohort_Dropouts_Count" 
                HeaderText="Cohort Dropouts" EnableMerge="false"                    
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>


                <asp:BoundField DataField="Students Who Reached the Maximum Age" 
                            HeaderText="Students Who Reached the Maximum Age"                    
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />

                </asp:BoundField>
                <asp:BoundField DataField="Certificates" HeaderText="Certificates"
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />

                </asp:BoundField>                                                                                                               
                <asp:BoundField DataField="HSEDs" HeaderText="HSEDs"                    
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />

                </asp:BoundField>                                                                                                               
                <asp:BoundField DataField="Regular Diplomas" HeaderText="Regular Diplomas"
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField>                                                                                                              
                </Columns>
            </cc1:SligoDataGrid>
        </td>
    </tr>
    <tr>
        <td>
	<SPAN class="text"></SPAN>
	
<br><SPAN class="text"><p>**Total Expected to Complete High School is a count of students who were expected to complete high school in the year indicated whether or not the students actually did.  This total includes actual high school completers, cohort dropouts, and noncompleters who reached the maximum age associated with the constitutional right to a free public education. <a href="javascript:popup('http://dpi.wi.gov/winss/perfacademic_glossary.html#expected_completer')" onClick="setCookie(question, url)">[More]</a></SPAN><SPAN class="text">	
</SPAN>

            <uc6:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            <uc11:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported2" runat="server" />
            <!--uc7:BottomLinkViewProfile id="BottomLinkViewProfile1" run at="server"/-->
            
            <uc8:BottomLinkDownload id="BottomLinkDownload1" runat="server" Col="25"/>
                      
             <uc9:BottomLinkWhatToConsider id="BottomLinkWhatToConsider1" runat="server"/>
        
        </td>
    </tr>
</table>
</asp:Content>
