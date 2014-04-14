<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="performance.aspx.cs" Inherits="SligoCS.Web.WI.performance" Title="Data Analysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<table width="390" cellpadding="0" cellspacing="0" border="0">
<tr><td align="right">
<img src="<%= Request.ApplicationPath %>/images/data_title4.gif" width="327" height="41" border="0" alt="How are students performing academically?" />
</td></tr>
<tr><td>&nbsp;</td></tr>
<tr><td>
<ul class="text">
<asp:Label ID="Label1" runat="server" Font-Bold = "true" Text="Examining School Performance on Statewide Tests." />
<li>How did students perform on state tests at grades 3-8 and 10?
(Go to: <a href="http://wisedash.dpi.wi.gov/Dashboard/Page/Home/Topic%20Area/Academic%20Performance/WSAS%20%28WKCE%20and%20WAA-SwD%29" target="_blank">WISEdash WSAS</a>)</li>   

   <asp:Panel ID="pnl_HasG3" runat="server"></asp:Panel>

        
<asp:Panel ID="pnl_WRCT" runat="server" Visible="false">
    <li>
        <asp:PlaceHolder ID="wrct" runat="server" />
    </li>
</asp:Panel>
    
</ul>

<br />

<ul class="text">
<asp:Label ID="Label2" runat="server" Font-Bold = "true" Text="Reviewing Other Student Performance Indicators" />
<li>
    <asp:Label ID="Label4" runat="server" Text="What other evidence of student proficiency is available locally?" />
</li>

<li>
    <asp:PlaceHolder ID="retention" runat="server" />
</li>

<asp:Panel ID="pnl_grade_12" Visible="false" runat="server">

<li>How did students perform on college admissions and placement tests?  
    (Go to: <a href="http://wisedash.dpi.wi.gov/Dashboard/Page/Home/Topic%20Area/Academic%20Performance/ACT" target="_blank">WISEdash ACT</a> and 
    <a href="http://wisedash.dpi.wi.gov/Dashboard/Page/Home/Topic%20Area/Academic%20Performance/AP" target="_blank">WISEdash Advanced Placement Program  Exams</a> )
</li>			

<li>
    What are the high school completion rates? (Go to: <a href="http://wisedash.dpi.wi.gov/Dashboard/Page/Home/Topic%20Area/Graduation/" target="_blank">WISEdash HS Completion</a>
</li>

<li>
    <asp:PlaceHolder ID="postgrad_plan" runat="server" />
</li>

</asp:Panel>
</ul>

</td></tr></table>
</asp:Content>