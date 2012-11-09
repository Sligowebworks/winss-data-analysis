<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InitSelSchoolInfo.ascx.cs" Inherits="SligoCS.Web.WI.WebUserControls.InitSelSchoolInfo" %>

<table cellspacing="0" cellpadding="0" border="0" style="padding:0px 0px 0px 0px; width:185px;" class="datatext">
<tr valign="top">
<td align="left">
To view public school or district data, first select your school or district. Fill in one of the boxes on the right. Type in a whole name or a part of a school, district, or county name. Next click on the GO button. If you have trouble finding your school or district, try clicking on the first letter of the name OR click on the <a href="javascript:popup('http://legis.wisconsin.gov/statutes/stat0116.pdf');" class='lightblue' >CESA</a> number on the Wisconsin map.
</td></tr>
<tr><td style="width: 185px">&nbsp;</td></tr>
<tr><td style="width: 185px">
The database should return a list of schools or districts. Each school or district name is a hyperlink that will take you to pages where you can click on questions you want answered.
</td></tr>
<tr><td style="width: 185px">&nbsp;</td></tr>
<tr><td style="width: 185px">
<asp:HyperLink ID="STATELevelData" runat="server" CssClass="lightblue" Text="STATE Level Data" Font-Bold="true" />
</td></tr>
<tr><td style="width: 185px">&nbsp;</td></tr>
<tr><td style="width: 185px">
<a href="http://winss.dpi.wi.gov/winss_data_download" class="lightblue" target="DownloadOptions">Download Options</a>&nbsp;|&nbsp;<a href="javascript:popup('http://winss.dpi.wi.gov/winss_usetips_data')"  class="lightblue">Tips</a>
</td></tr>
<tr><td style="width: 185px">&nbsp;</td></tr>
<tr><td style="width: 185px">
See also <a href="javascript:popup('http://winss.dpi.wi.gov/winss_work_settings')" class="lightblue">Workstation Settings</a>. To use this web application, you must have cookies enabled in your browser. 
</td></tr>
<tr><td style="width: 185px">
<br />Data for private schools participating in the <a href="javascript:popup('http://dpi.wi.gov/oea_mpcp/results')"  class="lightblue" >Milwaukee Parental Choice Program</a> are available. 
&nbsp;</td></tr>
</table>