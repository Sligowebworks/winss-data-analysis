<%@ Page Language="C#"   MasterPageFile="~/WI.Master" AutoEventWireup="true"  CodeBehind="WRCTPerformance.aspx.cs" Inherits="SligoCS.Web.WI.WRCTPerformance" Title="WRCT" %>

<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI" TagPrefix="cc1" %>

      
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table style="padding:0px 0px 0px 0px; width:400px;">
    <tr><td>
                <sli:NavSelectYear ID="nlrSelectYear" runat="server" /> 
               
        <sli:NavigationLinkRow ID="nlrLevel" runat="server">
                    <RowLabel>Level:</RowLabel>
                    <NavigationLinks>
                       <cc1:HyperLinkPlus ID="linkLevelAdvanced" runat="server" ParamName="Level" ParamValue="ADV">Advanced</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkLevelAdvancedProficient" runat="server" ParamName="Level" ParamValue="A-P">Advanced + Proficient</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkLevelAdvProfBas" runat="server" ParamName="Level" ParamValue="A-P-B">Advanced + Proficient + Basic</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkLevelMinPerfNotTested" runat="server" ParamName="Level" ParamValue="M-NT-wrct">Min Perf + Not Tested on WRCT</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkLevelMinimumPerf" runat="server" ParamName="Level" ParamValue="MIN">Min Perf</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="linkLevelNotTested" runat="server" ParamName="Level" ParamValue="NT-wrct">Not Tested on WRCT</cc1:HyperLinkPlus>
                    </NavigationLinks>
                </sli:NavigationLinkRow>
                
                 <sli:NavCompareTo ID="nlrCompareTo" runat="server" />
        <sli:ChangeSelectedSchoolOrDistrictLink id="ChangeSelectedSchoolOrDistrictLink1" runat="server" />
                
    </td></tr>
    
     <asp:Panel ID="GraphPanel" runat="server">    
    <tr>
        <td>
            <Graph:GraphBarChart ID="barChart" runat="server"></Graph:GraphBarChart>

        </td>
    </tr>    
    </asp:Panel>

      <tr>
        <td>
                <slx:WinssDataGrid ID="WrctDataGrid" runat="server" >
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
                     <slx:WinssDatagridColumn DataField="Enrolled" HeaderText="Enrolled at Test Time" FormatString="#,##0.###" />
                     <slx:WinssDatagridColumn DataField="Percent Not Tested" FormatString="#,##0.0%" />
                     <slx:WinssDatagridColumn DataField="Percent Minimal Performance" FormatString="#,##0.0%" />
                     <slx:WinssDatagridColumn DataField="Percent Basic" FormatString="#,##0.0%" />
                     <slx:WinssDatagridColumn DataField="Percent Proficient" FormatString="#,##0.0%" />
                     <slx:WinssDatagridColumn DataField="Percent Advanced" FormatString="#,##0.0%" />
                     <slx:WinssDatagridColumn DataField="Advanced + Proficient Total" FormatString="#,##0.0%" />
                     <slx:WinssDatagridColumn DataField="Advanced + Proficient + Basic Total" FormatString="#,##0.0%" />
                     <slx:WinssDatagridColumn DataField="Minimal Performance + Not Tested on WRCT Total" FormatString="#,##0.0%" />
                </Columns>
                </slx:WinssDataGrid>
        
        </td>
    </tr><tr>
        <td>
        <p>* Wisconsin Reading Comprehension Test (WRCT): An Assessment of Primary-Level Reading at Grade 3. This test has been discontinued. It was last administered in March 2005. </p>
        <br/>
            <!-- <sli:BottomLinkEnrollmentCounts id="BottomLinkEnrollmentCounts1" runat="server"/> -->
            <sli:BottomLinkViewReport id="BottomLinkViewReport2" runat="server"/>
            <sli:BottomLinkViewDistrictReport id="BottomLinkViewDistrictReport1" runat="server"/>
            <sli:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>
            <sli:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
            <sli:BottomLinkDownload id="BottomLinkDownload2" runat="server" Col="14"/>
            <p><span class="text">
	            <a href="javascript:popup('http://oeahist.dpi.wi.gov/ohist_wrct')" onclick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
            </span> </p>       
        </td>
    </tr>

</table>        
</asp:Content>
