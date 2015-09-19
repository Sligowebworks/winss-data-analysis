<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="Retention.aspx.cs" Inherits="SligoCS.Web.WI.Retention" Title="Retention" %>

<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table style="padding:0px 0px 0px 0px; width:400px;">
    <tr>
        <td>
        <sli:NavSchoolType ID="nlrSTYP" runat="server" />
        <sli:NavViewByGroup ID="nlrVwByGroup" runat="server" />
        <sli:NavCompareTo ID="nlrCompareTo" runat="server" />
        <sli:NavSelectYear ID="nlrSelectYear" runat="server" /> 
        <sli:ChangeSelectedSchoolOrDistrictLink id="ChangeSelectedSchoolOrDistrict" runat="server" />
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
            <td style="width: 291px; height: 3px;" >
              <p align="left" style ="width:475px">
              <span class="text">
The method of calculating retention rates changed in 2004-05. See formula for calculating <a href="javascript:popup('http://dpi.wi.gov/spr/ret_q&a.html#rate')" onClick="setCookie(question, url)">rates</a>. Also note that 2004-05 was a year of transition to a new retention data collection, and as a result 2004-05 retention data may not be comprehensive. <a href="javascript:popup('http://dpi.wi.gov/spr/ret_q&a.html')" onClick="setCookie(question, url)">[More]</a>
</span></p> </td>
        </tr>
        <tr>
            <td>
                &nbsp;
                <slx:WinssDataGrid ID="RetentionDataGrid" runat="server" >
                <Columns>                                        
                   <slx:WinssDatagridColumn MergeRows="true" DataField="YearFormatted" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="LinkedName"  HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="District Name"  HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="OrgLevelLabel"  HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="OrgSchoolTypeLabel" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="SchooltypeLabel" HeaderText="School Type" />
                    <slx:WinssDatagridColumn DataField="RaceLabel" HeaderText="Race" />
                    <slx:WinssDatagridColumn DataField="SexLabel" HeaderText="Gender" />
                    <slx:WinssDatagridColumn DataField="GradeLabel"  HeaderText="Grade" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="DisabilityLabel"  HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="EconDisadvLabel"  HeaderText="&nbsp;" />
                    <slx:WinssDataGridColumn MergeRows="true" DataField="ELPLabel"  HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn DataField="Total Enrollment (K-12)" HeaderText="Total Fall Enrollment (K-12)**" FormatString="#,##0.###" />
                    <slx:WinssDatagridColumn Datafield="Completed_School_Term" HeaderText="Students who completed the school term"  FormatString="#,##0.###" />
                    <slx:WinssDatagridColumn Datafield="Number of Retentions" HeaderText="Number of Retentions"  FormatString="#,##0.###" />
                    <slx:WinssDatagridColumn Datafield="Retention Rate" FormatString="#,##0.00%" />
                </Columns>
                </slx:WinssDataGrid>
                 
            </td>
        </tr>
            <tr>
        <td>
        <asp:Panel ID="DefPanel" runat="server">
	        <SPAN class="text">
	        ** Enrollment counts in this column may cover a narrower grade range if the "view by: grade" option is selected or if counts are for a specific "school type" (e.g. High School). <a href="javascript:popup('http://dpi.wi.gov/spr/demog_q&a.html')" onClick="setCookie(question, url)">[More]</a>
	        </SPAN>
        </asp:Panel>
            <!-- <sli:BottomLinkEnrollmentCounts id="BottomLinkEnrollmentCounts1" runat="server"/> -->
            <sli:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            <sli:BottomLinkViewDistrictReport id="BottomLinkViewDistrictReport1" runat="server"/>
            <sli:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>
            <sli:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
            <sli:BottomLinkDownload id="BottomLinkDownload1" runat="server" Col="14"/>
            <SPAN class="text">
	            <p><a href="javascript:popup('http://dpi.wi.gov/spr/ret_use.html')" onClick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
            </SPAN>        
        </td>
    </tr>
    </table>
</asp:Content>
