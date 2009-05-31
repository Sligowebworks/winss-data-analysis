<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="Suspensions.aspx.cs" Inherits="SligoCS.Web.WI.Suspensions" Title="What percentage of students were suspended or expelled?" %>

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

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table style="padding:0px 0px 0px 0px; width:400px;">
    <tr>
        <td>
         <sli:NavSchoolType ID="nlrSTYP" runat="server" />
         <sli:NavigationLinkRow ID="nlrShow" runat="server">
                <RowLabel>Show:</RowLabel> 
                <NavigationLinks>
                   <cc1:HyperLinkPlus ID="HyperLinkPlus6" runat="server" UrlFile="Suspensions.aspx" ParamName="Show" ParamValue="Su">Suspensions</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="HyperLinkPlus7" runat="server" UrlFile="Expulsions.aspx" ParamName="Show" ParamValue="Ex">Expulsions</cc1:HyperLinkPlus>
                </NavigationLinks>
         </sli:NavigationLinkRow>
        <sli:NavigationLinkRow ID="nlrVwBy" runat="server">
            <RowLabel>View By:</RowLabel>
            <NavigationLinks>
                <cc1:HyperLinkPlus ID="HyperLinkPlus1" runat="server" ParamName="VwBy" ParamValue="AP">All Students</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="HyperLinkPlus2" runat="server" ParamName="VwBy" ParamValue="AP">Gender</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="HyperLinkPlus3" runat="server" ParamName="VwBy" ParamValue="AP">Race/Ethnicity</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="HyperLinkPlus4" runat="server" ParamName="VwBy" ParamValue="AP">Grade</cc1:HyperLinkPlus>
                <cc1:HyperLinkPlus ID="HyperLinkPlus5" runat="server" ParamName="VwBy" ParamValue="Gr">Disability</cc1:HyperLinkPlus>
            </NavigationLinks>
        </sli:NavigationLinkRow>
        <sli:NavCompareTo ID="nlrCompareTo" runat="server" />

         </td>
    </tr>
    <asp:Panel ID="GraphPanel" runat="server">    
    <tr>
        <td>
<%--            <chartfx7:Chart ID="ChartOnPage" run at="server">
            </chartfx7:Chart>--%>
           <Graph:GraphBarChart ID="barChart" runat="server"></Graph:GraphBarChart>
        </td>
    </tr>    
    </asp:Panel>

    <tr>
        <td>
            <asp:hyperlink ID="ChangeSelectedSchoolOrDistrict" Font-Size="Small"  runat="server" />
        </td>
    </tr>
        <tr>
            <td style="width: 291px; height: 3px;" >
            <br/>
              <p align="justify" style ="width:400px">
              <span class="text">
	Major changes in WI discipline data collection systems were implemented in 2006-07. WINSS data about suspensions and expulsions were included in this transition year collection and are not comprehensive so should be interpreted with caution. See other <a href="javascript:popup('http://dpi.wi.gov/spr/discip_q&a.html')" onClick="setCookie(question, url)">cautions</a> and information about discipline data on WINSS.

</span></p> </td>
        </tr>
        <tr>
            <td>
                &nbsp;
                <slx:SligoDataGrid ID="SligoDataGrid1" runat="server" 
                AutoGenerateColumns="False"
                ShowSuperHeader="False" SuperHeaderText="" 
                OnRowDataBound="SligoDataGrid1_RowDataBound" 
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
                
                
                
                <cc1:MergeColumn DataField="Total Enrollment PreK-12" 
                        HeaderText="Total Enrollment PreK-12" EnableMerge="false"
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                
                <cc1:MergeColumn DataField="Number of Students Suspended" 
                        HeaderText="Number of Students Suspended" EnableMerge="false"
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                <cc1:MergeColumn DataField="Suspension Percent" 
                        HeaderText="Suspension Percent" EnableMerge="false"
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
        <%--<asp:Panel ID="DefPanel" runat="server">
	        <SPAN class="text">* <A HREF=javascript:popup('http://dpi.wi.gov/spr/tru_q&a.html') onClick='setCookie(question, url)'>Definition of Habitual Truancy changed in 1998-99</A></SPAN>
        </asp:Panel>--%>
            <uc10:BottomLinkEnrollmentCounts id="BottomLinkEnrollmentCounts1" runat="server"/>
            <uc6:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            <uc7:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>

            <uc11:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
           
            <uc8:BottomLinkDownload id="BottomLinkDownload1" runat="server" Col="14"/>
            <SPAN class="text">
	            <p><a href="javascript:popup('http://dpi.wi.gov/spr/discip_use.html')" onClick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
            </SPAN>        
        </td>
    </tr>
    </table>
</asp:Content>
