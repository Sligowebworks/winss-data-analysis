<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="StateTestsPerformance.aspx.cs" Inherits="SligoCS.Web.WI.StateTestsPerformance" title="How did students perform on state test at grades 3-8 and 10?"%>
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
    <tr><td><uc2:ParamsLinkBox ID="ParamsLinkBox1" runat="server" ShowTR_CompareTo="true" ShowTR_Group="true" ShowTR_STYP="true" /></td></tr>
    <tr>
        <td>
            <asp:hyperlink ID="ChangeSelectedSchoolOrDistrict" Font-Size="Small" runat="server" />
        </td>
    </tr>
         <asp:Panel ID="GraphPanel" runat="server">    
    <tr>
        <td>
            <Graph:GraphBarChart ID="barChart" runat="server"></Graph:GraphBarChart>
            	<span class="text"><br />
		        <!-- direct cut n' paste from old asp, html comments too :: MZD Jan '09 -->
				<!-- * Cutscores for proficiency levels changed effective November, 2002. [<a href="javascript:popup('http://dpi.wi.gov/oea/profdesc.html')" onClick="setCookie(question, url)">more</A>]<br> -->
				<!-- *<a href="http://dpi.wi.gov/oea/profdesc.html" target="_offsite_wsas">Proficiency data for November 2002 and later are not comparable to earlier years.</a>  Some subject area tests are given only at grades 4, 8, and 10.  -->
				<br />* <a href="javascript:popup('http://dpi.wi.gov/oea/profdesc.html')" onClick='setCookie(question, url)'>Proficiency data for November 2002 and later are not comparable to earlier years.</a>  Some subject area tests are given only at grades 4, 8, and 10. 
			
				<!-- FAY = full (prior) academic year. <a href="javascript:popup('http://dpi.wi.gov/oea/kce_q&a.html')" onClick='setCookie(question, url)'>What are WSAS, WKCE, and WAA?</a><br> -->
				FAY = full academic year. <a href="javascript:popup('http://dpi.wi.gov/oea/kce_q&a.html')" onClick='setCookie(question, url)'>What are WSAS, WKCE, and WAA?</a><br />
	</span>
        </td>
    </tr>    
    </asp:Panel>

      <tr>
        <td>
            <cc1:SligoDataGrid ID="SligoDataGrid2" runat="server" 
            AutoGenerateColumns="true"
                OnRowDataBound="SligoDataGrid2_RowDataBound"  >
            </cc1:SligoDataGrid>
        </td>
    </tr>

          <tr>
        <td>
<!-- MZD:: ToDo: these bottom links copied from DropOuts page; needs review -->
            <uc10:BottomLinkEnrollmentCounts id="BottomLinkEnrollmentCounts1" runat="server"/>
            <uc6:BottomLinkViewReport id="BottomLinkViewReport1" runat="server"/>
            <uc7:BottomLinkViewProfile id="BottomLinkViewProfile1" runat="server"/>

            <uc11:BottomLinkWhyNotReported ID="BottomLinkWhyNotReported1" runat="server" />
           
            <uc8:BottomLinkDownload id="BottomLinkDownload1" runat="server" Col="16"/>
            <span class="text">
	            <br /><a href="javascript:popup('http://dpi.wi.gov/spr/ret_use.html')" onClick="setCookie(question, url)">What are some questions to consider when reviewing these graphs?</a>
            </p></span>        
        </td>
    </tr>
 </table>        
</asp:Content>
