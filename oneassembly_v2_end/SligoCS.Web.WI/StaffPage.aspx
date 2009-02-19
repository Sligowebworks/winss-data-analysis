<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="StaffPage.aspx.cs" Inherits="SligoCS.Web.WI.StaffPage" Title="Staff" %>

<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.WI.WebServerControls.WI"
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
<table style="padding:0px 0px 0px 0px; width:400px;">
    <tr>
        <td><uc2:ParamsLinkBox ID="ParamsLinkBox1" runat="server" 
        ShowTR_CompareTo="true" showTR_ShowRatioOfStaff="true" /></td>
    </tr>
    <asp:Panel ID="GraphPanel" runat="server">    
    <tr>
        <td>
            <Graph:GraphBarChart ID="barChart" runat="server"></Graph:GraphBarChart>
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
            <SPAN class='text'>School level data are not available. </SPAN>
	        <SPAN class="text">	
	         Major changes in WI student enrollment collections were implemented in 2004-05. 2004-05 enrollment data 
	        may not be comprehensive so some 2004-05 ratios should be interpreted with caution.  
	        <a href="javascript:popup('http://www.dpi.wi.gov/spr/staff_q&a.html')" onClick="setCookie(question, url)">[More]</a>
	        <br/>
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

                <cc1:MergeColumn DataField="District Name"
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </cc1:MergeColumn>
                    
                    
                    <cc1:MergeColumn DataField="LinkedDistrictName" 
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center"/>
                    </cc1:MergeColumn>

                    <cc1:MergeColumn DataField="DistState" 
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center"/>
                    </cc1:MergeColumn>

                    
                    
                    <cc1:MergeColumn DataField="Category" EnableMerge="false" 
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center"/>
                    </cc1:MergeColumn>
                    <cc1:MergeColumn DataField="Number FTE Staff" 
                            EnableMerge="false" HeaderText="Number FTE Staff" 
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center"/>
                    </cc1:MergeColumn>
                    <cc1:MergeColumn DataField="Ratio of Students to FTE Staff" 
                        HeaderText="Ratio of Students to FTE Staff" 
                        EnableMerge="false" 
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center"/>
                    </cc1:MergeColumn>
                    <cc1:MergeColumn DataField="FTE Staff per 100 Students" 
                    HeaderText="FTE Staff per 100 Students" EnableMerge="false" 
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center"/>
                    </cc1:MergeColumn>
                </Columns>
            </cc1:SligoDataGrid>
        </td>
    </tr>
    <tr>
        <td>
            <uc10:BottomLinkEnrollmentCounts id="BottomLinkEnrollmentCounts1" runat="server"/>

            <uc6:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            <uc7:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>
          
            <uc8:BottomLinkDownload id="BottomLinkDownload1" runat="server" Col="15"/>
            <SPAN class="text">
	            <p><a href="javascript:popup('http://dpi.wi.gov/spr/staff_use.html')" onClick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
            </SPAN>        
        </td>
    </tr>
</table>        
</asp:Content>
