<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="StaffPage.aspx.cs" Inherits="SligoCS.Web.WI.StaffPage" Title="Staff" %>

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
    <tr>
        <td>
            <sli:NavSelectYear ID="nlrSelectYear" runat="server" /> 
            <sli:NavigationLinkRow ID="nlrShow" runat="server">
                <RowLabel>Show Ratio of: </RowLabel>
                <NavigationLinks>
                    <cc1:HyperLinkPlus ID="linkStaffRatioStudent" runat="server" ParamName="StaffRatio" ParamValue="STUDENTSTAFF">Students to Staff</cc1:HyperLinkPlus>
                    <cc1:HyperLinkPlus ID="linkStaffRatioStaff" runat="server" ParamName="StaffRatio" ParamValue="STAFFSTUDENT">Staff to Students</cc1:HyperLinkPlus>

                </NavigationLinks>
            </sli:NavigationLinkRow>
            
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
        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
    <tr>
        <td>School level data are not available.</td>
    </tr>
    </asp:Panel>
    <tr>
        <td>
	        <SPAN class="text">	
	         Major changes in WI student enrollment collections were implemented in 2004-05. 2004-05 enrollment data may not be comprehensive so some 2004-05 student-staff ratios should be interpreted with caution. Due to 2003-04 changes in the WI fall staff data collection, pre- and post-2003-04 staff counts may not be not comparable. 
	        <a href="javascript:popup('http://www.dpi.wi.gov/spr_staff_q&a')" onClick="setCookie(question, url)">[More]</a>
	        <br/>
	        </SPAN> 
        </td>
    </tr>
 
    <tr>
        <td>
            <slx:WinssDataGrid ID="StaffDataGrid" runat="server" >
                <Columns>
                     <slx:WinssDatagridColumn MergeRows="true" DataField="YearFormatted" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="LinkedName"  HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="District Name"  HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="OrgSchoolTypeLabel" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="OrgLevelLabel" HeaderText="&nbsp;" />
                    <slx:WinssDatagridColumn MergeRows="true" DataField="SchoolTypeLabel" HeaderText="School Type" />
                    <slx:WinssDatagridColumn DataField="Category" HeaderText="&nbsp;"/>
                    <slx:WinssDatagridColumn DataField="Number FTE Staff" FormatString="#,##0.0" />
                    <slx:WinssDatagridColumn DataField="Ratio of Students to FTE Staff" HeaderText="Ratio of Students to FTE Staff" FormatString="#,##0.0" />
                    <slx:WinssDatagridColumn DataField="FTE Staff per 100 Students" FormatString="#,##0.0" />
                </Columns>
            </slx:WinssDataGrid>
        </td>
    </tr>
    <tr>
        <td>
        <br/>
            <!-- <sli:BottomLinkEnrollmentCounts id="BottomLinkEnrollmentCounts1" runat="server"/> -->
            <sli:BottomLinkViewReport id="BottomLinkViewReport2" runat="server"/>
            <sli:BottomLinkViewDistrictReport id="BottomLinkViewDistrictReport1" runat="server"/>
            <sli:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>
            <sli:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
            <sli:BottomLinkDownload id="BottomLinkDownload2" runat="server" Col="14"/>
            <SPAN class="text">
	            <p><a href="javascript:popup('http://spr.dpi.wi.gov/spr_staff_use')" onClick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
            </SPAN>        
        </td>
    </tr>
</table>        
</asp:Content>
