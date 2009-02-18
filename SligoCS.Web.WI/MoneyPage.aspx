<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="MoneyPage.aspx.cs" Inherits="SligoCS.Web.WI.MoneyPage" Title="Money" %>

<%@ Register Src="WebUserControls/BottomLinks.ascx" TagName="BottomLinks" TagPrefix="uc1" %>
<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>
<%@ Register Src="WebUserControls/ParamsLinkBox.ascx" TagName="ParamsLinkBox" TagPrefix="uc2" %>

<%@ Register Src="WebUserControls/BottomLinkMoreData.ascx" TagName="BottomLinkMoreData" TagPrefix="uc5" %>
<%@ Register Src="WebUserControls/BottomLinkViewReport.ascx" TagName="BottomLinkViewReport" TagPrefix="uc6" %>
<%@ Register Src="WebUserControls/BottomLinkViewProfile.ascx" TagName="BottomLinkViewProfile" TagPrefix="uc7" %>
<%@ Register Src="WebUserControls/BottomLinkDownload.ascx" TagName="BottomLinkDownload" TagPrefix="uc8" %>
<%@ Register Src="WebUserControls/BottomLinkWhatToConsider.ascx" TagName="BottomLinkWhatToConsider" TagPrefix="uc9" %>
<%@ Register Src="WebUserControls/BottomLinkEnrollmentCounts.ascx" TagName="BottomLinkEnrollmentCounts" TagPrefix="uc10" %>

<%@ Register tagPrefix="Graph" namespace="SligoCS.Web.WI.WebUserControls" Assembly="SligoCS.Web.WI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>

<asp:Panel ID="DataPanel" runat="server">
    <tr>
        <td>
            <uc2:ParamsLinkBox ID="ParamsLinkBox1" runat="server" 
                ShowTR_CompareTo="true" 
                ShowTR_ShowMoney="true"
                ShowTR_TypeCost="true" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:hyperlink ID="ChangeSelectedSchoolOrDistrict" Font-Size="Small" runat="server" />
        </td>
    </tr> 
    <asp:Panel ID="GraphPanel" runat="server">       
    <tr>
        <td><!--Chart goes here.-->
        <Graph:GraphBarChart ID="barChart" runat="server"></Graph:GraphBarChart>
        </td>
    </tr>
    </asp:Panel>    
    <tr>
        <td>
            <SPAN class="text">		
	         <asp:Literal ID="DistrictDataProvided" runat="server" />School level data are not available. <br/>
	        </SPAN>
        </td>
    </tr>
    <tr>
        <td>
            <cc1:SligoDataGrid ID="SligoDataGrid2" runat="server" AutoGenerateColumns="False"
                OnRowDataBound="SligoDataGrid2_RowDataBound" UseAccessibleHeader="False" 
                BorderColor="#888888" BorderWidth="4px" BorderStyle="Double" Width="460" >
                <Columns>
                    <cc1:MergeColumn DataField="PriorYear"
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                        <itemstyle horizontalalign="Left" VerticalAlign="Top" />
                    </cc1:MergeColumn>
                    <cc1:MergeColumn DataField="LinkedName"
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                        <itemstyle horizontalalign="Center" />
                    </cc1:MergeColumn>                    
                    <cc1:MergeColumn DataField="DistState"
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                        <itemstyle horizontalalign="Center" />
                    </cc1:MergeColumn>
                    <cc1:MergeColumn DataField="District Name"
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                        <ItemStyle HorizontalAlign="Center" />
                    </cc1:MergeColumn>
                    <cc1:MergeColumn DataField="School Name"
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                        <ItemStyle HorizontalAlign="Center" />
                    </cc1:MergeColumn>                                        
                    <cc1:MergeColumn DataField="Category" HeaderText=" "
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                        <itemstyle horizontalalign="Left"  VerticalAlign="Top"  />
                    </cc1:MergeColumn>
                    <cc1:MergeColumn DataField="Revenue" HeaderText="Revenue"
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                        <itemstyle horizontalalign="Center" />
                    </cc1:MergeColumn>
                    <cc1:MergeColumn DataField="Cost" HeaderText="Cost"
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                        <itemstyle horizontalalign="Center" />
                    </cc1:MergeColumn>
                    <cc1:MergeColumn DataField="Revenue Per Member" EnableMerge="false" HeaderText="Revenue Per Member"
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                        <ItemStyle HorizontalAlign="Center" />
                    </cc1:MergeColumn>
                    <cc1:MergeColumn DataField="Cost Per Member" HeaderText="Cost Per Member" EnableMerge="false"
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                    </cc1:MergeColumn>
                    <cc1:MergeColumn DataField="Percent of Total" HeaderText="Percent of Total" EnableMerge="false"
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
    </asp:Panel>
    
    <!--asp:Panel ID="NoDataForSchoolPanel" run at="server">
    <tr>
    <td>
The data you have requested are not available for the school you have selected. The cause may be that the data are not available for the grade range of your school. For example, if the grade range of your school is kindergarten through second grade, then you will not get any data under "Examining School Performance on Statewide Tests" because no statewide tests are given to students in these grades. Another possible cause is that you selected a newly opened school, and the data you requested are not yet available. 
You may click on the "Back" button of your browser to select another type of data, or click on "change school" to select a different school. If you feel you have received this page in error, please email  <A HREF="mailto:winss@dpi.wi.gov">winss@dpi.wi.gov</A>. 
    </td>
    </tr>
    </asp:Panel-->
    
    
    <tr>
        <td>            
            <br/>
            <uc5:BottomLinkMoreData id="BottomLinkMoreData1" runat="server"/>

            <uc6:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            
            <uc7:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>
            
            <uc8:BottomLinkDownload id="BottomLinkDownload1" runat="server" Col="16"/>
                      
            <uc9:BottomLinkWhatToConsider id="BottomLinkWhatToConsider1" runat="server"/>

        </td>
    </tr>
</table>        
</asp:Content>
