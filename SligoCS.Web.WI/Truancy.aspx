<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="Truancy.aspx.cs" Inherits="SligoCS.Web.WI.Truancy" Title="Truancy" %>

<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table style="padding:0px 0px 0px 0px; width:400px;">
    <tr>
        <td>
        <sli:NavSelectYear ID="nlrSelectYear" runat="server" />
        <sli:NavSchoolType ID="nlrSTYP" runat="server" />
        <sli:NavViewByGroup ID="nlrVwByGroup" runat="server" />
        <sli:NavCompareTo ID="nlrCompareTo" runat="server" />
        <sli:ChangeSelectedSchoolOrDistrictLink id="ChangeSelectedSchoolOrDistrict" runat="server" />
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
            <td style="width: 291px; height: 3px;" >
            <br/>
              <p align="left" style ="width:475px">
              <span class="text">
	Major changes in WI student enrollment collections were implemented in
	2004-05. 2004-05 enrollment data may not be comprehensive so some 2004-05
	truancy rates should be interpreted with caution. <a href="javascript:popup('http://spr.dpi.wi.gov/spr_tru_q&a#cautions')" onclick="setCookie(question, url)">[More]</a>

</span></p> </td>
        </tr>
        <tr>
            <td>
                &nbsp;
                <slx:WinssDataGrid ID="TruancyDataGrid" runat="server" >
                <Columns>                                        
                   <slx:WinssDatagridColumn MergeRows="true" DataField="YearFormatted" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="LinkedName"  HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="District Name"  HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="OrgSchoolTypeLabel" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="OrgLevelLabel" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="SchoolTypeLabel" HeaderText="School Type" />
                    <slx:WinssDatagridColumn DataField="RaceLabel" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn DataField="SexLabel" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn DataField="GradeLabel"  HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn DataField="Total Enrollment (K-12)" 
                            HeaderText="Total Fall Enrollment (K-12)**" FormatString="#,##0.###" />
                      <slx:WinssDatagridColumn DataField="Number of Students Habitually Truant"  FormatString="#,##0.###" />
                     <slx:WinssDatagridColumn DataField="Truancy Rate" FormatString="#,##0.0%" />
                </Columns>
                </slx:WinssDataGrid>
                 
            </td>
        </tr>
            <tr>
        <td>
        <asp:Panel ID="DefPanel" runat="server">
	        <div class="text">* <a href="javascript:popup('http://spr.dpi.wi.gov/spr_tru_q&a')" onclick="setCookie(question, url)">Definition of Habitual Truancy changed in 1998-99</a></div>
            <br/>
	        <div class="text">
	        ** Enrollment counts in this column may cover a narrower grade range if the "view by: grade" option is selected or if counts are for a specific "school type" (e.g. High School). <a href="javascript:popup('http://spr.dpi.wi.gov/spr_demog_q&a')" onclick="setCookie(question, url)">[More]</a>
	        </div>
        </asp:Panel>
            <!-- <sli:BottomLinkEnrollmentCounts id="BottomLinkEnrollmentCounts1" runat="server"/> -->
            <sli:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            <sli:BottomLinkViewDistrictReport id="BottomLinkViewDistrictReport1" runat="server"/>
            <sli:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>
            <sli:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
            <sli:BottomLinkDownload id="BottomLinkDownload1" runat="server" />
            <div class="text">
	            <a href="javascript:popup('http://spr.dpi.wi.gov/spr_tru_use')" onclick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
            </div>        
        </td>
    </tr>
    </table>
</asp:Content>
