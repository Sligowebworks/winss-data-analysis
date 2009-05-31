<%@ Page Language="C#"   MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="ComparePerformance.aspx.cs" Inherits="SligoCS.Web.WI.ComparePerformance" %>
<%@ Register Assembly="SligoCS.Web.Base" Namespace="SligoCS.Web.Base.WebServerControls.WI"
    TagPrefix="cc1" %>
<%@ Register Src="WebUserControls/ParamsLinkBox.ascx" TagName="ParamsLinkBox" TagPrefix="uc2" %>
<%@ Register Src="WebUserControls/BottomLinkMoreData.ascx" TagName="BottomLinkMoreData" TagPrefix="uc5" %>
<%@ Register Src="WebUserControls/BottomLinkViewReport.ascx" TagName="BottomLinkViewReport" TagPrefix="uc6" %>
<%@ Register Src="WebUserControls/BottomLinkViewProfile.ascx" TagName="BottomLinkViewProfile" TagPrefix="uc7" %>
<%@ Register Src="WebUserControls/BottomLinkDownload.ascx" TagName="BottomLinkDownload" TagPrefix="uc8" %>
<%@ Register Src="WebUserControls/BottomLinkWhatToConsider.ascx" TagName="BottomLinkWhatToConsider" TagPrefix="uc9" %>
<%@ Register Src="WebUserControls/BottomLinkWhyNotReported.ascx" TagName="BottomLinkWhyNotReported" TagPrefix="uc11" %>
<%@ Register tagPrefix="Graph" namespace="SligoCS.Web.WI.WebUserControls" Assembly="SligoCS.Web.WI" %>
<%@ Register Src="~/WebUserControls/NavigationLinkRow.ascx"  TagName="NavigationLinkRow" TagPrefix="sli"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table style="padding:0px 0px 0px 0px; width:400px;">
    <tr><td>
        <sli:NavigationLinkRow ID="nlrGrade" runat="server">
            <RowLabel>Grade:</RowLabel>
            <NavigationLinks>
                        <cc1:HyperLinkPlus ID="grade3" runat="server" ParamName="Grade" ParamValue="3">3</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="grade4" runat="server" ParamName="Grade" ParamValue="4">4</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="grade5" runat="server" ParamName="Grade" ParamValue="5">5</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="grade6" runat="server" ParamName="Grade" ParamValue="6">6</cc1:HyperLinkPlus>
                         <cc1:HyperLinkPlus ID="grade7" runat="server" ParamName="Grade" ParamValue="7">7</cc1:HyperLinkPlus>
                          <cc1:HyperLinkPlus ID="grade8" runat="server" ParamName="Grade" ParamValue="8">8</cc1:HyperLinkPlus>
                          <cc1:HyperLinkPlus ID="grade9" runat="server" ParamName="Grade" ParamValue="9">9</cc1:HyperLinkPlus>
                           <cc1:HyperLinkPlus ID="grade10" runat="server" ParamName="Grade10" ParamValue="10">10</cc1:HyperLinkPlus>
                            <cc1:HyperLinkPlus ID="gradeCombined" runat="server" >Combined Grades</cc1:HyperLinkPlus>
            </NavigationLinks>
        </sli:NavigationLinkRow>
        <sli:NavigationLinkRow ID="nlrSubject" runat="server">
            <RowLabel>Subject: </RowLabel>
            <NavigationLinks>
                        <cc1:HyperLinkPlus ID="HyperLinkPlus2" runat="server" ParamName="Subject" ParamValue="2">Reading</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="HyperLinkPlus3" runat="server" ParamName="Subject" ParamValue="3">Language Arts</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="HyperLinkPlus4" runat="server" ParamName="Subject" ParamValue="4">Mathematics</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="HyperLinkPlus5" runat="server" ParamName="Subject" ParamValue="5">Science</cc1:HyperLinkPlus>
                         <cc1:HyperLinkPlus ID="HyperLinkPlus6" runat="server" ParamName="Subject" ParamValue="6">Social Studies</cc1:HyperLinkPlus>
            </NavigationLinks>
        </sli:NavigationLinkRow>
        
        <sli:NavigationLinkRow ID="nlrWkceCombined" runat="server">
                    <RowLabel><!-- --></RowLabel>
                    <NavigationLinks>
                        <cc1:HyperLinkPlus ID="HyperLinkPlus13" runat="Server" ParamName="Level" ParamValue="1">[WSAS: WKCE and WAA Combined</cc1:HyperLinkPlus>
                        <cc1:HyperLinkPlus ID="HyperLinkPlus14" runat="server" ParamName="Level" ParamValue="2">WKCE Only]</cc1:HyperLinkPlus>
                    </NavigationLinks>
                </sli:NavigationLinkRow>
                
    </td></tr>
    <tr>
        <td>
            <asp:hyperlink ID="ChangeSelectedSchoolOrDistrict" Font-Size="Small" runat="server" />
        </td>
    </tr>
     <asp:Panel ID="GraphPanel" runat="server">    
    <tr>
        <td>
            <Graph:GraphBarChart ID="barChart" runat="server"></Graph:GraphBarChart>
            	<SPAN class="text">
            	<br>* <a href="javascript:popup('http://dpi.wi.gov/oea/profdesc.html')" onClick='setCookie(question, url)'>Proficiency data for November 2002 and later are not comparable to earlier years.</a>  Some subject area tests are given only at grades 4, 8, and 10. 
				FAY = full academic year. <a href="javascript:popup('http://dpi.wi.gov/oea/kce_q&a.html')" onClick='setCookie(question, url)'>What are WSAS, WKCE, and WAA?</a><br>
	
	</SPAN>
        </td>
    </tr>    
    </asp:Panel>

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
                
                
                <asp:BoundField DataField="Enrollment" HeaderText="Total Fall Enrollment K-12**"
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Students who completed the school term" HeaderText="Students Who Completed the School Term"
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField>
                 <asp:BoundField DataField="Students expected to complete the school term" HeaderText="Students Expected To Complete the School Term"
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center"/>
                </asp:BoundField>
                <asp:BoundField DataField="Drop Outs" HeaderText="Number of Dropouts"
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Drop Out Rate" HeaderText="Dropout Rate"
                    HeaderStyle-BorderColor="Gray" HeaderStyle-BorderWidth="3px" 
                    HeaderStyle-BorderStyle="Double"
                    ItemStyle-BorderColor="Gray" ItemStyle-BorderWidth="3px" 
                    ItemStyle-BorderStyle="Double">
                    <itemstyle horizontalalign="Center" />
                </asp:BoundField>                
                </Columns>
            </slx:SligoDataGrid>
        </td>
    </tr>

          <tr>
        <td>

            <uc6:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            <uc7:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>

            <uc11:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
           
            <uc8:BottomLinkDownload id="BottomLinkDownload1" runat="server" Col="16"/>
            <SPAN class="text">
	            <p><a href="javascript:popup('http://dpi.wi.gov/spr/ret_use.html')" onClick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
            </p></SPAN>        
        </td>
    </tr>

</table>        
</asp:Content>
