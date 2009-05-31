<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InitSelSchoolInfo.ascx.cs" Inherits="SligoCS.Web.WI.WebUserControls.InitSelSchoolInfo" %>

<table cellspacing="0" cellpadding="0" border="0" style="padding:0px 0px 0px 0px; width:185px;" class="datatext">
<tr valign="top">
<td align="left">
To view school or district data, first select your school or district. Fill in one of the boxes on the right. Type in a whole name or a part of a school, district, or county name. Next click on the GO button. If you have trouble finding your school or district, try clicking on the first letter of the name OR click on the <a href='http://dpi.wi.gov/oea/cesainfo.html' class='lightblue' target='help' onclick="popup('http://dpi.wi.gov/oea/cesainfo.html','help');">CESA</a> number on the Wisconsin map.
</td></tr>
<tr><td style="width: 185px">&nbsp;</td></tr>
<tr><td style="width: 185px">
The database should return a list of schools or districts. Each school or district name is a hyperlink that will take you to pages where you can click on questions you want answered.
</td></tr>
<tr><td style="width: 185px">&nbsp;</td></tr>
<tr><td style="width: 185px">
<%--<a href="/SligoWI/questions.aspx?fullkey=ZZZZZZZZZZZZ&DN=None+Chosen&SN=None+Chosen&TYPECODE=0&OrgLevel=ST" class="lightblue"><b>STATE Level Data</b></a>--%>
<asp:HyperLink ID="STATELevelData" runat="server" CssClass="lightblue" Text="STATE Level Data" Font-Bold="true" />
</td></tr>
<tr><td style="width: 185px">&nbsp;</td></tr>
<tr><td style="width: 185px">
<a href="/SligoWI/Selschool.aspx" class="lightblue" target="DownloadOptions">Download Options</a>&nbsp;|&nbsp;<a href="http://dpi.wi.gov/sig/usetips_data.html" target="help" onclick="popup('http://dpi.wi.gov/sig/usetips_data.html','help');" class="lightblue">Tips</a>
</td></tr>
<tr><td style="width: 185px">&nbsp;</td></tr>
<tr><td style="width: 185px">
See also <a href="http://dpi.wi.gov/sig/work_settings.html" class="lightblue" target="help" onclick="popup('http://dpi.wi.gov/sig/work_settings.html','help');">Workstation Settings</a>. To use this web application, you must have cookies enabled in your browser. 
</td></tr></table>