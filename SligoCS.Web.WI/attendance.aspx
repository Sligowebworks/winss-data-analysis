<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="attendance.aspx.cs" Inherits="SligoCS.Web.WI.attendance" Title="Data Analysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<table width="390" cellpadding="0" cellspacing="0">
<tr><td align="right">

<img src="<%= Request.ApplicationPath %>/images/data_title2.gif" width="310" height="37" hspace="0" vspace="0" border="0" alt="What about attendance and behavior?" />
</td>
</tr>
<tr><td align="left">
<br /><br />

<ul class="text">	
<asp:Label ID="Label2" runat="server" Font-Bold = "true" Text="Examining Attendance Patterns" />
<li>
    What percent of students attend school each day? (Go to: <a href="http://wisedash.dpi.wi.gov/Dashboard/Page/Home/Topic%20Area/Attendance/" target="_blank">WISEdash Attendance</a>)
</li>

<li>
    What percent of students are habitually truant? (Go to: <a href="http://apps2.dpi.wi.gov/sdpr/redirect?topic=habitual-truancy<%= GlobalValues.SdprQS %>">SDPR Habitual Truancy</a>)
</li>

</ul>

<ul class="text">	
<asp:Label ID="Label1" runat="server" Font-Bold = "true" Text="Examining Student Involvement" />
<li>
    Do students participate in school supported activities? (Go To:  <a href="http://apps2.dpi.wi.gov/sdpr/redirect?topic=community-activities<%= GlobalValues.SdprQS %>">SDPR School-Sponsored Community Activities</a> and <a href="http://apps2.dpi.wi.gov/sdpr/redirect?topic=extra-curricular-activities<%= GlobalValues.SdprQS %>">SDPR Extra-/Co-Curricular Activities</a>)
</li>

<li>
        <asp:PlaceHolder ID="courses" runat="server" />
</li>

</ul>

<ul class="text">	
<asp:Label ID="Label3" runat="server" Font-Bold = "true" Text="Examining Disciplinary Patterns" />
<li>
    <asp:PlaceHolder ID="expelled" runat="server" />
</li> 

<li>
    <asp:PlaceHolder ID="lost_school_days" runat="server" />
</li>

<li>
    <asp:PlaceHolder ID="incidents" runat="server" />
</li>

<li>
    <asp:PlaceHolder ID="after_expelled" runat="server" />
</li>

</ul>

<ul class="text">
<asp:Label ID="Label4" runat="server" Font-Bold = "true" Text="Examining Dropout Rates" />
<li><!--
<a href="http://data.dpi.state.wi.us/data/graphshell.asp?fullkey=ZZZZZZZZZZZZ&DN=None+Chosen&SN=None+Chosen&TYPECODE=0&OrgLevel=ST&GRAPHFILE=DROPOUTS">How many students dropped out of school last year?</a>-->
 <asp:PlaceHolder ID="dropouts" runat="server" />
</li>
</ul>		
		
</td></tr></table>

</asp:Content>
