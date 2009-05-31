<%@ Page Language="C#" MasterPageFile="~/WI.Master" AutoEventWireup="true" CodeBehind="QStringLinks.aspx.cs" Inherits="SligoCS.Web.WI.QStringDemo.QStingLinks" Title="Links page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>
    <tr>
        <td colspan="2">This is the QString Links page.  Each of the links below will take you to the QStringDemo page, 
            but the next page will retrieve values from different sources.</td>            
    </tr>
    <tr>
        <td>Pass variables by QString only.</td>
        <td><a href="<%= DESTURL%>?FULLKEY=013619040022&GraphFile=HIGHSCHOOLCOMPLETION&CompareTo=PRIORYEARS&ORGLEVEL=SC&Group=AllStudentsFAY&DN=Milwaukee&SN=Madison%20Hi&DETAIL=YES&NumSchools=4&ZBackTo=performance.aspx&FileQuery=select%20count(*)%20from%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20sys.databases&FileName=c:\sample.txt&SchoolWebaddress=http://mpsportal.milwaukee.k12.wi.us&DistrictWebaddress=http://www.milwaukee.k12.wi.us&STYP=9&COUNTY=40&YearLocal=2007&TrendStartYearLocal=1997&WhichSchool=4&ConferenceKey=27&DistrictID=3619&GradeBreakout=999&GradeBreakoutLAG=555&GradeBreakoutEDISA=212">By QString</a></td>
    </tr>
    <tr>
        <td>Pass variables by Session only.</td>
        <td>
            <asp:Button ID="btnSession" runat="server" Text="By Session" OnClick="linkSession_Click" /></td>
    </tr>
    <tr>
        <td>Pass variables by Config [default values] only.</td>
        <td><asp:Button ID="btnConfig" runat="server" Text="By Config" OnClick="linkConfig_Click" /></td>
        
    </tr>
    <tr>
        <td>Pass variables by mix of QString, Session, and Config.</td>
        <td><asp:Button ID="btnMixed" runat="server" Text="Mixed Mode" OnClick="linkMixed_Click" /></td>
    </tr>
</table>
</asp:Content>
