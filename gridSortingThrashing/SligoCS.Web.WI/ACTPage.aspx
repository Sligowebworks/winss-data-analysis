<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="ACTPage.aspx.cs" Inherits="SligoCS.Web.WI.ACTPage" Title="ACT" %>

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
<table>
    <tr>
        <td>
        <sli:NavigationLinkRow ID="nlrShow" runat="server">
            <RowLabel>Show:</RowLabel>
            <NavigationLinks>
                <cc1:HyperLinkPlus ID="HyperLinkPlus1" runat="server" ParamName="Show" ParamValue="1">ACT</cc1:HyperLinkPlus>
                  <cc1:HyperLinkPlus ID="HyperLinkPlus2" runat="server" ParamName="Show" ParamValue="2">Advanced Placement Program &reg; Exams</cc1:HyperLinkPlus>
            </NavigationLinks>
        </sli:NavigationLinkRow>
        
        <sli:NavigationLinkRow ID="nlrSubject" runat="server">
            <RowLabel>Subject: </RowLabel>
            <NavigationLinks>
                <cc1:HyperLinkPlus ID="HyperLinkPlus3" runat="server" ParamName="Subj" ParamValue="1">Reading</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="HyperLinkPlus4" runat="server" ParamName="Subj" ParamValue="1">English</cc1:HyperLinkPlus>                
                <cc1:HyperLinkPlus ID="HyperLinkPlus5" runat="server" ParamName="Subj" ParamValue="1">Mathematics</cc1:HyperLinkPlus>                
                <cc1:HyperLinkPlus ID="HyperLinkPlus6" runat="server" ParamName="Subj" ParamValue="1">Science</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="HyperLinkPlus7" runat="server" ParamName="Subj" ParamValue="1">Summary</cc1:HyperLinkPlus>
               </NavigationLinks>
        </sli:NavigationLinkRow>
        
        <sli:NavigationLinkRow ID="nlrViewBy" runat="server">
            <RowLabel>View By: </RowLabel>
            <NavigationLinks>
                <cc1:HyperLinkPlus ID="HyperLinkPlus8" runat="server" ParamName="VwBy" ParamValue="1">All Students</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="HyperLinkPlus9" runat="server" ParamName="VwBy" ParamValue="1">Gender</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="HyperLinkPlus10" runat="server" ParamName="VwBy" ParamValue="1">Race/Ethnicity</cc1:HyperLinkPlus>
            </NavigationLinks>
        </sli:NavigationLinkRow>
        
        <sli:NavCompareTo ID="nlrCompareTo" runat="server" />
        
        </td>
    </tr>
    <tr>
        <td><!--Chart goes here.-->
        <Graph:GraphBarChart ID="barChart" runat="server"></Graph:GraphBarChart>
        </td>
    </tr>
    <tr>
        <td>
            <asp:hyperlink ID="ChangeSelectedSchoolOrDistrict" Font-Size="Small" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            <SPAN class="text">	
            ACT results are for public school students in grade 12 who took the ACT as juniors or seniors. 
            Major changes in WI student enrollment collections were implemented in 2004-05. 2004-05 enrollment data 
            may not be comprehensive so 2004-05 test participation rates should be interpreted with caution. 
            <a href="javascript:popup('http://www.dpi.wi.gov/spr/colleg_q&a.html')" onClick="setCookie(question, url)">[More]</a>
            </SPAN>
        </td>
    </tr>
    <tr>
        <td>
            <slx:SligoDataGrid ID="SligoDataGrid2" runat="server" 
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
              
                
                <cc1:MergeColumn DataField="enrollment" HeaderText="Total Fall Enrollment Grade 12" 
                EnableMerge="false"                    
                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="Pupilcount" HeaderText="Number Tested" EnableMerge="false"                   
                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="Perc Tested" HeaderText="% Tested" EnableMerge="false"                   
                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="Reading" HeaderText="Average Score - Reading" EnableMerge="false"                   
                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="English" HeaderText="Average Score - English" EnableMerge="false"                   
                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="Math" HeaderText="Average Score - Math" EnableMerge="false"                   
                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="Science" HeaderText="Average Score - Science" EnableMerge="false"                   
                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="Composite" HeaderText="Average Score - Composite" 
                EnableMerge="false"                   
                HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                </Columns>
            </slx:SligoDataGrid>
        </td>
    </tr>
    <tr>
        <td>
                        
            <asp:Panel ID="TextForRaceInBottomLink" runat="server">
                <p>
                </p>
                <span class='text'>If the number of test takers in the "No Response/Other" category
                    is large, then data by racial/ethnic group should be interpreted with caution. </span>
            </asp:Panel>  
            <br/>

            <uc6:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            <uc7:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>
            <uc11:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
            <uc8:BottomLinkDownload id="BottomLinkDownload1" runat="server" Col="16"/>
                      
<%--             <uc9:BottomLinkWhatToConsider id="BottomLinkWhatToConsider1" runat="server"/>
--%>   
<SPAN class="text">
	<p><a href="javascript:popup('http://dpi.wi.gov/spr/colleg_use.html')" onClick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
</SPAN>   
        </td>
    </tr>
</table>        
</asp:Content>
